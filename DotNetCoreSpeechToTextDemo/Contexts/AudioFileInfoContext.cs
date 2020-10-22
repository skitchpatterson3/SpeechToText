using DotNetCoreSpeechToTextDemo.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreSpeechToTextDemo.Contexts
{
    public class AudioFileInfoContext : DbContext
    {
        public DbSet<AudioFile> AudioFiles { get; set; }
        
        public AudioFileInfoContext(DbContextOptions<AudioFileInfoContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            DateTime currentDateTime = new DateTime();
            currentDateTime = DateTime.Now;

            // seed database
            modelBuilder.Entity<AudioFile>()
                .HasData(
                new AudioFile()
                {
                    Id = 1,
                    Filename = "Rec01",
                    Description = "First audio file from DB seed data",
                    CreatedOn = currentDateTime.AddDays(-7).AddHours(-5).AddMinutes(-8).AddSeconds(35)
                },
                new AudioFile()
                {
                    Id = 2,
                    Filename = "Rec02",
                    Description = "Second audio file from DB seed data",
                    CreatedOn = currentDateTime.AddDays(-3).AddHours(-1).AddMinutes(-42).AddSeconds(-9)
                },
                new AudioFile()
                {
                    Id = 3,
                    Filename = "Rec03",
                    Description = "Third audio file from DB seed data",
                    CreatedOn = currentDateTime
                });
            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("connectionstring");
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
