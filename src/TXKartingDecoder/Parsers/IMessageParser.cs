// -----------------------------------------------------------------------
// <copyright file="IMessageParser.cs" company="txcamp.org">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Org.TXCamp.TXKarting.TXKartingDecoder.Parsers
{
    using Org.TXCamp.TXKarting.TXKartingModel;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IMessageParser
    {
        KartMessage ParseKartMessage(object message);

        HeartbeatMessage ParseHeartbeatMessage(object message);
    }
}
