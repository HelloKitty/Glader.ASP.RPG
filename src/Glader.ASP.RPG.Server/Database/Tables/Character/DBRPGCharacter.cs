using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Glader.ASP.RPG
{
	/// <summary>
	/// Database-based implementation of <see cref="IRPGCharacterEntry"/> <see cref="IRPGDBCreationDetailable"/>
	/// </summary>
	[Table("character")]
	public class DBRPGCharacter<TRaceType, TClassType> : IRPGCharacterEntry, IRPGDBCreationDetailable
		where TRaceType : Enum
		where TClassType : Enum
	{
		/// <inheritdoc />
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; private set; }

		/// <inheritdoc />
		[Required]
		public string Name { get; private set; }

		/// <summary>
		/// The creation data of the character.
		/// </summary>
		[Required]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public DateTime CreationDate { get; private set; }

		/// <summary>
		/// Last time the character entry was modified.
		/// </summary>
		[Required]
		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime LastModifiedDate { get; private set; }

		[Required]
		[ForeignKey(nameof(Id))]
		public virtual DBRPGCharacterProgress Progress { get; private set; }

		[Column("Race")]
		public TRaceType RaceId { get; private set; }

		[ForeignKey(nameof(RaceId))]
		public virtual DBRPGRace<TRaceType> Race { get; private set; }

		[Column("Class")]
		public TClassType ClassId { get; private set; }

		[ForeignKey(nameof(ClassId))]
		public virtual DBRPGClass<TClassType> @Class { get; private set; }

		public DBRPGCharacter(string name, TRaceType raceId, TClassType classId)
		{
			Name = name ?? throw new ArgumentNullException(nameof(name));
			RaceId = raceId ?? throw new ArgumentNullException(nameof(raceId));
			ClassId = classId ?? throw new ArgumentNullException(nameof(classId));

			//Empty progress with defaults.
			Progress = new DBRPGCharacterProgress();
		}
	}
}
