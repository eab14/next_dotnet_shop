namespace server.Models
{
    public class NextNetDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string ProductsCollectionName { get; set; } = null!;
        public string FeaturedProductsCollectionName { get; set; } = null!;
        public string UsersCollectionName { get; set; } = null!;
        public string OrdersCollectionName { get; set; } = null!;
    }
}
