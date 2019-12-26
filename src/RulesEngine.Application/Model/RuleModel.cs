using System.Collections.Generic;

namespace Hein.RulesEngine.Application.Model
{
    public class RuleModel
    {
        public string Rule { get; set; }
        public Dictionary<string, object> Values { get; set; }
    }
}
