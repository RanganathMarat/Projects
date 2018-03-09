using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Infrastructure;
using Microsoft.Practices.Prism.Commands;
using System.Collections.Specialized;


namespace NavigationJournal
{
    [Export]
    public partial class Shell : UserControl
    {

        private readonly IRegionManager regionManager;
        private bool isMainRegionAvailable = false;

        public DelegateCommand JournalBackCommand { get; set; }
        public DelegateCommand JournalForwardCommand { get; set; }

        private IRegionNavigationService MainRegionNavigationService
        {
            get
            {
                // Retrieve the navigation service associated with the MainRegion
                return this.regionManager.Regions[RegionNames.MainRegion].NavigationService;
            }
        }

        [ImportingConstructor]
        public Shell(IRegionManager regionManager)
        {
            InitializeComponent();

            // store the region manager 
            this.regionManager = regionManager;

            // listen for region changes to check when the main region is available
            this.regionManager.Regions.CollectionChanged += new NotifyCollectionChangedEventHandler(Regions_CollectionChanged);

            // Initialize journal navigation commands
            this.JournalBackCommand = new DelegateCommand(this.JournalBack, this.CanNavigateBack);
            this.JournalForwardCommand = new DelegateCommand(this.JournalForward, this.CanNavigateForward);

            // set datacontext
            this.DataContext = this;
        }

        void Regions_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // onyl care if the main region was added
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var region = e.NewItems[0] as IRegion;
                if (region != null && region.Name == RegionNames.MainRegion)
                {
                    // set the flag to true
                    this.isMainRegionAvailable = true;

                    // Let the UI know it has to reevaluate the canExecute method of the commands
                    this.JournalForwardCommand.RaiseCanExecuteChanged();
                    this.JournalBackCommand.RaiseCanExecuteChanged();

                    // start listening to all navigation events
                    this.MainRegionNavigationService.Navigated += new EventHandler<RegionNavigationEventArgs>(MainRegionNavigationService_Navigated);
                }
            }
        }

        void MainRegionNavigationService_Navigated(object sender, RegionNavigationEventArgs e)
        {
            // When a navigation occurs, let the UI know it has to reevaluate the canExecute method of the commands
            this.JournalForwardCommand.RaiseCanExecuteChanged();
            this.JournalBackCommand.RaiseCanExecuteChanged();
        }

        private void JournalBack()
        {
            // Navigate back on the main region
            this.MainRegionNavigationService.Journal.GoBack();
        }

        private bool CanNavigateBack()
        {
            if (!this.isMainRegionAvailable)
            {
                return false;
            }

            // Check if main region journal can go back
            return this.MainRegionNavigationService.Journal.CanGoBack;
        }

        private void JournalForward()
        {
            // Navigate forward on the main region
            this.MainRegionNavigationService.Journal.GoForward();
        }

        private bool CanNavigateForward()
        {
            if (!this.isMainRegionAvailable)
            {
                return false;
            }

            // Check if main region journal can go back
            return this.MainRegionNavigationService.Journal.CanGoForward;
        }
    }
}
