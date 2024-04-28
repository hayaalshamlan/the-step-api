using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        BankContext _context;
        public BankController(BankContext context)
        {
            _context = context;
        }
        [HttpGet]
        public PageListResult<BankBranchResponse> GetAll(int page = 1, string search = "")
        {
            if (search == "")
                { 
                return _context.BankBranches.Select(b => new BankBranchResponse
                {
                    Name = b.Name,
                    BranchManager = b.BranchManager,
                    LocationURL = b.LocationURL,
                        LocationName = b.LocationName,
                     
                }).ToPageList(page, 1);
            } 
            return _context.BankBranches
                .Where(r => r.LocationName.StartsWith(search))
                .Select(b => new BankBranchResponse
                {
                    Name = b.Name,
                    BranchManager = b.BranchManager,
                    LocationURL = b.LocationURL,
                    LocationName = b.LocationName,

                }).ToPageList(page, 1);
        }
        [HttpPost]
        public IActionResult Add(AddBankRequest req)
        {
            _context.BankBranches.Add(new BankBranch()
            {
              Name = req.Name,
               LocationName = req.LocationName,
               BranchManager = req.BranchManager,
               LocationURL = req.LocationURL,
             
            });
            _context.SaveChanges();
            return Created();
        }
        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            var bank = _context.BankBranches.Find(id);
            if(bank == null)
            {
                return NotFound();
            }
            return Ok(new BankBranchResponse
            {

                BranchManager = bank.BranchManager,
                LocationName = bank.LocationName,
                Name = bank.Name
            });
        }

        [HttpPatch("{id}")]
        public IActionResult Edit(int id, AddBankRequest req)
        {
            var bank = _context.BankBranches.Find(id);
            bank.Name = req.Name;
            bank.LocationName = req.LocationName;
            bank.LocationURL = req.LocationURL;
            bank.BranchManager = req.BranchManager;
            _context.SaveChanges();
            return Created(nameof(Details), new { Id = bank.Id });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var bank = _context.BankBranches.Find(id);
            if (bank == null)
            {
                return BadRequest();
            }
            _context.BankBranches.Remove(bank);
        _context.SaveChanges();
        return Ok();
        }

    }
}
