namespace Messages
{
    public class ServerMessage : Message
    {
        public ServerMessage()
        {

        }
        public ServerMessage(string mesessage)
        {
            Mesessage = mesessage;
            TypeInfo = GetType().Name;
        }
        public string Mesessage { get; set; }
    }
}
