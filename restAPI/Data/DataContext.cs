using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using restAPI.Domain;

namespace restAPI.Data
{
    public class DataContext : IdentityDbContext
    {

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

    // DbSet = SQL table, named Posts
    public DbSet<Post> Posts { get; set; }

    public DbSet<RefreshToken> RefreshTokens { get; set; }

    }

}
