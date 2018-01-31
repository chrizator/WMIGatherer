using System.Collections.Generic;
using System.Management;

namespace WMIGatherer
{
    public static class Wmi
    {
        internal const string OPERATINGSYSTEM_CLASSNAME = "Win32_OperatingSystem";
        internal const string USERACCOUNT_CLASSNAME = "Win32_UserAccount";
        internal const string ANTIVIRUS_CLASSNAME = "AntivirusProduct";
        internal const string ANTISPYWARE_CLASSNAME = "AntiSpyWareProduct";
        internal const string FIREWALL_CLASSNAME = "FirewallProduct";
        internal const string NETWORKADAPTER_CLASSNAME = "Win32_NetworkAdapterConfiguration";
        internal const string BASEBOARD_CLASSNAME = "Win32_Baseboard";
        internal const string PHYSICALMEMORY_CLASSNAME = "Win32_PhysicalMemory";
        internal const string PROCESSOR_CLASSNAME = "Win32_Processor";
        internal const string VIDEOCONTROLLER_CLASSNAME = "Win32_VideoController";
        internal const string DISKDRIVE_CLASSNAME = "Win32_DiskDrive";
        internal const string BIOS_CLASSNAME = "Win32_BIOS";


        private static ManagementObjectCollection GetObjectCollection(ObjectQuery query, ManagementScope scope)
        {
            using (ManagementObjectSearcher objectSearcher = new ManagementObjectSearcher(scope, query))
            {
                ManagementObjectCollection objectCollection = objectSearcher.Get();
                return objectCollection;
            }
        }

        private static ManagementObjectCollection GetObjectCollection(string query, ManagementScope scope)
            => GetObjectCollection(new ObjectQuery(query), scope);

        private static ManagementObjectCollection GetObjectCollection(string wmiclass, string[] properties, string condition, ManagementScope scope)
        {
            string selectPart = (properties == null)
                ? "*"
                : string.Join(",", properties);

            string query = string.IsNullOrEmpty(condition)
                ? $"SELECT {selectPart} FROM {wmiclass}"
                : $"SELECT {selectPart} FROM {wmiclass} WHERE {condition}";

            return GetObjectCollection(query, scope);
        }

        private static ManagementObjectCollection GetObjectCollection(string wmiclass, string property, string condition, ManagementScope scope)
            => GetObjectCollection(wmiclass, new string[] { property }, condition, scope);


        private static WmiClassCollection ParseObjectCollection(ManagementObjectCollection objectCollection)
        {
            List<WmiClass> classList = new List<WmiClass>();

            try
            {
                foreach (ManagementObject obj in objectCollection)
                {
                    List<WmiProperty> propertyList = new List<WmiProperty>();
                    foreach (PropertyData property in obj.Properties)
                    {
                            propertyList.Add(new WmiProperty(property.Name, property.Value));
                    }

                    classList.Add(new WmiClass(propertyList));
                }
            }
            catch (ManagementException)
            {
                return null;
            }

            objectCollection?.Dispose();
            return new WmiClassCollection(classList);
        }


        /// <summary>
        /// A standard WMI query.
        /// </summary>
        /// <param name="wmiclass">The class(es) to be queried.</param>
        /// <param name="properties">Properties inside the classes.</param>
        /// <param name="condition">Condition the properties need to match.</param>
        /// <param name="scope">WMI scope.</param>
        /// <returns>WMI class collection containing obtained classes and their properties.</returns>
        public static WmiClassCollection Query(string wmiclass, string[] properties, string condition, ManagementScope scope = null)
        {
            ManagementObjectCollection objectCollection = GetObjectCollection(wmiclass, properties, condition, scope);
            return ParseObjectCollection(objectCollection);
        }

        public static WmiClassCollection Query(string wmiclass, string[] properties, ManagementScope scope = null)
            => Query(wmiclass, properties, null, scope);

        public static WmiClassCollection Query(string wmiclass, ManagementScope scope = null)
            => Query(wmiclass, null, scope);


        /// <summary>
        /// Get WMI data by entering a custom query (WMQ syntax)
        /// </summary>
        /// <param name="query">WMI query</param>
        /// <param name="scope">WMI scope</param>
        /// <returns>WMI class collection containing obtained classes and their properties.</returns>
        public static WmiClassCollection CustomQuery(ObjectQuery query, ManagementScope scope = null)
        {
            ManagementObjectCollection objectCollection = GetObjectCollection(query, scope);
            return ParseObjectCollection(objectCollection);
        }

        public static WmiClassCollection CustomQuery(string query, ManagementScope scope = null)
            => CustomQuery(new ObjectQuery(query), scope);


        /// <summary>
        /// Get a single property from a class.
        /// </summary>
        /// <param name="wmiclass">WMI class.</param>
        /// <param name="property">Property inside the class.</param>
        /// <param name="condition">Condition the property needs to match.</param>
        /// <param name="scope">WMI scope.</param>
        /// <returns>WMI property containing property name and value.</returns>
        public static WmiProperty PropertyQuery(string wmiclass, string property, string condition, ManagementScope scope = null)
        {
            ManagementObjectCollection objectCollection = GetObjectCollection(wmiclass, property, condition, scope);
            WmiClassCollection classCollection = ParseObjectCollection(objectCollection);

            foreach (WmiClass classObject in classCollection)
            {
                foreach (WmiProperty classProperty in classObject.Properties)
                {
                    return classProperty;
                }
            }

            return null;
        }

        public static WmiProperty PropertyQuery(string wmiclass, string property, ManagementScope scope = null)
            => PropertyQuery(wmiclass, property, null, scope);

        /// <summary>
        /// Generic PropertyQuery.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="wmiclass">WMI class.</param>
        /// <param name="property">Property inside the class.</param>
        /// <param name="condition">Condition the property needs to match.</param>
        /// <param name="scope">WMI scope.</param>
        /// <returns>The property value casted to the generic type.</returns>
        public static T PropertyQuery<T>(string wmiclass, string property, string condition, ManagementScope scope = null)
        {
            WmiProperty prop = PropertyQuery(wmiclass, property, condition, scope);
            return prop.Value == null
                ? default(T)
                : (T)prop.Value;
        }

        public static T PropertyQuery<T>(string wmiclass, string property, ManagementScope scope = null)
            => PropertyQuery<T>(wmiclass, property, null, scope);
    }
}
