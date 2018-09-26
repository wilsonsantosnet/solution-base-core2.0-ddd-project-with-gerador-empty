using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Score.Platform.Account.Domain.Entitys;

namespace Score.Platform.Account.Data.Map
{
    public abstract class ProgramMapBase 
    {
        protected abstract void CustomConfig(EntityTypeBuilder<Program> type);

        public ProgramMapBase(EntityTypeBuilder<Program> type)
        {
            
            type.ToTable("Program");
            type.Property(t => t.ProgramId).HasColumnName("ProgramId");
           

            type.Property(t => t.Description).HasColumnName("Description").HasColumnType("varchar(250)");
            type.Property(t => t.Datasource).HasColumnName("Datasource").HasColumnType("varchar(250)");
            type.Property(t => t.DatabaseName).HasColumnName("DatabaseName").HasColumnType("varchar(250)");
            type.Property(t => t.ThemaId).HasColumnName("ThemaId");
            type.Property(t => t.UserCreateId).HasColumnName("UserCreateId");
            type.Property(t => t.UserCreateDate).HasColumnName("UserCreateDate");
            type.Property(t => t.UserAlterId).HasColumnName("UserAlterId");
            type.Property(t => t.UserAlterDate).HasColumnName("UserAlterDate");


            type.HasKey(d => new { d.ProgramId, }); 

			CustomConfig(type);
        }
		
    }
}