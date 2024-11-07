using API.Domain.Users.Auth;
using API.Infra.SqlServer.Interfaces;
using API.Infra.SqlServer.ModelBuilders;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Project.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace API.Infra.SqlServer.Context
{
    public class TimedHostedService : IHostedService, IDisposable
    {
        private readonly ILogger<TimedHostedService> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private Timer _timer;

        public TimedHostedService(ILogger<TimedHostedService> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Hosted Service iniciado.");
            _timer = new Timer(ExecuteStoredProcedure, null, TimeSpan.Zero, TimeSpan.FromMinutes(60));
            return Task.CompletedTask;
        }

        private void ExecuteStoredProcedure(object state)
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApiServerContext>();
            context.ExecuteStoredProcedure();
            _logger.LogInformation("Stored Procedure executada.");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Hosted Service parando.");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }

    public class ApiServerContext : IdentityDbContext<AppUser>, IDbContext, IDisposable
    {
        public ApiServerContext(DbContextOptions<ApiServerContext> options) : base(options)
        {
        }

        public void ExecuteStoredProcedure()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ApplyEntityConfigurations(modelBuilder);

            SetGlobalDeleteBehavior(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void SetGlobalDeleteBehavior(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        private void ApplyEntityConfigurations(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AppRoleConfiguration());
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());

            // Entidades
            modelBuilder.Entity<Arvore>().HasKey(x => x.ArvoreID);
        }

        public async Task<int> SaveChangesAsync() => await base.SaveChangesAsync();
    }
}
