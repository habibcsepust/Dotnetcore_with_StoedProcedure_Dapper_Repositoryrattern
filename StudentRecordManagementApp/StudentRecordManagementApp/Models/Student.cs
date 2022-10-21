using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentRecordManagementApp.Models
{
    public class Student
    {
        public int StudentId{ get; set; }
        public string? FullName { get; set; }
        public string? EmailAddress { get; set; }
        public string? City { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? Response { get; set; }
        public string? CallType { get; set; }
    }
}
