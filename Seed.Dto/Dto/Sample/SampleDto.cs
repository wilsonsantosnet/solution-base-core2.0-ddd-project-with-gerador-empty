using System.ComponentModel.DataAnnotations;
using Common.Dto;
using System;

namespace Seed.Dto
{
	public class SampleDto  : DtoBase
	{
	
        

        public virtual int SampleId {get; set;}

        [Required(ErrorMessage="Sample - Campo Name é Obrigatório")]
        [MaxLength(50, ErrorMessage = "Sample - Quantidade de caracteres maior que o permitido para o campo Name")]
        public virtual string Name {get; set;}

        

        public virtual string Descricao {get; set;}

        

        public virtual int SampleTypeId {get; set;}

        

        public virtual bool? Ativo {get; set;}

        

        public virtual int? Age {get; set;}

        

        public virtual int? Category {get; set;}

        

        public virtual DateTime? Datetime {get; set;}

        

        public virtual string Tags {get; set;}


		
	}
}