using UnityEngine;

public class backgroundManager : MonoBehaviour
{
    public GameObject bckPrefab;

    public float speed;

    private GameObject[] bcks;

    public float pivotPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bcks = new GameObject[3];
        for (int i = 0; i < 3; i++)
        {
            float xPos = pivotPoint - (pivotPoint / 2 * i);
            float yPos = pivotPoint - (pivotPoint / 2 * i);
            Vector2 pos = new Vector2(xPos, yPos);
            bcks[i] = Instantiate(bckPrefab, pos, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 3; i++)
        {
            float xPos = bcks[i].transform.position.x + speed * Time.deltaTime;
            float yPos = bcks[i].transform.position.y + speed * Time.deltaTime;
            Vector2 newPos = new Vector2(xPos, yPos);
            bcks[i].transform.position = newPos;
            if (xPos > -pivotPoint / 2)
            {
                Vector2 pivot = new Vector2(pivotPoint, pivotPoint);
                bcks[i].transform.position = pivot;
            }
        }
    }
}
