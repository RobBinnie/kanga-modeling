﻿using System;
using KangaModeling.Compiler.SequenceDiagrams.Parsing;
using NUnit.Framework;
using KangaModeling.Compiler.SequenceDiagrams.Ast;
using KangaModeling.Compiler.Test.SequenceDiagrams.Helper;
using System.Linq;

namespace KangaModeling.Compiler.Test.SequenceDiagrams
{
    [TestFixture]
    public class TitleStatementParserTest
    {
        [TestCase("title", typeof(TitleStatement))]
        [TestCase("title abc", typeof(TitleStatement))]
        public void ParseTest(string input, Type expectedStatementType)
        {
            TitleStatementParser target = new TitleStatementParser();
            var actual = target.Parse(input);
            Assert.IsInstanceOf(expectedStatementType, actual);
        }

        [TestCase("title", "")]
        [TestCase("title abc", "abc")]
        [TestCase("title  ", "")]
        public void EnsureTokens_In_Valid_Statement(string input, string argumentValue)
        {
            TitleStatementParser target = new TitleStatementParser();
            var actual = target.Parse(input);
            var tokenValues = actual.Tokens().Select(token => token.Value);
            var expected = new[] {"title", argumentValue};
            CollectionAssert.AreEquivalent(expected, tokenValues);        
        }
    }
}