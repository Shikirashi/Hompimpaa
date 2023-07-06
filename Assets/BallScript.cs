using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallScript : MonoBehaviour{
    [SerializeField]
    private int speed, delay;
    private int x, y;
    private int player1Score, player2Score;
    private Rigidbody2D rb;

    public TextMeshProUGUI score1;
    public TextMeshProUGUI score2;
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        x = 1;
        y = 1;
        player1Score = 0;
        player2Score = 0;
        score1.text = player1Score.ToString();
        score2.text = player2Score.ToString();
        StartCoroutine("StartBall");
    }

    private IEnumerator StartBall() {
        transform.position = new Vector3(0, 0, 0);

        yield return new WaitForSeconds(delay);

        int rand = Random.Range(1, 5);
        switch (rand) {
            case 1:
                x = -4;
                y = 2;
                break;
            case 2:
                x = 4;
                y = -2;
                break;
            case 3:
                x = -4;
                y = -2;
                break;
            case 4:
                x = 4;
                y = 2;
                break;
            default:
                x = 4;
                y = 2;
                break;
        }
        rb.AddForce(new Vector2(x, y) * speed, ForceMode2D.Force);
    }
	private void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log("Hit " + collision.name);
        if (collision.transform.tag == "Goal1") {
            player2Score++;
            score2.text = player2Score.ToString();
        }
        else if (collision.transform.tag == "Goal2") {
            player1Score++;
            score1.text = player1Score.ToString();
        }
        rb.velocity = new Vector2(0f, 0f);
        StopCoroutine("StartBall");
        StartCoroutine("StartBall");
    }

	private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.transform.tag == "Player") {
            rb.AddForce(rb.velocity * 2f, ForceMode2D.Force);
        }
    }
}
