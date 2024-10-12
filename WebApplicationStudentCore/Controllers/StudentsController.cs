using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationStudentCore.Data;
using WebApplicationStudentCore.Models;
using WebApplicationStudentCore.Models.Entities;

namespace WebApplicationStudentCore.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public StudentsController(ApplicationDbContext dbContext) 
        { 
            this.dbContext=dbContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel viewModel)
        {
            //student entity to save data
            var student = new Student
            {
                Name = viewModel.Name,
                Email = viewModel.Email,
                Phone = viewModel.Phone,
                Subscribed = viewModel.Subscribed,

            };
            await dbContext.Students.AddAsync(student);
            await dbContext.SaveChangesAsync();
            return View();
        }
        //To list the Table data
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var students=await dbContext.Students.ToListAsync();
            return View(students);
        }
        //For Edit the Grid data
        [HttpGet]
        //Guid for unique identifier then show the data to textbox
        public async Task<IActionResult> Edit(Guid Id)
        {
            var student = await dbContext.Students.FindAsync(Id);
            return View(student);

        }
        [HttpPost]
        public async Task<IActionResult> Edit(Student viewModel)
        {
            var student=await dbContext.Students.FindAsync(viewModel.Id);
            if (student is not null) 
            {
                student.Name = viewModel.Name;
                student.Email = viewModel.Email;
                student.Phone = viewModel.Phone;
                student.Subscribed = viewModel.Subscribed;
                await dbContext.SaveChangesAsync();
            }
          
            
            return RedirectToAction("List","Students");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Student viewModel)
        {
            var student = await dbContext.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id==viewModel.Id);
            if (student is not null)
            {
                dbContext.Students.Remove(student);
                await dbContext.SaveChangesAsync();

            }
                return RedirectToAction("List", "Students");
        }
    }
}
