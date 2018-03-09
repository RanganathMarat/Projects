using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBindingDemo.Model
{
    public class Image
    {
        public string name { get; set; }
        public string path { get; set; }
        public string resolution { get; set; }
        public string format { get; set; }
   }

    public class ImageRep
    {
        public  List<Image> imageRepository{get; set;}

        public ImageRep()
        {
            imageRepository = new List<Image>();
            imageRepository.Add(new Image() {name="X", format="jpg", path="c:\\x", resolution="10*10"});
            imageRepository.Add(new Image() {name="Y", format="jpg", path="c:\\x", resolution="10*10"});
            imageRepository.Add(new Image() {name="Z", format="jpg", path="c:\\x", resolution="10*10"});
            imageRepository.Add(new Image() {name="A", format="jpg", path="c:\\x", resolution="10*10"});
            imageRepository.Add(new Image() {name="B", format="jpg", path="c:\\x", resolution="10*10"});
        }
    }
}
