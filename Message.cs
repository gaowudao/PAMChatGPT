namespace PAMChatGPT
{
    public class Message
    {
        public MessageFrom MessageFrom { get; set; }
        public string Text { get; set; }
       
    }

    public enum MessageFrom
    {
        User,
        Bot,
        System
    }
}
