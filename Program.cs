using System;
using System.Collections.Generic;
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
                $"описание {description}\n" +
                $"дед-лайн: {DeadLine.ToShortDateString()}\n" +
                $"приоритет: {Priority}\n" +
                $"тег: {Tag}\n\n";
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
    class Receiver
    {
        public void AddTask(TaskList obj)
        {
            Task t1 = new Task();
            Console.WriteLine("Введите задачу: ");
            t1.name = Console.ReadLine();
            Console.WriteLine("Введите описание: ");
            t1.description = Console.ReadLine();
            obj.ListOfTasks.Add(t1);
        }
        public string ToString(TaskList obj)
        {
            string temp = string.Empty;
            foreach(var it in obj.ListOfTasks)
            {
                temp += it.ToString();
            }

            return temp;
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
            Console.WriteLine("Менеджер Задач");
            Console.WriteLine("1. Добавить новую задачу");
            Console.WriteLine("2. Установить приоритеты задачам");
            Console.WriteLine("3. Установить сроки выполнения задач");
            Console.WriteLine("4. Установить теги задач");
            Console.WriteLine("5. Изменить задачу");
            Console.WriteLine("6. Удалить задачу");
            Console.WriteLine("7. Загрузить задачи из файла");
            Console.WriteLine("8. Сохранить задачи в файл");
            Console.WriteLine("9. Поиск задачи");
            Console.WriteLine("10. Показать все задачи");
            Console.WriteLine("0. Завершить программу");
            Console.WriteLine("Введите команду: ");
        }
        static void Main(string[] args)
        {
            Task task1 = new Task("Купить помидоры",
                "Много",
                new DateTime(2022, 11, 15),
                PriorityType.High,
                "базар");
            Task task2 = new Task("Починить полочку",
                "Найти отвертку",
                new DateTime(2022, 11, 10),
                PriorityType.Medium,
                "дом");
            TaskList t1 = new TaskList();
            t1.ListOfTasks.Add(task1);
            t1.ListOfTasks.Add(task2);

            Invoker invoker = new Invoker();
            Receiver receiver = new Receiver();

            while (true)
            {
                Menu();
                int ch = Convert.ToInt32(Console.ReadLine());

                switch (ch)
                {
                    case 1:
                        {
                            invoker.SetCommand(new AddTask(receiver, t1));
                            invoker.DoCommand();                         
                        }
                        break;
                    case 2:
                        { }
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
                        return;
                    default:
                        { }
                        break;
                }
            }
        }
    }   
}
