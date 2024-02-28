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
    public partial class Browser : ContentPage
    {

        Entry addressEntry;

        Picker picker;
        WebView webView;
        StackLayout st;
        string[] lehed = new string[4] { "https://moodle.edu.ee/", "https://www.tthk.ee/", "https://github.com/", "https://www.w3schools.com/" };
        string[] nimetused = new string[4] { "Moodle", "TTHK", "GitHub", "W3Schools" };


        public Browser()
        {
            Title = "Browser";
            picker = new Picker
            {
                Title = "Browser"
            };

            addressEntry = new Entry
            {
                Placeholder = "Enter URL",
                ReturnType = ReturnType.Go, 
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            addressEntry.Completed += AddressEntry_Completed; ;

            foreach (string leht in nimetused)
            {
                picker.Items.Add(leht);
            }
            webView = new WebView
            {
                Source = new UrlWebViewSource { Url = "https://www.w3schools.com/" },
                HeightRequest = 400,
                WidthRequest = 200,
            };


            picker.SelectedIndexChanged += Valime_leht_avamiseks;
            st = new StackLayout
            {
                Children = {addressEntry, picker, webView }
            };

            Content = st;


       
        }

        private void AddressEntry_Completed(object sender, EventArgs e)
        {
            string url = addressEntry.Text;

            if (!string.IsNullOrWhiteSpace(url))
            {
                webView.Source = new UrlWebViewSource { Url = "https://"+url };
            }
        }

        private void Valime_leht_avamiseks(object sender, EventArgs e)
        {
            webView.Source = new UrlWebViewSource {Url = lehed[picker.SelectedIndex] };
        }
    }
}