namespace WMIGatherer.Objects
{
    public class GraphicsCard
    {
        public uint? MemoryCapacity { get; }
        public string Caption { get; }
        public string Description { get; }
        public string Name { get; }



        public GraphicsCard(uint? memory, string caption, string description, string name)
        {
            this.MemoryCapacity = memory;
            this.Caption = caption;
            this.Description = description;
            this.Name = name;
        }
    }
}
