using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using System.Data;
using DAL;



namespace BLL
{
    public class Narchivo
    {
        static SQLConnClass conn;
        ArrayList aParam;
        DataSet ds;
        DataTable tbl;

        public int CargarArchivo(string _sArchivo, string _sProveedor, string _sEmpresa, string _sReferencia, int _iIdUsuario)
        {
            int iCarga = CargarTemporal(_sArchivo);
            
            if (iCarga == 1)
            {
                int iRefer = InsertarReferencias(_sProveedor, _sEmpresa, _sReferencia, _iIdUsuario);

                if (iRefer == 1)
                {

                    int iTrans = TransformaCarga();

                    if (iTrans == 1)
                    {
                        return 1;
                    }
                    else
                    {
                        return -2;
                    }
                    
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                return 0;
            }
        }


        private int CargarTemporal(string _sArchivo)
        {
            //variables
            int cnt;
            aParam = new ArrayList();
            conn = new SQLConnClass();
            ds = new DataSet();
            tbl = new DataTable();

            try
            {
                string sRuta = @"c:\temp";

                //copiar archivo a ruta 
                if (!System.IO.Directory.Exists(sRuta))
                {
                    System.IO.Directory.CreateDirectory(sRuta);
                }
                File.Delete(@"c:\temp\oc_tmp.txt");
                File.Copy(_sArchivo, @"c:\temp\oc_tmp.txt");


                //borrar, cargar y verificar
                conn.commandExec("delete from ordenestmp");

                conn.iRunStoredProcedure("spCargar2", aParam);

                conn.spArgumentsCollection(aParam, "@sTabla", "Ordenestmp", "varchar");
                ds = conn.dsRunStoredProcedure(ds, "spContar", aParam);
                tbl = ds.Tables[0];

                cnt = Convert.ToInt32(tbl.Rows[0][0].ToString());

                if (cnt > 0)
                {
                    return 1;
                }
                else
                {
                    return 0;

                }
            }
            catch 
            {
                return 0;

            }

        }


        private int InsertarReferencias(string _sProveedor, string _sEmpresa, string _sReferencia, int _iIdUsuario)
        {
            //variables
            int iRes;
            aParam = new ArrayList();
            conn = new SQLConnClass();
            ds = new DataSet();
            tbl = new DataTable();

            string dActual = DateTime.Today.ToString("yyyy-MM-dd");
            try
            {
                conn.spArgumentsCollection(aParam, "@dFecha", dActual, "datetime");
                conn.spArgumentsCollection(aParam, "@sProv", _sProveedor, "varchar");
                conn.spArgumentsCollection(aParam, "@sEmpresa", _sEmpresa, "varchar");
                conn.spArgumentsCollection(aParam, "@sRefer", _sReferencia, "varchar");
                conn.spArgumentsCollection(aParam, "@sIdUsuario", Convert.ToString(_iIdUsuario), "int");

                iRes = conn.iRunStoredProcedure("spInsertarReferencia", aParam);

                if (iRes > 0)
                {
                    return 1;
                }
                else
                {
                    return 0;

                }
            }
            catch
            {
                return 0;
            }
        }

        private int TransformaCarga()
        {
            //variables
            int iRes;
            aParam = new ArrayList();
            conn = new SQLConnClass();            
            
            try
            {
            iRes = conn.iRunStoredProcedure("spTransformOrdenes", aParam);

                if (iRes > 0)
                {
                    return 1;
                }
                else
                {
                    return 0;

                }
            }
            catch
            {
                return 0;
            }

        }





        //proceso no utilizado
        public int Cargar_Archivo(string _sArchivo, string _sTabla)
        {
            int cnt;
            conn = new SQLConnClass();
            aParam = new ArrayList();

            conn.commandExec("delete from ordenestmp");

            conn.spArgumentsCollection(aParam, "@sRuta", _sArchivo, "varchar");
            conn.spArgumentsCollection(aParam, "@sTabla", _sTabla, "varchar");
            conn.iRunStoredProcedure("usp_cargar", aParam);

            aParam.Clear();

            aParam.Add("ordenestmp");

            cnt = conn.iRunStoredProcedure("usp_contar", aParam);
            if (cnt > 0)
            {
                return 1;
            }
            else
            {
                return 0;
                
            }

        }

        
        //proceso no utilizado
        public string Procesar_Archivo(string _sArchivo)
        {
            StreamReader sr;
            StreamWriter sw;
            string sArchivo_Imp;

            sArchivo_Imp = @"c:\temp\prueba.txt";

            using (sr = new StreamReader(_sArchivo))
            {
                string sLine;
                while ((sLine = sr.ReadLine()) != null)
                {
                    var sCols = sLine.Split(',');
                    if (sLine.Length > 1)
                    {

                        //codigo de impresion de etiquetas
                        
                        using (sw = new StreamWriter(sArchivo_Imp))
                        {
                            sw.WriteLine(@"^XA");
                            sw.WriteLine(@"^FO250, 70^ADN, 11, 7^FD CORPORACION TECTRONIC SA de CV^FS");
                            sw.WriteLine(@"^B2N, 80, Y, Y, N^FD corptectr>147896325 ^FS");
                            sw.WriteLine(@"^XZ");

                        }


                    }
                }

            }


            return sArchivo_Imp;

        }

    }
}
