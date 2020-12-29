using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Glader.ASP.RPGCharacter
{
	/// <summary>
	/// Database table model that represents an ownership relationship
	/// for a character.
	/// </summary>
	[Table("character_ownership")]
	public class DBRPGCharacterOwnership
	{
		/// <summary>
		/// Represents the id of the ownership.
		/// (Ex. the account ID that owns or is linked to the character.
		/// </summary>
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Key]
		public int OwnershipId { get; private set; }

		/// <summary>
		/// The id of the character involved in this ownership relationship.
		/// </summary>
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Required]
		public int CharacterId { get; private set; }

		//This will make it cascade delete if the character is deleted.
		[Required]
		[ForeignKey(nameof(CharacterId))]
		public virtual DBRPGCharacter Character { get; private set; }

		public DBRPGCharacterOwnership(int ownershipId, int characterId)
		{
			OwnershipId = ownershipId;
			CharacterId = characterId;
		}
	}
}
