using System;
using System.Collections.Generic;

namespace Seed.Application.Config
{
	public class AutoMapperConfigSeed
    {
        public static Type[] RegisterMappings()
        {
            return new List<Type>
            {
                typeof(DominioToDtoProfileSeed),
                typeof(DominioToDtoProfileSeedCustom)
            }.ToArray();
        }
	}
}
