using System;

VendingMachine machine = new VendingMachine();

while (0 < 1)
{
    Console.WriteLine("Для взаимодейтсвия с автоматом используйте цифры");
    Console.WriteLine("1 - внести деньги, 2 - выбрать и купить товар 3 - снять деньги/отмена/сдача");
    Console.WriteLine("0 - просмотреть ассортимент");
    Console.WriteLine("112 - режим администратора");
    Console.WriteLine("Чтобы уйти введите 'q'");
    Console.WriteLine($"Текущий баланс {machine.CustomerTotalMoney} рублей");
    
    string code = Console.ReadLine();
    switch(code)
    {
        case "0":
        machine.ProductsVisualization();
        continue;

        case "1":
        machine.InputMoney();
        continue;

        case "2":
        machine.BuyGood();
        continue;
    
        case "3":
        machine.GetOutMoney();
        continue;

        case "112":
        Console.WriteLine("Введите pin");
        string pin = Console.ReadLine();
        machine.SudoMode(pin);
        continue;
        
        case "q":
        return;

        default:
        Console.WriteLine("ошибка ввода.");
        continue;
    }
}