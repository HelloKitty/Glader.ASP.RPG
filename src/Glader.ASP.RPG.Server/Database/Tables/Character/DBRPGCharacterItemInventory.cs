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
	public class DBRPGCharacterItemInventory<TItemClassType, TQualityType, TQualityColorStructureType> 
		where TItemClassType : Enum 
		where TQualityType : Enum
	{
		/// <summary>
		/// The id of the character.
		/// </summary>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int CharacterId { get; private set; }

		/// <summary>
		/// The character navigation property.
		/// </summary>
		[ForeignKey(nameof(CharacterId))]
		public virtual DBRPGCharacter Character { get; private set; }

		/// <summary>
		/// Collection of all Item ownerships (which map to an item instance).
		/// </summary>
		public virtual ICollection<DBRPGItemInstanceOwnership<TItemClassType, TQualityType, TQualityColorStructureType>> Items { get; private set; }

		public DBRPGCharacterItemInventory(int characterId)
		{
			if (characterId <= 0) throw new ArgumentOutOfRangeException(nameof(characterId));
			CharacterId = characterId;
		}

		public void AddItem(int instanceId)
		{
			if (instanceId <= 0) throw new ArgumentOutOfRangeException(nameof(instanceId));
			Items.Add(new DBRPGItemInstanceOwnership<TItemClassType, TQualityType, TQualityColorStructureType>(instanceId, ItemInstanceOwnershipType.CharacterInventory));
		}

		public void AddItem(DBRPGItemInstance<TItemClassType, TQualityType, TQualityColorStructureType> itemInstance)
		{
			if (itemInstance == null) throw new ArgumentNullException(nameof(itemInstance));
			AddItem(itemInstance.TemplateId);
		}
	}
}
