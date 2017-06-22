using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCPolls3.Models;
using Microsoft.EntityFrameworkCore;

namespace MVCPolls3.Data
{
    public class PollsContext: DbContext
    {
        public PollsContext(DbContextOptions<PollsContext> options) : base(options)
        {

        }
        public DbSet<Issue> Issues { get; set; }
    }
}
