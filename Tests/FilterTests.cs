using NUnit.Framework;
using RomSync.Model;

namespace Tests
{
    [TestFixture]
    public sealed class FilterTests
    {
        [TestCase("something", "something", true)]
        [TestCase("   something   ", "something", true)]
        [TestCase("SOMETHING", "something", true)]
        [TestCase("some thing", "something", true)]
        [TestCase("something", "some thing", false)]
        public void ShouldMatchSingleTerm(string filterString, string metadata, bool shouldMatch)
        {
            Assert.That(Filter.Parse(filterString).Matches(metadata), Is.EqualTo(shouldMatch));
        }

        [TestCase("\"red car\"", "red car", true)]
        [TestCase("\"red car\"", "red cars", true)]
        [TestCase("\"red car\"", "red ca rs", false)]
        [TestCase("\"red car\"", "redcarrs", false)]
        public void ShouldQuotedTerm(string filterString, string metadata, bool shouldMatch)
        {
            Assert.That(Filter.Parse(filterString).Matches(metadata), Is.EqualTo(shouldMatch));        }


        [TestCase("red car", "red car", true)]
        [TestCase("red car", "red", false)]
        [TestCase("red car", "car", false)]
        [TestCase("red car", "big red car", true)]
        public void ShouldMatchMultipleTerms(string filterString, string metadata, bool shouldMatch)
        {
            Assert.That(Filter.Parse(filterString).Matches(metadata), Is.EqualTo(shouldMatch));
        }
    }
}
