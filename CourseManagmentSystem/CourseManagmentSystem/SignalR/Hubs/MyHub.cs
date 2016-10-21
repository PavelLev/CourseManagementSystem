using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;


namespace CourseManagmentSystem
{
    public class MyHub : Hub
    {
        public static Dictionary<string, List<string>> groups = new Dictionary<string, List<string>>();
        public override Task OnConnected()
        {
            try
            {
                groups[Context.User.Identity.Name].Add(Context.ConnectionId);
            }
            catch
            {
                groups[Context.User.Identity.Name] = new List<string>()
                {
                    Context.ConnectionId
                };
            }
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            groups[Context.User.Identity.Name].Remove(Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }
    }
}