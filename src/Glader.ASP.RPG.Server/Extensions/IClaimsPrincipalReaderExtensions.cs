using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Glader.Essentials;

namespace Glader.ASP.RPG
{
	public static class IClaimsPrincipalReaderExtensions
	{
		/// <summary>
		/// Returns the User's character id.
		/// </summary>
		/// <param name="reader"></param>
		/// <param name="principal">The <see cref="ClaimsPrincipal"/> instance.</param>
		/// <returns>The User ID claim value. Will throw if the principal doesn't contain the id.</returns>
		/// <exception cref="ArgumentException">Throws if the provided principal doesn't contain an id.</exception>
		/// <remarks>The User ID's character id.</remarks>
		public static int GetCharacterId(this IClaimsPrincipalReader reader, ClaimsPrincipal principal)
		{
			if (reader == null) throw new ArgumentNullException(nameof(reader));
			if (principal == null) throw new ArgumentNullException(nameof(principal));

			return reader.GetSubAccountId<int>(principal);
		}
	}
}
