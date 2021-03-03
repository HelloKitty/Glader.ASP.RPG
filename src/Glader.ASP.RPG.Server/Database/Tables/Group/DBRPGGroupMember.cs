using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Glader.ASP.RPG
{
	//TODO: Create unified group interface for common lib
	[Table("group_member")]
	public class DBRPGGroupMember : IRPGDBCreationDetailable
	{
		/// <summary>
		/// The ID of the group this member is apart of.
		/// </summary>
		public int GroupId { get; private set; }

		[ForeignKey(nameof(GroupId))]
		public virtual DBRPGGroup Group { get; private set; }

		/// <summary>
		/// The character ID of the group members.
		/// This is the unique primary key of the member entry and foreign key to the character.
		/// </summary>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int CharacterId { get; private set; }

		/// <summary>
		/// The character nav property for the linked character.
		/// </summary>
		[ForeignKey(nameof(CharacterId))]
		public DBRPGCharacter Character { get; private set; }

		/// <summary>
		/// The date the player joined the group.
		/// </summary>
		[Required]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public DateTime CreationDate { get; private set; }

		public DBRPGGroupMember(int groupId, int characterId)
		{
			if (groupId <= 0) throw new ArgumentOutOfRangeException(nameof(groupId));
			if (characterId <= 0) throw new ArgumentOutOfRangeException(nameof(characterId));

			GroupId = groupId;
			CharacterId = characterId;
		}

		/// <summary>
		/// EF Core Ctor.
		/// </summary>
		public DBRPGGroupMember()
		{
			
		}
	}
}
