using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Glader.ASP.RPGCharacter
{
	/// <summary>
	/// Database-based implementation of <see cref="IRPGCharacterEntry"/> <see cref="IRPGCharacterCreationDetails"/>
	/// </summary>
	[Table("characters")]
	public sealed class DBRPGCharacter : IRPGCharacterEntry, IRPGCharacterCreationDetails, ICharacterEntryLinkable
	{
		/// <inheritdoc />
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Key]
		public int Id { get; }

		/// <inheritdoc />
		[Required]
		public string Name { get; private set; }

		/// <summary>
		/// The creation data of the character.
		/// </summary>
		[Required]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public DateTime CreationDate { get; }

		/// <summary>
		/// Last time the character entry was modified.
		/// </summary>
		[Required]
		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime LastModifiedDate { get; }

		[Required]
		[ForeignKey(nameof(Id))]
		public DBRPGCharacterProgress Progress { get; }

		public DBRPGCharacter(string name)
		{
			Name = name ?? throw new ArgumentNullException(nameof(name));
		}
	}
}
