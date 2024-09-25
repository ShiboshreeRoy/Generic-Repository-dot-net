namespace GenericRepository.Model
{
    public class Product
    {
        public  int  ProductId { get; set; }

        public string ProductName { get; set; }

        public string ProductDescription { get; set; }

        public decimal Price { get; set; }

        //navigation Property

        public  List<Order> Orders { get; set; }
    }
}
