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
using System.Data.Objects;

namespace FamilyTree
{
    /// <summary>
    /// Логика взаимодействия для AddPersonsToMaterial.xaml
    /// </summary>
    public partial class AddPersonsToMaterialWindow : Window
    {
        Model1Container model1;

        public bool MyDialogResult;
        public List<int> SelectedPersonIdList, DeselectedPersonIdList;

        public AddPersonsToMaterialWindow()
        {
            InitializeComponent();

            SelectedPersonIdList = new List<int>();
            DeselectedPersonIdList = new List<int>();

            model1 = new Model1Container();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string t;
            var persons = from p in model1.Persons orderby p.Id select p;
            foreach (var p in persons)
            {
                t = p.Surname + " " + p.Name + " " + p.Patronymic;
                deselectedPersonsListBox.Items.Add(t);
                DeselectedPersonIdList.Add(p.Id);
            }
        }

        private void addPersonsButton_Click(object sender, RoutedEventArgs e)
        {
            //if (personsListBox.SelectedItems.Count > 0)
            {
                MyDialogResult = true;
                this.Close();
            }
            //else MessageBox.Show("Выберите, хотя-бы, один элемент для добавления");
        }

        private void selectPersonButton_Click(object sender, RoutedEventArgs e)
        {
            if (deselectedPersonsListBox.SelectedIndex != -1)
            {
                int index = deselectedPersonsListBox.SelectedIndex;
                int id = DeselectedPersonIdList[index];
                string item = deselectedPersonsListBox.SelectedItem.ToString();

                deselectedPersonsListBox.Items.RemoveAt(index);
                DeselectedPersonIdList.RemoveAt(index);

                selectedPersonsListBox.Items.Add(item);
                SelectedPersonIdList.Add(id);
            }
        }

        private void deselectPersonButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedPersonsListBox.SelectedIndex != -1)
            {
                int index = selectedPersonsListBox.SelectedIndex;
                int id = SelectedPersonIdList[index];
                string item = selectedPersonsListBox.SelectedItem.ToString();

                selectedPersonsListBox.Items.RemoveAt(index);
                SelectedPersonIdList.RemoveAt(index);

                deselectedPersonsListBox.Items.Add(item);
                DeselectedPersonIdList.Add(id);
            }
        }
    }
}
