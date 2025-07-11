using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoho.UWP.Common.DI;

namespace Zoho.UWP.DI
{
    public sealed class AppDIServiceProvider : DIServiceProviderBase
    {
        /// <summary>Singleton instance of this class</summary>
        public static AppDIServiceProvider Instance { get { return AppDIServiceProviderSingleton.Instance; } }

        private AppDIServiceProvider() { }

        private static bool IsInitialized = false;

        public static void Initialize(IServiceCollection servicesCollection)
        {
            if (IsInitialized) { throw new InvalidOperationException("Already initialized"); }
            IsInitialized = true;
            Instance.BuildServiceProvider(servicesCollection);
        }

        #region AppDIServiceProvider Singleton class
        private class AppDIServiceProviderSingleton
        {
            //Marked as internal as it will be accessed from the enclosing class. It doesn't raise any problem, as the class itself is private.
            internal static readonly AppDIServiceProvider Instance = new AppDIServiceProvider();

            // Explicit static constructor
            static AppDIServiceProviderSingleton() { }
        }
        #endregion
    }
}
