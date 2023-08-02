using ProductRetailApp.Core.Models;

namespace ProductRetailApp.Core.Interfaces
{
    public interface IProductRepository : IGenericRepository<ProductDetailsInfo>
    {
        Task Add(ProductDetailsInfo product);

        Task<ProductDetailsInfo> GetById(int id);

        void Update(ProductDetailsInfo product);
        Task<IEnumerable<ProductDetailsInfo>> SearchProducts(string searchTerm, decimal? minPrice, decimal? maxPrice, DateTime? minPostedDate, DateTime? maxPostedDate);
    }
}
