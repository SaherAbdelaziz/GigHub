using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace GigHub.Models
{
    public class Notification
    {

        public int Id { get; private set; }
        public DateTime DateTime { get;private set; }
        public NotificationType Type { get; private set; }
        public DateTime? OriginalDateTime { get; private set; }
        public string OriginalVenue { get; private set; }

        [Required]
        public Gig Gig { get;private set; }
        
        protected Notification()
        {
            
        }


        private Notification(Gig gig, NotificationType type)
        {
            if (gig == null)
                throw new ArgumentNullException(nameof(gig));

            DateTime = DateTime.Now;
            Gig = gig;
            Type = type;
        }

        public static Notification GigCreated(Gig gig)
        {
            return new Notification(gig, NotificationType.GigCreated);
        }

        public static Notification GigUpdated(Gig gig , DateTime originalDateTime , string originalVenue)
        {
            return new Notification(gig, NotificationType.GigUpdated)
            {
                OriginalDateTime = originalDateTime,
                OriginalVenue = originalVenue
            };
        }

        public static Notification GigCanceled(Gig gig)
        {
            return new Notification(gig, NotificationType.GigCanceled);

        }


    }
}