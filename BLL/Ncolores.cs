using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using DAL;
using EL;

namespace BLL
{
    public class Ncolores
    {
        static SQLConnClass conn;
        ArrayList aParam;
        DataSet ds;
        DataTable tbl;

        public int ActualizarColores(List<Ecolor> _lstColores)
        {
                       
            
            int iRes = 0;
            foreach (Ecolor item in _lstColores)
            {
                aParam = new ArrayList();
                conn = new SQLConnClass();

                conn.spArgumentsCollection(aParam, "@sColor", item.cod_color, "varchar");
                conn.spArgumentsCollection(aParam, "@sDescLarga", item.desc_larga, "varchar");
                conn.spArgumentsCollection(aParam, "@sDescCorta", item.desc_corta, "varchar");
                iRes += conn.iRunStoredProcedure("spActualizaColor", aParam);

            }

            return iRes;
        }
    }
}
