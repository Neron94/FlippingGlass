using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private List<PlatformManager> allPlatforms; //������ ���� �������� �� ������
    [SerializeField] private List<GameObject> platformPool;      //��� �� ���� �������� ��������
    [SerializeField] private int levelPlatfCount = 16;           //���� ��������� �� ���
    
    private void RandomPlatform(PlatformManager platToInit)
    {
        if(platToInit.PrevPlatform.IsHazard == false) //���� �� ����� ��������� ��� ���� ���������� �� �� ���������  ����� �� �����
        {
            if (Random.Range(1, 100) > 20) //���� ���� ��� ����� ���������� 80%
            {
                platToInit.Init(true, Random.Range(1, 4));
            }
            else
            {
                platToInit.Init(false, Random.Range(1, 4));
            }
        }
    }   //�������������� ��������� ����������
    public void Start()
    {
        GenerateLvel();
    }
    public void GenerateLvel()
    {
        for (int i = 0; i < levelPlatfCount; i++)
        {
            GameObject newPlatform = Instantiate(platformPool[Mathf.Abs(i%2)], allPlatforms[0].transform.parent);
            PlatformManager platMan = newPlatform.transform.GetComponent<PlatformManager>();
            allPlatforms.Add(platMan);
            platMan.PrevPlatform = allPlatforms[allPlatforms.Count - 2];
            platMan.PrevPlatform.NextPlatform = platMan;
            platMan.Id = allPlatforms.Count;
            newPlatform.transform.position = new Vector3(0, 0, platMan.PrevPlatform.transform.position.z + 1);
            platMan.NextPlatform = null;
            RandomPlatform(platMan);
        }
            
        
    }                               //���������� ����� ��������� �� ������� ���������
    public void GenMorePlatforms()
    {
        allPlatforms[allPlatforms.Count - 1].NextPlatform = allPlatforms[1];
        allPlatforms[1].transform.position = new Vector3(0,0, allPlatforms.Count+1 +0.5f);
        allPlatforms[1].NextPlatform = allPlatforms[0];
        allPlatforms[1].PrevPlatform = allPlatforms[allPlatforms.Count - 1];
        allPlatforms[0].transform.position = new Vector3(0, 0, allPlatforms.Count + 2 + 0.5f);
        allPlatforms[0].PrevPlatform = allPlatforms[1];
        GenerateLvel();
    }                           //��� ���������� �������� ������ ����������� �������� ��� ��������� � ����� ��� ��������� ����� �� ���
    public List<PlatformManager> AllPlatforms => allPlatforms;
}
