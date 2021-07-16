using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Glader.Essentials;
using Refit;

namespace Glader.ASP.RPG
{
	/// <summary>
	/// Contract for REST service that provides
	/// character inventory services.
	/// </summary>
	[Headers("User-Agent: Glader")]
	public interface ICharacterInventoryService
	{
		//TODO: Require Role server authorization
		/// <summary>
		/// Attempts to add the provided item instance <see cref="instanceId"/> to the character <see cref="characterId"/> inventory.
		/// The item instance must be unowned.
		/// </summary>
		/// <param name="characterId">The character's id.</param>
		/// <param name="instanceId">The item instance's id.</param>
		/// <param name="token">The cancel token.</param>
		/// <returns>True if the item was added to the inventory.</returns>
		[RequiresAuthentication]
		[Post("/api/CharacterInventory/{cid}/{iid}/Add")]
		Task<bool> AddItemAsync([AliasAs("cid")] int characterId, [AliasAs("iid")] int instanceId, CancellationToken token = default);

		//TODO: Require Role server authorization
		/// <summary>
		/// Attempts to remove the provided item instance <see cref="instanceId"/> to the character <see cref="characterId"/> inventory.
		/// The item instance must be owned by the provided Character.
		/// </summary>
		/// <param name="characterId">The character's id.</param>
		/// <param name="instanceId">The item instance's id.</param>
		/// <param name="token">The cancel token.</param>
		/// <returns>True if the item was removed from the inventory.</returns>
		[RequiresAuthentication]
		[Delete("/api/CharacterInventory/{cid}/{iid}/Remove")]
		Task<bool> RemoveItemAsync([AliasAs("cid")] int characterId, [AliasAs("iid")] int instanceId, CancellationToken token = default);

		/// <summary>
		/// Retrieves the character's inventory if it exists.
		/// </summary>
		/// <param name="characterId">The character's id.</param>
		/// <param name="token">The cancel token.</param>
		/// <returns>Inventory data response.</returns>
		[RequiresAuthentication]
		[Get("/api/CharacterInventory/{cid}")]
		Task<ResponseModel<RPGCharacterItemInventoryResponse, CharacterItemInventoryQueryResult>> RetrieveItemsAsync([AliasAs("cid")] int characterId, CancellationToken token = default);
	}
}
