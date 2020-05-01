using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

namespace MyPrismSample.Module
{
    internal class AuthorListModule : IModule
    {
        private IRegionManager _regionManager;
        private readonly IUnityContainer _container;
        private AuthorListController _authorListController;

        public AuthorListModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }
        public void Initialize()
        {
            _authorListController = _container.Resolve<AuthorListController>();
            var view = _container.Resolve<AuthorListView>();
            _container.RegisterInstance<AuthorListView>(view);

            _regionManager.RegisterViewWithRegion(MyPrismSample.Bootstrapper.RegionNames.AuthorListRegion, 
                getContentDelegate: () => view);
        }
    }
}
