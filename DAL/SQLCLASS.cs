using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
//using System.Configuration.ConfigurationManager;
using System.Configuration;
using System.Collections;
//using System.Reflection;


namespace DAL
{
    public class SQLConnClass
    {
        SqlConnection SQLConn = new SqlConnection();
        public DataTable SQLTable = new DataTable();


        internal class SPArgBuild
        {
            internal string parameterName = "";
            internal string parameterValue = "";
            /// <summary>
            /// Write full data type, such as SqlDBType.VarChar.
            /// </summary>
            internal string pramValueType = "";


            /// <summary>
            /// Use to create SP Argument Build conestruction.
            /// </summary>
            /// <param name="pramName">SP Argument Parameter Name.</param>
            /// <param name="pramValue">SP Argument Parameter Value.</param>
            internal SPArgBuild(string pramName, string pramValue, string pramValueType)
            {
                this.parameterName = pramName;
                this.parameterValue = pramValue;
                this.pramValueType = pramValueType;


            }
        }

        public SQLConnClass()
        {
            SQLConn.ConnectionString = ConfigurationManager.ConnectionStrings["myConnString"].ConnectionString;
        }


        /// <summary>
        /// Use este procedimiento para obtener datos de la base de datos, no use este para comandos.
        /// </summary>
        /// <param name="sCommand">Use solo para SELECT</param>
        public void retrieveData(string sCommand)
        {
            try
            {
                SQLConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(sCommand, SQLConn);
                da.Fill(SQLTable);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            finally
            {
                SQLConn.Close();
            }


        }
        /// <summary>
        /// Use este procedimiento para INSERT, UPDATE AND DELETE
        /// </summary>
        /// <param name="sCommand">Use solo comandos. INSERT, UPDATE AND DELETE</param>
        public int commandExec(string sCommand)
        {
            try
            {
                SQLConn.Open();
                SqlCommand sqlcomm = new SqlCommand(sCommand, SQLConn);


                int rowinfected = sqlcomm.ExecuteNonQuery();
                //if (rowinfected > 0)
                //{
                //    Console.Write("El comando termino.");
                //}
                //else
                //{
                //    Console.Write("Algo no funciono bien en la DBMS, intente nuevamente.");
                //}
                return rowinfected;


            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return 0;
            }
            finally
            {
                SQLConn.Close();
            }
        }








        /// <summary>
        /// This function built an Array List, which is collection of some SP parameter's Name, Value and Data type.
        /// </summary>
        /// <param name="arrLst">Array List which will store all argument.</param>
        /// <param name="spParmName">SP Argument Parameter Name.</param>
        /// <param name="spParmValue">SP Argument Parameter Value.</param>
        /// <param name="spPramValueType">Parameter value type EXACTLY same as SqlDBType. E.g. 'SqlDbType.BigInt' will 'BigInt'. </param>
        /// <returns></returns>
        public ArrayList spArgumentsCollection(ArrayList arrLst, string spParmName, string spParmValue, string spPramValueType)
        {
            SPArgBuild spArgBuiltObj = new SPArgBuild(spParmName, spParmValue, spPramValueType);
            arrLst.Add(spArgBuiltObj);
            return arrLst;
        }


        /// <summary>
        /// Run a stored procedure of Select SQL type.
        /// </summary>
        /// <param name="dbConnStr">Connection String to connect Sql Server</param>
        /// <param name="ds">DataSet which will return after filling Data</param>
        /// <param name="spName">Stored Procedure Name</param>
        /// <param name="spPramArrList">Parameters in ArrayList</param>
        /// <returns>Return DataSet after filing data by SQL.</returns>
        public DataSet dsRunStoredProcedure(DataSet ds, string spName, ArrayList spPramArrList)
        {
            try
            {
                SQLConn.Open();


                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = SQLConn;
                cmd.CommandText = spName;


                string spPramName = ""; 
                string spPramValue = "";
                string spPramDataType = "";
                for (int i = 0; i < spPramArrList.Count; i++)
                {
                    spPramName = ((SPArgBuild)spPramArrList[i]).parameterName;
                    spPramValue = ((SPArgBuild)spPramArrList[i]).parameterValue;
                    spPramDataType = ((SPArgBuild)spPramArrList[i]).pramValueType;
                    SqlParameter pram = null;
                    #region SQL DB TYPE AND VALUE ASSIGNMENT
                    switch (spPramDataType.ToUpper())
                    {
                        case "BIGINT":
                            pram = cmd.Parameters.Add(spPramName, SqlDbType.BigInt);
                            pram.Value = spPramValue;
                            break;


                        case "BINARY":
                            pram = cmd.Parameters.Add(spPramName, SqlDbType.Binary);
                            pram.Value = spPramValue;
                            break;


                        case "BIT":
                            pram = cmd.Parameters.Add(spPramName, SqlDbType.Bit);
                            pram.Value = spPramValue;
                            break;


                        case "CHAR":
                            pram = cmd.Parameters.Add(spPramName, SqlDbType.Char);
                            pram.Value = spPramValue;
                            break;


                        case "DATETIME":
                            pram = cmd.Parameters.Add(spPramName, SqlDbType.DateTime);
                            pram.Value = spPramValue;
                            break;


                        case "DECIMAL":
                            pram = cmd.Parameters.Add(spPramName, SqlDbType.Decimal);
                            pram.Value = spPramValue;
                            break;


                        case "FLOAT":
                            pram = cmd.Parameters.Add(spPramName, SqlDbType.Float);
                            pram.Value = spPramValue;
                            break;


                        case "IMAGE":
                            pram = cmd.Parameters.Add(spPramName, SqlDbType.Image);
                            pram.Value = spPramValue;
                            break;


                        case "INT":
                            pram = cmd.Parameters.Add(spPramName, SqlDbType.Int);
                            pram.Value = spPramValue;
                            break;


                        case "MONEY":
                            pram = cmd.Parameters.Add(spPramName, SqlDbType.Money);
                            pram.Value = spPramValue;
                            break;


                        case "NCHAR":
                            pram = cmd.Parameters.Add(spPramName, SqlDbType.NChar);
                            pram.Value = spPramValue;
                            break;


                        case "NTEXT":
                            pram = cmd.Parameters.Add(spPramName, SqlDbType.NText);
                            pram.Value = spPramValue;
                            break;


                        case "NVARCHAR":
                            pram = cmd.Parameters.Add(spPramName, SqlDbType.NVarChar);
                            pram.Value = spPramValue;
                            break;


                        case "REAL":
                            pram = cmd.Parameters.Add(spPramName, SqlDbType.Real);
                            pram.Value = spPramValue;
                            break;


                        case "SMALLDATETIME":
                            pram = cmd.Parameters.Add(spPramName, SqlDbType.SmallDateTime);
                            pram.Value = spPramValue;
                            break;


                        case "SMALLINT":
                            pram = cmd.Parameters.Add(spPramName, SqlDbType.SmallInt);
                            pram.Value = spPramValue;
                            break;


                        case "SMALLMONEY":
                            pram = cmd.Parameters.Add(spPramName, SqlDbType.SmallMoney);
                            pram.Value = spPramValue;
                            break;


                        case "TEXT":
                            pram = cmd.Parameters.Add(spPramName, SqlDbType.Text);
                            pram.Value = spPramValue;
                            break;


                        case "TIMESTAMP":
                            pram = cmd.Parameters.Add(spPramName, SqlDbType.Timestamp);
                            pram.Value = spPramValue;
                            break;


                        case "TINYINT":
                            pram = cmd.Parameters.Add(spPramName, SqlDbType.TinyInt);
                            pram.Value = spPramValue;
                            break;


                        case "UDT":
                            pram = cmd.Parameters.Add(spPramName, SqlDbType.Udt);
                            pram.Value = spPramValue;
                            break;


                        case "UMIQUEIDENTIFIER":
                            pram = cmd.Parameters.Add(spPramName, SqlDbType.UniqueIdentifier);
                            pram.Value = spPramValue;
                            break;


                        case "VARBINARY":
                            pram = cmd.Parameters.Add(spPramName, SqlDbType.VarBinary);
                            pram.Value = spPramValue;
                            break;


                        case "VARCHAR":
                            pram = cmd.Parameters.Add(spPramName, SqlDbType.VarChar);
                            pram.Value = spPramValue;
                            break;


                        case "VARIANT":
                            pram = cmd.Parameters.Add(spPramName, SqlDbType.Variant);
                            pram.Value = spPramValue;
                            break;


                        case "XML":
                            pram = cmd.Parameters.Add(spPramName, SqlDbType.Xml);
                            pram.Value = spPramValue;
                            break;
                    }
                    #endregion
                    pram.Direction = ParameterDirection.Input;
                }


                SqlDataAdapter adap = new SqlDataAdapter(cmd);


                adap.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return ds;
            }
            finally
            {
                SQLConn.Close();


            }


        }


        /// <summary>
        /// Run a stored procedure which will execure some nonquery SQL.
        /// </summary>
        /// <param name="dbConnStr">Connection String to connect Sql Server</param>
        /// <param name="spName">Stored Procedure Name</param>
        /// <param name="spPramArrList">Parameters in a ArrayList</param>
        public int iRunStoredProcedure(string spName, ArrayList spPramArrList)
        {
            try
            {
                SQLConn.Open();


                SqlCommand cmd = new SqlCommand();
                cmd.CommandTimeout = 420;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = SQLConn;
                cmd.CommandText = spName;


                string spPramName = "";
                string spPramValue = "";
                string spPramDataType = "";
                for (int i = 0; i < spPramArrList.Count; i++)
                {
                    spPramName = ((SPArgBuild)spPramArrList[i]).parameterName;
                    spPramValue = ((SPArgBuild)spPramArrList[i]).parameterValue;
                    spPramDataType = ((SPArgBuild)spPramArrList[i]).pramValueType;
                    SqlParameter pram = null;
                    #region SQL DB TYPE AND VALUE ASSIGNMENT
                    switch (spPramDataType.ToUpper())
                    {
                        case "BIGINT":
                            pram = cmd.Parameters.Add(spPramName, SqlDbType.BigInt);
                            pram.Value = spPramValue;
                            break;


                        case "BINARY":
                            pram = cmd.Parameters.Add(spPramName, SqlDbType.Binary);
                            pram.Value = spPramValue;
                            break;


                        case "BIT":
                            pram = cmd.Parameters.Add(spPramName, SqlDbType.Bit);
                            pram.Value = spPramValue;
                            break;


                        case "CHAR":
                            pram = cmd.Parameters.Add(spPramName, SqlDbType.Char);
                            pram.Value = spPramValue;
                            break;


                        case "DATETIME":
                            pram = cmd.Parameters.Add(spPramName, SqlDbType.DateTime);
                            pram.Value = spPramValue;
                            break;


                        case "DECIMAL":
                            pram = cmd.Parameters.Add(spPramName, SqlDbType.Decimal);
                            pram.Value = spPramValue;
                            break;


                        case "FLOAT":
                            pram = cmd.Parameters.Add(spPramName, SqlDbType.Float);
                            pram.Value = spPramValue;
                            break;


                        case "IMAGE":
                            pram = cmd.Parameters.Add(spPramName, SqlDbType.Image);
                            pram.Value = spPramValue;
                            break;


                        case "INT":
                            pram = cmd.Parameters.Add(spPramName, SqlDbType.Int);
                            pram.Value = spPramValue;
                            break;


                        case "MONEY":
                            pram = cmd.Parameters.Add(spPramName, SqlDbType.Money);
                            pram.Value = spPramValue;
                            break;


                        case "NCHAR":
                            pram = cmd.Parameters.Add(spPramName, SqlDbType.NChar);
                            pram.Value = spPramValue;
                            break;


                        case "NTEXT":
                            pram = cmd.Parameters.Add(spPramName, SqlDbType.NText);
                            pram.Value = spPramValue;
                            break;


                        case "NVARCHAR":
                            pram = cmd.Parameters.Add(spPramName, SqlDbType.NVarChar);
                            pram.Value = spPramValue;
                            break;


                        case "REAL":
                            pram = cmd.Parameters.Add(spPramName, SqlDbType.Real);
                            pram.Value = spPramValue;
                            break;


                        case "SMALLDATETIME":
                            pram = cmd.Parameters.Add(spPramName, SqlDbType.SmallDateTime);
                            pram.Value = spPramValue;
                            break;


                        case "SMALLINT":
                            pram = cmd.Parameters.Add(spPramName, SqlDbType.SmallInt);
                            pram.Value = spPramValue;
                            break;


                        case "SMALLMONEY":
                            pram = cmd.Parameters.Add(spPramName, SqlDbType.SmallMoney);
                            pram.Value = spPramValue;
                            break;


                        case "TEXT":
                            pram = cmd.Parameters.Add(spPramName, SqlDbType.Text);
                            pram.Value = spPramValue;
                            break;


                        case "TIMESTAMP":
                            pram = cmd.Parameters.Add(spPramName, SqlDbType.Timestamp);
                            pram.Value = spPramValue;
                            break;


                        case "TINYINT":
                            pram = cmd.Parameters.Add(spPramName, SqlDbType.TinyInt);
                            pram.Value = spPramValue;
                            break;


                        case "UDT":
                            pram = cmd.Parameters.Add(spPramName, SqlDbType.Udt);
                            pram.Value = spPramValue;
                            break;


                        case "UMIQUEIDENTIFIER":
                            pram = cmd.Parameters.Add(spPramName, SqlDbType.UniqueIdentifier);
                            pram.Value = spPramValue;
                            break;


                        case "VARBINARY":
                            pram = cmd.Parameters.Add(spPramName, SqlDbType.VarBinary);
                            pram.Value = spPramValue;
                            break;


                        case "VARCHAR":
                            pram = cmd.Parameters.Add(spPramName, SqlDbType.VarChar);
                            pram.Value = spPramValue;
                            break;


                        case "VARIANT":
                            pram = cmd.Parameters.Add(spPramName, SqlDbType.Variant);
                            pram.Value = spPramValue;
                            break;


                        case "XML":
                            pram = cmd.Parameters.Add(spPramName, SqlDbType.Xml);
                            pram.Value = spPramValue;
                            break;
                    }
                    #endregion
                    pram.Direction = ParameterDirection.Input;
                }
                int rowinfected = cmd.ExecuteNonQuery();
                return rowinfected;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return 0;
            }
            finally
            {
                SQLConn.Close();
            }
        }







    }
}
