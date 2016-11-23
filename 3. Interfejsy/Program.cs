using System;

namespace Interfejsy
{
    class Program
    {
        static void Main(string[] args)
        {
            Ptak ćwirek = new Ptak();
            ćwirek.Biegaj();
            ćwirek.Lataj();

            IMogęLatać ktośKtoMożeLatać = ćwirek;
            ktośKtoMożeLatać.Lataj();
        }
    }

    interface IMogęLatać
    {
        void Lataj();
    }

    interface IMogęBiegać
    {
        void Biegaj();
    }

    class Zwierze : IMogęBiegać
    {
        public void Biegaj()
        {
            Console.WriteLine("Biegam");
        }
    }

    class Ptak : Zwierze, IMogęLatać
    {
        public void Lataj()
        {
            Console.WriteLine("Latam");
        }
    }

}
