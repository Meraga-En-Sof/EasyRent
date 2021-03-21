using EasyRent.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyRent.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<AboutUs> AboutUs  { get; set; }

        public DbSet<Amenities> Amenities { get; set; }

        public DbSet<ContactInformation> ContactInformation  { get; set; }

        public DbSet<ContactUs> ContactUs { get; set; }

        public DbSet<Faq> Fag  { get; set; }

        public DbSet<Property> Properties { get; set; }

        public DbSet<PropertyAmenities> PropertyAmenities { get; set; }

        public DbSet<PropertyMode> propertyModes { get; set; }


        public DbSet<PropertySlider> PropertySliders { get; set; }

        public DbSet<PropertyType> PropertyTypes { get; set; }

        public DbSet<SocialMedia> SocialMedias { get; set; }

        public DbSet<Testimonials> Testimonials { get; set; }

        public DbSet<User> Users { get; set; }


    }
}
