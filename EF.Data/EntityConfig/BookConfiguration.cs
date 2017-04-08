using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF.Core.Data;

namespace EF.Data.EntityConfig
{
    public class BookConfiguration : EntityTypeConfiguration<Book>
    {
        public BookConfiguration()
        {
            HasKey(b => b.ID);
            Property(b => b.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(b => b.Title).IsRequired();
            Property(b => b.Author).IsRequired();
            Property(b => b.ISBN).IsRequired();
            Property(b => b.DatePublished).IsRequired();
            ToTable("Books");
        }
    }
}
