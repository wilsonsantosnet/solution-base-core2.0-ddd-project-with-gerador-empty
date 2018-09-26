using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Score.Platform.Account.Domain.Entitys;

namespace Score.Platform.Account.Data.Map
{
    public class ThemaMap : ThemaMapBase
    {
        public ThemaMap(EntityTypeBuilder<Thema> type) : base(type)
        {

        }

        protected override void CustomConfig(EntityTypeBuilder<Thema> type)
        {

        }

    }
}