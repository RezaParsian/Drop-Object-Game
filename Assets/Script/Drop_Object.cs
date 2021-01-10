using UnityEngine;

public class Drop_Object : MonoBehaviour
{
    public float dealy = 2f;
    float temp = 0;
    public Transform prefab = null;
    public string[] jobs = null;
    string correct_job;
    // Start is called before the first frame update
    void Start()
    {
        temp = dealy;
        correct_job = jobs[Random.Range(0, jobs.Length)];
        GameObject.Find("basket").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(correct_job);
    }

    // Update is called once per frame
    void Update()
    {
        if (dealy > 0)
        {
            dealy -= Time.deltaTime;
            if (dealy <= 0)
            {
                int rnd = Random.Range(-4, 4);
                dealy = temp;
                Transform item = Instantiate(prefab, new Vector3(rnd, 16, -1), Quaternion.identity);
                ChangeItemSprite(item);
            }
        }
    }

    void ChangeItemSprite(Transform item)
    {
        string job = jobs[Random.Range(0, jobs.Length)];
        string tag;
        if (job == correct_job)
        {
            tag = "correct";
        }
        else
        {
            tag = "wrong";
        }
        item.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(job + Random.Range(1, 3));
        item.tag = tag;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
        if (tag=="correct" && transform.name=="basket")
        {
            Camera.main.GetComponent<Point>().IncresePoint();
        }

        if (tag == "wrong" && transform.name == "basket")
        {
            Handheld.Vibrate();
        }
            Destroy(collision.gameObject);
    }
}
