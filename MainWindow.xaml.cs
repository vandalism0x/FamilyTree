﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Objects.DataClasses;

namespace FamilyTree
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Model1Container model1;
        public ProjectContainer projectContainer1;
        public int ProjectId;

        public MainWindow()
        {
            InitializeComponent();
            model1 = new Model1Container();

            personsTab.Visibility = System.Windows.Visibility.Hidden;
            materialsTab.Visibility = System.Windows.Visibility.Hidden;
            eventsTab.Visibility = System.Windows.Visibility.Hidden;

            projectsListBox.ItemsSource = model1.Projects.ToList();
        }

        private void personsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (personsDataGrid.SelectedIndex != -1)
            {
                EditPersonWindow editPersonWindow1 = new EditPersonWindow();
                var t = personsDataGrid.SelectedItem; int id = (int)t.GetType().GetProperty("Id").GetValue(t, null);
                editPersonWindow1.Person_Id = id;

                editPersonWindow1.ShowDialog();
            }
        }

        private void newProjectButton_Click(object sender, RoutedEventArgs e)
        {
            CreateProjectWindow cpw = new CreateProjectWindow();
            cpw.ShowDialog();

            if (cpw.DialogResult.HasValue && cpw.DialogResult==true)
            {
                Project project1 = new Project();
                project1.Name = cpw.nameTextBox.Text;
                model1.Projects.AddObject(project1);
                model1.SaveChanges();
                ProjectId = model1.Projects.ToList().Last().Id;
                projectsListBox.ItemsSource = model1.Projects;
                
                LoadColumns();
                personsTab.Visibility = System.Windows.Visibility.Visible;
                materialsTab.Visibility = System.Windows.Visibility.Visible;
                eventsTab.Visibility = System.Windows.Visibility.Visible;
                tabControl1.SelectedIndex = 1;
            }
        }

        private void loadProjectButton_Click(object sender, RoutedEventArgs e)
        {
            if (projectsListBox.SelectedIndex != -1) LoadProject();
            else MessageBox.Show("Выберите элемент для загрузки");
        }

        private void personsDataGrid_AutoGeneratedColumns(object sender, EventArgs e)
        { 
            try
            {
                DataGridColumn c = personsDataGrid.Columns.ToList().First(o => o.Header.ToString() == "Id");
                personsDataGrid.Columns.Remove(c);
            }
            catch (InvalidOperationException ex) { }
            if (personsDataGrid.Columns.Count > 3)
            {
                personsDataGrid.Columns.RemoveAt(0);
                personsDataGrid.Columns.RemoveAt(0);
                personsDataGrid.Columns.RemoveAt(0);
            }
        }

        private void projectsListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (projectsListBox.SelectedIndex != -1) LoadProject();
        }

        public void LoadProject()
        {
            personsDataGrid.IsEnabled = true;
            EntityCollection<Person> t_Persons = new EntityCollection<Person>();
            var t = projectsListBox.SelectedItem; ProjectId = ((Project)t).Id; t_Persons = (EntityCollection<Person>)t.GetType().GetProperty("Persons").GetValue(t, null);
            var persons = t_Persons.Select(o => new { Id = o.Id, Фамилия = o.Surname, Имя = o.Name, Отчество = o.Patronymic });
            personsDataGrid.ItemsSource = persons.ToList();
            
            LoadColumns();
            personsTab.Visibility = System.Windows.Visibility.Visible;
            materialsTab.Visibility = System.Windows.Visibility.Visible;
            eventsTab.Visibility = System.Windows.Visibility.Visible;
            tabControl1.SelectedIndex = 1;
        }

        public void LoadColumns()
        {
            try
            {
                if (personsDataGrid.ItemsSource.OfType<object>().Count() == 0)
                {
                    DataGridTextColumn textColumn = new DataGridTextColumn();
                    textColumn.Header = "Фамилия";
                    personsDataGrid.Columns.Add(textColumn);

                    textColumn = new DataGridTextColumn();
                    textColumn.Header = "Имя";
                    personsDataGrid.Columns.Add(textColumn);

                    textColumn = new DataGridTextColumn();
                    textColumn.Header = "Отчество";
                    personsDataGrid.Columns.Add(textColumn);

                    personsDataGrid.IsEnabled = false;
                    personsDataGrid.ItemsSource = new List<Person>() { new Person() { Name = "" } }.Select(o => new { Фамилия = o.Surname, Имя = o.Name, Отчество = o.Patronymic });
                }
            }
            catch (ArgumentNullException ex)
            {
                DataGridTextColumn textColumn = new DataGridTextColumn();
                textColumn.Header = "Фамилия";
                personsDataGrid.Columns.Add(textColumn);

                textColumn = new DataGridTextColumn();
                textColumn.Header = "Имя";
                personsDataGrid.Columns.Add(textColumn);

                textColumn = new DataGridTextColumn();
                textColumn.Header = "Отчество";
                personsDataGrid.Columns.Add(textColumn);

                personsDataGrid.IsEnabled = false;
                personsDataGrid.ItemsSource = new List<Person>() { new Person() { Name = "" } }.Select(o => new { Фамилия = o.Surname, Имя = o.Name, Отчество = o.Patronymic });
            }
        }

        private void deleteProjectButton_Click(object sender, RoutedEventArgs e)
        {
            if (projectsListBox.SelectedIndex != -1)
            {
                var t = projectsListBox.SelectedItem; int Id = (int)t.GetType().GetProperty("Id").GetValue(t, null);
                Project p = model1.Projects.First(o => o.Id == Id);
                model1.Projects.DeleteObject(p);
                model1.SaveChanges();
                projectsListBox.ItemsSource = model1.Projects.ToList();
            }
        }

        private void addPersonButton_Click(object sender, RoutedEventArgs e)
        {
            EditPersonWindow editPersonWindow1 = new EditPersonWindow();
            Person p = new Person();
            //editPersonWindow1.
            model1.Persons.AddObject(p);
            //model1.Projects.First().Persons.Add(p);
        }
    }           
}              
                
                

                
                
                
                