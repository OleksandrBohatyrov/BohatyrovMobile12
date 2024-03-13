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
    public partial class StartPage1 : ContentPage
    {
        List<ContentPage> pages = new List<ContentPage>()
            {
                new EntryPage(),
                new TimePage(),
                new BoxView_Page(),
                new Calendar(),
                new StepperSlider_Page(),
                new RGB(),
                new Lumememm(),
                new FramePage2(),
                new tripstrapstrull(),
                new PickerPage(),
                new Browser(),
                new Helista(),
                new List_Page()
            };
        List<string> text = new List<string>()
            {
                "Ava entry leht",
                "Ava timer leht",
                "Ava box leht",
                "Ava Calendar",
                "Ava Stepper Slider",
                "Ava rgb",
                "Ava lumememm",
                "Ava frame page 2",
                "Ava trips traps trull",
                "Ava picker leht",
                "Ava browser",
                "Ava helista",
                "Ava list leht"
            };
        StackLayout st;
        public StartPage1()
        {
            st= new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                BackgroundColor = Color.AliceBlue
            };
            for (int i = 0; i < pages.Count; i++)
            {
                Button button = new Button 
                { 
                    Text = text[i] ,
                    BackgroundColor = Color.AntiqueWhite,
                    TextColor= Color.Black,
                    TabIndex= i
                };
                st.Children.Add(button);
                button.Clicked += Button_Clicked;
            }
            ScrollView sv = new ScrollView { Content = st };
            Content = sv;

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            await Navigation.PushAsync(pages[btn.TabIndex]);
        }
    }
}