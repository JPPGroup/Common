using System.Collections.Generic;
using System.ComponentModel;
using Jpp.Common;
using NUnit.Framework;

namespace Common.Tests
{
    [TestFixture]
    public class TestBaseNotify
    {
        [Test]
        public void SetField_ShouldSetPropertyValue()
        {
            const string value = "Testing";
            var mock = new MockBaseNotify
            {
                TestProperty = value
            };

            Assert.AreEqual(value, mock.TestProperty);
        }

        [Test]
        public void SetField_ShouldReturnTrue()
        {
            const string value = "Testing";
            var mock = new MockBaseNotify();

            var result = mock.SetField(value);
            Assert.IsTrue(result);
        }

        [Test]
        public void SetField_ShouldReturnFalse()
        {
            const string value = "Testing";
            var mock = new MockBaseNotify
            {
                TestProperty = value
            };

            var result = mock.SetField(value);
            Assert.IsFalse(result);
        }


        [Test]
        public void PropertyChanged_ShouldRaiseEvent()
        {
            var receivedEvents = new List<string>();
            var mock = new MockBaseNotify();

            mock.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                receivedEvents.Add(e.PropertyName);
            };

            mock.TestProperty = "Testing";
            Assert.AreEqual(1, receivedEvents.Count);
            Assert.AreEqual("TestProperty", receivedEvents[0]);
        }

        private class MockBaseNotify : BaseNotify
        {
            public string TestProperty
            {
                get => _testProperty;
                set => SetField(ref _testProperty, value, nameof(TestProperty));
            }
            private string _testProperty;

            public bool SetField(string value)
            {
                return SetField(ref _testProperty, value, nameof(TestProperty));
            }
        }
    }
}