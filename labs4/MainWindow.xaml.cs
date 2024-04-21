using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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

namespace labs4
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string path = "text1.txt";
        public MainWindow()
        {
            InitializeComponent();
            LoadNotes();
            LoadFontSizes();
            LoadFontFamilies();
            LoadColors();
        }
        private void LoadNotes()
        {
            if (File.Exists("Notes.txt"))
            {
                string text = File.ReadAllText("Notes.txt");
                textBox.Text = text;
            }
        }

        private void SaveNotes()
        {
            string textToSave = textBox.Text.Replace("\n", Environment.NewLine).Trim();
            File.WriteAllText("Notes.txt", textToSave);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveNotes();
        }

        private void comboBoxFontFamily_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (comboBoxFontFamily.SelectedItem != null)
            {
                string fontFamily = comboBoxFontFamily.SelectedItem.ToString();
                textBox.FontFamily = new FontFamily(fontFamily);
            }
        }

        private void comboBoxFontSize_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (comboBoxFontSize.SelectedItem != null)
            {
                double fontSize;
                if (double.TryParse(comboBoxFontSize.SelectedItem.ToString(), out fontSize))
                {
                    textBox.FontSize = fontSize;
                }
            }
        }

        private void comboBoxColor_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (comboBoxColor.SelectedItem != null)
            {
                string colorName = comboBoxColor.SelectedItem.ToString();
                textBox.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(colorName));
            }
        }

        // Метод для загрузки доступных размеров шрифта
        private void LoadFontSizes()
        {
            for (int i = 8; i <= 24; i += 2)
            {
                comboBoxFontSize.Items.Add(i.ToString());
            }
            comboBoxFontSize.SelectedItem = "12"; // Устанавливаем значение по умолчанию
        }

        // Метод для загрузки доступных семейств шрифта
        private void LoadFontFamilies()
        {
            foreach (FontFamily fontFamily in Fonts.SystemFontFamilies)
            {
                comboBoxFontFamily.Items.Add(fontFamily.ToString());
            }
            comboBoxFontFamily.SelectedItem = "Arial"; // Устанавливаем значение по умолчанию
        }

        // Метод для загрузки доступных цветов
        private void LoadColors()
        {
            typeof(Colors).GetProperties().ToList().ForEach(prop => comboBoxColor.Items.Add(prop.Name));
            comboBoxColor.SelectedItem = "White"; // Устанавливаем значение по умолчанию
        }
    }
}
