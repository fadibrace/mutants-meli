using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MutantsAPI.Models;

namespace MutantsApi.Tests.Utils
{

    public class TestDbContextOptionsFactory
    {
        public static DbContextOptions<ReadGenomeContext> GetTestReadDbOptions()
        {
            var options = new DbContextOptionsBuilder<ReadGenomeContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

            return options;
        }

        public static DbContextOptions<CommandGenomeContext> GetTestCommandDbOptions()
        {
            var options = new DbContextOptionsBuilder<CommandGenomeContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

            return options;
        }
    }
}
