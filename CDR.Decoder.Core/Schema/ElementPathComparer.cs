using System;
using System.Collections.Generic;

namespace CDR.Schema
{
    public class ElementPathComparer : IComparer<String>
    {
        #region IComparer<string> Members

        public int Compare(string x, string y)
        {
            string[] pA = x.Split('.');
            string[] pB = y.Split('.');

            int cRes = 0;

            int pMax = (pA.Length <= pB.Length) ? pA.Length : pB.Length;

            for (int p = 0; p < pMax; p++)
            {
                cRes = Int16.Parse(pA[p]) - Int16.Parse(pB[p]); ;
                if (cRes != 0)
                    break;
            }

            return (cRes == 0) ? pA.Length - pB.Length : cRes;
        }

        #endregion
    }
}
