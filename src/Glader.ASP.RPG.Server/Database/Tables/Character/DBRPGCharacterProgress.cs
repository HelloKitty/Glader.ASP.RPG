using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Glader.ASP.RPG
{
	[Table("character_progress")]
	public class DBRPGCharacterProgress : IRPGCharacterProgress, ICharacterEntryLinkable
	{
		/// <inheritdoc />
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; private set; }

		//We use this property attribute this way because of this: https://stackoverflow.com/questions/50575259/asp-net-core-2-1-crashing-due-to-model-inverseproperty
		[Required]
		[ForeignKey(nameof(Id))]
		public virtual DBRPGCharacter Character { get; private set; }

		/// <inheritdoc />
		[Required]
		public int Experience { get; private set; }

		/// <inheritdoc />
		[Required]
		public int Level { get; private set; }

		//TODO: This only supports up to 800-ish hours!! Need to fix DB type.
		/// <inheritdoc />
		[Required]
		public TimeSpan PlayTime { get; private set; } = TimeSpan.Zero;

		/// <summary>
		/// Last time the character progress was modified.
		/// </summary>
		[Required]
		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime LastModifiedDate { get; private set; }

		public DBRPGCharacterProgress(int experience, int level, TimeSpan playTime)
		{
			Experience = experience;
			Level = level;
			PlayTime = playTime;
		}

		public DBRPGCharacterProgress(int experience, int level)
		{
			Experience = experience;
			Level = level;
		}

		/// <summary>
		/// Empty progress.
		/// </summary>
		public DBRPGCharacterProgress()
		{
			
		}
	}
}
