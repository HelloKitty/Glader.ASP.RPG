using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Glader.ASP.RPG
{
	/// <summary>
	/// Table that represents the character's inventory which maps to <see cref="DBRPGItemInstance{TItemClassType,TQualityType,TQualityColorStructureType}"/>s and <see cref="DBRPGItemInstanceOwnership{TItemClassType,TQualityType,TQualityColorStructureType}"/>s.
	/// Containing a 
	/// </summary>
	/// <typeparam name="TItemClassType"></typeparam>
	/// <typeparam name="TQualityType"></typeparam>
	/// <typeparam name="TQualityColorStructureType"></typeparam>
	[Table("character_item_inventory")]
	public class DBRPGCharacterItemInventory<TItemClassType, TQualityType, TQualityColorStructureType> : IDBRPGItemInventoryContainable<TItemClassType, TQualityType, TQualityColorStructureType>
		where TItemClassType : Enum 
		where TQualityType : Enum
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; private set; }

		/// <summary>
		/// The id of the character.
		/// </summary>
		public int CharacterId { get; private set; }

		/// <summary>
		/// The character navigation property.
		/// </summary>
		[ForeignKey(nameof(CharacterId))]
		public virtual DBRPGCharacter Character { get; private set; }

		/// <inheritdoc />
		public int OwnershipId { get; private set; }

		/// <inheritdoc />
		public ItemInstanceOwnershipType OwnershipType { get; private set; }

		/// <inheritdoc />
		public virtual DBRPGItemInstanceOwnership<TItemClassType, TQualityType, TQualityColorStructureType> ItemOwnership { get; private set; }

		public DBRPGCharacterItemInventory(int characterId)
		{
			if (characterId <= 0) throw new ArgumentOutOfRangeException(nameof(characterId));
			CharacterId = characterId;
			OwnershipType = ItemInstanceOwnershipType.CharacterInventory;
		}

		public void SetItem(DBRPGItemInstanceOwnership<TItemClassType, TQualityType, TQualityColorStructureType> ownership)
		{
			if (ownership == null) throw new ArgumentNullException(nameof(ownership));

			//Important we never link an item of the wrong inventory type.
			if (ownership.OwnershipType != ItemInstanceOwnershipType.CharacterInventory)
				throw new InvalidOperationException($"Cannot link: {nameof(DBRPGCharacterItemInventory<TItemClassType, TQualityType, TQualityColorStructureType>)} with {nameof(DBRPGItemInstanceOwnership<TItemClassType, TQualityType, TQualityColorStructureType>.OwnershipType)}: {ownership.OwnershipType}. Must be: {ItemInstanceOwnershipType.CharacterInventory}");

			OwnershipType = ItemInstanceOwnershipType.CharacterInventory;
			OwnershipId = ownership.Id;
			ItemOwnership = ownership;
		}
	}
}
