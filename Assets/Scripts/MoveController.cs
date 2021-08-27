using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveController : MonoBehaviour
{
    [SerializeField] private List<GameObject> playerGlassObject; //������� ��������� 
    [SerializeField] private PlatformChecker platformCheck;      //������ �� �������� ���� �� ���������� �� ���������
    [SerializeField] private PlatformManager curPlatform;        //������� ��������� �� ������� �����
    [SerializeField] private LevelController levelController;    //������ �� ��������� ������
    [SerializeField] private GameObject cmCamera;                //������ �� ������  ��� �� ���������� ��� ����� ����
    [SerializeField] private OnHazard onHazard;                  //������ �� �������� �������������� ���������� ����������� ������������ � �����������
    [SerializeField] private bool isInJump;                      //���� � ������ ��
    [SerializeField] private bool isReverse = false;             //���� ������� ������� ��� ����������� �� �������� � ������� � ��������
    [SerializeField] private float defaultJumpY = 0.65f;         //������� �������� ������� ���������� �� Y (������ �������� ��� ����������� ���������� ��� ������ �� ������ ��������� ��� �� ����������� � ������ �����������)
    [SerializeField] private float jumpImpulse = 0.7f;           //������� �������� ��� �������� ������ - ��� �������� �������� ������ ��� ������ � ��������� ��� � �� ������� ���� ��� ������ � ���������
    private Sequence flipSeq;

    public List<GameObject> PlayerGlases => playerGlassObject;
    
    private void Awake()
    {
        levelController = transform.GetComponent<LevelController>();
        platformCheck = transform.GetComponent<PlatformChecker>();
        onHazard = transform.GetComponent<OnHazard>();
        curPlatform = levelController.AllPlatforms[0];
        cmCamera = GameObject.Find("CM vcam1").gameObject;
    }
    private void TestNextFlipHaveGlass(bool isDouble)
    {
        if(isDouble)
        {
            if(curPlatform.NextPlatform.NextPlatform.typeHazard == PlatformManager.Hazard.bonusGlass)
            {
                isReverse = true;
                defaultJumpY = 0.7f;
            }
        }
        else
        {
            if (curPlatform.NextPlatform.typeHazard == PlatformManager.Hazard.bonusGlass)
            {
                isReverse = true;
                defaultJumpY = 0.7f;
            }
        }
    }        //���������������� �������� ���� �� ��������� �� ���� ��� � ������ �������� ����� �������� ���
    private void CurFlipHavePike()
    {
        if (curPlatform.typeHazard == PlatformManager.Hazard.pike)
        {
            playerGlassObject.Remove(playerGlassObject[playerGlassObject.Count - 1]);
        }
    }                           //�������� �� ���� �� �� �� ����
    private void NextFlipIsAbyss(bool isDouble)
    {
        if (isDouble)
        {
            if (curPlatform.NextPlatform.NextPlatform.typeHazard == PlatformManager.Hazard.abyss)
            {
                defaultJumpY = -4f;
                jumpImpulse = 5;
                cmCamera.SetActive(false);
            }
        }
        else
        {
            if (curPlatform.NextPlatform.typeHazard == PlatformManager.Hazard.abyss)
            {
                defaultJumpY = -4f;
                jumpImpulse = 4;
                cmCamera.SetActive(false);
            }
        }
    }              //���������������� �������� �� ������� �� �� � �������
    private IEnumerator StartFlip(int flipDist)
    {
        isInJump = true;
        if (isReverse)
        {
            float counterForPos = 0;
            //������ � ����
            for (int counter = playerGlassObject.Count - 1; counter >= 0; counter--)
            {
                flipSeq = DOTween.Sequence();
                playerGlassObject[counter].transform.DOJump(new Vector3(0, defaultJumpY + (counterForPos * 0.05f), Mathf.Abs(playerGlassObject[counter].transform.position.z) + flipDist), jumpImpulse, 1, 1f);
                flipSeq.Append(playerGlassObject[counter].transform.DORotate(new Vector3(360, 0, 0), 0.7f, RotateMode.LocalAxisAdd));
                flipSeq.AppendInterval(0.7f);
                flipSeq.Join(playerGlassObject[counter].transform.DORotate(new Vector3(0, 0, 0), 0.7f, RotateMode.LocalAxisAdd));
                yield return new WaitForSeconds(0.4f);
                counterForPos++;
            }
            isReverse = false;
        }
        else
        {
            //�� ���� � ������
            for (int counter = 0; counter <= playerGlassObject.Count -1; counter++)
            {
                flipSeq = DOTween.Sequence();
                playerGlassObject[counter].transform.DOJump(new Vector3(0, defaultJumpY + (counter * 0.05f), Mathf.Abs(playerGlassObject[counter].transform.position.z) + flipDist), jumpImpulse, 1, 1f);
                flipSeq.Append(playerGlassObject[counter].transform.DORotate(new Vector3(360, 0, 0), 0.7f, RotateMode.LocalAxisAdd));
                flipSeq.AppendInterval(0.7f);
                flipSeq.Join(playerGlassObject[counter].transform.DORotate(new Vector3(0, 0, 0), 0.7f, RotateMode.LocalAxisAdd));
                yield return new WaitForSeconds(0.4f);
            }
            isReverse = true;

        }
        CurFlipHavePike();
        defaultJumpY = 0.65f;
        StartCoroutine("EndOfJump");
        
    }              //���������� ����� � ��������� ��� ���������������� ������� �����������
    private IEnumerator EndOfJump()
    {
        yield return new WaitForSeconds(0.6f);
        isInJump = false;
        if (levelController.AllPlatforms[levelController.AllPlatforms.Count / 2] == curPlatform ||
            levelController.AllPlatforms[levelController.AllPlatforms.Count / 2] == curPlatform.NextPlatform ||
            levelController.AllPlatforms[levelController.AllPlatforms.Count / 2] == curPlatform.PrevPlatform)
            levelController.GenMorePlatforms();
    }                          //������ ������� ��� ���������� ������ ��� �� ��������� ������
    
    public void Flip(float flipDist)
    {
        StartCoroutine("StartFlip", flipDist);
    }                         //����� ������ ������
    public void Tap(float dist)
    {
        if (isInJump == false)
        {
            if (dist == 2)
            {
                if (platformCheck.IsHazardOnNextPlatform(curPlatform.NextPlatform)) NextFlipIsAbyss(true);
                TestNextFlipHaveGlass(true);
                Flip(dist);
                if (platformCheck.IsHazardOnNextPlatform(curPlatform.NextPlatform))
                {
                    onHazard.JumpOnHazard(curPlatform.NextPlatform.NextPlatform);
                    if (curPlatform.NextPlatform.NextPlatform.typeHazard == PlatformManager.Hazard.bonusGlass)
                    {
                        PlayerGlases.Add(curPlatform.NextPlatform.NextPlatform.transform.GetChild(1).gameObject);
                    }
                }
                curPlatform = curPlatform.NextPlatform.NextPlatform;
            }
            else if (dist == 1)
            {
                if (platformCheck.IsHazardOnNextPlatform(curPlatform)) NextFlipIsAbyss(false);
                TestNextFlipHaveGlass(false);
                Flip(dist);
                if (platformCheck.IsHazardOnNextPlatform(curPlatform))
                {
                    onHazard.JumpOnHazard(curPlatform.NextPlatform);
                    if (curPlatform.NextPlatform.typeHazard == PlatformManager.Hazard.bonusGlass)
                    {
                        PlayerGlases.Add(curPlatform.NextPlatform.transform.GetChild(1).gameObject);
                    }
                }
                curPlatform = curPlatform.NextPlatform;
            }
        }
    }                              //�������� ���������� � ���������� - �������� �����
}
    


