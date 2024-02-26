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
    public partial class PickerPage : ContentPage
    {
        Picker picker;
        WebView webView;
        StackLayout st;
        string[] lehed = new string[4] { "https://moodle.edu.ee/", "https://www.tthk.ee/", "https://github.com/", "https://www.w3schools.com/" };
        string[] nimetused = new string[4] { "Moodle", "TTHK", "GitHub", "W3Schools" };
        public PickerPage()
        {
            Title = "Veebilehed";
            picker = new Picker
            {
                Title = "Veebilehed"
            };
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
            SwipeGestureRecognizer swipe_right = new SwipeGestureRecognizer { Direction = SwipeDirection.Right};
            swipe_right.Swiped += Swipe_Swiped1;
            SwipeGestureRecognizer swipe_left = new SwipeGestureRecognizer { Direction = SwipeDirection.Left };
            swipe_left.Swiped += Swipe_Swiped1;
            SwipeGestureRecognizer swipe_up = new SwipeGestureRecognizer { Direction = SwipeDirection.Up };
            swipe_up.Swiped += Swipe_Swiped1;
            SwipeGestureRecognizer swipe_down = new SwipeGestureRecognizer { Direction = SwipeDirection.Down };
            swipe_down.Swiped += Swipe_Swiped1;




            picker.SelectedIndexChanged += Valime_leht_avamiseks;
            st = new StackLayout
            {
                Children = { picker, webView }
            };
            st.GestureRecognizers.Add(swipe_right);
            st.GestureRecognizers.Add(swipe_left);
            st.GestureRecognizers.Add(swipe_down);
            st.GestureRecognizers.Add(swipe_up);
            Content = st;
        }

      

        private void Swipe_Swiped1(object sender, SwipedEventArgs e)
        {
            switch (e.Direction)
            {
                case SwipeDirection.Right:
                    webView.GoBack();
        
                    if (picker.SelectedIndex==lehed.Length-1)
                    {
                        picker.SelectedIndex = 0;
                    }
                    else
                    {
                        picker.SelectedIndex += 1;
                    }
                    break;
                case SwipeDirection.Left:
                    webView.GoForward();
                    if (picker.SelectedIndex == 0)
                    {
                        picker.SelectedIndex = lehed.Length-1;
                    }
                    else
                    {
                        picker.SelectedIndex -= 1;
                    }
 
                    break;
                case SwipeDirection.Up:
                    webView.Source = new UrlWebViewSource { Url = lehed[0] };
                    break;
                case SwipeDirection.Down:
                    webView.Source = new UrlWebViewSource { Url = lehed[lehed.Length - 1] };
                    break;
                default: break;
            }
       
        }

  

        private void Valime_leht_avamiseks(object sender, EventArgs e)
        {
            webView.Source = new UrlWebViewSource { Url = lehed[picker.SelectedIndex] };
        }
    }
    }