using System;
using System.Data;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (UIElement element in Ordinary.Children)
            {
                if (element is Button)
                    ((Button)element).Click += Button_Click;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Calculate((sender as Button).Content.ToString());
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            Key[] keys = { Key.NumPad0, Key.NumPad1, Key.NumPad2, Key.NumPad3, Key.NumPad4, Key.NumPad5, Key.NumPad6, Key.NumPad7, Key.NumPad8, Key.NumPad9, Key.D0, Key.D1, Key.D2,
                            Key.D3, Key.D4, Key.D5, Key.D6, Key.D7, Key.D8, Key.D9, Key.Add, Key.Subtract, Key.Divide, Key.Multiply, Key.Decimal, Key.Back};

            string[] keysR = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "+", "-", "/", "*", ",", "<--" };

            for (int i = 0; i < keys.Length; i++)
            {
                if (e.Key == keys[i])
                {
                    Calculate(keysR[i]);
                    return;
                }
            }
        }

        void Calculate(string key)
        {
            switch (key)
            {
                case "=":
                    try
                    {
                        UpAndDownClear();
                        Up.Text += Down.Text;
                        Down.Text = new DataTable().Compute(Up.Text, null).ToString().Replace(',', '.');
                        Up.Text += "=";
                    }
                    catch
                    {
                        Up.Clear();
                        Down.FontSize = 30;
                        Down.Text = "Неверные данные";
                    }
                    break;

                case "√x":
                    UpAndDownClear();
                    if (Down.Text == "Неверные данные") return;
                    Up.Text += "Math.Sqrt(" + Down.Text + ")";
                    Down.Text = Math.Sqrt(Convert.ToDouble(Down.Text.Replace('.', ','))).ToString().Replace(',', '.');
                    break;

                case "x^2":
                    UpAndDownClear();
                    if (Down.Text == "Неверные данные") return;
                    Up.Text += "Math.Pow(" + Down.Text + ")";
                    Down.Text = Math.Pow(Convert.ToDouble(Down.Text.Replace('.', ',')), 2).ToString().Replace(',', '.');
                    break;

                case "+/-":
                    if (Down.Text == "Неверные данные") return;
                    Down.Text = (Convert.ToDouble(Down.Text.Replace('.', ',')) * -1).ToString().Replace(',', '.');
                    break;

                case "1/x":
                    if (Down.Text == "Неверные данные") return;
                    Up.Text += "1/" + Down.Text;
                    Down.Text = (1 / Convert.ToDouble(Down.Text.Replace('.', ','))).ToString().Replace(',', '.');
                    break;

                case "%":
                    if (Down.Text == "Неверные данные") return;
                    UpAndDownClear();
                    Down.Text = (Convert.ToDouble(Down.Text) / 100).ToString().Replace(',', '.');
                    break;

                case "/":
                case "*":
                case "-":
                case "+":
                    if (Down.Text == "Неверные данные") return;
                    UpAndDownClear();
                    Up.Text += Down.Text + key;
                    Down.Text = "0";
                    break;

                case "C":
                    Up.Clear();
                    Down.Text = "0";
                    break;

                case "CE":
                    Down.Text = "0";
                    break;

                case "<--":
                    Down.Text = Down.Text.Substring(0, Down.Text.Length - 1);
                    if (Down.Text.Length == 0) Down.Text = "0";
                    break;

                case ",":
                    if (Down.Text == "Неверные данные") return;
                    Down.Text += ".";
                    break;

                default:
                    UpAndDownClear();
                    if (Down.Text == "Неверные данные") return;

                    if (Down.Text == "0")
                    {
                        Down.Text = key;
                        return;
                    }

                    Down.Text += key;

                    break;
            }
        }

        void UpAndDownClear()
        {
            if (Up.Text.Contains("=")) Up.Clear();

            if (Up.Text.Contains("Math")) Up.Clear();
        }

        private void Developer_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo { FileName = @"https://p0b0t.neocities.org", UseShellExecute = true });
        }

    }
}
