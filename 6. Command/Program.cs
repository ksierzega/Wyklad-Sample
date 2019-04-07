using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6.Command
{
    class Program
    {
        static void Main(string[] args)
        {
            ButtonSample();
            CommandBusSample();
        }

        private static void ButtonSample()
        {
            ICommand cmd = new ChangeUserPasswordCmd(1, "zxc", "asd");
            Button btn = new Button(cmd);

            btn.Click();

            ICommand cmd2 = new AddUserCommnad("jan", "kowalski");
            btn.SetClickCommnad(cmd2);

            btn.Click();
        }

        private static void CommandBusSample()
        {
            CommandBus bus = new CommandBus();

            ICommand cmd = new ChangeUserPasswordCmd(1, "qwert1234", "QWERTY1234");

            bus.Send(cmd);

            cmd = new AddUserCommnad("jan", "kowalski");

            bus.Send(cmd);
        }
    }

    public interface ICommand
    {
        void Execute();
    }

    //Concrete command
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
            //Receiver
            Console.WriteLine(string.Format("Zmieniam hasło użytkownika o Id: {0} z {1} na {2}", _userId, _oldPassword, _newPassword));
        }
    }

    //Concrete command
    class AddUserCommnad : ICommand
    {
        private readonly string _firstName;
        private readonly string _lastName;

        public AddUserCommnad(string firstName, string lastName)
        {
            _firstName = firstName;
            _lastName = lastName;
        }

        public void Execute()
        {
            //Receiver
            Console.WriteLine("Tworzenie użytkownika: {0} {1}", _firstName, _lastName);
        }
    }


    //Invoker
    class Button
    {
        private ICommand _clickCmd;

        public Button(ICommand clickCmd)
        {
            _clickCmd = clickCmd;
        }

        public void Click()
        {
            _clickCmd.Execute();
        }

        internal void SetClickCommnad(ICommand clickCmd)
        {
            _clickCmd = clickCmd;
        }
    }



    class CommandBus
    {
        public void Send(ICommand cmd)
        {
            //wykonuj za pomocą kolejki czy współbieżnie
            cmd.Execute();
        }
    }  
}
