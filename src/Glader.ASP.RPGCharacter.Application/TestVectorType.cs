using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Glader.ASP.RPG
{
	public class TestVectorType<T>
	{
		public T X { get; private set; }

		public T Y { get; private set; }

		public T Z { get; private set; }

		public TestVectorType(T x, T y, T z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		public TestVectorType()
		{
			
		}
	}
}
