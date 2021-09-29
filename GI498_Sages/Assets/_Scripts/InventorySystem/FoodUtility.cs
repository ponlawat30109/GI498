public class FoodUtility
{
    public static float MilligramsToGrams(float milligrams)
    {
        return milligrams / 1000;
    }

    public static float GramsToMilligrams(float grams)
    {
        return grams * 1000;
    }

    public static bool IsLowSodium(float sodiumMilligrams)
    {
        if (sodiumMilligrams < 140)
        {
            return true;
        }

        return false;
    }
}