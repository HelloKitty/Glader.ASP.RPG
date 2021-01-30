using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Glader.ASP.RPG
{
	[Table("character_skill_level")]
	public class DBRPGCharacterSkillLevel<TSkillType>
		where TSkillType : Enum
	{
		/// <summary>
		/// The character id.
		/// </summary>
		public int CharacterId { get; private set; }

		[Column("Skill")]
		public TSkillType SkillId { get; private set; }

		//Foreign key composite key: https://stackoverflow.com/questions/5436731/composite-key-as-foreign-key
		/// <summary>
		/// Navigation property to the linked known skill.
		/// </summary>
		[Required]
		[ForeignKey(nameof(CharacterId) + "," + nameof(SkillId))]
		public virtual DBRPGCharacterSkillKnown<TSkillType> KnownSkill { get; private set; }

		[Required]
		[ForeignKey(nameof(SkillId))]
		public DBRPGSkill<TSkillType> Skill { get; private set; }

		/// <summary>
		/// The level of the skill.
		/// </summary>
		public int Level { get; set; }

		/// <summary>
		/// The total experience of the skill.
		/// </summary>
		public int Experience { get; set; }

		/// <summary>
		/// Last time the character skill level was modified.
		/// </summary>
		[Required]
		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime LastModifiedDate { get; private set; }

		public DBRPGCharacterSkillLevel(int characterId, TSkillType skillId)
		{
			if (characterId <= 0) throw new ArgumentOutOfRangeException(nameof(characterId));

			CharacterId = characterId;
			SkillId = skillId ?? throw new ArgumentNullException(nameof(skillId));
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public DBRPGCharacterSkillLevel()
		{
			
		}
	}
}
