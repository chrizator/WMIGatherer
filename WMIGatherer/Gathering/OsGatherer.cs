using System;

namespace WMIGatherer.Gathering
{
    public static class OsGatherer
    {
        public static string GetCaption() 
            => Wmi.PropertyQuery<string>(Wmi.OPERATINGSYSTEM_CLASSNAME, "Caption");


        public static string GetOSArchitecture()
            => Wmi.PropertyQuery<string>(Wmi.OPERATINGSYSTEM_CLASSNAME, "OSArchitecture");


        public static string GetSerialNumber()
            => Wmi.PropertyQuery<string>(Wmi.OPERATINGSYSTEM_CLASSNAME, "SerialNumber");


        public static DateTime? GetInstallDate()
            => Wmi.PropertyQuery<DateTime?>(Wmi.OPERATINGSYSTEM_CLASSNAME, "InstallDate");


        public static DateTime? GetLastBootUpTime()
            => Wmi.PropertyQuery<DateTime?>(Wmi.OPERATINGSYSTEM_CLASSNAME, "LastBootUpTime");


        public static DateTime? GetLocalDateTime()
            => Wmi.PropertyQuery<DateTime?>(Wmi.OPERATINGSYSTEM_CLASSNAME, "LocalDateTime");


        public static string GetBootDevice()
            => Wmi.PropertyQuery<string>(Wmi.OPERATINGSYSTEM_CLASSNAME, "BootDevice");


        public static string GetSystemDevice()
            => Wmi.PropertyQuery<string>(Wmi.OPERATINGSYSTEM_CLASSNAME, "SystemDevice");


        public static string GetSystemDrive()
            => Wmi.PropertyQuery<string>(Wmi.OPERATINGSYSTEM_CLASSNAME, "SystemDrive");


        public static string GetSystemDirectory()
            => Wmi.PropertyQuery<string>(Wmi.OPERATINGSYSTEM_CLASSNAME, "SystemDirectory");


        public static string GetWindowsDirectory()
            => Wmi.PropertyQuery<string>(Wmi.OPERATINGSYSTEM_CLASSNAME, "WindowsDirectory");


        public static uint? GetNumberOfUsers()
            => Wmi.PropertyQuery<uint?>(Wmi.OPERATINGSYSTEM_CLASSNAME, "NumberOfUsers");


        public static uint? GetNumberOfProcesses()
            => Wmi.PropertyQuery<uint?>(Wmi.OPERATINGSYSTEM_CLASSNAME, "NumberOfProcesses");
    }
}
