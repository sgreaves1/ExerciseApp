using System;

namespace ExerciseApp.Eunmerators
{
    public enum WeightUnits
    {
        None = 0,
        Kg = 1,
        Lbs = 2
    }

    public class WeightUnitsHelper
    {
        public static WeightUnits FromString(string text)
        {
            if (text != null)
            {
                if (text == "Kg")
                    return WeightUnits.Kg;

                if (text == "Lbs")
                    return WeightUnits.Lbs;
            }
            
            return WeightUnits.None;
        }
    }
}