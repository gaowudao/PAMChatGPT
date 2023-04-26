using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;
using System.Collections.ObjectModel;

namespace PAMChatGPT
{
    public class ChatGPTDockpaneViewModel : DockPane
    {
       
        private const string DockPaneId = "Dockpane_ChatGPTDockpane";

        private readonly Bot _bot;

        private string _inputText;
        public string InputText
        {
            get => _inputText;
            set
            {
                _inputText = value;
                
                SendMessageCommand.RaiseCanExecuteChanged();
                
            }
        }

        private bool _isSendingMessage;

        private bool IsSendingMessage
        {
            get => _isSendingMessage;
            set
            {
                _isSendingMessage = value;
                
                SendMessageCommand.RaiseCanExecuteChanged();
                
            }
        }

        private RelayCommand _sendMessageCommand;
        public RelayCommand SendMessageCommand => _sendMessageCommand ??
            (_sendMessageCommand = new RelayCommand(SendMessageExecute, SendMessageCanExecute));

        public ReadOnlyObservableCollection<Message> Messages { get; }

        private async void SendMessageExecute()
        {
            IsSendingMessage = true;
            try
            {
                await _bot.SendActivityAsync(InputText);
                InputText = "";
            }
            finally
            {
                IsSendingMessage = false;
            }
        }

        private bool SendMessageCanExecute() => !string.IsNullOrWhiteSpace(InputText) && !IsSendingMessage;

        //public void Closed()
        //{
        //    _bot.Stop();
        //}

        public ChatGPTDockpaneViewModel()
        {
            _bot = new Bot();
            Messages = new ReadOnlyObservableCollection<Message>(_bot.Messages);
        }

       


        #region Show dockpane 
        /// <summary>
        /// Show the DockPane.
        /// </summary>
        internal static void Show()
        {
            var pane = FrameworkApplication.DockPaneManager.Find(DockPaneId);
            pane?.Activate();
        }


        #endregion Show dockpane 



        

       
    }

    /// <summary>
    /// Button implementation to show the DockPane.
    /// </summary>
    internal class ChatGPTDockpane_ShowButton : Button
    {
        protected override void OnClick()
        {
            ChatGPTDockpaneViewModel.Show();
        }
    }
}