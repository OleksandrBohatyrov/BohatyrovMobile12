using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static System.Net.WebRequestMethods;

namespace BohatyrovMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RGB : ContentPage
    {
        Slider sld, sld2,sld3;
        Stepper stp;
        BoxView box;
        Button rnd;
        public RGB()
        {

            rnd = new Button
            {
                Text= "Random",
            };
            rnd.Clicked += Rnd_Clicked;
            sld = new Slider
            {
                Minimum = 0,
                Maximum = 100,
                Value = 30,
                MinimumTrackColor = Color.White,
                MaximumTrackColor = Color.Black,
                ThumbColor = Color.Red
            };
            sld2 = new Slider
            {
                Minimum = 0,
                Maximum = 100,
                Value = 30,
                MinimumTrackColor = Color.White,
                MaximumTrackColor = Color.Black,
                ThumbColor = Color.Red
            };
            sld3 = new Slider
            {
                Minimum = 0,
                Maximum = 100,
                Value = 30,
                MinimumTrackColor = Color.White,
                MaximumTrackColor = Color.Black,
                ThumbColor = Color.Red
            };
            sld.ValueChanged += Sld_ValueChanged;
            sld2.ValueChanged += Sld2_ValueChanged; 
            sld3.ValueChanged += Sld3_ValueChanged; 

            stp = new Stepper
            {
                Minimum = 0,
                Maximum = 100,
                Increment = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            stp.ValueChanged += Stp_ValueChanged;

            box = new BoxView
            {
                WidthRequest = 200,
                HeightRequest = 400,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            StackLayout st = new StackLayout
            {
                Children = { box, sld, sld2, sld3, stp, rnd }
            };
            Content = st;
        }
     
        private void Rnd_Clicked(object sender, EventArgs e)
        {
            Random rand = new Random();
            sld.Value = rand.Next(0,256);
            sld2.Value = rand.Next(0, 256);
            sld3.Value = rand.Next(0, 256);
        }

        private void Stp_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (e.NewValue < e.OldValue)
            {
                // Уменьшение размеров box при изменении значения степпера на минус
                box.WidthRequest--;
                box.HeightRequest--;
            }
            else if (e.NewValue > e.OldValue)
            {
                // Увеличение размеров box при изменении значения степпера на плюс
                box.WidthRequest++;
                box.HeightRequest++;
            }
        }

        private void Sld_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            box.BackgroundColor= Color.FromRgb((int)sld.Value, (int)sld2.Value, (int)sld3.Value);
        }
        private void Sld2_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            box.BackgroundColor = Color.FromRgb((int)sld.Value, (int)sld2.Value, (int)sld3.Value);
        }

        private void Sld3_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            box.BackgroundColor = Color.FromRgb((int)sld.Value, (int)sld2.Value, (int)sld3.Value);
        }

  
    }
    
}