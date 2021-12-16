using System.Collections.Generic;

namespace _Scripts.InventorySystem
{
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
            if (sodiumMilligrams < 3400/3)
            {
                return true;
            }

            return false;
        }

        public static string GetNutritionStringOfIngredient(IngredientObject ingredient)
        {
            var result = "";
            
            var i = ingredient.nutrition;

            if(i.cholesterol> 0){result += $"{nameof(i.cholesterol)} {i.cholesterol} mg\n";}
            if(i.carbohydrate> 0){result += $"{nameof(i.carbohydrate)} {i.carbohydrate} mg\n";}
            if(i.sugars> 0){result += $"{nameof(i.sugars)} {i.sugars} mg\n";}
            if(i.fiber> 0){result += $"{nameof(i.fiber)} {i.fiber} mg\n";}
            if(i.proteins> 0){result += $"{nameof(i.proteins)} {i.proteins} mg\n";}
            if(i.fat> 0){result += $"{nameof(i.fat)} {i.fat} mg\n";}
            if(i.saturatedfat> 0){result += $"{nameof(i.saturatedfat)} {i.saturatedfat} mg\n";}
            if(i.water> 0){result += $"{nameof(i.water)} {i.water} mg\n";}
            if(i.potassium> 0){result += $"{nameof(i.potassium)} {i.potassium} mg\n";}
            if(i.sodium> 0){result += $"{nameof(i.sodium)} {i.sodium} mg\n";}
            if(i.calcium> 0){result += $"{nameof(i.calcium)} {i.calcium} mg\n";}
            if(i.phosphorus> 0){result += $"{nameof(i.phosphorus)} {i.phosphorus} mg\n";}
            if(i.magnesium> 0){result += $"{nameof(i.magnesium)} {i.magnesium} mg\n";}
            if(i.zinc> 0){result += $"{nameof(i.zinc)} {i.zinc} mg\n";}
            if(i.iron> 0){result += $"{nameof(i.iron)} {i.iron} mg\n";}
            if(i.manganese> 0){result += $"{nameof(i.manganese)} {i.manganese} mg\n";}
            if(i.copper> 0){result += $"{nameof(i.copper)} {i.copper} mg\n";}
            if(i.selenium> 0){result += $"{nameof(i.selenium)} {i.selenium} mg\n";}
            if(i.vitaminB1> 0){result += $"{nameof(i.vitaminB1)} {i.vitaminB1} mg\n";}
            if(i.vitaminB2> 0){result += $"{nameof(i.vitaminB2)} {i.vitaminB2} mg\n";}
            if(i.vitaminB3> 0){result += $"{nameof(i.vitaminB3)} {i.vitaminB3} mg\n";}
            if(i.vitaminB5> 0){result += $"{nameof(i.vitaminB5)} {i.vitaminB5} mg\n";}
            if(i.vitaminB6> 0){result += $"{nameof(i.vitaminB6)} {i.vitaminB6} mg\n";}
            if(i.vitaminB7> 0){result += $"{nameof(i.vitaminB7)} {i.vitaminB7} mg\n";}
            if(i.vitaminB9> 0){result += $"{nameof(i.vitaminB9)} {i.vitaminB9} mg\n";}
            if(i.vitaminB12> 0){result += $"{nameof(i.vitaminB12)} {i.vitaminB12} mg\n";}
            if(i.vitaminC> 0){result += $"{nameof(i.vitaminC)} {i.vitaminC} mg\n";}
            if(i.vitaminA> 0){result += $"{nameof(i.vitaminA)} {i.vitaminA} mg\n";}
            if(i.vitaminD> 0){result += $"{nameof(i.vitaminD)} {i.vitaminD} mg\n";}
            if(i.vitaminE> 0){result += $"{nameof(i.vitaminE)} {i.vitaminE} mg\n";}
            if(i.vitaminK> 0){result += $"{nameof(i.vitaminK)} {i.vitaminK} mg\n";}

            return result;
        }

        public void GetCalculateNutritionOfFood(FoodObject foodToCalculate)
        {
            
        }
    }
}