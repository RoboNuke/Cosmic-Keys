using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  [SerializeField]
  float rotationSensitivity;
  [SerializeField]
  GameObject focusPoint;
  [SerializeField][Range(0f, 1f)]
  float moveMousePadding;
  [SerializeField]
  float moveSpeed;
  
  private Transform Obstruction;
  private Stack<GameObject> obstructions = new Stack<GameObject>();
    private GameStateManager gameManager;
  private void Start()
  {
    // Set the camera to the default location
    transform.localPosition = new Vector3(3, 10, -12);
    transform.eulerAngles = new Vector3(45,0,0);
    gameManager = GameObject.FindGameObjectWithTag("GameStateManager").GetComponent<GameStateManager>();
    }
  
  private void Update()
    {
        if (gameManager.canControlCamera)
        {
            Rotate(Input.GetAxis("Horizontal"));
            Move();
        }
  }

  private void Move()
  {
    Vector3 moveDir = GetCameraMoveDirection(Input.mousePosition, moveMousePadding);
    //Debug.Log(moveDir);
    focusPoint.transform.Translate(moveDir * moveSpeed * Time.deltaTime);
  }

  private void Rotate(float rotationInput)
  {
    //Debug.Log(rotationInput);
    focusPoint.transform.RotateAround(transform.position,
                                      Vector3.up,
                                      rotationInput * rotationSensitivity * Time.deltaTime);
  }
  
  private Vector3 GetCameraMoveDirection(Vector3 mousePos, float padding)
  {
    if (mousePos.x < padding * Screen.width)
    {
      return Vector3.left;
    } else if (mousePos.x > (1.0f - padding) * Screen.width)
    {
      return Vector3.right;
    } else if (mousePos.y < padding * Screen.height)
    {
      return Vector3.back;
    } else if (mousePos.y > (1f - padding) * Screen.height)
    {
      return Vector3.forward;
    }
    return Vector3.zero;
  }
  
  private void makeClear(ref GameObject obj)
  {
    obj.GetComponent<MeshRenderer>().shadowCastingMode =
      UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
    Debug.Log("Made Something Clear");
  }
  
  private void makeVisiable(ref GameObject obj)
  {
    obj.GetComponent<MeshRenderer>().shadowCastingMode =
      UnityEngine.Rendering.ShadowCastingMode.On;
    Debug.Log("Made something visable");
  }
}
