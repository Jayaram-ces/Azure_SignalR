namespace Messages
{
    public class UserMessage : Message
    {
        public string Name { get; set; }

        public UserMessage(string name, string text) : base(text)
        {
            Name = name;
        }
    }
}
