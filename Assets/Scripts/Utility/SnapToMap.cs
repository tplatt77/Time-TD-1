﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TD.Maps;
using UnityEditor;

/// <summary>
/// Component that stores a TD Map and contains behavior to move the transform to a valid coordinate on that Map
/// </summary>
[ExecuteInEditMode]
public class SnapToMap : MonoBehaviour
{
    [SerializeField]
    Map targetMap;
    Vector3 lastPosition;

    bool invalidPosition = false;

    /// <summary>
    /// Moves this transform to the nearest valid coordinate on the TD Map
    /// </summary>
    public void SnapToGrid()
    {
        targetMap = FindObjectOfType<Map>();

        if(!targetMap.GridGenerated)
        {
            invalidPosition = true;
            Debug.LogWarning("Either no map is in the scene or the map needs to be regenerated");
            return;
        }

        if (transform.position != lastPosition)
        {
            OccupyCell();
        }
        
    }

    public void OccupyCell()
    {
        targetMap = FindObjectOfType<Map>();

        Cell targetCell = targetMap.GetCellAtPosition(transform.position);
        Cell lastCell = targetMap.GetCellAtPosition(lastPosition);
        if (targetCell.IsOccupied && targetCell.occupant != gameObject)
        {
            invalidPosition = true;
            return;
        }

        invalidPosition = false;
        transform.position = targetMap.GetClosestCoordinatePosition(transform.position);
        lastPosition = transform.position;
        if (gameObject.tag == "Environment")
        {
            lastCell.occupant = null;
            targetCell.occupant = gameObject;
        }
    }

    private void OnDrawGizmos()
    {
        if(invalidPosition)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, new Vector3(targetMap.CellSize, targetMap.CellSize, targetMap.CellSize));
        }
    }
}


