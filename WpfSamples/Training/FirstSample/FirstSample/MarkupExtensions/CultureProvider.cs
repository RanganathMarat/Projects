using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstSample.MarkupExtensions
{
    class CultureProvider:System.Windows.Markup.MarkupExtension
    {
        private string cultureName;

        public string CultureName
        {
            get { return cultureName; }
            set { cultureName = value; }
        }
        public CultureProvider() { }

        public CultureProvider(string cultureName) { this.cultureName = cultureName; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return new CultureInfo(cultureName);
        }
    }
}
