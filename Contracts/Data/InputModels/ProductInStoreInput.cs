namespace StoreAPI.Contracts.Data.InputModels
{
    public class ProductInStoreInput
    {
        public Guid ProductId { get; set; }
        public Guid StoreId { get; set; }
        public int Quantity { get; set; }
    }
}
