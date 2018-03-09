using System.Windows;
using Workshop.Infrastructure.Services;

namespace Prism.Workshop.Services
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
