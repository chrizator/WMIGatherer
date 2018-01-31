namespace WMIGatherer.Objects
{
    public class NetworkAdapter
    {
        public string Caption { get; }
        public string Description { get; }
        public bool? IsIpEnabled { get; }
        public string MacAddress { get; }



        public NetworkAdapter(string caption, string description, bool? enabled, string mac)
        {
            this.Caption = caption;
            this.Description = description;
            this.IsIpEnabled = enabled;
            this.MacAddress = mac;
        }
    }
}
