namespace GraphApiAppOnlyWebApiSample.Models
{
    /// <summary>
    /// This represents the model entity for app settings.
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Gets or sets the <see cref="Logging"/> instance.
        /// </summary>
        public Logging Logging { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="GraphApp"/> instance.
        /// </summary>
        public GraphApp GraphApp { get; set; }
    }

    /// <summary>
    /// This represents the model entity for logging.
    /// </summary>
    public class Logging
    {
        /// <summary>
        /// Gets or sets a value indicating whether to include scopes or not.
        /// </summary>
        public bool IncludeScopes { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="LogLevel"/> instance.
        /// </summary>
        public Loglevel LogLevel { get; set; }
    }

    /// <summary>
    /// This represents the model entity for log level.
    /// </summary>
    public class Loglevel
    {
        /// <summary>
        /// Gets or sets the default log level.
        /// </summary>
        public string Default { get; set; }

        /// <summary>
        /// Gets or sets the system log level.
        /// </summary>
        public string System { get; set; }

        /// <summary>
        /// Gets or sets the microsoft log level.
        /// </summary>
        public string Microsoft { get; set; }
    }

    /// <summary>
    /// This represents the model entity for graph app.
    /// </summary>
    public class GraphApp
    {
        /// <summary>
        /// Gets or sets the tenant name. eg) contoso.onmicrosoft.com
        /// </summary>
        public string Tenant { get; set; }

        /// <summary>
        /// Gets or sets the login URL. eg) https://login.microsoftonline.com
        /// </summary>
        public string AuthUrl { get; set; }

        /// <summary>
        /// Gets or sets the graph URL.
        /// </summary>
        public string GraphUrl { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the client Id.
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the client secret key.
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// Gets or sets the application Id.
        /// </summary>
        public string AppId { get; set; }
    }
}