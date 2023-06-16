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

    [Header("Change Scene Animation")]
    [SerializeField] float timeToChangeScene = 7.5f;
    [SerializeField] Animation cameraChangeAnimation;
    [SerializeField] Animation canvasChangeAnimation;

    [Header("FMOD")]
    public StudioEventEmitter SelectEmitter;
    public StudioEventEmitter MoveAnimationEmitter;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    public void onStartGame()
    {
        SelectEmitter.Play();
        StartCoroutine(changeScene());
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



    private IEnumerator changeScene()
    {
        if (MoveAnimationEmitter!=null) MoveAnimationEmitter.Play();
        cameraChangeAnimation.Play();
        canvasChangeAnimation.Play();
        yield return new WaitForSeconds(timeToChangeScene);
        SceneManager.LoadScene(1);
    }
}
