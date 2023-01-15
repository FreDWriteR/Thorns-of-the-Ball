using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class MovingBackGround : MonoBehaviour
{
    //public GameObject backgroundWall;

    [SerializeField] private float speed;
    [SerializeField] private MeshRenderer MRenderer;
    private Vector2 meshOffset;
    private float x;
    public GameObject Thorn;

    

    [SerializeField] private GameObject Ball;

    public bool GameOver = false;

    bool EndWaitForShowThorn = true;

    private const string fileSpeed = "fileSpeed.txt";

    // Start is called before the first frame update
    void Start()
    {
        meshOffset = MRenderer.sharedMaterial.mainTextureOffset; //�������� �������� ����
        if (File.Exists(fileSpeed)) 
        {
            var fileContent = File.ReadAllText(fileSpeed); //��������� �� ����� �������� �� ��������� ���������, ���������� � ���� � ������ �����
            speed = float.Parse(fileContent);
        }
    }

    void Awake()
    {
        if (Ball != null)
        {
            GameObject GameBall = Instantiate(Ball, new Vector2(0f, 0f), Quaternion.identity); //������� ��� �� �������
            Thorn.GetComponent<Trigger>().SecCount = (int)Time.time; //��� ������ ����������������� ��������� ������� 
        }

    }

    IEnumerator ShowThorn(float x) //��������� �� ������� � ������ ��������
    {
        GameObject ThornForBall;

        yield return new WaitForSeconds(3f / (speed * 10));
        EndWaitForShowThorn = true;
        if (Thorn != null) 
        { 
            ThornForBall = Instantiate(Thorn, new Vector2(0f, 0f), Quaternion.identity);
            Vector2 ThornSize = ThornForBall.GetComponent<SpriteRenderer>().size;
            ThornSize.y *= UnityEngine.Random.Range(1, 6);
            ThornForBall.GetComponent<SpriteRenderer>().size = ThornSize;
            Vector3 ThornPosition = new Vector3(10.5f, UnityEngine.Random.Range(-4, 4), -1f);
            ThornForBall.transform.position = ThornPosition;
            StartCoroutine(moveThorn(ThornForBall, ThornPosition));
        }
    }

    IEnumerator moveThorn(GameObject Thorn, Vector2 ThornPosition) //���������� ��������
    {
        
        while (ThornPosition.x > -10.5f)
        {
            ThornPosition.x -= Time.deltaTime * (1.77f * speed * 10);
            Thorn.transform.position = ThornPosition;
            float t = Time.time;
            yield return new WaitForEndOfFrame();
        }
        Destroy(Thorn);
    }

    private void OnDisable()
    {
        MRenderer.sharedMaterial.mainTextureOffset = meshOffset;
    }

    // Update is called once per frame
    void Update()
    {
        //�������� ��������
        x = Mathf.Repeat(Time.time * speed, 1);
        var xForWall = x; 
        var offset = new Vector2(xForWall, meshOffset.y);
        ////////

        if (EndWaitForShowThorn) //����� ����� ��������� ������� ������
        {
            EndWaitForShowThorn = false;
            StartCoroutine(ShowThorn(xForWall));
        }
        
        MRenderer.sharedMaterial.mainTextureOffset = offset; //�������� ��������

    }
}
