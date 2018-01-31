namespace WMIGatherer.Objects
{
    public class UserAccount
    {
        public string Name { get; }
        public string FullName { get; }
        public bool? IsDisabled { get; }



        public UserAccount(string name, string fullName, bool? disabled)
        {
            this.Name = name;
            this.FullName = fullName;
            this.IsDisabled = disabled;
        }
    }
}
