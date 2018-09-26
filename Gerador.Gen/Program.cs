using Common.Gen;
using Score.Platform.Account.Gen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Score.Platform.Account.Gen
{
    class Program
    {
        static void Main(string[] args)
        {
            HelperFlow.Flow(args, () =>
            {

                return new ConfigExternalResources().GetConfigExternarReources();

            }, new HelperSysObjects(new ConfigContext().GetConfigContext()));
        }

    }
}
