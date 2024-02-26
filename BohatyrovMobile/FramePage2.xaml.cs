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
    public partial class FramePage2 : ContentPage
    {
        Grid grid;
        Random random = new Random();
        Frame fr;
        Label lbl;
        Image img;
        Switch sw;
        public FramePage2()
        {
            grid= new Grid
            {
                BackgroundColor= Color.LightBlue,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,

            };
            TapGestureRecognizer tap = new TapGestureRecognizer();
            tap.Tapped += Tap_Tapped;
            tap.NumberOfTapsRequired = 1;
            grid.GestureRecognizers.Add(tap);

            for (int i = 0; i < 10; i++)
            {


                for (int j = 0; j < 15; j++)
                {
                    grid.Children.Add(
                        fr = new Frame { BackgroundColor=Color.FromRgb(random.Next(0,255), random.Next(0, 255), random.Next(0, 255)) ,
                            VerticalOptions = LayoutOptions.FillAndExpand,
                            HorizontalOptions = LayoutOptions.FillAndExpand
                        }, i, j
                        );
                    fr.GestureRecognizers.Add( tap );
                }
            }
            lbl = new Label { 
            Text = "Tekst",
            TextColor= Color.Black,
            FontSize= Device.GetNamedSize(NamedSize.Subtitle,typeof(Label))};

            grid.Children.Add( lbl,0,5 );
            Grid.SetColumnSpan(lbl, 3);

            img = new Image
            {
                Source = "sun.png"
            };
            sw = new Switch { IsToggled= false };
            sw.Toggled += Sw_Toggled;
            grid.Children.Add( sw,0,7 );
            grid.Children.Add( img,1,7 );



            Content = (grid);
        }

        private void Sw_Toggled(object sender, ToggledEventArgs e)
        {
            if (e.Value)
            {
                img.IsVisible = true;
            }
            else
            {
                img.IsVisible = false;
            }
        }

        private void Tap_Tapped(object sender, EventArgs e)
        {
            Frame fr = (Frame)sender;
            var r = Grid.GetRow(fr)+1;
            var c = Grid.GetColumn(fr)+1;
            lbl.Text = "Rida: "+r.ToString()+"Veerg: "+c.ToString();
        }
    }
}