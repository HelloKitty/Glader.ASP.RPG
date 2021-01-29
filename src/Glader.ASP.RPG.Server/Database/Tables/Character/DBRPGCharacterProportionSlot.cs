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
	[Table("character_proportion_slot")]
	public class DBRPGCharacterProportionSlot<TProportionSlotType, TProportionStructureType>
		where TProportionSlotType : Enum
	{
		/// <summary>
		/// The character id this proportion is linked to.
		/// </summary>
		public int CharacterId { get; private set; }

		/// <summary>
		/// The slot type reference.
		/// </summary>
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Required]
		public TProportionSlotType SlotType { get; private set; }

		/// <summary>
		/// Slot type definition.
		/// </summary>
		[Required]
		[ForeignKey(nameof(SlotType))]
		public virtual DBRPGCharacterProportionSlotType<TProportionSlotType> SlotDefinition { get; private set; }

		/// <summary>
		/// The proportion data for the slot.
		/// </summary>
		public TProportionStructureType Proportion { get; private set; }

		public DBRPGCharacterProportionSlot(int characterId, TProportionSlotType slotType, TProportionStructureType proportion)
		{
			CharacterId = characterId;
			SlotType = slotType;
			Proportion = proportion ?? throw new ArgumentNullException(nameof(proportion));
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public DBRPGCharacterProportionSlot()
		{

		}
	}
}
