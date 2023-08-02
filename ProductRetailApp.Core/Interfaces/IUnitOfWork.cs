

namespace ProductRetailApp.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }

        void Save();
    }
}
