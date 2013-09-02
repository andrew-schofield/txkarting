// -----------------------------------------------------------------------
// <copyright file="TXID.cs" company="txcamp.org">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Org.TXCamp.TXKarting.TXKartingModel
{
    /// <summary>
    /// A 16 bit ID for identifying karts and drivers
    /// </summary>
    public class TXID
    {
        /// <summary>
        /// The internal representation of the TXID.
        /// In this case, a simple short. (16 bits).
        /// </summary>
        private readonly short internalID;

        /// <summary>
        /// Initializes a new instance of the TXID class.
        /// </summary>
        /// <param name="id">A 16 bit identifier</param>
        public TXID(short id)
        {
            this.internalID = id;
        }

        /// <summary>
        /// Gets the ID of the current kart
        /// </summary>
        /// <returns>A 16 bit identifier</returns>
        public short GetID()
        {
            return this.internalID;
        }

        public override bool Equals(object obj)
        {
            return this.internalID.Equals(((TXID)obj).GetID());
        }

        public override int GetHashCode()
        {
            return this.internalID.GetHashCode();
        }
    }
}
