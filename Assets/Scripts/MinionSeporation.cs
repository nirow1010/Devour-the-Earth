using UnityEngine;

public class MinionSeporation : MonoBehaviour
{
    GameObject[] AI;
    public float spaceBetween;
    
    void Start()
    {
        AI = GameObject.FindGameObjectsWithTag("AvoidMe");
        //AI = GameObject.FindGameObjectsWithTag("enemy");
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject go in AI)
        {
            if (go != this.gameObject)
            {

                float distance = Vector2.Distance(go.transform.position, this.transform.position);
                if (distance <= spaceBetween)
                {
                    Vector2 direction = transform.position - go.transform.position;
                    transform.TransformDirection(direction * Time.deltaTime);
                }
            }
        }
    }
}
