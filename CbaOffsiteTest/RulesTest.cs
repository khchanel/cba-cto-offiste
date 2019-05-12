using System.IO;
using System.Linq;
using CbaOffsite;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CbaOffsiteTest
{
    [TestClass]
    public class RulesTest
    {
        private string TestData { get; set; }
        private RuleManager _ruleManager;

        [TestInitialize]
        public void Init()
        {
            _ruleManager = new RuleManager
            {
                RulesConfigFilePath = @"Rules.xml"
            };
            _ruleManager.Load();
            TestData = File.ReadAllText(@"TestInput.txt");
        }

        [TestMethod]
        public void Rule1AvgLengthOfWordStartWithA()
        {
            const string expected = "7.67"; // apple app airplane assistant assistant aerodynamics; avg = 7.666...
            var rule = _ruleManager.Rules.First(x => x.Name == "Counting rule 1");

            var result = _ruleManager.Run(rule, TestData);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Rule2CountEInWordsStartingWithB()
        {
            const string expected = "3"; // "BeE band bend" there are 3 e
            var rule = _ruleManager.Rules.First(x => x.Name == "Counting rule 2");

            var result = _ruleManager.Run(rule, TestData);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Rule3LongestWordsStartingWithAbc()
        {
            const string expected = "12"; // aerodynamics is the longest word
            var rule = _ruleManager.Rules.First(x => x.Name == "Counting rule 3");

            var result = _ruleManager.Run(rule, TestData);
            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        public void Rule4CountOfSequenceOfWordsStartingWithCAndA()
        {
            const string expected = "3";
            var rule = _ruleManager.Rules.First(x => x.Name == "Counting rule 4");

            var result = _ruleManager.Run(rule, TestData);
            Assert.AreEqual(expected, result);
        }
    }
}
