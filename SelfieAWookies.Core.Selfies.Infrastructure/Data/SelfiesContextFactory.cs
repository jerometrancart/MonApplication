﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SelfieAWookies.Core.Selfies.Infrastructure.Data
{
    internal class SelfiesContextFactory : IDesignTimeDbContextFactory<SelfiesContext>
    {
        /// <summary>
        /// implement design factory
        /// </summary>
        
        #region Public methods
        public SelfiesContext CreateDbContext(string[] args)
        {
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();

            configurationBuilder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "Settings", "appSettings.json"));

            IConfigurationRoot configurationRoot = configurationBuilder.Build();

            DbContextOptionsBuilder builder = new DbContextOptionsBuilder();

            builder.UseSqlServer(configurationRoot.GetConnectionString("SelfiesDatabase"));
            //, b => b.MigrationsAssembly("SelfieAWookies.Core.Selfies.Data.Migrations")
            SelfiesContext context = new SelfiesContext(builder.Options);

            return context;
        }
        #endregion
    }
}
