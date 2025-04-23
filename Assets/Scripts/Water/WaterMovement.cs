using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class WaterMovement : MonoBehaviour
{
    [SerializeField] private int shipState = 0;
    private float waterSpeed;

    [SerializeField] private float slowWater = 1f;
    [SerializeField] private float mediumWater = 2f;
    [SerializeField] private float fastWater = 4f;
    [SerializeField] private float extremeWater = 10f;

    void Update()
    {
        MoveShip();
    }

    void MoveShip()
    {
        var displace = (ShipDamageState)shipState;
        switch (displace)
        {
            case ShipDamageState.GamePaused:
                waterSpeed = 0f;
                break;
            case ShipDamageState.FactoryNew:
                waterSpeed = slowWater;
                break;
            case ShipDamageState.MinimalWear:
                waterSpeed = mediumWater;
                break;
            case ShipDamageState.FieldTested:
                waterSpeed = fastWater;
                break;
            case ShipDamageState.BattleScarred:
                waterSpeed = extremeWater;
                break;    
        }

        transform.position += Time.deltaTime * waterSpeed * Vector3.up;
    }

    private enum ShipDamageState
    {
        GamePaused,
        FactoryNew,
        MinimalWear,
        FieldTested,
        BattleScarred
    }
}
