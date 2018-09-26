using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Score.Platform.Account.Domain.Entitys;

namespace Score.Platform.Account.Data.Map
{
    public abstract class TenantMapBase 
    {
        protected abstract void CustomConfig(EntityTypeBuilder<Tenant> type);

        public TenantMapBase(EntityTypeBuilder<Tenant> type)
        {
            
            type.ToTable("Tenant");
            type.Property(t => t.TenantId).HasColumnName("TenantId");
           

            type.Property(t => t.Name).HasColumnName("Name").HasColumnType("varchar(250)");
            type.Property(t => t.Email).HasColumnName("Email").HasColumnType("varchar(250)");
            type.Property(t => t.Password).HasColumnName("Password").HasColumnType("varchar(250)");
            type.Property(t => t.Active).HasColumnName("Active");
            type.Property(t => t.ProgramId).HasColumnName("ProgramId");
            type.Property(t => t.GuidResetPassword).HasColumnName("GuidResetPassword");
            type.Property(t => t.DateResetPassword).HasColumnName("DateResetPassword");
            type.Property(t => t.ChangePasswordNextLogin).HasColumnName("ChangePasswordNextLogin");
            type.Property(t => t.UserCreateId).HasColumnName("UserCreateId");
            type.Property(t => t.UserCreateDate).HasColumnName("UserCreateDate");
            type.Property(t => t.UserAlterId).HasColumnName("UserAlterId");
            type.Property(t => t.UserAlterDate).HasColumnName("UserAlterDate");


            type.HasKey(d => new { d.TenantId, }); 

			CustomConfig(type);
        }
		
    }
}