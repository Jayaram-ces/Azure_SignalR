using LiveChatApp.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiveChatApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public DelegateCommand StartLiveChatCommand { get; set; }
        public DelegateCommand ServerCommand { get; set; }
        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Main Page";
            _navigationService = navigationService;
            StartLiveChatCommand = new DelegateCommand(StartLiveChatCommandHandler);
            ServerCommand = new DelegateCommand(ServerCommandHandler);
        }

        private string name;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }


        public async void StartLiveChatCommandHandler()
        {
            await _navigationService.NavigateAsync($"{nameof(LiveChatRoomPage)}", new NavigationParameters
            {
                {"UserName", Name }
            });
        }

        public async void ServerCommandHandler()
        {
            await _navigationService.NavigateAsync($"{nameof(ServerPage)}");
        }
    }
}
