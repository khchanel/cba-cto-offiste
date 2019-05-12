using System;
using System.IO;

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

            var cwd = Directory.GetCurrentDirectory();
            Console.WriteLine($">>> Watching file changes on {Path.Join(cwd, ruleManager.RulesConfigFilePath)}");
            Console.WriteLine(">>> Press any key to exit");
            Console.ReadLine();
        }
    }
}
