using RepositoryJson.BaseRepository.Repositorys;
using RepositoryJson.Samples.RepositorySample.Entitys;

namespace RepositoryJson.Samples.RepositorySample 
{
    public class ContaRepository : RepositoryJson<Conta>
    {
        public ContaRepository() : base("Contas.json") {}
    }
}