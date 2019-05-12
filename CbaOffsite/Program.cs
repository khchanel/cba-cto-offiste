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
            //ruleManager.WatchRulesFile();
            ruleManager.Execute();
        }
    }
}
