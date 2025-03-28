namespace Tema1PS.Presenter
{
    public interface IMovieGUI
    {
        int GetMovieId();
        int GetDeleteMovieId();
        string GetMovieTitle();
        int GetMovieYear();
        string GetMovieCategory();
        string GetMovieType();
        int GetMovieDirectorId();
        int GetMovieScreenWriterId();
        List<int> GetMovieActorIds();
        string GetMoviePhoto1();
        string GetMoviePhoto2();
        string GetMoviePhoto3();
    }
}