using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using DAL;

namespace BLL
{
    public class Nconsulta
    {
        static SQLConnClass conn;
        ArrayList aParam;
        DataSet ds;
        DataTable tbl;

        public DataTable ListarTabla(string _sTabla)
        {
            aParam = new ArrayList();
            conn = new SQLConnClass();
            ds = new DataSet();
            tbl = new DataTable();

            conn.spArgumentsCollection(aParam, "@sTabla", _sTabla, "varchar");
            ds = conn.dsRunStoredProcedure(ds, "spConsultaTabla", aParam);
            tbl = ds.Tables[0];

            return tbl;

        }


    }
}
