# WMIGatherer - A WMI wrapper and system information gatherer
WMIGatherer provides simple and easy WMI querying and already contains functionality to gather useful system informations.
## Usage
### Query Creation
If only **one** property of **one** WMI class is required:
```cs
string windowsCaption = Wmi.PropertyQuery<string>("Win32_OperatingSystem", "Caption");
```
- When querying a property as a primitive type (```int```, ```long```, ```DateTime```) **always** use nullables (```int?```, ```long?```, ```DateTime?```) in order to check if the property is ```null```
```cs
ulong? capacity = Wmi.PropertyQuery<ulong?>("Win32_PhysicalMemory", "Capacity");
```
If **multiple** properties or **multiple** WMI classes are required:
```cs
string[] properties = new string[] { "Capacity", "PositionInRow" };
string condition = "PositionInRow = 1";
WmiClassCollection classCollection = Wmi.Query("Win32_PhysicalMemory", properties, condition);
```
If a **custom** query string (WQL syntax) needs to be used:
```cs
WmiClassCollection classCollection = Wmi.CustomQuery("FROM Win32_Bios SELECT Name, Caption");
```
#### What to do with ```WmiClassCollection```
- ```WmiClassCollection``` is an ```ICollection``` of all queried classes (```WmiClass```)
- Each ```WmiClass``` carries its properties (```WmiProperty```)
- Each ```WmiProperty``` contains ```Name``` and ```Value```
- A ```WmiProperty``` inside a ```WmiClass``` can be accessed by either:
	- iterating through ```WmiClass.Properties```
	- using the ```Name``` property of ```WmiProperty``` as index (```WmiClass["Name Of Property"]```)

```cs
foreach (WmiClass wmiClass in classCollection)
{
	uint? posInRow = wmiClass["PositionInRow"];
    ulong? capacity = wmiClass["Capacity"];
    // Do something with it...
}
```
- If the property type is primitive, it's always a nullable since a WMI property can be null
### Built-In System Information Gathering
Besides the easy to use querying system, WMIGatherer offers an information gathering functionality using preset WMI queries.
- Hardware (CPU, GPU, HDD, RAM, Mainboard, **HWID Creation**, ...)
- Operating System (Serial Number, Caption, Architecture, ...)
- Security (Antivirus, Antispyware, Firewall)
- User (Name, FullName)
- Network (Network Adapter, MAC-Addresses, ...)
#### Examples
**Get antivirus, antispyware and firewall product:**
- ```SecurityProductType.AntiVirus``` gets antivirus software
- ```SecurityProductType.AntiSpyware``` gets antispyware software
- ```SecurityProductType.Firewall``` gets firewall applications
```cs
using WMIGatherer.Gathering;
using WMIGatherer.Enums;

// var = ICollection<SecurityProduct>
var antiviruses = SecurityGatherer.GetSecurityProducts(SecurityProductType.AntiVirus);

foreach (var av in antiviruses)
{
	string name = av.Name;
    string path = av.PathToExe;
    // Do something with it...
}
```
**Create custom HWID:**
- ```HwidStrength.Light``` uses CPU ID
- ```HwidStrength.Medium``` uses CPU ID + HDD Signature
- ```HwidStrength.Medium``` uses CPU ID + HDD Signature + BIOS Serial Number + MAC Address
```cs
using WMIGatherer.Gathering;
using WMIGatherer.Enums;

string hwid = HardwareGatherer.GetHwid(HwidStrength.Medium);
// Do something with it...
```
**Get MAC-Address of IP enabled network interfaces:**
```cs
using WMIGatherer.Gathering;

string macAddress = NetGatherer.GetActiveMacAddress();
```
**Retrieve all RAM sticks:**
```cs
using WMIGatherer.Gathering;

// var = ICollection<RamStick>
var ramSticks = HardwareGatherer.GetRamSticks();
foreach (var stick in ramSticks)
{
	ulong? capacity = stick.Capacity;
    uint? clockSpeed = stick.ClockSpeed;
    // ...
    // Do something with it...
}
```
## Requirements
- .NET Framwork **3.5** or higher
