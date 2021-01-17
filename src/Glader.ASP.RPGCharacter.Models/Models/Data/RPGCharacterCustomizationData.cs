using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Glader.ASP.RPGCharacter
{
	/// <summary>
	/// Data model that represents customization data
	/// for a character.
	/// </summary>
	/// <typeparam name="TCustomizableSlotType">The customizable slot type. (Think base-boots, hair, skin, eye style)</typeparam>
	/// <typeparam name="TColorStructureType">The color structure for customization.</typeparam>
	/// <typeparam name="TProportionSlotType">Proportional character customization data. (Think width of fore-arm, cheek chubbyness)</typeparam>
	/// <typeparam name="TProportionStructureType">The proportion data structure for customization.</typeparam>
	public sealed class RPGCharacterCustomizationData<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType>
		where TCustomizableSlotType : Enum
		where TProportionSlotType : Enum
	{
		/// <summary>
		/// Empty instance of character customization data.
		/// </summary>
		public static RPGCharacterCustomizationData<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType> Empty { get; } = new RPGCharacterCustomizationData<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType>();

		//DO NOT REMOVE
		static RPGCharacterCustomizationData()
		{
			
		}

		/// <summary>
		/// Dictionary of customized slots (think belt, pants, skin, hair) mapped to a specified slot id.
		/// (Ex. the 3rd Hair Style)
		/// </summary>
		[JsonProperty]
		public Dictionary<TCustomizableSlotType, int> SlotData { get; private set; } = new Dictionary<TCustomizableSlotType, int>();

		/// <summary>
		/// Dictionary of colors for customized slots (think belt, pants, skin, hair) mapped to a specified slot id.
		/// (Ex. the color of the Hair Style)
		/// </summary>
		[JsonProperty]
		public Dictionary<TCustomizableSlotType, TColorStructureType> SlotColorData { get; private set; } = new Dictionary<TCustomizableSlotType, TColorStructureType>();

		/// <summary>
		/// Dictionary of proportion data that contains overrides of the default proportions.
		/// (Ex. width of forearm)
		/// </summary>
		[JsonProperty]
		public Dictionary<TProportionSlotType, TProportionStructureType> ProportionData { get; private set; } = new Dictionary<TProportionSlotType, TProportionStructureType>();

		/// <summary>
		/// The serializer ctor.
		/// </summary>
		[JsonConstructor]
		public RPGCharacterCustomizationData()
		{
			
		}
	}
}
