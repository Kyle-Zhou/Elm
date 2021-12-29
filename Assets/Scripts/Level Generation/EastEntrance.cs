using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EastEntrance : Entrance
{
    private void Start()
    {
        ps = Instantiate(fog, transform.position, transform.rotation);
        ps.transform.SetParent(gameObject.transform);
        ps.transform.localPosition = new Vector3(1, 0, 0);
        ps.transform.localRotation = Quaternion.Euler(0, 0, 90);
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
        RoomTransitionAnims.instance.MovingRightAnimStart();
        yield return new WaitForSeconds(0.25f);
        RoomManager.instance.MoveRoomRight();
        RoomTransitionAnims.instance.MovingRightAnimEnd();
    }
}
