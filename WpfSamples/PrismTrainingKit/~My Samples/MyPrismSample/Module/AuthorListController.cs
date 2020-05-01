using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Prism.Regions;

namespace MyPrismSample.Module
{
    public class AuthorListController
    {
        private IUnityContainer _container;
        private IRegionManager _regionManager;
        private AuthorListView _view;
        private Author _viewModel;

        public AuthorListController(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;

            _viewModel = _container.Resolve<Author>();
            _container.RegisterInstance<Author>(_viewModel);
            _viewModel.AuthorName = "Alistair Maclean";
            _viewModel.BookTitle = "Guns of Navarone";
            _view = _container.Resolve<AuthorListView>();

            _container.RegisterInstance<AuthorListView>(_view);
            _view.DataContext = _viewModel;            

        }
    }
}

