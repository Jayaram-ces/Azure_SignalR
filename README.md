# Azure_SignalR
Azure signalR functions with Xamarin.Forms app


Important points to do when working with signalR 

1) Create a Azure signalR service in azure portal.
2) Create a azure function app in visual studio.
3) Add the below values in local.settings.json with valid endpoint created in Azure signalR service

  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "AzureWebJobsDashboard": "UseDevelopmentStorage=true",
    "AzureSignalRConnectionString": "Endpoint=https://livechatroomdemo.service.signalr.net;AccessKey=eSONdSztq4oqOv17VQYfySyN2ODwn825UAZqrcuXnwc=;Version=1.0;",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet"
  },


4) Publish the app to the azure portal to use serverless configuration.
5) Main point : Update or Add Configuration in Function app in azure port after publish.

Key : AzureSignalRConnectionString

Value : Endpoint=https://livechatroomdemo.service.signalr.net;AccessKey=eSONdSztq4oqOv17VQYfySyN2ODwn825UAZqrcuXnwc=;Version=1.0;
	[ConnetionString created for SignalR service]
