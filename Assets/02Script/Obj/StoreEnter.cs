using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreEnter : MonoBehaviour
{
    [SerializeField] private ButtonClickUI clickUI;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
            collider.transform.position += new Vector3(1, -0.2f);
            collider.GetComponent<Player>().ChangeState(PlayerState.Idle);

            clickUI.Store(); 
        }
    }
}
