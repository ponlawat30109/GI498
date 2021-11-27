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

    public void SetDetail(Rank rank, bool isActive)
    {
        rankNameText.text = rank.rankName;

        //rankImage.sprite = rank.sprite;
        if(rank.sprite != null)
        {
            rankImage.sprite = rank.sprite;
        }

        if (rank.newRecipe == null)
        {
            newRecipe.GetComponent<Image>().enabled = false;
            Destroy(newRecipe.GetComponent<Button>());
        }
        else
        {
            newRecipe.GetComponent<Image>().sprite = rank.newRecipe.itemIcon;
            newRecipe.GetComponent<Button>().onClick.AddListener(() => RecipeInfoManager.Instance.OpenRecipeInfo(rank.newRecipe));
            newRecipe.GetComponent<Button>().interactable = isActive;
        }

        if (rank.newIngredient == null)
        {
            newIngredient.GetComponent<Image>().enabled = false;
            Destroy(newIngredient.GetComponent<Button>());
        }
        else
        {
            newIngredient.GetComponent<Image>().sprite = rank.newIngredient.itemIcon;
            newIngredient.GetComponent<Button>().onClick.AddListener(() => IngredientInfoManager.Instance.OpenIngredientInfo(rank.newIngredient));
            newIngredient.GetComponent<Button>().interactable = isActive;
        }
    }

    public void ActiveDetail(bool haveRecipe, bool haveIngredient)
    {
        if (haveRecipe) newRecipe.GetComponent<Button>().interactable = haveRecipe;
        if (haveIngredient) newIngredient.GetComponent<Button>().interactable = haveIngredient;
    }
}
