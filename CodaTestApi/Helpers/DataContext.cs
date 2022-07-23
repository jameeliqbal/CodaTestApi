using CodaTestApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodaTestApi.Helpers
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration configuration;

        public DataContext(IConfiguration configuration,DbContextOptions options): base(options)
        {
            this.configuration = configuration;
            LoadEvents();
        }

        //public DataContext(DbContextOptions options) : base(options)
        //{
        //    LoadEvents();
        //}

        private void LoadEvents()
        {
            var @event = new Event()
            {
                Title = "Event One",
                StartOn = DateTime.Now.AddDays(10),
                EndOn = DateTime.Now.AddDays(10).AddHours(1)
            };
            Events.Add(@event);

            @event = new Event()
            {
                Title = "Event Two",
                StartOn = DateTime.Now.AddDays(11),
                EndOn = DateTime.Now.AddDays(11).AddHours(1)
            };
            Events.Add(@event);

            @event = new Event()
            {
                Title = "Event Three",
                StartOn = DateTime.Now.AddDays(12),
                EndOn = DateTime.Now.AddDays(12).AddHours(1)
            };
            Events.Add(@event);

            @event = new Event()
            {
                Title = "Event Four",
                StartOn = DateTime.Now.AddDays(13),
                EndOn = DateTime.Now.AddDays(13).AddHours(1)
            };
            Events.Add(@event);


            @event = new Event()
            {
                Title = "Event Five",
                StartOn = DateTime.Now.AddDays(14),
                EndOn = DateTime.Now.AddDays(14).AddHours(1)
            };
            Events.Add(@event);
            SaveChanges();

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("CodaTestDB");
        }

         

        public DbSet<Event> Events { get; set; }
    }
}
