using Microsoft.Extensions.Configuration;
using StudentRecordManagementApp.Models;
using StudentRecordManagementApp.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace StudentRecordManagementApp.Repository
{
    public class StudentRepository : IStudentService
    {
        private readonly IConfiguration _cofiguration;
        public StudentRepository(IConfiguration configuration)
        {
            _cofiguration = configuration;
            ConnectionString = _cofiguration.GetConnectionString("DBConnection");
            ProviderName = "System.Data.SqlClient";
        }
        public string ConnectionString { get; set; }
        public string ProviderName { get; set; }



        ProcessAccess accData = new ProcessAccess();
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

        public string InsertStudent(Student model)
        {
            throw new NotImplementedException();
        }
    }
}
