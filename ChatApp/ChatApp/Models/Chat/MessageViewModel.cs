namespace ChatApp.Models.Chat
{
    public class MessageViewModel
    {
        public int Id { get; set; }
        public required string Message {  get; set; }
        public required string Author {  get; set; }
    }
}
