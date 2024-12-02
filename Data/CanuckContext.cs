using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using canuckacs.Models;

    public class CanuckContext : DbContext
    {
        public CanuckContext (DbContextOptions<CanuckContext> options)
            : base(options)
        {
        }

        public DbSet<canuckacs.Models.Tag> Tag { get; set; } = default!;
    }
