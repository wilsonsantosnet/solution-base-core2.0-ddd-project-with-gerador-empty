using System.Threading.Tasks;
using Sso.Server.Api.Model;

namespace Sso.Server.Api
{
    public interface IUserServices
    {
        Task<User> Auth(string userName, string password);
    }
}
