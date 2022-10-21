using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StudentRecordManagementApp.Models;
using StudentRecordManagementApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace StudentRecordManagementApp.Controllers
{
    public class StudentController : Controller
    {
        private readonly IConfiguration _cofiguration;
        private readonly IStudentService _studentService;

        public StudentController(IConfiguration configuration, IStudentService studentService)
        {
            _cofiguration = configuration;
            _studentService = studentService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public string InsertStudents(Student model)
        {
            _studentService.InsertStudent(model).ToList();
            //return View(model);
            return "Saved Successfully..";
        }


        [HttpGet]
        public IActionResult StudentsList(int pg=1)
        {
            List<Student> students = _studentService.GetAllStudent().ToList();
            const int pageSize = 5;
            if (pg < 1)
            {
                pg = 1;
            }
            int totStudent = students.Count();
            var pager = new Pager(totStudent, pg, pageSize);
            int resSkip = (pg - 1) * pageSize;
            var data = students.Skip(resSkip).Take(pager.PageSize).ToList();

            this.ViewBag.Pager = pager;

            //AllModels allModels = new AllModels();
            //allModels.students = _studentService.GetAllStudent().ToList();
            //allModels.students.ToPagedList(pageNumber, pageSize);
            //return View(allModels);
            //return View(students);
            return View(data);
        }
    }
}
