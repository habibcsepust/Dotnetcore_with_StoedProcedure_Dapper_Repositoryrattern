using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace StudentRecordManagementApp.Services
{
    public class ProcessAccess
    {

        private Hashtable _errObj;
        DataAccess _dataAccess;
        public ProcessAccess()
        {
            _dataAccess = new DataAccess();
            _errObj = new Hashtable();
        }





        public DataSet GetDataSet(SqlCommand Cmd,string conn)
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(conn);
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = Cmd;
                Cmd.CommandTimeout = 0;
                Cmd.Connection = sqlConnection;

                DataSet ds = new DataSet();
                adp.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                //this.SetError(ex);
                return null;
            }
        }

        public DataSet GetTransInfo(string SQLprocName, string CallType, string connectionString = "", string mDesc1 = "", string mDesc2 = "", string mDesc3 = "", string mDesc4 = "", string mDesc5 = "", string mDesc6 = "", string mDesc7 = "", string mDesc8 = "", string mDesc9 = "")
        {
            try
            {
                this.ClearErrors();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = SQLprocName;
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.Add(new SqlParameter("@Comp1", comCode));
                cmd.Parameters.Add(new SqlParameter("@CallType", CallType));
                cmd.Parameters.Add(new SqlParameter("@Desc1", mDesc1));
                cmd.Parameters.Add(new SqlParameter("@Desc2", mDesc2));

                //DataSet result = _dataAccess.GetDataSet(cmd);
                DataSet result = this.GetDataSet(cmd, connectionString);



                if (result == null)  //_result==false
                {
                    //this.SetError(_dataAccess.ErrorObject);
                }
                return result;
            }
            catch (Exception exp)
            {
                string message = exp.Message;
                return null;
            }// try
        }


        private void ClearErrors()
        {
            //this._errObj["Src"] = string.Empty;
            //this._errObj["Msg"] = string.Empty;
            //this._errObj["Location"] = string.Empty;
        }

        private void SetError(Hashtable errObject)
        {
            //this._errObj["Src"] = errObject["Src"];
            //this._errObj["Msg"] = errObject["Msg"];
            //this._errObj["Location"] = errObject["Location"];
        }


    }
}
