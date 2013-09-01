// -----------------------------------------------------------------------
// <copyright file="HeartbeatMessage.cs" company="txcamp.org">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Org.TXCamp.TXKarting.TXKartingUserInterface
{
    /// <summary>
    /// A parsed 'heartbeat' message received by the decoder
    /// </summary>
    public class HeartbeatMessage
    {
        /// <summary>
        /// The elapsed race-time in milliseconds. This will be the time since the decoder was started.
        /// </summary>
        private readonly uint elapsedTimeMillis;

        /// <summary>
        /// Initializes a new instance of the HeartbeatMessage class.
        /// </summary>
        /// <param name="elapsedTimeMillis">The elapsed time in milliseconds
        /// since the decoder was started</param>
        public HeartbeatMessage(uint elapsedTimeMillis)
        {
            this.elapsedTimeMillis = elapsedTimeMillis;
        }

        /// <summary>
        /// Gets the elapsed time from the message
        /// </summary>
        /// <returns>The elapsed time in milliseconds</returns>
        public uint GetElapsedTime()
        {
            return elapsedTimeMillis;
        }
    }
}
