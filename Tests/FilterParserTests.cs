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
    public sealed class FilterParserTests
    {
        [Test]
        public void ShouldLowerCaseInput()
        {
            var output = FilterParser.Parse("SomeTHING");

            Assert.That(output.ToArray(), Is.EqualTo(new[] { "something"}));
        }

        [Test]
        public void ShouldSplitOnWhitespace()
        {
            var output = FilterParser.Parse("  Here Is Some Stuff");

            Assert.That(output.ToArray(), Is.EqualTo(new[] {"here","is","some","stuff"}));
        }
    }
}
