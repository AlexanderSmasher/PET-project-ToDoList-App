using System;
using System.ComponentModel;

namespace WPF_ToDo_App.Model
{
    public class ToDoModel : INotifyPropertyChanged
    {
        // Дата создания записи todo
        public string CreationDate { get; set; } = DateTime.Now.ToShortDateString();
        // Время создание записи todo
        public string CreationTime { get; set; } = DateTime.Now.ToShortTimeString();
        // Логический чекбокс записи todo
        private bool IsDone;
        // Сама строка todo листа
        private string TaskToDo;

        // Элемент проверки изменений в строках .xaml <DataGrid>
        public event PropertyChangedEventHandler PropertyChanged;
        // Вызов события изменения в строке todo
        protected virtual void OnPropertyChanged(string propertyName = "")
        {
            // Проверка на null
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        // Геттер и сеттер логического чекбокса
        public bool isDone
        {
            get { return this.IsDone; }
            set 
            {
                if (IsDone == value)
                    return;
                IsDone = value;
                OnPropertyChanged("IsDone");
            }
        }
        // Геттер и сеттер строки todo
        public string taskToDo
        {
            get { return this.TaskToDo; }
            set 
            {
                if (TaskToDo == value)
                    return;
                TaskToDo = value;
                OnPropertyChanged("Text");
            }
        }
    }
}
