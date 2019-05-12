using System;

namespace CbaOffsite
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var ruleManager = new RuleManager
            {
                InputFile = @"TestInput.txt",
                RulesConfigFilePath = @"Rules.xml"
            };

            ruleManager.Load();
            ruleManager.Execute();
            ruleManager.WatchRulesFile();
            Console.WriteLine($">>> Watching on {ruleManager.RulesConfigFilePath}. Press any key to exit");
            Console.ReadLine();
        }
    }
}
