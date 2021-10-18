using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;
using Glader.Essentials;

namespace Glader.ASP.RPG
{
	/// <summary>
	/// Table for RPG Map definitions.
	/// </summary>
	[DataContract]
	[Table("map")]
	public class DBRPGMap : IModelDescriptable, IContentPathable
	{
		/// <summary>
		/// Primary map identifier.
		/// </summary>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[DataMember(Order = 1)]
		public int MapId { get; private set; }

		/// <inheritdoc />
		[DataMember(Order = 2)]
		public string ContentPath { get; private set; }

		/// <summary>
		/// The entry point to the map definition.
		/// (Ex. instance entry? Default spawn position?)
		/// </summary>
		[DataMember(Order = 3)]
		public Vector3<float> EntryPoint { get; private set; }

		/// <inheritdoc />
		[DataMember(Order = 4)]
		public string VisualName { get; private set; }

		/// <inheritdoc />
		[DataMember(Order = 5)]
		public string Description { get; private set; }

		public DBRPGMap(string contentPath, Vector3<float> entryPoint, string visualName, string description)
		{
			ContentPath = contentPath ?? throw new ArgumentNullException(nameof(contentPath));
			EntryPoint = entryPoint ?? throw new ArgumentNullException(nameof(entryPoint));
			VisualName = visualName ?? throw new ArgumentNullException(nameof(visualName));
			Description = description ?? throw new ArgumentNullException(nameof(description));
		}

		public DBRPGMap()
		{
			
		}
	}
}
