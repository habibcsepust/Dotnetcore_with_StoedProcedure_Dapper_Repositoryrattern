using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using StudentRecordManagementApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using StudentRecordManagementApp.Repository;

namespace StudentRecordManagementApp.Services
{
    public class StudentServices: IStudentService
    {
        private readonly IConfiguration _cofiguration;
        ProcessAccess accData = new ProcessAccess();
        StudentRepository _studentRepository = new StudentRepository();

        Student student = new Student();
        AllModels allModels = new AllModels();
        public StudentServices(IConfiguration configuration) {
            _cofiguration = configuration;
            ConnectionString = _cofiguration.GetConnectionString("DBConnection");

            ProviderName = "System.Data.SqlClient";
        }

        public string ConnectionString { get; set; }
        public string ProviderName { get; set; }

        public IDbConnection Connection
        {
            get {
                return new SqlConnection(ConnectionString);
            }
        }
        
        public string InsertStudent(Student model) {
            using (IDbConnection dbConnection = Connection) {
                DynamicParameters param = new DynamicParameters();
                param.Add("@Calltype", "INSERTSTUDENT");
                model.CallType = "INSERTSTUDENT";
                dbConnection.Open();
                var student = dbConnection.Query<Student>("SP_STUDENTAMANAGEMENT", model, commandType: CommandType.StoredProcedure);
                dbConnection.Close();
            }

            return "";
        }

        public List<Student> GetAllStudent()
        {
            List<Student> studentList = new List<Student>();

            try
            {
                DataSet ds1 = accData.GetTransInfo("SP_STUDENTAMANAGEMENT", "SELECTALLSTUDENT", ConnectionString);

                DataTable dt = ds1.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    //return;
                }

                studentList = (from DataRow dr in dt.Rows
                               select new Student()
                               {
                                   StudentId = Convert.ToInt32(dr["StudentId"]),
                                   FullName = dr["FullName"].ToString(),
                                   EmailAddress = dr["EmailAddress"].ToString(),
                                   City = dr["City"].ToString(),
                                   CreatedOn = Convert.ToDateTime(dr["CreatedOn"].ToString())
                               }).ToList();
                return studentList;
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return studentList;
            }
        }
    }

    public interface IStudentService {
        public string InsertStudent(Student model);
        public List<Student> GetAllStudent();
    }
}
