
using GenericRepository.Model;
using GenericRepository.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GenericRepository.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IRepository<Product> _productRepository;

        public ProductController(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]

        public async Task<IActionResult> Get()
        {
            var products = await _productRepository.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("id")]

        public async Task<IActionResult> GetById(int Id)
        {
            var product = await _productRepository.GetByIdAsync(Id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Request.ProductRequest product)
        {

            var newProduct = new Product()
            {
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                Price = product.Price
            };

            var createdProductResponse = await _productRepository.AddAsync(newProduct);
            return CreatedAtAction(nameof(GetById), new { id = createdProductResponse.ProductId }, createdProductResponse);
        }
        [HttpPut]

        public async Task<IActionResult> Put(int Id, [FromBody] Request.ProductRequest product)
        {
            var products = await _productRepository.GetByIdAsync(Id);
            if (products == null)
            {
                return NotFound();
            }
            products.ProductName = product.ProductName;
            products.Price = product.Price;
            await _productRepository.UpdateAsync(products);
            return Ok(products);

        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int Id)
        {
            var products = await _productRepository.GetByIdAsync(Id);
            if (products == null)
            {
                return NotFound();
            }
            await _productRepository.DeleteAsync(products);
            return NoContent();
        }
    }
}