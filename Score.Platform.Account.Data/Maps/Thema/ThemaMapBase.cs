using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Score.Platform.Account.Domain.Entitys;

namespace Score.Platform.Account.Data.Map
{
    public abstract class ThemaMapBase 
    {
        protected abstract void CustomConfig(EntityTypeBuilder<Thema> type);

        public ThemaMapBase(EntityTypeBuilder<Thema> type)
        {
            
            type.ToTable("Thema");
            type.Property(t => t.ThemaId).HasColumnName("ThemaId");
           

            type.Property(t => t.Name).HasColumnName("Name").HasColumnType("varchar(250)");
            type.Property(t => t.Description).HasColumnName("Description").HasColumnType("varchar(250)");


            type.HasKey(d => new { d.ThemaId, }); 

			CustomConfig(type);
        }
		
    }
}