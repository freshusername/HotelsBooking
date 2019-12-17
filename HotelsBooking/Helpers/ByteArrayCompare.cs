using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelsBooking.Helpers
{
	public static class ByteArrayCompare
	{
		public static bool Compare(byte[] a1, byte[] a2)
		{
			return StructuralComparisons.StructuralEqualityComparer.Equals(a1, a2);
		}
	}
}
