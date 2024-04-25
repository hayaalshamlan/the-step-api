using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public List<BankBranch> GetAll()
        { return _context.BankBranches.ToList(); 
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

    }
}
