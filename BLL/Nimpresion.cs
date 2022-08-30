using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Data;
using System.IO;
using DAL;
using EL;

namespace BLL
{
    public class Nimpresion
    {
        static SQLConnClass conn;
        ArrayList aParam;
        DataSet ds;
        DataTable tbl;

        public string imprimirLista(ref EOperationResult objOpResult,  List<EordenDetalle> _lista, string _sEtiqueta, string _sImpresora, string _sEmpresa)
        {

            StreamWriter sw;
            string sArchivoImp;
            
            aParam = new ArrayList();
            conn = new SQLConnClass();
            ds = new DataSet();
            tbl = new DataTable();

            string sArchivoId = DateTime.Today.ToString("yyyyMMddHHmm");

            sArchivoImp = @"c:\temp\imp" + sArchivoId  + ".txt";
     

            try
            {

                conn.spArgumentsCollection(aParam, "@sEtiqueta", _sEtiqueta, "varchar");
                conn.spArgumentsCollection(aParam, "@sImpresora", _sImpresora, "varchar");
                conn.spArgumentsCollection(aParam, "@sEmpresa", _sEmpresa, "varchar");

                ds = conn.dsRunStoredProcedure(ds, "spConsultaCodigos", aParam);
                tbl = ds.Tables[0];

                //variables de codigos de impresion
                string sCod1 = tbl.Rows[0][0].ToString();
                string sCod2 = tbl.Rows[1][0].ToString();
                string sCod3 = tbl.Rows[2][0].ToString();
                string sCod4 = tbl.Rows[3][0].ToString();
                string sCod5 = tbl.Rows[4][0].ToString();
                string sCod6 = tbl.Rows[5][0].ToString();
                string sCod7 = tbl.Rows[6][0].ToString();

             
                //falta mes 10 , 11 y 12
                string sMes = DateTime.Today.Month.ToString();
                string sAno = DateTime.Today.Year.ToString().Substring(3,1);

                
                    //crear archivo con codigo de impresion
                    using (sw = new StreamWriter(sArchivoImp))
                    {

                        foreach (var item in _lista)
                        {
                            sw.WriteLine(@"^XA");
                            sw.WriteLine(@sCod1.Replace("#DATA#", item.no_proveedor.ToString("00000000") + "-" + item.estilo + "-" + item.cod_color + "-" + sMes + sAno));
                            sw.WriteLine(@sCod2.Replace("#DATA#", item.talla_corta + " " + item.desc_corta));
                            sw.WriteLine(@sCod3.Replace("#DATA#", item.division.ToString()));
                            sw.WriteLine(@sCod4.Replace("#DATA#", item.oc.ToString()));
                            sw.WriteLine(@sCod5.Replace("#DATA#", item.sku.ToString()));
                            sw.WriteLine(@sCod6.Replace("#DATA#", item.sku.ToString()));
                            sw.WriteLine(@sCod7.Replace("#DATA#", "$" + item.precio.ToString() + ".00"));
                            sw.WriteLine(@"^PQ" + item.pzas);
                            sw.WriteLine(@"^XZ");
                            
                        }
                        
                    }                    
                    
                    using (sw = new StreamWriter(@"C:\temp\imp.bat"))
                    {
                        sw.WriteLine(@"COPY /B """ + sArchivoImp + @""" \\GPO-WU-129\ZEBRAGEN");
                    }
                    
                    return "1";
                }
            
                

                catch (Exception ex)
                {
                    return ex.Message;
                }                   

        }
    }
}
