using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Glader.ASP.RPG
{
	/// <summary>
	/// <see cref="IDBContextAdapter{TDBContextType}"/> for generic context types.
	/// </summary>
	public sealed class GenericCharacterDatabaseContextAdapter<TPublicDBContextType, TPrivateDBContextType> : IDBContextAdapter<TPublicDBContextType> 
		where TPublicDBContextType : DbContext
		where TPrivateDBContextType : TPublicDBContextType
	{
		/// <summary>
		/// Internal implementation type for the context.
		/// </summary>
		private TPrivateDBContextType _Context { get; }

		/// <inheritdoc />
		public TPublicDBContextType Context => _Context;

		public GenericCharacterDatabaseContextAdapter(TPrivateDBContextType context)
		{
			_Context = context ?? throw new ArgumentNullException(nameof(context));
		}
	}
}
