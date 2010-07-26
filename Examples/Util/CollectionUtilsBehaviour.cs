#region

using System.Collections.Generic;
using NUnit.Framework;
using WiPFlash.Util;

#endregion

namespace WiPFlash.Examples.Util
{
    [TestFixture]
    public class CollectionUtilsBehaviour
    {
        [Test]
        public void ShouldConvertACollectionIntoAListOfOtherTypes()
        {
            var expected = new List<char> {'h', 'd', 'p', 'd', 'g'};
            var actual = CollectionUtils.Convert(new[] {"horse", "dog", "pig", "duck", "goat"}, s => ((string)s).ToCharArray()[0]);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ShouldAllowTheIndexMatchingAPredicateToBeFoundOrReturnMinusOneIfNot()
        {
            var animals = new []{"horse", "dog", "pig", "duck", "goat"};
            Assert.AreEqual(3, CollectionUtils.IndexOf(animals, s => s.Equals("duck")));
            Assert.AreEqual(-1, CollectionUtils.IndexOf(animals, s => s.Equals("giraffe")));
        }
    }
}