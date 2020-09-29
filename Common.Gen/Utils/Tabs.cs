using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Gen
{
    public static class Tabs
    {

        public static string TabModels(string value)
        {
            return String.Format("{0}{1}", "        ", value);
        }
        public static string TabProp()
        {
            return "        ";
        }
        public static string TabItemMethod()
        {
            return "        ";
        }
        public static string TabJs()
        {
            return "                ";
        }
        public static string TabModels()
        {
            return Tabs.TabModels(string.Empty);
        }
        public static string TabMaps()
        {
            return "            ";
        }
        public static string TabSets()
        {
            return "            ";
        }

        public static string TabModelsPlus()
        {
            return "                ";
        }

        public static string TabFilters()
        {
            return string.Empty;
        }
        
    }
}
