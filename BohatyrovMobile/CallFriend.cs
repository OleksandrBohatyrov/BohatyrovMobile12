using Plugin.Messaging;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BohatyrovMobile
{
    internal class CallFriend : ContentPage
    {
        TableView tableView;
        SwitchCell sc;
        ImageCell ic;
        TableSection fotosection;
        EntryCell tel_nr, email,  text;

        public CallFriend()
        {
 
             
            tel_nr = new EntryCell()
            {
                Label = "Telefon",
                Placeholder = "Sisesta tel. number",
                Keyboard = Keyboard.Telephone
            };
            email = new EntryCell()
            {
                Label = "Email",
                Placeholder = "Sisesta email",
                Keyboard = Keyboard.Email

            };
            text = new EntryCell()
            {
                Label = "Plaun kirjuta text",
                Placeholder = "Sisesta text",
                Keyboard = Keyboard.Default,
            };
            fotosection = new TableSection();
            tableView = new TableView()
            {
                Intent = TableIntent.Form,
                Root = new TableRoot("Andmete sisestamine")
                {
                    new TableSection("Põhiandmed: ")
                    {
                        text
                    },
                    new TableSection("Kontaktiandmed: ")
                    {
                        tel_nr,
                        email,
                        sc
                    },
                    fotosection
                }
            };

            Button sms_btn = new Button { Text = "Saada SMS" };
            sms_btn.Clicked += Sms_btn_Clicked;

            Button call_btn = new Button { Text = "Helista" };
            call_btn.Clicked += Call_btn_Clicked;

            Button mail_btn = new Button { Text = "Kirjuta kiri" };
            mail_btn.Clicked += Mail_btn_Clicked;

            Grid actionStackLayout = new Grid
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                }
            };
            actionStackLayout.Children.Add(call_btn, 0, 0);
            actionStackLayout.Children.Add(sms_btn, 1, 0);
            actionStackLayout.Children.Add(mail_btn, 2, 0);

            StackLayout st = new StackLayout { Children = { tableView, actionStackLayout } };

            Content = st;
        }

       
        private void Sms_btn_Clicked(object sender, EventArgs e)
        {
            var sms = CrossMessaging.Current.SmsMessenger;
            if (sms.CanSendSms)
            {
                sms.SendSms(tel_nr.Text, text.Text);
            }
        }

        private void Call_btn_Clicked(object sender, EventArgs e)
        {
            var call = CrossMessaging.Current.PhoneDialer;
            if (call.CanMakePhoneCall)
            {
                call.MakePhoneCall(tel_nr.Text);
            }
        }

        private void Mail_btn_Clicked(object sender, EventArgs e)
        {
            var mail = CrossMessaging.Current.EmailMessenger;
            if (mail.CanSendEmail)
            {
                mail.SendEmail(email.Text, "Tervitus!", text.Text);
            }
        }
    
}
}
