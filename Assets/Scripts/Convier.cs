using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Convier : MonoBehaviour
{
    [SerializeField] private Transform _endPoint;
    [Range(0,5)][SerializeField] private float _speed;

    private void OnTriggerStay(Collider other)
    {
        other.transform.position = Vector3.MoveTowards(other.transform.position, _endPoint.position, _speed * Time.deltaTime);
    }
}
