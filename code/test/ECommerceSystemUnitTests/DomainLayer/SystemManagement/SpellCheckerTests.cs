﻿using NUnit.Framework;
using ECommerceSystem.DomainLayer.SystemManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace ECommerceSystem.DomainLayer.SystemManagement.Tests
{
    [TestFixture()]
    public class SpellCheckerTests
    {
        private SpellChecker _spellChecker;
        public readonly static string AppRoot = Path.GetDirectoryName(Assembly.GetCallingAssembly().Location);
        private readonly string SPELL_CHECK_FILE_NAME = Path.Combine(AppRoot, "spell_checker.bin");
        private readonly string SPELL_CHECK_WORDS_TXT = Path.Combine(AppRoot, "words_alpha.txt");

        [OneTimeSetUp]
        public void setUp()
        {
            _spellChecker = new SpellChecker();
        }

        [Test()]
        public void SpellCheckerTest()
        {
            Assert.True(File.Exists(SPELL_CHECK_WORDS_TXT));
            Assert.True(File.Exists(SPELL_CHECK_FILE_NAME));
            Assert.IsNotEmpty(SpellChecker.Deserialize<HashSet<string>>(File.Open(SPELL_CHECK_FILE_NAME, FileMode.Open)));
        }

        [Test()]
        public void readDictionaryFromTextTest()
        {
            Assert.IsNotEmpty(_spellChecker.readDictionaryFromText());
            Assert.True(File.Exists(SPELL_CHECK_FILE_NAME));
        }

        [Test()]
        public void CorrectTest()
        {
            Assert.True(_spellChecker.Correct("speling").Contains("spelling"));
            Assert.True(_spellChecker.Correct("sucessful").Contains("successful"));
            Assert.True(_spellChecker.Correct("usful").Contains("useful"));
            Assert.True(_spellChecker.Correct("aplpe").Contains("apple"));
            Assert.True(_spellChecker.Correct("varation").Contains("variation"));
            Assert.True(_spellChecker.Correct("prodcut").Contains("product"));
            Assert.True(_spellChecker.Correct("teleivsion").Contains("television"));
            Assert.True(_spellChecker.Correct("hcange").Contains("change"));
            Assert.True(_spellChecker.Correct("Ingnious").Contains("ingenious"));
            Assert.True(_spellChecker.Correct("wednsday").Contains("wednesday"));
            Assert.True(_spellChecker.Correct("MISATKE").Contains("mistake"));
            Assert.True(_spellChecker.Correct("MISIGN").Contains("missing"));
            Assert.True(_spellChecker.Correct("mIxdE").Contains("mixed"));
        }
    }
}