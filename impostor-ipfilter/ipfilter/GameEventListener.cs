﻿using Impostor.Api.Events;
using Impostor.Api.Innersloth;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IPFilter
{
    public class GameEventListener : IEventListener
    {
        readonly ILogger<IPFilterPlugin> _logger;
        private IPFilterSettings IpFilterSettings => IPFilterPlugin.ipFilterSettings;

        public GameEventListener(ILogger<IPFilterPlugin> logger) { 
            _logger = logger;
        }

        private bool IsClientBlockedFromCreatingLobbies(string clientIp) {
            if (IpFilterSettings.BlockListEnabled && IpFilterSettings.Blocked.Contains(clientIp)) {
                return true;
            }

            if (IpFilterSettings.AllowListEnabled && IpFilterSettings.Allowed.Contains(clientIp) == false) {
                return true;
            }

            return false;
        }

        [EventListener]
        public void OnGameCreated(IGameCreatedEvent e)
        {
            Task.Run(async () =>
            {
                await Task.Delay(2500);
                var clientIp = e.Game.Host.Client.Connection.EndPoint.Address.ToString();
                if (IsClientBlockedFromCreatingLobbies(clientIp)) {
                    _logger.LogInformation($"Player {e.Game.Host.Character.PlayerInfo.PlayerName} with IP: {e.Game.Host.Client.Connection.EndPoint.Address} tried to create a game lobby");
                    await e.Game.Host.Client.DisconnectAsync(DisconnectReason.Custom, IpFilterSettings.BlockedMessage);
                }
            });
        }
    }
}
