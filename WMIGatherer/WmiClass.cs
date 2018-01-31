using System;
using System.Collections.Generic;

namespace WMIGatherer
{
    public class WmiClass
    {
        public WmiProperty[] Properties { get; }



        public WmiProperty this[string propertyName]
        {
            get
            {
                foreach (WmiProperty property in Properties)
                {
                    if (property.Name == propertyName)
                        return property;
                }

                string ex_msg = $"The specified property '{propertyName}' does not exists.";
                throw new IndexOutOfRangeException(ex_msg);
            }
        }



        public WmiClass(ICollection<WmiProperty> properties)
        {
            this.Properties = new WmiProperty[properties.Count];
            properties.CopyTo(this.Properties, 0);
        }
    }
}
