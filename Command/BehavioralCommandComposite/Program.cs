namespace DesignPatterns.Behavioral.Command.Composite
{
    public class BankAccount
    {
        private int balance;
        private const int overdraftLimit = -500;

        public BankAccount(int balance = 0)
        {
            this.balance = balance;
        }

        public void Deposite(int amount)
        {
            balance += amount;
            Console.WriteLine($"Deposite ${amount}, balance is now {balance}");
        }

        public bool Withdraw(int amount)
        {
            if (balance - amount >= overdraftLimit)
            {
                balance -= amount;
                Console.WriteLine($"Withdraw ${amount}, balance is now {balance}");
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
        bool Success { get; set; }
    }

    public class BankAccountCommand : ICommand
    {
        private BankAccount _account;

        public enum Action
        {
            Deposite, Withdraw
        }

        private Action _action;
        private int _amount;
        private bool _succeeded;

        public BankAccountCommand(BankAccount account, Action action, int amount)
        {
            _account = account;
            _action = action;
            _amount = amount;
        }

        public void Call()
        {
            switch (_action)
            {
                case Action.Deposite:
                    _account.Deposite(_amount);
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
                case Action.Deposite:
                    _account.Withdraw(_amount);
                    break;
                case Action.Withdraw:
                    _account.Deposite(_amount);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public bool Success
        {
            get { return _succeeded; }
            set { _succeeded = value; }
        }
    }

    public class CompositeBankAccountCommand
        : List<BankAccountCommand>, ICommand
    {
        public CompositeBankAccountCommand()
        {

        }

        public CompositeBankAccountCommand(IEnumerable<BankAccountCommand> collection) : base(collection)
        {

        }
        public virtual void Call()
        {
            ForEach(cmd => cmd.Call());
        }

        public virtual void Undo()
        {
            foreach(var cmd in
                ((IEnumerable<BankAccountCommand>)this).Reverse())
            {
                if (cmd.Success) cmd.Undo();
            }
        }

        public virtual bool Success 
        {
            get
            {
                return this.All(cmd => cmd.Success);
            }
            set
            {
                foreach (var cmd in this)
                    cmd.Success = value;
            }
        }
    }

    public class MoneyTransferCommand: CompositeBankAccountCommand
    {
        public MoneyTransferCommand (BankAccount from, BankAccount to, int amount)
        {
            AddRange(new[]
            {
                new BankAccountCommand(from,
                BankAccountCommand.Action.Withdraw, amount),
                new BankAccountCommand(to,
                BankAccountCommand.Action.Deposite, amount),
            });
        }

        public override void Call()
        {
            BankAccountCommand last = null;
            foreach(var cmd in this)
            {
                if (last == null || last.Success)
                {
                    cmd.Call();
                    last = cmd;
                }
                else
                {
                    cmd.Undo();
                    break;
                }
            }
        }
    }
    public class Program
    {
        static void Main(string[] args)
        {
            var from = new BankAccount();
            from.Deposite(100);
            var to = new BankAccount();

            var mtc = new MoneyTransferCommand(from, to, 100);
            mtc.Call();


            Console.WriteLine(from);
            Console.WriteLine(to);

            mtc.Undo();

            Console.WriteLine(from);
            Console.WriteLine(to);
        }
    }
}