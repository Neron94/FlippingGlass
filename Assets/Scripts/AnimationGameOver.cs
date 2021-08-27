using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class AnimationGameOver : MonoBehaviour
{
    [SerializeField] private Image myImage;
    [SerializeField] private float duration;
    [SerializeField] private Ease seqEase;
    [SerializeField] private Sequence fadeSequence;
    private TweenCallback myCall;

    private void Start()
    {
        myCall += SetOff;
    }
    //��������� ��������
    public void FadeIn()
    {
        Fade(1);
    }
    //������������ ��������
    public void FadeOut()
    {
        fadeSequence = DOTween.Sequence();
        fadeSequence.Append(myImage.DOFade(0, duration).SetEase(seqEase).OnComplete(myCall));
    }
    //������� ����� �������� ��������
    public void Fade(float value)
    {
        fadeSequence = DOTween.Sequence();
        fadeSequence.Append(myImage.DOFade(value, duration).SetEase(seqEase));
        fadeSequence.AppendInterval(1);
    }
    //��� ���������� ������� ������ ������ �����
    public void SetOff()
    {
        gameObject.SetActive(false);
    }
}
