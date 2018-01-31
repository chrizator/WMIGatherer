using System.Collections.Generic;

using WMIGatherer.Objects;

namespace WMIGatherer.Gathering
{
    public static class NetGatherer
    {

        public static ICollection<NetworkAdapter> GetNetworkAdapter()
        {
            List<NetworkAdapter> adapterList = new List<NetworkAdapter>();
            string[] requiredProperties = new string[]
            {
                "Caption",
                "Description",
                "IPEnabled",
                "MacAddress"
            };

            WmiClassCollection classCollection = Wmi.Query(Wmi.NETWORKADAPTER_CLASSNAME, requiredProperties);
            if (classCollection == null)
                return adapterList;

            foreach (WmiClass wmiClass in classCollection)
            {
                NetworkAdapter adapter = new NetworkAdapter(
                    (string)wmiClass["Caption"].Value,
                    (string)wmiClass["Description"].Value,
                    (bool?)wmiClass["IPEnabled"].Value,
                    (string)wmiClass["MACAddress"].Value);

                adapterList.Add(adapter);
            }

            return adapterList;
        }


        public static string GetActiveMacAddress()
        {
            string property = "MACAddress";
            string condition = "IPEnabled = TRUE";

            return Wmi.PropertyQuery<string>(Wmi.NETWORKADAPTER_CLASSNAME, property, condition);
        }
    }
}
