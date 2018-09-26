using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Score.Platform.Account.Domain.Entitys;

namespace Score.Platform.Account.Data.Map
{
    public class ProgramMap : ProgramMapBase
    {
        public ProgramMap(EntityTypeBuilder<Program> type) : base(type)
        {

        }

        protected override void CustomConfig(EntityTypeBuilder<Program> type)
        {

        }

    }
}