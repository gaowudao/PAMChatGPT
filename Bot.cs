using OpenAI_API;
using OpenAI_API.Chat;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PAMChatGPT
{
    public class  Bot
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
                chat.AppendUserInput(text);
                string response = await chat.GetResponseFromChatbotAsync();
                //await foreach (var res in chat.StreamResponseEnumerableFromChatbotAsync())
                //{
                //    Console.Write(res);
                //}


                Messages.Add(new Message
                {
                    MessageFrom = MessageFrom.User,
                    Text = text,
                });

                Messages.Add(new Message
                {
                    MessageFrom = MessageFrom.Bot,
                    Text = response,
                });
            }
            catch (Exception ex)
            {
                Messages.Add(new Message
                {
                    MessageFrom = MessageFrom.System,
                    Text = ex.Message,
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
