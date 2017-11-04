using System.Collections.Generic;
using System.IO;
using System.Linq;
using alfrek.api.Models.ApplicationUsers;
using alfrek.api.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Newtonsoft.Json;

namespace alfrek.api
{
    public static class DbContextExtension
    {
        public static bool AllMigrationsApplied(this DbContext context)
        {
            var applied = context.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);
            var total = context.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
        }

        public static void EnsureSeeded(this AlfrekDbContext context)
        {
            if (!context.Affiliations.Any())
            {
                var affiliations = JsonConvert.DeserializeObject<List<Affiliation>>(File.ReadAllText("Seeds" + Path.DirectorySeparatorChar + "affiliations.json"));
                context.AddRange(affiliations);
                context.SaveChanges();
            }
        }
    }
}