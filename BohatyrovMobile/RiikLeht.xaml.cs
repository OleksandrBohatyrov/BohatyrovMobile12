using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Essentials;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BohatyrovMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RiikLeht : ContentPage
    {
        internal ObservableCollection<Ruhm<string, Riik>> Riigid { get; set; }
        Label lbl_list;
        ListView list;
        Button lisa, kustuta, chooseImage;
        Riik selectedCountry;

        Entry nimiEntry = new Entry { Placeholder = "Nimi" };
        Entry pealinnEntry = new Entry { Placeholder = "Pealinn" };
        Entry rahvaarvEntry = new Entry { Placeholder = "Rahvaarv" };
        Picker continentPicker = new Picker { Title = "Kontinent" };
        Image flagImage = new Image();

        List<string> kontinets = new List<string> { "USA", "Europe", "Asia" };

        public RiikLeht()
        {
            continentPicker.ItemsSource = kontinets;

            lisa = new Button { Text = "Lisa riik" };
            kustuta = new Button { Text = "Kustuta riik" };
            chooseImage = new Button { Text = "Vali pilt" };
            Riigid = new ObservableCollection<Ruhm<string, Riik>>();
            Riigid.Add(new Ruhm<string, Riik>("Europe", new List<Riik>()));

            lbl_list = new Label
            {
                HorizontalOptions = LayoutOptions.Center,
                Text = "Riikide loetelu",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            };

            list = new ListView
            {
                SeparatorColor = Color.Orange,
                Header = "Riikide loetelu",
                Footer = DateTime.Now.ToString("T"),
                HasUnevenRows = true,
                ItemsSource = Riigid,
                IsGroupingEnabled = true,
                GroupDisplayBinding = new Binding("Nimetus"),
                ItemTemplate = new DataTemplate(() =>
                {
                    Image image = new Image { Aspect = Aspect.AspectFill, HeightRequest = 50, WidthRequest = 50 };
                    image.SetBinding(Image.SourceProperty, "Pilt");
                    Label nimi = new Label();
                    nimi.SetBinding(Label.TextProperty, "CountryName");

                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            Padding = new Thickness(0, 5),
                            Orientation = StackOrientation.Horizontal,
                            Children = { image, nimi }
                        }
                    };
                })
            };

            list.ItemTapped += List_ItemTapped;
            lisa.Clicked += Lisa_Clicked;
            kustuta.Clicked += Kustuta_Clicked;
            chooseImage.Clicked += ChooseImage_Clicked;
            this.Content = new StackLayout
            {
                Children = { lbl_list, list, lisa, kustuta, nimiEntry, pealinnEntry, rahvaarvEntry, continentPicker, flagImage }
            };
        }

        private async void ChooseImage_Clicked(object sender, EventArgs e)
        {
            try
            {
                // Проверяем разрешение на чтение из хранилища
                var status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
                if (status != PermissionStatus.Granted)
                {
                    // Разрешение не предоставлено, запрашиваем его
                    status = await Permissions.RequestAsync<Permissions.StorageRead>();
                    if (status != PermissionStatus.Granted)
                    {
                        // Пользователь отклонил запрос на разрешение, обработайте это
                        return;
                    }
                }

                // Разрешение предоставлено, продолжаем с выбором изображения
                var result = await MediaPicker.PickPhotoAsync();
                if (result != null)
                {
                    flagImage.Source = ImageSource.FromStream(() => result.OpenReadAsync().Result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void Kustuta_Clicked(object sender, EventArgs e)
        {
            if (selectedCountry != null)
            {
                var ruhm = Riigid.FirstOrDefault(r => r.Contains(selectedCountry));
                if (ruhm != null)
                {
                    ruhm.Remove(selectedCountry);
                }
            }
        }

        private async void Lisa_Clicked(object sender, EventArgs e)
        {
            string nimi = nimiEntry.Text;
            string pealinn = pealinnEntry.Text;
            int rahvaarv = Convert.ToInt32(rahvaarvEntry.Text);
            var selectedValue = continentPicker.SelectedItem as string;

            var pickResult = await Xamarin.Essentials.MediaPicker.PickPhotoAsync();

            // Проверка выбрано ли значение из continentPicker
            if (string.IsNullOrEmpty(selectedValue))
            {
                await DisplayAlert("Hoiatus", "Valige kontinent", "Ok");
                return;
            }

            // Проверка есть ли в списке
            if (Riigid.Any(r => r.Nimetus.Equals(selectedValue, StringComparison.OrdinalIgnoreCase) && r.Any(c => c.CountryName.Equals(nimi, StringComparison.OrdinalIgnoreCase))))
            {
                await DisplayAlert("Hoiatus", "Selline riik on juba loetelus!", "Ok");
                return;
            }

            var ruhm = Riigid.FirstOrDefault(r => r.Nimetus.Equals(selectedValue, StringComparison.OrdinalIgnoreCase));
            if (ruhm != null)
            {
                ruhm.Add(new Riik { CountryName = nimi, Capital = pealinn, Population = rahvaarv, Continent = selectedValue, Flag = ImageSource.FromStream(() => pickResult.OpenReadAsync().Result) });
            }
            else
            {
                Riigid.Add(new Ruhm<string, Riik>(selectedValue, new List<Riik> { new Riik { CountryName = nimi, Capital = pealinn, Population = rahvaarv, Continent = selectedValue, Flag = ImageSource.FromStream(() => pickResult.OpenReadAsync().Result) } }));
            }
        }

        private async void List_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            selectedCountry = e.Item as Riik;
            if (selectedCountry != null)
            {
                await DisplayAlert("Info", $"Riik: {selectedCountry.CountryName}\nPealinn: {selectedCountry.Capital}\nRahvaarv: {selectedCountry.Population}\nKontinent: {selectedCountry.Continent}", "Ok");
                bool choice = await DisplayAlert("Vali foto", "Soovid muuta telefoni pilti?", "Jah", "Ei");

                if (choice)
                {
                    var pickResult = await MediaPicker.PickPhotoAsync();

                    if (pickResult != null)
                    {
                        selectedCountry.Flag = ImageSource.FromStream(() => pickResult.OpenReadAsync().Result);

                    }
                }

            }
        }
    }
}
