using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class UserProfileMap
    {
        public UserProfileMap(EntityTypeBuilder<UserProfile> entityTypeBuilder) 
        {
            entityTypeBuilder.HasKey(t  => t.Id);
            entityTypeBuilder.Property(t => t.FirstName).IsRequired();
            entityTypeBuilder.Property(t => t.LastName).IsRequired();
            entityTypeBuilder.Property(t => t.ContactNo).IsRequired();
            entityTypeBuilder.Property(t => t.Address).IsRequired();
            entityTypeBuilder.Property(t => t.Address).HasMaxLength(100);
        }
    }
}
