namespace WMIGatherer.Objects
{
    public class BaseBoard
    {
        public string Model { get; }
        public string Product { get; }
        public string Name { get; }
        public string Manufacturer { get; }
        public string SerialNumber { get; }



        public BaseBoard(string model, string product, string name, string manufacturer, string serial)
        {
            this.Model = model;
            this.Product = product;
            this.Name = name;
            this.Manufacturer = manufacturer;
            this.SerialNumber = serial;
        }
    }
}
