namespace StoreAPI.Database.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

    }
}
