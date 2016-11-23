using System;

namespace Dziedziczenie
{
    class Program
    {
        static void Main(string[] args)
        {
            Zwierze jakiesZwierze = new Zwierze();
            jakiesZwierze.Biegaj(); // patataj

            Pies reksio = new Pies();
            reksio.Biegaj(); // patataj
            reksio.Szczekaj(); // hau hau

            jakiesZwierze = reksio;
            jakiesZwierze.Biegaj(); // patataj
                                    // jakiesZwierze.Szczekaj(); - błąd kompilacji

        }
    }

    class Zwierze
    {
        public void Biegaj()
        {
            Console.WriteLine("patataj");
        }
    }

    class Ssak : Zwierze
    {

    }

    class Pies : Ssak
    {
        public void Szczekaj()
        {
            Console.WriteLine("hau hau");
        }
    }
}
