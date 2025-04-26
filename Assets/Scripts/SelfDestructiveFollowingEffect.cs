using UnityEngine;

public class SelfDestructiveFollowingEffect : MonoBehaviour
{
    private Transform follow;

    public void SetFollow(Transform follow)
    {
        this.follow = follow;
    }

    // Update is called once per frame
    void Update()
    {
        if (follow == null)
        {
            Destroy(gameObject);
        }
        else {
            transform.position = follow.position;
        }
    }
}
