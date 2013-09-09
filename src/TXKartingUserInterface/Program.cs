// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="txcamp.org">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Org.TXCamp.TXKarting.TXKartingUserInterface
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Windows.Forms;

    using FTD2XX_NET;

    using Org.TXCamp.TXKarting.TXKartingDecoder;
    using Org.TXCamp.TXKarting.TXKartingDecoder.Parsers;
    using Org.TXCamp.TXKarting.TXKartingModel;

    /// <summary>
    /// The main class for the TXKarting application
    /// </summary>
    public static class Program
    {
        public static DecodedMessageQueue messageQueue;

        public static DecoderComms messageDecoder;

        public static List<FTDI.FT_DEVICE_INFO_NODE> ftdiDeviceList;

        public static Thread pThreadRead;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            messageQueue = new DecodedMessageQueue();

            messageDecoder = new DecoderComms(ArduinoOutputV1.GetKartMessageLength());

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
            pThreadRead.Abort();
        }
    }
}
