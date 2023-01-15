using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallUp : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float Speed;

    private float Timer = 15;

    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W)/* || Input.GetKey(KeyCode.UpArrow)*/) //Запускаем шар вверх по W
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * Speed, ForceMode2D.Force);
            if (Mathf.Abs(gameObject.GetComponent<Rigidbody2D>().velocity.y) > Speed)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, gameObject.GetComponent<Rigidbody2D>().velocity.y * Speed);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Timer -= Time.deltaTime; //Увеличение вертикальной скорости шара раз в 15 сек
        if (Timer <= 0)
        {
            Speed += 1;
            Timer = 15;
        }
    }
}
