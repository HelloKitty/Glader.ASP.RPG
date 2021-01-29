using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Glader.ASP.RPG
{
	/// <summary>
	/// Database table model that represents an ownership relationship
	/// for a character.
	/// </summary>
	[Table("character_customization_slot")]
	public class DBRPGCharacterCustomizableSlot<TCustomizableSlotType, TColorStructureType>
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

		/// <summary>
		/// The color value of the slot.
		/// </summary>
		public TColorStructureType SlotColor { get; private set; }

		public DBRPGCharacterCustomizableSlot(int characterId, TCustomizableSlotType slotType, int customizationId, TColorStructureType slotColor)
		{
			CharacterId = characterId;
			SlotType = slotType;
			CustomizationId = customizationId;
			SlotColor = slotColor ?? throw new ArgumentNullException(nameof(slotColor));
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public DBRPGCharacterCustomizableSlot()
		{

		}
	}
}
