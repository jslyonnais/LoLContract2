﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AnswerHandler : MonoBehaviour
{
    [Header("UI")]
    public InputField incognita1;
    public InputField incognita2;
    public GameObject line;
    public QuestionHandler question;
    string defaultValue = "?";
    public AnswerType type;

    public void SetAnswerType(AnswerType answerType)
    {
        type = answerType;
        ResetValues();
        DisplayAnswerFields();
    }

    public void DisplayAnswerFields()
    {
        switch (type)
        {
            case AnswerType.Denominator:
                incognita1.enabled = false;
                break;
            case AnswerType.Number:
                incognita2.gameObject.SetActive(false);
                line.SetActive(false);
                break;
        }
    }

    Answer AnswerValue()
    {
        Answer respuesta = new Answer();
        float.TryParse(incognita1.text, out respuesta.numerator);
        float.TryParse(incognita2.text, out respuesta.denominator);

        return respuesta;
    }

    // Llamada por el boton
    public void CheckAnswer()
    {
        question.CheckAnswer(AnswerValue());
    }

    public void ResetValues()
    {
        incognita1.gameObject.SetActive(true);
        incognita1.enabled = true;
        incognita1.text = defaultValue;

        incognita2.gameObject.SetActive(true);
        incognita2.enabled = true;
        incognita2.text = defaultValue;
    }
}

public enum AnswerType { Denominator, Fraction, Number }

[System.Serializable]
public class Answer
{
    public float numerator;
    public float denominator;
}