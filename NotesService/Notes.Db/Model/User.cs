namespace Notes.Db.Model
{
    /// <summary>
    ///  User entity
    /// </summary>
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        // Mapped User Id from IDP (Identity Provider)
        public string ExternalUserId { get; set; }
        public bool IsAdmin { get; set; }
        public bool Active { get; set; }
    }
}
