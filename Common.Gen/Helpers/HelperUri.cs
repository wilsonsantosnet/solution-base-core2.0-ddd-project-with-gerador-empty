using System;
using System.IO;
using System.Linq;

namespace Common.Gen.Helpers
{
    public static class HelperUri
    {
        public static string CombineRelativeUri(params string[] paths)
        {
            var newPathSlices = paths.SelectMany(_ => _.Split(@"\")).ToArray();
            var patch = Path.Combine(newPathSlices);
            return patch;

        }

        public static string CombineAbsoluteUri(params string[] paths)
        {
            var newPathSlices = paths.SelectMany(_ => _.Split(@"\")).ToArray();
            var patch = Path.Combine(newPathSlices);
            return new Uri(patch).LocalPath;
        }
    }
}