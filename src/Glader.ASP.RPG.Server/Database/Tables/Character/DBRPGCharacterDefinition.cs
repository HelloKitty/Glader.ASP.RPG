using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Glader.ASP.RPG
{
	[Table("character_definition")]
	public class DBRPGCharacterDefinition<TRaceType, TClassType> : ICharacterEntryLinkable
		where TRaceType : Enum 
		where TClassType : Enum
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; private set; }

		//We use this property attribute this way because of this: https://stackoverflow.com/questions/50575259/asp-net-core-2-1-crashing-due-to-model-inverseproperty
		[Required]
		[ForeignKey(nameof(Id))]
		public virtual DBRPGCharacter Character { get; private set; }

		[Column("Race")]
		public TRaceType RaceId { get; private set; }

		[ForeignKey(nameof(RaceId))]
		public virtual DBRPGRace<TRaceType> Race { get; private set; }

		[Column("Class")]
		public TClassType ClassId { get; private set; }

		[ForeignKey(nameof(ClassId))]
		public virtual DBRPGClass<TClassType> @Class { get; private set; }

		public DBRPGCharacterDefinition(int characterId, TRaceType raceId, TClassType classId)
		{
			if (characterId <= 0) throw new ArgumentOutOfRangeException(nameof(characterId));

			Id = characterId;
			RaceId = raceId ?? throw new ArgumentNullException(nameof(raceId));
			ClassId = classId ?? throw new ArgumentNullException(nameof(classId));
		}

		/// <summary>
		/// EF Core Ctor.
		/// </summary>
		public DBRPGCharacterDefinition()
		{
			
		}
	}
}
