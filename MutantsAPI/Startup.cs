using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using MutantsAPI.Models;
using MutantsAPI.Domains;
using MutantsAPI.Repositories;
using Hangfire;
using Hangfire.MemoryStorage;
using System;
using MutantsAPI.Utils;

namespace MutantsAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IHousekeeperJob, HousekeeperJob>();

            services.AddScoped<IGenomeService, GenomeService>();
            services.AddScoped<IStatsService, StatsService>();

            services.AddScoped<IGenomeRepository, GenomeRepository>();
            services.AddScoped<IStatsRepository, StatsRepository>();

            services.AddDbContext<CommandGenomeContext>(opt => opt.UseInMemoryDatabase("GenomeList"));
            services.AddDbContext<ReadGenomeContext>(o => o.UseSqlServer(Configuration.GetConnectionString("SqlServer")));

            services.AddHangfire(config =>
                config.UseSimpleAssemblyNameTypeSerializer()
                .UseDefaultTypeSerializer()
                .UseMemoryStorage());
            //services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("SqlServer")));
            services.AddHangfireServer();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHousekeeperJob jobs)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var options = new BackgroundJobServerOptions { WorkerCount = Environment.ProcessorCount * 5 };
            app.UseHangfireServer(options);
            BackgroundJob.Schedule(() => jobs.LoadReadDbJob(), TimeSpan.FromSeconds(5));
        }
    }
}
