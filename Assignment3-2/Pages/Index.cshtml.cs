using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment3_2.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Assignment3_2.Pages
{
    public class IndexModel : PageModel
    {
        [ViewData]
        public List<Student> students { get; set; }

        [ViewData]
        public List<Student> studentList { get; set; }

        [ViewData]
        public List<Course> courses { get; set; }

        [ViewData]
        public List<Grade> grades { get; set; }

        [ViewData]
        public String selectedCourse { get; set; } = "";

        [ViewData]
        public String errorMsg { get; set; } = "";

        private readonly ILogger<IndexModel> _logger;

        private readonly dat154Context _context;

        public IndexModel(ILogger<IndexModel> logger, dat154Context context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task OnGetAsync()
        {
            
            students = await _context.Student.ToListAsync();
            courses = await _context.Course.ToListAsync();
            grades = await _context.Grade.ToListAsync();
            studentList = students;
        }

        public async Task<ActionResult> OnPostSearch(String input)
        {
            students = await _context.Student.ToListAsync();
            courses = await _context.Course.ToListAsync();
            grades = await _context.Grade.ToListAsync();
            studentList = students;
            if (!String.IsNullOrEmpty(input))
            {
                
                students = students.Where(x => x.Studentname.ToUpper().Contains(input.ToUpper())).ToList();

            }

            return Page();
        }

        public async Task<ActionResult> OnPostSelectCourse(String course)
        {
            selectedCourse = course;
            Console.WriteLine(course);
            students = await _context.Student.ToListAsync();
            courses = await _context.Course.ToListAsync();
            grades = await _context.Grade.ToListAsync();
            if (!String.IsNullOrEmpty(course))
            {
                students.ForEach(x => x.Grade.ToList().ForEach(grade => Console.WriteLine(grade.Coursecode)));
                students = students.Where(x => x.Grade.Any(x => x.Coursecode == course)).ToList();

            }

            studentList = await _context.Student.ToListAsync();

            return Page();
        }

        public async Task<ActionResult> OnPostSelectGrade(String grade)
        {
            students = await _context.Student.ToListAsync();
            courses = await _context.Course.ToListAsync();
            grades = await _context.Grade.ToListAsync();
            studentList = await _context.Student.ToListAsync();
            if (!String.IsNullOrEmpty(grade))
            {
                grades = grades.Where(x => x.Grade1.ToUpper().CompareTo(grade.ToUpper()) <= 0).ToList();

            }

            return Page();
        }

        public async Task<ActionResult> OnPostFailed()
        {
            students = await _context.Student.ToListAsync();
            courses = await _context.Course.ToListAsync();
            grades = await _context.Grade.ToListAsync();
            studentList = await _context.Student.ToListAsync();

            grades = grades.Where(x => x.Grade1.CompareTo("F") == 0).ToList();

            Console.WriteLine("her");

            return Page();
        }


        public async Task<ActionResult> OnPostAddStudentToCourse(String grade, String course, String studentId)
        {
            
            students = await _context.Student.ToListAsync();
            courses = await _context.Course.ToListAsync();
            grades = await _context.Grade.ToListAsync();
            studentList = await _context.Student.ToListAsync();

          

            if(grades.Where(x => x.Coursecode == course && x.Studentid.ToString() == studentId).Any()){
                errorMsg = "Student har allerede fag " + course;
                return Page();
            }


            if (!String.IsNullOrEmpty(grade) && !String.IsNullOrEmpty(course) && !String.IsNullOrEmpty(studentId))
            {
                Student std = students.Where(x => x.Id == Int32.Parse(studentId)).First();
                Grade newGrade = new Grade();
                newGrade.Student = std;
                newGrade.Grade1 = grade;
                newGrade.Coursecode = course;
                _context.Add<Grade>(newGrade);
                _context.SaveChanges();
            }

            students = await _context.Student.ToListAsync();
            courses = await _context.Course.ToListAsync();
            grades = await _context.Grade.ToListAsync();

            return Page();
        }


        public async Task<IActionResult> OnPostDeleteGrade(String course, string studentId)
        {
            Console.WriteLine(course + " " + studentId);

            students = await _context.Student.ToListAsync();
            courses = await _context.Course.ToListAsync();
            grades = await _context.Grade.ToListAsync();
            studentList = await _context.Student.ToListAsync();

            Grade gd = grades.Where(x => x.Coursecode == course && x.Studentid.ToString() == studentId).First();

            

            if (gd != null)
            {
                _context.Remove<Grade>(gd);
                _context.SaveChanges();

                students = await _context.Student.ToListAsync();
                courses = await _context.Course.ToListAsync();
                grades = await _context.Grade.ToListAsync();
                studentList = await _context.Student.ToListAsync();
            }

            return Page();
        }

    }
}
