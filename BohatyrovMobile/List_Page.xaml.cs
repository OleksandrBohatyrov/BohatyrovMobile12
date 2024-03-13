using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BohatyrovMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class List_Page : ContentPage
    {
       public ObservableCollection<Telefon> telefons{ get; set; }
        public ObservableCollection<Ruhm<string, Telefon>> telefonideruhmades { get; set; }
        Label lbl_list;
        ListView list;
        Button lisa, kustuta;
        Telefon selectedPhone;

        

        Entry nimetusEntry = new Entry { Placeholder = "Nimetus",  };
        Entry tootjaEntry = new Entry { Placeholder = "Tootja",  };
        Entry hindEntry = new Entry { Placeholder = "Hind",  };




        public List_Page()
        {

       
            lisa = new Button { Text = "Lisa felefon" };
            kustuta = new Button { Text = "Kustuta telefn" };
            telefons = new ObservableCollection<Telefon>
                {
                new Telefon { Nimetus = "Samsung Galaxy S22 Ultra", Tootja = "Samsung", Hind = 1349, Pilt="telefon.png"},
                new Telefon { Nimetus = "Google Pixel 6A", Tootja = "Google", Hind = 490, Pilt="telefon.png" },
                new Telefon { Nimetus = "Xiaomi Mi 11 Lite 5G", Tootja = "Xiaomi", Hind = 339 , Pilt="telefon.png"},
                new Telefon { Nimetus = "iPhone 13", Tootja = "Apple", Hind = 1179 , Pilt = "telefon.png"}
                };
            var ruhmad = telefons.GroupBy(p => p.Tootja)
                         .Select(g => new Ruhm<string, Telefon>(g.Key, g));
            telefonideruhmades = new ObservableCollection<Ruhm<string, Telefon>>(ruhmad);
            lbl_list = new Label
            {
                HorizontalOptions = LayoutOptions.Center,
                Text = "Telefonid loetelu",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            };

            list = new ListView
            {
                
                SeparatorColor = Color.Orange,
                Header = "Telefonid rühmades",
                Footer = DateTime.Now.ToString("T"),
                HasUnevenRows = true,
                ItemsSource = telefonideruhmades,
                IsGroupingEnabled = true,           
                GroupHeaderTemplate = new DataTemplate(() =>
                {

                    Label tootja = new Label();
                    tootja.SetBinding(Label.TextProperty, "Nimetus");
                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            Padding = new Thickness(0, 5),
                            Orientation = StackOrientation.Vertical,
                            Children = { tootja }
                        }
                    };
                }),
                ItemTemplate = new DataTemplate(() =>
                {

                    Image image = new Image { Aspect=Aspect.AspectFill, HeightRequest = 50, WidthRequest= 50};
                    image.SetBinding(Image.SourceProperty, "Pilt");
                    Label nimetus = new Label { FontSize = 20 };
                    nimetus.SetBinding(Label.TextProperty, "Nimetus");
                    Label hind = new Label();
                    hind.SetBinding(Label.TextProperty, "Hind");
                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                          
                            Padding = new Thickness(0, 5),
                            Orientation = StackOrientation.Horizontal,
                            Children = { image, nimetus, hind  }
                        }
                    };
                })
            };

            list.ItemTapped += List_ItemTapped;
            lisa.Clicked += Lisa_Clicked;
            kustuta.Clicked += Kustuta_Clicked;
            this.Content = new StackLayout { Children = { lbl_list, list, lisa, kustuta, nimetusEntry, tootjaEntry, hindEntry } };




        }

        private void Kustuta_Clicked(object sender, EventArgs e)
        {
            telefons.Remove(selectedPhone);
            var ruhmad = telefons.GroupBy(p => p.Tootja)
                         .Select(g => new Ruhm<string, Telefon>(g.Key, g));
            telefonideruhmades = new ObservableCollection<Ruhm<string, Telefon>>(ruhmad);
            list.ItemsSource = null;
            list.ItemsSource = telefonideruhmades;
        }

        private async void Lisa_Clicked(object sender, EventArgs e)
        {


            var pickResult = await Xamarin.Essentials.MediaPicker.PickPhotoAsync();

            if (pickResult != null)
            {
                var telefon = new Telefon
                {
                    Nimetus = nimetusEntry.Text,
                    Tootja = tootjaEntry.Text,
                    Hind = int.Parse(hindEntry.Text),
                    Pilt = ImageSource.FromStream(() => pickResult.OpenReadAsync().Result)
                };

                telefons.Add(telefon);

                UpdateList();
            }
        }


        private void UpdateList()
        {
            var ruhmad = telefons.GroupBy(p => p.Tootja)
                            .Select(g => new Ruhm<string, Telefon>(g.Key, g));
            telefonideruhmades = new ObservableCollection<Ruhm<string, Telefon>>(ruhmad);
            list.ItemsSource = telefonideruhmades;
        }

        private async void List_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            selectedPhone = e.Item as Telefon;
            if (selectedPhone != null)
            {

                await DisplayAlert("Info", $"{selectedPhone.Tootja} | {selectedPhone.Nimetus} - {selectedPhone.Hind} eurot", "Ok");

                bool choice = await DisplayAlert("Vali foto", "Soovid muuta telefoni pilti?", "Jah", "Ei");

                if (choice)
                {
                    var pickResult = await Xamarin.Essentials.MediaPicker.PickPhotoAsync();
                        
                    if (pickResult != null)
                    {
                        selectedPhone.Pilt = ImageSource.FromStream(() => pickResult.OpenReadAsync().Result);
                        UpdateList();
                    }
                }
            }
        }
    }
}