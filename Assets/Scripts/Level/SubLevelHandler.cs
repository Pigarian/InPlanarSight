using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubLevelHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] subLevels;
    [SerializeField] private float subLevelBuffer = 3f;
    [SerializeField] private GameObject floor;
    [SerializeField] private bool tutorial = false;
    [SerializeField] private bool levelTwo = false;
    [SerializeField] private bool levelThree;
    [SerializeField] private AudioClip[] subSounds;
    [SerializeField] private AudioSource subSound;
    [SerializeField] private TutorialSounds audioHandle;
    [SerializeField] private Text instructionFade;
    [SerializeField] private GameObject timer;
    private int curLevel;
    private Player player;

    private void Start()
    {
        if (tutorial) StartCoroutine(HandleTutorial());
        else if (levelTwo && !FindObjectOfType<SceneManager>().beenTwo) StartCoroutine(LevelTwo());
        else if (levelThree && !FindObjectOfType<SceneManager>().beenThree) StartCoroutine(LevelThree());
        else
        {
            curLevel = FindObjectOfType<SceneManager>().curSub;
            subLevels[curLevel].SetActive(true);
            player = FindObjectOfType<Player>();
        }
    }

    public void Next()
    {
        subLevels[curLevel].SetActive(false);
        FindObjectOfType<SceneManager>().curSub++;
        curLevel++;
        if (curLevel == 2 && tutorial)
        {
            audioHandle.PlayClip(1);
            StartCoroutine(MidTutorial());
            return;
        }
        player.subLevelJump = false;
        if (curLevel >= subLevels.Length)
        {
            if (tutorial) StartCoroutine(EndTutorial());
            else StartCoroutine(EndLevel());
        }
        else StartCoroutine(NextSub());
    }

    private IEnumerator NextSub()
    {
        subSound.clip = subSounds[1];
        subSound.PlayDelayed(0);
        yield return new WaitForSeconds(subLevelBuffer);
        while (player.transform.position.y > -4)
        {
            yield return new WaitForSeconds(subLevelBuffer);
        }
        subSound.clip = subSounds[0];
        subSound.PlayDelayed(0);
        subLevels[curLevel].SetActive(true);
        player.subLevelJump = true;
        player.canJump = true;
    }

    IEnumerator HandleTutorial()
    {
        audioHandle.PlayClip(0);
        yield return new WaitForSeconds(12f);
        subSound.clip = subSounds[0];
        subSound.PlayDelayed(0);
        curLevel = 0;
        subLevels[0].SetActive(true);
        player = FindObjectOfType<Player>();
    }

    IEnumerator LevelTwo()
    {
        subSound.clip = subSounds[3];
        subSound.volume = .4f;
        AudioSource musicVolume = FindObjectOfType<Timer>().GetComponent<AudioSource>();
        musicVolume.volume = .1f;
        subSound.PlayDelayed(0);
        yield return new WaitForSeconds(8.5f);
        curLevel = 0;
        subLevels[0].SetActive(true);
        player = FindObjectOfType<Player>();
        FindObjectOfType<SceneManager>().beenTwo = true;
        yield return new WaitForSeconds(8.5f);
        subSound.volume = 1;
        musicVolume.volume = .8f;
    }

    IEnumerator LevelThree()
    {
        subSound.clip = subSounds[3];
        subSound.volume = .4f;
        AudioSource musicVolume = FindObjectOfType<Timer>().GetComponent<AudioSource>();
        subSound.PlayDelayed(0);
        musicVolume.volume = Single.Epsilon;
        musicVolume.Stop();
        yield return new WaitForSeconds(5.4f);
        musicVolume.clip = subSounds[4];
        musicVolume.Play();
        subSound.volume = 1;
        for (int i = 0; i < 8; i++)
        {
            musicVolume.volume += .1f;
            yield return new WaitForSeconds(.3f);
        }
        curLevel = 0;
        subLevels[0].SetActive(true);
        subSound.clip = subSounds[0];
        subSound.PlayDelayed(0);
        player = FindObjectOfType<Player>();
        FindObjectOfType<SceneManager>().beenThree = true;
    }

    IEnumerator MidTutorial()
    {
        subSound.clip = subSounds[1];
        subSound.PlayDelayed(0);
        yield return new WaitForSeconds(7);
        subSound.clip = subSounds[0];
        subSound.PlayDelayed(0);
        subLevels[curLevel].SetActive(true);
        player.subLevelJump = true;
        player.canJump = true;
        for (float i = 0; i < 100; i++)
        {
            instructionFade.color = new Color(instructionFade.color.r, instructionFade.color.g, instructionFade.color.b, i / 100);
            yield return new WaitForSeconds(.04f);
        }
        for (float i = 100; i >= 0; i--)
        {
            instructionFade.color = new Color(instructionFade.color.r, instructionFade.color.g, instructionFade.color.b, i / 100);
            yield return new WaitForSeconds(.04f);
        }
    }

    IEnumerator EndTutorial()
    {
        subSound.clip = subSounds[1];
        subSound.PlayDelayed(0);
        audioHandle.PlayClip(2);
        yield return new WaitForSeconds(5.5f);
        StartCoroutine(EndLevel());
    }

    IEnumerator EndLevel()
    {
        FindObjectOfType<SceneManager>().curSub = 0;
        subSound.clip = subSounds[2];
        subSound.PlayDelayed(0);
        yield return new WaitForSeconds(.4f);
        if (tutorial) Instantiate(timer);
        floor.SetActive(false);
    }
}
