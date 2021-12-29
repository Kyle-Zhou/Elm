using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSpriteSelector : MonoBehaviour
{

    public Sprite spU, spD, spR, spL,
            spUD, spRL, spUR, spUL, spDR, spDL,
            spULD, spRUL, spDRU, spLDR, spUDRL;


    //public Image img;

	public Sprite box;

	public GameObject homeIconBox, bossBox;



	public bool up, down, left, right;

	public int type; // 0: normal, 1: enter

	public Color normalColor, enterColor, bossColor;
	Color mainColor;
	SpriteRenderer rend;
	void Start()
	{
		rend = GetComponent<SpriteRenderer>();
		mainColor = normalColor;
		PickSprite();
		PickColor();

		//Vector3 newScale = transform.localScale;
		//newScale *= 20;
		//transform.localScale = newScale;



		//Vector2 pos = transform.position;
		//pos.x += 5;
		//pos.y += 2.5f;

		Vector3 pos = transform.position;
		pos.x += 5;
		pos.y += 2.5f;
		pos.z += 4f;
		//pos.x += 25;
		//pos.y += 25f;
		transform.position = pos;

		//RectTransform rt = GetComponent<RectTransform>();
		//Vector2 pos = rt.position;

		//rt.anchoredPosition = new Vector3(300, 100);

		//GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
		//GetComponent<RectTransform>().anchorMax = Vector2.zero;
		//GetComponent<RectTransform>().anchorMin = Vector2.zero;
		//GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 1);

	}
	void PickSprite()
	{ //picks correct sprite based on the four door bools

		rend.sprite = box;

		//if (up)
		//{
		//	if (down)
		//	{
		//		if (right)
		//		{
		//			if (left)
		//			{
		//				rend.sprite = spUDRL;
		//				//img.sprite = spUDRL;
		//			}
		//			else
		//			{
		//				rend.sprite = spDRU;
		//				//img.sprite = spDRU;
		//			}
		//		}
		//		else if (left)
		//		{
		//			rend.sprite = spULD;
		//			//img.sprite = spULD;
		//		}
		//		else
		//		{
		//			rend.sprite = spUD;
		//			//img.sprite = spUD;
		//		}
		//	}
		//	else
		//	{
		//		if (right)
		//		{
		//			if (left)
		//			{
		//				rend.sprite = spRUL;
		//				//img.sprite = spRUL;
		//			}
		//			else
		//			{
		//				rend.sprite = spUR;
		//				//img.sprite = spUR;
		//			}
		//		}
		//		else if (left)
		//		{
		//			rend.sprite = spUL;
		//			//img.sprite = spUL;
		//		}
		//		else
		//		{
		//			rend.sprite = spU;
		//			//img.sprite = spU;
		//		}
		//	}
		//	return;
		//}
		//if (down)
		//{
		//	if (right)
		//	{
		//		if (left)
		//		{
		//			rend.sprite = spLDR;
		//			//img.sprite = spLDR;
		//		}
		//		else
		//		{
		//			rend.sprite = spDR;
		//			//img.sprite = spDR;
		//		}
		//	}
		//	else if (left)
		//	{
		//		rend.sprite = spDL;
		//		//img.sprite = spDL;
		//	}
		//	else
		//	{
		//		rend.sprite = spD;
		//		//img.sprite = spD;
		//	}
		//	return;
		//}
		//if (right)
		//{
		//	if (left)
		//	{
		//		rend.sprite = spRL;
		//		//img.sprite = spRL;
		//	}
		//	else
		//	{
		//		rend.sprite = spR;
		//		//img.sprite = spR;
		//	}
		//}
		//else
		//{
		//	rend.sprite = spL;
		//	//img.sprite = spL;
		//}
		
	}

	void PickColor()
	{ //changes color based on what type the room is
		if (type == 0)
		{
			mainColor = normalColor;
		}
		else if (type == 1)
		{
			mainColor = enterColor;
			rend.sprite = homeIconBox.GetComponent<SpriteRenderer>().sprite;
		}
		else if (type == 2)
        {
			mainColor = bossColor;
			rend.sprite = bossBox.GetComponent<SpriteRenderer>().sprite;


		}
		rend.color = mainColor;


	}
}