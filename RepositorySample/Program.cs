using System;
using RepositoryJson.Samples.RepositorySample.Entitys;

namespace RepositoryJson.Samples.RepositorySample
{
    class Program
    {
        static void Main(string[] args)
        {
            var repository = new ContaRepository();

            var conta = new Conta
            {
                Nome = "Bradesco",
                SaldoInicial = 100,
                Saldo = 100
            };

            repository.Save(conta).Wait();
        }
    }
}
