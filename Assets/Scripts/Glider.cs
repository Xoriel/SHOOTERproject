using UnityEngine;

public class Glider : MonoBehaviour
{
    public bool goingUp;
    public float speed;
    private GameManager gameManager;
    // Update is called once per frame
    void Start()
    {
    gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if(goingUp)
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed);
        }
        else if(goingUp == false)
        {
            transform.Translate(Vector3.down * Time.deltaTime * speed);
        }

        if(transform.position.y < -gameManager.verticalScreenSize * 1.25 || transform.position.y > gameManager.verticalScreenSize * 1.25)
        {
            Destroy(this.gameObject);
        }

    }
}
