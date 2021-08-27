using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformChecker : MonoBehaviour
{
    public bool IsHazardOnNextPlatform(PlatformManager platform)            //Проверка платформы на препятствия
    {
        if (platform.NextPlatform.IsHazard) return true;
        else return false;
    }
}
