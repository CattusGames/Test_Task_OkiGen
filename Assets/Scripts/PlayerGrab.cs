using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
public class PlayerGrab : MonoBehaviour
{
    public GameObject objectToPickUp;
    [Header("Targets")]
    [SerializeField] private Transform _handTarget;
    [SerializeField] private Transform _pickUpTarget;
    [SerializeField] private Transform _pickDownTarget;
    [SerializeField] private Transform _headTarget;
    [SerializeField] private Transform _chestTarget;
    [Header("Grab properties controll")]
    [SerializeField] private float _pickUpDistance;
    [SerializeField] private float _grabOffset;
    [SerializeField] private float _pickDownOffset;
    [SerializeField] private float _grabSpeed;
    [Header("Inverse Kinematic Properties")]
    [SerializeField] private TwoBoneIKConstraint _leftBoneIK;
    [SerializeField] private TwoBoneIKConstraint _rightBoneIK;
    [SerializeField] private MultiAimConstraint _headAim;
    [SerializeField] private MultiAimConstraint _chestAim;
    private Vector3 defaultHeadAimPosition;
    private Vector3 defaultChestAimPosition;
    private Vector3 defaultHandTargetPosition;
    private Animator anim;
    private bool isGrabbed = false;
    private bool isGrabbing = false;
    private bool isGrab = false;

    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        defaultHandTargetPosition = _handTarget.position;
        defaultChestAimPosition = _chestTarget.position;
        defaultHeadAimPosition = _headTarget.position;
    }
    private void Update()
    {
        if (objectToPickUp != null)
        {
            if ((Vector3.Distance(transform.position, objectToPickUp.transform.position) < _pickUpDistance))
            {
                anim.SetBool("Grab", true);
                isGrab = true;
            }

            if (isGrabbing)
            {
                _handTarget.position = Vector3.Lerp(_handTarget.position, _pickDownTarget.position, _grabSpeed * Time.deltaTime);
            }
            if (isGrab)
            {
                _handTarget.position = Vector3.Lerp(_handTarget.position, objectToPickUp.transform.position, _grabSpeed * Time.deltaTime);
                _headTarget.position = Vector3.Lerp(_headTarget.position, objectToPickUp.transform.position, _grabSpeed * Time.deltaTime);
                _chestTarget.position = Vector3.Lerp(_chestTarget.position, objectToPickUp.transform.position, _grabSpeed * Time.deltaTime);
                if (Vector3.Distance(_pickUpTarget.position, objectToPickUp.transform.position) < _grabOffset)
                {
                    anim.SetBool("Grab", false);
                    isGrab = false;
                }
            }

        }
        else
        {
            isGrab = false;
            anim.SetBool("Grab", false);
            isGrabbed = true;
        }
        if (isGrabbed)
        {
            if (Vector3.Distance(defaultHandTargetPosition, _handTarget.position) < _grabOffset / 10)
            {
                _pickUpTarget.DetachChildren();
                _headTarget.position = Vector3.Lerp(_headTarget.position, defaultHeadAimPosition, _grabSpeed * Time.deltaTime);
                _chestTarget.position = Vector3.Lerp(_chestTarget.position, defaultChestAimPosition, _grabSpeed * Time.deltaTime);
                isGrabbed = false;
            }
            else
            {
                _handTarget.position = Vector3.Lerp(_handTarget.position, defaultHandTargetPosition, _grabSpeed * Time.deltaTime);
            }
        }
    }
    private void OnAnimationGrabbedItem()
    {
        if (objectToPickUp != null)
        {
                objectToPickUp.GetComponent<Rigidbody>().isKinematic = true;
                objectToPickUp.transform.position = _pickUpTarget.transform.position;
                objectToPickUp.transform.SetParent(_pickUpTarget, true);
                isGrabbing = true;
        }
    }
    private void OnAnimationStoredItem()
    {

        if (Vector3.Distance(_pickDownTarget.position, _handTarget.position) < _pickDownOffset)
        {
            
            isGrabbing = false;
            objectToPickUp.transform.SetParent(_pickUpTarget, false);
            objectToPickUp.transform.position = _pickDownTarget.transform.position;
            objectToPickUp.transform.SetParent(_pickDownTarget, true);
            _handTarget.position = Vector3.Lerp(_handTarget.position, defaultHandTargetPosition, _grabSpeed * Time.deltaTime);
            isGrabbed = true;
            objectToPickUp.GetComponent<Rigidbody>().isKinematic = false;
            anim.SetBool("Grab", false);
            isGrab = false;
            objectToPickUp = null;

        }
    }
    public void CancelWeight()
    {
        _leftBoneIK.weight = 0;
        _rightBoneIK.weight = 0;
        _headAim.weight = 0;
        _chestAim.weight = 0;
        anim.SetBool("Dance",true);
        _pickDownTarget.DetachChildren();
    }
}
