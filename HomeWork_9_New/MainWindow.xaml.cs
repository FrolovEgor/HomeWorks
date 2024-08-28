using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace HomeWork_9_New
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string[] SplitStr(string Text)
        {
            return Text.Split(' ');
        }

        private ObservableCollection<string> SplitedList = new ObservableCollection<string>();

        public MainWindow()
        {
            InitializeComponent();
            VievSplitedList.ItemsSource = SplitedList;
        }

        private void Split_Button_press(object sender, RoutedEventArgs e)
        {
            
            if (InputField_Devide.Text != null)
            {
                SplitedList.Clear();
                string[] SplitedText = SplitStr(InputField_Devide.Text);

                if (SplitedText.Length > 0)
                {
                    for (int i = 0; i < SplitedText.Length; i++)
                    {
                        SplitedList.Add(SplitedText[i]);
                    }

                }
            }
        }

        private void Split_Button_enter(object sender, KeyEventArgs e) 
        {
            if (e.Key == Key.Enter) 
            {
                Split_Button_press(sender, e);
            }
        }

        private void Reverse_Button_press(object sender, RoutedEventArgs e)
        {
            string FieldText = InputField_Reverse.Text;

            if (FieldText != null)
            {
                string[] SplitedStr = SplitStr(FieldText);
                StringBuilder ReversedText = new StringBuilder();

                for (int i = SplitedStr.Length - 1; i >= 0; i--)
                {
                    ReversedText.Append(SplitedStr[i]);
                    ReversedText.Append(" ");
                }
                this.Resources["ReversalText"] = ReversedText.ToString();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void InputField_Devide_TextChanged(object sender, TextChangedEventArgs e)
        {
        
        }

    }
}
