using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DLDReborn.Entity
{
    public class BotDbContetxt : DbContext
    {
        private readonly string _connectionString;

        public BotDbContetxt(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //BUILD CONNECTION
        }
    }
}
