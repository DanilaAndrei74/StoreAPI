namespace StoreAPI.Contracts.Data.OutputModels
{
    public class ProductInStoreOutput
    {
        public Guid ProductId { get; set; }
        public Guid StoreId { get; set; }
        public int Quantity { get; set; }
        public bool IsDeleted { get; set; }
    }
}
