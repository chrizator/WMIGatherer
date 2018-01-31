namespace WMIGatherer.Objects
{
    public class RamStick
    {
        public ulong? Capacity { get; }
        public uint? ClockSpeed { get; }
        public string Manufacturer { get; }
        public string SerialNumber { get; }
        public uint? PositionInRow { get; }



        public RamStick(ulong? capacity, uint? speed, string manufacturer, string serial, uint? pos)
        {
            this.Capacity = capacity;
            this.ClockSpeed = speed;
            this.Manufacturer = manufacturer;
            this.SerialNumber = serial;
            this.PositionInRow = pos;
        }
    }
}
