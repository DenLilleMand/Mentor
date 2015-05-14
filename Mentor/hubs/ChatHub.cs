using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mentor.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.SignalR;

namespace Mentor.hubs
{
        public class ChatHub : Hub
        {
            public void Send(string message)
            {
               /* System.Diagnostics.Debug.WriteLine("userId:" + System.Web.HttpContext.Current.User.Identity.GetUserId());
                System.Diagnostics.Debug.WriteLine("userManager:" + UserManager);
                // Call the addNewMessageToPage method to update clients.
                var user = UserManager.FindById(Int32.Parse(HttpContext.Current.User.Identity.GetUserId()));*/
                Clients.All.addNewMessageToPage(message);
            }
        }
}