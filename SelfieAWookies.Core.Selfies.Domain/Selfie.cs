using SelfieAWookies.Core.Selfies.Domain;

namespace SelfieAWookie.API.UI
{
    /// <summary>
    /// Represent a selfie with a wookie linked
    /// </summary>
    public class Selfie
    {
        #region Properties
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? ImagePath { get; set; }
        /// <summary>
        /// Shadow foreign key
        /// </summary>
        public int WookieId { get; set; }    
        public Wookie? Wookie { get; set; }

        public int PictureId { get; set; }

        public Picture? Picture { get; set;}
        #endregion
    }
}
