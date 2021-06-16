using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Glader.ASP.RPG
{
	/// <summary>
	/// Models the types of skills.
	/// </summary>
	/// <typeparam name="TSkillType">The skill type.</typeparam>
	[Table("skill")]
	public class DBRPGSkill<TSkillType> : IModelDescriptable
		where TSkillType : Enum
	{
		/// <summary>
		/// The skill identifier.
		/// </summary>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public TSkillType Id { get; private set; }

		/// <summary>
		/// Indicates if the skill is a passive skill.
		/// (Cannot be leveled)
		/// Concept based on: https://wowwiki.fandom.com/wiki/Passive_skill#:~:text=A%20passive%20skill%2C%20is%20a,increases%20your%20critical%20hit%20chance.
		/// </summary>
		public bool IsPassiveSkill { get; private set; }

		/// <summary>
		/// The visual human-readable name for the slot.
		/// </summary>
		public string VisualName { get; private set; }

		/// <summary>
		/// The description of the slot.
		/// </summary>
		public string Description { get; private set; }

		public DBRPGSkill(TSkillType skill, string visualName, string description)
		{
			Id = skill ?? throw new ArgumentNullException(nameof(skill));
			VisualName = visualName;
			Description = description;
		}

		public DBRPGSkill(TSkillType skill)
		{
			Id = skill ?? throw new ArgumentNullException(nameof(skill));
			VisualName = String.Empty;
			Description = String.Empty;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public DBRPGSkill()
		{
			
		}
	}
}
