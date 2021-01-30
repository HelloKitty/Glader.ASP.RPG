using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Glader.ASP.RPG
{
	[Table("character_skill_known")]
	public class DBRPGCharacterSkillKnown<TSkillType>
		where TSkillType : Enum
	{
		/// <summary>
		/// The character id.
		/// </summary>
		public int CharacterId { get; private set; }

		[Column("Skill")]
		public TSkillType SkillId { get; private set; }

		[ForeignKey(nameof(SkillId))]
		public DBRPGSkill<TSkillType> Skill { get; private set; }

		/// <summary>
		/// The creation data of the character.
		/// </summary>
		[Required]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public DateTime CreationDate { get; private set; }

		public DBRPGCharacterSkillKnown(int characterId, TSkillType skillId)
		{
			if (characterId <= 0) throw new ArgumentOutOfRangeException(nameof(characterId));

			CharacterId = characterId;
			SkillId = skillId ?? throw new ArgumentNullException(nameof(skillId));
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public DBRPGCharacterSkillKnown()
		{
			
		}
	}
}
