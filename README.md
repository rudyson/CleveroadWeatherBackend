# 🌩️ OpenWeather backend

## Summary
Provides weather forecast in specific city:
- current weather;
- 5 day forecast.

## 🗒️ Configuration
Create `appsettings.Development.json` file using template:
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "OpenWeatherMap":{
    "ApiKey": "yourapikey12345",
    "Language": "en",
    "Units": "metric"
  },
  "AllowedHosts": "*"
}
```

## ▶️ Launching
### Via Dotnet-CLI
1. Type `dotnet run` in the project's directory
2. Wait until application starts
3. Find `info: Microsoft.Hosting.Lifetime` "Now listening on" message in launching logs
4. For example, it will be `http://localhost:80` or `https://localhost:443`. So this is your current API path.
5. Test in using `/swagger/index.html` route.
### Via Visual Studio or Jetbrains Rider
1. Launch IDE
2. Open project
3. Click on Run (F5 or Ctrl+F5)
4. Swagger will be opened automatically
