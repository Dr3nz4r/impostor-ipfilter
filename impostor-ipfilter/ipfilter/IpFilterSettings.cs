using System.Collections.Generic;

namespace IpFilter
{
    class IpFilterSettings
    {
        public bool AllowListEnabled { get; set; }
        public bool BlockListEnabled { get; set; }
        public List<string> Allowed { get; set; }
        public List<string> Blocked { get; set; }
        public string BlockedMessage { get; set; }


        public IpFilterSettings() {
            Allowed = new List<string>();
            Blocked = new List<string>();
            BlockedMessage = "You are not allowed to create lobbies.";
        }
    }
}
