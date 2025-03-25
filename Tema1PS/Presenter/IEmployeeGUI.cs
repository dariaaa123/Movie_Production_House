namespace Tema1PS.Presenter
{
    public interface IEmployeeGUI
    {
        // For Actor, you need the ID and Name.
        int GetEmployeeId();
        string GetEmployeeName();
        
        public string GetNewEmployeeName();
        public int GetNewEmployeeId();
        public int GetDeletingEmployeeId();
        public string GetDeletingEmployeeName();
    }
}