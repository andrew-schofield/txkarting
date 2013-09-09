// -----------------------------------------------------------------------
// <copyright file="MainForm.cs" company="txcamp.org">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Org.TXCamp.TXKarting.TXKartingUserInterface
{
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;

    using Org.TXCamp.TXKarting.TXKartingDecoder.Parsers;
    using Org.TXCamp.TXKarting.TXKartingModel;

    /// <summary>
    /// The main form for the TXKarting application
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the MainForm class.
        /// </summary>
        public MainForm()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            if (Program.messageDecoder.DeviceCount > 0)
            {
                Program.ftdiDeviceList = Program.messageDecoder.GetDeviceList();
            }
            else
            {
                MessageBox.Show("No FTDI devices found");
            }
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            StringBuilder serialNumbers = new StringBuilder();
            serialNumbers.AppendLine("Device serial numbers");
            foreach (var device in Program.ftdiDeviceList)
            {
                serialNumbers.AppendLine(device.SerialNumber);
            }

            MessageBox.Show(serialNumbers.ToString(), "FTDI Devices found");

            Program.messageDecoder.OpenDeviceBySerialNumber(Program.ftdiDeviceList[0].SerialNumber);

            Program.pThreadRead = new Thread(new ThreadStart(ReadThread));
            Program.pThreadRead.Start();
        }

        private void ReadThread()
        {
            ArduinoOutputV1 parser = new ArduinoOutputV1();
            while (true)
            {
                Program.messageQueue.Add(parser.ParseKartMessage(Program.messageDecoder.ReadData()));
            }
        }

        private void button3_Click(object sender, System.EventArgs e)
        {
            KartMessage message;
            while (true)
            {
                if (Program.messageQueue.Check() != null)
                {
                    message = (KartMessage)Program.messageQueue.Pull();
                    dataGridView1.Rows.Add(
                        message.GetKartID().GetID(),
                        message.GetLapTime().ToString(),
                        message.GetBatteryStatus().ToString(),
                        message.GetDriverID().GetID(),
                        message.GetLoopID().GetID());
                    dataGridView1.Update();
                }
                Thread.Sleep(1);
        }
        }
    }
}
