namespace WMIGatherer.Objects
{
    public class HardDrive
    {
        public string Caption { get; }
        public string Model { get; }
        public uint? Signature { get; }
        public ulong? Capacity { get; }



        public HardDrive(string caption, string model, uint? signature, ulong? capacity)
        {
            this.Caption = caption;
            this.Model = model;
            this.Signature = signature;
            this.Capacity = capacity;
        }
    }
}
