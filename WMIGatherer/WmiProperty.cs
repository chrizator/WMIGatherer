using System;

namespace WMIGatherer
{
    public class WmiProperty
    {
        public string Name { get; }
        public object Value { get; }



        public WmiProperty(string name, object value)
        {
            this.Name = name;
            this.Value = value;
        }
    }
}
