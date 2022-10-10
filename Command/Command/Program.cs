
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

        public bool Withdraw(int amount)
        {
            if (balance - amount >= overdraftLimit)
            {
                balance -= amount;
                Console.WriteLine($"Withdraw {amount}, balance is now {balance}");
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return $"{nameof(balance)}: {balance}";
        }
    }

    public interface ICommand
    {
        void Call();
        void Undo();
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
        private bool _succeeded = false;

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
                    _succeeded = true;
                    break;
                case Action.Withdraw:
                    _succeeded = _account.Withdraw(_amount);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Undo()
        {
            if (!_succeeded) return;
            switch (_action)
            {
                case Action.Deposit:
                    _account.Withdraw(_amount);
                    break;
                case Action.Withdraw:
                    _account.Deposit(_amount);
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
                new BankAccountCommand(ba, BankAccountCommand.Action.Withdraw, 10000),
            };

            Console.WriteLine(ba);

            foreach(var c in commands)
                c.Call();

            Console.WriteLine(ba);

            foreach (var c in Enumerable.Reverse(commands))
                c.Undo();

            Console.WriteLine(ba);
        }
    }
}