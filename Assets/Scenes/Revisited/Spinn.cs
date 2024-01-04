using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// tt:  handles sprites rotation for spinning effect 2D 
/// </summary>
public class Spinn : MonoBehaviour
{
    [Header("Spinn Configurations")]
    [SerializeField] private float rotationZSpeed; 
    private Transform objTransform;

    // xtras
//private float rotationXSpeed;
//private float rotationYSpeed;


    void Start()
    {
        objTransform = GetComponent<Transform>();
    }


    void Update()
    {
        Spinning(); 
        // future : we can call InvokeR on spinning start for length of intro  cutscene and stop dramatic effect 
    }

    /// <summary>
    /// tt: rotates obj transform component on Z at allocated speeds through Tdt
    /// </summary>
    private void Spinning()
    {
        objTransform.transform.Rotate(new Vector3(0, 0, rotationZSpeed) * Time.deltaTime); 
    }
}
