
class VendingMachine {
    
    private Dictionary<int, (string name, int price, int number)> Products = new()
    //ключ - код товара, первое - название, второе - цена товара в рублях, третье - доступное количество товара 
    {
        [01] = ("water", 90, 5),
        [04] = ("snickers", 80, 10),
        [02] = ("bread", 50, 8),
    };

    private Dictionary<int, int> MoneyRepository = new()
    //первое число - номинал, второе - количество денег соответствующего номинала
    {
        {1, 100},
        {2, 100},
        {5, 100},
        {10, 100},
        {50, 100},
        {100, 100},
        {500, 100},
    };
    private int MachineBalance => MoneyRepository.Sum(x => x.Key * x.Value);
    private Dictionary<int, int> CustomerMoneyRepository = new()
    {
        {1, 0},
        {2, 0},
        {5, 0},
        {10, 0},
        {50, 0},
        {100, 0},
    };

    public int CustomerTotalMoney => CustomerMoneyRepository.Sum(x => x.Key * x.Value);


    public void InputMoney()
    {
        int[] allowedNominals = { 1, 2, 5, 10, 50, 100, 500 };

        Console.WriteLine("Вы можете внести деньги номиналом 1, 2, 5, 10, 50, 100, 500 рублей");
        Console.WriteLine("Укажите номинал и количество целыми числами через пробел");
        Console.WriteLine("Для выхода из режима внесения денег укажите 'отмена' ");

        string input = Console.ReadLine();

        if (input?.ToLower() == "отмена")
            return;

        string[] parts = input.Split(" ");
        if (parts.Length == 2 &&
            int.TryParse(parts[0], out int nominal) &&
            int.TryParse(parts[1], out int count)
            )
        {
            if (allowedNominals.Contains(nominal))
            {
                CustomerMoneyRepository[nominal] += count;
            }

            else
            {
                Console.WriteLine("Неправильные деньги.");
            }

        }

    }

    public void GetOutMoney()
    {
        int total = 0;

        foreach (var nominal in CustomerMoneyRepository.Keys)
        {
            total += CustomerMoneyRepository[nominal] * nominal;
            CustomerMoneyRepository[nominal] = 0;
        }
        Console.WriteLine($"Вернули {total} рублей");
    }
 
    private void ResetCustomerMoney()
    {
        foreach (var nominal in CustomerMoneyRepository.Keys.ToList())
        {
            CustomerMoneyRepository[nominal] = 0;
        }
    }
    public void BuyGood()
    {
        Console.WriteLine("Введите код товара который хотите выбрать");
        if (int.TryParse(Console.ReadLine(), out int code) &&
            Products.TryGetValue(code, out var product) &&
            CustomerTotalMoney >= product.price)
        {
            
            Products[code] = (product.name, product.price, product.number - 1);
            Console.WriteLine($"Ваш {product.name}");
            int diff = CustomerTotalMoney - product.price;


            foreach (var nominal in CustomerMoneyRepository.Keys)
            {
                MoneyRepository[nominal] += CustomerMoneyRepository[nominal];
            }
            
            
            ResetCustomerMoney();

            int count = 0;
            if (diff > 0)
            {
                foreach (var nominal in CustomerMoneyRepository.Keys.OrderByDescending(x => x))
                {

                    while (MoneyRepository[nominal] > 0 && (count + nominal) <= diff)
                    {
                        count += nominal;
                        CustomerMoneyRepository[nominal] += 1;
                    }

                    if (count == diff)
                    {
                        break;
                    }
                if (count < diff)
                {
                    Console.WriteLine("В автомате недостаточно средств для выдачи сдачи. Воспользуйтесь QR кодом для получения сдачи в банк.");
                }

                }

            }
        else
        {
            Console.WriteLine("Недостаточно денег или неверный код товара.");                    }
        }

    }
    public void ProductsVisualization()
    {
        foreach (var product in Products)
        {
            Console.WriteLine($" '{product.Value.name}' стоимость: {product.Value.price}р. доступно: {product.Value.number} код: {product.Key} ");
        }
    }


    private void AddProduct()
    {
        Console.WriteLine("Для добавления товара корректно укажите данные");
        Console.WriteLine("Формат ввода через пробел: name price count code");
        Console.WriteLine("NB | Выбирайте такой код, чтобы он не совпадал с уже занятыми ");

        string input = Console.ReadLine();
        string[] adding = input.Split(' ');
        string name = adding[0];

        if (!int.TryParse(adding[1], out int price))
        {
            Console.WriteLine("Ошибка ввода.");
            return;
        }

        if (!int.TryParse(adding[2], out int count))
        {
            Console.WriteLine("Ошибка ввода.");
            return;
        }
        if (!int.TryParse(adding[3], out int code))
        {

            Console.WriteLine("Ошибка ввода.");
            return;
        }
        if (Products.ContainsKey(code)) 
        {

            Console.WriteLine("Ошибка ввода.");
            return;
        }
        
        Products[code] = (name, price, count);
        Console.WriteLine($"Товар '{name}' добавлен.");
        

    }
    private void GetOwnerMoney(string par)
    {
        if (par == "y")
        {
            int total = 0;
            foreach (var nominal in MoneyRepository.Keys)
            {
                total += (MoneyRepository[nominal] * nominal * 9) / 10;
            }
            Console.WriteLine($"Выведено{total} рублей");

        }
        else
        {
            int total = 0;
            foreach (var nominal in MoneyRepository.Keys)
            {
                total += MoneyRepository[nominal] * nominal ;
            }
            Console.WriteLine($"Выведено{total} рублей");
            
        }
        
    }

    public void SudoMode(string password)
    {
        if (password == "1864")
        {
            Console.WriteLine($"Баланс автомата: {MachineBalance} рублей");
            while (0 < 1)
            {
                Console.WriteLine("Для добавления товара введите 'n', для снятия денег введите 'm', для выхода из режима введите 'q'");
                string input = Console.ReadLine();

                if (input == "q")
                {
                    return;
                }

                if (input == "n")
                {
                    AddProduct();
                    continue;
                }

                if (input == "m")
                {
                    Console.WriteLine("Если вы хотите оставить часть денег для сдачи введите 'y', иначе - 'n'");
                    string par = Console.ReadLine();
                    GetOwnerMoney(par);
                    continue;
                }



            }

        }

        else
        {
            Console.WriteLine("ошибка доступа");
        }
    }
}
