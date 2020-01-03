using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightObj : MonoBehaviour {


    public Material LightOnMat, LightOffMat;
    public List<Light> TheLights = new List<Light>();

    public List<LightObj> ChildLights =new List<LightObj>();


    MeshRenderer TheMesh;
    public void OnClick()
    {
        if (TheLights.Count == 0 && transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).GetComponent<Light>())
                {
                    TheLights.Add(transform.GetChild(i).GetComponent<Light>());
                }
            }
        }

        if (!TheMesh)
        {
            TheMesh = transform.GetComponent<MeshRenderer>();
        }

        /*for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<AreaLight>())
            {
                if (TheMesh.sharedMaterial == LightOnMat)
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                }
                else
                {
                    transform.GetChild(i).gameObject.SetActive(true);
                }
            }
        }*/

        if (LightOnMat && LightOffMat)
        {
            if (TheMesh.sharedMaterial == LightOnMat)
            {
                TheMesh.sharedMaterial = LightOffMat;
                foreach (Light TheLight in TheLights)
                {
                    TheLight.enabled = false;
                }
            }
            else
            {
                TheMesh.sharedMaterial = LightOnMat;
                foreach (Light TheLight in TheLights)
                {
                    TheLight.enabled = true;
                }
            }
        }

        if (ChildLights.Count > 0)
        {
            foreach (LightObj TheLO in ChildLights)
            {
                TheLO.OnClick();
            }
        }
    }



}
