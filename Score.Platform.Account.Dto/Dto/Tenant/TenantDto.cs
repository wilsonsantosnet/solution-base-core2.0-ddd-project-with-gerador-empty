using System.ComponentModel.DataAnnotations;
using Common.Dto;
using System;

namespace Score.Platform.Account.Dto
{
	public class TenantDto  : DtoBase
	{
	
        

        public virtual int TenantId {get; set;}

        [Required(ErrorMessage="Tenant - Campo Name é Obrigatório")]
        [MaxLength(250, ErrorMessage = "Tenant - Quantidade de caracteres maior que o permitido para o campo Name")]
        public virtual string Name {get; set;}

        [Required(ErrorMessage="Tenant - Campo Email é Obrigatório")]
        [MaxLength(250, ErrorMessage = "Tenant - Quantidade de caracteres maior que o permitido para o campo Email")]
        public virtual string Email {get; set;}

        [Required(ErrorMessage="Tenant - Campo Password é Obrigatório")]
        [MaxLength(250, ErrorMessage = "Tenant - Quantidade de caracteres maior que o permitido para o campo Password")]
        public virtual string Password {get; set;}

        [Required(ErrorMessage="Tenant - Campo Active é Obrigatório")]
        public virtual bool Active {get; set;}

        

        public virtual int ProgramId {get; set;}

        

        public virtual Guid? GuidResetPassword {get; set;}

        

        public virtual DateTime? DateResetPassword {get; set;}

        [Required(ErrorMessage="Tenant - Campo ChangePasswordNextLogin é Obrigatório")]
        public virtual bool ChangePasswordNextLogin {get; set;}


		
	}
}