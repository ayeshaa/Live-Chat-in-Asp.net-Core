using ConflictRenewal.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;

namespace ConflictRenewal.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any conflicts.
            if (!context.Conflict.Any())
            {
                var conflicts = new Conflict[]
                {
                new Conflict
                {
                    ConflictDate = DateTime.Parse("2018-01-01"),
                    Question1 = "I'm supposed to start my job tomorrow. I don't want to go. It's going to be so awkward and I'm going to blow it.",
                    Question2 = "When I think about starting a new job I imagine myself standing there paralyzed, not knowing what to do. I'm sweating and my hands are shaking.",
                    Question3 = "I feel fear when I think about starting my new job.",
                    Question4 = "I want a good job to support myself and start a family some day.",
                    Question5 = "I'm going to tell my trainer how I feel, and I'm going to ask him to come with me to start my first day on the job.",
                    Question6 = "I'm going to take it day to day. I'm committing to one day at this new place, then I'll talk it over with my trainer if I still want to leave.",
                },
                new Conflict
                {
                    ConflictDate = DateTime.Parse("2018-02-01"),
                    Question1 = "Today the shift leader griped me out and said my station was dirty. But we had been really busy and there was just a tiny thing out of place. He singled me out and embarrassed me.",
                    Question2 = "I'm so mad right now. When I think about him embarrassing me in front of the team and even customers I wished I had hollered back at him!",
                    Question3 = "I feel angry when I think about my shift leader calling me out.",
                    Question4 = "I want a calm and relaxed work environment. If I'm in the wrong, I'm happy to make a correction, but I don't want to be yelled at.",
                    Question5 = "I'm going to calmly tell my shift leader in private tomorrow that I had an angry reaction to the way he corrected me. I'm going to ask that in the future he just let me know calmly.",
                    Question6 = "I'm going to talk to him tomorrow, but if I'm not able to resolve this and this keeps happening, I'm going to find another job."
                }
                };
                foreach (Conflict c in conflicts)
                {
                    c.MostrecentjournalDate = DateTime.Parse("2018-02-01");
                    context.Conflict.Add(c);
                }
                context.SaveChanges();

                var journals = new Journal[]
                {
                new Journal
                {
                    JournalDate = DateTime.Parse("2018-01-08"),
                    JournalContent = "I talked to my trainer and he agreed to drop me off in the morning, then call me during my lunch hour. I feel a lot better, but I'm still really nervous.",
                    ConflictId = 1
                },
                new Journal
                {
                    JournalDate = DateTime.Parse("2018-01-15"),
                    JournalContent = "Success! I made it. The first half of the day was terrible, but my trainer encouraged me to confess my fear with a coworker. She said she felt the same way on her first day and ruined a dropped a drink on a customer. We laughed about it and I relaxed. Now for day two!",
                    ConflictId = 1
                },
                new Journal
                {
                    JournalDate = DateTime.Parse("2018-02-08"),
                    JournalContent = "I had a pretty good talk with my shift leader. He said he understood, but that when things get busy he has to act quickly and can't always pull people aside for a chat. I get it. He said he'd do his best.",
                    ConflictId = 2
                },
                new Journal
                {
                    JournalDate = DateTime.Parse("2018-02-15"),
                    JournalContent = "Things have been going pretty well, I've actually been keeping my station immaculate and working really hard, so there is no reason for my shift leader to say anything.",
                    ConflictId = 2
                }
                };
                foreach (Journal j in journals)
                {
                    context.Journal.Add(j);
                }
                context.SaveChanges();
            }

            if (!context.Roles.Any())
            {
                var UserRole = new IdentityRole[]{
                    new IdentityRole
                    {
                        Name = "Admin",
                        NormalizedName = "Admin",
                        ConcurrencyStamp = Guid.NewGuid().ToString()
                    },
                     new IdentityRole
                    {
                        Name = "User",
                        NormalizedName = "User",
                        ConcurrencyStamp = Guid.NewGuid().ToString()
                    }
                };

                foreach (IdentityRole r in UserRole)
                {
                    context.Roles.Add(r);
                }
                context.SaveChanges();
            }

        }
    }
}
