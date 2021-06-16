using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Glader.ASP.RPG;

namespace Glader.ASP.RPG
{
	/// <summary>
	/// Contract for slot definition types.
	/// </summary>
	public interface ICharacterSlotTypeDefinition<out TSlotType> : IModelDescriptable
		where TSlotType : Enum
	{
		/// <summary>
		/// The slot type value.
		/// </summary>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		TSlotType SlotType { get; }
	}
}
