﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ThePMDB.Models;

namespace ThePMDB.Data
{
    public class PMDBMovieContext : DbContext
    {
        public PMDBMovieContext(DbContextOptions<PMDBMovieContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movie { get; set; }
    }
}
