using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoho.Common.L10N;
using Zoho.Common.Util;

namespace Zoho.UWP.Tasks
{
    public sealed class TasksServiceManager 
    {
        /// <summary>Singleton instance of this class</summary>
        public static TasksServiceManager Instance { get { return TasksServiceManagerSingleton.Instance; } }

        private TasksServiceManager() { }

        public async Task InitializeAsync()
        {
            IServiceCollection tasksServices = new ServiceCollection();
            TasksDIServiceProvider.Instance.Initialize(tasksServices);
        }

        public async Task InitializeUserAsync(string zuid)
        {
            await TasksServiceManager.Instance.InitializeAsync().ConfigureAwait(false);
        }

     

        #region NotesServiceManager Singleton class
        private class TasksServiceManagerSingleton
        {
            //Marked as internal as it will be accessed from the enclosing class. It doesn't raise any problem, as the class itself is private.
            internal static readonly TasksServiceManager Instance = new TasksServiceManager();

            // Explicit static constructor
            static TasksServiceManagerSingleton() { }
        }
        #endregion
    }
}
