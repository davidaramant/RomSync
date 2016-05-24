using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        [TestCase("some thing", "something", false)]
        public void ShouldMatchSingleTerm(string filterString, string data, bool shouldMatch)
        {
            Assert.That(Filter.Parse(filterString).Matches(data), Is.EqualTo(shouldMatch));
        }
    }
}
