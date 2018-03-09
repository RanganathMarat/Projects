using System.Windows;
using Workshop.Infrastructure;

namespace Workshop.UIMessagesModule.Services
{
    public class UIMessagesService : IUIMessagesService
    {
        public void ShowMessage(string message)
        {
            // Show a message
            MessageBox.Show(message);
        }        
    }
}
