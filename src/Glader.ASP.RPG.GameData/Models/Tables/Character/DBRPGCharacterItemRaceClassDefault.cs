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
	[Table("character_item_race_class_default")]
	public class DBRPGCharacterItemRaceClassDefault<TRaceType, TClassType, TItemClassType, TQualityType, TQualityColorStructureType> : DBRPGCharacterItemDefault<TItemClassType, TQualityType, TQualityColorStructureType>
		where TItemClassType : Enum 
		where TQualityType : Enum
		where TRaceType : Enum
		where TClassType : Enum
	{
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

		public DBRPGCharacterItemRaceClassDefault(int itemTemplateId, TRaceType raceId, TClassType classId)
			: base(itemTemplateId)
		{
			RaceId = raceId ?? throw new ArgumentNullException(nameof(raceId));
			ClassId = classId ?? throw new ArgumentNullException(nameof(classId));
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public DBRPGCharacterItemRaceClassDefault()
			: base()
		{

		}
	}
}
