
namespace Sso.Server.Api.Model
{
    public class AccountCredential
    {

        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Scope { get; set; }

        public string User { get; set; }
        public string Password { get; set; }

    }
}
