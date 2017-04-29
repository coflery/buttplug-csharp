﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ButtplugMessages
{
    public interface IButtplugMessage
    {
        String Name { get; }
    }

    public interface IButtplugDeviceMessage : IButtplugMessage
    {
        UInt32 DeviceIndex { get; }
    }

    public class FleshlightLaunchRawMessage : IButtplugDeviceMessage
    {
        public static readonly String MessageName = "FleshlightLaunchRaw";
        public String Name { get => MessageName; }
        public UInt32 DeviceIndex { get; }
        public readonly UInt16 Speed;
        public readonly UInt16 Position;

        FleshlightLaunchRawMessage(UInt32 aDeviceIndex, UInt16 aSpeed, UInt16 aPosition)
        {
            DeviceIndex = aDeviceIndex;
            Speed = aSpeed;
            Position = aPosition;
        }
    }

    public class VibeaseRawMessage : IButtplugDeviceMessage
    {
        public static readonly String MessageName = "VibeaseRawMessage";
        public String Name { get => MessageName; }
        public UInt32 DeviceIndex { get; }
    }

    public class SingleMotorVibrateMessage : IButtplugDeviceMessage
    {
        public String Name { get; }
        public UInt32 DeviceIndex { get; }
    }

    public class VectorSpeedMessage : IButtplugDeviceMessage
    {
        public String Name { get; }
        public UInt32 DeviceIndex { get; }
    }
}