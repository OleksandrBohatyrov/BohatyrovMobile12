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
        Button tagasi, home;
        Grid gridTop;
        Entry addressEntry;
        //Entry tel_nr_email, text;
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
                Title = "Browser",
               
              
            };
            isPortrait = true;



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
                RowDefinitions = { new RowDefinition {  } },
                ColumnDefinitions = {
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Auto },
                new ColumnDefinition { Width = GridLength.Auto }
                }

            };

            tagasi = new Button 
            { 
               
                Text = "<",
            
            };
            tagasi.Clicked += Tagasi_Clicked;


            home = new Button
            {
                Text= "|",
            };
            home.Clicked += Home_Clicked;


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


            gridTop.Children.Add(addressEntry, 0, 0);
            gridTop.Children.Add(tagasi, 1, 0);
            gridTop.Children.Add(home, 2, 0);



            picker.SelectedIndexChanged += Valime_leht_avamiseks;
            st = new StackLayout
            {
                Children = {gridTop, picker, webView,/* tel_nr_email, sms_btn, call_btn, mail_btn*/ }
            };

            Content = st;


       
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
                webView.Source = new UrlWebViewSource { Url = "https://"+url };
                
            }
        }

        private void Valime_leht_avamiseks(object sender, EventArgs e)
        {
            webView.Source = new UrlWebViewSource {Url = lehed[picker.SelectedIndex] };
        
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