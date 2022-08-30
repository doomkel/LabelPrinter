using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Data;
using DAL;
using EL;

namespace BLL
{
    public class Nusuario
    {
        
        SQLConnClass conn;
        ArrayList aParam;
        DataSet ds;
        DataTable tbl;
        

        public int agregarUsuario(string _sUsuario, string _sClave, string _sPerfil)
        {
            string _sLlave = "r3g1str0";
            conn = new SQLConnClass();
            aParam = new ArrayList();
            int iRes;
                        
            conn.spArgumentsCollection(aParam, "@sUsuario", _sUsuario, "varchar");
            conn.spArgumentsCollection(aParam, "@sClave", _sClave, "varchar");
            conn.spArgumentsCollection(aParam, "@sPerfil", _sPerfil, "varchar");
            conn.spArgumentsCollection(aParam, "@sLlave", _sLlave, "varchar");

            iRes = conn.iRunStoredProcedure("spInsertarUsuario", aParam);

            return iRes;
        }


        public DataTable validaUsuario(string _sUsuario, string _sClave)
        {
            string _sLlave = "r3g1str0";
            conn = new SQLConnClass();
            aParam = new ArrayList();
            ds = new DataSet();
            tbl = new DataTable();

            conn.spArgumentsCollection(aParam, "@sUsuario", _sUsuario, "varchar");
            conn.spArgumentsCollection(aParam, "@sClave", _sClave, "varchar");
            conn.spArgumentsCollection(aParam, "@sLlave", _sLlave, "varchar");

            ds = conn.dsRunStoredProcedure(ds,"spConsulUsuario", aParam);
            if (ds.Tables.Count > 0)
            {
                tbl = ds.Tables[0];
            }
	
            return tbl;

        }
    }
}
