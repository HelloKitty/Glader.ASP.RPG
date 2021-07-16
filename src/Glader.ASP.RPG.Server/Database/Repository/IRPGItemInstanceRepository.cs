using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Glader.Essentials;
using Microsoft.EntityFrameworkCore;

namespace Glader.ASP.RPG
{
	public interface IRPGItemInstanceRepository<TItemClassType, TQualityType, TQualityColorStructureType> : IGenericRepositoryCrudable<int, DBRPGItemInstance<TItemClassType, TQualityType, TQualityColorStructureType>>
		where TItemClassType : Enum 
		where TQualityType : Enum
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="model">The instance model.</param>
		/// <param name="token">Cancel token.</param>
		/// <returns></returns>
		Task<ItemInstanceCreationResult> TryCreateItemAsync(DBRPGItemInstance<TItemClassType, TQualityType, TQualityColorStructureType> model, CancellationToken token = default);
	}

	public class DefaultRPGItemInstanceRepository<TItemClassType, TQualityType, TQualityColorStructureType> 
		: GeneralGenericCrudRepositoryProvider<int, DBRPGItemInstance<TItemClassType, TQualityType, TQualityColorStructureType>>, IRPGItemInstanceRepository<TItemClassType, TQualityType, TQualityColorStructureType>
		where TItemClassType : Enum 
		where TQualityType : Enum
	{
		public DefaultRPGItemInstanceRepository(DbContext context) 
			: base(context.Set<DBRPGItemInstance<TItemClassType, TQualityType, TQualityColorStructureType>>(), context)
		{

		}

		/// <inheritdoc />
		public async Task<ItemInstanceCreationResult> TryCreateItemAsync(DBRPGItemInstance<TItemClassType, TQualityType, TQualityColorStructureType> model, CancellationToken token = default)
		{
			//We can give better error results by checking if the template is missing.
			if (!await Context
				.Set<DBRPGItemTemplate<TItemClassType, TQualityType, TQualityColorStructureType>>()
				.AnyAsync(i => i.Id == model.TemplateId, token))
			{
				return ItemInstanceCreationResult.TemplateMissing;
			}

			try
			{

				if (await TryCreateAsync(model, token))
					return ItemInstanceCreationResult.Success;
				else
					return ItemInstanceCreationResult.GeneralServerError;
			}
			catch (Exception e)
			{
				//TODO: Logging
				return ItemInstanceCreationResult.GeneralServerError;
			}
		}
	}
}
