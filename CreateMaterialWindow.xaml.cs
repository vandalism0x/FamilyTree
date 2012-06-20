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
using System.Data.Objects;

namespace FamilyTree
{
    /// <summary>
    /// Логика взаимодействия для AddMaterialWindow.xaml
    /// </summary>
    public partial class CreateMaterialWindow : Window
    {
        Model1Container model1;

        public bool MyDialogResult;
        public int MaterialType_Id;
        public List<Person> PersonsToAdd;

        public CreateMaterialWindow()
        {
            InitializeComponent();

            model1 = new Model1Container();
            PersonsToAdd = new List<Person>();
        }

        private void browseFileButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.Filter = "Текстовые документы(*.DOC;*.DOCX;*.TXT;*.DJVU;*.PDF)|*.DOC;*.DOCX;*.TXT;*.DJVU;*.PDF|Аудио(*.MP3;*.WAV;*.M3U)|*.MP3;*.WAV;*.M3U|Видео(*.AVI;*.MP4;*.MOV)|*.AVI;*.MP4;*.MOV|Изображения(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|Все фаилы (*.*)|*.*";
            ofd.ShowDialog();
            pathTextBox.Text = ofd.FileName;
        }

        private void createMaterialButton_Click(object sender, RoutedEventArgs e)
        {
            if (titleTextBox.Text != "" && pathTextBox.Text != "")
            {
                if (PersonsToAdd.Count == 0)
                {
                    if (MessageBox.Show("Вы не выбрали персон, которым будет принадлежать материал. Добавить материал в общий раздел?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes | MessageBoxResult.No) == MessageBoxResult.Yes)
                    {
                        MyDialogResult = true;
                    }
                }
                else MyDialogResult = true;
                this.Close();
            }
            else MessageBox.Show("Заполнены не все поля");

            string ext = pathTextBox.Text.Substring(pathTextBox.Text.LastIndexOf('.')+1).ToLower();
            if (ext == "doc" || ext == "docx" || ext == "txt" || ext == "djvu" || ext == "pdf") MaterialType_Id = 5;
            else if (ext == "mp3" || ext == "wav" || ext == "m3u") MaterialType_Id = 3;
            else if (ext == "bmp" || ext == "jpg" || ext == "gif") MaterialType_Id = 2;
            else if (ext == "avi" || ext == "mp4" || ext == "mov") MaterialType_Id = 4;
        }

        private void addPersonsButton_Click(object sender, RoutedEventArgs e)
        {
            AddPersonsToMaterialWindow addPersonsToMaterialWindow1 = new AddPersonsToMaterialWindow();
            addPersonsToMaterialWindow1.ShowDialog();

            if (addPersonsToMaterialWindow1.MyDialogResult == true)
            {
                Person p;
                int t;
                
                for (int i = 0; i < addPersonsToMaterialWindow1.SelectedPersonIdList.Count; i++)
                {
                    t = addPersonsToMaterialWindow1.SelectedPersonIdList[i];
                    p = model1.Persons.First(o => o.Id == t);
                    PersonsToAdd.Add(p);
                }
            }
        }
    }
}
