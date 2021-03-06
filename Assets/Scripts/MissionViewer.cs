﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using LoLSDK;

public class MissionViewer : MonoBehaviour
{
    public List<string> textos;
    public int currentShowing;
    public TextMeshProUGUI text;

    public void Start() {
        //Show();
    }

    public void SetTexts(List<string> t) {
        textos = new List<string>();
        textos.AddRange(t);
        currentShowing = 0;
        Show();
    }

    public void ButtonPressed() {
        currentShowing++;
        if(currentShowing >= textos.Count) {
            Hide();
        } else {
            Show();
        }
    }

    public void Show() {
        string s = SharedState.LanguageDefs[textos[currentShowing]];
        //Debug.Log(s);
        text.text = s;
        LOLSDK.Instance.SpeakText(textos[currentShowing]);
        //text.text = SharedState.LanguageDefs[textos[currentShowing].ToString()];
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}
