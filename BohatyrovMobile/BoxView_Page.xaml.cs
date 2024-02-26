using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BohatyrovMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class BoxView_Page : ContentPage
    {
        BoxView box;
        Label schet;


        public BoxView_Page()
        {
            int r = 0, g = 0, b = 0;
            box = new BoxView
            {
                Color = Color.FromRgb(r, g, b),
                CornerRadius = 10,
                WidthRequest = 200,
                HeightRequest = 400,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            schet = new Label
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Text = "",
                FontSize = 30,
                TextColor=Color.White
               
                
            };

            TapGestureRecognizer tap = new TapGestureRecognizer();
            tap.Tapped += Tap_Tapped1;
            box.GestureRecognizers.Add(tap);
            StackLayout st = new StackLayout { Children = { box, schet } };
            Content = st;
        }
        Random rnd;

        int IntSchet = 0;    
        void Tap_Tapped1(object sender, EventArgs e)
        {
            rnd = new Random();
            box.Color = Color.FromRgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
            IntSchet++;
            schet.Text = Convert.ToString(IntSchet);
            box.WidthRequest++;
            box.HeightRequest++;
            Vibration.Vibrate();

        }

    }
}