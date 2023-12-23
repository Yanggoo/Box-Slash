using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody targetRb;
    private float xRange = 4.0f;
    private float spawnY = -2.0f;
    private float minSpeed = 12.0f;
    private float maxSpeed = 16.0f;
    private float torqueRange = 10.0f;
    private GameManager gameManager;
    public int scoreValue;
    public ParticleSystem particleSystem;
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        targetRb = GetComponent<Rigidbody>();
        transform.position = GetRandomPos();
        targetRb.AddForce(GetRandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(GetRandomTorque(), ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {

    }
    Vector3 GetRandomPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), spawnY);
    }
    Vector3 GetRandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }
    Vector3 GetRandomTorque()
    {
        return new Vector3(Random.Range(-torqueRange, torqueRange),
            Random.Range(-torqueRange, torqueRange), Random.Range(-torqueRange, torqueRange));

    }
    //private void OnMouseDown()
    //{
    //    Destroy(gameObject);
    //    gameManager.UpdateScore(scoreValue);
    //    Instantiate(particleSystem, transform.position, particleSystem.transform.rotation);
    //}
    public void DestroyThisTarget()
    {

        if (gameManager.isGameActive)
        {
            Destroy(gameObject);
            gameManager.UpdateScore(scoreValue);
            Instantiate(particleSystem, transform.position, particleSystem.transform.rotation);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (!gameObject.CompareTag("Bad"))
        {
            if (gameManager.isGameActive)
                gameManager.UpdateLives(-1);
        }
        if (gameManager.lives == 0)
        {
            gameManager.GameOver();
        }
    }
}
