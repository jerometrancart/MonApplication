using SelfieAWookie.API.UI;
using SelfiesAWookie.Core.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfieAWookies.Core.Selfies.Domain
{   /// <summary>
    /// Repository to manage selfies
    /// </summary>
    public interface ISelfieRepository : IRepository
    {
        /// <summary>
        /// GETS ALL SELFIES
        /// </summary>
        /// <returns></returns>
        ICollection<Selfie> GetAll(int wookieId);
        /// <summary>
        /// ADD ONE SELFIE TO DB
        /// </summary>

        Selfie AddOne(Selfie item);

        /// <summary>
        /// creates a new picture
        /// </summary>
        Picture AddOnePicture(string url);
    }
}
