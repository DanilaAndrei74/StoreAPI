namespace StoreAPI.Database.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] Password { get; set; }
        public string Salt { get; set; }
        public bool IsDeleted { get; set; }

    }
}
