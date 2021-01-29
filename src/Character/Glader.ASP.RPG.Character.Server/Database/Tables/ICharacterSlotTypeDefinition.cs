using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Glader.ASP.RPGCharacter
{
	/// <summary>
	/// Contract for slot definition types.
	/// </summary>
	public interface ICharacterSlotTypeDefinition<out TSlotType>
		where TSlotType : Enum
	{
		/// <summary>
		/// The slot type value.
		/// </summary>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		TSlotType SlotType { get; }

		/// <summary>
		/// The visual human-readable name for the slot.
		/// </summary>
		string VisualName { get; }

		/// <summary>
		/// The description of the slot.
		/// </summary>
		string Description { get; }
	}
}
