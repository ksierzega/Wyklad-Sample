using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5.Metoda_wytwórcza
{
    class Program
    {
        static void Main(string[] args)
        {
            ICommandFactory factory = new StandardCommandFactory();
                        
            ICommand cmd = factory.CreateChangeUserPasswordCmd(2, "stare hasło", "nowe hasło");
            cmd.Execute();

            Console.WriteLine();

            factory = new AdvancedCommandFactory();
            cmd = factory.CreateChangeUserPasswordCmd(2, "stare hasło", "nowe hasło");

            cmd.Execute();
        }
    }

    //Creator
    interface ICommandFactory
    {
        ICommand CreateChangeUserPasswordCmd(int userId, string oldPassword, string newPassword);
    }

    //Concrete Creator
    class StandardCommandFactory : ICommandFactory
    {
        public ICommand CreateChangeUserPasswordCmd(int userId, string oldPassword, string newPassword)
        {
            return new ChangeUserPasswordCmd(userId, oldPassword, newPassword);
        }
    }

    //Concrete Creator
    class AdvancedCommandFactory : ICommandFactory
    {
        public ICommand CreateChangeUserPasswordCmd(int userId, string oldPassword, string newPassword)
        {
            ICommand cmd = new ChangeUserPasswordCmd(userId, oldPassword, newPassword);
            ICommand withTransaction = new WithTransaction(cmd);
            ICommand withLoggingAndTransaction = new LoggingDecorator(withTransaction);
            return withLoggingAndTransaction;
        }
    }


    //Product
    public interface ICommand
    {
        void Execute();
    }


    //Concrete Product
    class ChangeUserPasswordCmd : ICommand
    {
        private readonly int _userId;
        private readonly string _oldPassword;
        private readonly string _newPassword;

        public ChangeUserPasswordCmd(int userId, string oldPassword, string newPassword)
        {
            _userId = userId;
            _oldPassword = oldPassword;
            _newPassword = newPassword;
        }

        public void Execute()
        {
            Console.WriteLine(string.Format("Zmieniam hasło użytkownika o Id: {0} z {1} na {2}", _userId, _oldPassword, _newPassword));
        }
    }

    //itp np AddUserCommand


    abstract class CommandDecorator : ICommand
    {
        protected readonly ICommand _decoratedCmd;

        public CommandDecorator(ICommand cmd)
        {
            _decoratedCmd = cmd;
        }

        public virtual void Execute()
        {
            _decoratedCmd.Execute();
        }
    }

    class LoggingDecorator : CommandDecorator
    {
        public LoggingDecorator(ICommand cmd) : base(cmd)
        {

        }

        public override void Execute()
        {
            Console.WriteLine("Przed wywoałeniem execute");
            base.Execute();
            Console.WriteLine("Po wywołaniu execute");
        }
    }

    class WithTransaction : CommandDecorator
    {
        public WithTransaction(ICommand cmd) : base(cmd)
        {

        }

        public override void Execute()
        {
            Console.WriteLine("Otwieram transakcje");
            base.Execute();
            Console.WriteLine("Zamykam transakcje");
        }
    }
}
