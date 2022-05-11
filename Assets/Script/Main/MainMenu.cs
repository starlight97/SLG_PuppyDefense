using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public AudioSource soundSource;

    public void SetSoundVolume(float volume)
    {
        //audioma
        soundSource.volume = volume;
    }
    public void Exit()
    {
        Application.Quit();
    }




}
