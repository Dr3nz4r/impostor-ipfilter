using System.Collections.Generic;

namespace IPFilter
{
    class IPFilterSettings
    {
        public bool AllowListEnabled { get; set; }
        public bool BlockListEnabled { get; set; }
        public List<string> Allowed { get; set; }
        public List<string> Blocked { get; set; }
        public string BlockedMessage { get; set; }


        public IPFilterSettings() {
            Allowed = new List<string>();
            Blocked = new List<string>();
            BlockedMessage = "You are not allowed to create lobbies.";
        }
    }
}
