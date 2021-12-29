using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NorthEntrance : Entrance
{
    private void Start()
    {
        ps = Instantiate(fog, transform.position, transform.rotation);
        ps.transform.SetParent(gameObject.transform);
        ps.transform.localPosition = new Vector3(0, 1, 0);
        ps.transform.localRotation = Quaternion.Euler(-180, 0, 0);
        prompt = GameObject.FindGameObjectWithTag("Prompt");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !active)
        {
            StartCoroutine(LoadAnim());
        }
        else if (collision.gameObject.CompareTag("Player") && active)
        {
            prompt.GetComponent<Text>().enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            prompt.GetComponent<Text>().enabled = false;
        }
    }

    IEnumerator LoadAnim()
    {
        RoomTransitionAnims.instance.MovingUpAnimStart();
        yield return new WaitForSeconds(0.25f);
        RoomManager.instance.MoveRoomUp();
        RoomTransitionAnims.instance.MovingUpAnimEnd();
    }

}
