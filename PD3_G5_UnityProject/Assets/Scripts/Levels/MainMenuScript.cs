using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] GameObject options;
    [SerializeField] GameObject volumeOptions;
    [SerializeField] GameObject displayOptions;
    [SerializeField] GameObject ControlsOptions;

    [Header("FMOD")]
    public StudioEventEmitter SelectEmitter;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    public void onStartGame()
    {
        SelectEmitter.Play();
        SceneManager.LoadScene(1);
    }

    public void onSettings()
    {
        if(options.activeSelf == false)
            options.SetActive(true);
        else
            options.SetActive(false);
        if (volumeOptions.activeSelf == true)
            volumeOptions.SetActive(false);
        if (displayOptions.activeSelf == true)
            displayOptions.SetActive(false);
        if (ControlsOptions.activeSelf == true)
            ControlsOptions.SetActive(false);
        SelectEmitter.Play();
    }


    public void onVolumeSettings()
    {
        options.SetActive(false);
        volumeOptions.SetActive(true);
        SelectEmitter.Play();

    }

    public void onDisplaySettings()
    {
        options.SetActive(false);
        displayOptions.SetActive(true);
        SelectEmitter.Play();

    }

    public void onControllerSettings()
    {
        options.SetActive(false);
        ControlsOptions.SetActive(true);
        SelectEmitter.Play();

    }


    public void onQuit()
    {
        Application.Quit();
    }




}
