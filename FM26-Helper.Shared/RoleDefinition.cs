using System.Collections.Generic;

namespace FM26_Helper.Shared
{
    public class RoleDefinition
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Phase { get; set; } // "InPossession" or "OutPossession"
        public Dictionary<string, double> Weights { get; set; } = new();
    }
}
