using Microsoft.EntityFrameworkCore;
using SelfieAWookie.API.UI;
using SelfieAWookies.Core.Selfies.Domain;
using SelfieAWookies.Core.Selfies.Infrastructures.Data;
using SelfiesAWookie.Core.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfieAWookies.Core.Selfies.Infrastructures.Repositories
{
    public class DefaultSelfieRepository : ISelfieRepository
    {
        //what i wanna get
        #region Fields
        private readonly SelfiesContext? _context = null;
        #endregion
        //how i wanna get
        #region Constructor
        public DefaultSelfieRepository( SelfiesContext context )
        {
            this._context = context;
        }

        
        #endregion
        #region Public methods
        public ICollection<Selfie> GetAll()
        {
            return this._context.Selfies.Include(item => item.Wookie).ToList();
        }

        public Selfie AddOne(Selfie item)
        {
            return this._context.Selfies.Add(item).Entity;
        }
        #endregion

        #region Properties
        public IUnitOfWork UnitOfWork => this._context;
        #endregion
    }
}
