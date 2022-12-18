namespace StoreAPI.Database.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        
        public IEnumerable<ProductsInStores> ProductsInStores { get; set; }
    }
}
