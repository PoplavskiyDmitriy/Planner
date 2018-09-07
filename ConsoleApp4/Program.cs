using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
namespace MyProgect
{
    class Program
    {
        interface IPlanningTasks
        {
            List<MyTaskClass> Tasks { get; set; }
            void AddTask();
            void DeleteTask(int id);
            void SearchAndEditTask(int id);
        }
        class TasksPlanner : IPlanningTasks
        {
            public List<MyTaskClass> Tasks { get; set; }
            public int lastId { get; set; }
            public TasksPlanner(List<MyTaskClass> register)
            {
                Tasks = register;
                if (Tasks.Count > 0)
                {
                    lastId = Tasks.Last().Id;
                }
                else
                {
                    lastId = 0;
                }
            }
            public void AddTask()
            {
                Console.Clear();
                Console.WriteLine("Введите дату начала выполнеия (дд.мм.гггг):");
                string enteredData = Console.ReadLine();
                string[] date = enteredData.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                Console.WriteLine("Введите время начала выполнения (чч:мм)");
                enteredData = Console.ReadLine();
                string[] time = enteredData.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                DateTime startedDateTime = new DateTime();
                try
                {
                    startedDateTime = new DateTime(Convert.ToInt16(date[2]), Convert.ToInt16(date[1]), Convert.ToInt16(date[0]), Convert.ToInt16(time[0]), Convert.ToInt16(time[1]), Convert.ToInt16(0));
                }
                catch (System.ArgumentOutOfRangeException e)
                {
                    if (e.Source != null)
                    {
                        Console.WriteLine("Проверьте введенные данные");
                        Console.ReadLine();
                        return;
                    }
                }
                Console.WriteLine("Введите название задачи:");
                string taskName = Console.ReadLine();
                Console.WriteLine("Введите описание задачи:");
                string taskDescription = Console.ReadLine();
                Console.WriteLine("Введите дату окончания выполнеия (дд.мм.гггг):");
                enteredData = Console.ReadLine();
                date = enteredData.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                Console.WriteLine("Введите время окончания выполнения (чч:мм)");
                enteredData = Console.ReadLine();
                time = enteredData.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                DateTime completionDateTime = new DateTime();
                try
                {
                    completionDateTime = new DateTime(Convert.ToInt16(date[2]), Convert.ToInt16(date[1]), Convert.ToInt16(date[0]), Convert.ToInt16(time[0]), Convert.ToInt16(time[1]), Convert.ToInt16(0));
                }
                catch (System.ArgumentOutOfRangeException e)
                {
                    if (e.Source != null)
                    {
                        Console.WriteLine("Проверьте введенные данные");
                        Console.ReadLine();
                        return;
                    }
                }
                lastId += 1;
                MyTaskClass newTask = new MyTaskClass(startedDateTime, taskName, taskDescription, completionDateTime, lastId);
                Tasks.Add(newTask);

            }
            public void DeleteTask(int id)
            {
                foreach (MyTaskClass task in Tasks)
                {
                    if (task.Id == id)
                    {
                        Tasks.Remove(task);
                        Console.WriteLine("Задача " + id + " была успешно удалена");
                        break;
                    }
                }
            }
            public void SearchAndEditTask(int id)
            {
                foreach (MyTaskClass task in Tasks)
                {
                    if (id == task.Id)
                    {
                        char menuOption = ' ';
                        while (menuOption != '0')
                        {
                            Console.WriteLine("Выберите пункт для редактирования:");
                            Console.WriteLine("1 - Дата и время начала выполнения");
                            Console.WriteLine("2 - Название задачи");
                            Console.WriteLine("3 - Описание задачи");
                            Console.WriteLine("4 - Дата и время окончания выполнения");
                            Console.WriteLine("0 - В главное меню");
                            try
                            {
                                menuOption = Convert.ToChar(Console.ReadLine());
                            }
                            catch (System.FormatException e)
                            {
                                if (e.Source != null)
                                {
                                    Console.WriteLine("Проверьте правильность введенных данных");
                                }
                            }
                            switch (menuOption)
                            {
                                case '1':
                                    Console.Clear();
                                    Console.WriteLine("Введите дату и время начала выполнения в формате дд.мм.гггг чч:мм");
                                    string enteredData = Console.ReadLine();
                                    string[] data = enteredData.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                    string[] startDate = data[0].Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                                    string[] startTime = data[1].Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                                    DateTime startDateTime = new DateTime();
                                    try
                                    {
                                        startDateTime = new DateTime(Convert.ToInt16(startDate[2]), Convert.ToInt16(startDate[1]), Convert.ToInt16(startDate[0]), Convert.ToInt16(startTime[0]), Convert.ToInt16(startTime[1]), Convert.ToInt16(0));
                                    }
                                    catch (System.ArgumentOutOfRangeException e)
                                    {
                                        if (e.Source != null)
                                        {
                                            Console.WriteLine("Проверьте введенные данные");
                                            Console.ReadLine();
                                            return;
                                        }
                                    }
                                    task.StartedDateTime = startDateTime;
                                    break;
                                case '2':
                                    Console.Clear();
                                    Console.WriteLine("Введите новое название задачи:");
                                    task.TaskName = Console.ReadLine();
                                    break;
                                case '3':
                                    Console.Clear();
                                    Console.WriteLine("Введите новое описание задачи:");
                                    task.TaskDescription = Console.ReadLine();
                                    break;
                                case '4':
                                    Console.Clear();
                                    Console.WriteLine("Введите дату и время начала выполнения в формате дд.мм.гггг чч:мм");
                                    enteredData = Console.ReadLine();
                                    data = enteredData.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                    string[] endDate = data[0].Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                                    string[] endTime = data[1].Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                                    DateTime endDateTime = new DateTime();
                                    try
                                    {
                                        endDateTime = new DateTime(Convert.ToInt16(endDate[2]), Convert.ToInt16(endDate[1]), Convert.ToInt16(endDate[0]), Convert.ToInt16(endTime[0]), Convert.ToInt16(endTime[1]), Convert.ToInt16(0));
                                    }
                                    catch (System.ArgumentOutOfRangeException e)
                                    {
                                        if (e.Source != null)
                                        {
                                            Console.WriteLine("Проверьте введенные данные");
                                            Console.ReadLine();
                                            return;
                                        }
                                    }
                                    task.CompletionDateTime = endDateTime;
                                    break;
                            }
                        }
                    }
                }
            }
        }
        [Serializable]
        class MyTaskClass
        {
            public int Id { get; set; }
            public string TaskName { get; set; }
            public DateTime StartedDateTime { get; set; }
            public string TaskDescription { get; set; }
            public DateTime CompletionDateTime { get; set; }
            public MyTaskClass(DateTime startedDateTime, string taskName, string taskDescription, DateTime completionDateTime, int id)
            {
                TaskName = taskName;
                StartedDateTime = startedDateTime;
                CompletionDateTime = completionDateTime;
                TaskDescription = taskDescription;
                Id = id;
            }
            public override string ToString()
            {
                return string.Format("Номер: {0}\nДата и время начала: {1}\nНазвание задачи: {2}\nОписание задачи: {3}\nДата и время окончания: {4}", Id, StartedDateTime, TaskName, TaskDescription, CompletionDateTime);
            }
        }
        [Serializable]
        class TasksSaver
        {
            public List<MyTaskClass> Tasks { get; set; }
            string saveFileName { get; set; }
            public TasksSaver(List<MyTaskClass> tasks, string SaveFileName)
            {
                Tasks = tasks;
                saveFileName = Directory.GetCurrentDirectory() + "/" + SaveFileName;
            }
            public void SavingToFile()
            {
                BinaryFormatter format = new BinaryFormatter();
                using (FileStream fileStream = new FileStream(saveFileName, FileMode.OpenOrCreate))
                {
                    format.Serialize(fileStream, Tasks);
                    Console.WriteLine("Список дел успешно сохранен");
                }
            }
            public List<MyTaskClass> DownloadFromFile()
            {
                Tasks.Clear();
                BinaryFormatter format = new BinaryFormatter();
                using (FileStream fileStream = new FileStream(saveFileName, FileMode.Open))
                {
                    return (List<MyTaskClass>)format.Deserialize(fileStream);
                }
            }
            public int GetLastId()
            {
                try
                {
                    return Tasks.Last().Id;
                }
                catch (System.InvalidOperationException e)
                {
                    if (e.Source != null)
                    {
                        return 1;
                    }
                    else
                    {
                        return Tasks.Last().Id;
                    }
                }
            }
        }
        static void Main(string[] args)
        {
            List<MyTaskClass> tasks = new List<MyTaskClass>();
            TasksPlanner TPlanner = new TasksPlanner(tasks);
            TasksSaver ts = new TasksSaver(TPlanner.Tasks, "planner.dat");
            char menuOption = ' ';
            while (menuOption != '0')
            {
                Console.Clear();
                Console.WriteLine("Выберете пункт меню:");
                Console.WriteLine("1. Просмотр списка задач");
                Console.WriteLine("2. Добавление задачи");
                Console.WriteLine("3. Удаление задачи");
                Console.WriteLine("4. Редактирование задачи");
                Console.WriteLine("5. Сохранение списка задач");
                Console.WriteLine("6. Загрузка списка задач из файла");
                Console.WriteLine("0. Выход");
                try
                {
                    menuOption = Convert.ToChar(Console.ReadLine());
                }
                catch (System.FormatException e)
                {
                    if (e.Source != null)
                    {
                        Console.WriteLine("Выберете пункт меню с 0 по 6");
                    }
                }
                switch (menuOption)
                {
                    case '1':
                        Console.Clear();
                        bool taskIsPresent = false;
                        foreach (MyTaskClass task in TPlanner.Tasks)
                        {
                            Console.WriteLine("*************************");
                            Console.WriteLine(task);
                            taskIsPresent = true;
                        }
                        if (taskIsPresent == false)
                        {
                            Console.WriteLine("У вас еще нет задач");
                        }
                        Console.ReadLine();
                        break;
                    case '2':
                        Console.Clear();
                        TPlanner.AddTask();
                        break;
                    case '3':
                        Console.Clear();
                        Console.WriteLine("Введите номер удаляемой задачи");
                        int removeNumber = Convert.ToInt32(Console.ReadLine());
                        TPlanner.DeleteTask(removeNumber);
                        break;
                    case '4':
                        Console.Clear();
                        Console.WriteLine("Введите номер задачи для редактирования");
                        int editNumber = Convert.ToInt32(Console.ReadLine());
                        TPlanner.SearchAndEditTask(editNumber);
                        break;
                    case '5':
                        Console.Clear();
                        ts.Tasks = TPlanner.Tasks;
                        ts.SavingToFile();
                        break;

                    case '6':
                        Console.Clear();
                        TPlanner.Tasks = ts.DownloadFromFile();
                        TPlanner.Tasks = TPlanner.Tasks.Where(x => x.CompletionDateTime > DateTime.Now).ToList();
                        try
                        {
                            TPlanner.lastId = TPlanner.Tasks.Last().Id;
                        }
                        catch (System.InvalidOperationException)
                        {
                            TPlanner.lastId = 0;
                        }
                        break;
                }
                if (menuOption == '0')
                {
                    Console.Clear();
                    Console.Write("Сохранить изменения перед выходом?(Д/Н)");
                    char saveOption = Convert.ToChar(Console.ReadLine());
                    if (saveOption == 'Д')
                    {
                        ts.Tasks = TPlanner.Tasks;
                        ts.SavingToFile();
                    }
                }
            }
        }
    }

}
