using Domain.Models;

namespace Services.Repository
{
    public interface IProductRepository
    {
        int CreateProduct(Product product);

        IEnumerable<Product> GetProducts();
    }
}
