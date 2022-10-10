
namespace DesignPatterns.Command
{ 
    public class BankAccount
    {
        private int balance;
        private const int overdraftLimit = -500;

        public void Deposit(int amount)
        {
            balance += amount;
            Console.WriteLine($"Deposited {amount}, balance is now {balance}");
        }

        public void Withdraw(int amount)
        {
            if (balance - amount >= overdraftLimit)
            {
                balance -= amount;
                Console.WriteLine($"Withdraw {amount}, balance is now {balance}");
            }
        }

        public override string ToString()
        {
            return $"{nameof(balance)}: {balance}";
        }
    }

    public interface ICommand
    {
        void Call();
    }

    public class BankAccountCommand : ICommand
    {
        private BankAccount _account;

        public enum Action
        {
            Deposit, Withdraw
        }
        
        private Action _action;
        private int _amount;

        public BankAccountCommand(BankAccount account, Action action, int amount)
        {
            _account = account ?? throw new ArgumentNullException(paramName: nameof(account));
            _action = action;
            _amount = amount;
        }

        public void Call()
        {
            switch (_action)
            {
                case Action.Deposit:
                    _account.Deposit(_amount);
                    break;
                case Action.Withdraw:
                    _account.Withdraw(_amount);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
    public class Command
    {
        public static void Main(string[] args)
        {
            var ba = new BankAccount();
            var commands = new List<BankAccountCommand>
            {
                new BankAccountCommand(ba, BankAccountCommand.Action.Deposit, 200),
                new BankAccountCommand(ba, BankAccountCommand.Action.Withdraw, 100),
            };

            Console.WriteLine(ba);

            foreach(var c in commands)
                c.Call();

            Console.WriteLine(ba);
        }
    }
}