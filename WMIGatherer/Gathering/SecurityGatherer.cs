using System;
using System.Management;
using System.Collections.Generic;

using WMIGatherer.Objects;
using WMIGatherer.Enums;

namespace WMIGatherer.Gathering
{
    public static class SecurityGatherer
    {
        private static string SecurityScope;



        static SecurityGatherer()
        {
            bool isNt = Environment.OSVersion.Platform == PlatformID.Win32NT;
            bool isVistaOrHigher = Environment.OSVersion.Version.Major >= 6;

            SecurityScope = isNt && isVistaOrHigher
                ? @"ROOT\SecurityCenter2"
                : @"ROOT\SecurityCenter";
        }



        public static ICollection<SecurityProduct> GetSecurityProducts(SecurityProductType productType)
        {
            List<SecurityProduct> productList = new List<SecurityProduct>();
            ManagementScope scope = new ManagementScope(SecurityScope);
            string[] requiredProperties = new string[]
            {
                "displayName",
                "pathToSignedProductExe"
            };

            WmiClassCollection classCollection;
            switch (productType)
            {
                case SecurityProductType.AntiVirus:
                    classCollection = Wmi.Query(Wmi.ANTIVIRUS_CLASSNAME, requiredProperties, scope);
                    break;
                case SecurityProductType.AntiSpyware:
                    classCollection = Wmi.Query(Wmi.ANTISPYWARE_CLASSNAME, requiredProperties, scope);
                    break;
                case SecurityProductType.Firewall:
                    classCollection = Wmi.Query(Wmi.FIREWALL_CLASSNAME, requiredProperties, scope);
                    break;
                default:
                    classCollection = null;
                    break;
            }
            
            if (classCollection == null)
                return productList;

            foreach (WmiClass wmiClass in classCollection)
            {
                SecurityProduct product = new SecurityProduct(
                    (string)wmiClass["displayName"].Value,
                    (string)wmiClass["pathToSignedProductExe"].Value);

                productList.Add(product);
            }

            return productList;
        }
    }
}
