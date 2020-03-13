using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seed.Domain.Entitys;

namespace Seed.Data.Map
{
    public abstract class SampleMapBase 
    {
        protected abstract void CustomConfig(EntityTypeBuilder<Sample> type);

        public SampleMapBase(EntityTypeBuilder<Sample> type)
        {
            
            type.ToTable("Sample");
            type.Property(t => t.SampleId).HasColumnName("Id");
           

            type.Property(t => t.Name).HasColumnName("Name").HasColumnType("varchar(50)");
            type.Property(t => t.Descricao).HasColumnName("Descricao").HasColumnType("varchar(300)");
            type.Property(t => t.SampleTypeId).HasColumnName("SampleTypeId");
            type.Property(t => t.Ativo).HasColumnName("Ativo");
            type.Property(t => t.Age).HasColumnName("Age");
            type.Property(t => t.Category).HasColumnName("Category");
            type.Property(t => t.Datetime).HasColumnName("Datetime");
            type.Property(t => t.Tags).HasColumnName("Tags").HasColumnType("varchar(1000)");
            type.Property(t => t.UserCreateId).HasColumnName("UserCreateId");
            type.Property(t => t.UserCreateDate).HasColumnName("UserCreateDate");
            type.Property(t => t.UserAlterId).HasColumnName("UserAlterId");
            type.Property(t => t.UserAlterDate).HasColumnName("UserAlterDate");


            type.HasKey(d => new { d.SampleId, }); 

			CustomConfig(type);
        }
		
    }
}