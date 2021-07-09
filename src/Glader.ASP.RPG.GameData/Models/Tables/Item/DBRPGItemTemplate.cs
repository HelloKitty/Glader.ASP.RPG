using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;
using GGDBF;

namespace Glader.ASP.RPG
{
	/// <summary>
	/// Table for RPG Item Templates.
	/// </summary>
	/// <typeparam name="TItemClassType">The itemclass type.</typeparam>
	/// <typeparam name="TQualityType">Quality type enum.</typeparam>
	/// <typeparam name="TQualityColorStructureType"></typeparam>
	[DataContract]
	[Table("item_template")]
	public class DBRPGItemTemplate<TItemClassType, TQualityType, TQualityColorStructureType> : IModelDescriptable
		where TItemClassType : Enum 
		where TQualityType : Enum
	{
		/// <summary>
		/// Unique key identifier for the item template.
		/// </summary>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[DataMember(Order = 1)]
		public int Id { get; private set; }

		/// <summary>
		/// The Item Class identifier.
		/// </summary>
		[DataMember(Order = 2)]
		public TItemClassType ClassId { get; private set; }

		[DataMember(Order = 3)]
		public int SubClassId { get; private set; }

		[CompositeKeyHint(nameof(ClassId), nameof(SubClassId))]
		[IgnoreDataMember]
		public virtual DBRPGSItemSubClass<TItemClassType> ItemSubClass { get; private set; }

		/// <summary>
		/// The visual human-readable name for the item class.
		/// </summary>
		[DataMember(Order = 4)]
		public string VisualName { get; private set; }

		/// <summary>
		/// The description of the item class.
		/// </summary>
		[DataMember(Order = 5)]
		public string Description { get; private set; }

		[DataMember(Order = 6)]
		public TQualityType QualityType { get; private set; }

		[IgnoreDataMember]
		[ForeignKey(nameof(QualityType))]
		public virtual DBRPGQuality<TQualityType, TQualityColorStructureType> Quality { get; private set; }

		public DBRPGItemTemplate(TItemClassType classId, int subClassId, string visualName, string description, TQualityType qualityType)
		{
			if (subClassId <= 0) throw new ArgumentOutOfRangeException(nameof(subClassId));
			ClassId = classId ?? throw new ArgumentNullException(nameof(classId));
			SubClassId = subClassId;
			VisualName = visualName ?? throw new ArgumentNullException(nameof(visualName));
			Description = description;
			QualityType = qualityType;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public DBRPGItemTemplate()
		{

		}
	}
}
