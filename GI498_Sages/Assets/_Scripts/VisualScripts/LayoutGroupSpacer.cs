using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("_VisualMod/LayoutGroupSpacer")]
public class LayoutGroupSpacer : MonoBehaviour
{
    private GridLayoutGroup gLParent;
    private RectTransform rectParent;
    [SerializeField] private float maxSpacing = 40f;
    [SerializeField] private float minSpacing = 20f;
    [SerializeField] private float widthOffset = 1f;

    private void Awake()
    {
        gLParent = GetComponent<GridLayoutGroup>();
        rectParent = GetComponent<RectTransform>();
    }

    public void LayoutSpacing()
    {
        try
        {
            if (gLParent == null) return;

            var fullWidth = rectParent.rect.width;
            var cellWidth = gLParent.cellSize.x;
            if (fullWidth <= 0 || cellWidth == 0) return;

            var contentWidth = fullWidth - gLParent.padding.left - gLParent.padding.right;
            var maxColumn = Mathf.FloorToInt((contentWidth + minSpacing) / (cellWidth + minSpacing));
            var allFreeSpace = contentWidth - (cellWidth * maxColumn);
            var oneFreeSpace = allFreeSpace / (maxColumn - 1);

            oneFreeSpace = ((oneFreeSpace < minSpacing) ? minSpacing : ((oneFreeSpace > maxSpacing) ? maxSpacing : oneFreeSpace)) - widthOffset;

            gLParent.spacing = new Vector2(oneFreeSpace, gLParent.spacing.y);
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }
    }

}
