using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camp : MonoBehaviour
{
    private FSM manager;
    private Parameter parameter;
    private void Start()
    {
        manager = transform.parent.GetComponentInChildren<FSM>();
        parameter = manager.parameter;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            print(1);
            parameter.target = collision.transform;
            parameter.currentVigilValue = parameter.VigilanceValue;
        }

    }

}
