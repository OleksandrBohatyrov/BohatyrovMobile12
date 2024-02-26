using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BohatyrovMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Frame_Page : ContentPage
    {
        Frame fr;
        Label lbl;
        Grid gr;
        public Frame_Page()
        {
            lbl = new Label
            {
                Text = "Raami kujundus",
                FontSize=Device.GetNamedSize(NamedSize.Subtitle, typeof(Label))
            };  
            fr = new Frame
            {
                BorderColor=Color.FromRgb(20, 120, 255),
                CornerRadius=20,
                VerticalOptions= LayoutOptions.FillAndExpand
            };
            StackLayout st = new StackLayout
            {
                Children= {lbl,fr}
            };
            Content= st;
        }
    }
}