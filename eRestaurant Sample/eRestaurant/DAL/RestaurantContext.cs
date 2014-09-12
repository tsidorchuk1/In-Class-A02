﻿using eRestaurant.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity; // needed for DBContent
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRestaurant.DAL
{
    public class RestaurantContext : DbContext
    {
        #region Constructor
        public RestaurantContext() : base("name=EatIn") { }
        #endregion

        #region Properties for Table-To-Entity Mappings
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<SpecialEvent> SpecialEvents { get; set; }
        #endregion

        #region Over-Ride OnModelCreating

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Reservation>().HasMany(r => r.Tables)
                .WithMany(t => t.Reservations)
                .Map(mapping =>
            {

                mapping.ToTable("ReservationsTable");
                mapping.MapLeftKey("ReservationID");
                mapping.MapRightKey("TableID");

            });

            base.OnModelCreating(modelBuilder);
        }
        #endregion
    }
}