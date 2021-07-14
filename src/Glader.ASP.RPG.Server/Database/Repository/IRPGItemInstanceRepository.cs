using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;
using Microsoft.EntityFrameworkCore;

namespace Glader.ASP.RPG
{
	public interface IRPGItemInstanceRepository<TItemClassType, TQualityType, TQualityColorStructureType> : IGenericRepositoryCrudable<int, DBRPGItemInstance<TItemClassType, TQualityType, TQualityColorStructureType>>
		where TItemClassType : Enum 
		where TQualityType : Enum
	{

	}

	public class DefaultRPGItemInstanceRepository<TItemClassType, TQualityType, TQualityColorStructureType> : GeneralGenericCrudRepositoryProvider<int, DBRPGItemInstance<TItemClassType, TQualityType, TQualityColorStructureType>> 
		where TItemClassType : Enum 
		where TQualityType : Enum
	{
		public DefaultRPGItemInstanceRepository(DbContext context) 
			: base(context.Set<DBRPGItemInstance<TItemClassType, TQualityType, TQualityColorStructureType>>(), context)
		{

		}
	}
}
