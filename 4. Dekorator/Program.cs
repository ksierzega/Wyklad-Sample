using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4.Dekorator
{
    class Program
    {
        static void Main(string[] args)
        {
            ChangeUserPasswordCmd cmd = new ChangeUserPasswordCmd(2, "qwerty123", "QWERTY1234");
            cmd.Execute();

            Console.WriteLine();

            ICommand withTransaction = new TransactionDecorator(cmd);
            ICommand withLoggingAndTransaction = new LoggingDecorator(withTransaction);

            withLoggingAndTransaction.Execute();
            }
    }

    public interface ICommand
    {
        void Execute();
    }

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
            var sw = Stopwatch.StartNew();
            Console.WriteLine(string.Format("{0}: {1}", DateTime.Now, "Przed wywoałeniem execute"));

            base.Execute();

            sw.Stop();
            Console.WriteLine("{0}: {1}", DateTime.Now, "Po wywołaniu execute, czas trwania (ticks): " + sw.ElapsedTicks);
        }
    }

    class TransactionDecorator : CommandDecorator
    {
        public TransactionDecorator(ICommand cmd) : base(cmd)
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
