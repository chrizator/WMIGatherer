/* FUNCTIONS INSIDE THIS UTILITY CLASS HAVE NOT BEEN CODED
 * BY ME. I FOUND THEM SOMEWHERE ON THE INTERNET
 */

using System.Text;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;

namespace WMIGatherer.Utils
{
    internal static class Crypto
    {
        internal static string GetMd5Hash(string source)
        {
            MD5 csp = new MD5CryptoServiceProvider();
            byte[] raw = Encoding.ASCII.GetBytes(source);
            return GetHexString(csp.ComputeHash(raw));
        }


        private static string GetHexString(IList<byte> bt)
        {
            string s = string.Empty;
            for (int i = 0; i < bt.Count; i++)
            {
                byte b = bt[i];
                int n = b;
                int n1 = n & 15;
                int n2 = (n >> 4) & 15;
                if (n2 > 9)
                    s += ((char)(n2 - 10 + 'A')).ToString(CultureInfo.InvariantCulture);
                else
                    s += n2.ToString(CultureInfo.InvariantCulture);
                if (n1 > 9)
                    s += ((char)(n1 - 10 + 'A')).ToString(CultureInfo.InvariantCulture);
                else
                    s += n1.ToString(CultureInfo.InvariantCulture);
                if ((i + 1) != bt.Count && (i + 1) % 2 == 0) s += "-";
            }

            return s;
        }
    }
}
