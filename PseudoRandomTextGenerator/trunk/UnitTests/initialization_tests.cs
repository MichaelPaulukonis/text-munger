using System;
using System.Linq;
using NUnit.Framework;
using TextTransformer;

namespace UnitTests
{
	[TestFixture]
	public class initialization_tests
	{
		[Test]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void keySize_zero_throws_error()
		{
			var _ = new MarkovGenerator(0);
		}

		[Test]
		public void keySize_one_through_five_are_good()
		{
			foreach (var i in Enumerable.Range(1,5))
			{
				var _ = new MarkovGenerator(i);
			}
		}

		[Test]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void keySize_six_throws_error()
		{
			var _ = new MarkovGenerator(6);
		}
	}
}