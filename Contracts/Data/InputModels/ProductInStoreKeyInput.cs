namespace StoreAPI.Contracts.Data.InputModels
{
    public class ProductInStoreKeyInput
    {
        public Guid ProductId { get; set; }
        public Guid StoreId { get; set; }
    }
}
