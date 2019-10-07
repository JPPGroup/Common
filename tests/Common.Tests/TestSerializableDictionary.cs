using System;
using Jpp.Common;
using NUnit.Framework;

namespace Common.Tests
{
    [TestFixture]
    public class TestSerializableDictionary
    {
        [Test]
        public void IsReadOnly_ShouldBeFalse()
        {
            var value = new SerializableDictionary<string, string>().IsReadOnly;
            Assert.IsFalse(value);
        }

        [Test]
        public void GetSchema_ShouldBeNull()
        {
            var value = new SerializableDictionary<string, string>().GetSchema();
            Assert.IsNull(value);
        }

        [Test]
        public void TryGetValue_ShouldThrowNotImplementedException()
        {
            Assert.Throws<NotImplementedException>(delegate { new SerializableDictionary<string, string>().TryGetValue("", out _); });
        }
    }
}