using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigHub.Models
{
    public class UserNotification
    {
        [Key]
        [Column(Order = 1)]
        public string UserId { get;private set; }

        [Key]
        [Column(Order = 2)]
        public int NotificationId { get;private set; }

        public ApplicationUser User { get; private set; }
        public Notification Notification { get;private set; }
        public bool IsRead { get; set; }


        protected UserNotification()
        {
            
        }
        public UserNotification(ApplicationUser applicationUser, Notification notification)
        {
            if(applicationUser == null)
                throw new ArgumentNullException(nameof(applicationUser));
                
            if(notification  == null)
                throw new ArgumentNullException(nameof(notification));

            User = applicationUser;
            Notification = notification;
        }
    }
}