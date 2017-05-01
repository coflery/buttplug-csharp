﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Buttplug
{
    public class DeviceAddedEventArgs : EventArgs
    {
        public IButtplugDevice Device { get; }
        public DeviceAddedEventArgs(IButtplugDevice d)
        {
            this.Device = d;
        }
    }

    public class MessageReceivedEventArgs : EventArgs
    {
        public IButtplugMessage Message { get; }
        public MessageReceivedEventArgs(IButtplugMessage aMsg)
        {
            Message = aMsg;
        }
    }

    public class ButtplugService
    {
        ButtplugJSONMessageParser Parser;
        List<DeviceManager> Managers;
        Dictionary<uint, IButtplugDevice> Devices;
        uint DeviceIndex;
        public event EventHandler<DeviceAddedEventArgs> DeviceAdded;
        // TODO Should I just make StartScanning async across device managers?
        public event EventHandler FinishedScanning;
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        public ButtplugService()
        {
            Parser = new ButtplugJSONMessageParser();
            Devices = new Dictionary<uint, IButtplugDevice>();
            DeviceIndex = 0;
            Managers = new List<DeviceManager>();
            Managers.Add(new BluetoothManager());
            Managers.Add(new XInputGamepadManager());
            Managers.ForEach(m => m.DeviceAdded += DeviceAddedHandler);
        }

        public void DeviceAddedHandler(object o, DeviceAddedEventArgs e)
        {
            Devices.Add(DeviceIndex, e.Device);
            DeviceAdded?.Invoke(this, e);
        }

        public async Task<bool> SendMessage(IButtplugMessage aMsg)
        {
            switch (aMsg)
            {
                case IButtplugDeviceMessage m:
                    if (!Devices.ContainsKey(m.DeviceIndex))
                    {
                        return false;
                    }
                    return await Devices[m.DeviceIndex].ParseMessage(m);
            }
            return false;
        }

        public bool SendMessage(String aJSONMsg)
        {
            Parser.Deserialize(aJSONMsg).IfSome(m =>
            {
                SendMessage(m);
            });
            return false;
        }

        public void StartScanning()
        {
            Managers.ForEach(m => m.StartScanning());
        }

        public void StopScanning()
        {
            Managers.ForEach(m => m.StopScanning());
        }
    }
}
