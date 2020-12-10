using System;
using System.Threading.Tasks;

using Impostor.Api.Plugins;
using Impostor.Api.Events.Managers;

using Microsoft.Extensions.Logging;
using System.IO;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace IpFilter
{
    [ImpostorPlugin(
        package: "de.dr3n.ipfilter",
        name: "IP Filter",
        author: "Dr3nz4r",
        version: "1.0.0")]
    public class IpFilterPlugin : PluginBase
    {
        readonly ILogger<IpFilterPlugin> _logger;
        readonly IEventManager _eventManager;
        IDisposable _unregister;

        internal static IpFilterSettings ipFilterSettings = new IpFilterSettings();

        public IpFilterPlugin(ILogger<IpFilterPlugin> logger, IEventManager eventManager)
        {
            _logger = logger;
            _eventManager = eventManager;

            //fill ipFilterSettings
            new ConfigurationBuilder()
                .AddJsonFile("config.json", optional: false, reloadOnChange: false)
                .Build()
                .Bind("IpFilter", ipFilterSettings);

            _logger.LogInformation($"IpFilter Plugin status" +
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
