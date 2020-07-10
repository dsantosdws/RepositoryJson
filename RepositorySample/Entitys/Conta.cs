using RepositoryJson.BaseRepository.BaseEntitys;

namespace RepositoryJson.Samples.RepositorySample.Entitys 
{
    public class Conta : BaseEntity
    {
        public string Nome { get; set; }

        public decimal SaldoInicial { get; set; }

        public decimal Saldo { get; set; }

        public bool Status { get; set; }
    }
}