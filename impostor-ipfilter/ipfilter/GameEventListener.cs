using Impostor.Api.Events;
using Impostor.Api.Innersloth;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IpFilter
{
    public class GameEventListener : IEventListener
    {
        readonly ILogger<IpFilterPlugin> _logger;
        private IpFilterSettings IpFilterSettings => IpFilterPlugin.ipFilterSettings;

        public GameEventListener(ILogger<IpFilterPlugin> logger) { 
            _logger = logger;
        }

        private bool IsClientBlockedFromCreatingLobbies(string clientIp) {
            return (IpFilterSettings.BlockListEnabled && IpFilterSettings.Blocked.Contains(clientIp))
                || (IpFilterSettings.AllowListEnabled && IpFilterSettings.Allowed.Contains(clientIp) == false);
        }

        [EventListener]
        public void OnGameCreated(IGameCreatedEvent e)
        {
            Task.Run(async () =>
            {
                await Task.Delay(1000);
                var clientIp = e.Game.Host.Client.Connection.EndPoint.Address.ToString();
                if (IsClientBlockedFromCreatingLobbies(clientIp)) {
                    _logger.LogInformation($"Player {e.Game.Host.Character.PlayerInfo.PlayerName} with IP: {e.Game.Host.Client.Connection.EndPoint.Address} triedto create a game lobby");
                    await e.Game.Host.Client.DisconnectAsync(DisconnectReason.Custom, IpFilterSettings.BlockedMessage);
                }
            });
        }
    }
}
