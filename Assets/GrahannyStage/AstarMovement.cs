using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstarMovement : MonoBehaviour {

    private Pathfinding pf;
    public float speed;
    public float forcePower;
    private bool grounded = false;
    private bool lastNodeReached = false;

    // Use this for initialization
    void Start () {
        pf = GetComponent<Pathfinding>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (grounded) { 
            RaycastHit hit;
            if (pf.path != null)
            {
                if (pf.path.Count > 0)
                {
                    if ((transform.position - pf.path[pf.path.Count - 1].worldPosition).magnitude >= 1 && !lastNodeReached)
                    {
                        Node target = pf.path[0];
                        Vector3 center = transform.position;
                        Vector3 left = transform.position - (transform.right * 0.5f);
                        Vector3 right = transform.position + (transform.right * 0.5f);
                        foreach (Node n in pf.path)
                        {
                            if (!Physics.Raycast(center, n.worldPosition - center, out hit, (center - n.worldPosition).magnitude) &&
                                !Physics.Raycast(left, n.worldPosition - center, out hit, (left - n.worldPosition).magnitude) &&
                                !Physics.Raycast(right, n.worldPosition - center, out hit, (right - n.worldPosition).magnitude))
                            {
                                //non collide con nulla, quindi è visibile
                                target = n;
                            }
                            else
                            {
                                //il nodo non è raggiungibile, quindi mi tengo l'ultimo che potevo raggiungere
                                if (hit.collider.gameObject.Equals(transform.gameObject)) target = n;
                                else break;
                                break;
                            }
                        }

                        Debug.DrawRay(center, target.worldPosition - center, Color.green);
                        Debug.DrawRay(left, target.worldPosition - center, Color.green);
                        Debug.DrawRay(right, target.worldPosition - center, Color.green);


                        transform.LookAt(new Vector3(target.worldPosition.x, 0.0f, target.worldPosition.z));
                        //transform.Rotate(new Vector3(target.worldPosition.x - transform.position.x, 0.0f, target.worldPosition.z - transform.position.z));
                        //Debug.DrawRay(transform.position, transform.forward, Color.green);

                        //transform.LookAt(target.worldPosition);
                        //Debug.DrawRay(transform.position, target.worldPosition - transform.position, Color.green);
                        /*if (Physics.Raycast(left, transform.forward, out hit, 1.0f))
                        {
                            if (hit.collider.gameObject.layer != 9 && !hit.collider.gameObject.Equals(transform.gameObject)) transform.position += (transform.right * speed * Time.deltaTime);
                        }
                        else if (Physics.Raycast(right, transform.forward, out hit, 1.0f))
                        {
                            if (hit.collider.gameObject.layer != 9 && !hit.collider.gameObject.Equals(transform.gameObject)) transform.position += (-transform.right * speed * Time.deltaTime);
                        }*/
                        //Debug.DrawRay(left, transform.forward, Color.green);
                        //Debug.DrawRay(right, transform.forward, Color.green);
                        //transform.Translate((target.worldPosition + new Vector3(0,1,0) - transform.position).normalized * speed * Time.deltaTime);
                        //transform.Translate(transform.forward * speed * Time.deltaTime);
                        transform.position += (target.worldPosition - transform.position).normalized * speed * Time.deltaTime;
                    }
                    else
                    {
                        lastNodeReached = true;
                        transform.position += (pf.finalPoint - transform.position).normalized * speed * Time.deltaTime;
                    }
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject go = collision.collider.gameObject;
        if (go.layer == 9)
        {
            //Debug.Log("colpito");
            go.GetComponent<Rigidbody>().AddForce((transform.forward + new Vector3(0,1,0)) * forcePower);
        }

        else if (go.layer == 11)
        {
            grounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.gameObject.layer == 11) grounded = false;
    }
}
