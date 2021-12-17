using Messages;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace LiveChatApp.ViewModels
{
    public class ServerPageViewModel : ViewModelBase
    {

        private string message;
        public string Message
        {
            get => message;
            set => SetProperty(ref message, value);
        }

        public DelegateCommand SendCommand { get; }
        public ServerPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Live ChatRoom";
            SendCommand = new DelegateCommand(SendCommandHandler);
        }

        private async void SendCommandHandler()
        {
            HttpClient client = new HttpClient();

            var comment = new UserMessage("Server", Message);
            var json = JsonConvert.SerializeObject(comment);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            
            var result = await client.PostAsync("https://chatroomdemo.azurewebsites.net/api/AddMessage", content);

            Message = string.Empty;
        }
    }
}
