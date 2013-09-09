// -----------------------------------------------------------------------
// <copyright file="ArduinoOutputV1.cs" company="txcamp.org">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Org.TXCamp.TXKarting.TXKartingDecoder.Parsers
{
    using System;

    using Org.TXCamp.TXKarting.TXKartingModel;

    /// <summary>
    /// Class to parse messages produced by the V1 Arduino code
    /// </summary>
    public class ArduinoOutputV1 : IMessageParser
    {
        public KartMessage ParseKartMessage(object message)
        {
            // turn the message object into a string and split it based on the delimiter ':'
            var messageString = Convert.ToString(message);
            var messageParts = messageString.Split(':', '\r');

            if (messageParts[0].Length != 2)
            {
                return null;
            }

            if (messageParts[1].Length != 10)
            {
                return null;
            }

            if (messageParts[2].Length != 2)
            {
                return null;
            }

            // create all the parts of the kart message
            TXID kartID;
            try
            {
                kartID = new TXID(short.Parse(messageParts[0]));
            }
            catch (FormatException)
            {
                return null;
            }

            uint lapTime;
            try
            {
                lapTime = uint.Parse(messageParts[1]);
            }
            catch (FormatException)
            {
                return null;
            }

            // these three fields are not part of the message produced by the v1 Arduino output
            var batteryStatus = BatteryStatus.OK;
            var driverID = new TXID(0);
            var loopID = new TXID(0);

            var returnMessage = new KartMessage(lapTime, batteryStatus, kartID, driverID, loopID);

            return returnMessage;
        }

        public HeartbeatMessage ParseHeartbeatMessage(object message)
        {
            throw new System.NotImplementedException();
        }

        static public uint GetKartMessageLength()
        {
            return 18;
        }

        static public uint GetHeartBeatMessageLength()
        {
            return 18;
        }
    }
}
