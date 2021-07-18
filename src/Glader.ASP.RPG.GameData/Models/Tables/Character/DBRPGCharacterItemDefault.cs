using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace Glader.ASP.RPG
{
	/// <summary>
	/// Table for RPG default items for characters.
	/// (Ex. Items all characters should have by default)
	/// </summary>
	/// <typeparam name="TItemClassType">The itemclass type.</typeparam>
	/// <typeparam name="TQualityType">Quality type enum.</typeparam>
	/// <typeparam name="TQualityColorStructureType">The structure for the color.</typeparam>
	[DataContract]
	[Table("character_item_default")]
	public class DBRPGCharacterItemDefault<TItemClassType, TQualityType, TQualityColorStructureType>
		where TItemClassType : Enum 
		where TQualityType : Enum
	{
		/// <summary>
		/// The Item Class identifier.
		/// </summary>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[DataMember(Order = 1)]
		public int Id { get; private set; }

		[DataMember(Order = 2)]
		public int ItemTemplateId { get; private set; }

		[IgnoreDataMember]
		[ForeignKey(nameof(ItemTemplateId))]
		public virtual DBRPGItemTemplate<TItemClassType, TQualityType, TQualityColorStructureType> ItemTemplate { get; private set; }

		public DBRPGCharacterItemDefault(int itemTemplateId)
		{
			if (itemTemplateId <= 0) throw new ArgumentOutOfRangeException(nameof(itemTemplateId));
			ItemTemplateId = itemTemplateId;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public DBRPGCharacterItemDefault()
		{

		}
	}
}
