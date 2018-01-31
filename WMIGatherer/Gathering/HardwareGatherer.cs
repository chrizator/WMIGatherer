using System.Collections.Generic;

using WMIGatherer.Objects;
using WMIGatherer.Enums;
using WMIGatherer.Utils;

namespace WMIGatherer.Gathering
{
    public static class HardwareGatherer
    {
        #region RAM / Memory
        public static ICollection<RamStick> GetRamSticks()
        {
            List<RamStick> ramSticks = new List<RamStick>();
            string[] requiredProperties = new string[]
            {
                "Capacity",
                "ConfiguredClockSpeed",
                "Manufacturer",
                "SerialNumber",
                "PositionInRow"
            };

            WmiClassCollection classCollection = Wmi.Query(Wmi.PHYSICALMEMORY_CLASSNAME, requiredProperties);
            if (classCollection == null)
                return ramSticks;

            foreach (WmiClass wmiClass in classCollection)
            {
                RamStick ramStick = new RamStick(
                    (ulong?)wmiClass["Capacity"].Value,
                    (uint?)wmiClass["ConfiguredClockSpeed"].Value,
                    (string)wmiClass["Manufacturer"].Value,
                    (string)wmiClass["SerialNumber"].Value,
                    (uint?)wmiClass["PositionInRow"].Value);

                ramSticks.Add(ramStick);
            }

            return ramSticks;
        }


        public static ulong? GetTotalMemoryCapacity()
        {
            ulong? totalCapacity = default(ulong);
            foreach (RamStick ramStick in GetRamSticks())
            {
                totalCapacity += ramStick.Capacity;
            }

            return totalCapacity == default(ulong)
                ? null
                : totalCapacity;
        }
        #endregion RAM / Memory


        #region CPU
        public static ICollection<Processor> GetProcessors()
        {
            List<Processor> processorList = new List<Processor>();
            string[] requiredProperties = new string[]
            {
                "CurrentClockSpeed",
                "CurrentVoltage",
                "Name",
                "Manufacturer",
                "NumberOfCores",
                "ProcessorId"
            };

            WmiClassCollection classCollection = Wmi.Query(Wmi.PROCESSOR_CLASSNAME, requiredProperties);
            if (classCollection == null)
                return processorList;

            foreach (WmiClass wmiClass in classCollection)
            {
                Processor processor = new Processor(
                    (uint?)wmiClass["CurrentClockSpeed"].Value,
                    (ushort?)wmiClass["CurrentVoltage"].Value,
                    (string)wmiClass["Name"].Value,
                    (string)wmiClass["Manufacturer"].Value,
                    (uint?)wmiClass["NumberOfCores"].Value,
                    (string)wmiClass["ProcessorId"].Value);

                processorList.Add(processor);
            }

            return processorList;
        }
        #endregion CPU


        #region MotherBoard
        public static BaseBoard GetBaseBoard()
        {
            string condition = "PoweredOn = TRUE";
            string[] requiredProperties = new string[]
            {
                "Model",
                "Product",
                "Name",
                "Manufacturer",
                "SerialNumber",
                "PoweredOn"
            };

            WmiClassCollection classCollection = Wmi.Query(Wmi.BASEBOARD_CLASSNAME, requiredProperties, condition);
            if (classCollection == null || classCollection.Count == 0)
                return null;

            WmiClass wmiClass = classCollection[0];
            BaseBoard baseBoard = new BaseBoard(
                (string)wmiClass["Model"].Value,
                (string)wmiClass["Product"].Value,
                (string)wmiClass["Name"].Value,
                (string)wmiClass["Manufacturer"].Value,
                (string)wmiClass["SerialNumber"].Value);

            return baseBoard;
        }
        #endregion MotherBoard


        #region GPU
        public static ICollection<GraphicsCard> GetGraphicsCards()
        {
            List<GraphicsCard> gpuList = new List<GraphicsCard>();
            string[] requiredProperties = new string[]
            {
                "AdapterRAM",
                "Caption",
                "Description",
                "Name"
            };

            WmiClassCollection classCollection = Wmi.Query(Wmi.VIDEOCONTROLLER_CLASSNAME, requiredProperties);
            if (classCollection == null)
                return gpuList;

            foreach (WmiClass wmiClass in classCollection)
            {
                GraphicsCard gpu = new GraphicsCard(
                    (uint?)wmiClass["AdapterRAM"].Value,
                    (string)wmiClass["Caption"].Value,
                    (string)wmiClass["Description"].Value,
                    (string)wmiClass["Name"].Value);

                gpuList.Add(gpu);
            }

            return gpuList;
        }


        public static uint? GetTotalGpuMemoryCapacity()
        {
            uint? totalCapacity = default(uint);
            foreach (GraphicsCard gpu in GetGraphicsCards())
            {
                totalCapacity += gpu.MemoryCapacity;
            }

            return totalCapacity == default(uint)
                ? null
                : totalCapacity;
        }
        #endregion GPU


        #region HDD
        public static ICollection<HardDrive> GetHardDrives()
        {
            List<HardDrive> hddList = new List<HardDrive>();
            string[] requiredProperties = new string[]
            {
                "Caption",
                "Model",
                "Signature",
                "Size"
            };

            WmiClassCollection classCollection = Wmi.Query(Wmi.DISKDRIVE_CLASSNAME, requiredProperties);
            if (classCollection == null)
                return hddList;

            foreach (WmiClass wmiClass in classCollection)
            {
                HardDrive hdd = new HardDrive(
                    (string)wmiClass["Caption"].Value,
                    (string)wmiClass["Model"].Value,
                    (uint?)wmiClass["Signature"].Value,
                    (ulong?)wmiClass["Size"].Value);

                hddList.Add(hdd);
            }

            return hddList;
        }
        #endregion HDD


        #region HWID
        public static string GetHwid(HwidStrength hwidStrength, bool ignoreNullValues = false)
        {
            string cpuId = default(string);
            uint? hddSignature = default(uint?);
            string biosSerial = default(string);
            string macAddress = default(string);

            foreach (Processor processor in GetProcessors())
            {
                if (processor.Id == null)
                    continue;

                cpuId = processor.Id;
            }

            foreach (HardDrive hdd in GetHardDrives())
            {
                if (hdd.Signature == null)
                    continue;

                hddSignature = hdd.Signature;
            }

            biosSerial = BiosGatherer.GetSerialNumber();
            macAddress = NetGatherer.GetActiveMacAddress();

            switch (hwidStrength)
            {
                case HwidStrength.Light:
                    return (!ignoreNullValues) && (cpuId == null)
                        ? null
                        : Crypto.GetMd5Hash(cpuId);
                case HwidStrength.Medium:
                    return (!ignoreNullValues) && (cpuId == null || hddSignature == null)
                        ? null
                        : Crypto.GetMd5Hash(cpuId + hddSignature);
                case HwidStrength.Strong:
                    return (!ignoreNullValues) && (cpuId == null || hddSignature == null || biosSerial == null || macAddress == null)
                        ? null
                        : Crypto.GetMd5Hash(cpuId + hddSignature + biosSerial + macAddress);
            }

            return null;
        }
        #endregion HWID
    }
}
