using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class CategoryMap
    {
        public CategoryMap(EntityTypeBuilder<Category> entityTypeBuilder) 
        {
            entityTypeBuilder.HasMany(x => x.LstProduct).WithOne(x => x.Category).HasForeignKey(x => x.CategoryId);
        }
    }
}
