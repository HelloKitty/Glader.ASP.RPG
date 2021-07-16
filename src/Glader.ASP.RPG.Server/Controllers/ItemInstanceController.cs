using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Glader.Essentials;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Glader.ASP.RPG
{
	[Route("api/ItemInstance")]
	public sealed class ItemInstanceController<TItemClassType, TQualityType, TQualityColorStructureType> : AuthorizationReadyController, IItemInstanceService
		where TQualityType : Enum 
		where TItemClassType : Enum
	{
		private IRPGItemInstanceRepository<TItemClassType, TQualityType, TQualityColorStructureType> ItemInstanceRepository { get; }

		public ItemInstanceController(IClaimsPrincipalReader claimsReader, 
			ILogger<AuthorizationReadyController> logger, 
			IRPGItemInstanceRepository<TItemClassType, TQualityType, TQualityColorStructureType> itemInstanceRepository) 
			: base(claimsReader, logger)
		{
			ItemInstanceRepository = itemInstanceRepository ?? throw new ArgumentNullException(nameof(itemInstanceRepository));
		}

		/// <inheritdoc />
		[ProducesJson]
		[HttpPost("Create")]
		public async Task<ResponseModel<RPGCreateItemInstanceResponse, ItemInstanceCreationResult>> CreateItemAsync([FromBody] RPGCreateItemInstanceRequest request, CancellationToken token = default)
		{
			if (request.IsNull())
				return Failure<RPGCreateItemInstanceResponse, ItemInstanceCreationResult>(ItemInstanceCreationResult.RequestInvalid);

			if (request.TemplateId == 0)
				return Failure<RPGCreateItemInstanceResponse, ItemInstanceCreationResult>(ItemInstanceCreationResult.TemplateMissing);

			try
			{
				var instance = new DBRPGItemInstance<TItemClassType, TQualityType, TQualityColorStructureType>(request.TemplateId);
				var result = await ItemInstanceRepository.TryCreateItemAsync(instance, token);

				switch (result)
				{
					case ItemInstanceCreationResult.Success:
						return Success<RPGCreateItemInstanceResponse, ItemInstanceCreationResult>(new RPGCreateItemInstanceResponse(instance.Id));
					default:
						return Failure<RPGCreateItemInstanceResponse, ItemInstanceCreationResult>(result);
				}
			}
			catch (Exception e)
			{
				if (Logger.IsEnabled(LogLevel.Warning))
					Logger.LogWarning($"Failed to create Item Instance with Template: {request.TemplateId}. Reason: {e}");

				return Failure<RPGCreateItemInstanceResponse, ItemInstanceCreationResult>(ItemInstanceCreationResult.GeneralServerError);
			}
		}
	}
}
