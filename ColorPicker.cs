using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ColorTap
{
    public partial class MainPage : ContentPage
    {
        Button[,] buttonarray;
        double x = 8; //for aligning the buttons horizontally
        double y = 15; //for aligning the buttons vertically
        double width = 25; // button width
        double height = 28; // button heigth
        int rows = 4; // number of rows with bright colors
        int rowstot = 8; // total number of rows
        int blue;
        int green;
        int red;

        public MainPage()
        {
            InitializeComponent();
            buttonarray = new Button[rowstot * 6 + 1, rowstot + 1];
            AddButton(0, 0, 255, 255, 255, 0, 0); // button white in the middle
            for(int i = 0; i < rows; i++) // inside bright colors
            {
                for(int rd = 0; rd <= i; rd++)  // red to yellow
                {
                    red = 255;
                    blue = 255 - (i + 1) * 256 / rows;
                    green = blue + (255 - blue) / (i + 1) * rd;
                    AddButton(1 + rd, i, red, green, blue, -(2 * (i + 1) - rd) * x, -rd * y);
                }
                for(int yel = 0; yel <= i; yel++)  // yellow to green
                {
                    blue = 255 - (i + 1) * 256 / rows;
                    green = 255;
                    red = 255 - (255 - blue) / (i + 1) * yel;
                    AddButton(2 + yel + i, i, red, green, blue, (-(i + 1) + 2 * yel) * x, -(i + 1) * y);
                }
                for(int gr = 0; gr <= i; gr++)  // green to lime
                {
                    green = 255;
                    red = 255 - (i + 1) * 256 / rows;
                    blue = red + (255 - red) / (i + 1) * gr;
                    AddButton(3 + gr + i * 2, i, red, green, blue, ((i + 1) + gr) * x, (gr - i - 1) * y);
                }
                for(int li = 0; li <= i; li++)  // lime to blue
                {
                    blue = 255;
                    red = 255 - (i + 1) * 256 / rows;
                    green = 255 - (255 - red) / (i + 1) * li;
                    AddButton(4 + li + i * 3, i, red, green, blue, ((2 - li) + 2 * i) * x, li * y);
                }
                for(int bl = 0; bl <= i; bl++) // blue to purple
                {
                    blue = 255;
                    green = 255 - (i + 1) * 256 / rows;
                    red = green + (255 - green) / (i + 1) * bl;
                    AddButton(5 + bl + i * 4, i, red, green, blue, (1 + i - 2 * bl) * x, (1 + i) * y);
                }
                for(int pu = 0; pu <= i; pu++)  // purple to red
                {
                    red = 255;
                    green = 255 - (i + 1) * 256 / rows;
                    blue = 255 - (255 - green) / (i + 1) * pu;
                    AddButton(6 + pu + i * 5, i, red, green, blue, -((1 + pu) + i) * x, (1 - pu + i) * y);
                }
            }

            for(int j = rows; j < rowstot; j++) //outside dark colors
            {
                for(int rd1 = 0; rd1 <= j; rd1++)
                {
                    red = 255 - 255 / (rowstot - rows + 1) * (j - rows + 1);
                    blue = 0;
                    green = 0 + red / (rows + 1) * rd1;
                    AddButton(1 + rd1, j, red, green, blue, -(2 * (j + 1) - rd1) * x, -rd1 * y);
                }
                for(int yel1 = 0; yel1 <= j; yel1++)
                {
                    green = 255 - 255 / (rowstot - rows + 1) * (j - rows + 1);
                    red = green - green / (rows + 1) * yel1;
                    blue = 0;
                    AddButton(2 + yel1 + j, j, red, green, blue, (-(j + 1) + 2 * yel1) * x, -(j + 1) * y);
                }
                for(int gr1 = 0; gr1 <= j; gr1++)
                {
                    green = 255 - 255 / (rowstot - rows + 1) * (j - rows + 1);
                    blue = 0 + green / (rows + 1) * gr1;
                    red = 0;
                    AddButton(3 + gr1 + j * 2, j, red, green, blue, ((j + 1) + gr1) * x, (gr1 - j - 1) * y);
                }
                for(int li1 = 0; li1 <= j; li1++)
                {
                    blue = 255 - 255 / (rowstot - rows + 1) * (j - rows + 1);
                    green = blue - blue / (rows + 1) * li1;
                    red = 0;
                    AddButton(4 + li1 + j * 3, j, red, green, blue, ((2 - li1) + 2 * j) * x, li1 * y);
                }
                for(int bl1 = 0; bl1 <= j; bl1++)
                {
                    blue = 255 - 255 / (rowstot - rows + 1) * (j - rows + 1);
                    green = 0;
                    red = 0 + blue / (rows + 1) * bl1;
                    AddButton(5 + bl1 + j * 4, j, red, green, blue, (1 + j - 2 * bl1) * x, (1 + j) * y);
                }
                for(int pu1 = 0; pu1 <= j; pu1++)
                {
                    red = 255 - 255 / (rowstot - rows + 1) * (j - rows + 1);
                    green = 0;
                    blue = red - red / (rows + 1) * pu1;
                    AddButton(6 + pu1 + j * 5, j, red, green, blue, -((1 + pu1) + j) * x, (1 - pu1 + j) * y);
                }
            }

        }

        private void AddButton(int i, int j, int red, int green, int blue, double x1, double y1)
        {
            buttonarray[i, j] = new Button()
            {
                BackgroundColor = Color.FromRgb(red, green, blue),
                TranslationX = x1,
                TranslationY = y1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                HeightRequest = height,
                WidthRequest = width,
            };
            mygrid.Children.Add(buttonarray[i, j]);
            buttonarray[i, j].Clicked += (s, e) => { BtnColor_Click(s, e, buttonarray[i, j].BackgroundColor); };
        }
        private void BtnColor_Click(object sender, EventArgs e, Color btncolor)
        {
            mygrid.BackgroundColor = btncolor;
        }
    }
}

