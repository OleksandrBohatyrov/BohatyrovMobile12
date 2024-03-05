using Plugin.Messaging;
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
        private bool isPortrait;
        Button tagasi, home, forward, favourites, historybtn;
        Grid gridTop;
        Entry addressEntry;
        //Entry tel_nr_email, text;
        Picker picker, pickerHistory;
        WebView webView;
        StackLayout st;
        List<string> lehed = new List<string> { "https://moodle.edu.ee/", "https://www.tthk.ee/", "https://github.com/", "https://www.w3schools.com/" };
        List<string> nimetused = new List<string> { "Moodle", "TTHK", "GitHub", "W3Schools" };
        List<string> history = new List<string>();


        string homeURL = "https://www.tthk.ee/";

        string result = "";


        public Browser()
        {
            Title = "Browser";
            picker = new Picker
            {
                Title = "Browser",


            };
            isPortrait = true;

            pickerHistory = new Picker
            {

            };



            //tel_nr_email = new Entry { Placeholder = "Kirjuta siia telefoni number" };
            //text = new Entry { Placeholder = "Kirjuta tekst siia" };

            //Button sms_btn = new Button { Text = "Saada SMS" };
            //sms_btn.Clicked += Sms_btn_Clicked;

            //Button call_btn = new Button { Text = "Helista" };
            //call_btn.Clicked += Call_btn_Clicked1;

            //Button mail_btn = new Button { Text = "Kirjuta kiri" };
            //mail_btn.Clicked += Mail_btn_Clicked;



            gridTop = new Grid
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions = { new RowDefinition { } },
                ColumnDefinitions = {
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Star }
                }

            };

            tagasi = new Button
            {

                Text = "<",

            };
            tagasi.Clicked += Tagasi_Clicked;


            home = new Button
            {
                Text = "|",
            };
            home.Clicked += Home_Clicked;

            forward = new Button
            {

                Text = ">",

            };
            forward.Clicked += Forward_Clicked;


            favourites = new Button
            {

                Text = "*",

            };
            favourites.Clicked += Favourites_Clicked;

            historybtn = new Button
            {

                Text = "?",

            };
            historybtn.Clicked += Historybtn_Clicked; ;


            addressEntry = new Entry
            {
                Placeholder = "Enter URL",
                ReturnType = ReturnType.Go,
                HorizontalOptions = LayoutOptions.FillAndExpand,


            };
            addressEntry.Completed += AddressEntry_Completed; ;

            foreach (string leht in nimetused)
            {
                picker.Items.Add(leht);
            }
            webView = new WebView
            {
                Source = new UrlWebViewSource { Url = "https://www.tthk.ee/" },
                HeightRequest = 400,
                WidthRequest = 200,
            };


            SwipeGestureRecognizer swipe_right = new SwipeGestureRecognizer { Direction = SwipeDirection.Right };
            swipe_right.Swiped += Swipe_Swiped1;
            SwipeGestureRecognizer swipe_left = new SwipeGestureRecognizer { Direction = SwipeDirection.Left };
            swipe_left.Swiped += Swipe_Swiped1;
            SwipeGestureRecognizer swipe_up = new SwipeGestureRecognizer { Direction = SwipeDirection.Up };
            swipe_up.Swiped += Swipe_Swiped1;
 

            gridTop.Children.Add(addressEntry, 0, 0);
            Grid.SetColumnSpan(addressEntry, 6);
            gridTop.Children.Add(tagasi, 0, 1);
            gridTop.Children.Add(home, 1, 1);
            gridTop.Children.Add(forward, 2, 1);
            gridTop.Children.Add(favourites, 3, 1);
            gridTop.Children.Add(historybtn, 4, 1);

            addressEntry.HorizontalOptions = LayoutOptions.FillAndExpand;


            var longPressGesture = new TapGestureRecognizer();
            longPressGesture.NumberOfTapsRequired = 1;
            longPressGesture.Tapped += LongPressGesture_Tapped ;
            picker.GestureRecognizers.Add(longPressGesture);


            picker.SelectedIndexChanged += Valime_leht_avamiseks;
            st = new StackLayout
            {
                Children = { gridTop, picker, webView,/* tel_nr_email, sms_btn, call_btn, mail_btn*/ }
            };
            st.GestureRecognizers.Add(swipe_right);
            st.GestureRecognizers.Add(swipe_left);
     
            st.GestureRecognizers.Add(swipe_up);

            Content = st;



        }

        private async void LongPressGesture_Tapped(object sender, EventArgs e)
        {
            var pickedItem = picker.SelectedItem as string;
            var confirm = await DisplayAlert("Kustuta", $"Kas soovite kustutada {pickedItem}?", "Jah", "Ei");
            if (confirm)
            {
                if (lehed.Contains(pickedItem))
                {
                    lehed.Remove(pickedItem);
                    nimetused.Remove(pickedItem);
                    picker.Items.Remove(pickedItem);
                 
                    webView.Source = new UrlWebViewSource { Url = homeURL }; 
                }
            }
        }

        private void Swipe_Swiped1(object sender, SwipedEventArgs e)
        {
         
            switch (e.Direction)
            {
                case SwipeDirection.Right:
                

                    if (webView.CanGoBack)
                    {

                        webView.GoBack();
                        UpdatePickerSelection();


                    }
                    break;
                case SwipeDirection.Left:
                    if (webView.CanGoForward)
                    {
                        webView.GoForward();
                        UpdatePickerSelection();

                    }

                    break;
                case SwipeDirection.Up:
                    webView.Source = new UrlWebViewSource { Url = homeURL };
                    break;
              
                default: break;
            }
        }
        private void UpdatePickerSelection()
        {
            var currentUrl = webView.Source?.ToString();
            var index = lehed.IndexOf(currentUrl);
            if (index >= 0 && index < picker.Items.Count)
            {
                picker.SelectedIndex = index;
            }
        }

        private async void Historybtn_Clicked(object sender, EventArgs e)
        {
            if (history.Count > 0)
            {
                string selectedUrl = await DisplayActionSheet("Recent Pages", "Cancel", null, history.ToArray());

                if (selectedUrl != "Cancel")
                {
                    webView.Source = new UrlWebViewSource { Url = selectedUrl };
                }
            }
            else
            {
                await DisplayAlert("History", "No recent pages", "OK");
            }
        }

        private async void Favourites_Clicked(object sender, EventArgs e)
        {
            if (webView.Source is UrlWebViewSource urlWebViewSource)
            {
                string currentUrl = urlWebViewSource.Url;

                lehed.Add(currentUrl);

                result = await DisplayPromptAsync("Vali uus nimi", "Uus nimi");
                nimetused.Add(result);
                picker.Items.Add(result);
                picker.SelectedIndex = nimetused.IndexOf(result);

                UpdateWebViewSource();
            }
            else
            {
                await DisplayAlert("Error", "The current source is not a URL", "OK");
            }
        }
        private void UpdateWebViewSource()
        {
            if (picker.SelectedIndex >= 0 && picker.SelectedIndex < lehed.Count)
            {
                string selectedUrl = lehed[picker.SelectedIndex];
                if (!string.IsNullOrEmpty(selectedUrl))
                {
                    webView.Source = new UrlWebViewSource { Url = selectedUrl };
                    AddToHistory(selectedUrl);
                }
            }
        }



        private void Forward_Clicked(object sender, EventArgs e)
        {
            if (webView.CanGoForward)
            {
                webView.GoForward();

            }
        }

        private void Home_Clicked(object sender, EventArgs e)
        {
            webView.Source = new UrlWebViewSource { Url = "https://www.tthk.ee/" };
        }

        private void Tagasi_Clicked(object sender, EventArgs e)
        {
            if (webView.CanGoBack)
            {
                webView.GoBack();

            }
        }

        //private void Call_btn_Clicked1(object sender, EventArgs e)
        //{
        //    var call = CrossMessaging.Current.PhoneDialer;
        //    if (call.CanMakePhoneCall)
        //    {
        //        call.MakePhoneCall(tel_nr_email.Text);
        //    }
        //}
            
        private void AddressEntry_Completed(object sender, EventArgs e)
        {
            string url = addressEntry.Text;

            if (!string.IsNullOrWhiteSpace(url))
            {
                if (!url.StartsWith("http://") && !url.StartsWith("https://"))
                {
                    url = "https://" + url;
                }

                webView.Source = new UrlWebViewSource { Url = url };
                AddToHistory(url); 
            }
        }

        private void Valime_leht_avamiseks(object sender, EventArgs e)
        {
                if (picker.SelectedIndex >= 0 && picker.SelectedIndex < lehed.Count)
                {
                    string selectedUrl = lehed[picker.SelectedIndex];
                    webView.Source = new UrlWebViewSource { Url = selectedUrl };
                    AddToHistory(selectedUrl);
                }
        }
        private void AddToHistory(string url)
        {
        
            if (!history.Contains(url))
            {
                history.Add(url);
            }
         
            if (history.Count > 5)
            {
                history.RemoveAt(0); 
            }
        }




        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            // Проверяем, изменилась ли ориентация
            bool newIsPortrait = width < height;

            if (newIsPortrait != isPortrait)
            {
                isPortrait = newIsPortrait;

                // Обновляем расположение элементов в зависимости от ориентации
                UpdateLayout();
            }
        }

        private void UpdateLayout()
        {
            if (isPortrait)
            {
                // Вертикальная ориентация
                // Ваши изменения для вертикальной ориентации здесь

                // Пример:
                gridTop.ColumnDefinitions.Clear();
                gridTop.RowDefinitions.Clear();
                gridTop.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                gridTop.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                gridTop.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                gridTop.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            }
            else
            {
                // Горизонтальная ориентация
                // Ваши изменения для горизонтальной ориентации здесь

                // Пример:
                gridTop.ColumnDefinitions.Clear();
                gridTop.RowDefinitions.Clear();
                gridTop.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                gridTop.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                gridTop.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
            }
        }

        //private void Sms_btn_Clicked(object sender, EventArgs e)
        //{
        //    var sms = CrossMessaging.Current.SmsMessenger;
        //    if (sms.CanSendSms)
        //    {
        //        sms.SendSms(tel_nr_email.Text, text.Text);
        //    }
        //}

        //private void Call_btn_Clicked(object sender, EventArgs e)
        //{
        //    var call = CrossMessaging.Current.PhoneDialer;
        //    if (call.CanMakePhoneCall)
        //    {
        //        call.MakePhoneCall(tel_nr_email.Text);
        //    }
        //}

        //private void Mail_btn_Clicked(object sender, EventArgs e)
        //{
        //    var mail = CrossMessaging.Current.EmailMessenger;
        //    if (mail.CanSendEmail)
        //    {
        //        mail.SendEmail(tel_nr_email.Text, "Tervitus!", text.Text);
        //    }
        //}
    }
}