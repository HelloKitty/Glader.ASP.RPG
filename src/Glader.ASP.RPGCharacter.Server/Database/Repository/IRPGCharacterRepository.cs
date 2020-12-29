using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Glader.Essentials;

namespace Glader.ASP.RPGCharacter
{
	/// <summary>
	/// <see cref="IGenericRepositoryCrudable{TKey,TModel}"/> for <see cref="DBRPGCharacter"/>.
	/// </summary>
	public interface IRPGCharacterRepository : IGenericRepositoryCrudable<int, DBRPGCharacter>
	{
		/// <summary>
		/// Retrieves owned characters associated with the <see cref="ownershipId"/>.
		/// </summary>
		/// <param name="ownershipId">Ownership id.</param>
		/// <param name="token">Cancel token.</param>
		/// <returns>Array of all characters linked to the ownership id.</returns>
		Task<DBRPGCharacter[]> RetrieveOwnedCharactersAsync(int ownershipId, CancellationToken token = default);
	}
}
