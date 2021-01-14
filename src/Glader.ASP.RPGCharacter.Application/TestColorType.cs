using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Glader.ASP.RPGCharacter
{
	public class TestColorType
	{
		public int R { get; private set; }

		public int G { get; private set; }

		public int B { get; private set; }

		public TestColorType(int r, int g, int b)
		{
			R = r;
			G = g;
			B = b;
		}

		public TestColorType()
		{
			
		}
	}
}
