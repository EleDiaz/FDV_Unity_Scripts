using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const float MIN_RESPAWN_SECS = 5.0f;
    private const float MAX_RESPAWN_SECS = 10.0f;

    public Color initialColor = Color.white;
    public float movementSpeed = 1.0f;
    public float rotationSpeed = 5.0f;
    public float cameraDistance = 10.0f;
    public float radiusSpawn = 100.0f;
    public PrimitiveType[] kindPower = new PrimitiveType[4]
        {
            PrimitiveType.Cube,
            PrimitiveType.Cylinder,
            PrimitiveType.Sphere,
            PrimitiveType.Capsule
        };
    public int amountPowerObjects = 5;

    private GameObject[] powerObjects;


    // Start is called before the first frame update
    void Start()
    {


        Material mat = GetComponent<Renderer>().material;
        mat.color = initialColor;

        Camera.main.transform.position = transform.position + transform.rotation * new Vector3(cameraDistance, 0, 0);

        powerObjects = new GameObject[5];
        for (int i = 0; i < amountPowerObjects; i++)
        {
            GameObject obj = //GameObject.Instantiate(
                GameObject.CreatePrimitive(kindPower[Random.Range(0, kindPower.Length)]);
            //GetRandomPosition(), 
            //Quaternion.identity
            //);
            obj.transform.position = GetRandomPosition();
            powerObjects[i] = obj;
            Collectables collectable = obj.AddComponent<Collectables>() as Collectables;
            collectable.player = gameObject;
        }

        StartCoroutine(UpdateObjects());
    }


    // Update is called once per frame
    void Update()
    {
        // float verticalLook = Input.GetAxis("Mouse Y"); // TO BE Used with the camera
        float horizontalLook = Input.GetAxis("Mouse X"); 

        transform.rotation *= Quaternion.Euler(0, -horizontalLook * rotationSpeed, 0);

        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        
        transform.position += 
            transform.rotation * new Vector3(vertical, 0, -horizontal).normalized * movementSpeed * Time.deltaTime;

        Camera.main.transform.position = 
            transform.position + transform.rotation * new Vector3(-cameraDistance, 2, 0);
        Camera.main.transform.LookAt(transform.position);
    }

    private Vector3 GetRandomPosition()
    {
        return new Vector3(
            Random.Range(-radiusSpawn / 2, radiusSpawn / 2), 
            1, 
            Random.Range(-radiusSpawn / 2, radiusSpawn / 2)
        );
    }

    private IEnumerator UpdateObjects()
    {
        foreach (GameObject powerObj in powerObjects)
        {
            powerObj.transform.position = GetRandomPosition();
            yield return new WaitForSeconds(Random.Range(MIN_RESPAWN_SECS, MAX_RESPAWN_SECS));
        }
        UpdateObjects();
    }
}
