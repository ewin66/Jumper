using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Jumper
{
    public class StringLogicalComparer : IComparer, IComparer<string>
    {
        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        private static extern int StrCmpLogicalW(string x, string y);

        public int Compare(object x, object y)
        {
            return StrCmpLogicalW(x.ToString(), y.ToString());
        }

        public int Compare(string x, string y)
        {
            return StrCmpLogicalW(x, y);
        }
    }

}
