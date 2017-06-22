using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCPolls3.Models;

namespace MVCPolls3.Data
{
    public static class DbInitializer
    {
        public static void Initialize(PollsContext context)
        {
            context.Database.EnsureCreated();
            if (context.Issues.Any())
            {
                return;
            }

            var issues = new Issue[]
            {
                new Issue{Description = "Not enough bins on street", Postcode = Postcode.AB10},
                new Issue{Description = "Broken streetlamps", Postcode = Postcode.AB10},
                new Issue{Description = "Cars driving too fast down Crown Street", Postcode = Postcode.AB11},
                new Issue{Description = "Too noisy on Saturday nights", Postcode = Postcode.AB11},
                new Issue{Description = "Park requires cleaning", Postcode = Postcode.AB12},
            };
            foreach(Issue i in issues)
            {
                context.Issues.Add(i);
            }
            context.SaveChanges();

        }
    }
}
