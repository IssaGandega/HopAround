using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tuto : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tutoTxt;
    [SerializeField] private Image tutoImage;
    [SerializeField] private SpriteRenderer step5Ring;
    [SerializeField] private SpriteRenderer step6Ring;
    [SerializeField] private Sprite[] tutoSprite;
    private void Start()
    {
        Step1();
    }

    public void Step1()
    {
        ShowText("Tilt your phone right or left to move Rebby");
        ShowImage(tutoSprite[3],new Vector2(2,2));

    }

    public void Step2()
    {
        ShowText("Touch the screen to make Rebby jump");
        ShowImage(tutoSprite[2],new Vector2(2,2));

    }

    public void Step3()
    {
        ShowText("maintain touch  to jump longer distances");
        ShowImage(tutoSprite[2],new Vector2(2,2));

    }

    public void Step4()
    {
        ShowText("This is a checkpoint walk near it in order to activate it. When you’ll lose you will respawn here.");
        ShowImage(tutoSprite[3],new Vector2(2,2));
    }

    public void Step5Part1()
    {
        step5Ring.gameObject.SetActive(true);
        ShowText("Touch the hanging ring in order to grip it with your tongue");
        ShowImage(tutoSprite[2],new Vector2(2,2));

    }

    public void Step5Part2()
    {
        step5Ring.gameObject.SetActive(false);
        ShowText("Tilt your phone to choose the direction you want to go to and then touch the screen again to jump out");
        ShowImage(tutoSprite[3],new Vector2(2,2));

    }

    public void Step6Part1()
    {
        ShowText("Use your tongue to grip the ring");
        step6Ring.gameObject.SetActive(true);
        ShowImage(tutoSprite[2],new Vector2(2,2));
    }

    public void Step6Part2()
    {
        ShowText("touch the screen to jump out");
        step6Ring.gameObject.SetActive(false);
        ShowImage(tutoSprite[2],new Vector2(2,2));

    }

    public void Step7()
    {
        ShowText("Walk on top of the button to activate a one time mechanism");
        ShowImage(tutoSprite[3],new Vector2(2,2));

    }

    public void Step8()
    {
        ShowText("Use the tongue to move the lever in order to move endlessly a platform");
        ShowImage(tutoSprite[2],new Vector2(2,2));

    }



    private void ShowText(string txtToShow)
    {
        InstantDisabler();
        tutoTxt.gameObject.SetActive(true);
        tutoTxt.alpha = 0;
        tutoTxt.text = txtToShow;
        tutoTxt.DOFade(1f, 1f);
        StartCoroutine(Disabler());
    }

    private void ShowImage(Sprite sprite,Vector2 scale)
    {
        tutoImage.rectTransform.localScale = scale;
        tutoImage.sprite = sprite;
        tutoImage.gameObject.SetActive(true);
        tutoImage.color = new Color(255, 255, 255, 0);
        tutoImage.DOFade(1, 1f);
        StartCoroutine(Disabler());
    }

    public IEnumerator Disabler()
    {
        yield return new WaitForSeconds(5);
        tutoImage.DOFade(0,1f).OnComplete((() => tutoImage.gameObject.SetActive(false)));
        tutoTxt.DOFade(0f, 1f).OnComplete(() => tutoTxt.gameObject.SetActive(false));
    }

    private void InstantDisabler()
    {
        StopAllCoroutines();
        tutoImage.DOKill();
        tutoTxt.DOKill();
        tutoImage.gameObject.SetActive(false);
        tutoTxt.gameObject.SetActive(false);
    }
}
