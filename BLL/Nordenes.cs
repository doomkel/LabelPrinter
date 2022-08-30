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
    public class Nordenes
    {

        static SQLConnClass conn;
        ArrayList aParam;
        DataSet ds;
        DataTable tbl;

        public DataTable listarOrdenes(string _sStatus)
        {
            conn = new SQLConnClass();
            aParam = new ArrayList();
            ds = new DataSet();
            tbl = new DataTable();

            conn.spArgumentsCollection(aParam, "@sStatus", _sStatus, "varchar");

            ds = conn.dsRunStoredProcedure(ds, "spConsulOrdenes", aParam);
            tbl = ds.Tables[0];

            return tbl;
        }

        public DataTable listarOrdenesDet(string _sOrden)
        {
            conn = new SQLConnClass();
            aParam = new ArrayList();
            ds = new DataSet();
            tbl = new DataTable();

            conn.spArgumentsCollection(aParam, "@iOrden", _sOrden, "int");

            ds = conn.dsRunStoredProcedure(ds, "spConsulOrdenDetalle", aParam);
            tbl = ds.Tables[0];

            return tbl;
        }

        public DataTable listarOrden(string _sOrden)
        {
            conn = new SQLConnClass();
            aParam = new ArrayList();
            ds = new DataSet();
            tbl = new DataTable();

            conn.spArgumentsCollection(aParam, "@iOrden", _sOrden, "int");

            ds = conn.dsRunStoredProcedure(ds, "spConsulOrden", aParam);
            tbl = ds.Tables[0];

            return tbl;
        }

        public DataTable listarOrdenes(DateTime _dFechaIni, DateTime _dFechaFin)
        {
            conn = new SQLConnClass();
            aParam = new ArrayList();
            ds = new DataSet();
            tbl = new DataTable();

            conn.spArgumentsCollection(aParam, "@dFechaIni", _dFechaIni.ToString(), "smalldatetime");
            conn.spArgumentsCollection(aParam, "@dFechaFin", _dFechaFin.ToString(), "smalldatetime");

            ds = conn.dsRunStoredProcedure(ds, "spConsulOrdenFecha", aParam);
            tbl = ds.Tables[0];

            return tbl;
        }

        public DataTable listarOrdenesRefer(string _sReferencia)
        {
            conn = new SQLConnClass();
            aParam = new ArrayList();
            ds = new DataSet();
            tbl = new DataTable();

            conn.spArgumentsCollection(aParam, "@sReferencia", _sReferencia, "varchar");
            
            ds = conn.dsRunStoredProcedure(ds, "spConsulOrdenesRefer", aParam);
            tbl = ds.Tables[0];

            return tbl;
        }

        public int actualizaStatus(int _iOrden, int _iStatus)
        {
            int iRes;
            conn = new SQLConnClass();
            aParam = new ArrayList();

            conn.spArgumentsCollection(aParam, "@iOrden", _iOrden.ToString(), "int");
            conn.spArgumentsCollection(aParam, "@iStatus", _iStatus.ToString(), "int");

            iRes = conn.iRunStoredProcedure("spActualStatusOrden", aParam);

            return iRes;
        }

    }
}
