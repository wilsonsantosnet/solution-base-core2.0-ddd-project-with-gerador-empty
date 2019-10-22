using Common.Domain.Base;
using Common.Domain.Interfaces;
using Common.Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace Sso.Server.Api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class ChangePasswordController : Controller
    {
        //private readonly IPessoasRepository _repPessoas;
        private readonly CurrentUser _user;
        private readonly ICripto _cripto;
        private readonly IOptions<ConfigSettingsBase> _configSettingsBase;
        private readonly IOptions<ConfigSettingsBase> _settings;

        public ChangePasswordController(
            //IPessoasRepository repPessoas, 
            ICripto cripto, 
            IOptions<ConfigSettingsBase> configSettingsBase, 
            CurrentUser user, 
            IOptions<ConfigSettingsBase> settings)
        {
            //this._repPessoas = repPessoas;
            this._user = user;
            this._cripto = cripto;
            this._configSettingsBase = configSettingsBase;
            this._settings = settings;
        }


        [HttpGet]
        [Route("GoBack")]
        public IActionResult GoBack()
        {
            return Redirect(Request.Cookies["returnUrl"]);
        }

        [HttpGet]
        public async Task<IActionResult> Get(string returnUrl)
        {

            //var user = await this._repPessoas.SingleOrDefaultAsync(this._repPessoas.GetAll()
            //    .Where(_ => _.PessoaId == this._user.GetSubjectId<int>()));



            Response.Cookies.Delete("ReturnUrl");
            Response.Cookies.Append("ReturnUrl", returnUrl, new Microsoft.AspNetCore.Http.CookieOptions()
            {
                Path = "/",
                Expires = DateTime.Now.AddDays(5)
            });

            //if (user.IsNotNull())
            //{
            //    return View("ChangePassword");
            //}

            return View("ChangePasswordError");
        }


        [HttpPost]
        public async Task<IActionResult> ChangePassword(string newPassword, string newConfPassword)
        {

            //var user = await this._repPessoas.SingleOrDefaultAsync(this._repPessoas.GetAll()
            //    .Where(_ => _.PessoaId == this._user.GetSubjectId<int>()));

            //if (user.IsNotNull())
            //{
            //    if (newPassword == newConfPassword)
            //    {
            //        var newPasswordCripto = this._cripto.TripleDESCripto(newPassword, SecurityConfig.GetSalt());
            //        user.RedefinirPassword(newPasswordCripto);
            //        user.SetarTrocarSenhaProximoLogin(false);

            //        this._repPessoas.Update(user);
            //        await this._repPessoas.CommitAsync();

            //        return View("ChangePasswordSuccess");
            //    }
            //    else
            //    {
            //        return View("ChangePasswordErrorPassword");
            //    }

            //}
            return View("ChangePasswordError");
        }

    }
}
