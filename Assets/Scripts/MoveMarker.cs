using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MoveMarker : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject mark;

    public void OnPointerEnter(PointerEventData eventData)
    {
        mark.SetActive(true);
        mark.transform.position = gameObject.transform.position;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mark.SetActive(false);

    }

}