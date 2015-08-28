using NUnit.Framework;
using RagingRudolf.UCommerce.CodeFirst.Core.Extensions;

namespace RagingRudolf.UCommerce.CodeFirst.Tests.Core.Extensions
{
	[TestFixture]
	public class StringExtensionsTester
	{
		[Test]
		public void IsEmpty_StringIsNull_DoesNotThrowException()
		{
			string str = null;

			Assert.DoesNotThrow(() => str.IsEmpty());
		}
		
		[Test]
		public void IsEmpty_StringIsNull_ReturnsTrue()
		{
			string str = null;

			Assert.That(str.IsEmpty(), Is.True);
		}

		[Test]
		public void IsEmpty_StringIsStringEmpty_ReturnsTrue()
		{
			string str = string.Empty;

			Assert.That(str.IsEmpty(), Is.True);
		}

		[Test]
		public void IsEmpty_StringIsWhitespace_ReturnsTrue()
		{
			string str = "		  ";

			Assert.That(str.IsEmpty(), Is.True);
		}

		[Test]
		public void IsEmpty_StringHasValue_ReturnsFalse()
		{
			string str = " test string!";

			Assert.That(str.IsEmpty(), Is.False);
		}

		[Test]
		public void IsNotEmpty_StringIsNull_DoesNotThrowException()
		{
			string str = null;

			Assert.DoesNotThrow(() => str.IsNotEmpty());
		}

		[Test]
		public void IsNotEmpty_StringIsNull_ReturnsFalse()
		{
			string str = null;

			Assert.That(str.IsNotEmpty(), Is.False);
		}

		[Test]
		public void IsNotEmpty_StringIsStringEmpty_ReturnsFalse()
		{
			string str = string.Empty;

			Assert.That(str.IsNotEmpty(), Is.False);
		}

		[Test]
		public void IsNotEmpty_StringIsWhitespace_ReturnsFalse()
		{
			string str = "		  ";

			Assert.That(str.IsNotEmpty(), Is.False);
		}

		[Test]
		public void IsNotEmpty_StringHasValue_ReturnsTrue()
		{
			string str = " test string!";

			Assert.That(str.IsNotEmpty(), Is.True);
		}
	}
}