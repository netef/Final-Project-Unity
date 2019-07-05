using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDestroy : MonoBehaviour
{
    float XPos, YPos, ZPos;
    float waiting = 0.02f;
    // Start is called before the first frame update
    void Start()
    {
        XPos = transform.position.x;
        YPos = transform.position.y;
        ZPos = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            transform.position = new Vector3(XPos, YPos + 0.1f, ZPos);
            yield return new WaitForSeconds(waiting);
            transform.position = new Vector3(XPos, YPos + 0.2f, ZPos);
            yield return new WaitForSeconds(waiting);
            transform.GetComponent<Collider>().isTrigger = false;
            transform.position = new Vector3(XPos, YPos + 0.3f, ZPos+0.5f);
            yield return new WaitForSeconds(waiting);
            transform.position = new Vector3(XPos, YPos + 0.4f, ZPos+1.0f);
            yield return new WaitForSeconds(waiting);
            transform.position = new Vector3(XPos, YPos -0.1f, ZPos+1.0f);
            yield return new WaitForSeconds(waiting);
            transform.position = new Vector3(XPos, YPos -0.6f, ZPos+2.0f);
            yield return new WaitForSeconds(waiting);
            transform.position = new Vector3(XPos, YPos - 1.6f, ZPos + 2.0f);
            yield return new WaitForSeconds(waiting);
            transform.position = new Vector3(XPos, YPos - 2.6f, ZPos + 2.0f);
            yield return new WaitForSeconds(waiting);
            transform.position = new Vector3(XPos, YPos - 4.0f, ZPos + 2.0f);
            yield return new WaitForSeconds(0.25f);
            transform.GetComponent<Collider>().isTrigger = true;
            Destroy(gameObject);
        }
    }
}
