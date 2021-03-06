﻿using System;
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
	public class DBRPGCharacter : IRPGCharacterEntry, IRPGDBCreationDetailable
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

		[InverseProperty(nameof(DBRPGCharacterProgress.Character))]
		public virtual DBRPGCharacterProgress Progress { get; private set; }

		public DBRPGCharacter(string name)
		{
			Name = name ?? throw new ArgumentNullException(nameof(name));

			//Empty progress with defaults.
			Progress = new DBRPGCharacterProgress();
		}
	}
}
