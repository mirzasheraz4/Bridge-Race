using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public List<GameObject> BoxsList;
    public GameObject Stairs;
    public Rigidbody RbPlayer;

    public float HorizontalInput;
    public float VerticalInput;

    private float PlayerMovementSpeed = 10;

    private Vector3 BoxPosition = new Vector3 (0, 1.1f, -1f);
    void Start()
    {

        RbPlayer.GetComponent<Rigidbody>();
    }
    void Update()
    {
        Movement();
    }
    public void Movement()
    {
        HorizontalInput = Input.GetAxis("Horizontal");
        VerticalInput = Input.GetAxis("Vertical");

        RbPlayer.AddForce(Vector3.right * PlayerMovementSpeed * HorizontalInput * Time.deltaTime, ForceMode.Impulse);
        RbPlayer.AddForce(Vector3.forward * PlayerMovementSpeed * VerticalInput * Time.deltaTime , ForceMode.Impulse);
     
    }
    private void OnCollisionEnter(Collision collision)
    {
       Debug.Log("" + collision.gameObject.name);
        if (collision.gameObject.tag == "Box")
        {
            collision.gameObject.transform.parent = gameObject.transform;
            BoxsList.Add(collision.gameObject);
            //Debug.Log("LIST ELEMENT 0 IS === )"+BoxsList[0].name);
            if (BoxsList.Count == 1)
            {
               BoxsList[0].transform.position = new Vector3(0, 1, -1.5f);
            }
            float j = 0.5f;
            if (BoxsList.Count > 1)
            {
                for (int i = 0; i < BoxsList.Count; i++)
                {
                    BoxsList[i].transform.position = new Vector3(transform.position.x,j , transform.position.z - 1);
                    j += 0.5f ;
                }
            }
        }
        if (collision.gameObject.tag == "Stair_Detector" || collision.gameObject.tag == "Stairs")
        {
            float k = 0;
            int j = 0;
            float StairPositionZ = 0.5f;
            for (int i = 0; i < BoxsList.Count; i++)
            {
                //BoxsList.RemoveAt(i);
                //Debug.Log("Collided with " + gameObject.name);
                //Instantiate(Stairs, new Vector3(0, 0f, 10f + k), transform.rotation);
                StairPositionZ = GameObject.Find("Stair_Detector").GetComponent<Transform>().position.z;
                Instantiate(Stairs, new Vector3(0, 0f, StairPositionZ + k), transform.rotation);

                Debug.Log("ROUND NO::==="+i);
                //GameObject.Find("Stair_Detector").SetActive(false);
                k += 1.2f;
                j++;
                BoxsList[i].SetActive(false);
                //GameObject.Find("Stair_Detector").GetComponent<Transform>().position = 
            }

            BoxsList.Clear();
        }
    }
}
