using System.ComponentModel.DataAnnotations;
using Common.Dto;
using System;

namespace Score.Platform.Account.Dto
{
	public class ThemaDto  : DtoBase
	{
	
        

        public virtual int ThemaId {get; set;}

        [Required(ErrorMessage="Thema - Campo Name é Obrigatório")]
        [MaxLength(250, ErrorMessage = "Thema - Quantidade de caracteres maior que o permitido para o campo Name")]
        public virtual string Name {get; set;}

        [Required(ErrorMessage="Thema - Campo Description é Obrigatório")]
        [MaxLength(250, ErrorMessage = "Thema - Quantidade de caracteres maior que o permitido para o campo Description")]
        public virtual string Description {get; set;}


		
	}
}