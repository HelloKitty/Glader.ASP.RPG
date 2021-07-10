using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace Glader.ASP.RPG
{
	/// <summary>
	/// Table for RPG Quality.
	/// (Ex. Maybe Item quality)
	/// </summary>
	/// <typeparam name="TQualityType">The quality type.</typeparam>
	/// <typeparam name="TQualityColorStructureType">The structure that represents how quality colors are defined.</typeparam>
	[DataContract]
	[Table("quality")]
	public class DBRPGQuality<TQualityType, TQualityColorStructureType> : IModelDescriptable
		where TQualityType : Enum
	{
		/// <summary>
		/// The Quality identifier.
		/// </summary>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[DataMember(Order = 1)]
		public TQualityType Id { get; private set; }

		/// <summary>
		/// The visual human-readable name for the quality.
		/// </summary>
		[DataMember(Order = 2)]
		public string VisualName { get; private set; }

		/// <summary>
		/// The description of the quality.
		/// </summary>
		[DataMember(Order = 3)]
		public string Description { get; private set; }

		/// <summary>
		/// The proportion data for the slot.
		/// </summary>
		[DataMember(Order = 4)]
		public TQualityColorStructureType Color { get; private set; }

		public DBRPGQuality(TQualityType qualityType, string visualName, string description)
		{
			Id = qualityType ?? throw new ArgumentNullException(nameof(qualityType));
			VisualName = visualName;
			Description = description;
		}

		public DBRPGQuality(TQualityType qualityType)
		{
			Id = qualityType ?? throw new ArgumentNullException(nameof(qualityType));
			VisualName = String.Empty;
			Description = String.Empty;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public DBRPGQuality()
		{

		}
	}
}
