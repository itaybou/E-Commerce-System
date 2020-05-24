namespace PresentationLayer.Models
{
    public class ActionMessageModel
    {
        public string Message { get; set; }
        public string RedirectPath { get; set; }

        public ActionMessageModel(string message, string redirectPath)
        {
            Message = message;
            RedirectPath = redirectPath;
        }
    }
}