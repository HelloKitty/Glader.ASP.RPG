using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Glader.ASP.RPGCharacter
{
	/// <summary>
	/// The character proportions slot type.
	/// </summary>
	[Table("character_proportion_slot_type")]
	public class DBRPGCharacterProportionSlotType<TProportionSlotType> : ICharacterSlotTypeDefinition<TProportionSlotType>
		where TProportionSlotType : Enum
	{
		/// <summary>
		/// The primary key (enumerated slot type).
		/// </summary>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public TProportionSlotType SlotType { get; private set; }

		/// <summary>
		/// The visual human-readable name for the slot.
		/// </summary>
		public string VisualName { get; private set; }

		/// <summary>
		/// The description of the slot.
		/// </summary>
		public string Description { get; private set; }

		public DBRPGCharacterProportionSlotType(TProportionSlotType slotType, string visualName, string description)
		{
			SlotType = slotType ?? throw new ArgumentNullException(nameof(slotType));
			VisualName = visualName;
			Description = description;
		}

		public DBRPGCharacterProportionSlotType(TProportionSlotType slotType)
		{
			SlotType = slotType ?? throw new ArgumentNullException(nameof(slotType));
			VisualName = String.Empty;
			Description = String.Empty;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public DBRPGCharacterProportionSlotType()
		{
			
		}
	}
}
