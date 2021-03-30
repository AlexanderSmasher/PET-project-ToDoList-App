using Newtonsoft.Json;
using System.ComponentModel;
using System.IO;
using WPF_ToDo_App.Model;

namespace WPF_ToDo_App.Services
{
    class JSONSaver
    {
        // Путь к файлу
        private readonly string Path;
        // Конструктор пути
        public JSONSaver(string path)
        {
            this.Path = path;
        }
        // Метод загрузки todo листа с файла
        public BindingList<ToDoModel> LoadData()
        {
            bool fileExists = File.Exists(Path);
            // Если по заданому пути файл не существует - то создаем новый
            if (!fileExists)
            {
                File.CreateText(Path).Dispose();
                return new BindingList<ToDoModel>();
            }
            // Открываем файл
            using (var reader = File.OpenText(Path))
            {
                string fileText = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<BindingList<ToDoModel>>(fileText);
            }
        }
        // Метод сохранения todo листа в файл
        public void SaveData(object toDoDataList)
        {
            using (StreamWriter writer = File.CreateText(Path))
            {
                string output = JsonConvert.SerializeObject(toDoDataList);
                writer.Write(output);
            }
        }
    }
}
