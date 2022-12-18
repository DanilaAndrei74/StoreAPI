namespace StoreAPI.Database.Entities
{
    public class ProductsInStores
    {
        public Guid ProductId { get; set; }
        public Guid StoreId { get; set; }
        public int Quantity { get; set; }
        public bool IsDeleted { get; set; }

        public Product Product { get; set; }
        public Store Store { get; set; }
    }
}