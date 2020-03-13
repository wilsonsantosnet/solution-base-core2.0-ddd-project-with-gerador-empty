using System.ComponentModel.DataAnnotations;
using Common.Dto;
using System;

namespace Seed.Dto
{
	public class SampleTypeDto  : DtoBase
	{
	
        

        public virtual int SampleTypeId {get; set;}

        [Required(ErrorMessage="SampleType - Campo Name é Obrigatório")]
        [MaxLength(250, ErrorMessage = "SampleType - Quantidade de caracteres maior que o permitido para o campo Name")]
        public virtual string Name {get; set;}


		
	}
}