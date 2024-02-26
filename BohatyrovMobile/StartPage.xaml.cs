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
    public partial class StartPage : ContentPage
    {
        public StartPage()
        {
            Button Entry_btn = new Button
            {
                Text = "Entry leht",
                BackgroundColor = Color.FromRgb(32, 32, 98),
                TextColor=Color.White
            };
            Button Time_btn = new Button
            {
                Text = "Time leht",
                BackgroundColor = Color.FromRgb(32, 32, 98),
                TextColor = Color.White
            };
            Button Tap_btn = new Button
            {
                Text = "Box View leht",
                BackgroundColor = Color.FromRgb(32, 32, 98),
                TextColor = Color.White
            };
            StackLayout st = new StackLayout
            {
                Orientation=StackOrientation.Vertical,
                BackgroundColor=Color.FromRgb(32,32, 34)
            };
            st.Children.Add(Entry_btn);
            st.Children.Add(Time_btn);
            st.Children.Add(Tap_btn);
            Content = st;
            Entry_btn.Clicked += Entry_btn_Clicked;
            Time_btn.Clicked += Time_btn_Clicked;
            Tap_btn.Clicked += Tap_btn_Clicked;
        }

        private async void Tap_btn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BoxView_Page());
        }

        private async void Time_btn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TimePage());
        }

        private async void Entry_btn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EntryPage());
        }
    }
}