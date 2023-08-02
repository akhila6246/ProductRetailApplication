using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductRetailApp.Core.Models;

namespace ProductRetailApp.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDetailsInfo>> FetchProductsList();
        Task<ProductDetailsInfo> AddProductInList(ProductDetailsInfo product);
        Task<ProductDetailsInfo> UpdateProductInList(ProductDetailsInfo product);
        Task<bool> DeleteProductInList(int productId);
        Task<IEnumerable<ProductDetailsInfo>> FetchProductsInApprovalQueue();
    }
}
