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
    public partial class EntryPage : ContentPage
    {
        Label lbl;
        Label lbl2;
        Editor editor;
        public EntryPage()
        {
            Title= "Entry page";
            lbl = new Label { Text = "Mingi tekst" };
            lbl.BackgroundColor = Color.White;
            lbl.TextColor = Color.Orange;

            lbl2 = new Label { Text = "Tere" };
            lbl2.HorizontalOptions= LayoutOptions.Start;
            lbl2.TextColor = Color.Orange;

            Button Entry_btn = new Button
            {
                Text = "Tagasi Start lehele",
                BackgroundColor = Color.Red,
                TextColor = Color.White
            };
            editor = new Editor
            {
                Placeholder = "Sisesta siia teksti...",
                HorizontalOptions = LayoutOptions.Center
            };
            StackLayout st = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                BackgroundColor = Color.FromRgb(32, 32, 34),
                Children = {  Entry_btn, lbl, editor },
                VerticalOptions= LayoutOptions.End
            };
            

            Content = st;
            Entry_btn.Clicked += Entry_btn_Clicked;
            editor.TextChanged += Editor_TextChanged;
        }

        private void Editor_TextChanged(object sender, TextChangedEventArgs e)
        {
            lbl.Text=editor.Text;
            if (editor.Text.Contains("Tere"))
            {
                lbl2.Text = "Tere";
            }
        }

        private async void Entry_btn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}