using Daniel_Project.Data;
using Daniel_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Daniel_Project.Controllers
{
    [Authorize]
    [Route("api/[controller]/{action}")]
    [ApiController]
    public class AdministratorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AdministratorsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.Administrators);

        }
        [HttpGet("{id}")]
        public IActionResult GetById(int? id) 
        {
            var administrators =_context.Administrators.FirstOrDefault(e => e.BookingID == id);
            if(administrators == null) 
                return Problem(detail:"BookingID " +  id + "is not found.", statusCode: 404);
            return Ok(administrators);  
        }
        [HttpGet("{bookingstatus}")]
        public IActionResult GetByStatus(string? bookingstatus = "All") 
        {
            switch (bookingstatus.ToLower())
            {
                case "all":
                    return Ok(_context.Administrators);
                case "Booked":
                    return Ok(_context.Administrators.Where(e => e.BookingStatus.ToLower() == "Booked"));
                case "Checked Out":
                    return Ok(_context.Administrators.Where(e => e.BookingStatus.ToLower() == "Checked Out"));
                default:
                    return Problem(detail: "Booking with status " + bookingstatus + " is not found.", statusCode: 404);
            }
        }
        [HttpPost]
        public IActionResult Post(Administrator  administrator)
        {
            _context.Administrators.Add(administrator);
            _context.SaveChanges(); 

            return CreatedAtAction("Post", new { id = administrator.BookingID }, administrator);
        }
        [HttpPut]
        public IActionResult Put(int? id, Administrator administrator)
        {
            var entity = _context.Administrators.FirstOrDefault(e => e.BookingID == id);
            if (entity == null)
                return Problem(detail: "Booking with id " + id + " is not found", statusCode: 404);

            entity.FacilityDescription = administrator.FacilityDescription;
            entity.FacilityDateFrom = administrator.FacilityDateFrom;
            entity.FacilityDateTo = administrator.FacilityDateTo;
            entity.BookedBy = administrator.BookedBy;
            entity.BookingStatus = administrator.BookingStatus;

            _context.SaveChanges();

            return Ok(entity);
        }
        [HttpDelete]
        public IActionResult Delete(int? id, Administrator administrator) 
        {
            var entity = _context.Administrators.FirstOrDefault(e =>e.BookingID == id);
            if (entity == null)
                return Problem(detail: "Booking with id " + id + " is not found.", statusCode: 404);

            _context.Administrators.Remove(entity);
            _context.SaveChanges();

            return Ok(entity);

        }
    }
}
