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
            ICommand cmd = new ChangeUserPassowrdCmd(2, "qwerty123", "QWERTY1234");

            ICommandFactory factory = new StandardCommandFactory();

            
            cmd = factory.CreateCommand(cmd);
            cmd.Execute();

            Console.WriteLine();

            factory = new AdvancedCommandFactory();
            cmd = factory.CreateCommand(cmd);

            cmd.Execute();
        }
    }

    interface ICommandFactory
    {
        ICommand CreateCommand(ICommand cmd);
    }

    class StandardCommandFactory : ICommandFactory
    {
        public ICommand CreateCommand(ICommand cmd)
        {
            return cmd;
        }
    }

    class AdvancedCommandFactory : ICommandFactory
    {
        public ICommand CreateCommand(ICommand cmd)
        {
            ICommand withTransaction = new WithTransaction(cmd);
            ICommand withLoggingAndTransaction = new WithLogging(withTransaction);
            return withLoggingAndTransaction;
        }
    }



    public interface ICommand
    {
        void Execute();
    }

    class ChangeUserPassowrdCmd : ICommand
    {
        private readonly int _userId;
        private readonly string _oldPassword;
        private readonly string _newPassword;

        public ChangeUserPassowrdCmd(int userId, string oldPassword, string newPassword)
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

    class WithLogging : CommandDecorator
    {
        public WithLogging(ICommand cmd) : base(cmd)
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
