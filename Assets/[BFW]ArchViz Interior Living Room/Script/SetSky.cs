using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetSky : MonoBehaviour {

    public List<Material> TheSkys;
    public Text TheAngleText;

    public float RotSpeed = 10;

    //to change the skybox
    public void ChangeSky (int TheNum) {
        RenderSettings.skybox = TheSkys[TheNum];
	}
        
    //to set sky angle
    public void SetAngle (float TheAngle) {
        RenderSettings.skybox.SetFloat("_Rotation", TheAngle * 360);
        TheAngleText.text = "Sky Angle: " + (int)(TheAngle * 360);
       
    }

    float NowRot = 0;
    void Update () {
        //Use key to change skybox
        if (Input.GetKeyDown(KeyCode.F1))
        {
            ChangeSky(0);
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            ChangeSky(1);
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            ChangeSky(2);
        }

        //Use key to rotate the sky
        if (Input.GetKey(KeyCode.R))
        {
            NowRot = RenderSettings.skybox.GetFloat("_Rotation");
            NowRot += Time.deltaTime * RotSpeed;
            while (NowRot > 360)
            {
                NowRot -= 360;
            }
            RenderSettings.skybox.SetFloat("_Rotation", NowRot);
            TheAngleText.text = "Sky Angle: " + (int)(NowRot);
        }
           
    }






}
