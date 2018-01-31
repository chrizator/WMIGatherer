namespace WMIGatherer.Objects
{
    public class SecurityProduct
    {
        public string Name { get; }
        public string PathToExe { get; }



        public SecurityProduct(string name, string path)
        {
            this.Name = name;
            this.PathToExe = path;
        }
    }
}
