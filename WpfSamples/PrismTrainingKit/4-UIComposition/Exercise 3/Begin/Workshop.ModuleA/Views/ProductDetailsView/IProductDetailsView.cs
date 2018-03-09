using Workshop.ModuleA.Model;
using Microsoft.Practices.Prism.Events;
using System;

namespace Workshop.ModuleA.Views.ProductDetailsView
{
    public interface IProductDetailsView
    {
        Product Model { get; set; }        
    }
}
