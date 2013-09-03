// -----------------------------------------------------------------------
// <copyright file="DecoderComms.cs" company="txcamp.org">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Org.TXCamp.TXKarting.TXKartingDecoder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    using FTD2XX_NET;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class DecoderComms
    {
        /// <summary>     
        /// The number of ftdi devices discovered
        /// </summary>
        private readonly uint ftdiDeviceCount;

        /// <summary>
        /// The current ftdi device you're connected to
        /// </summary>
        private readonly FTDI myFtdiDevice;

        /// <summary>
        /// The return status from the current ftdi device
        /// </summary>
        private FTDI.FT_STATUS ftStatus = FTDI.FT_STATUS.FT_OK;

        public int DeviceCount
        {
            get { return Convert.ToInt32(this.ftdiDeviceCount); }
        }

        /// <summary>
        /// Initializes a new instance of the DecoderComms class.
        /// </summary>
        public DecoderComms()
        {
            // Create new instance of the FTDI device class
            this.myFtdiDevice = new FTDI();

            // Determine the number of FTDI devices connected to the machine
            this.ftStatus = this.myFtdiDevice.GetNumberOfDevices(ref this.ftdiDeviceCount);

            // Check status
            if (this.ftStatus == FTDI.FT_STATUS.FT_OK)
            {
                Console.WriteLine("Number of FTDI devices: " + this.ftdiDeviceCount);
                Console.WriteLine(string.Empty);
            }
            else
            {
                Console.WriteLine("Failed to get number of devices (error " + this.ftStatus + ")");
                return;
            }

            // If no devices available, return
            if (this.ftdiDeviceCount == 0)
            {
                Console.WriteLine("Failed to get number of devices (error " + this.ftStatus + ")");
            }
        }

        public List<FTDI.FT_DEVICE_INFO_NODE> GetDeviceList()
        {
            // Allocate storage for device info list
            FTDI.FT_DEVICE_INFO_NODE[] ftdiDeviceList = new FTDI.FT_DEVICE_INFO_NODE[this.ftdiDeviceCount];

            // Populate our device list
            this.ftStatus = this.myFtdiDevice.GetDeviceList(ftdiDeviceList);

            if (this.ftStatus == FTDI.FT_STATUS.FT_OK)
            {
                for (uint i = 0; i < this.ftdiDeviceCount; i++)
                {
                    Console.WriteLine("Device Index: " + i);
                    Console.WriteLine("Flags: " + string.Format("{0:x}", ftdiDeviceList[i].Flags));
                    Console.WriteLine("Type: " + ftdiDeviceList[i].Type);
                    Console.WriteLine("ID: " + string.Format("{0:x}", ftdiDeviceList[i].ID));
                    Console.WriteLine("Location ID: " + string.Format("{0:x}", ftdiDeviceList[i].LocId));
                    Console.WriteLine("Serial Number: " + ftdiDeviceList[i].SerialNumber);
                    Console.WriteLine("Description: " + ftdiDeviceList[i].Description);
                    Console.WriteLine(string.Empty);
                }
            }

            return ftdiDeviceList.ToList();
        }

        public void OpenDeviceBySerialNumber(string serialNumber)
        {
            // Open first device in our list by serial number
            this.ftStatus = this.myFtdiDevice.OpenBySerialNumber(serialNumber);
            if (this.ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                Console.WriteLine("Failed to open device (error " + this.ftStatus + ")");
            }
        }

        public void SetupDevice(
            uint baudRate = 9600, 
            byte dataBits = FTDI.FT_DATA_BITS.FT_BITS_8, 
            byte stopBits = FTDI.FT_STOP_BITS.FT_STOP_BITS_1, 
            byte parity = FTDI.FT_PARITY.FT_PARITY_NONE,
            ushort flowControl = FTDI.FT_FLOW_CONTROL.FT_FLOW_RTS_CTS,
            byte xon = 0x11,
            byte xoff = 0x13,
            uint readTimeout = 5000,
            uint writeTimeout = 0)
        {
            // Set up device data parameters
            // Set Baud rate to 9600
            this.ftStatus = this.myFtdiDevice.SetBaudRate(baudRate);
            if (this.ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                // Wait for a key press
                Console.WriteLine("Failed to set Baud rate (error " + this.ftStatus + ")");
                return;
            }

            // Set data characteristics - Data bits, Stop bits, Parity
            this.ftStatus = this.myFtdiDevice.SetDataCharacteristics(dataBits, stopBits, parity);
            if (this.ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                // Wait for a key press
                Console.WriteLine("Failed to set data characteristics (error " + this.ftStatus + ")");
                return;
            }

            // Set flow control - set RTS/CTS flow control
            this.ftStatus = this.myFtdiDevice.SetFlowControl(flowControl, xon, xoff);
            if (this.ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                // Wait for a key press
                Console.WriteLine("Failed to set flow control (error " + this.ftStatus + ")");
                return;
            }

            // Set read timeout to 5 seconds, write timeout to infinite
            this.ftStatus = this.myFtdiDevice.SetTimeouts(readTimeout, writeTimeout);
            if (this.ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                // Wait for a key press
                Console.WriteLine("Failed to set timeouts (error " + this.ftStatus + ")");
            }
        }

        public void WriteData(string data)
        {
            // Perform loop back - make sure loop back connector is fitted to the device
            // Write string data to the device
            uint numBytesWritten = 0;

            // Note that the Write method is overloaded, so can write string or byte array data
            this.ftStatus = this.myFtdiDevice.Write(data, data.Length, ref numBytesWritten);
            if (this.ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                // Wait for a key press
                Console.WriteLine("Failed to write to device (error " + this.ftStatus + ")");
            }
        }

        public string ReadData()
        {
            // Check the amount of data available to read
            // In this case we know how much data we are expecting, 
            // so wait until we have all of the bytes we have sent.
            uint numBytesAvailable = 0;
            do
            {
                this.ftStatus = this.myFtdiDevice.GetRxBytesAvailable(ref numBytesAvailable);
                if (this.ftStatus != FTDI.FT_STATUS.FT_OK)
                {
                    // Wait for a key press
                    Console.WriteLine("Failed to get number of bytes available to read (error " + this.ftStatus + ")");
                    return string.Empty;
                }

                Thread.Sleep(1);
            }
            while (numBytesAvailable < 18); // 16bytes of data plus linefeed

            // Now that we have the amount of data we want available, read it
            string readData;
            uint numBytesRead = 0;

            // Note that the Read method is overloaded, so can read string or byte array data
            this.ftStatus = this.myFtdiDevice.Read(out readData, numBytesAvailable, ref numBytesRead);
            if (this.ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                // Wait for a key press
                Console.WriteLine("Failed to read data (error " + this.ftStatus + ")");
                return string.Empty;
            }

            Console.Write(readData);
            return readData;
        }

        public void Close()
        {
            // Close our device
            this.ftStatus = this.myFtdiDevice.Close();
        }
    }
}
