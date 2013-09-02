// -----------------------------------------------------------------------
// <copyright file="KartMessage.cs" company="txcamp.org">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Org.TXCamp.TXKarting.TXKartingModel
{
    /// <summary>
    /// A parsed 'kart arrived' message received by the decoder
    /// </summary>
    public class KartMessage
    {
        /// <summary>
        /// The time in milliseconds for the previous lap.
        /// </summary>
        private readonly uint lapTimeMilliseconds;

        /// <summary>
        /// The status of the battery in the transponder.
        /// </summary>
        private readonly BatteryStatus batteryStatus;

        /// <summary>
        /// The ID of the kart.
        /// </summary>
        private readonly TXID kartID;

        /// <summary>
        /// The ID of the driver.
        /// </summary>
        private readonly TXID driverID;

        /// <summary>
        /// The ID of the detection loop.
        /// </summary>
        private readonly TXID loopID;

        /// <summary>
        /// Initializes a new instance of the KartMessage class
        /// </summary>
        /// <param name="lapTime">The previous lap time in milliseconds</param>
        /// <param name="battStatus">The current status of the transponder's power source</param>
        /// <param name="kart">The kart id of the transponder which triggered the message</param>
        /// <param name="driver">The driver id of the transponder which triggered the message</param>
        /// <param name="loop">The id of the loop which was triggered by the transponder</param>
        public KartMessage(uint lapTime, BatteryStatus battStatus, TXID kart, TXID driver, TXID loop)
        {
            this.lapTimeMilliseconds = lapTime;
            this.batteryStatus = battStatus;
            this.kartID = kart;
            this.driverID = driver;
            this.loopID = loop;
        }

        /// <summary>
        /// Gets the previous lap time
        /// </summary>
        /// <returns>Milliseconds between detections on the same loop</returns>
        public uint GetLapTime()
        {
            return this.lapTimeMilliseconds;
        }

        /// <summary>
        /// Gets the current battery status
        /// </summary>
        /// <returns>The status of the battery</returns>
        public BatteryStatus GetBatteryStatus()
        {
            return this.batteryStatus;
        }

        /// <summary>
        /// Gets the Kart ID for the message
        /// </summary>
        /// <returns>The Kart ID</returns>
        public TXID GetKartID()
        {
            return this.kartID;
        }

        /// <summary>
        /// Gets the Driver ID for the message
        /// </summary>
        /// <returns>The Driver ID</returns>
        public TXID GetDriverID()
        {
            return this.driverID;
        }

        /// <summary>
        /// Gets the Loop ID for the message
        /// </summary>
        /// <returns>The Loop ID</returns>
        public TXID GetLoopID()
        {
            return this.loopID;
        }

        /// <summary>
        /// Equality comparison
        /// </summary>
        /// <param name="obj"></param>
        /// <returns><c>true</c> if objects are equal otherwise <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            var compareTo = (KartMessage)obj;
            return this.batteryStatus.Equals(compareTo.GetBatteryStatus()) && this.driverID.Equals(compareTo.GetDriverID())
                   && this.kartID.Equals(compareTo.GetKartID()) && this.lapTimeMilliseconds.Equals(compareTo.GetLapTime())
                   && this.loopID.Equals(compareTo.GetLoopID());
        }

        public override int GetHashCode()
        {
            return this.batteryStatus.GetHashCode() ^ this.driverID.GetHashCode() ^ this.kartID.GetHashCode()
                   ^ this.lapTimeMilliseconds.GetHashCode() ^ this.loopID.GetHashCode();
        }
    }
}
