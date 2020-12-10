# impostor-ipfilter
A plugin for the impostor among us server

# configuration
to configure the allow and blocklist use the following snippet and place it in your config.json.

```json
"IpFilter": {
	"BlockedMessage": "You are not allowed to create lobbies.",
	"AllowListEnabled": false,
	"BlockListEnabled": false,
	"Allowed": [],
	"Blocked": []
}
```

# notes
`Allowed` and `Blocked` are lists of strings and accept any sort of IP v4 addresses in the following notation: xxx.xxx.xxx.xxx

if `Allowed` or `Blocked` are missing, they default to false
if `BlockedMessage` is empty or missing it will default to `You are not allowed to create lobbies.`