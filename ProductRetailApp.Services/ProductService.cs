using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ProductRetailApp.Core.Interfaces;
using ProductRetailApp.Core.Models;
using ProductRetailApp.Services.Interfaces;

namespace ProductRetailApp.Services
{
    public class ProductService : IProductService
    {
        public IUnitOfWork _unitOfWork;
        const string status = "Active";
        const string productStatus = "PendingApproval";
        const int productPrice = 10000;
        const int price = 5000;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> DeleteProductInList(int productId)
        {
            var existingProduct = await _unitOfWork.Products.GetById(productId);

            if (existingProduct == null)
                return false;

            existingProduct.ProductStatus = productStatus;
            _unitOfWork.Products.Update(existingProduct);
            _unitOfWork.Save();

            return true;
        }

        public async Task<IEnumerable<ProductDetailsInfo>> FetchProductsInApprovalQueue()
        {
            var products = await _unitOfWork.Products.GetAll();
            return products.Where(p => p.ProductStatus == productStatus).OrderByDescending(p => p.PostedDate);
        }
        public async Task<IEnumerable<ProductDetailsInfo>> FetchProductsList()
        {
            var productsInfo = await _unitOfWork.Products.GetAll();
            return productsInfo.Where(p => p.ProductStatus == status)
                           .OrderByDescending(p => p.PostedDate);
        }

        public async Task<ProductDetailsInfo> AddProductInList(ProductDetailsInfo product)
        {
            // Check if the product price exceeds the limit
            if (product.ProductPrice > productPrice)
            {
                product.ProductStatus = productStatus;
            }
            else
            {
                product.ProductStatus = status;
            }

            _unitOfWork.Products.Add(product);
            _unitOfWork.Save();

            return product;
        }

        public async Task<ProductDetailsInfo> UpdateProductInList(ProductDetailsInfo product)
        {
            var existingProduct = await _unitOfWork.Products.GetById(product.Id);

            if (existingProduct == null)
                return null;

            if (product.ProductPrice > price || product.ProductPrice > existingProduct.ProductPrice * 1.5m)
            {
                product.ProductStatus = productStatus;
            }
            else
            {
                product.ProductStatus = status;
            }

            _unitOfWork.Products.Update(product);
            _unitOfWork.Save();

            return product;
        }

    }
}
