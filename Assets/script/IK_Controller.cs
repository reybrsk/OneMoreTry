using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using RootMotion.FinalIK;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

public class IK_Controller : MonoBehaviour
{

    public Transform leftFootPoint;
    public Transform rightFootPoint;
    public GameObject leftLegCalf;
    public GameObject rightLegCalf;
    
    public Transform pelvis;
    
    
    public Transform leftHandPoint;
    public Transform rightHandPoint;
    public GameObject leftHand;
    public GameObject rightHand;

    public GameObject trigger;

    public bool isIkActive;
    public bool isHandActive = false;
    public bool isHaveImpulce;
    

    private Animator _animator;
    private CharacterController _controller;
    private bool isLeft;
    private float newY;
    public float distance;
    public float impulce = 1f;
    public float fallDistance = 0.7f;
    
    
    
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        newY = transform.position.y;
        _animator = GetComponent<Animator>();
        // _controller = GetComponent<CharacterController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            isLeft = !isLeft;
        }
        
        MakeStep(leftFootPoint,rightFootPoint);
       
        if (isHandActive) MakeStep(rightHandPoint,leftHandPoint);
        
        
        
        if (Vector3.Distance(leftFootPoint.position,rightFootPoint.position) > fallDistance)
        {
            if (isHaveImpulce)
            {
                isHaveImpulce = !isHaveImpulce;
                GetComponent<Ragdoll>().Fall();
                GetComponent<Animator>().enabled = false;
                if (isLeft)
                {
                    leftLegCalf.GetComponent<Rigidbody>().AddForce(Vector3.up * impulce);
                }
                else
                {
                    rightLegCalf.GetComponent<Rigidbody>().AddForce(Vector3.up * impulce);
                }
            }
        }
       
    }

    
    
    
    private void OnAnimatorIK(int layerIndex)
    {
        if (!_animator)
            return;
    
        if (isIkActive)
        {
            
    
            if (!pelvis || !leftFootPoint || !rightFootPoint)
                return;
    
    
            
            
            
            
            _animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
            _animator.SetIKPosition(AvatarIKGoal.LeftFoot, leftFootPoint.position);
           
    
            _animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
            _animator.SetIKPosition(AvatarIKGoal.RightFoot, rightFootPoint.position);
            
            
            if (isHandActive)
            {
                _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                _animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandPoint.position);
           
    
                _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                _animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandPoint.position);
            }
    
    
    
    
    
    
    
    
    
    
    
        }
    
    }

    private void MakeStep(Transform left, Transform right)
    {
       

        if (Input.GetKey(KeyCode.W))
        {
            if (isLeft)
            {
                // Left();
                
                RaycastHit hit;
                RaycastHit[] hits;
                
                Debug.DrawRay(leftFootPoint.position + Vector3.up, Vector3.down);

                hits = Physics.RaycastAll(left.position + Vector3.up, Vector3.down,10f);


                for (int i = 0; i < hits.Length; i++)
                {
                    if (hits[0].transform.name.Equals("box-trigger"))
                    {
                        OnTheFloor();  
                    }
                    
                    if (hits[0].transform.tag.Equals("floor"))
                    {
                        hit = hits[0];
                        left.position = hit.point;
                    } 
                }
                  
                
                
                    
               
                
                
                
                
                
                var position = left.position;
                position = new Vector3(position.x,
                    position.y, position.z + 0.02f);
                left.position = position;
            }
            else
            { 
                
                RaycastHit hit;
                RaycastHit[] hits;
                
                Debug.DrawRay(right.position + Vector3.up, Vector3.down);
                
                hits = Physics.RaycastAll(right.position + Vector3.up, Vector3.down,10f);
                for (int i = 0; i < hits.Length; i++)
                {
                    if (hits[i].collider == trigger)
                    {
                        OnTheFloor();  
                    }
                    
                    if (hits[i].transform.tag.Equals("floor"))
                    {
                        hit = hits[i];
                        right.position = hit.point;
                        
                    }
                  
                }

               
                
                
                
                var position = right.position;
                position = new Vector3(position.x,
                    position.y, position.z + 0.02f);
                right.position = position;
            }



        }


    }

    // private void Left()
    // {
    //     RaycastHit leftHit;
    //     Vector3 lpos = leftFootPoint.position;
    //     if (Physics.Raycast(lpos + Vector3.up * 0.5f, Vector3.down, out leftHit, 1f)) ;
    //     {
    //         
    //     }
    // }

    private void OnTheFloor()
    {
        isHandActive = true;
        
        
        
        
    }
}

