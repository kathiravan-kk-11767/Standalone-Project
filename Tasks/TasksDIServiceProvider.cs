using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoho.Common.Data.Handler.Net;
using Zoho.Common.DI;
using Zoho.Network.Adapter;
using Zoho.Network.Adapter.Contract;

namespace Zoho.UWP.Tasks
{
    public class TasksDIServiceProvider : DotNetDIServiceProviderBase
    {
        public static TasksDIServiceProvider Instance { get { return TasksDIServiceProviderSingleton.Instance; } }

        private TasksDIServiceProvider()
        {

        }

        public void Initialize(IServiceCollection tasksServices)
        {
            tasksServices.AddSingleton<ITasksNetHandler, TasksNetHandler>();
            tasksServices.AddSingleton(typeof(INetworkAdapter), new ZNetworkAdapter(ZNetworkHandlerFactory.Instance));
            BuildServiceProvider(tasksServices, false);

        }

        #region TasksDIServiceProviderSingleton Class
        private class TasksDIServiceProviderSingleton
        {
            // Explicit static constructor 
            static TasksDIServiceProviderSingleton() { }

            //Marked as internal as it will be accessed from the enclosing class. It doesn't raise any problem, as the class itself is private.
            internal static readonly TasksDIServiceProvider Instance = new TasksDIServiceProvider();
        }
        #endregion
    }

}
