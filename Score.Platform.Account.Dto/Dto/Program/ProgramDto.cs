using System.ComponentModel.DataAnnotations;
using Common.Dto;
using System;

namespace Score.Platform.Account.Dto
{
	public class ProgramDto  : DtoBase
	{
	
        

        public virtual int ProgramId {get; set;}

        [Required(ErrorMessage="Program - Campo Description é Obrigatório")]
        [MaxLength(250, ErrorMessage = "Program - Quantidade de caracteres maior que o permitido para o campo Description")]
        public virtual string Description {get; set;}

        [Required(ErrorMessage="Program - Campo Datasource é Obrigatório")]
        [MaxLength(250, ErrorMessage = "Program - Quantidade de caracteres maior que o permitido para o campo Datasource")]
        public virtual string Datasource {get; set;}

        [Required(ErrorMessage="Program - Campo DatabaseName é Obrigatório")]
        [MaxLength(250, ErrorMessage = "Program - Quantidade de caracteres maior que o permitido para o campo DatabaseName")]
        public virtual string DatabaseName {get; set;}

        

        public virtual int ThemaId {get; set;}


		
	}
}