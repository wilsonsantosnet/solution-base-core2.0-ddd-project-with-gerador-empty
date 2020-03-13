using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seed.Domain.Entitys;

namespace Seed.Data.Map
{
    public class SampleMap : SampleMapBase
    {
        public SampleMap(EntityTypeBuilder<Sample> type) : base(type)
        {

        }

        protected override void CustomConfig(EntityTypeBuilder<Sample> type)
        {

        }

    }
}