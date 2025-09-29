using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            VendingMachine machine = new VendingMachine();

            Console.WriteLine("\nУ данного Вендингового автомата есть функции:");
            Console.WriteLine("1. Показать товары");
            Console.WriteLine("2. Вставить деньги");
            Console.WriteLine("3. Выбрать товар");
            Console.WriteLine("4. Вернуть деньги");
            Console.WriteLine("5. Завершить покупки");
            Console.WriteLine("Для того, чтобы выбрать одну из предложенных функций, просто набирите ее номер :)");

            while (true)
            {
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        machine.DisplayAvailableProducts();
                        Console.WriteLine("\nВведите номер функции, которой хотите воспользоваться:");
                        break;
                    case "2":
                        machine.Payment();
                        Console.WriteLine("\nВведите номер функции, которой хотите воспользоваться:");
                        break;
                    case "3":
                        machine.Purchase();
                        Console.WriteLine("\nВведите номер функции, которой хотите воспользоваться:");
                        break;
                    case "4":
                        machine.RerurnMoney();
                        Console.WriteLine("\nВведите номер функции, которой хотите воспользоваться:");
                        break;
                    case "5":
                        return;
                    case "6":
                        machine.AdminMode();
                        Console.WriteLine("\nВведите номер функции, которой хотите воспользоваться:");
                        break;
                    default:
                        Console.WriteLine("Такой функции не существует :(");
                        Console.WriteLine("\nВведите номер функции, которой хотите воспользоваться:");
                        break;
                }
            }
            
        }
    }
}
