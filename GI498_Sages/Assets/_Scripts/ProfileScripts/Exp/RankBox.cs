using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RankBox : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image rankImage;
    [SerializeField] private TextMeshProUGUI rankNameText;
    [SerializeField] private GameObject newRecipe;
    [SerializeField] private GameObject newIngredient;

    //[Header("Asset")]
    //[SerializeField]
    //private Sprite rank;

    public void SetDetail(Rank rank)
    {
        rankNameText.text = rank.rankName;

        //rankImage.sprite = rank.sprite;
        if(rank.sprite != null)
        {
            rankImage.sprite = rank.sprite;
        }
        //newRecipe.GetComponent<Image>().sprite = rank.newRecipe.image;
        //newIngredient.GetComponent<Image>().sprite = rank.newIngredient.image;

        //if(rank.newRecipe == null)
        //{
        //    Destroy(newRecipe.gameObject);
        //}
        //else
        //{
        //    //newRecipe.GetComponent<Button>().onClick.AddListener(()=> RecipeInfoManager.Instance.OpenRecipeInfo(recipe class));
        //}
        newRecipe.GetComponent<Button>().onClick.AddListener(() => RecipeInfoManager.Instance.OpenRecipeInfo());


        //if(rank.newIngredient == null)
        //{
        //    Destroy(newIngredient.gameObject);
        //}
        //else
        //{
        //    //newIngredient.GetComponent<Button>().onClick.AddListener(() => IngredientInfoManager.Instance.OpenIngredientInfo(recipe class));
        //}
        newIngredient.GetComponent<Button>().onClick.AddListener(() => IngredientInfoManager.Instance.OpenIngredientInfo());
    }
}
