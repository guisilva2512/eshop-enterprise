namespace eShopEnterprise.Pagamento.ShopPag
{
    public class ShopPagService
    {
        public readonly string ApiKey;
        public readonly string EncryptionKey;

        public ShopPagService(string apiKey, string encryptionKey)
        {
            ApiKey = apiKey;
            EncryptionKey = encryptionKey;
        }
    }
}
