﻿using Microsoft.Practices.Prism.Regions;
using Workshop.ModuleA.Views.ProductDetailsView;
using Workshop.Infrastructure.Model;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.Events;
using Workshop.Infrastructure.Events;

namespace Workshop.ModuleA.Views
{
    public class ProductsController : IProductsController
    {
        private readonly IRegionManager regionManager;
        private readonly IUnityContainer container;
        private readonly IEventAggregator eventAggregator;

        public ProductsController(IUnityContainer container, IRegionManager regionManger, IEventAggregator eventAggregator)
        {
            this.regionManager = regionManger;
            this.container = container;
            this.eventAggregator = eventAggregator;
        }

		public void OnProductSelected(Product product)
		{
				   
			// Get the Bottom region (View Injection)
			var region = this.regionManager.Regions["BottomRegion"];

			// we will name views to register and retrieve them (using the product id). This avoids having several view instances for the same product.
			var viewName = product.ProductId.ToString();

			// Get a view from the region using the view's name.
			var productDetailsView = region.GetView(viewName);

			// If the view was never created, create it for the first time
			if (productDetailsView == null)
			{
				// Resolve the presenter and set the product
				var presenter = this.container.Resolve<ProductDetailsPresenter>();
				presenter.SetProduct(product);

				productDetailsView = presenter.View;

				// Add the view using its name. As the view contains a region, create a ScopedRegionManager.                
				var bottomRegionManager = region.Add(productDetailsView, viewName, true);
				string regionManagerName = string.Format("Product{0}BottomRegionManager", product.ProductId);
				container.RegisterInstance<IRegionManager>(regionManagerName, bottomRegionManager);
			}

			//Get the event through event aggregator
			var productSelected = this.eventAggregator.GetEvent<ProductSelectedEvent>();

			//Publish the event
			productSelected.Publish(product);

			// Activate the view
			region.Activate(productDetailsView);
		}

    }
}
