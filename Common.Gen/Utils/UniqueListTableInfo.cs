using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Gen
{
    public class UniqueListTableInfo : List<TableInfo>
    {

        public new void Add(TableInfo item)
        {
            var exists = this.Where(_ => _.TableName == item.TableName).Any();
            if (exists)
                throw new InvalidOperationException(string.Format("configuração para {0} ja existe no Contexto, favor eliminar a duplicidade :-) ", item.TableName));

            base.Add(item);

        }


    }
}
