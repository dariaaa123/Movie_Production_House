namespace Tema1PS.Presenter
{
    public interface IEmployeeGUI
    {
        int GetEmployeeId();
        string GetEmployeeName();
        
        public string GetNewEmployeeName();
        public int GetNewEmployeeId();
        public int GetDeletingEmployeeId();
        public string GetDeletingEmployeeName();
    }
}