using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{

  [SerializeField]
  Camera cam;

  private UnityEngine.AI.NavMeshAgent navAgent;
    private GameStateManager gameManager;
  
    // Start is called before the first frame update
    void Start()
    {
      navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        gameManager = GameObject.FindGameObjectWithTag("GameStateManager").GetComponent<GameStateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.canMove && Input.GetButtonDown("Fire1"))
        {
            if (!EventSystem.current.currentSelectedGameObject)
            {
                Vector3 mousePos = Input.mousePosition;
                Ray ray = cam.ScreenPointToRay(mousePos);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit) && hit.collider.tag == "Walkable")
                {

                    //Debug.DrawLine(transform.position, hit.point, Color.green, 2f, false);
                    //Debug.Log("Hit");
                    //Debug.Log(hit.point);
                    navAgent.destination = hit.point;
                }
            }
        }
        if (Input.GetButtonDown("Fire2"))
        {
            SkillSystem ss = GetComponent<SkillSystem>();
            float roll = ss.SkillCheck(0);
            Debug.Log("I Rolled a " + roll.ToString() + " on my " + ss.Skills[0].skillName + " check!");

        }
    }
}
