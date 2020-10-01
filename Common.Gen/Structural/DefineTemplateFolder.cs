using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Gen.Helpers;

namespace Common.Gen
{
    public class DefineTemplateFolder
    {
        private string _templatePathBase;

        public DefineTemplateFolder()
        {
            _templatePathBase = GetDefaultTemplateFolder();
        }

        public string GetDefaultTemplateFolder()
        {
            return "Templates";
        }

        public void SetTemplatePathBase(string templatePathBase)
        {
            _templatePathBase = HelperUri.CombineAbsoluteUri(AppDomain.CurrentDomain.BaseDirectory, templatePathBase);
        }

        public string Define()
        {

            if (!new DirectoryInfo(_templatePathBase).Exists)
                throw new InvalidOperationException($"Templates folder not Found, remember mark copy allways {_templatePathBase}");

            return _templatePathBase;
        }

        public string Define(TableInfo tableInfo)
        {

            if (tableInfo.UsePathStrategyOnDefine)
            {
                if (tableInfo.ModelBase)
                    return "Templates\\Base";

                if (tableInfo.InheritQuery)
                    return "Templates\\inherit";

                if (tableInfo.ModelBaseWithoutGets)
                    return "Templates\\withoutgets";

                if (!tableInfo.Scaffold)
                    return "Templates\\withoutScaffold";
            }

            return Define();

        }

    }
}
