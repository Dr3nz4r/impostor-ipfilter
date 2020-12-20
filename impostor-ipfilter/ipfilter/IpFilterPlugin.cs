using System;
using System.Threading.Tasks;

using Impostor.Api.Plugins;
using Impostor.Api.Events.Managers;

using Microsoft.Extensions.Logging;
using System.IO;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace IPFilter
{
    [ImpostorPlugin(
        package: "de.dr3n.ipfilter",
        name: "IP Filter",
        author: "Dr3nz4r",
        version: "1.0.0")]
    public class IPFilterPlugin : PluginBase
    {
        readonly ILogger<IPFilterPlugin> _logger;
        readonly IEventManager _eventManager;
        IDisposable _unregister;

        internal static IPFilterSettings ipFilterSettings = new IPFilterSettings();

        public IPFilterPlugin(ILogger<IPFilterPlugin> logger, IEventManager eventManager)
        {
            _logger = logger;
            _eventManager = eventManager;

            //fill ipFilterSettings
            new ConfigurationBuilder()
                .AddJsonFile("config.json", optional: false, reloadOnChange: false)
                .Build()
                .Bind("IPFilter", ipFilterSettings);

            _logger.LogInformation($"IPFilter Plugin status" +
                $"\n\tBlocklist: {(ipFilterSettings.BlockListEnabled ? "on" : "off")}" +
                $"\n\tAllowlist: {(ipFilterSettings.AllowListEnabled ? "on" : "off")}");
        }

        public override ValueTask EnableAsync()
        {
            _unregister = _eventManager.RegisterListener(new GameEventListener(_logger));
            return default;
        }

        public override ValueTask DisableAsync()
        {
            _unregister.Dispose();
            return default;
        }
    }
}
