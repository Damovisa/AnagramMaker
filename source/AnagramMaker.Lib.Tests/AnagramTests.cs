using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AnagramMaker.Lib.Tests
{
    [TestClass]
    public class AnagramTests
    {
        private AnagramCreator _anagramLib;

        [TestInitialize]
        public void Init()
        {
            _anagramLib = new AnagramCreator();
        }

        [TestMethod]
        public void AnagramsForHello_ReturnsCorrectAnagrams()
        {
            var expected = new[]
            {
                "hello",
                "ho ell",
                "ell ho",
                "ell oh",
                "oh ell"
            };

            var anagrams = _anagramLib.GenerateAnagrams("hello");

            anagrams.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void AnagramsForInvalidCharacters_ReturnsEmptyCollection()
        {
            var word = "*#&@";

            var anagrams = _anagramLib.GenerateAnagrams(word);

            anagrams.Should().BeEmpty();
        }

        [TestMethod]
        public void AnagramsForWord_IncludesSameWord()
        {
            var word = "cheese";

            var anagrams = _anagramLib.GenerateAnagrams(word);

            anagrams.Should().Contain(word);
        }
    }
}
