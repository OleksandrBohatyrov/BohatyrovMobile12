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
                //ItemsSource =telefons,
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
                    Label nimetus = new Label { FontSize = 20 };
                    nimetus.SetBinding(Label.TextProperty, "Nimetus");
                    Label hind = new Label();
                    hind.SetBinding(Label.TextProperty, "Hind");
                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            Padding = new Thickness(0, 5),
                            Orientation = StackOrientation.Vertical,
                            Children = { nimetus, hind }
                        }
                    };
                })
            };

            list.ItemTapped += List_ItemTapped;
            lisa.Clicked += Lisa_Clicked;
            kustuta.Clicked += Kustuta_Clicked;
            this.Content = new StackLayout { Children = { lbl_list, list, lisa, kustuta } };

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

        private void Lisa_Clicked(object sender, EventArgs e)
        {
            telefons.Add(new Telefon { Nimetus = "Uus telefon", Tootja = "Uus tootja", Hind = 1 });
            var ruhmad = telefons.GroupBy(p => p.Tootja)
                         .Select(g => new Ruhm<string, Telefon>(g.Key, g));
            telefonideruhmades = new ObservableCollection<Ruhm<string, Telefon>>(ruhmad);
            list.ItemsSource = null;
            list.ItemsSource = telefonideruhmades;
        }

        private async void List_ItemTapped(object sender, ItemTappedEventArgs e)
        {

            selectedPhone = e.Item as Telefon;
            if (selectedPhone != null)
            {
                await DisplayAlert("See on", $"{selectedPhone.Tootja} | {selectedPhone.Nimetus} - {selectedPhone.Hind} eurot", "Ok");
            }
        }
    }
}