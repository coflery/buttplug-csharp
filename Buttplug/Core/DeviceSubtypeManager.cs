﻿using System;
using Buttplug.Logging;

namespace Buttplug.Core
{
    public interface IDeviceSubtypeManager
    {
        event EventHandler<DeviceAddedEventArgs> DeviceAdded;  
        void StartScanning();
        void StopScanning();
    }

    public abstract class DeviceSubtypeManager : IDeviceSubtypeManager
    {
        protected readonly ButtplugLog BpLogger;
        protected readonly ButtplugLogManager LogManager;

        public event EventHandler<DeviceAddedEventArgs> DeviceAdded;

        protected DeviceSubtypeManager(ButtplugLogManager aLogManager)
        {
            LogManager = aLogManager;
            BpLogger = aLogManager.GetLogger(LogProvider.GetCurrentClassLogger());
            BpLogger.Trace($"Setting up Device Manager {GetType().Name}");
        }

        protected void InvokeDeviceAdded(DeviceAddedEventArgs args)
        {
            //Can't invoke this from child classes? Weird.
            DeviceAdded?.Invoke(this, args);
        }

        public abstract void StartScanning();
        public abstract void StopScanning();
    }
}