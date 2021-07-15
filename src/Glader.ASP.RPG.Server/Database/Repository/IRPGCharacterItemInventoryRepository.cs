using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Glader.Essentials;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Glader.ASP.RPG
{
	//Needed a specialized repository
	public interface IRPGCharacterItemInventoryRepository<TItemClassType, TQualityType, TQualityColorStructureType>
		where TItemClassType : Enum 
		where TQualityType : Enum
	{
		/// <summary>
		/// Indicates if the character with the specified id <see cref="characterId"/> has any items in their inventory.
		/// </summary>
		/// <param name="characterId">The character id.</param>
		/// <param name="token">The cancel token.</param>
		/// <returns>True if the character has any items in their inventory.</returns>
		Task<bool> CharacterHasItemsAsync(int characterId, CancellationToken token = default);

		/// <summary>
		/// Adds the item <see cref="instance"/> to the player's inventory if able.
		/// The <see cref="instance"/> could already be claimed or linked. Therefore it's possible this method indicates failure to add to inventory
		/// for a variety of reasons.
		/// </summary>
		/// <param name="characterId">The character id to add the instance to.</param>
		/// <param name="instance">The item instance.</param>
		/// <param name="token">The cancel token.</param>
		/// <returns>True if the item is added.</returns>
		Task<bool> AddInstanceAsync(int characterId, DBRPGItemInstance<TItemClassType, TQualityType, TQualityColorStructureType> instance, CancellationToken token = default);

		/// <summary>
		/// Adds the item <see cref="instanceId"/> to the player's inventory if able.
		/// The <see cref="instanceId"/> could already be claimed or linked. Therefore it's possible this method indicates failure to add to inventory
		/// for a variety of reasons.
		/// </summary>
		/// <param name="characterId">The character id to add the instance to.</param>
		/// <param name="instanceId">The id of the item instance.</param>
		/// <param name="token">The cancel token.</param>
		/// <returns>True if the item is added.</returns>
		Task<bool> AddInstanceAsync(int characterId, int instanceId, CancellationToken token = default);

		/// <summary>
		/// Attempts to remove the character's item instance from their inventory.
		/// </summary>
		/// <param name="model">The inventory item model.</param>
		/// <param name="token">The cancel token.</param>
		/// <returns>True if the item is removed.</returns>
		Task<bool> TryDeleteAsync(DBRPGCharacterItemInventory<TItemClassType, TQualityType, TQualityColorStructureType> model, CancellationToken token = default);

		/// <summary>
		/// Attempts to remove the character's item instance from their inventory.
		/// The provided <see cref="characterId"/> must own the <see cref="instanceId"/>.
		/// </summary>
		/// <param name="characterId">The character id of the inventory.</param>
		/// <param name="instanceId">The id of the item instance to remove.</param>
		/// <param name="token">The cancel token.</param>
		/// <returns>True if the item is removed.</returns>
		Task<bool> TryDeleteAsync(int characterId, int instanceId, CancellationToken token = default);
	}

	public class DefaultRPGCharacterItemInventoryRepository<TItemClassType, TQualityType, TQualityColorStructureType> : IRPGCharacterItemInventoryRepository<TItemClassType, TQualityType, TQualityColorStructureType>
		where TItemClassType : Enum 
		where TQualityType : Enum
	{
		public DbSet<DBRPGCharacterItemInventory<TItemClassType, TQualityType, TQualityColorStructureType>> ModelSet { get; }

		public DbContext Context { get; }

		private ILogger<DefaultRPGCharacterItemInventoryRepository<TItemClassType, TQualityType, TQualityColorStructureType>> Logger { get; }

		public DefaultRPGCharacterItemInventoryRepository(DbContext context, 
			ILogger<DefaultRPGCharacterItemInventoryRepository<TItemClassType, TQualityType, TQualityColorStructureType>> logger)
		{
			Context = context ?? throw new ArgumentNullException(nameof(context));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			ModelSet = context.Set<DBRPGCharacterItemInventory<TItemClassType, TQualityType, TQualityColorStructureType>>();
		}

		/// <inheritdoc />
		public async Task<bool> CharacterHasItemsAsync(int characterId, CancellationToken token = default)
		{
			return await ModelSet.AnyAsync(i => i.CharacterId == characterId, token);
		}

		/// <inheritdoc />
		public async Task<bool> AddInstanceAsync(int characterId, DBRPGItemInstance<TItemClassType, TQualityType, TQualityColorStructureType> instance, CancellationToken token = default)
		{
			DBRPGCharacterItemInventory<TItemClassType, TQualityType, TQualityColorStructureType> inventoryItem = new DBRPGCharacterItemInventory<TItemClassType, TQualityType, TQualityColorStructureType>(characterId);
			inventoryItem.SetItem(instance);
			ModelSet.Add(inventoryItem);

			try
			{
				return 0 != await Context.SaveChangesAsync(true, token);
			}
			catch (Exception e)
			{
				//TODO: Add logging, this can fail if the item is already owned or doesn't exist.
				if (Logger.IsEnabled(LogLevel.Warning))
					Logger.LogWarning($"Failed to add Item: {instance.Id} to Character: {characterId} but failed. Reason: {e}");

				return false;
			}
		}

		public async Task<bool> AddInstanceAsync(int characterId, int instanceId, CancellationToken token = default)
		{
			var instance = await Context.Set<DBRPGItemInstance<TItemClassType, TQualityType, TQualityColorStructureType>>()
				.FirstOrDefaultAsync(i => i.Id == instanceId, token);

			if(instance.IsNull())
				return false;

			return await AddInstanceAsync(characterId, instance, token);
		}

		/// <inheritdoc />
		public async Task<bool> TryDeleteAsync(DBRPGCharacterItemInventory<TItemClassType, TQualityType, TQualityColorStructureType> model, CancellationToken token = default)
		{
			var ownershipSet = Context.Set<DBRPGItemInstanceOwnership<TItemClassType, TQualityType, TQualityColorStructureType>>();

			//Chance the nav property isn't loaded
			if (model.ItemOwnership.IsNull())
			{
				var ownershipEntry = await ownershipSet.FirstOrDefaultAsync(o => o.Id == model.OwnershipId && o.OwnershipType == model.OwnershipType, token);

				if (ownershipEntry.IsNull())
					return false;

				ownershipSet.Remove(ownershipEntry);
			}
			else
				ownershipSet.Remove(model.ItemOwnership);

			try
			{
				return 0 != await Context.SaveChangesAsync(true, token);
			}
			catch(Exception e)
			{
				//TODO: OwnershipId might not be ItemId eventually sooo we need a safer way to do this
				if(Logger.IsEnabled(LogLevel.Warning))
					Logger.LogWarning($"Failed to remove Item: {model.OwnershipId} to Character: {model.CharacterId} but failed. Reason: {e}");

				return false;
			}
		}

		public async Task<bool> TryDeleteAsync(int characterId, int instanceId, CancellationToken token = default)
		{
			//WARNING: It's important we ONLY locate an item instanced ownered by characterId. Otherwise, we'd be deleting items that don't own.
			var inventoryItem = await Context.Set<DBRPGCharacterItemInventory<TItemClassType, TQualityType, TQualityColorStructureType>>()
				.Include(m => m.ItemOwnership)
				.ThenInclude(m => m.Instance)
				.FirstOrDefaultAsync(i => i.CharacterId == characterId && i.ItemOwnership.Instance.Id == instanceId, token); //Don't assume InstanceId is PK of Ownership, it could change in the future. Check instance table.

			if(inventoryItem.IsNull())
				return false;

			return await TryDeleteAsync(inventoryItem, token);
		}
	}
}
