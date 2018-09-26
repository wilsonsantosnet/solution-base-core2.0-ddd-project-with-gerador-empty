using Common.Domain.Interfaces;
using Score.Platform.Account.CrossCuting.Auth;
using Score.Platform.Account.Domain.Entitys;
using Score.Platform.Account.Domain.Interfaces.Repository;
using Sso.Server.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sso.Server.Api
{
    public class UserServices : IUserServices
    {
        private readonly ITenantRepository _tenantRepository;
        private readonly ICripto _cripto;

        public UserServices(ITenantRepository tenantRepository, ICripto cripto)
        {
            this._tenantRepository = tenantRepository;
            this._cripto = cripto;
        }

        public async Task<User> Auth(string userName, string password)
        {

            var passwordCripto = this._cripto.TripleDESCripto(password, SecurityConfig.GetSalt());
            var userTenant = await this._tenantRepository.SingleOrDefaultAsync(this._tenantRepository.GetAll()
                .Where(_ => _.Email == userName)
                .Where(_ => _.Password == passwordCripto));

            var user = await ConfigInitialClaims(userTenant);

            var userAdmin = Config.GetUsers()
                .Where(_ => _.Username == userName)
                .Where(_ => _.Password == password)
                .SingleOrDefault();

            if (userAdmin.IsNotNull())
                user = userAdmin;

            return user;


        }

        private async Task<User> ConfigInitialClaims(Tenant userTenant)
        {
            return await Task.Run(() =>
            {
                var user = default(User);

                if (userTenant.IsNotNull())
                {

                    user = new User
                    {
                        Claims = Config.ClaimsForTenant(userTenant.TenantId, userTenant.Name, userTenant.Email, userTenant.ProgramId, userTenant.Program.Datasource, userTenant.Program.DatabaseName, userTenant.Program.ThemaId),
                        SubjectId = userTenant.TenantId.ToString(),
                        Username = userTenant.Name,
                        ChangePassword = userTenant.ChangePasswordNextLogin,
                        Active = userTenant.Active

                    };
                }

                return user;
            });
        }
    }
}
