using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seed.Domain.Entitys;

namespace Seed.Data.Map
{
    public abstract class SampleTypeMapBase 
    {
        protected abstract void CustomConfig(EntityTypeBuilder<SampleType> type);

        public SampleTypeMapBase(EntityTypeBuilder<SampleType> type)
        {
            
            type.ToTable("SampleType");
            type.Property(t => t.SampleTypeId).HasColumnName("Id");
           

            type.Property(t => t.Name).HasColumnName("Name").HasColumnType("varchar(250)");


            type.HasKey(d => new { d.SampleTypeId, }); 

			CustomConfig(type);
        }
		
    }
}