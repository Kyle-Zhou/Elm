using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler
{

    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = gameObject.GetComponent<Button>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SoundManager.PlaySound(SoundManager.Sound.click);
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        SoundManager.PlaySound(SoundManager.Sound.popToClick);
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        SoundManager.PlaySound(SoundManager.Sound.clickToPop);
    }

    public void PlayPopClick()
    {
        SoundManager.PlaySound(SoundManager.Sound.pop);
    }

    public void CookingPotBuy()
    {
        //if can buy play cooking pot sound
        //else play error noise
        //oh wait no cooking pot will only show buy button if u can buy it say bet
        //SoundManager.PlaySound(SoundManager.Sound.)
    }

}
