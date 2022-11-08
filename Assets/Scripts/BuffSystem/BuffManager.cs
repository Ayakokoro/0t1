using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuffManager : MonoBehaviour
{
    public List<TimedBuff> CurrentBuffs = new List<TimedBuff>();
    public Dictionary<BuffType,TimedBuff> ExistBuffs = new Dictionary<BuffType,TimedBuff>();
    public GameObject buffUI;
   // public ScriptableBuff testBuff1;
         public GameObject CanvasPos;

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.A))
        {
            testBuff1.InstantiateBuff(this.gameObject);
            TimedPotionedBuff testBuff = new TimedPotionedBuff(testBuff1.Duration, testBuff1, this.gameObject);
            AddBuff(testBuff);
        }*/
        foreach (TimedBuff buff in CurrentBuffs.ToArray())
        {
            buff.Tick(Time.deltaTime);
            if (buff.IsFinished)
            {
                CurrentBuffs.Remove(buff);
                ExistBuffs.Remove(buff.BuffType);
            }
        }
    }

    public void AddBuff(TimedBuff buff)
    {
        if (!ExistBuffs.ContainsKey(buff.BuffType))
        {
            CurrentBuffs.Add(buff);
            ExistBuffs.Add(buff.BuffType, buff);
            buff.Activate(GenBuffUI(buff));
        }
        else
        {
            ExistBuffs[buff.BuffType].Refresh();
        }
    }

    private BoolController GenBuffUI(TimedBuff buff)
    {
        GameObject newBuffUI = Instantiate(buffUI);
        newBuffUI.transform.SetParent(CanvasPos.transform);
        RectTransform rectTransform = newBuffUI.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(20, -40);
        BoolController boolController = newBuffUI.GetComponent<BoolController>();

        Image imageReplace = newBuffUI.transform.GetChild(0).GetChild(0).GetComponent<Image>();
        imageReplace.sprite = buff.icon;

        return boolController;
    }

}
