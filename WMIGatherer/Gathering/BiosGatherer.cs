namespace WMIGatherer.Gathering
{
    public static class BiosGatherer
    {
        public static string GetCaption()
            => Wmi.PropertyQuery<string>(Wmi.BIOS_CLASSNAME, "Caption");


        public static string GetDescription()
            => Wmi.PropertyQuery<string>(Wmi.BIOS_CLASSNAME, "Description");


        public static string GetName()
            => Wmi.PropertyQuery<string>(Wmi.BIOS_CLASSNAME, "Name");


        public static string GetManufacturer()
            => Wmi.PropertyQuery<string>(Wmi.BIOS_CLASSNAME, "Manufacturer");


        public static bool? IsPrimaryBios()
            => Wmi.PropertyQuery<bool?>(Wmi.BIOS_CLASSNAME, "PrimaryBIOS");


        public static string GetSerialNumber()
            => Wmi.PropertyQuery<string>(Wmi.BIOS_CLASSNAME, "SerialNumber");
    }
}
