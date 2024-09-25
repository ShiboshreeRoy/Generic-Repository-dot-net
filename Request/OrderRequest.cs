using GenericRepository.Model;

namespace GenericRepository.Request
{
    public class OrderRequest
    {
        public int OrderId { get; set; }

        public DateTime OrderDate { get; set; }

        public  int ProductId { get; set; }

        
    }
}
