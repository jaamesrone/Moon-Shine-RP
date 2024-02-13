using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyController : MonoBehaviour
{
    private Material SkyBox;
    public float Speed;
    private float tFactor;
    public bool isNight;

    public float DayValue;
    public float NightValue;
    public float changeSpeed;
    private void Start()
    {
        SkyBox = RenderSettings.skybox;

        isNight = false;
        
    }


    private void Update()
    {
        SkyBox.SetFloat("_Rotation", SkyBox.GetFloat("_Rotation") + Time.deltaTime * Speed);


        if (isNight == true && tFactor >= NightValue)
        {
            tFactor -= Time.deltaTime * changeSpeed;

            SkyBox.SetColor("_Tint",new Color(tFactor, tFactor, tFactor, 1));
           
            if (tFactor == NightValue)
            {
                Debug.Log("NightTime");
            }
           

        }

          else if (isNight == false && tFactor <= DayValue)
        {
            tFactor += Time.deltaTime * changeSpeed;

            SkyBox.SetColor("_Tint",new Color(tFactor, tFactor, tFactor, 1));

            if (tFactor == DayValue)
            {
                Debug.Log("DayTime");
            }

        }

        

    }


  



}
