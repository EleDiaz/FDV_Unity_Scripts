using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    public Color color;
    public GameObject player;
    public float triggerDistance = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        color = Color.HSVToRGB(Random.Range(0.0f, 1.0f), 1, 1);

        Material mat = GetComponent<Renderer>().material;

        mat.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (triggerDistance > distance)
        {
            Player p = player.GetComponent<Player>();
            Material pMat = player.GetComponent<Renderer>().material;
            pMat.color = Color.Lerp(color, p.initialColor, distance / triggerDistance);
        }
    }
}
