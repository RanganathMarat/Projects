using Common.Messages;

namespace IManageApp
{
    /// <summary>
    ///     IManageAppCallback
    /// </summary>
    public interface IManageAppCallback
    {
        /// <summary>
        ///     Closes all.
        /// </summary>
        void CloseAll();

        /// <summary>
        /// Pings the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <returns></returns>
        bool Ping(Oporational.AppType app);

        /// <summary>
        /// Applications the closed.
        /// </summary>
        /// <param name="app">The application.</param>
        void AppClosed(Oporational.AppType app);

        /// <summary>
        /// Applications the started.
        /// </summary>
        /// <param name="app">The application.</param>
        void AppStarted(Oporational.AppType app);

        /// <summary>
        /// Starts the application.
        /// </summary>
        /// <param name="app">The application.</param>
        void StartApplication(Oporational.AppType app);
    }
}