using UnityEngine;
using System.Collections.Generic;

public class CameraSetup : MonoBehaviour
{
    private List<Camera> cameras = new List<Camera>();

    public void RegisterTankCameras(GameObject tank)
    {

        Camera[] tankCameras = tank.GetComponentsInChildren<Camera>(true);

        for (int i = 0; i < tankCameras.Length; i++)
        {
            cameras.Add(tankCameras[i]);
        }
    }

    public void ConfigureCameras(int players)
    {
        if (players == 2 && cameras.Count >= 2)
        {
            cameras[0].rect = new Rect(0f, 0f, 0.5f, 1f);
            cameras[1].rect = new Rect(0.5f, 0f, 0.5f, 1f);
        }
        else if (players == 4 && cameras.Count >= 4)
        {
            cameras[0].rect = new Rect(0f, 0.5f, 0.5f, 0.5f);
            cameras[1].rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
            cameras[2].rect = new Rect(0f, 0f, 0.5f, 0.5f);
            cameras[3].rect = new Rect(0.5f, 0f, 0.5f, 0.5f);
        }
        else
        {
            Debug.LogError("No hay suficientes cámaras registradas. Se necesitan " + players + " jugadores, pero solo hay " + cameras.Count);
        }
    }
}
