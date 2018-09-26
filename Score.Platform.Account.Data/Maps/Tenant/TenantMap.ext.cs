using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Score.Platform.Account.Domain.Entitys;

namespace Score.Platform.Account.Data.Map
{
    public class TenantMap : TenantMapBase
    {
        public TenantMap(EntityTypeBuilder<Tenant> type) : base(type)
        {

        }

        protected override void CustomConfig(EntityTypeBuilder<Tenant> type)
        {

        }

    }
}