using AutoMapper;

namespace Score.Platform.Account.Application.Config
{
	public class AutoMapperConfigScore
    {
		public static void RegisterMappings()
		{

			Mapper.Initialize(x =>
			{
				x.AddProfile<DominioToDtoProfileScore>();
				x.AddProfile<DominioToDtoProfileScoreCustom>();
			});

		}
	}
}
