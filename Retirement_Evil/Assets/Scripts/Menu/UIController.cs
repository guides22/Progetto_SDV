using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIController : MonoBehaviour
{

    public Slider musicSlider;

    public void ToggleMusic() {
        AudioManager.Instance.ToggleMusic();
    }

    public void MusicVolume() {
        AudioManager.Instance.Volume(musicSlider.value);
    }
}
