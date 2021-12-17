using Messages;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LiveChatApp.ViewModels
{
    public class LiveChatRoomPageViewModel : ViewModelBase
    {
        private HubConnection hub;
        private HttpClient client;

        private string serverMessage;
        public string ServerMessage
        {
            get => serverMessage;
            set => SetProperty(ref serverMessage, value);
        }

        private string message;
        public string Message
        {
            get => message;
            set => SetProperty(ref message, value);
        }

        private string name;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
        public ObservableCollection<UserMessage> ChatList { get; private set; } = new ObservableCollection<UserMessage>();

        public DelegateCommand SendCommand { get; }
        public LiveChatRoomPageViewModel(INavigationService navigationService): base(navigationService)
        {
            Title = "Live ChatRoom";
            SendCommand = new DelegateCommand(SendCommandHandler);
        }

        private async void SendCommandHandler()
        {
            var comment = new UserMessage(Name, Message);
            var json = JsonConvert.SerializeObject(comment);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var result = await client.PostAsync("https://chatroomdemo.azurewebsites.net/api/AddMessage", content);

            Message = string.Empty; 
        }

        public async override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            try
            {
                Name = parameters.GetValue<string>("UserName");
                client = new HttpClient();
                var result = await client.GetStringAsync("https://chatroomdemo.azurewebsites.net/api/GetInfo");
                var info = JsonConvert.DeserializeObject<ConnectionInfo>(result);

                var connectionBuilder = new HubConnectionBuilder();
                connectionBuilder.WithUrl(info.Url, (Microsoft.AspNetCore.Http.Connections.Client.HttpConnectionOptions obj) =>
                {
                    obj.AccessTokenProvider = () => Task.Run(() => info.AccessToken);
                });

                hub = connectionBuilder.Build();

                hub.On<object>(nameof(UserMessage), (message) =>
                {
                    var userMessage = JsonConvert.DeserializeObject<UserMessage>(message.ToString());
                    ChatList.Add(userMessage);
                });

                hub.On<object>(nameof(ServerMessage), (message) =>
                {
                    var json = message.ToString();
                    var obj = JsonConvert.DeserializeObject<ServerMessage>(json);

                    ServerMessage = obj.Mesessage;

                });
                await hub.StartAsync();
            }
            catch(Exception ex)
            {
                var message = ex.Message;
            }
        }
    }
}
