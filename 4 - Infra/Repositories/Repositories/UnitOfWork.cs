using API.Domain.Interfaces.Write;
using API.Domain.Notifications;
using API.Domain.Users.Auth;
using API.Infra.Repositories.Interfaces;
using API.Infra.SqlServer.Interfaces;
using Project.Entities;
using System;
using System.Threading.Tasks;

namespace API.Infra.Repositories.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly INotificationHandler _notificationHandler;
        private readonly IDbContext _context;

        public UnitOfWork(IDbContext context, INotificationHandler notificationHandler)
        {
            _notificationHandler = notificationHandler;
            _context = context;

            AppRoleRepository = new Repository<AppRole>(context);
            AppUserRepository = new Repository<AppUser>(context);

            ArvoreRepository = new Repository<Arvore>(context);
        }

        public IRepository<AppRole> AppRoleRepository { get; }
        public IRepository<AppUser> AppUserRepository { get; }

        public IRepository<Arvore> ArvoreRepository { get; }

        public void Dispose() => _context.Dispose();

        public async Task Save() => await _context.SaveChangesAsync();
    }
}
