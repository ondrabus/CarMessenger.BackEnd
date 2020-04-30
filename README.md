This is the back-end implementation of the CarMessenger.app project.

## How it works

This application is the center of the project. It is WebAPI app that handles Twilio webhook notifications (incoming text messages) (TwilioController) as well as website requests (WebController).

To run this app, make sure to add a appsettings.json file with following content:
```js
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*",
  "FaunaDB": {
    "Endpoint": "{endpoint}",
    "Secret": "{secret}"
  },
  "Twilio": {
    "AccountSID": "{account sid}"
  }
}
```

To run the app simply start it from within Visual Studio. The default URL is https://localhost:44316.

The app does not handle page requests. Use Postman or similar tool to try API requests.

## License

[MIT](http://www.opensource.org/licenses/mit-license.html)

## Disclaimer

No warranty expressed or implied. Software is as is.
