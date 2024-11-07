using API.Domain.Users.Auth;
using API.Infra.Repositories.Interfaces;
using System.Threading.Tasks;
using Project.Entities;

namespace API.Domain.Interfaces.Write
{
    public interface IUnitOfWork
    {
        IRepository<AppUser> AppUserRepository { get; }
        IRepository<AppRole> AppRoleRepository { get; }

        IRepository<Arvore> ArvoreRepository { get; }

        Task Save();
    }
}
