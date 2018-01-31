using System;
using System.Collections.Generic;

using WMIGatherer;
using WMIGatherer.Gathering;
using WMIGatherer.Enums;
using WMIGatherer.Objects;

namespace ExampleProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Gatherer Example:\n");
            GathererExample();

            Console.WriteLine("\n\nQuery Example:\n");
            QueryExample();

            Console.ReadLine();
        }


        private static void GathererExample()
        {
            // Security Gatherer
            ICollection<SecurityProduct> firewalls = SecurityGatherer.GetSecurityProducts(SecurityProductType.Firewall);
            var antiviruses = SecurityGatherer.GetSecurityProducts(SecurityProductType.AntiVirus);
            var antispyware = SecurityGatherer.GetSecurityProducts(SecurityProductType.AntiSpyware);

            foreach (var fw in firewalls)
                Console.WriteLine(fw.Name);

            foreach (var av in antiviruses)
                Console.WriteLine(av.Name);

            foreach (var asw in antispyware)
                Console.WriteLine(asw.Name);

            //Hardware Gatherer
            var graphicsCards = HardwareGatherer.GetGraphicsCards();
            var processors = HardwareGatherer.GetProcessors();
            var ramSticks = HardwareGatherer.GetRamSticks();
            ulong? totalRam = HardwareGatherer.GetTotalMemoryCapacity();

            foreach (var card in graphicsCards)
                Console.WriteLine($"{card.Name}: {card.MemoryCapacity}");

            foreach (var proc in processors)
                Console.WriteLine($"{proc.Name}: Cores: {proc.NumberOfCores} Voltage: {proc.Voltage}");

            foreach (var ram in ramSticks)
                Console.WriteLine($"{ram.PositionInRow}: {ram.Capacity}");

            Console.WriteLine(totalRam);


            string hwid = HardwareGatherer.GetHwid(HwidStrength.Medium);
            Console.WriteLine(hwid);

            // OS Gatherer
            string caption = OsGatherer.GetCaption();
            string bootDevice = OsGatherer.GetBootDevice();
            Console.WriteLine(caption);
            Console.WriteLine(bootDevice);

            // Other gatherers not covered...
        }


        private static void QueryExample()
        {
            // Print Caption and OSArchitecture from Win32_OperatingSystem class
            WmiClassCollection classCollection = Wmi.Query("Win32_OperatingSystem", new string[] { "Caption", "OSArchitecture" });
            foreach (WmiClass wmiClass in classCollection)
            {
                Console.WriteLine(wmiClass["Caption"].Value);
                Console.WriteLine(wmiClass["OSArchitecture"].Value);
            }

            // Print out all properties for all Win32_Processor classes
            WmiClassCollection classCollection2 = Wmi.Query("Win32_Processor");
            foreach (WmiClass wmiClass in classCollection2)
            {
                foreach (WmiProperty property in wmiClass.Properties)
                {
                    Console.WriteLine($"{property.Value}: {property.Name}");
                }
            }

            // Get a single property from Win32_OperatingSystem
            string windowsName = Wmi.PropertyQuery<string>("Win32_OperatingSystem", "Caption");
            Console.WriteLine(windowsName);
        }
    }
}
