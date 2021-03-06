﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    public string scene;

    public Image fadeImage;
    public float fadeSpeed;

    public GameObject LoadingGameObject;

    private void Start()
    {

        StartCoroutine(FadeIn());
    }

    public void ChangeLevel()
    {
        StartCoroutine(LoadBackgroundScene());
    }

    IEnumerator LoadBackgroundScene()
    {
        yield return StartCoroutine(FadeOut());
        yield return null;
        SceneManager.LoadScene(scene);
        
    }

    IEnumerator FadeOut() {
        float fade = 0f;
        while(fade <= 1f) {
            fade += Time.deltaTime * fadeSpeed;
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, fade);
            yield return null;
        }
        LoadingGameObject.SetActive(true);
    }

    IEnumerator FadeIn() {
        LoadingGameObject.SetActive(false);
        float fade = 1f;
        while(fade >= 0f) {
            fade -= Time.deltaTime * fadeSpeed;
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, fade);
            yield return null;
        }
    }
}