﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionHandler : MonoBehaviour
{
    public delegate void delegateQuestion();
    public delegate void delegateQuestion2(string val);
    public event delegateQuestion2 OnCorrect;
    public event delegateQuestion OnAnswerReceived, OnWrong, OnQuestionSet;

    public InfoTutorial infoTutorial;
    public Question question;

    public AnswerHandler answerHandler;
    public QuestionViewer questionViewer;
    public QuestionViewer questionViewerRevive;

    int errors = 0;

    [Header("Test")]
    public int testExerciseNumber;

    private void Start() {
       
        SetQuestion(GameManager.GetNewQuestion());
    }

    public void SetQuestion(int exerciseNumber) {
        if(!GameManager.posibleRandomQuestions(exerciseNumber)) {
            question = new Question();
        }
        question.exerciseNumber = exerciseNumber;
        question.SetRandomValues();
        SetAnswerType();
        ShowQuestion();
    }

    //public void SetQuestion(int exerciseNumber, int a, int b, int c ,int d) {
    //    question = new Question();
    //    question.exerciseNumber = exerciseNumber;
    //    question.SetSpecificValues(a,b,c,d);
    //    SetAnswerType();
    //    ShowQuestion();
    //}

    public void ShowQuestion() {
        questionViewer.Show(question);
        questionViewerRevive.Show(question);
        if(OnQuestionSet != null) {
            OnQuestionSet();
        }
    }

    public void SetAnswerType() {
        answerHandler.ShowAnswer(question.answer);
    }

    public void ShowTutorial() {

    }

    public Answer MCD(Answer a) {
        Answer simplified = new Answer();
        simplified.answerType = a.answerType;
        simplified.numerator = a.numerator;
        simplified.denominator = a.denominator;
        int divisor = 0;
        int maxValue = a.numerator < a.denominator ? a.numerator : a.denominator;
        for(int i = 1; i <= maxValue; i++) {
            if((a.numerator % i == 0) && (a.denominator % i == 0)) {
                divisor = i;
            }
        }
        simplified.numerator /= divisor;
        simplified.denominator /= divisor;

        return simplified;
    }

    bool CompareFraction(Answer a, Answer b) {
        Debug.Log("Pregunta es: A:" + a.ToString() + " , B:" + b.ToString());
        Answer MCDA = question.exerciseNumber == 7 || question.exerciseNumber == 8 || question.exerciseNumber == 9 ? a : MCD(a);
        Answer MCDB = question.exerciseNumber == 7 || question.exerciseNumber == 8 || question.exerciseNumber == 9 ? b : MCD(b);
        if ((MCDA.numerator == MCDB.numerator) && (MCDA.denominator == MCDB.denominator)) {

            Debug.Log("Correcto: A:" + MCDA.ToString() + " , B:" + MCDA.ToString());
            return true;
        }
        Debug.Log("Falso: A:" + MCDA.ToString() + " , B:" + MCDA.ToString());
        return false;
    }

    public void CheckAnswer(Answer respuesta, string accion) {

        bool sw = false;

        Debug.Log("EntreCheckAnswer");
        switch(respuesta.answerType) {
            case AnswerType.Number:
                Debug.Log("EntreNumber");
            if (question.answer.numerator == respuesta.numerator) {

                    Debug.Log("EntreNumber");
                    sw = true;
                }
            break;
            case AnswerType.Denominator:
                
                    Debug.Log("EntreDenominator");
            if(question.answer.numerator == respuesta.numerator) {

                    Debug.Log("EntreDenominator");
                    sw = true;
            }
            break;
            case AnswerType.Fraction:
            Debug.Log("Entro Fraccion");
            //if((question.answer.numerator == respuesta.numerator) && (question.answer.denominator == respuesta.denominator)) {
            if(CompareFraction(question.answer, respuesta)) {
                    Debug.Log("Entro al Compare");
                sw = true;
            }
            break;
        }

        if(OnAnswerReceived != null) {
            OnAnswerReceived();
        }

        if (sw) {
            if (OnCorrect != null)
            {
                OnCorrect(accion);
            } 
            SetQuestion(GameManager.GetNewQuestion());
        } else {
            if (OnWrong != null)
            {
                OnWrong();
                errors++;
                if(errors >= 3) {
                    errors = 0;
                    FindObjectOfType<TutorialViewer>().TutorialButtonPressed();
                }
            }
        }
    }
}

[System.Serializable]
public class InfoTutorial {
    public string texto;
}

[System.Serializable]
public class Question {

    public int exerciseNumber;
    public int a;
    public int b;
    public int c;
    public int d;
    public int e;
    public int f;
    public int g;
    public Answer answer;

    public void SetRandomValues() {
        if(!GameManager.posibleRandomQuestions(exerciseNumber)) {
            a = Random.Range(2, 10);
            b = Random.Range(2, 10);
            c = Random.Range(2, 10);
            d = Random.Range(2, 10);
        } 
        SetValues();
    }

    public void SetSpecificValues(int i, int j, int k, int l) {
        a = i;
        b = j;
        c = k;
        d = l;
        SetValues();
    }

    void SetValues() {
        answer = new Answer();    
        switch(exerciseNumber) {
            case 0:
            answer.numerator = (int)(a * b);
            answer.answerType = AnswerType.Number;
            break;
            case 1:
            answer.numerator = (int)(a * b);
            answer.answerType = AnswerType.Number;
            break;
            //------ COMBO
            case 2:
            c = (int)(a * b);
            answer.numerator = c;
            answer.answerType = AnswerType.Number;
            break;

            case 3:
            answer.numerator = a;
            answer.answerType = AnswerType.Number;
            break;
            //---------------
            case 4:
            answer.numerator = (int)((a * c));
            answer.denominator = (int)(b);
            answer.answerType = AnswerType.Fraction;
            break;

            case 5:
            answer.numerator = (int)(a * b);
            answer.answerType = AnswerType.Denominator;
            break;

            case 6:
            answer.numerator = (int)(a * b);
            answer.answerType = AnswerType.Denominator;
            break;

            case 7:
            answer.numerator = Random.Range(2,10);
            answer.denominator = answer.numerator;   
            c = (int)(a * answer.numerator);
            d = (int)(b * answer.numerator);
            answer.answerType = AnswerType.Fraction;
            break;

            //-----COMBO
            case 8:
            e = a * d;
            f = c * b;
            g = b * d;
            answer.answerType = AnswerType.Fraction;
            answer.numerator = d;
            answer.denominator = d;
            break;

            case 9:
            answer.numerator = b;
            answer.denominator = b;
            answer.answerType = AnswerType.Fraction;
            break;

            case 10:
            answer.numerator = e + f;
            answer.denominator = g;
            answer.answerType = AnswerType.Fraction;
            break;
            //----------
            case 11:
            answer.numerator = e + f;
            answer.denominator = g;
            //------muestra interfaz
            answer.answerType = AnswerType.Fraction;
            
            break;
            //--------COMBO
            case 12:
            e = a * d;
            f = c * b;
            g = b * d;
            answer.numerator = e;
            answer.denominator = g;
            answer.answerType = AnswerType.Fraction;
            break;

            case 13:
            answer.numerator = f;
            answer.denominator = g;
            answer.answerType = AnswerType.Fraction;
            break;

            case 14:
            answer.numerator = e + f;
            answer.denominator = g;
            answer.answerType = AnswerType.Fraction;
            break;

            case 15:
            answer.numerator = e + f;
            answer.denominator = g;
            answer.answerType = AnswerType.Fraction;
            //------Vacio
            break;
            
            case 16:
           
            answer.numerator = (a * d) + (c * b);
            answer.denominator = b * d ;
            answer.answerType = AnswerType.Fraction;
               
            break;

            case 17:
            if((a * d) < (c * b) ) {
                int temp1 = a;
                int temp2 = d;
                a = c;
                d = b;
                c = temp1;
                b = temp2;
            }
            answer.numerator = (a * d) - (c * b);
            answer.denominator = b * d;
            answer.answerType = AnswerType.Fraction;
                
            break;
            case 18:

            answer.numerator = a * c;
            answer.denominator = b * d;
            answer.answerType = AnswerType.Fraction;

            break;
            case 19:

            answer.numerator = a * d;
            answer.denominator = b * c;
            answer.answerType = AnswerType.Fraction;

            break;
            default:

            break;
        }
        
        
    }
}

