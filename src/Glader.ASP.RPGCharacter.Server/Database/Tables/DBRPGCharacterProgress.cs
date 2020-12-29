using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Glader.ASP.RPGCharacter
{
	[Table("character_progress")]
	public sealed class DBRPGCharacterProgress : IRPGCharacterProgress, ICharacterEntryLinkable
	{
		/// <inheritdoc />
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; private set; }

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
