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
	/// <typeparam name="TClassType">The class type.</typeparam>
	/// <typeparam name="TRaceType">The race type.</typeparam>
	[DataContract]
	[Table("character_item_default")]
	public class DBRPGCharacterItemDefault<TRaceType, TClassType, TItemClassType, TQualityType, TQualityColorStructureType>
		where TItemClassType : Enum 
		where TQualityType : Enum
		where TRaceType : Enum
		where TClassType : Enum
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

		[DataMember(Order = 3)]
		public TRaceType RaceId { get; private set; }

		[ForeignKey(nameof(RaceId))]
		[IgnoreDataMember]
		public virtual DBRPGRace<TRaceType> Race { get; private set; }

		[DataMember(Order = 4)]
		public TClassType ClassId { get; private set; }

		[ForeignKey(nameof(ClassId))]
		[IgnoreDataMember]
		public virtual DBRPGClass<TClassType> @Class { get; private set; }

		public DBRPGCharacterItemDefault(int itemTemplateId, TRaceType raceId, TClassType classId)
		{
			ItemTemplateId = itemTemplateId;
			RaceId = raceId ?? throw new ArgumentNullException(nameof(raceId));
			ClassId = classId ?? throw new ArgumentNullException(nameof(classId));
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public DBRPGCharacterItemDefault()
		{

		}
	}
}
