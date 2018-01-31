namespace WMIGatherer.Objects
{
    public class Processor
    {
        public uint? ClockSpeed { get; }
        public ushort? Voltage { get; }
        public string Name { get; }
        public string Manufacturer { get; }
        public uint? NumberOfCores { get; }
        public string Id { get; }



        public Processor(uint? speed, ushort? volt, string name, string manufacturer, uint? cores, string id)
        {
            this.ClockSpeed = speed;
            this.Voltage = volt;
            this.Name = name;
            this.Manufacturer = manufacturer;
            this.NumberOfCores = cores;
            this.Id = id;
        }
    }
}
