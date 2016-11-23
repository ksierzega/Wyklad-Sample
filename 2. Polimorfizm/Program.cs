using System;

namespace Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            Pies reksio = new Pies();
            reksio.DajGłos(); // hau hau
            reksio.Biegaj(); // patataj

            Ssak ssak = reksio;
            ssak.DajGłos(); // hau hau

            Królik tuptuś = new Królik();
            tuptuś.Biegaj(); // kic kic

            ssak = tuptuś;
            ssak.Biegaj(); // kic kic
        }
    }

    abstract class Ssak
    {
        public abstract void DajGłos();

        public virtual void Biegaj()
        {
            Console.WriteLine("patataj");
        }
    }

    class Pies : Ssak
    {
        public override void DajGłos()
        {
            Console.WriteLine("Hau hau");
        }
    }

    class Kot : Ssak
    {
        public override void DajGłos()
        {
            Console.WriteLine("Miau miau");
        }
    }

    class Królik : Ssak
    {
        public override void DajGłos()
        {
            Console.WriteLine("...");
        }

        public override void Biegaj()
        {
            Console.WriteLine("kic kic");
        }
    }
}

