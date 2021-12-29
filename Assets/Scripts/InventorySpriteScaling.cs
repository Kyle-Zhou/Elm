using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySpriteScaling : MonoBehaviour
{
    void Start() //
    {
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        RectTransform parentRectTransform = transform.parent.GetComponent<RectTransform>();
        rectTransform.localScale = new Vector3(1,1,1);
        rectTransform.anchoredPosition = parentRectTransform.anchoredPosition;
        parentRectTransform.localScale = new Vector3(1,1,1);
    }

}
