using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace DataBindingDemo.TemplateSelector
{
    class ImageDatatemplateSelector:DataTemplateSelector
    {
        public DataTemplate RedImageTemplate { get; set; }
        public DataTemplate BlueImageTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            Model.ImageElement img = item as Model.ImageElement;
            if (img.Code == "X")
            {
                return RedImageTemplate;
            }
            else
            {
                return BlueImageTemplate;
            }
        }
    }
}
