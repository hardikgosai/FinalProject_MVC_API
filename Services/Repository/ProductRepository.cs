using DAL.EntityFramework;
using Domain.Models;

namespace Services.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext context;

        public ProductRepository(ApplicationDbContext _context)
        {
            context = _context;
        }

        public int CreateProduct(Product product)
        {
            context.Products.Add(product);
            return context.SaveChanges();
        }

        public IEnumerable<Product> GetProducts()
        {
            return context.Products.ToList();
        }
    }
}
