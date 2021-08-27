using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private int glassCount;                    // кол-во стаканчиков
    [SerializeField] private GameObject gameOverScreen;         //экран конца игры
    [SerializeField] private AnimationGameOver animGameOver;    //ссылка на анимацию конца игры
    [SerializeField] private GameObject startScreen;            //стратовый экран
    [SerializeField] private AnimationGameOver animStart;       //ссылка на анимацию стартового экрана

    private void Awake()
    {
        gameOverScreen = GameObject.Find("GameOver").gameObject;
        animGameOver = gameOverScreen.transform.GetComponent<AnimationGameOver>();
        gameOverScreen.SetActive(false);
        startScreen = GameObject.Find("StartScreen").gameObject;
        animStart = startScreen.transform.GetComponent<AnimationGameOver>();
    }
    //ќтсчет времени до  онец игры
    private IEnumerator End()
    {
        yield return new WaitForSeconds(1);
        gameOverScreen.SetActive(true);
        animGameOver.FadeIn();
    }
    
    public void Start()
    {
        GameStart();
    }
    public int GlassCount => glassCount;
    public void GameStart()
    {
        animStart.FadeOut();
        glassCount = 1;
    }                                 //€рлык начала игры
    public void GameOver()
    {
        StartCoroutine("End");
    }                                  //€рлык конца игры
    public int GetGlassCount()
    {
        return glassCount;
    } 
    public void GlassCountManage(int count, bool isPlus)
    {
        if(isPlus == false)
        {
            if (glassCount - count > 0)
            {
                glassCount -= count;
            }
            else GameOver();
        }
        else
        {
            glassCount += count;
        }
        
    }    // управление кол-вом стаканчиков тру плюс фолс минус
    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }                               //перезапуск игры при нажатие рестарт в геймовере
}
