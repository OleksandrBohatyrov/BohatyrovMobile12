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
    public partial class TimePage : ContentPage
    {
        public TimePage()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            lbl.Text = "Vajutage!";   
        }
        bool flag=false;
        public async void NaitaAeg()
        {
            while (flag)
            {
                lbl.Text = DateTime.Now.ToString("f");
                Time_run.Text = DateTime.Now.ToString("T");
                await Task.Delay(1000);
            }
        }

        private void Time_run_Clicked(object sender, EventArgs e)
        {

            if (flag)
            {
                flag = false;
            }
            else
            {
                flag= true;
                NaitaAeg();
            }

        }
    }
}