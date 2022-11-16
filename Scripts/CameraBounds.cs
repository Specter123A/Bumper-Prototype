using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    public List<Transform> players;
    public Vector3 offset;

    void LateUpdate()    //because camera has to follow up: after the movement/task is done. 
    {
        if(players.Count ==0)  //if no players, do nothing
        
            return;
        
    //if there are players, it will calculate the center point accprdingly.
        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPosition = centerPoint + offset;
        transform.position = newPosition;
    }

    //method for getting centerpoint
    Vector3 GetCenterPoint()
    {
        if(players.Count ==1)
        {
            return players [0].position;
            
        }

        //Bounds - Class that encapsulates multiple objects. 
            //Simply focuses on the player by creating a box around it
        var bounds = new Bounds(players[0].position, Vector3.zero);
        for (int i=0; i <players.Count; i++)
        {
            bounds.Encapsulate(players[i].position);
        }

        return bounds.center;  //will return the center point and do all calculations
    }
}
