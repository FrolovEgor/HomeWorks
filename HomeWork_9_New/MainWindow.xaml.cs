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

        //Collection of splitted words for the visualization 
        private ObservableCollection<string> _SplitedList = new ObservableCollection<string>();

        //Flags for input clear method
        private bool _ReverseTextCleared = false;
        private bool _DevideTextCleared = false;
        
        public MainWindow()
        {
            InitializeComponent();
            VievSplitedList.ItemsSource = _SplitedList;  //connecting colletion with visualization
            this.Resources["ReversalText"] = "";        //Clear initial text from string resiouce
        }

        //Method for spliting the text
        public string[] SplitStr(string Text)
        {
            return Text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        }

        //When split button pressed clear last collection elements and fill it with new words 
        private void Split_Button_press(object sender, RoutedEventArgs e)
        {

            if (InputField_Devide.Text != null)
            {
                _SplitedList.Clear();
                string[] SplitedText = SplitStr(InputField_Devide.Text);

                if (SplitedText.Length > 0)
                {
                    for (int i = 0; i < SplitedText.Length; i++)
                    {
                        _SplitedList.Add(SplitedText[i]);
                    }
                }
            }
        }

        //When reverse button pressed generate new reversed string
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

        //Run split or reverse methods by pressing enter button
        private void InputField_PressedEnter(object sender, KeyEventArgs e) 
        {
            if (e.Key == Key.Enter) 
            {
                if (TabSelector.SelectedIndex == 0) Split_Button_press(sender, e);
                if (TabSelector.SelectedIndex == 1) Reverse_Button_press(sender, e);
            }
        }


        //Clear the input fields when they are selected first time
        private void InputField_FirstClear(object sender, RoutedEventArgs e)
        {
            if (TabSelector.SelectedIndex == 0 && _DevideTextCleared == false)
            {
                InputField_Devide.Clear();
                _DevideTextCleared = true;
            }

            if (TabSelector.SelectedIndex == 1 && _ReverseTextCleared == false)
            {
                InputField_Reverse.Clear();
                _ReverseTextCleared = true;
            }
        }

        //Close the app
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
