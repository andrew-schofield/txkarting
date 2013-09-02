// -----------------------------------------------------------------------
// <copyright file="ArduinoV1Tests.cs" company="txcamp.org">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace TXKartingTests.ParserTests
{
    using NUnit.Framework;

    using Org.TXCamp.TXKarting.TXKartingDecoder.Parsers;
    using Org.TXCamp.TXKarting.TXKartingModel;

    [TestFixture]
    public class ArduinoV1Tests
    {
        [Test]
        public void correctly_formatted_decoder_message_creates_message_object()
        {
            var expectedResult = new KartMessage(10000, BatteryStatus.OK, new TXID(1), new TXID(0), new TXID(0));

            var inputMessage = (object)"01:0000010000:00";

            var messageParser = new ArduinoOutputV1();

            var result = messageParser.ParseKartMessage(inputMessage);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase("1:0000000001:00")]
        [TestCase("01:000000001:00")]
        [TestCase("01:0000000001:0")]
        [TestCase("")]
        [TestCase("::")]
        [TestCase("123")]
        [TestCase("aaa")]
        [TestCase("aa:0000000000:00")]
        [TestCase("00:aaaaaaaaaa:00")]
        [TestCase("00:0000000000:aa")]
        public void incorrectly_formatted_decoder_message_returns_null(object message)
        {
            var messageParser = new ArduinoOutputV1();

            var result = messageParser.ParseKartMessage(message);

            Assert.That(result, Is.Null);
        }
    }
}
