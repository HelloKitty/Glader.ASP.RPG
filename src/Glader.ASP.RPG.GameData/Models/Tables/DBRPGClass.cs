using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace Glader.ASP.RPG
{
	/// <summary>
	/// Table for RPG Class.
	/// </summary>
	/// <typeparam name="TClassType">The class type.</typeparam>
	[DataContract]
	[Table("class")]
	public class DBRPGClass<TClassType> : IModelDescriptable
		where TClassType : Enum
	{
		/// <summary>
		/// The Class identifier.
		/// </summary>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[DataMember(Order = 1)]
		public TClassType Id { get; private set; }

		/// <summary>
		/// The visual human-readable name for the race.
		/// </summary>
		[DataMember(Order = 2)]
		public string VisualName { get; private set; }

		/// <summary>
		/// The description of the race.
		/// </summary>
		[DataMember(Order = 3)]
		public string Description { get; private set; }

		public DBRPGClass(TClassType classType, string visualName, string description)
		{
			Id = classType ?? throw new ArgumentNullException(nameof(classType));
			VisualName = visualName;
			Description = description;
		}

		public DBRPGClass(TClassType classType)
		{
			Id = classType ?? throw new ArgumentNullException(nameof(classType));
			VisualName = String.Empty;
			Description = String.Empty;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public DBRPGClass()
		{

		}
	}
}
