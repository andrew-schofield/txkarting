// -----------------------------------------------------------------------
// <copyright file="DecodedMessageQueue.cs" company="txcamp.org">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Org.TXCamp.TXKarting.TXKartingModel
{
    using System.Collections.Generic;

    /// <summary>
    /// A FIFO queue for decoded messages
    /// </summary>
    public class DecodedMessageQueue
    {
        /// <summary>
        /// The internal queue representation
        /// </summary>
        private readonly Queue<IDecodedMessage> internalQueue;

        /// <summary>
        /// Initializes a new instance of the DecodedMessageQueue class.
        /// </summary>
        public DecodedMessageQueue()
        {
            this.internalQueue = new Queue<IDecodedMessage>();
        }

        /// <summary>
        /// Adds a new entry to the end of the queue
        /// </summary>
        /// <param name="message">The message to add to the queue</param>
        public void Add(IDecodedMessage message)
        {
            this.internalQueue.Enqueue(message);
        }

        /// <summary>
        /// Returns the first entry in the queue without removing it
        /// </summary>
        /// <returns>The first message in the queue</returns>
        public IDecodedMessage Check()
        {
            return this.internalQueue.Peek();
        }

        /// <summary>
        /// Returns the first entry in the queue and removes it
        /// </summary>
        /// <returns>The first message in the queue</returns>
        public IDecodedMessage Pull()
        {
            return this.internalQueue.Dequeue();
        }
    }
}
