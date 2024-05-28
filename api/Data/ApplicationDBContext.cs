using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace api.Data
{
    public class ApplicationDBContext:IdentityDbContext<Buyer>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions)
        : base(dbContextOptions)
        {

        }
        
        public DbSet<Phone> Phones {get;set;}
        public DbSet<Review> Reviews{get;set;}

        public DbSet<PhoneChar> PhoneChar{get;set;}

        public DbSet<Pocket> Pockets{get;set;}
         public DbSet<UserLikes> UserLikes {get;set;}

         protected override void OnModelCreating(ModelBuilder builder)
         {


             base.OnModelCreating(builder);

            builder.Entity<Pocket>(x => x.HasKey(p => new { p.BuyerId, p.PhoneId }));

             builder.Entity<Pocket>()
                .HasOne(u => u.Buyer)
                .WithMany(u => u.Pockets)
                .HasForeignKey(p => p.BuyerId);

            builder.Entity<Pocket>()
                .HasOne(u => u.Phone)
                .WithMany(u => u.Pockets)
                .HasForeignKey(p => p.PhoneId);
                
                
         builder.Entity<Phone>()
        .HasMany(c => c.PhoneChar)
        .WithOne(e => e.Phone).OnDelete(DeleteBehavior.Cascade);

          builder.Entity<Phone>()
        .HasMany(c => c.Reviews)
        .WithOne(e => e.Phone).OnDelete(DeleteBehavior.Cascade);


        builder.Entity<UserLikes>(x => x.HasKey(p => new { p.BuyerId, p.PhoneId }));

             builder.Entity<UserLikes>()
                .HasOne(u => u.Buyer)
                .WithMany(u => u.UserLikes)
                .HasForeignKey(p => p.BuyerId);

            builder.Entity<UserLikes>()
                .HasOne(u => u.Phone)
                .WithMany(u => u.UserLikes)
                .HasForeignKey(p => p.PhoneId);

            

              List<IdentityRole> roles= new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name="Admin",
                    NormalizedName="ADMIN"
                },
                
                new IdentityRole
                {
                    Name="Buyer",
                    NormalizedName="BUYER"
                },

                  new IdentityRole
                {
                    Name="Reviewer",
                    NormalizedName="REVIEWER"
                },
            };
            builder.Entity<IdentityRole>().HasData(roles);
         }

    }
      
      
}