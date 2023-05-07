using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SelfieAWookie.API.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SelfieAWookies.Core.Selfies.Domain;

namespace SelfieAWookies.Core.Selfies.Infrastructure.Data.TypeConfiguration
{
    /// <summary>
    /// Define all that is needed for the wookie table
    /// </summary>
    internal class WookieEntityTypeConfiguration : IEntityTypeConfiguration<Wookie>
    {
        #region Public methods
        public void Configure(EntityTypeBuilder<Wookie> builder)
        {
            builder.ToTable("Wookie");
            builder.HasKey(item => item.Id);
            builder.HasMany(item => item.Selfies);
        }
        #endregion
    }
}
