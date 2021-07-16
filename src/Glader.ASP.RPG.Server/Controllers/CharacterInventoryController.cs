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
	[Route("api/CharacterInventory")]
	public sealed class CharacterInventoryController<TItemClassType, TQualityType, TQualityColorStructureType> : AuthorizationReadyController, ICharacterInventoryService
		where TQualityType : Enum 
		where TItemClassType : Enum
	{
		private IRPGCharacterItemInventoryRepository<TItemClassType, TQualityType, TQualityColorStructureType> InventoryRepository { get; }

		public CharacterInventoryController(IClaimsPrincipalReader claimsReader, ILogger<AuthorizationReadyController> logger, 
			IRPGCharacterItemInventoryRepository<TItemClassType, TQualityType, TQualityColorStructureType> inventoryRepository) 
			: base(claimsReader, logger)
		{
			InventoryRepository = inventoryRepository ?? throw new ArgumentNullException(nameof(inventoryRepository));
		}

		//TODO: Require Role server authorization
		/// <inheritdoc />
		[AuthorizeJwt]
		[HttpPost("{cid}/{iid}/Add")]
		public async Task<bool> AddItemAsync([FromRoute(Name = "cid")] int characterId, [FromRoute(Name = "iid")] int instanceId, CancellationToken token = default)
		{
			if(Logger.IsEnabled(LogLevel.Warning))
				Logger.LogWarning($"WARNING: API must be secured by Server role one day.");

			return await InventoryRepository.AddInstanceAsync(characterId, instanceId, token);
		}

		//TODO: Require Role server authorization
		/// <inheritdoc />
		[AuthorizeJwt]
		[HttpDelete("{cid}/{iid}/Add")]
		public async Task<bool> RemoveItemAsync([FromRoute(Name = "cid")] int characterId, [FromRoute(Name = "iid")] int instanceId, CancellationToken token = default)
		{
			if(Logger.IsEnabled(LogLevel.Warning))
				Logger.LogWarning($"WARNING: API must be secured by Server role one day.");

			return await InventoryRepository.TryDeleteAsync(characterId, instanceId, token);
		}
	}
}
