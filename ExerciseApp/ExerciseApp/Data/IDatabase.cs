using ExerciseApp.Model;
using System.Collections.Generic;

namespace ExerciseApp.Data
{
    public interface IDatabase
    {
        string CreateDatabase();
        string InsertData(WorkoutRoutine data);
        WorkoutRoutine GetTodaysRoutine();
        List<Exercise> GetExercisesByRoutineId(int routineId);
    }
}