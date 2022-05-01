using Microsoft.EntityFrameworkCore;
using CountriesbyGDP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountriesbyGDP.Data
{
    public class StoredProcedureDBContext : DbContext
    {
        public StoredProcedureDBContext(DbContextOptions<StoredProcedureDBContext> options) : base(options) { }

        public DbSet<CountriesGdp> CountriesGdp { get; set; }
    }
}
