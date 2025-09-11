using System.Collections.Generic;
using UnityEngine;

public class TankRegistry : MonoBehaviour
{
    public static TankRegistry Instance { get; private set; }

    private List<TankMovement> tanks = new List<TankMovement>();


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); 
            return;
        }
    }


    public void RegisterTank(TankMovement tank)
    {
        if (!tanks.Contains(tank))
            tanks.Add(tank);
    }

    public void UnregisterTank(TankMovement tank)
    {
        if (tanks.Contains(tank))
            tanks.Remove(tank);
    }

    public List<TankMovement> GetAllTanks()
    {
        return tanks;
    }
}
