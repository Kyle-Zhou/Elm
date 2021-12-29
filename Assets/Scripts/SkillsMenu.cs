using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillsMenu : MonoBehaviour
{

    public Text skillPointsText;
    public List<GameObject> skillItems;

    public void Awake()
    {
        skillItems[0].GetComponent<SkillsIcon>().SetAvailable(true);
        //skillItems[1].GetComponent<SkillsIcon>().SetAvailable(true);

    }

    private void OnEnable()
    {
        skillPointsText.text = SkillController.instance.GetSkillPoints().ToString();
        foreach(GameObject item in skillItems)
        {
           item.GetComponent<SkillsIcon>().Check();
        }
    }


    public void Unlock(int index)
    {
        if (SkillController.instance.GetSkillPoints() >= 1) {

            skillItems[index].GetComponent<SkillsIcon>().Unlock();
            SoundManager.PlaySound(SoundManager.Sound.skillUnlocked);
            SkillController.instance.AddSkillPoints(-1);
            skillPointsText.text = SkillController.instance.GetSkillPoints().ToString();

            switch (index) {
                case 0:
                    SkillController.instance.UnlockSkillOne();
                    skillItems[2].GetComponent<SkillsIcon>().SetAvailable(true);
                    break;
                case 1:
                    SkillController.instance.UnlockSkillTwo();

                    break;

            }

            foreach (GameObject button in skillItems)
            {
                button.GetComponent<SkillsIcon>().Check();
            }

        }
    }


}
