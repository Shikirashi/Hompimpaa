using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CarterGames.Assets.AudioManager;

public class BallScript : MonoBehaviour{
    [SerializeField]
    private int speed, delay;
    [SerializeField] private float velocity;
    private int x, y;
    private int player1Score, player2Score;
    private Rigidbody2D rb;
    PingPongManager pingpong;
    AudioVariables audioVars;

    public TextMeshProUGUI score1;
    public TextMeshProUGUI score2;
    void Start() {
        audioVars = FindObjectOfType<AudioVariables>();
        pingpong = FindObjectOfType<PingPongManager>();
        rb = GetComponent<Rigidbody2D>();
        x = 1;
        y = 1;
        player1Score = 0;
        player2Score = 0;
        score1.text = player1Score.ToString();
        score2.text = player2Score.ToString();
        StartCoroutine("StartBall"); 
        velocity = rb.velocity.magnitude;
    }

    private IEnumerator StartBall() {
        transform.position = new Vector3(0, 0, 0);

        yield return new WaitForSeconds(delay);

        int rand = Random.Range(1, 5);
        if (Application.platform == RuntimePlatform.WindowsEditor) {
            rand = 6;
        }
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
            case 6:
                x = 4;
                y = 0;
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
        if(player1Score == 5) {
            pingpong.ClearLevel("Pemain 1 menang!");
		}
        else if(player2Score == 5) {
            pingpong.ClearLevel("Pemain 2 menang!");
        }
        AudioManager.instance.Play("vs-pop-4", volume: audioVars.SFXVolume, loop: false);
        StartCoroutine("StartBall");
    }

	private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.transform.tag == "Player") {
            Debug.Log("Bounce off player");
            if(rb.velocity.magnitude < 20f) {
                rb.AddForce(rb.velocity * 5f, ForceMode2D.Force);
            }
            AudioManager.instance.Play("bounce", volume: audioVars.SFXVolume, loop: false);
            velocity = rb.velocity.magnitude;
        }
    }
}
