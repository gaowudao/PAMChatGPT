using OpenAI_API;
using OpenAI_API.Chat;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PAMChatGPT
{
    public class Bot
    {
        
        private readonly OpenAIAPI api;
        private Conversation chat = null;

        public ObservableCollection<Message> Messages { get; } = new ObservableCollection<Message>();

        public Bot() 
        {

            api = new OpenAIAPI(""); // se here your key chatgpt
            
        }

       

        public async Task SendActivityAsync(string text)
        {
            try
            {
                CreateConversationIfNotExistAsync();

                Messages.Add(new Message
                {
                    MessageFrom = MessageFrom.Bot,
                    Text = text,
                });

                chat.AppendUserInput(text);
                string response = await chat.GetResponseFromChatbotAsync();

                Message message = new Message();
                message.MessageFrom = MessageFrom.User;
                message.Text = response;
                Messages.Add(message);
                //await foreach (var res in chat.StreamResponseEnumerableFromChatbotAsync())
                //{
                //    message.Text += res;
                //}

            }
            catch (Exception ex)
            {
                Messages.Add(new Message
                {
                    MessageFrom = MessageFrom.System,
                    Text = ex.Message
                });
            }

        }

        private void CreateConversationIfNotExistAsync()
        {
            if (chat != null)
            {
                return;
            }

            chat = api.Chat.CreateConversation();
        }

        

        
    }
}
