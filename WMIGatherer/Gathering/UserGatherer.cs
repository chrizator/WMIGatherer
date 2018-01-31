using System.Collections.Generic;

using WMIGatherer.Objects;

namespace WMIGatherer.Gathering
{
    public static class UserGatherer
    {
        public static ICollection<UserAccount> GetUsers()
        {
            List<UserAccount> userList = new List<UserAccount>();
            string[] requiredProperties = new string[]
            {
                "Name",
                "FullName",
                "Disabled"
            };

            WmiClassCollection classCollection = Wmi.Query(Wmi.USERACCOUNT_CLASSNAME, requiredProperties);
            if (classCollection == null)
                return userList;

            foreach (WmiClass wmiClass in classCollection)
            {
                UserAccount user = new UserAccount(
                    (string)wmiClass["Name"].Value,
                    (string)wmiClass["FullName"].Value,
                    (bool?)wmiClass["Disabled"].Value);

                userList.Add(user);
            }

            return userList;
        }
    }
}
