using System.Windows;

namespace Workshop.UIMessagesModule.Services
{
    public class UIMessagesService
    {
        public void ShowMessage(string message)
        {
            // Show a message
            MessageBox.Show(message);
        }
    }
}
