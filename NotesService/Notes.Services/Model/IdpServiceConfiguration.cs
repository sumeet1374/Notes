namespace Notes.Services.Model
{
    /// <summary>
    ///  Data required to establish connection with IDP
    /// </summary>
    public class IdpServiceConfiguration
    {
        public string UserServiceClientId { get; set; }
        public string UserServiceClientSecret { get; set; }
        public string UserServiceBaseUrl { get; set; }
        public string UserServiceAudience { get; set; }
        public string DefaultRole { get; set; }
    }
}
