using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductMicroserviceProject.Models;
using ProductMicroserviceProjectC.Repository;
using System.Transactions;

namespace ProductMicroserviceProjectC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _repository;
        public ProductController(IProductRepository productRepository)
        {
            _repository = productRepository;
        }

        //Get:api/product
        [HttpGet]
        public IActionResult Get()
        {
            var products = _repository.GetProducts();
            return new OkObjectResult(products);
        }

        //Get:api/product/5
        [HttpGet("{id}", Name ="Get")]
        public IActionResult Get(int id)
        {
            var product = _repository.GetProductById(id);
            return new OkObjectResult(product);
        }

        //Post:api/product
        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            using(var scope = new TransactionScope())
            {
                _repository.InsertProduct(product);
                scope.Complete();
                return CreatedAtAction(nameof(Get),new {id = product.Id},product);
            }
        }


        [HttpPut]
        public IActionResult Put([FromBody] Product product)
        {
            if(product != null)
            {
                using (var scope = new TransactionScope())
                {
                    _repository.UpdateProduct(product);
                    scope.Complete();
                    //return new OkResult();
                    return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
                }
            }
            return new NoContentResult();
        }

        //DELETE:api/ApiWithAction/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repository?.DeleteProduct(id);
            return new OkResult();
        }

    }
}
