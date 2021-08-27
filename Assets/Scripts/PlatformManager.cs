using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Класс управление каждой платформочкой
public class PlatformManager : MonoBehaviour
{
    [SerializeField] private bool isHazard;                     //флаг имею ли препятсвия
    [SerializeField] private PlatformManager nextPlatform;      //ссылка на след платформу
    [SerializeField] private PlatformManager prevPlatform;      //ссылка на предидущую платформу
    [SerializeField] private int id;                            //айдишник (пока так и не пригодился)
    [SerializeField] private GameObject pike;                   //обьект пики для отображения
    [SerializeField] private GameObject glass;                  //обьект стаканчика для спавна
    [SerializeField] private List<Material> glassMaterials;     //ряд материалов для рандомизации цветов спавна стаканчиков

    private void Awake()
    {
        pike.SetActive(false);
    }
    private void SetPike(bool isActive)
    {
        pike.SetActive(isActive);
    }                      //Инициализируем пику
    private void SetAbyss(bool isActive)
    {
        if(isActive)
        {
            transform.localScale = new Vector3(0,0,0);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }                     //Иниц. пропасть
    private void SetGlass()
    {
        GameObject _glass = Instantiate(glass, transform);
        _glass.transform.position = new Vector3(0,0.65f,transform.position.z);
        _glass.GetComponent<MeshRenderer>().material = glassMaterials[Random.Range(0, glassMaterials.Count - 1)];
    }                                  //Иницю стаканчик

    public enum Hazard { abyss, pike, bonusGlass };             //флаги возможных препятсвий
    [SerializeField] public Hazard typeHazard = Hazard.abyss;
    public PlatformManager NextPlatform
    {
        get { return nextPlatform; }
        set { nextPlatform = value; }
    }
    public PlatformManager PrevPlatform
    {
        get { return prevPlatform; }
        set { prevPlatform = value; }
    }
    public int Id
    { 
        get { return id; }
        set { id = value; }
    }
    public bool IsHazard => isHazard;
    public void Init(bool hazard, int hazardType)
    {
        if(hazard)
        {
            isHazard = hazard;
            switch (hazardType)
            {
                case 1:
                    //abyss
                    SetAbyss(true);
                    typeHazard = Hazard.abyss;
                    break;
                case 2:
                    //pike
                    SetPike(isHazard);
                    typeHazard = Hazard.pike;
                    break;
                case 3:
                    //Glass
                    typeHazard = Hazard.bonusGlass;
                    SetGlass();
                    break;
            }
        }
    }           // Иниц. при создании
    
}
