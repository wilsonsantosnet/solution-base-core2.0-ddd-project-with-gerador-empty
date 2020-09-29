using Common.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Gen
{
    class HelperStream
    {

        private string _path;
        private int _attemps;
        public HelperStream(string path)
        {
            this._path = path;
            this._attemps = 0;
        }

        public StreamWriter GetInstance()
        {
            try
            {
                this._attemps++;
                return new StreamWriter(this._path, false, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                this._attemps++;
                if (this._attemps <= 99)
                {
                    PrinstScn.WriteWarningLine(string.Format(">>>>> Retry {0} GetInstance {1} Erro: [{2}]", this._path, _attemps, ex.Message));
                    Thread.Sleep(2000 * _attemps);

                    return GetInstance();
                }
                throw ex;
            }
            
        }

    }
}
