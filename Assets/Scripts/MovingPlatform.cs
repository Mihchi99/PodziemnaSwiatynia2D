using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 5f;
    public Transform[] points;
    private int i;
    private Vector3 lastPosition;

    void Start()
    {
        transform.position = points[i].position;
        lastPosition = transform.position;
    }

    void FixedUpdate()
    {
        lastPosition = transform.position;

        if(Vector2.Distance(transform.position, points[i].position) < 0.01f)
        {
            i++;
            if(i == points.Length)
            {
                i = 0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.fixedDeltaTime);

        Vector3 platformMovement = transform.position - lastPosition;

        Collider2D platformCollider = GetComponent<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D();
        filter.useTriggers = false;
        filter.SetLayerMask(Physics2D.AllLayers);
        filter.useLayerMask = true;

        Collider2D[] hits = new Collider2D[10];
        int numHits = Physics2D.OverlapCollider(platformCollider, filter, hits);

        for(int j = 0; j < numHits; j++)
        {
            if(hits[j] != null && hits[j].CompareTag("Player"))
            {
                hits[j].transform.position += platformMovement;
            }
        }
    }
}