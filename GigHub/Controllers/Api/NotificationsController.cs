using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class NotificationsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public NotificationsController()
        {
            _context = new ApplicationDbContext();
        }

        public IEnumerable<NotificationDto> GetNewNotifications()
        {
            var userId = User.Identity.GetUserId();

            var notifications = _context.UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead)
                .Select(un => un.Notification)
                .Include(n => n.Gig.Artist)
                .ToList();
            
            return notifications.Select(Mapper.Map<Notification, NotificationDto>);
        }

        public IHttpActionResult MarkAsRead()
        {
            var userId = User.Identity.GetUserId();


            var noti = _context.UserNotifications
                .Where((n => n.UserId == userId && !n.IsRead))
                .ToList();

            noti.ForEach(n=>n.Read());

            _context.SaveChanges();

            return Ok();
        }
    }
}
