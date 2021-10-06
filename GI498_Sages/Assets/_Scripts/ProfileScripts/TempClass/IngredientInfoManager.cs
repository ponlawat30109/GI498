using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class IngredientInfoManager : MonoBehaviour
{
    public static IngredientInfoManager Instance { get; private set; }
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI ingredientName;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private Image image;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button closeBlankAreaExit;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(panel != null, "RecipeInfo UI (" + name + "): panel is null");
        Debug.Assert(ingredientName != null, "RecipeInfo UI (" + name + "): name is null");
        Debug.Assert(description != null, "RecipeInfo UI (" + name + "): description is null");
        Debug.Assert(image != null, "RecipeInfo UI (" + name + "): image is null");
        Debug.Assert(closeButton != null, "RecipeInfo UI (" + name + "): imcloseButtonage is null");
        Debug.Assert(closeBlankAreaExit != null, "RecipeInfo UI (" + name + "): closeBlankExit is null");

        panel.SetActive(false);

        closeBlankAreaExit.onClick.AddListener(ClosePanel);
        closeButton.onClick.AddListener(ClosePanel);
    }


    public void OpenIngredientInfo(/*ingredient class*/)
    {
        //name = recipe.name;
        //description = recipe.description;
        //image.sprite = recipe.image;
        panel.SetActive(true);
    }

    private void ClosePanel()
    {
        panel.SetActive(false);
    }
}
