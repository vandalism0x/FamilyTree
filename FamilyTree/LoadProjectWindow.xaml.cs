using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FamilyTree
{
    /// <summary>
    /// Логика взаимодействия для LoadProjectWindow.xaml
    /// </summary>
    public partial class LoadProjectWindow : Window
    {
        public bool Load = false;
        Model1Container model1;

        public LoadProjectWindow()
        {
            InitializeComponent();
            model1 = new Model1Container();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            projectsListBox.ItemsSource = model1.Projects;
        }

        private void loadProjectButton_Click(object sender, RoutedEventArgs e)
        {
            Load = true;
            this.Close();
        }

        private void projectsListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (projectsListBox.SelectedIndex != -1)
            {
                Load = true;
                this.Close();
            }
        }
    }
}
