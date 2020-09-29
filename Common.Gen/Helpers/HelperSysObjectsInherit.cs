using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Gen
{
    public class HelperSysObjectsInherit : HelperSysObjectsTableModel
    {

        public HelperSysObjectsInherit(IEnumerable<Context> contexts) : base(contexts)
        {
            this.Contexts = contexts;
            foreach (var item in contexts)
                item.UsePathProjects = true;

            this._defineTemplateFolder = new DefineTemplateFolder();
        }



    }
}
