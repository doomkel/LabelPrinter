using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Data;
using System.Text;
using DAL;
using EL;

namespace BLL
{
    public class Ntallas
    {
        static SQLConnClass conn;
        ArrayList aParam;
        DataSet ds;
        DataTable tbl;

        public int ActualizarTallas(List<Etalla> _lstTallas)
        {
            
            int iRes = 0;
            foreach (Etalla item in _lstTallas)
            {
                aParam = new ArrayList();
                conn = new SQLConnClass();            
                conn.spArgumentsCollection(aParam, "@sTalla", item.talla, "varchar");
                conn.spArgumentsCollection(aParam, "@sTallaCorta", item.talla_corta, "varchar");
                iRes += conn.iRunStoredProcedure("spActualizaTalla", aParam);

            }

            return iRes;
        }
    }
}
