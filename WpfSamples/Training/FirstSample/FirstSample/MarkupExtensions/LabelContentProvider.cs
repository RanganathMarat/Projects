using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmptyStart.MarkupExtensions
{
    class CultureService
    {
        static CultureService instance = new CultureService();
        private Dictionary<string, string> resDict = new Dictionary<string, string>();

        public static CultureService Instance
        {
            get { return instance; }
        }

        private CultureService()
        {
            resDict.Add(new CultureInfo("en-US").DisplayName, "English");
            resDict.Add(new CultureInfo("fr-FR").DisplayName, "French"); 
            resDict.Add(new CultureInfo("de-DE").DisplayName, "German");
        }

        public string this[string key]
        {
            get
            {
                if (resDict.ContainsKey(key))
                    return resDict[key];
                else
                    return "BLA";
            }
        }
    }

    class LabelContentProvider:System.Windows.Markup.MarkupExtension
    {
        private CultureInfo culture;

        public CultureInfo Culture1
        {
            get { return culture; }
            set { culture = value; }
        }
        

        public LabelContentProvider() { culture = new CultureInfo("en-US"); }
        
        //public LabelContentProvider(CultureInfo culture) { this.culture = culture; }
        
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return CultureService.Instance[culture.NativeName];
            //return "labelContent" + (++count).ToString() ;
        }
    }
}
