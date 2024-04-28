﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        BankContext _context;
        public EmployeeController(BankContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Employee> GetAll()
        {
            return _context.Employees.Select(e => new Employee
            {
                Name = e.Name,
                CivilId = e.CivilId,
                Position = e.Position,
                Id = e.Id
            }).ToList();
        }
           
            [HttpPost]
            public IActionResult Add(int id, AddEmployeeRequest employee)

            {
            var bank = _context.BankBranches.Find(id);
                _context.Employees.Add(new Employee()
                {
                    Name = employee.Name,
                    CivilId = employee.CivilId,
                    Position = employee.Position,
                    BankBranch = bank

                });
                _context.SaveChanges();
                return Created();
            }
        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            var bank = _context.Employees.Find(id);
            if (bank == null)
            {
                return NotFound();
            }
            return Ok(new EmployeeResponse
            { 
                Name = bank.Name,
                CivilId = bank.CivilId,
                Position = bank.Position,
                Id = bank.Id
            });
        }

        [HttpPatch("{id}")]
        public IActionResult Edit(int id, AddEmployeeRequest add)
        {
            var employee = _context.Employees.Find(id);
          
            employee.Name = add.Name;
            employee.CivilId = add.CivilId;
            employee.Position = add.Position;
         
           // _context.Employees.Add( bank);
            _context.SaveChanges();
            return Created(nameof(Details), new { Id = employee.Id });
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var bank = _context.Employees.Find(id);
            if (bank == null)
            {
                return BadRequest();
            }
            _context.Employees.Remove(bank);
            _context.SaveChanges();
            return Ok();
        }

    }
    }

