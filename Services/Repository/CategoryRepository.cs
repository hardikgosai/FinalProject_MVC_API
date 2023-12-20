using DAL.EntityFramework;
using Domain.Models;

namespace Services.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
       private readonly ApplicationDbContext applicationDbContext;

        public CategoryRepository(ApplicationDbContext _applicationDbContext)
        {
            applicationDbContext = _applicationDbContext;
        }

        public List<Category> GetCategories()
        {
            return applicationDbContext.Categories.ToList();
        }

        public int CreateCategory(Category category)
        {
            applicationDbContext.Categories.Add(category);
            return applicationDbContext.SaveChanges();
        }

        public int DeleteCategory(int id)
        {
            var products = applicationDbContext.Products.Where(x => x.CategoryId == id).ToList();
            applicationDbContext.Products.RemoveRange(products);

            var category = applicationDbContext.Categories.Find(id);
            applicationDbContext.Categories.Remove(category);
            return applicationDbContext.SaveChanges();
        }

        public Category SearchCategory(int id)
        {
            var category = applicationDbContext.Categories.Find(id);
            return category;
        }

        public int UpdateCategory(Category category)
        {
            var categoryToUpdate = applicationDbContext.Categories.Find(category.CategoryId);
            categoryToUpdate.CategoryName = category.CategoryName;
            applicationDbContext.Update(categoryToUpdate);    
            return applicationDbContext.SaveChanges();
        }

    }
}