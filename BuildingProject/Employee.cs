public class Employee
{
    private int Id;
    private string FullName;
    private int Age;
    private string Position;
    private int TotalEarned;
    private string OrderId;

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
    public string position
    {
        get { return Position; }
        set { Position = value; }
    }
    public int totalEarned
    {
        get { return TotalEarned; }
        set { TotalEarned = value; }
    }
    public string orderId
    {
        get { return OrderId; }
        set { OrderId = value; }
    }

    public Employee(int _id, string _fullName, int _age, string _position, int _totalEarned, string _orderId)
    {
        id = _id;
        fullName = _fullName;
        age = _age;
        position = _position;
        totalEarned = _totalEarned;
        orderId = _orderId;
    }

    public void ShowInfo()
    {
        Console.WriteLine($"{id} | {fullName} | {age} | {position} | {totalEarned} | {orderId}");
    }

    public void UpdateInfo(int _id, string _fullName, int _age, string _position, int _totalEarned, string _orderId)
    {
        id = _id;
        fullName = _fullName;
        age = _age;
        position = _position;
        totalEarned = _totalEarned;
        orderId = _orderId;
    }
}