using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;


namespace CourseManagementSystem
{
    public class MyHub : Hub
    {
        public static List<CookieConnections> CookieConnectionses { get; } = new List<CookieConnections>();

        public static List<string> GetConnections(string cookieValue)
        {
            try
            {
                return
                    CookieConnectionses.First((cookieConnections => cookieConnections.CookieValue == cookieValue))
                        .Connections;
            }
            catch
            {
                return new List<string>();
            }
        }
    
        public void Echo()
        {
            //		Key	"__RequestVerificationToken"	string
            Clients.Caller.alert(Context.User.Identity.Name);
        }
        public override Task OnConnected()
        {
            try
            {
                CookieConnectionses.First((cookieConnections) => cookieConnections.CookieValue == Context.RequestCookies["__RequestVerificationToken"].Value)
                    .Connections.Add(Context.ConnectionId);
            }
            catch
            {
                CookieConnectionses.Add(new CookieConnections()
                {
                    CookieValue = Context.RequestCookies["__RequestVerificationToken"].Value,
                    Connections = new List<string>()
                    {
                        Context.ConnectionId
                    }
                });
            }
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            try
            {
                CookieConnectionses.First(
                        (cookieConnections) =>
                            cookieConnections.CookieValue ==
                            Context.RequestCookies["__RequestVerificationToken"].Value)
                    .Connections.Remove(Context.ConnectionId);
            }
            catch
            {
                
            }
            
            return base.OnDisconnected(stopCalled);
        }
    }
}