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
    public partial class Calendar : ContentPage
    {
        DatePicker dp;
        TimePicker tp;
        Label lbl;
        public Calendar()
        {
            dp = new DatePicker
            {
                Format = "D",
                MinimumDate = DateTime.Now.AddDays(-10),
                MaximumDate = DateTime.Now.AddDays(10),
                TextColor = Color.Beige
            };
            dp.DateSelected += Dp_DateSelected;
            tp = new TimePicker
            {
                Time = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second),
            };
            tp.PropertyChanged += Tp_PropertyChanged;
            lbl = new Label
            {
                BackgroundColor = Color.Transparent
            };
            AbsoluteLayout abs = new AbsoluteLayout
            {
                Children = {lbl, dp, tp }
            };
            AbsoluteLayout.SetLayoutBounds(dp, new Rectangle(0.2,0.2,300,300));
            AbsoluteLayout.SetLayoutFlags(dp, AbsoluteLayoutFlags.PositionProportional);

            AbsoluteLayout.SetLayoutBounds(tp, new Rectangle(0.2, 0.6, 300, 300));
            AbsoluteLayout.SetLayoutFlags(tp,  AbsoluteLayoutFlags.PositionProportional);
            Content = abs;
        }

        private void Tp_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            lbl.Text="Aeg: "+tp.Time.ToString();
        }

        private void Dp_DateSelected(object sender, DateChangedEventArgs e)
        {
            lbl.Text=e.NewDate.ToString("D");
        }
    }
}