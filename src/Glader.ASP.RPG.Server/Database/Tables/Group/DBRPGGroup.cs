using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Glader.ASP.RPG
{
	//TODO: Create unified group interface for common lib
	[Table("group")]
	public class DBRPGGroup : IRPGDBCreationDetailable
	{
		/// <summary>
		/// Primary unique identifier for the group.
		/// </summary>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; private set; }
		
		/// <summary>
		/// The group's name.
		/// </summary>
		[Required]
		public string Name { get; private set; }

		/// <summary>
		/// The group's comment data.
		/// </summary>
		public string Comment { get; private set; }

		/// <summary>
		/// The creation data of the group.
		/// </summary>
		[Required]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public DateTime CreationDate { get; private set; }

		/// <summary>
		/// Last time the group entry was modified.
		/// </summary>
		[Required]
		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime LastModifiedDate { get; private set; }

		//This prop is populated by EF Core when queried.
		/// <summary>
		/// List of group members.
		/// </summary>
		public List<DBRPGGroupMember> Members { get; set; }

		/// <summary>
		/// Indicates if the group is empty.
		/// (Empty groups shouldn't really exist though).
		/// </summary>
		[NotMapped]
		bool IsEmpty => Members == null || Members.Count == 0;

		public DBRPGGroup(string name, string comment)
		{
			Name = name ?? throw new ArgumentNullException(nameof(name));
			Comment = comment;
		}

		/// <summary>
		/// EF Core Ctor.
		/// </summary>
		public DBRPGGroup()
		{
			
		}
	}
}
