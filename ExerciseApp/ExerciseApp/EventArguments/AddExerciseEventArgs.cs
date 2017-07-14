using System;

namespace ExerciseApp.EventArguments
{
    public class AddExerciseEventArgs : EventArgs
    {
        public string Name { get; set; }
        public string Gif { get; set; }
    }
}