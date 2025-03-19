namespace Tema1PS.Presenter;

public interface IEmployeeGUI<T>
{
    Task<List<T>> GetEmployeesAsync();
    Task AddEmployeeAsync(string name);
    Task UpdateEmployeeAsync(int id, string newName);
    Task DeleteEmployeeAsync(int id);
    Task<T> GetEmployeeByIdAsync(int id); 
}