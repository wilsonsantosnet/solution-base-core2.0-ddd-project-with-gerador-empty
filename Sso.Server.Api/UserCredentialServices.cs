using Sso.Server.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sso.Server.Api
{
    public class UserCredentialServices : IUserServices
    {

        public async Task<UserCredential> Auth(string userName, string password)
        {

            //return await Task.Run(() =>
            //{
            //    var user = default(User);

            //    var userAdmin = Config.GetUsers()
            //        .Where(_ => _.Username == userName)
            //        .Where(_ => _.Password == password)
            //        .SingleOrDefault();

            //    if (userAdmin.IsNotNull())
            //        user = userAdmin;

            //    return user;
            //});


            throw new InvalidCastException("Auth User Service not implemented, uncomment code above");


        }

     

    }
}
