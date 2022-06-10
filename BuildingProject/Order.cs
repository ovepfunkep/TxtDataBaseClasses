public class Order
{
    private int Id;
    private string Name;
    private string Type;
    private int Cost;
    private string EmployeeId;
    private string ClientId;

    public int id
    {
        get { return Id; }
        set { Id = value; }
    }
    public string name
    {
        get { return Name; }
        set { Name = value; }
    }
    public string type
    {
        get { return Type; }
        set { Type = value; }
    }
    public int cost
    {
        get { return Cost; }
        set { Cost = value; }
    }
    public string employeeId
    {
        get { return EmployeeId; }
        set { EmployeeId = value; }
    }
    public string clientId
    {
        get { return ClientId; }
        set { ClientId = value; }
    }

    public Order(int _id, string _name, string _type, int _cost, string _employeeId, string _clientId)
    {
        id = _id;
        name = _name;
        type = _type;
        cost = _cost;
        employeeId = _employeeId;
        clientId = _clientId;
    }

    public void ShowInfo()
    {
        Console.WriteLine($"{id} | {name} | {type} | {cost} | {employeeId} | {clientId}");
    }

    public void UpdateInfo(int _id, string _name, string _type, int _cost, string _employeeId, string _clientId)
    {
        id = _id;
        name = _name;
        type = _type;
        cost = _cost;
        employeeId = _employeeId;
        clientId = _clientId;
    }
}