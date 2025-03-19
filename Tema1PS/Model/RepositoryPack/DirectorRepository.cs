using Tema1PS.DataBase;
using Tema1PS.Model.RepositoryPack;

namespace Tema1PS.Model.Repositories
{
    public class DirectorRepository : Repository<Director>
    {
        public DirectorRepository(ProdHouseContext context) : base(context) { }
    }
}