﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using WorkshopMVC.Models;

namespace WebAdminAndApi.Models
{
    /// <summary>
    /// This class defines the database context which uses entity framework to access the database.
    /// </summary>
    public class ImageAppDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageAppDbContext"/> on a given connection string.
        /// </summary>
        public ImageAppDbContext()
            : base("DefaultConnection")
        {
        }
        
        public DbSet<Image> Images { get; set; }
		
		// TODO: Add the DbSets for Posts, Likes and Users

        /// <summary>
        /// Is invoked when model is created in the database.
        /// </summary>
        /// <param name="modelBuilder">The model builder instance</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Creates the tables in singular names: Posts => Post, Users => User, ...
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}