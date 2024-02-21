using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class miscare : MonoBehaviour
{
    public Text MyscoreText;
    public static int scr = 0;

    private Vector2 _direction = Vector2.right;
    private List<Transform> _segments;
    public Transform segmentPrefab;
    private void Start()
    {
        scr = 0;
        MyscoreText.text = "Score : " + scr;
        _segments = new List<Transform>();
        _segments.Add(this.transform);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _direction = Vector2.up;
        }else if (Input.GetKeyDown(KeyCode.S))
        {
            _direction = Vector2.down;
        }else if (Input.GetKeyDown(KeyCode.A))
        {
            _direction = Vector2.left;
        }else if (Input.GetKeyDown(KeyCode.D))
        {
            _direction = Vector2.right;
        }
    }

    private void FixedUpdate()
    {

        for(int i =  _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }

        this.transform.position = new Vector3(
                Mathf.Round(this.transform.position.x) + _direction.x,
                Mathf.Round(this.transform.position.y) + _direction.y,
                0.0f
            );
    }

    private void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = _segments[ _segments.Count - 1].position;
        scr++;
        MyscoreText.text = "Score : " + scr;
        _segments.Add(segment);

    }

    private void ResetState()
    {
        for(int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }
        _segments.Clear();
        _segments.Add(this.transform);

        this.transform.position = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "haleala")
        {
            Grow();
        }else if(other.tag == "perete")
        {
            SceneManager.LoadScene("menu");
        }
        
    }

}
