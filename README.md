# impostor-ipfilter
A plugin for the impostor among us server to prevent lobby creation for either blocked IPs or only allow lobbies being created from a specific IP.

Please do feel free to fork, star or contribute! Any additions, issues or PRs are greatly welcome!

# configuration
to configure the allow- and blocklist use the following example config.json


```json
{
  "Server": {
    "PublicIp": "127.0.0.1",
    "PublicPort": 22023,
    "ListenIp": "0.0.0.0",
    "ListenPort": 22023
  },
  "AntiCheat": {
    "BanIpFromGame": true
  },
  "IpFilter": {
	"BlockedMessage": "You are not permitted to create a lobby! Bugger off!",
	"AllowListEnabled": true,
	"BlockListEnabled": true,
	"Allowed": [
		"123.123.123.123",
		"123.123.123.123"
	],
	"Blocked": [
		"123.123.123.123",
		"123.123.123.123"
	]
  }
}
```


# notes
`Allowed` and `Blocked` are lists of strings and accept any sort of IP v4 addresses in the following notation: xxx.xxx.xxx.xxx

if `Allowed` or `Blocked` are missing, they default to false
if `BlockedMessage` is empty or missing it will default to `You are not allowed to create lobbies.`

The IP checked against, will always resolve to your local network gateway if the server runs in the same network,
If you have an idea on how to solve this, feel free to submit a PR or open an issue with some idea.
