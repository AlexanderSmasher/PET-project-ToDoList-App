using System;
using System.ComponentModel;
using System.Windows;
using WPF_ToDo_App.Model;
using WPF_ToDo_App.Services;

namespace WPF_ToDo_App
{
    public partial class MainWindow : Window
    {
        // Путь создания/читания файла
        private readonly string Path = $"{Environment.CurrentDirectory}\\ToDoData.json";
        //  Лист с моделью todo
        private BindingList<ToDoModel> ToDoData;
        // Логика записи todo листа на файл .json
        private JSONSaver FileJSON;
        // Состояние окна (свернуто или развернуто)
        private WindowState prevState;
        // Стандартный конструктор
        public MainWindow()
        {
            InitializeComponent();
        }
        // Загрузка todo листа с файла в пользовательский интерфейс
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FileJSON = new JSONSaver(Path);
            try
            {
                ToDoData = FileJSON.LoadData();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                Close();
            }
            dataGridToDoList.ItemsSource = ToDoData;
            ToDoData.ListChanged += ToDoDataListChanged;
        }
        // Запись изменений в файл
        private void ToDoDataListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemDeleted || e.ListChangedType == ListChangedType.ItemChanged)
            {
                try
                {
                    FileJSON.SaveData(sender);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Close();
                }
            }
        }
        // Свертывание окна в трей
        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
                Hide();
            else
                prevState = WindowState;
        }
        // Развертывание окна по нажатию на иконку в трее
        private void TaskbarIcon_TrayLeftMouseDown(object sender, RoutedEventArgs e)
        {
            Show();
            WindowState = prevState;
        }
        // Закрытие в трей меню
        private void TrayMenu(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
