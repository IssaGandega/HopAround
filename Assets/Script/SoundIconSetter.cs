using System;
using UnityEngine;
using UnityEngine.UI;

public class SoundIconSetter : MonoBehaviour
{

    [SerializeField] private SoundManager sM;
    [SerializeField] private Sprite spriteOn,SpriteOff;
    [SerializeField] private Image img;

    private void OnEnable()
    {
        TogleSprite();
    }

    public void TogleSprite()
    {
        if (sM.musicSource.mute == true)
        {
            img.sprite = SpriteOff;
        }
        else
        {
            img.sprite = spriteOn;

        }
    }
}
