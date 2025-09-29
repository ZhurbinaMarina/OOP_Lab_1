using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_1
{
    internal class VendingMachine
    {
        public List<Product> products;
        public int amount;
        private Dictionary<int, int> coins;

        public VendingMachine()
        {
            products = new List<Product>()
            {
                new Product {name = "Вода", price = 32, quantity = 20},
                new Product {name = "Шоколадка 'Алёнка'", price = 60, quantity = 35},
                new Product {name = "Чипсы Lays с сыром", price = 85, quantity = 30},
                new Product {name = "Батончик", price = 40, quantity = 15}
            };

            coins = new Dictionary<int, int>()
            {
                {1, 100 }, {2, 100}, {5, 100}, {10, 100 }
            };

            amount = 0;
        }

        public void DisplayAvailableProducts()
        {
            Console.WriteLine("\nДоступные товары:");
            for (int i = 0; i < products.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {products[i].name} - {products[i].price} руб. - Количество: {products[i].quantity}");
            }
        }

        public void Payment()
        {
            Console.WriteLine("Введите количество монет, которые хотите ввести (Доступные номиналы: 1, 2, 5, 10):");
            
            if (int.TryParse(Console.ReadLine(), out int count))
            {
                Console.WriteLine("Вводите монеты по одной:");
                
                for (int i = 0; i < count; i++)
                {
                    string coin = Console.ReadLine();
                    InsertCoin(coin);
                }

                Console.WriteLine("Ввод окончен");
            }
            else
            {
                Console.WriteLine("Неккоректное значение");
            }
        }

        private void InsertCoin(string coin_str)
        {
            if (int.TryParse(coin_str, out int coin) && coins.ContainsKey(coin))
            {
                amount += coin;
                coins[coin] += 1;
            }
            else
            {
                Console.WriteLine("Неккоректное значение, попробуйте ещё раз:");
                coin_str = Console.ReadLine();
                InsertCoin(coin_str);
            }
        }

        public void Purchase()
        {
            DisplayAvailableProducts();
            Console.WriteLine("Выберите номер товара, который хотите приобрести:");
            
            if (int.TryParse (Console.ReadLine(), out int number) && number > 0 && number <= products.Count)
            {
                Product product = products[number - 1];

                if (product.quantity == 0)
                {
                    Console.WriteLine("Выбранный товар закончился");
                    return;
                }

                if (amount >= product.price)
                {
                    int change = amount - product.price;
                    bool canGiveChange = AbilityGiveChange(change);
                    if (!canGiveChange)
                    {
                        Console.WriteLine("Автомат не сможет вернуть вам сдачу, продолжить?");
                        Console.WriteLine("Если хотите продолжить покупку введите 'Да', если хотите завершить покупку введите 'Нет'");
                        if (Console.ReadLine() != "Да")
                        {
                            return;
                        }
                    }

                    product.quantity -= 1;
                    amount = 0;
                    Console.WriteLine("Возьмите ваш товар, хорошего дня!");

                    if (change > 0 && canGiveChange)
                    {
                        GiveChange(change);
                        Console.WriteLine($"Ваша сдача: {change} руб.");
                    }
                }
                else
                {
                    Console.WriteLine("Недостаточно средств для покупки данного товара");
                }
            }
            else
            {
                Console.WriteLine("Неверный номер товара");
            }
        }

        public void RerurnMoney()
        {
            GiveChange(amount);
            Console.WriteLine($"Возвращена сумма в размере {amount} руб.");
            amount = 0;
        }

        private void GiveChange(int change)
        {
            List<int> nomination = coins.Keys.OrderByDescending(x => x).ToList();
            
            foreach (int coin in nomination)
            {
                while (change >= coin && coins[coin] > 0)
                {
                    change -= coin;
                    coins[coin] -= 1;
                    Console.WriteLine($"Возврат монеты {coin} руб.");
                }
            }
        }

        private bool AbilityGiveChange(int change)
        {
            List<int> nominations = coins.Keys.OrderByDescending(x => x).ToList();

            return ChangeCombination(change, nominations, 0);
        }
        
        private bool ChangeCombination(int change, List<int> nominations, int index)
        {
            if (change == 0)
            {
                return true;
            }

            if (index >= nominations.Count)
            {
                return false;
            }

            int currNominal = nominations[index];
            int maxCoins = Math.Min(coins[currNominal], change / currNominal);

            for (int count = maxCoins; count >= 0; count--)
            {
                int newChange = change - count * currNominal;
                
                if (ChangeCombination(newChange, nominations, index + 1))
                {
                    return true;
                }
            }

            return false;
        }

        public void AdminMode()
        {
            Console.WriteLine("Введите пароль:");
            string password = Console.ReadLine();

            if (password != "admin")
            {
                Console.WriteLine("Неверный пароль");
                return;
            }

            Console.WriteLine("Администраторский режим:");
            Console.WriteLine("1. Пополнить товары");
            Console.WriteLine("2. Собрать собранные средства");
            Console.WriteLine("3. Выйти из Администраторского режима");
            Console.WriteLine("Для того, чтобы выбрать одну из предложенных функций, просто набирите ее номер :)");

            while (true)
            {
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ReplenishProducts();
                        Console.WriteLine("\nВведите номер функции, которой хотите воспользоваться:");
                        break;
                    case "2":
                        CollectMoney();
                        Console.WriteLine("\nВведите номер функции, которой хотите воспользоваться:");
                        break;
                    case "3":
                        Console.WriteLine("Выход из Администраторского режима");
                        return;
                    default:
                        Console.WriteLine("Такой функции не существует :(");
                        Console.WriteLine("\nВведите номер функции, которой хотите воспользоваться:");
                        break;
                }
            }
        }

        private void ReplenishProducts()
        {
            DisplayAvailableProducts();
            Console.WriteLine("Выберите номер товара, количество которого хотите пополнить:");

            if (int.TryParse(Console.ReadLine(), out int id) && id > 0 && id <= products.Count)
            {
                Console.WriteLine("Введите количество:");

                if (int.TryParse(Console.ReadLine(), out int count) && count > 0)
                {
                    products[id - 1].quantity += count;
                    Console.WriteLine("Товар пополнен");
                }
                else
                {
                    Console.WriteLine("Неккоректное значение");
                }
            }
            else
            {
                Console.WriteLine("Неверный номер товара");
            }
        }

        private void CollectMoney()
        {
            int totalAmount = coins.Sum(coin => coin.Value * coin.Key);
            Console.WriteLine($"Собрано средств: {totalAmount} руб.");
            coins = coins.ToDictionary(coin => coin.Key, coin => 0);
        }
    }
}
