namespace Workshop.ModuleA.Model
{
    public class Product
    {
        public Product(int productId)
        {
            this.ProductId = productId;
        }

        public int ProductId { get; private set; }
        public string Name { get; set; }
        public int Price { get; set; }
    }
}
