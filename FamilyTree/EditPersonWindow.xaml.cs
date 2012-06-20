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
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data;
using System.Data.Objects.DataClasses;

namespace FamilyTree
{
    /// <summary>
    /// Логика взаимодействия для EditPersonWindow.xaml
    /// </summary>
    public partial class EditPersonWindow : Window
    {
        public int Person_Id;
        private Person person;

        Model1Container model1;

        public EditPersonWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            model1 = new Model1Container();
            person = model1.Persons.First(o => o.Id == Person_Id);

            LoadPerson(person);
            LoadMaterials(person);
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            SavePerson(Person_Id);

            this.Close();
        }

        private void SavePerson(int PID)
        {
            model1.Persons.First(o => o.Id == PID).Surname = surnameTextBox.Text;
            model1.Persons.First(o => o.Id == PID).Name = nameTextBox.Text;
            model1.Persons.First(o => o.Id == PID).Patronymic = patronymicTextBox.Text;
            model1.Persons.First(o => o.Id == PID).BirthDate = birthDateTextBox.Text;
            model1.Persons.First(o => o.Id == PID).PlaceOfBirth = placeOfBirthTextBox.Text;
            model1.Persons.First(o => o.Id == PID).DeathDate = deathDateTextBox.Text;
            model1.Persons.First(o => o.Id == PID).CauseOfDeath = causeOfDeathTextBox.Text;
            model1.Persons.First(o => o.Id == PID).PlaceOfDeath = placeOfDeathTextBox.Text;
            model1.Persons.First(o => o.Id == PID).Nationality = nationalityTextBox.Text;
            model1.Persons.First(o => o.Id == PID).Physique = physiqueTextBox.Text;
            model1.Persons.First(o => o.Id == PID).MentalHealth = mentalHealthTextBox.Text;
            model1.Persons.First(o => o.Id == PID).Religion = religionTextBox.Text;
            model1.Persons.First(o => o.Id == PID).PoliticalViews = politicalViewsTextBox.Text;
            model1.Persons.First(o => o.Id == PID).Education = educationTextBox.Text;
            model1.Persons.First(o => o.Id == PID).Rank = rankTextBox.Text;
            model1.Persons.First(o => o.Id == PID).Rewards = rewardsTextBox.Text;
            model1.Persons.First(o => o.Id == PID).FinancialPosition = financialPositionTextBox.Text;
            model1.Persons.First(o => o.Id == PID).Gender = genderComboBox.SelectedIndex;

            if (model1.Persons.First(o => o.Id == PID).Materials.Where(o => o.MaterialType_Id == 1).Count() > 0)
            {
                if (image1.Tag == null) model1.Materials.DeleteObject(model1.Persons.First(o => o.Id == PID).Materials.First(o => o.MaterialType_Id == 1));
                else if (image1.Tag != "db")
                {
                    byte[] buffer = File.ReadAllBytes(image1.Tag.ToString());
                    model1.Persons.First(o => o.Id == PID).Materials.First(o => o.MaterialType_Id == 1).Data = buffer;
                }
            }
            else if (image1.Tag != null && image1.Tag.ToString()!= "db")
            {
                Material m = new Material();
                byte[] buffer = File.ReadAllBytes(image1.Tag.ToString());
                m.Data = buffer;
                m.Name = "Фотография карточки";
                m.Persons.Add(model1.Persons.First(o => o.Id == PID));
                m.MaterialType_Id = 1;
                m.FileName = image1.Tag.ToString().Substring(image1.Tag.ToString().LastIndexOf('\\')+1);
                m.MaterialTypes = model1.MaterialTypes.First(o => o.Id == m.MaterialType_Id);
                model1.Materials.AddObject(m);
            }
            model1.SaveChanges();
        }

        private void LoadPerson(Person p)
        {
            genderComboBox.Items.Add("?");
            genderComboBox.Items.Add("Мужской");
            genderComboBox.Items.Add("Женский");

            surnameTextBox.Text = p.Surname;
            nameTextBox.Text = p.Name;
            patronymicTextBox.Text = p.Patronymic;
            birthDateTextBox.Text = p.BirthDate;
            placeOfBirthTextBox.Text = p.PlaceOfBirth;
            deathDateTextBox.Text = p.DeathDate;
            causeOfDeathTextBox.Text = p.CauseOfDeath;
            placeOfDeathTextBox.Text = p.PlaceOfDeath;
            nationalityTextBox.Text = p.Nationality;
            physiqueTextBox.Text = p.Physique;
            mentalHealthTextBox.Text = p.MentalHealth;
            religionTextBox.Text = p.Religion;
            politicalViewsTextBox.Text = p.PoliticalViews;
            educationTextBox.Text = p.Education;
            rankTextBox.Text = p.Rank;
            rewardsTextBox.Text = p.Rewards;
            financialPositionTextBox.Text = p.FinancialPosition;
            genderComboBox.SelectedIndex = Convert.ToInt32(p.Gender);

            if (model1.Persons.First(o => o.Id == p.Id).Materials.Where(o => o.MaterialType_Id == 1).Count() > 0)
            {
                Material photo = p.Materials.First(o => o.MaterialType_Id == 1);
                MemoryStream byteStream = new MemoryStream(photo.Data);
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = byteStream;
                image.EndInit();
                image1.Source = image;
                image1.Tag = "db";
            }
            else
            {
                Image _image = new Image();
                string packUri = "";
                
                switch (genderComboBox.SelectedIndex)
                {
                    case 0:
                        packUri = "pack://application:,,,/FamilyTree;component/Images/unknown.png";
                        break;
                    case 1:
                        packUri = "pack://application:,,,/FamilyTree;component/Images/man.png";
                        break;
                    case 2:
                        packUri = "pack://application:,,,/FamilyTree;component/Images/woman.png";
                        break;
                }
                _image.Source = new ImageSourceConverter().ConvertFromString(packUri) as ImageSource;
                image1.Source = _image.Source;
            }
        }

        private void LoadMaterials(Person p)
        {
            materialsDataGrid.ItemsSource = p.Materials.Select(o => new { Id = o.Id, Название = o.Name, Описание = o.Description, Data = o.Data, }).ToList();

            string path = Environment.CurrentDirectory + "\\tmp_materials";
            Directory.CreateDirectory(path);
            DirectoryInfo dinfo = new DirectoryInfo(path);
            dinfo.Attributes = FileAttributes.Directory | FileAttributes.Hidden | FileAttributes.System;
            dinfo.Create();
        }

        private void addMaterialButton_Click(object sender, RoutedEventArgs e)
        {
            CreateMaterialWindow createMaterialWindow1 = new CreateMaterialWindow();
            createMaterialWindow1.ShowDialog();

            if (createMaterialWindow1.MyDialogResult == true)
            {
                byte[] buffer = File.ReadAllBytes(createMaterialWindow1.pathTextBox.Text);
                Material m = new Material();
                m.Name = createMaterialWindow1.titleTextBox.Text;
                m.Description = createMaterialWindow1.descriptionTextBox.Text;
                m.Data = buffer;
                
                m.MaterialType_Id = createMaterialWindow1.MaterialType_Id;
                m.MaterialTypes = model1.MaterialTypes.First(o => o.Id == m.MaterialType_Id);
                m.FileName = createMaterialWindow1.pathTextBox.Text.Substring(createMaterialWindow1.pathTextBox.Text.LastIndexOf('\\') + 1);
                foreach (Person x in createMaterialWindow1.PersonsToAdd)
                {
                    m.Persons.Add(x);
                }
                Person p; p = model1.Persons.First(o => o.Id == Person_Id);
                materialsDataGrid.ItemsSource = p.Materials.Select(o => new { Id = o.Id, Название = o.Name, Описание = o.Description, Data = o.Data, }).ToList();
            }
        }

        private void browsePhotoButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.Filter = "Изображения(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF";
            ofd.ShowDialog();

            if (File.Exists(ofd.FileName))
            {
                var uriSource = new Uri(ofd.FileName);
                image1.Source = new BitmapImage(uriSource);

                image1.Tag = ofd.FileName;
            }
        }

        private void clearPhotoButton_Click(object sender, RoutedEventArgs e)
        {
            Image _image = new Image();
            string packUri = "";

            switch (genderComboBox.SelectedIndex)
            {
                case 0:
                    packUri = "pack://application:,,,/FamilyTree;component/Images/unknown.png";
                    break;
                case 1:
                    packUri = "pack://application:,,,/FamilyTree;component/Images/man.png";
                    break;
                case 2:
                    packUri = "pack://application:,,,/FamilyTree;component/Images/woman.png";
                    break;
            }
            _image.Source = new ImageSourceConverter().ConvertFromString(packUri) as ImageSource;
            image1.Source = _image.Source;
            image1.Tag = null;
        }

        private void genderComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (image1.Tag == null)
            {
                Image _image = new Image();
                string packUri = "";

                switch (genderComboBox.SelectedIndex)
                {
                    case 0:
                        packUri = "pack://application:,,,/FamilyTree;component/Images/unknown.png";
                        break;
                    case 1:
                        packUri = "pack://application:,,,/FamilyTree;component/Images/man.png";
                        break;
                    case 2:
                        packUri = "pack://application:,,,/FamilyTree;component/Images/woman.png";
                        break;
                }
                _image.Source = new ImageSourceConverter().ConvertFromString(packUri) as ImageSource;
                image1.Source = _image.Source;
            }
        }

        private void removeMaterialButton_Click(object sender, RoutedEventArgs e)
        {
            if (materialsDataGrid.SelectedIndex != -1)
            {
                Material m = model1.Materials.OrderBy(o => o.Id).Skip(materialsDataGrid.SelectedIndex).First();
                model1.Materials.DeleteObject(m);
                model1.SaveChanges();
                materialsDataGrid.ItemsSource = model1.Persons.First(o=>o.Id == Person_Id).Materials.Select(o => new { Id = o.Id, Название = o.Name, Описание = o.Description, Data = o.Data, }).ToList();
            }
            else MessageBox.Show("Выберите элемент для удаления");
        }

        private void openMaterialButton_Click(object sender, RoutedEventArgs e)
        {
            var t = ((Grid)(((Button)sender).Parent)).DataContext; int id = (int)t.GetType().GetProperty("Id").GetValue(t,null);
            Material m = model1.Materials.First(o => o.Id == id);
            string fileName = m.FileName;
            string fullPath = Environment.CurrentDirectory + "\\tmp_materials\\" + fileName;
            if(!File.Exists(fullPath))
            {
                FileStream fs = File.Create(fullPath);
                fs.Write(m.Data, 0, m.Data.Length);
                fs.Close();
            }
            System.Diagnostics.Process.Start(fullPath);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Directory.Delete(Environment.CurrentDirectory + "\\tmp_materials");
        }

        private void materialsDataGrid_AutoGeneratedColumns(object sender, EventArgs e)
        {
            if (materialsDataGrid.Columns.Count > 2)
            {
                materialsDataGrid.Columns.RemoveAt(0);
                materialsDataGrid.Columns.RemoveAt(2);
            }
        }
        public void RefreshItemsSource(DataGrid dataGrid)
        {
            var t = dataGrid.ItemsSource;
            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = t;
        }
    }
}
