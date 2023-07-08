using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using si730ebu202023992.API.Inventory.Dto.Request;
using si730ebu202023992.API.Inventory.Dto.Response;
using si730ebu202023992.Domain.Inventory.Interface;
using si730ebu202023992.Infraestructure.Inventory.Interface;
using si730ebu202023992.Infraestructure.Inventory.Model;

namespace si730ebu202023992.API.Inventory.Controller
{
    [Route("api/v1")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IMapper _mapper;
        private IProductDomain _productDomain;
        private IProductInfraestructure _productInfraestructure;
        
        public ProductController(IMapper mapper, IProductDomain productDomain, IProductInfraestructure productInfraestructure)
        {
            _mapper = mapper;
            _productDomain = productDomain;
            _productInfraestructure = productInfraestructure;
        }
        
        [HttpGet("products", Name = "Get Product By Id")]
        public IActionResult GetProductById(int id)
        {
            var product = _productDomain.GetProductById(id);
            var result = _mapper.Map<Product, ProductResponse>(product);
            return Ok(result);
        }

        [HttpPost("products")]
        public async Task<IActionResult> PostProductAsync([FromBody] ProductRequest productRequest)
        {
            if (ModelState.IsValid)
            {
                _productDomain.CheckMonitoringLevel(productRequest.MonitoringLevel);
                var product = _mapper.Map<ProductRequest, Product>(productRequest);
                var result = await _productDomain.SaveAsync(product);
                return result ? Created("", product) : StatusCode(500);
            }
            return StatusCode(400);
        }
    }
}
