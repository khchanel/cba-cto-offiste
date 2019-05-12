using System;

namespace CbaOffsite.Rules
{
    [Serializable]
    public class Rule
    {
        public string Name { get; set; }
        public string Pattern { get; set; }
        public string Output { get; set; }
        public Mode Mode { get; set; }
    }
}
