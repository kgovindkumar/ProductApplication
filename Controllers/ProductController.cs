using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductApplication.Data;
using ProductApplication.Models;

namespace ProductApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private ApplicationDbContext _dbContext;

        public ProductController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet(Name = "GetProduct")]
        public IEnumerable<Product> Get()
        {
            return _dbContext.Products.ToList();
        }

        [HttpPost(Name = "addProduct")]
        public int AddProduct(ProductModel productModel)
        {
            var product = new Product
            {
                Name = productModel.Name,
                Description = productModel.Description,
                Price = productModel.Price
            };
            _dbContext.Products.AddAsync(product);
            _dbContext.SaveChangesAsync();
            return product.Id;
        }

        [HttpPut(Name = "updateProduct")]
        public Product UpdateProduct(Product product)
        {
            _dbContext.Products.Update(product);
            _dbContext.SaveChangesAsync();
            return product;
        }

        [HttpDelete(Name = "deleteProduct")]
        public bool DeleteProduct(int id)
        {
            var a =_dbContext.Products.FirstOrDefault(p => p.Id == id) is not Product product
                ? throw new KeyNotFoundException($"Product with id {id} not found.")
                 : product;
            _dbContext.Products.Remove(a);
            _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
