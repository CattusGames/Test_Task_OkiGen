using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    [SerializeField] private PlayerGrab _player;
    [HideInInspector] public bool isChoosen;
    [HideInInspector] public bool hasEntered = false;
    private MeshRenderer mr;
    private void Start()
    {
        mr = GetComponent<MeshRenderer>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Dissapear")
        {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Basket")
        {
            mr.material.DisableKeyword("_EMISSION");
        }
    }
    private void Update()
    {
        Emission();
    }
    private void Emission()
    {
        if (isChoosen)
        {
            mr.material.EnableKeyword("_EMISSION");
        }
        else
        {
            mr.material.DisableKeyword("_EMISSION");
        }
    }
    private void OnMouseUp()
    {
        if (_player.objectToPickUp == null && isChoosen == false)
        {
            isChoosen = true;
            _player.objectToPickUp = gameObject;
        }
        else if(_player.objectToPickUp != null)
        {
            if (_player.objectToPickUp = gameObject)
            {
                _player.objectToPickUp = null;
            }
            isChoosen = false;
        } 
    }
}
