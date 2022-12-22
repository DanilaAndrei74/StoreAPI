namespace StoreAPI.Database.Entities
{
    public class Store
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }

        public IEnumerable<ProductInStore> ProductsInStores { get; set; }
    }
}