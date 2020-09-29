using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Gen
{
    public class UniqueListString : List<string>
    {

        public new void Add(string item)
        {
            var exists = this.Where(_ => _ == item).IsAny();
            if (!exists)
                base.Add(item);

        }

        public new void AddRange(IEnumerable<string> items)
        {
            foreach (var item in items)
                this.Add(item);

        }




    }
    public class UniqueListInfo : List<Info>
    {

        public new void Add(Info item)
        {
            var exists = this.Where(_ => _.Table == item.Table).Where(_ => _.PropertyName == item.PropertyName).Any();
            if (!exists)
                base.Add(item);

        }

        public new void AddRange(IEnumerable<Info> items)
        {
            foreach (var item in items)
                this.Add(item);

        }




    }
}
