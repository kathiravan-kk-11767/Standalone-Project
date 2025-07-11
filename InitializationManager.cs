using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.AtomPub;
using Zoho.Common.BackgroundTransfer;
using Zoho.Common.BackgroundTransfer.Filters;
using Zoho.Common.L10N;
using Zoho.Common.Util;
using Zoho.FileSystem.Adapter.Contracts;
using Zoho.FileSystem.Adapter.Contracts.Models;
using Zoho.FileSystem.Adapter.DI;
using Zoho.FileSystem.Adapter.UWP.DI;
using Zoho.Logging;
using Zoho.Logging.Logger;
using Zoho.Logging.Util;
using Zoho.SSO.Adapter;
using Zoho.SSO.Adapter.Constants;
using Zoho.SSO.Adapter.UWP.Util;
using Zoho.UWP.Common;
using Zoho.UWP.Common.BackgroundTransfer1;
using Zoho.UWP.Common.BackgroundTransfer1.Filters;
using Zoho.UWP.Common.Util;
using Zoho.UWP.Components.Theme;
using Zoho.UWP.DI;


namespace Zoho.UWP
{
    public sealed class InitializationManager// : IAppInitializationManager
    {
        private readonly Lazy<ILogger> LazyLogger = new Lazy<ILogger>(() => LogManager.GetLogger());

        private ILogger Logger { get => LazyLogger.Value; }

        /// <summary>Singleton instance of this class</summary>
        public static InitializationManager Instance { get { return InitializationManagerSingleton.Instance; } }

        private InitializationManager() { }

        public async Task InitializeAppAsync()
        {
            NetworkConnectivity.Initialize(NetworkStatusTrigger2.Instance);
            UWPAppInfo.Initialize("Trident");
            UWPAppInfo.Instance.SetAppUserAgent();
            InitializeDI();
            InitializeLogger();


           await ZThemeManager.Instance.InitializeAppTheme("ms-appx:///Assets/accentColor.json", string.Empty);
            ZThemeManager.Instance.InitializeViewTheme();
            ZThemeManager.Instance.ChangeThemeMode(ThemeMode.Dark);

            await InitializeSSOKitAsync().ConfigureAwait(false);
        }

        private async Task InitializeSSOKitAsync()
        {
            ZSSOProvider.Instance.Initialize(SSOAdapterUtil.GetSSOAdapterDIServices());

            var ssoAdapter = ZSSOProvider.Instance.GetService<IAuthenticationAdapter>();
            if (ssoAdapter.IsSSOKitInitialised()) { return; }

            var clientId = "1002.XKJBYWWAZFZGCSPBWA1M9FN3W62YYZ";
            var redirectUri = "ms-app://s-1-15-2-1385349783-2852487995-1342666973-2751748487-2965140785-540118441-2292754330/";
            await ssoAdapter.InitializeSSOKit(clientId, redirectUri, null, false, true, SSOBuildType.Live_SSO, isChinaSetupEnabled: true).ConfigureAwait(false);
            Logger.Debug(LogManager.GetCallerInfo(), "SSOKit initialized");
        }

        private void InitializeDI()
        {


            FileSystemProvider.Initialize(FileSystemProviderUtil.GetDefaultServices(new ServiceCollection().AddSingleton<IFileSystemAppInfo>(ZAppInfoProvider.ZAppInfo)));

            ZCommonServiceManager.Instance.InitializeDI(new ServiceCollection()
                .AddSingleton<IBackgroundTransferFilterFactory, BackgroundTransferFilterFactory>()
                .AddSingleton<IBackgroundTransferManager, BackgroundTransferManager>());
            //ComponentsDIServiceProvider.Initialize(new ServiceCollection()
            //    .AddSingleton<IZL10NServiceParams>(new ZL10NServiceParams("ZComponents.Controls.UWP/Resources", string.Empty)));
            //.AddSingleton<IZComponentsL10NService>(serviceProvider => serviceProvider.GetService<IZL10NService>() as IZComponentsL10NService));
            AppDIServiceProvider.Initialize(new ServiceCollection());
        }

        private void InitializeLogger()
        {
            UWPCommonUtil.TryGetValueFromLocalSettings(LoggingConstants.EnabledAppLogLevelKey, out int logLevel);
            UWPCommonUtil.TryGetValueFromLocalSettings(LoggingConstants.IsPerformanceLoggingEnabledKey, out bool isPerformanceLoggingEnabled);
            UWPCommonUtil.TryGetValueFromLocalSettings(LoggingConstants.IsSQLLoggingEnabledKey, out bool isSQLLoggingEnabled);

            IZAppFolderProvider appFolderProvider = FileSystemProvider.Instance.GetService<IZAppFolderProvider>();
            //LogLevel.GetLogLevelByInt(logLevel)
            LogManager.Instance.Init(LoggingTarget.File, LogLevel.All, UWPCommonUtil.GetLogsFolderPath(), UWPCommonUtil.GetLogFileBaseName(), includeCallerDetails: true);
            LogManager.Instance.IsSQLLoggingEnabled = isSQLLoggingEnabled;
            LogManager.Instance.IsPerformanceLoggingEnabled = isPerformanceLoggingEnabled;
            Logger.Debug(LogManager.GetCallerInfo(), "Logger initialized");
        }

        #region InitializationManager Singleton class
        private class InitializationManagerSingleton
        {
            //Marked as internal as it will be accessed from the enclosing class. It doesn't raise any problem, as the class itself is private.
            internal static readonly InitializationManager Instance = new InitializationManager();

            // Explicit static constructor
            static InitializationManagerSingleton() { }
        }
        #endregion
    }

}
