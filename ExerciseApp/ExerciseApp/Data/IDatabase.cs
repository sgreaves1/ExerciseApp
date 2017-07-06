using ExerciseApp.Model;

namespace ExerciseApp.Data
{
    public interface IDatabase
    {
        string CreateDatabase();
        string InsertData(WorkoutRoutine data);
        WorkoutRoutine GetTodaysRoutine();
    }
}