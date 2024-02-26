using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static System.Net.WebRequestMethods;

namespace BohatyrovMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class tripstrapstrull : ContentPage
    {
        int counter = 0;
        Label counterlbl;
        Grid grid, bottomgrid;
        Random random = new Random();
        Frame[,] frames;
        bool isRedTurn;
        Button reset, firstMoveButton, botBtn;
        bool isBot = false;
    


        public tripstrapstrull()
        {
            grid = new Grid
            {
                BackgroundColor = Color.LightBlue,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };
            counterlbl = new Label
            {
                BackgroundColor = Color.LightBlue,
                FontSize = 12,
                TextColor = Color.Black,
                Text = "Counter",
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };

        

            isRedTurn = random.Next(2)==1;

            // Создаем сетку с фреймами
            frames = new Frame[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Frame frame = new Frame
                    {
                        BackgroundColor = Color.Black,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Margin = 5,
                        
                    };
                    frame.GestureRecognizers.Add(new TapGestureRecognizer { CommandParameter = (i, j), Command = new Command(OnFrameTapped) });

                    frames[i, j] = frame;
                    grid.Children.Add(frame, i, j);
                }
            }
            reset = new Button
            {
                BackgroundColor = Color.LightBlue,
                FontSize = 32,
                TextColor = Color.Black,
                Text = "Reset",
                BorderWidth = 0,
                
           
           
                

            };
            reset.Clicked += Reset_Clicked;

            firstMoveButton = new Button
            {
                BackgroundColor= Color.LightBlue,
                FontSize = 26,
                TextColor = Color.Black,
                BorderWidth= 0,
                Text = "Esimene käik",
             

            };
            firstMoveButton.Clicked += FirstMoveButton_Clicked;


            Grid bottomGrid = new Grid
            {
                BackgroundColor = Color.LightBlue,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions = { new RowDefinition { Height = GridLength.Star } }, //  высота
                ColumnDefinitions = { new ColumnDefinition { Width = GridLength.Star } }, //  ширина столбца
               
                
    
            };
            botBtn = new Button
            {
                BackgroundColor = Color.LightBlue,
                FontSize = 26,
                TextColor = Color.Black,
                BorderWidth = 0,
                Text = "BOT",

            };
            botBtn.Clicked += BotBtn_Clicked;


            bottomGrid.Children.Add(firstMoveButton, 0, 0);
            bottomGrid.Children.Add(reset, 1, 0);
         


            StackLayout st = new StackLayout
            {
                BackgroundColor = Color.LightBlue,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = { grid, bottomGrid, counterlbl, botBtn }
            };
            Content = st;

    



        }

        private void BotBtn_Clicked(object sender, EventArgs e)
        {
            isBot = true;
            int botTurn = (isRedTurn ? 0 : 1); // 0 for Red, 1 for Green
            isRedTurn = random.Next(2) == 1;
            frames[1, botTurn].BackgroundColor = isRedTurn ? Color.Red : Color.Green;
        }
        

        private async void FirstMoveButton_Clicked(object sender, EventArgs e)
        {
            string firstMove = await DisplayActionSheet("Vali, kes teeb esimese käigu", null, null, "Punane", "Roheline");
            if (firstMove == "Punane")
            {
                isRedTurn = true;
            }
            else if (firstMove == "Roheline")
            {
                isRedTurn = false;
            }
        }

        private void Reset_Clicked(object sender, EventArgs e)
        {
            isBot = false;
            counter = 0;
            isRedTurn = random.Next(2) == 1;
            foreach (Frame frame in frames)
            {
                frame.BackgroundColor = Color.Black;
            }

        }

        private void ResetGame() {
            isBot = false;
            counter = 0;
            isRedTurn = random.Next(2) == 1;
            foreach (Frame frame in frames)
            {
                frame.BackgroundColor = Color.Black;
            }

        }

        private bool CheckForWin(Color color)
        {
            // Проверяем по горизонтали
            for (int i = 0; i < 3; i++)
            {
                if (frames[i, 0].BackgroundColor == color &&
                    frames[i, 1].BackgroundColor == color &&
                    frames[i, 2].BackgroundColor == color)
                {
                    return true;
                }
            }

            // Проверяем по вертикали
            for (int i = 0; i < 3; i++)
            {
                if (frames[0, i].BackgroundColor == color &&
                    frames[1, i].BackgroundColor == color &&
                    frames[2, i].BackgroundColor == color)
                {
                    return true;
                }
            }

            // Проверяем по диагоналям
            if (frames[0, 0].BackgroundColor == color &&
                frames[1, 1].BackgroundColor == color &&
                frames[2, 2].BackgroundColor == color)
            {
                return true;
            }
            if (frames[0, 2].BackgroundColor == color &&
                frames[1, 1].BackgroundColor == color &&
                frames[2, 0].BackgroundColor == color)
            {
                return true;
            }

            return false;
        }
        void Count()
        {
          
            counter++;
            counterlbl.Text = "Counter: " + counter.ToString();




        }

        private void BotMove()
        {
            int botTurn = (isRedTurn ? 0 : 1); // цвет, который бот будет использовать
            bool moveMade = false;

           // случ ход
            while (!moveMade)
            {
                int randomX = random.Next(3);
                int randomY = random.Next(3);

             
                if (frames[randomX, randomY].BackgroundColor == Color.Black)
                {
                    frames[randomX, randomY].BackgroundColor = isRedTurn ? Color.Red : Color.Green;
                    moveMade = true;
                }
            }

            isRedTurn = !isRedTurn; 
        }


        // Метод вызывается при нажатии на фрейм
        private async void OnFrameTapped(object parameter)
        {
           
            Count();
            (int x, int y) = ((int, int))parameter;
            Frame frame = frames[x, y];

            if (frame.BackgroundColor == Color.Black) // Проверяем, что ячейка пустая
            {
  
                if (isRedTurn)
                {
                    frame.BackgroundColor = Color.Red;
                }
                else
                {
                    frame.BackgroundColor = Color.Green; 
                }

                isRedTurn = !isRedTurn; // Меняем очередь игрока

                if (isBot==true)
                {
                    BotMove();
                }
            }

            if (CheckForWin(Color.Red))
            {
          
               await DisplayAlert("Trips traps trull", "Punane võidab", "OK");
        
                    frame.BackgroundColor = Color.Black;
                ResetGame();

            }
            else if (CheckForWin(Color.Green))
            {

                await DisplayAlert("Trips traps trull", "Rohelised võitsid", "OK");

                ResetGame();
                
            }
            else if (counter==9)
            {
          
                await DisplayAlert("Trips traps trull", "Nichiya", "OK");

                ResetGame();

            }
           
        }
    }
}