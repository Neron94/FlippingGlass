using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private int glassCount;                    // ���-�� �����������
    [SerializeField] private GameObject gameOverScreen;         //����� ����� ����
    [SerializeField] private AnimationGameOver animGameOver;    //������ �� �������� ����� ����
    [SerializeField] private GameObject startScreen;            //��������� �����
    [SerializeField] private AnimationGameOver animStart;       //������ �� �������� ���������� ������

    private void Awake()
    {
        gameOverScreen = GameObject.Find("GameOver").gameObject;
        animGameOver = gameOverScreen.transform.GetComponent<AnimationGameOver>();
        gameOverScreen.SetActive(false);
        startScreen = GameObject.Find("StartScreen").gameObject;
        animStart = startScreen.transform.GetComponent<AnimationGameOver>();
    }
    //������ ������� �� ����� ����
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
    }                                 //����� ������ ����
    public void GameOver()
    {
        StartCoroutine("End");
    }                                  //����� ����� ����
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
        
    }    // ���������� ���-��� ����������� ��� ���� ���� �����
    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }                               //���������� ���� ��� ������� ������� � ���������
}
