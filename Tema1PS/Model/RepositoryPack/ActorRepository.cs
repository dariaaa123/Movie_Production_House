using Tema1PS.DataBase;
using Tema1PS.Model.RepositoryPack;

namespace Tema1PS.Model.Repositories
{
    public class ActorRepository : Repository<Actor>
    {
        public ActorRepository(ProdHouseContext context) : base(context) { }
    }
}