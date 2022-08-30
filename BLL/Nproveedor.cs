using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Data;
using DAL;

namespace BLL
{
    public class Nproveedor
    {
        static SQLConnClass conn;
        ArrayList aParam;
        DataSet ds;
        DataTable tbl;

        public DataTable listarProveedores()
        {
            aParam = new ArrayList();
            conn = new SQLConnClass();
            ds = new DataSet();
            tbl = new DataTable();

            ds = conn.dsRunStoredProcedure(ds, "spConsulProveedores", aParam);
            tbl = ds.Tables[0];

            return tbl;
        }

    }
}
