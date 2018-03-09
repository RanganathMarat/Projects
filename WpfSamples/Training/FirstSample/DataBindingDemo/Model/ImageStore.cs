using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBindingDemo.Model
{
    public class ImageElement
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string Code { get; set; }

    }

    public class ImageContainer
    {
        public List<ImageElement> ImageList { get; set; }

        public ImageContainer()
        {
            ImageList = new List<ImageElement>();
            ImageList.Add(new ImageElement() { Name = "1", Path = "1.bmp", Code = "X" });
            ImageList.Add(new ImageElement() { Name = "2", Path = "2.bmp", Code = "Y" });
            ImageList.Add(new ImageElement() { Name = "3", Path = "3.bmp", Code = "X" });
            ImageList.Add(new ImageElement() { Name = "4", Path = "4.bmp", Code = "Y" });
        }

    }
}
