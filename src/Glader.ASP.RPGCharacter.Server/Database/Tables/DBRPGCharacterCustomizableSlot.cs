using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Glader.ASP.RPGCharacter
{
	/// <summary>
	/// Database table model that represents an ownership relationship
	/// for a character.
	/// </summary>
	[Table("character_customization_slot")]
	public class DBRPGCharacterCustomizableSlot<TCustomizableSlotType>
		where TCustomizableSlotType : Enum
	{
		/// <summary>
		/// The character id this customization is linked to.
		/// </summary>
		public int CharacterId { get; private set; }

		/// <summary>
		/// The slot type reference.
		/// </summary>
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Required]
		public TCustomizableSlotType SlotType { get; private set; }

		/// <summary>
		/// Slot type definition.
		/// </summary>
		[Required]
		[ForeignKey(nameof(SlotType))]
		public virtual DBRPGCharacterCustomizableSlotType<TCustomizableSlotType> SlotDefinition { get; private set; }

		/// <summary>
		/// The customized id.
		/// </summary>
		public int CustomizationId { get; private set; }

		public DBRPGCharacterCustomizableSlot(int characterId, TCustomizableSlotType slotType, int customizationId)
		{
			CharacterId = characterId;
			SlotType = slotType;
			CustomizationId = customizationId;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public DBRPGCharacterCustomizableSlot()
		{
			
		}
	}
}
