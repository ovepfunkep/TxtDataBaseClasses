public class Client
{
    private int Id;
    private string FullName;
    private int Age;
    private string Orders;
    private int TotalSpent;

    public int id
    {
        get { return Id; }
        set { Id = value; }
    }
    public string fullName 
    { 
        get { return FullName; } 
        set { FullName = value; }
    }
    public int age
    {
        get { return Age; }
        set { Age = value; }
    }
    public string orders
    {
        get { return Orders; }
        set { Orders = value; }
    }
    public int totalSpent
    {
        get { return TotalSpent; }
        set { TotalSpent = value; }
    }

    public Client(int _id, string _fullName, int _age, string _orders, int _totalSpent)
    {
        id = _id;
        fullName = _fullName;
        age = _age;
        orders = _orders;
        totalSpent = _totalSpent;
    }

    public void ShowInfo()
    {
        Console.Write($"{id} | {fullName} | {age} | {orders} | {totalSpent}\n");
    }

    public void UpdateInfo(int _id, string _fullName, int _age, string _orders, int _totalSpent)
    {
        id = _id;
        fullName = _fullName;
        age = _age;
        orders = _orders;
        totalSpent = _totalSpent;
    }
}