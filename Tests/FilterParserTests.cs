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
            TestParsing(
                "SomeThing", 
                "something");
        }

        [Test]
        public void ShouldSplitOnWhitespace()
        {
            TestParsing(
                "  Here Is Some Stuff",
                "here","is","some","stuff");
        }

        [Test]
        public void ShouldParseQuotedStringIntoSingleToken()
        {
            TestParsing(
                "\"some string\" and more ",
                "some string", "and", "more");
        }

        [Test]
        public void ShouldParseQuotedStringAtEndOfInput()
        {
            TestParsing(
                "at the \"end instead\"",
                "at", "the", "end instead");
        }

        private static void TestParsing(string input, params string[] tokens)
        {
            var output = FilterParser.Parse(input);

            Assert.That(output.ToArray(), Is.EqualTo(tokens));
        }
    }
}
