// See https://aka.ms/new-console-template for more information

class Program
{
    static void Main(string[] args)
    {
        ILogger logger = new FIleLogger("mylog.txt");
        BankAccount account1 = new BankAccount("Fred", 100, logger);
        BankAccount account2 = new BankAccount("Daniel", 200, logger);

        List<BankAccount> accounts = new List<BankAccount>();
        accounts.Add(account1);
        accounts.Add(account2);

        foreach(BankAccount account in accounts) {
            Console.WriteLine(account.Balance);
        }

        List<int> numbers = new List<int>(){1,2,3,4};
        DataStore<int> store = new DataStore<int>();
        store.Value = 42;
        Console.WriteLine(store.Value);
    }
}

class DataStore<T>
{
    public T Value { get; set; }
}

class FIleLogger : ILogger
{
    private readonly string filePath;

    public FIleLogger(string filePath)
    {
        this.filePath = filePath;
    }

    public void Log(string message)
    {
        File.AppendAllText(filePath, $"{message}{Environment.NewLine}");
    }
}

class ConsoleLoger : ILogger
{
}

interface ILogger {
    
    public void Log(string message)
    {
        Console.WriteLine($"LOGGER: {message}");
    }
}

class BankAccount {
    private string name;
    private readonly ILogger logger;

    public decimal Balance {
        get; private set;
    }

    public BankAccount(string name, decimal balance, ILogger logger)
    {
        if(string.IsNullOrEmpty(name)) {
            throw new ArgumentNullException("Invalid name!", nameof(name));
        }
        if(balance < 0) {
            throw new ArgumentException("Balance must be positive!", "balance");
        }
        this.name = name;
        Balance = balance;
        this.logger = logger;
    }

    public void Deposit(decimal amount) {
        if(amount < 0) {
            logger.Log($"Not possible to deposit {amount} in {this.name}'s account.");
            return;
        }
        Balance += amount;
    }
}