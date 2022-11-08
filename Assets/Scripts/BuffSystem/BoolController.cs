using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoolController : MonoBehaviour
{
    public float Duration;
    private float Timer;
    private Image mask;
    private float originalSize;
    private float nowSize;
    // Start is called before the first frame update
    public void Activate()
    {
        mask = GetComponent<Image>();
        originalSize = mask.rectTransform.rect.width;
        nowSize = originalSize;
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, nowSize);
        Timer = Duration;
    }

    public void Tick(float detTime)
    {
        Timer -= detTime;
        nowSize = Timer / Duration * originalSize;
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, nowSize);
    }

    public void End()
    {
        Destroy(this.gameObject);
    }

    public void Refresh()
    {
        nowSize = originalSize;
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, nowSize);
        Timer = Duration;
    }

}
