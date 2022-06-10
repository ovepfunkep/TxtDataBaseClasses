using System.Text;

//Чтобы работал рус.яз.
Encoding.RegisterProvider(CodePagesEncodingProvider.Instance); 
Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.GetEncoding(1251);

const string pathClients = @"E:\Projects\BuildingProject\BuildingProject\DataClients.txt";
const string pathEmployees = @"E:\Projects\BuildingProject\BuildingProject\DataEmployees.txt";
const string pathOrders = @"E:\Projects\BuildingProject\BuildingProject\DataOrders.txt";

List<Client> ListClients = FillClients();
List<Employee> ListEmployees = FillEmployees();
List<Order> ListOrders = FillOrders();

bool repeat = true;
do repeat = ShowMenu();
while (repeat == true);

bool ShowMenu()
{
    Console.WriteLine("===Основное меню===");
    Console.WriteLine("1.Просмотр информации");
    Console.WriteLine("2.Добавление информации");
    Console.WriteLine("3.Поиск информации");
    Console.WriteLine("4.Удаление информации");
    Console.WriteLine("5.Обновление информации");
    Console.WriteLine("6.Выход и сохранение\n");
    int input = GetItemChoice();
    bool repeat = true;
    switch (input)
    {
        case 1:
            do repeat = ViewMenu();
            while (repeat == true);
            return true;
        case 2:
            do repeat = AddMenu();
            while (repeat == true);
            return true;
        case 3:
            do repeat = SearchMenu();
            while (repeat == true);
            return true;
        case 4:
            do repeat = DeleteMenu();
            while (repeat == true);
            return true;
        case 5:
            do repeat = UpdateMenu();
            while (repeat == true);
            return true;
        default:
            SaveInfo();
            return false;
    }
}

void WriteItems()
{
    Console.WriteLine("1.Клиенты");
    Console.WriteLine("2.Сотрудники");
    Console.WriteLine("3.Заказы");
    Console.WriteLine("4.Меню\n");
}
int GetItemChoice()
{
    string? inputString = Console.ReadLine();
    if (inputString == "") return -1;
    Console.WriteLine();
    return int.Parse(inputString);
}
bool AskForAnother(string word)
{
    Console.WriteLine($"Желаете {word} другого?\n1.Да\n2.Нет");
    if (Console.ReadLine() == "1")
    {
        Console.WriteLine();
        return true;
    }
    else
    {
        Console.WriteLine();
        return false;
    }
}

//<Заполнение данных>
List<Client> FillClients()
{
    string[] clientsStrings = File.ReadAllLines(pathClients);
    List<Client> clientsList = new();
    for (int i = 0; i < clientsStrings.Length; i++)
    {
        clientsList.Add(fillClient(clientsStrings[i]));
    }
    return clientsList;
}
Client fillClient(string data)
{
    int Id = 0;
    string FullName = "";
    int Age = 0;
    string Orders = "";
    int TotalSpent = 0;
    string value = "";
    int count = 0;
    for (int i = 0; i < data.Length; i++)
    {
        if (data[i] == '|')
        {
            switch (count)
            {
                case 0:
                    Id = int.Parse(value);
                    break;
                case 1:
                    FullName = value;
                    break;
                case 2:
                    Age = int.Parse(value);
                    break;
                case 3:
                    Orders = value;
                    break;
                case 4:
                    TotalSpent = int.Parse(value);
                    break;
            }
            value = "";
            count++;
        }
        else value += data[i];
    }
    return new Client(Id, FullName, Age, Orders, TotalSpent);
}
List<Employee> FillEmployees()
{
    string[] employeesStrings = File.ReadAllLines(pathEmployees);
    List<Employee> employeesList = new();
    for (int i = 0; i < employeesStrings.Length; i++)
    {
        employeesList.Add(fillEmployee(employeesStrings[i]));
    }
    return employeesList;
}
Employee fillEmployee(string data)
{
    int Id = 0;
    string FullName = "";
    int Age = 0;
    string Position = "";
    int TotalEarned = 0;
    string OrderId = "";
    string value = "";
    int count = 0;
    for (int i = 0; i < data.Length; i++)
    {
        if (data[i] == '|')
        {
            switch (count)
            {
                case 0:
                    Id = int.Parse(value);
                    break;
                case 1:
                    FullName = value;
                    break;
                case 2:
                    Age = int.Parse(value);
                    break;
                case 3:
                    Position = value;
                    break;
                case 4:
                    TotalEarned = int.Parse(value);
                    break;
                case 5:
                    OrderId = value;
                    break;
            }
            value = "";
            count++;
        }
        else value += data[i];
    }
    return new Employee(Id, FullName, Age, Position, TotalEarned, OrderId);
}
List<Order> FillOrders()
{
    string[] ordersStrings = File.ReadAllLines(pathOrders);
    List<Order> ordersList = new();
    for (int i = 0; i < ordersStrings.Length; i++)
    {
        ordersList.Add(fillOrder(ordersStrings[i]));
    }
    return ordersList;
}
Order fillOrder(string data)
{
    int Id = 0;
    string Name = "";
    string Type = "";
    int Cost = 0;
    string EmployeeId = "";
    string ClientId = "";
    string value = "";
    int count = 0;
    for (int i = 0; i < data.Length; i++)
    {
        if (data[i] == '|')
        {
            switch (count)
            {
                case 0:
                    Id = int.Parse(value);
                    break;
                case 1:
                    Name = value;
                    break;
                case 2:
                    Type = value;
                    break;
                case 3:
                    Cost = int.Parse(value);
                    break;
                case 4:
                    EmployeeId = value;
                    break;
                case 5:
                    ClientId = value;
                    break;
            }
            value = "";
            count++;
        }
        else value += data[i];
    }
    return new Order(Id, Name, Type, Cost, EmployeeId, ClientId);
}
//</Заполнение данных>

//<Просмотр>
bool ViewMenu()
{
    Console.WriteLine("===Просмотр информации===");
    WriteItems();
    int input = GetItemChoice();
    switch (input)
    {
        case 1:
            ShowClients();
            return true;
        case 2:
            ShowEmployees();
            return true;
        case 3:
            ShowOrders();
            return true;
        default:
            return false;
    }
}

void ShowClients()
{
    Console.WriteLine("ID | Полное имя | Возраст | Заказы | Всего потрачено");
    Console.WriteLine("-----------------------------------------------------");
    foreach (Client client in ListClients)
        client.ShowInfo();
    Console.WriteLine();
}
void ShowEmployees()
{
    Console.WriteLine("ID | Полное имя | Возраст | Должность | Заработал | Заказы");
    Console.WriteLine("-----------------------------------------------------------");
    foreach (Employee employee in ListEmployees)
    {
        employee.ShowInfo();
    }
    Console.WriteLine();
}
void ShowOrders()
{
    Console.WriteLine("ID | Название | Тип | Стоимость | Работники | Клиент");
    Console.WriteLine("-----------------------------------------------------");
    foreach (Order order in ListOrders)
    {
        order.ShowInfo();
    }
    Console.WriteLine();
}
//</Просмотр>

//<Добавление>
bool AddMenu()
{
    Console.WriteLine("===Добавление информации===");
    WriteItems();
    int input = GetItemChoice();
    bool repeat = true;
    switch (input)
    {
        case 1:
            do repeat = AddClient();
            while (repeat == true);
            return true;
        case 2:
            do repeat = AddEmployee();
            while (repeat == true);
            return true;
        case 3:
            do repeat = AddOrder();
            while (repeat == true);
            return true;
        default:
            return false;
    }
}

bool AddClient()
{
    Console.WriteLine("Данные нового клиента:");
    Console.Write("ID:");
    string? enteredValue = Console.ReadLine();
    if (enteredValue == "") return false;
    int Id = int.Parse(enteredValue);

    Console.Write("Полное имя:");
    enteredValue = Console.ReadLine();
    if (enteredValue == "") return false;
    string FullName = enteredValue;

    Console.Write("Возраст:");
    enteredValue = Console.ReadLine();
    if (enteredValue == "") return false;
    int Age = int.Parse(enteredValue);

    Console.Write("Заказы:");
    enteredValue = Console.ReadLine();
    string Orders = enteredValue;

    Console.Write("Всего потрачено:");
    enteredValue = Console.ReadLine();
    int TotalSpent;
    if (enteredValue == "") TotalSpent = 0;
    else TotalSpent = int.Parse(enteredValue);

    Console.WriteLine();
    Console.WriteLine("Вы уверены что хотите добавить клиента с такими данными?\n1.Да\n2.Нет");
    if (Console.ReadLine() == "1")
        ListClients.Add(new Client(Id, FullName, Age, Orders, TotalSpent));
    return AskForAnother("добавить");
}
bool AddEmployee()
{
    Console.WriteLine("Данные нового сотрудника:");
    Console.Write("ID:");
    string? enteredValue = Console.ReadLine();
    if (enteredValue == "") return false;
    int Id = int.Parse(enteredValue);

    Console.Write("Полное имя:");
    enteredValue = Console.ReadLine();
    if (enteredValue == "") return false;
    string FullName = enteredValue;

    Console.Write("Возраст:");
    enteredValue = Console.ReadLine();
    if (enteredValue == "") return false;
    int Age = int.Parse(enteredValue);

    Console.Write("Должность:");
    enteredValue = Console.ReadLine();
    string Position = enteredValue;

    Console.Write("Всего заработано:");
    enteredValue = Console.ReadLine();
    int TotalEarned;
    if (enteredValue == "") TotalEarned = 0;
    else TotalEarned = int.Parse(enteredValue);

    Console.Write("Проработанные заказы:");
    enteredValue = Console.ReadLine();
    string Orders = enteredValue;

    Console.WriteLine();
    Console.WriteLine("Вы уверены что хотите добавить сотрудника с такими данными?\n1.Да\n2.Нет");
    if (Console.ReadLine() == "1")
        ListEmployees.Add(new Employee(Id, FullName, Age, Position, TotalEarned, Orders));
    return AskForAnother("добавить");
}
bool AddOrder()
{
    Console.WriteLine("Данные нового заказа:");
    Console.Write("ID:");
    string? enteredValue = Console.ReadLine();
    if (enteredValue == "") return false;
    int Id = int.Parse(enteredValue);

    Console.Write("Название:");
    enteredValue = Console.ReadLine();
    if (enteredValue == "") return false;
    string Name = enteredValue;

    Console.Write("Тип:");
    enteredValue = Console.ReadLine();
    if (enteredValue == "") return false;
    string Type = enteredValue;

    Console.Write("Стоимость:");
    enteredValue = Console.ReadLine();
    if (enteredValue == "") return false;
    int Cost = int.Parse(enteredValue);

    Console.Write("Рабочие:");
    enteredValue = Console.ReadLine();
    if (enteredValue == "") return false;
    string EmployeeId = enteredValue;

    Console.Write("Заказчик:");
    enteredValue = Console.ReadLine();
    if (enteredValue == "") return false;
    string ClientId = enteredValue;

    Console.WriteLine();
    Console.WriteLine("Вы уверены что хотите добавить заказ с такими данными?\n1.Да\n2.Нет");
    if (Console.ReadLine() == "1")
    {
        ListOrders.Add(new Order(Id, Name, Type, Cost, EmployeeId, ClientId));

        Client client = ListClients.Where(c => c.id == int.Parse(ClientId)).First();
        client.totalSpent += Cost;
        if (client.orders == "") client.orders = Id.ToString();
        else client.orders += $",{Id}";

        string[] employeesIds = EmployeeId.Split(",");
        foreach (Employee employee in ListEmployees
        .Where(e => employeesIds.Any(o => int.Parse(o) == e.id)))
        {
            if (employee.position == "Бригадир") employee.totalEarned += Cost / 2;
            else employee.totalEarned += Cost / 4;
            if (employee.orderId == "") employee.orderId = Id.ToString();
            else employee.orderId += $",{Id}";
        }
    }
    return AskForAnother("добавить");
}
//</Добавление>

//<Поиск>
bool SearchMenu()
{
    Console.WriteLine("===Поиск информации===");
    WriteItems();
    int input = GetItemChoice();
    bool repeat = true;
    switch (input)
    {
        case 1:
            SearchClients();
            return true;
        case 2:
            SearchEmployees();
            return true;
        case 3:
            SearchOrders();
            return true;
        default:
            return false;
    }
}

void SearchClients()
{
    Console.WriteLine("По каким данным совершать поиск?");
    Console.WriteLine("1.ФИО");
    Console.WriteLine("2.Возраст\n");
    int input = GetItemChoice();
    if (input == 1)
    {
        Console.WriteLine("Напишите букву/слово/фио по которому совершать поиск\n");
        string search = Console.ReadLine();
        if (search == "") return;
        foreach (Client client in ListClients.Where(c => c.fullName.ToLower().Contains(search.ToLower())))
            client.ShowInfo();
    }
    else
    {
        Console.WriteLine("Напишите возраст по которому совершать поиск\n");
        string search = Console.ReadLine();
        if (search == "") return;
        foreach (Client client in ListClients.Where(c => c.age.Equals(int.Parse(search))))
            client.ShowInfo();
    }
    Console.WriteLine();
}
void SearchEmployees()
{
    Console.WriteLine("По каким данным совершать поиск?");
    Console.WriteLine("1.ФИО");
    Console.WriteLine("2.Возраст\n");
    int input = GetItemChoice();
    if (input == 1)
    {
        Console.WriteLine("Напишите букву/слово/фио по которому совершать поиск\n");
        string search = Console.ReadLine();
        if (search == "") return;
        foreach (Employee employee in ListEmployees.Where(e => e.fullName.ToLower().Contains(search.ToLower())))
            employee.ShowInfo();
    }
    else
    {
        Console.WriteLine("Напишите возраст по которому совершать поиск\n");
        string search = Console.ReadLine();
        if (search == "") return;
        foreach (Employee employee in ListEmployees.Where(e => e.age.Equals(int.Parse(search))))
            employee.ShowInfo();
    }
    Console.WriteLine();
}
void SearchOrders()
{
    Console.WriteLine("Напишите букву/слово/название по которому совершать поиск\n");
    string search = Console.ReadLine();
    if (search == "") return;
    foreach (Order order in ListOrders.Where(o => o.name.ToLower().Contains(search.ToLower())))
        order.ShowInfo();
    Console.WriteLine();
}
//</Поиск>

//<Удаление>
bool DeleteMenu()
{
    Console.WriteLine("!===УДАЛЕНИЕ информации===!");
    WriteItems();
    int input = GetItemChoice();
    bool repeat = true;
    switch (input)
    {
        case 1:
            do repeat = DeleteClient();
            while (repeat == true);
            return true;
        case 2:
            do repeat = DeleteEmployee();
            while (repeat == true);
            return true;
        case 3:
            do repeat = DeleteOrder();
            while (repeat == true);
            return true;
        default:
            return false;
    }
}

bool DeleteClient()
{
    Console.WriteLine("Введите ID клиента для удаления\n");
    string input = Console.ReadLine();
    if (input == "") return false;
    if (ListOrders.Exists(o => o.clientId.Split(',').Any(el => el == input)))
    {
        Console.WriteLine("\nНевозможно удалить клиента у которого есть заказы\n");
        Console.WriteLine("Хотите удалить все его заказы?\n1.Да\n2.Нет\n");
        if (Console.ReadLine() == "1") 
            foreach (Order order in ListOrders.Where(o => o.clientId == input).ToList()) 
                DeleteParticularOrder(order);
        else return false;
    }
    Console.WriteLine("\nВы уверены что хотите удалить этого клиента?\n1.Да\n2.Нет\n");
    if (Console.ReadLine() == "1")
        ListClients.Remove(
            ListClients.Where(c => c.id == int.Parse(input)).First());
    Console.WriteLine();
    return AskForAnother("удалить");
}
bool DeleteEmployee()
{
    Console.WriteLine("Введите ID работника для удаления\n");
    string input = Console.ReadLine();
    if (input == "") return false;
    if (ListOrders.Exists(o => o.employeeId.Split(',').Any(el => el == input)))
    {
        Console.WriteLine("\nНевозможно удалить работника у которого есть заказы");
        Console.WriteLine("Хотите удалить все заказы с его присутствиеем?\n1.Да\n2.Нет");
        if (Console.ReadLine() == "1")
            foreach (Order order in ListOrders.Where(o => o.employeeId.Contains(input)).ToList())
                DeleteParticularOrder(order);
        else return false;
    }
    Console.WriteLine("\nВы уверены что хотите удалить этого работника?\n1.Да\n2.Нет\n");
    if (Console.ReadLine() == "1")
        ListEmployees.Remove(
            ListEmployees.Where(e => e.id == int.Parse(input)).First());
    Console.WriteLine();
    return AskForAnother("удалить");
}
bool DeleteOrder()
{
    Console.WriteLine("Введите ID заказа для удаления\n");
    string input = Console.ReadLine();
    if (input == "") return false;
    Console.WriteLine("\nВы уверены что хотите удалить этот заказ?\n1.Да\n2.Нет\n");
    if (Console.ReadLine() == "1")
    {
        Order order = ListOrders.Where(o => o.id == int.Parse(input)).First();
        DeleteParticularOrder(order);
    }
    return AskForAnother("удалить");
}
void DeleteParticularOrder(Order order)
{
    string[] employeesIds = order.employeeId.Split(",");
    foreach (Employee employee in ListEmployees
        .Where(e => employeesIds.Any(o => int.Parse(o) == e.id)))
    { 
        if (employee.position == "Бригадир") employee.totalEarned -= order.cost / 2;
        else employee.totalEarned -= order.cost / 4;
        employee.orderId = String.Join(",", employee.orderId.Split(',').Where(i => i != order.id.ToString()));
    }

    Client client = ListClients.Where(c => c.id.ToString() == order.clientId).First();
    client.totalSpent -= order.cost;
    client.orders = String.Join(",", client.orders.Split(',').Where(i => i != order.id.ToString()));

    ListOrders.Remove(order);
}
//</Удаление>

//<Обновление>
bool UpdateMenu()
{
    Console.WriteLine("===Обновление информации===");
    WriteItems();
    int input = GetItemChoice();
    bool repeat = true;
    switch (input)
    {
        case 1:
            do repeat = UpdateClient();
            while (repeat == true);
            return true;
        case 2:
            do repeat = UpdateEmployee();
            while (repeat == true);
            return true;
        case 3:
            do repeat = UpdateOrder();
            while (repeat == true);
            return true;
        default:
            return false;
    }
}

bool UpdateClient()
{
    Console.WriteLine("Напишите ID клиента, у которого хотите переписать данные\n");
    string input = Console.ReadLine();
    if (input == "") return false;
    int clientId = int.Parse(input);
    Client client = ListClients.Where(c => c.id == clientId).First();

    Console.WriteLine("Какие данные вы хотите изменить?");
    Console.WriteLine("1.ID");
    Console.WriteLine("2.ФИО");
    Console.WriteLine("3.Возраст");
    Console.WriteLine("4.ID заказов");
    Console.WriteLine("5.Всего потрачено\n");

    input = Console.ReadLine();
    if (input == "") return false;
    int chosenField = int.Parse(input);

    Console.Write("\nНовые данные для этого поля: ");
    input = Console.ReadLine();
    if (input == "") return false;
    switch (chosenField)
    {
        case 1:
            foreach (Order order in ListOrders.Where(o => int.Parse(o.clientId) == clientId))
                order.clientId = input;
            client.id = int.Parse(input);
            break;
        case 2:
            client.fullName = input;
            break;
        case 3:
            client.age = int.Parse(input);
            break;
        case 4:
            client.orders = input;
            break;
        case 5:
            client.totalSpent = int.Parse(input);
            break;
        default:
            return false;
    }
    Console.WriteLine();
    return AskForAnother("обновить данные");
}
bool UpdateEmployee()
{
    Console.WriteLine("Напишите ID сотрудника, у которого хотите переписать данные\n");
    string input = Console.ReadLine();
    if (input == "") return false;
    int employeeId = int.Parse(input);
    Employee employee = ListEmployees.Where(e => e.id == employeeId).First();

    Console.WriteLine("Какие данные вы хотите изменить?");
    Console.WriteLine("1.ID");
    Console.WriteLine("2.ФИО");
    Console.WriteLine("3.Возраст");
    Console.WriteLine("4.Должность");
    Console.WriteLine("5.Всего заработано");
    Console.WriteLine("6.Выполненные заказы\n");

    input = Console.ReadLine();
    if (input == "") return false;
    int chosenField = int.Parse(input);

    Console.Write("\nНовые данные для этого поля: ");
    input = Console.ReadLine();
    if (input == "") return false;
    switch (chosenField)
    {
        case 1:
            foreach (Order order in ListOrders.Where(o => o.employeeId.Contains(employeeId.ToString())))
                order.employeeId = input;
            employee.id = int.Parse(input);
            break;
        case 2:
            employee.fullName = input;
            break;
        case 3:
            employee.age = int.Parse(input);
            break;
        case 4:
            employee.position = input;
            break;
        case 5:
            employee.totalEarned = int.Parse(input);
            break;
        case 6:
            employee.orderId = input;
            break;
        default:
            return false;
    }
    Console.WriteLine();
    return AskForAnother("обновить данные");
}
bool UpdateOrder()
{
    Console.WriteLine("Напишите ID заказа, у которого хотите переписать данные\n");
    string input = Console.ReadLine();
    if (input == "") return false;
    int orderId = int.Parse(input);
    Order order = ListOrders.Where(o => o.id == orderId).First();

    Console.WriteLine("Какие данные вы хотите изменить?");
    Console.WriteLine("1.ID");
    Console.WriteLine("2.Название");
    Console.WriteLine("3.Тип");
    Console.WriteLine("4.Стоимость");
    Console.WriteLine("5.Работники");
    Console.WriteLine("6.Заказчик\n");

    input = Console.ReadLine();
    if (input == "") return false;
    int chosenField = int.Parse(input);

    Console.Write("\nНовые данные для этого поля: ");
    input = Console.ReadLine();
    if (input == "") return false;
    switch (chosenField)
    {
        case 1:
            foreach (Client client in ListClients.Where(c => c.orders.Split(',').Any(i => i == orderId.ToString())))
            {
                string[] orders = client.orders.Split(',');
                for (int i = 0; i < orders.Length; i++) 
                if (orders[i] == orderId.ToString()) orders[i] = input;
                client.orders = String.Join(",", orders);
            }
            foreach (Employee employee in ListEmployees.Where(e => e.orderId.Split(',').Any(i => i == orderId.ToString())))
            {
                string[] orders = employee.orderId.Split(',');
                for (int i = 0; i < orders.Length; i++)
                    if (orders[i] == orderId.ToString()) orders[i] = input;
                employee.orderId = String.Join(",", orders);
            }
            order.id = int.Parse(input);
            break;
        case 2:
            order.name = input;
            break;
        case 3:
            order.type = input;
            break;
        case 4:
            order.cost = int.Parse(input);
            break;
        case 5:
            order.employeeId = input;
            break;
        case 6:
            order.clientId = input;
            break;
        default:
            return false;
    }
    Console.WriteLine();
    return AskForAnother("обновить данные");
}
//</Обновление>

//<Сохранение>
void SaveInfo()
{
    string[] Employees = new string[ListEmployees.Count];
    for (int i = 0; i < Employees.Length; i++)
    {
        Employee employee = ListEmployees[i];
        Employees[i] = $"{employee.id}|{employee.fullName}|{employee.age}|{employee.position}|{employee.totalEarned}|{employee.orderId}|";
    }
    File.WriteAllLines(pathEmployees,Employees);

    string[] Clients = new string[ListClients.Count];
    for (int i = 0; i < Clients.Length; i++)
    {
        Client client = ListClients[i];
        Clients[i] = $"{client.id}|{client.fullName}|{client.age}|{client.orders}|{client.totalSpent}|";
    }
    File.WriteAllLines(pathClients, Clients);

    string[] Orders = new string[ListOrders.Count];
    for (int i = 0; i < Orders.Length; i++)
    {
        Order order = ListOrders[i];
        Orders[i] = $"{order.id}|{order.name}|{order.type}|{order.cost}|{order.employeeId}|{order.clientId}|";
    }
    File.WriteAllLines(pathOrders, Orders);
}
//<.Сохранение>