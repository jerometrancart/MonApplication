using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SelfieAWookie.API.UI;
using SelfieAWookies.Core.Selfies.Domain;
using SelfieAWookies.Core.Selfies.Infrastructures.Data.TypeConfiguration;
using SelfiesAWookie.Core.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SelfieAWookies.Core.Selfies.Infrastructures.Data
{
    public class SelfiesContext : DbContext, IUnitOfWork
    {
        #region Constructor
        public SelfiesContext([NotNullAttribute] DbContextOptions options) : base(options) { }

        public SelfiesContext(): base() { }
        #endregion

        #region Internal methods

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new SelfieEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new WookieEntityTypeConfiguration());
        }

        #endregion
        #region Properties
        public DbSet<Selfie> Selfies { get; set; }
        public DbSet<Wookie> Wookies { get; set; }
        #endregion
    }
}
