namespace Notes.Api.Auth
{
    /// <summary>
    ///  separate Authorization policies used to authorize admin and normal user.
    ///  Please find policy options set up in Startup.cs
    /// </summary>
    public static class AuthPolicies
    {
        public const string USER = "NotesAppUser";
        public const string ADMIN = "NotesAppAdmin";
    }
}
