using System;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.Regions;

namespace ModuleA.Views
{

    [Export(ViewNames.ModuleAView)]
    public partial class ModuleAView : UserControl, IConfirmNavigationRequest
    {
        private InteractionRequest<Confirmation> navigationInteractionRequest;

        public IInteractionRequest NavigationInteractionRequest
        {
            get
            {
                return this.navigationInteractionRequest;
            }
        }
        
        public ModuleAView()
        {
            this.navigationInteractionRequest = new InteractionRequest<Confirmation>();
            this.DataContext = this.NavigationInteractionRequest;
            InitializeComponent();
        }


        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            //throw new NotImplementedException();
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            //throw new NotImplementedException();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            string previousPage = navigationContext.Parameters["previous"];
            if (!String.IsNullOrEmpty(previousPage))
            {
                this.txtNavigatedFrom.Text = string.Format("Navigating from: {0}", previousPage.Split('?')[0]);
            }
        }

        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            string targetPage = navigationContext.Uri.ToString().Split('?')[0];
            if (!String.IsNullOrEmpty(targetPage) && !targetPage.Contains("ModuleAView"))
            {
                this.navigationInteractionRequest.Raise
                (
                    new Confirmation()
                    {
                        Content = "Are you sure you want to Navigate away from ModuleAView?",
                        Title = "Confirm Navigation"
                    },
                    c =>
                    {
                        continuationCallback(c.Confirmed);
                    }
                );
            }
            else 
            {
                continuationCallback(true);
            }
        }
    }
}
