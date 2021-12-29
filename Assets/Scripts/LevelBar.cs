using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class LevelBar : MonoBehaviour
{
    public static LevelBar instance;

    public GameObject notification;
    public Image mask;
    public Text levelText;
    public Tilemap tilemap;
    public TileBase grass;
    private int level, current, maximum;
    private Vector3Int localPlace;
    private Vector3 place;
    private List<Vector3> existingTiles;

    private void Start()
    {
        instance = this;

        if (level == 0)
        {
            maximum = 100;
        }

        //Scan();
    }

    public void LevelUp(int carryOver)
    {
        notification.GetComponent<NotificationFade>().StartCoroutine("Fade");
        SoundManager.PlaySound(SoundManager.Sound.LevelUp);

        level++;
        levelText.text = level.ToString();
        current = 0 + carryOver;
        maximum += 50;

        SkillController.instance.AddSkillPoints(1);
    }

    public void AddXP(int addedXp)
    {
        current += addedXp;
        if (current >= maximum)
        {
            int carryOver = current - maximum;
            LevelUp(carryOver);
        }
        float fillAmount = (float)current / (float)maximum;
        mask.fillAmount = fillAmount;
    }

    private void Scan()
    {
        existingTiles = new List<Vector3>();

        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            localPlace = new Vector3Int(pos.x, pos.y, pos.z);
            place = tilemap.CellToWorld(localPlace);

            if (tilemap.HasTile(localPlace))
            {
                existingTiles.Add(place);
            }
        }
    }

    public List<Vector3> GetExistingTiles()
    {
        return existingTiles;
    }
}