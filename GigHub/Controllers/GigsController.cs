using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using GigHub.Migrations;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GigsController()
        {
            _context = new ApplicationDbContext();
        }


        // GET: Gigs/create
        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigViewModel
            {
                Genres = _context.Genres.ToList(),
                Heading = "Add a Gig"
            };
            return View("GigForm", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigViewModel viewModel)
        {

            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();

                return View("GigForm", viewModel);
            }

            var gig = new Gig
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = viewModel.DateTime, // string.Format("{0} {1}" , viewModel.Date , viewModel.Time)),
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue
            };

            _context.Gigs.Add(gig);
            _context.SaveChanges();

            return RedirectToAction("Mine", "Gigs");
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();

            var gig = _context.Gigs.SingleOrDefault(g => g.Id == id && g.ArtistId == userId);
            var viewModel = new GigViewModel
            {
                Id = gig.Id,
                Genres = _context.Genres.ToList(),
                DateTime = gig.DateTime,
                Genre = gig.GenreId,
                Venue = gig.Venue,
                Heading = "Edit a Gig"
            };
            return View("GigForm", viewModel);
        }


        public ActionResult Update(GigViewModel viewModel)
        {

            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();

                return View("GigForm", viewModel);
            }

            var userId = User.Identity.GetUserId();

            var gig = _context.Gigs
                .Include(g=>g.Attendances
                .Select(a=>a.Attendee))
                .Single(g => g.Id == viewModel.Id && g.ArtistId == userId);

            //update
            gig.Update(viewModel.DateTime , viewModel.Genre , viewModel.Venue);
            gig.Venue = viewModel.Venue;
            gig.GenreId = viewModel.Genre;
            gig.DateTime = viewModel.DateTime;

            _context.SaveChanges();

            return RedirectToAction("Mine", "Gigs");
        }


        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();

            var gigs = _context.Gigs
                .Where(g => g.ArtistId == userId && !g.IsCanceled)
                .Include(g => g.Genre)
                .Include(g => g.Artist)
                .ToList();

            return View(gigs);
        }

        [Authorize]
        public ActionResult Attending()
        {
            var artistId = User.Identity.GetUserId();

            var gigs = _context.Attendances
                .Where(a => a.AttendeeId == artistId)
                .Select(a => a.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();
;
            var attendances = _context.Attendances
                .Where(a => a.AttendeeId == artistId)
                .ToList()
                .ToLookup(a => a.GigId);



            var viewModel = new GigsViewModel
            {
                UpcomingGigs = gigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs i'm Attending",
                Attendances = attendances

            };
            return View("Gigs", viewModel);
        }

        public ActionResult Details(int id)
        {
            
            var gig = _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Attendances)
                .FirstOrDefault(g => g.Id == id);

            var gigviewmodel = new GigDetailsViewModel()
            {
                Gig = gig,
        };
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                gigviewmodel.IsGoing = _context.Attendances
                    .Any(a => a.GigId == id && a.AttendeeId == userId);

                gigviewmodel.IsFollowing = _context.Followings
                    .Any(a => a.FollowerId == userId && a.FolloweeId == gig.ArtistId);
            }

            return View(gigviewmodel);
        }

        public ActionResult Search(GigsViewModel viewModel)
        {
            return RedirectToAction("Index" , "Home" , new {query = viewModel.SearchTerm });
            
        }
    }
}