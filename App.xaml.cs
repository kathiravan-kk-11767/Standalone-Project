using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.Services.Maps;
using Windows.Storage;
using Windows.System.Profile;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Zoho.Common.Analytics.Data;
using Zoho.Common.BackgroundTransfer;
using Zoho.Common.CrashLogs;
using Zoho.Common.Util;
using Zoho.Contacts.ServiceContracts;
using Zoho.FileSystem.Adapter.Contracts;
using Zoho.FileSystem.Adapter.DI;
using Zoho.Logging;
using Zoho.SSO.Adapter;
using Zoho.Streams.Collaboration.ViewModels.UWP;
using Zoho.UWP;
using Zoho.UWP.Common.BackgroundTransfer1;
using Zoho.UWP.Common.Util;
using Zoho.UWP.Components.OnBoarding.View;
using Zoho.UWP.Contacts.Lib;
using Zoho.UWP.Tasks;

namespace SemSeparation
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();

            EasClientDeviceInformation easDeviceInfo = new EasClientDeviceInformation();
            string manufacturer = easDeviceInfo.SystemManufacturer;
            if (!string.IsNullOrEmpty(easDeviceInfo.SystemSku))
            {
                manufacturer += $" ({easDeviceInfo.SystemSku})";
            }

            DeviceInfo.Initialize(easDeviceInfo.SystemProductName, manufacturer, AnalyticsInfo.VersionInfo.DeviceFamily, AnalyticsInfo.VersionInfo.DeviceFamilyVersion, Package.Current.Id.Architecture.ToString());
            UWPAppInfo.Initialize("Trident");
            ZAppInfoProvider.Initialize(UWPAppInfo.Instance);
            this.UnhandledException += OnUnhandledException;
            this.Suspending += OnAppSuspending;
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
        }

        private async void OnAppSuspending(object sender, SuspendingEventArgs e)
        {
         
        }
        private async void OnUnhandledException(object sender, Windows.UI.Xaml.UnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            var exception = e.Exception;
            App.Current.UnhandledException -= OnUnhandledException;
            await LogManager.GetCrashLogger().Log(new CrashLog(exception));
            Current.Exit();
        }


        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {


            if (e.PrelaunchActivated) { return; }
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                //var isSignedIn =
                //    await Task.Run(async () =>
                //{
                await InitializationManager.Instance.InitializeAppAsync();
                var ssoAdapter = ZSSOProvider.Instance.GetService<IAuthenticationAdapter>();
                ISSOUserAdapter userAdapter = ZSSOProvider.Instance.GetService<ISSOUserAdapter>();

                async Task<bool> isInvalidSession() => (await ssoAdapter.CheckAndLogOutAsync(userAdapter.GetCurrentUserZuid())).GetValueOrDefault(true);

                var isSignedIn = ssoAdapter.IsUserLoggedIn();
                //});

                if (isSignedIn)
                {
                    await InitializeYourServiceHere(userAdapter);
                    rootFrame.Navigate(typeof(DemoPage), e.Arguments);
                    // Ensure the current window is active
                    Window.Current.Activate();
                }
                else
                {
                    var signIn = new SignIn();
                    rootFrame.Content = signIn;
                    // Ensure the current window is active
                    Window.Current.Activate();
                    await signIn.AuthenticateUserAsync();
                    await InitializeYourServiceHere(userAdapter);
                    rootFrame.Navigate(typeof(DemoPage), e.Arguments);
                }

            }
        }

        public async Task InitializeYourServiceHere(ISSOUserAdapter userAdapter)
        {

            ServiceCollection viewModelCollection = new ServiceCollection();
            viewModelCollection.AddTransient<IContactsSearchProvider, ContactsSearchProvider>();

            ServiceCollection libraryModelCollection = new ServiceCollection();
            libraryModelCollection.AddSingleton<IZContactsServiceProvider, ZContactsServiceProvider>()
                    .AddSingleton<IBackgroundTransferManager, BackgroundTransferManager>();

            await StreamsCollabServiceManager.Instance.InitializeUserAsync(userAdapter.GetCurrentUserZuid(), viewModelServiceCollection: viewModelCollection, libraryServiceCollection: libraryModelCollection);

            IZAppFolderProvider appFolderProvider = FileSystemProvider.Instance.GetRequiredService<IZAppFolderProvider>();
            string rootFolderPath = (await appFolderProvider.GetAppLocalFolderAsync().ConfigureAwait(false)).Path;

            await ContactsServiceManager.InitializeAsync(rootFolderPath, default, userAdapter.GetCurrentUserZuid());
            // NotesServiceManager.Instance.InitializeUserAsync(userAdapter.GetCurrentUserZuid());
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
