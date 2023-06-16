using CrudTask.Data;
using CrudTask.DTOs;
using CrudTask.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace CrudTask.Controllers
{
    [ApiController]
    public class StudentController : ControllerBase
    {
        public readonly DataContext _dataContext;

        public StudentController(DataContext dataContext) 
        {
            _dataContext = dataContext;
        }

        [HttpGet("{page}")]
        public async Task<ActionResult<List<Student>>> GetStudents(int page)
        {
            var pageResults = 3f;
            var pageCount = Math.Ceiling(_dataContext.Students.Count() / pageResults);
            var students = await _dataContext.Students.Skip((page - 1) * (int)pageResults).Take((int)pageResults).ToListAsync();

            var response = new ResponseDTO
            {
                Students = students,
                CurrentPage = page,
                Page = (int)pageCount
            };
            return Ok(response);
        }

        [HttpGet("GetItems")]
        public async Task<IEnumerable<Student>> Get()
        {
            var items = await _dataContext.Students.ToListAsync();
            return items;
        }

        [HttpGet("GetItemsById")]
        public ActionResult <Student> Get(int id)
        {
            var items = _dataContext.Students.FirstOrDefault(x => x.StudentId == id);
            if (items == null)
            {
                return NotFound();
            }
            return Ok(items);
        }

        [HttpPost("PostItems")]
        public async Task<ActionResult<Student>> Post(Student student)
        {
            if (student == null)
            {
                return BadRequest("Invalid Credential");
            }
                _dataContext.Students.Add(student);
                await _dataContext.SaveChangesAsync();
                return Ok(student);
        }

        [HttpPut("UpdateItem")]
        public async Task<IActionResult> Put(int id, Student student)
        {
            if(student == null)
            {
                return BadRequest("Invalid Credentials");
            }
            var items = _dataContext.Students.FirstOrDefault(x => x.StudentId == id);
            if(items == null)
            {
                return NotFound();
            }
            items.StudentName = student.StudentName;
            items.ContactNumber = student.ContactNumber;
            items.StudentAge = student.StudentAge;
            await _dataContext.SaveChangesAsync();
            return Ok(items);
        }

        [HttpDelete("DeleteItem")]
        public async Task<ActionResult<Student>> Delete(int id)
        {
            var item = _dataContext.Students.FirstOrDefault(x => x.StudentId == id);
            if (item == null)
            {
                return NotFound("Invalid Credential");
            }
            _dataContext.Students.Remove(item);
            await _dataContext.SaveChangesAsync();
            return Ok(item);
        }
    }
}
