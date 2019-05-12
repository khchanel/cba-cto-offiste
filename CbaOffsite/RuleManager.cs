using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using CbaOffsite.Rules;

namespace CbaOffsite
{
    public class RuleManager
    {
        private FileSystemWatcher _watcher;


        public RuleManager()
        {
            Rules = new List<Rule>();
        }

        public string RulesConfigFilePath { get; set; }
        public List<Rule> Rules { get; private set; }
        public string InputFile { get; set; }


        /// <summary>
        ///     Execute once and start rules file watcher
        /// </summary>
        public void Execute()
        {
            var text = File.ReadAllText(InputFile);
            foreach (var rule in Rules)
            {
                var result = Run(rule, text);
                Console.WriteLine($"{rule.Name} result = {result} saving to '{rule.Output}'");
                WriteResult(rule.Output, result);
            }
        }


        /// <summary>
        ///     Attach file watcher to monitor rules config change and reload
        /// </summary>
        public void WatchRulesFile()
        {
            _watcher = new FileSystemWatcher
            {
                Path = Directory.GetCurrentDirectory(),
                Filter = Path.GetFileName(RulesConfigFilePath),
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite,
                EnableRaisingEvents = true
            };
            _watcher.Changed += OnRulesChanged;
        }


        /// <summary>
        ///     Execute rule against input
        /// </summary>
        /// <param name="rule"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public string Run(Rule rule, string text)
        {
            var regex = new Regex(rule.Pattern, RegexOptions.Compiled);
            var matches = regex.Matches(text);

            // if there is no match, return empty
            if (!matches.Any()) return null;

            switch (rule.Mode)
            {
                // rule 1
                case Mode.AverageLength:
                    return matches.Average(x => x.Length).ToString("#.##");
                // rule 2
                case Mode.CountEInB:
                    return string.Join("", matches).Count(x => x == 'e' || x == 'E').ToString();
                // rule 3
                case Mode.LongestLength:
                    return matches.Max(x => x.Length).ToString();
                //rule 4
                case Mode.Count:
                    return matches.Count.ToString();
                default:
                    throw new Exception("the given Counting mode is not implemented");
            }
        }


        /// <summary>
        ///     Load new rules from config
        ///     keep existing rules if failed
        /// </summary>
        public void Load()
        {
            try
            {
                // parse rules from file
                using (var xmlFile = new XmlTextReader(RulesConfigFilePath))
                {
                    var xmlSerializer = new XmlSerializer(typeof(List<Rule>), new XmlRootAttribute {ElementName = "Rules"});
                    var newRules = (List<Rule>) xmlSerializer.Deserialize(xmlFile);

                    Rules = newRules;
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error while loading rules file. Keeping existing rules.");
                Console.Error.WriteLine(ex.ToString());
            }
        }

        private void OnRulesChanged(object sender, FileSystemEventArgs fileSystemEventArgs)
        {
            // work around Windows firing  changed event twice 
            // https://stackoverflow.com/questions/1764809/filesystemwatcher-changed-event-is-raised-twice
            try
            {
                _watcher.EnableRaisingEvents = false;

                Load();
                Execute();
            }
            finally
            {
                _watcher.EnableRaisingEvents = true;
            }
        }

        private static void WriteResult(string outputFilePath, string content)
        {
            File.WriteAllText(outputFilePath, content);
        }
    }
}
