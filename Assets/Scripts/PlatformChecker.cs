using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformChecker : MonoBehaviour
{
    public bool IsHazardOnNextPlatform(PlatformManager platform)            //�������� ��������� �� �����������
    {
        if (platform.NextPlatform.IsHazard) return true;
        else return false;
    }
}
