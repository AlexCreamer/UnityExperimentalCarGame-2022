using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    //MeshRenderer renderer;
    Rigidbody rigidBody;
   [SerializeField] float timeToWait = 5f;
    Renderer rend;

    public void hideDropper() 
    {
        Renderer[] renderChildren = GetComponentsInChildren<Renderer>();
        int i = 0;
        for (i=0; i < renderChildren.Length; ++i)
        {
            renderChildren[i].enabled = false;
        }
    }

    public void showDropper()
    {
        Renderer[] renderChildren = GetComponentsInChildren<Renderer>();
        int i = 0;
        for (i=0; i < renderChildren.Length; ++i)
        {
            renderChildren[i].enabled = true;
        }
    }


    void Start()
    {
        //renderer = GetComponent<MeshRenderer>();
        
        rigidBody = GetComponent<Rigidbody>();
        /*
        rend =  GetComponent<Renderer>();
        //renderer.enabled = false;
        rigidBody.useGravity = false;

        rend.enabled = false;   
        */
    
        //hideDropper(rend);
        //ourDropper.SetActive(false);

        hideDropper();
        rigidBody.useGravity = false;

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Time.time);
        if(Time.time > timeToWait)
        {
            ////renderer.enabled = true;
//            Debug.Log("Hello");

            showDropper();

            //showDropper(rend); 
            rigidBody.useGravity = true;
        }
        

        //Debug.Log("Time elapse: " + Time.time);
    }
}
