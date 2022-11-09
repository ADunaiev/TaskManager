using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Система планирования задач
//Необходимо реализовать простейшую систему планирования задач,
//используя принципы S.O.L.I.D. и паттерны проектирования. Система должна
//предоставлять следующие возможности:
// создание списка задач;
// установка приоритетов задач;
// установка даты выполнения каждой задачи;
// удаление и изменение задач;
// установка тега для каждой задачи;
// загрузка и сохранение задач в файл;
// поиск конкретной задачи по критерию (дата, тег, приоритет).

namespace Pattern5_Task1
{   
    public enum PriorityType { Low, Medium, High }
    class Task
    {
        public string name { get; set; }
        public string description { get; set; }
        public DateTime DeadLine { get; set; }
        public PriorityType Priority { get; set; }
        public string Tag { get; set; }
        public Task(string _name,
            string _description,
            DateTime _deadline,
            PriorityType _priority,
            string _Tag)
        {
            name = _name;
            description = _description;
            DeadLine = _deadline;
            Priority = _priority;
            Tag = _Tag;
        }
        public Task()
        {
            name = "";
            description = "";
            DeadLine = DateTime.Now;
            Priority = PriorityType.Low;
            Tag = "";
        }
        public override string ToString()
        {
            return $"Задача: {name},\n" +
                $"описание: {description}\n" +
                $"дед-лайн: {DeadLine.ToShortDateString()}\n" +
                $"приоритет: {Priority}\n" +
                $"тег: {Tag}\n";
        }
    }
    public interface ICommand
    {
        void Execute();
    }
    class AddTask : ICommand
    {
        private TaskList taskList;
        private Receiver receiver;
        public AddTask(Receiver _receiver, TaskList _taskList)
        {
            receiver = _receiver;
            taskList = _taskList;     
        }
        public void Execute()
        {
            Console.WriteLine();
            receiver.AddTask(taskList);
        }
    }
    class PrintTasks : ICommand
    {
        private TaskList tasklist;
        private Receiver receiver;
        public PrintTasks(Receiver _receiver, TaskList _tasklist)
        {
            receiver = _receiver;
            tasklist = _tasklist;
        }
        public void Execute()
        {
            Console.WriteLine("Распечатываем задачи");
            Console.WriteLine(receiver.ToString(tasklist));
        }
    }
    class SetPriority: ICommand
    {
        private Receiver receiver;
        private TaskList tasklist;
        public SetPriority(Receiver _receiver, TaskList _tasklist)
        {
            receiver = _receiver;
            tasklist = _tasklist;
        }
        public void Execute()
        {
            receiver.SetPriority(tasklist);
        }
    }
    class SetDeadLines : ICommand
    {
        private Receiver receiver;
        private TaskList tasklist;
        public SetDeadLines(Receiver _receiver, TaskList _tasklist)
        {
            receiver = _receiver;
            tasklist = _tasklist;
        }
        public void Execute()
        {
            receiver.SetDeadLines(tasklist);
        }
    }
    class SetTags : ICommand
    {
        private Receiver receiver;
        private TaskList tasklist;
        public SetTags(Receiver _receiver, TaskList _tasklist)
        {
            receiver = _receiver;
            tasklist = _tasklist;
        }
        public void Execute()
        {
            receiver.SetTags(tasklist);
        }

    }
    class ChangeTask : ICommand
    {
        private TaskList taskList;
        private Receiver receiver;
        public ChangeTask(Receiver _receiver, TaskList _taskList)
        {
            receiver = _receiver;
            taskList = _taskList;
        }
        public void Execute()
        {          
            receiver.ChangeTask(taskList);
        }
    }
    class DeleteTask : ICommand
    {
        private TaskList taskList;
        private Receiver receiver;
        public DeleteTask(Receiver _receiver, TaskList _taskList)
        {
            receiver = _receiver;
            taskList = _taskList;
        }
        public void Execute()
        {
            receiver.DeleteTask(taskList);
        }
    }
    class SaveToFile: ICommand
    {
        public string filepath { get; set; }
        private TaskList taskList;
        private Receiver receiver;
        public SaveToFile(Receiver _receiver, TaskList _taskList)
        {
            receiver = _receiver;
            taskList = _taskList;
            filepath = "../../tasks.dat";
        }
        public SaveToFile(Receiver _receiver, TaskList _taskList, string str)
        {
            receiver = _receiver;
            taskList = _taskList;
            filepath = "../../" + str + ".dat";
        }
        public void Execute()
        {
            receiver.SaveToFile(taskList, filepath);
        }
    }
    class LoadFromFile:ICommand
    {
        public string filepath { get; set; }
        private TaskList taskList;
        private Receiver receiver;
        public LoadFromFile(Receiver _receiver, TaskList _taskList)
        {
            receiver = _receiver;
            taskList = _taskList;
            filepath = "../../tasks.dat";
        }
        public LoadFromFile(Receiver _receiver, TaskList _taskList, string str)
        {
            receiver = _receiver;
            taskList = _taskList;
            filepath = "../../" + str + ".dat";
        }
        public void Execute()
        {
            receiver.LoadFromFile(taskList, filepath);
        }
    }
    class Receiver
    {
        public void AddTask(TaskList obj)
        {
            Task t1 = new Task();
            Console.WriteLine("Введите задачу: ");
            t1.name = Console.ReadLine();

            Console.WriteLine("Введите описание: ");
            t1.description = Console.ReadLine();

            Console.WriteLine("Введите дату выполнения: ");
            t1.DeadLine = Convert.ToDateTime(Console.ReadLine());

            Console.WriteLine("Выберите приоритет: 1-низкий; " +
                "2 - средний; 3 - высокий");
            int temp = Convert.ToInt32(Console.ReadLine());
            if (temp == 2)
                t1.Priority = PriorityType.Medium;
            else if (temp == 3)
                t1.Priority = PriorityType.High;
            else
                t1.Priority = PriorityType.Low;

            Console.WriteLine("Введите тег: ");
            t1.Tag = Console.ReadLine();

            obj.ListOfTasks.Add(t1);
        }
        public string ToString(TaskList obj)
        {
            string temp = string.Empty;
            int count = 1;
            foreach(var it in obj.ListOfTasks)
            {
                temp += "Задача " + count++ + "\n" + it.ToString() + "\n";
            }

            return temp;
        }
        public void SetPriority(TaskList obj)
        {          
            for (int i = 0; i < obj.ListOfTasks.Count; i++)
            {
                Console.WriteLine(obj.ListOfTasks[i]);

                Console.WriteLine("Выберите приоритет: 1-низкий; " +
                "2 - средний; 3 - высокий");
                int temp = Convert.ToInt32(Console.ReadLine());
                if (temp == 2)
                    obj.ListOfTasks[i].Priority = PriorityType.Medium;
                else if (temp == 3)
                    obj.ListOfTasks[i].Priority = PriorityType.High;
                else
                    obj.ListOfTasks[i].Priority = PriorityType.Low;
                Console.WriteLine();
            }
        }
        public void SetDeadLines(TaskList obj)
        {
            foreach (var it in obj.ListOfTasks)
            {
                Console.WriteLine(it.ToString());
                Console.WriteLine("Введите новую дату выполнения: ");
                it.DeadLine = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine();
            }
        }
        public void SetTags(TaskList obj)
        {
            foreach (var it in obj.ListOfTasks)
            {
                Console.WriteLine(it.ToString());
                Console.WriteLine("Введите новый тег: ");
                it.Tag = Console.ReadLine();
                Console.WriteLine();
            }
        }
        public void ChangeTask(TaskList obj)
        {
            Console.WriteLine("Введите номер задачи, которую нужно изменить: ");
            Int32 temp = -1;
            temp = Convert.ToInt32(Console.ReadLine()) - 1;

            if (temp >= 0 && temp < obj.ListOfTasks.Count)
            {
                Task task1 = new Task();
                Console.WriteLine("Введите новое названии задачи: ");
                task1.name = Console.ReadLine();

                Console.WriteLine("Введите описание: ");
                task1.description = Console.ReadLine();

                Console.WriteLine("Введите дату выполнения: ");
                task1.DeadLine = Convert.ToDateTime(Console.ReadLine());

                Console.WriteLine("Выберите приоритет: 1-низкий; " +
                    "2 - средний; 3 - высокий");
                int temp2 = Convert.ToInt32(Console.ReadLine());
                if (temp2 == 2)
                    task1.Priority = PriorityType.Medium;
                else if (temp2 == 3)
                    task1.Priority = PriorityType.High;
                else
                    task1.Priority = PriorityType.Low;

                Console.WriteLine("Введите тег: ");
                task1.Tag = Console.ReadLine();

                obj.ListOfTasks[temp] = task1;
            }
        }
        public void DeleteTask(TaskList obj)
        {
            Console.WriteLine("Введите номер задачи, которую нужно удалить: ");
            Int32 temp = -1;
            temp = Convert.ToInt32(Console.ReadLine()) - 1;

            obj.ListOfTasks.Remove(obj.ListOfTasks[temp]);
        }
        public void SaveToFile(TaskList obj, string _filepath)
        {
            using (FileStream fs = new FileStream(_filepath, FileMode.Create,
                FileAccess.Write, FileShare.None)) 
            {
                using (BinaryWriter bw = 
                    new BinaryWriter(fs, Encoding.Unicode))
                {
                    foreach(var item in obj.ListOfTasks)
                    {
                        //int temp = item.name.Length;
                        //bw.Write(temp);
                        bw.Write(item.name);

                        //temp = item.description.Length;
                        //bw.Write(temp);
                        bw.Write(item.description);

                        bw.Write(item.DeadLine.Year);
                        bw.Write(item.DeadLine.Month);
                        bw.Write(item.DeadLine.Day);

                        //temp = ((int)item.Priority);
                        bw.Write(((int)item.Priority));

                        //temp = item.Tag.Length;
                        //bw.Write(temp);
                        bw.Write(item.Tag);
                    }
                }
            }
        }
        public void LoadFromFile(TaskList obj, string _filepath)
        {
            using (FileStream fs = new FileStream(_filepath, FileMode.Open,
                FileAccess.Read, FileShare.None))
            {
                using (BinaryReader br = new BinaryReader(fs, Encoding.Unicode))
                {
                    TaskList t2 = new TaskList();

                    while (br.PeekChar() > -1)
                    {
                        Task tempTask = new Task();

                        tempTask.name = br.ReadString();
                        tempTask.description = br.ReadString();
                        tempTask.DeadLine = new DateTime(br.ReadInt32(),
                            br.ReadInt32(), br.ReadInt32());
                        tempTask.Priority = (PriorityType)br.ReadInt32();
                        tempTask.Tag = br.ReadString();                   
                        
                        t2.ListOfTasks.Add(tempTask);
                    }
                    obj.ListOfTasks = t2.ListOfTasks;
                    fs.Close();
                }
            }
        }
        
    }
    class Invoker
    {
        private ICommand command;
        public void SetCommand(ICommand command)
        {
            this.command = command;
        }
        public void DoCommand()
        {
            if(this.command is ICommand)
            {
                this.command.Execute();
            }
        }
    }
    class TaskList
    {
        public List<Task> ListOfTasks{ get; set; }
        public TaskList()
        {
            ListOfTasks = new List<Task>();
        }
    }
    class Program
    {
        static public void Menu()
        {
            Console.Clear();
            Console.WriteLine("Менеджер Задач\n");
            Console.WriteLine("1. Добавить новую задачу");
            Console.WriteLine("2. Установить приоритеты задачам");
            Console.WriteLine("3. Установить сроки выполнения задач");
            Console.WriteLine("4. Установить теги задач");
            Console.WriteLine("5. Изменить задачу");
            Console.WriteLine("6. Удалить задачу");
            Console.WriteLine("7. Сохранить задачи в файл");
            Console.WriteLine("8. Загрузить задачи из файла");
            Console.WriteLine("9. Поиск задачи");
            Console.WriteLine("10. Показать все задачи");
            Console.WriteLine("0. Завершить программу");
            Console.WriteLine("\nВведите команду: ");
        }
        static void Main(string[] args)
        {
            TaskList t1 = new TaskList();

            Invoker invoker = new Invoker();
            Receiver receiver = new Receiver();

            invoker.SetCommand(new LoadFromFile(receiver, t1));
            invoker.DoCommand();

            while (true)
            {
                Menu();
                int ch = Convert.ToInt32(Console.ReadLine());

                switch (ch)
                {
                    case 1:
                        {
                            Console.Clear();
                            invoker.SetCommand(new AddTask(receiver, t1));
                            invoker.DoCommand();                         
                        }
                        break;
                    case 2:
                        {
                            Console.Clear();
                            invoker.SetCommand(new SetPriority(receiver, t1));
                            invoker.DoCommand();
                        }
                        break;
                    case 3:
                        {
                            Console.Clear();
                            invoker.SetCommand(new SetDeadLines(receiver, t1));
                            invoker.DoCommand();
                        }
                        break;
                    case 4:
                        {
                            Console.Clear();
                            invoker.SetCommand(new SetTags(receiver, t1));
                            invoker.DoCommand();
                        }
                        break;
                    case 5:
                        {
                            Console.Clear();
                            invoker.SetCommand(new PrintTasks(receiver, t1));
                            invoker.DoCommand();
                            invoker.SetCommand(new ChangeTask(receiver, t1));
                            invoker.DoCommand();                           
                        }
                        break;
                    case 6:
                        {
                            Console.Clear();
                            invoker.SetCommand(new PrintTasks(receiver, t1));
                            invoker.DoCommand();
                            invoker.SetCommand(new DeleteTask(receiver, t1));
                            invoker.DoCommand();
                            
                        }
                        break;
                    case 7:
                        {
                            Console.Clear();
                            Console.WriteLine("Введите имя файла: ");
                            string filename = Console.ReadLine();
                            invoker.SetCommand(new SaveToFile(receiver, t1, filename));
                            invoker.DoCommand();
                            Console.WriteLine($"Файл {filename}.dat сохранен успешно.");
                            Console.WriteLine("Нажмите кнопку Enter, чтобы продолжить");
                            Console.ReadLine();
                        }
                        break;
                    case 8:
                        {
                            Console.Clear();
                            Console.WriteLine("Введите имя файла: ");
                            string filename = Console.ReadLine();
                            invoker.SetCommand(new LoadFromFile(receiver, t1, filename));
                            invoker.DoCommand();
                            Console.WriteLine($"Файл {filename}.dat загружен успешно.");
                            Console.WriteLine("Нажмите кнопку Enter, чтобы продолжить");
                            Console.ReadLine();
                        }
                        break;
                    case 10:
                        {
                            Console.Clear();
                            invoker.SetCommand(new PrintTasks(receiver, t1));
                            invoker.DoCommand();
                            Console.WriteLine("Нажмите кнопку Enter, чтобы продолжить");
                            Console.ReadLine();
                        }
                        break;
                    case 0:
                        {
                            invoker.SetCommand(new SaveToFile(receiver, t1));
                            invoker.DoCommand();
                        }
                        return;
                    default:
                        {
                            Console.WriteLine("Сделайте правильный выбор");
                        }
                        break;
                }
            }

            

        }
    }   
}
