using Tema1PS.DataBase;

namespace Tema1PS.Model.RepositoryPack;

public class ScreenWriterRepository:Repository<ScreenWriter>
{
    public ScreenWriterRepository(ProdHouseContext context) : base(context) { }
}