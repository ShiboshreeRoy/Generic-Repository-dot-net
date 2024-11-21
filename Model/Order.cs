namespace GenericRepository.Model
{
    public class Order
    {
        public int OrderId { get; set; }

        public  DateTime OrderDate { get; set; }

        //Product Foregin Key

        public int ProductId { get; set; }

        // navigation property

        public Product? Product { get; set; }
    }
}
