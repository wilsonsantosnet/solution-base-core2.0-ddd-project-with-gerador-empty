using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seed.Domain.Entitys;

namespace Seed.Data.Map
{
    public class SampleTypeMap : SampleTypeMapBase
    {
        public SampleTypeMap(EntityTypeBuilder<SampleType> type) : base(type)
        {

        }

        protected override void CustomConfig(EntityTypeBuilder<SampleType> type)
        {

        }

    }
}