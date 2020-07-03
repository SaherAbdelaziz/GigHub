using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Web.Http;
namespace GigHub.Controllers.Api
{
    [System.Web.Http.Authorize]
    public class AttendancesController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public AttendancesController()
        {
            _context = new ApplicationDbContext();
        }

        //api/attendance
        [System.Web.Http.HttpPost]
        public IHttpActionResult Attend(Attendance dto)
        {
            var userId = User.Identity.GetUserId();

            var exists = _context.Attendances.Any(a => a.AttendeeId == userId && a.GigId == dto.GigId);
            if (exists)
            {
                return BadRequest("The attendance already exist. ");
            }

            var attendance = new Attendance
            {
                GigId = dto.GigId,
                AttendeeId = userId
            };

            _context.Attendances.Add(attendance);
            _context.SaveChanges();

            return Ok();

        }

        [System.Web.Http.HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            string userId = User.Identity.GetUserId();

            var attendance = _context.Attendances
                .SingleOrDefault(g => g.GigId == id && g.AttendeeId == userId);

            if (attendance == null)
                return NotFound();


            _context.Attendances.Remove(attendance);
            _context.SaveChanges();
            return Ok(id);
        }
    }
}
