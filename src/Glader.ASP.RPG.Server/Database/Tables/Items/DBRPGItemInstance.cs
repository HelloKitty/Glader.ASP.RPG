using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Glader.ASP.RPG
{
	/// <summary>
	/// Database-based implementation of item instances <see cref="IRPGDBCreationDetailable"/>
	/// </summary>
	[Table("item_instance")]
	public class DBRPGItemInstance<TItemClassType, TQualityType, TQualityColorStructureType> : IRPGDBCreationDetailable, IRPGItemInstance
		where TItemClassType : Enum 
		where TQualityType : Enum
	{
		/// <summary>
		/// The unique identifier for the item.
		/// </summary>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]

		[Required]
		public int TemplateId { get; private set; }

		[ForeignKey(nameof(TemplateId))]
		public virtual DBRPGItemTemplate<TItemClassType, TQualityType, TQualityColorStructureType> Template { get; private set; }
		public int Id { get; private set; }

		/// <summary>
		/// The creation data of the character.
		/// </summary>
		[Required]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public DateTime CreationDate { get; private set; }

		public DBRPGItemInstance(int templateId)
		{
			if (templateId <= 0) throw new ArgumentOutOfRangeException(nameof(templateId));
			TemplateId = templateId;
		}

		/// <summary>
		/// EF Serializer
		/// </summary>
		public DBRPGItemInstance()
		{
			
		}
	}
}
