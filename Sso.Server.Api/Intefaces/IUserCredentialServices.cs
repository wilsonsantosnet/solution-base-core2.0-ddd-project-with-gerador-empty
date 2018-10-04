using System.Threading.Tasks;
using Sso.Server.Api.Model;

namespace Sso.Server.Api
{
    public interface IUserCredentialServices
    {
        Task<UserCredential> Auth(string userName, string password);
    }
}
