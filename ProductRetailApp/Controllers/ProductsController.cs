using Microsoft.AspNetCore.Mvc;
using ProductRetailApp.Core.Models;
using ProductRetailApp.Services.Interfaces;

namespace ProductRetailApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public readonly IProductService _productServiceObj;
        public ProductsController(IProductService productService)
        {
            _productServiceObj = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductListItems()
        {
            var productDetailsListInfo = await _productServiceObj.FetchProductsList();
            if(productDetailsListInfo == null)
            {
                return NotFound();
            }
            return Ok(productDetailsListInfo);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductDetailsInfo productdetails)
        {
            var createdProductCatalog = await _productServiceObj.AddProductInList(productdetails);
            if (createdProductCatalog == null)
            {
                return BadRequest("Product creation failed.");
            }

            return Ok(createdProductCatalog);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductList(int id, ProductDetailsInfo productdetails)
        {
            if (id != productdetails.Id)
            {
                return BadRequest("Invalid data of product.");
            }

            var updatedProduct = await _productServiceObj.UpdateProductInList(productdetails);
            if (updatedProduct == null)
            {
                return NotFound("Not found any product.");
            }

            return Ok(updatedProduct);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductItem(int id)
        {
            var isItemDeleted = await _productServiceObj.DeleteProductInList(id);
            if (!isItemDeleted)
            {
                return NotFound("specified Product not found in the list.");
            }

            return Ok("Product deleted successfully.");
        }

        [HttpGet("Approval-list-queue")]
        public async Task<IActionResult> GetProductsInApprovalQueue()
        {
            var approvalQueue = await _productServiceObj.FetchProductsInApprovalQueue();
            return Ok(approvalQueue);
        }
    }
}
