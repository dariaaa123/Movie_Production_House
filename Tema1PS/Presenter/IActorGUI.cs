namespace Tema1PS.Presenter;

public interface IActorGUI
{
    Task<List<ActorDTO>> GetActorsAsync();
    Task AddActorAsync(string name);
    Task UpdateActorAsync(int id, string newName);
    Task DeleteActorAsync(int id);
}