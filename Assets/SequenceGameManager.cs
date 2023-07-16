using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CarterGames.Assets.AudioManager;

public class SequenceGameManager : MonoBehaviour
{
	[SerializeField]
	List<GameObject> arrows = new List<GameObject>();
	[SerializeField]
	GameObject upArrow, rightArrow, leftArrow, downArrow;
	[SerializeField]
	GameObject correctImg, wrongImg;
	[SerializeField]
	TextMeshProUGUI levelText, yourTurn;

	Material upMat;
	Material rightMat;
	Material leftMat;
	Material downMat;

	[SerializeField]
	int sequenceCount;
	[SerializeField]
	List<int> sequence = new List<int>();
	List<int> answer = new List<int>();
	[SerializeField]
	bool isRandomized, playingSequence, startWaiting, isPlaying;
	[SerializeField]
	float delay, wait, sequenceDelay, count1, count2;

	Ray ray;
	RaycastHit hit;
	void Start() {
		isRandomized = false;
		playingSequence = false;
		startWaiting = false;
		ResetImg();
		arrows.Add(upArrow);
		arrows.Add(rightArrow);
		arrows.Add(leftArrow);
		arrows.Add(downArrow);

		upMat = upArrow.GetComponent<MeshRenderer>().material;
		rightMat = rightArrow.GetComponent<MeshRenderer>().material;
		leftMat = leftArrow.GetComponent<MeshRenderer>().material;
		downMat = downArrow.GetComponent<MeshRenderer>().material;

		DisableAll();
		levelText.text = "Level: " + (sequenceCount - 2).ToString();
		//yourTurn.gameObject.SetActive(false);
		yourTurn.text = "Tunggu...";

		if(AudioManager.instance == null) {
			AudioManager.instance = FindObjectOfType<AudioManager>();
		}
	}

	void Update() {
		if (count1 < delay) {
			count1 += Time.deltaTime;
		}
		else if (isRandomized && !playingSequence) {
			playingSequence = true;
			StartCoroutine("PlaySequence");
		}
		else if (!isRandomized) {
			StopAllCoroutines();
			isPlaying = false;
			RandomizeSequence();
		}
		if (startWaiting) {
			if (count2 < wait) {
				count2 += Time.deltaTime;
				//mouse control
				if (isPlaying) {
					if(Application.platform == RuntimePlatform.WindowsEditor) {
						if (Input.GetMouseButtonDown(0)) {
							Debug.Log("Clicked");
							ray = new Ray(Camera.main.ScreenToWorldPoint(Input.mousePosition), Camera.main.transform.forward);
							if (Physics.Raycast(ray, out hit, 100f)) {
								Debug.Log("Hit " + hit.collider.gameObject.tag);
								switch (hit.collider.gameObject.tag) {
									case "UpArrow":
										PressUp();
										Debug.Log("Clicked on up");
										break;
									case "RightArrow":
										PressRight();
										Debug.Log("Clicked on right");
										break;
									case "LeftArrow":
										PressLeft();
										Debug.Log("Clicked on left");
										break;
									case "DownArrow":
										PressDown();
										Debug.Log("Clicked on down");
										break;
								}
							}
						}
					}
					else if(Application.platform == RuntimePlatform.Android) {
						if (Input.touchCount > 0) {
							if (Input.GetTouch(0).phase == TouchPhase.Began) {
								Touch touch = Input.GetTouch(0);
								ray = new Ray(Camera.main.ScreenToWorldPoint(touch.position), Camera.main.transform.forward);
								if (Physics.Raycast(ray, out hit, 100f)) {
									Debug.Log("Hit " + hit.collider.gameObject.tag);
									switch (hit.collider.gameObject.tag) {
										case "UpArrow":
											PressUp();
											Debug.Log("Clicked on up");
											break;
										case "RightArrow":
											PressRight();
											Debug.Log("Clicked on right");
											break;
										case "LeftArrow":
											PressLeft();
											Debug.Log("Clicked on left");
											break;
										case "DownArrow":
											PressDown();
											Debug.Log("Clicked on down");
											break;
									}
								}
							}
						}
					}
				}

				if (answer.Count == sequenceCount) {
					if (CheckAnswer(answer, sequence)) {
						Debug.Log("All correct");
						IsCorrect();
						isRandomized = false;
						playingSequence = false;
						count1 = 0f;
						startWaiting = false;
						count2 = 0f;
						sequenceCount++;
						levelText.text = "Level: " + (sequenceCount - 2).ToString();
						sequenceDelay -= 0.15f;
						sequenceDelay = Mathf.Clamp(sequenceDelay, 0.15f, float.MaxValue);
						//DisableAll();
					}
					else {
						Debug.Log("You're wrong");
					}
					//signify whether player is correct or false
				}
			}
			else {
				Debug.Log("Time's up!");
			}
		}

	}

	void IsCorrect() {
		correctImg.SetActive(true);
		wrongImg.SetActive(false);
		yourTurn.text = "Tunggu...";
		AudioManager.instance.Play("confirm-button", loop: false);
	}

	void IsWrong() {
		correctImg.SetActive(false);
		wrongImg.SetActive(true);
		AudioManager.instance.Play("wrong-answer", loop: false);
	}

	void ResetImg() {
		correctImg.SetActive(false);
		wrongImg.SetActive(false);
	}

	bool CheckAnswer(List<int> l1, List<int> l2) {
		if (l1.Count != l2.Count)
			return false;
		for (int i = 0; i < l1.Count; i++) {
			if (l1[i] != l2[i])
				return false;
		}
		return true;
	}

	void RandomizeSequence() {
		//1 is up
		//2 is right
		//3 is left
		//4 is down
		answer = new List<int>();
		sequence = new List<int>();
		for (int i = 0; i < sequenceCount; i++) {
			sequence.Add(Random.Range(1, 5));
		}
		isRandomized = true;
	}

	IEnumerator PlaySequence() {
		playingSequence = true;
		correctImg.SetActive(false);
		for (int i = 0; i < sequence.Count; i++) {
			yield return new WaitForSeconds(sequenceDelay);
			switch (sequence[i]) {
				case 1: EnableUp(); break;
				case 2: EnableRight(); break;
				case 3: EnableLeft(); break;
				case 4: EnableDown(); break;
				default: EnableAll(); break;
			}
			AudioManager.instance.Play("vs-pop-4", loop: false);
			yield return new WaitForSeconds(sequenceDelay);
			DisableAll();
			//play sound to signify player's turn
		}
		DisableAll();
		count2 = 0f;
		wait = 2f * sequenceCount;
		startWaiting = true;
		isPlaying = true;
		yourTurn.text = "Giliranmu!";
		ResetImg();
	}

	void PressUp() {
		AudioManager.instance.Play("vs-pop-4", loop: false);
		StopAllCoroutines();
		answer.Add(1);
		StartCoroutine("PlayUp");
		CheckInput();
		//check
	}

	void PressRight() {
		AudioManager.instance.Play("vs-pop-4", loop: false);
		StopAllCoroutines();
		answer.Add(2);
		StartCoroutine("PlayRight");
		CheckInput();
		//check
	}

	void PressLeft() {
		AudioManager.instance.Play("vs-pop-4", loop: false);
		StopAllCoroutines();
		answer.Add(3);
		StartCoroutine("PlayLeft");
		CheckInput();
		//check
	}

	void PressDown() {
		AudioManager.instance.Play("vs-pop-4", loop: false);
		StopAllCoroutines();
		answer.Add(4);
		StartCoroutine("PlayDown");
		CheckInput();
		//check
	}

	void CheckInput() {
		if (answer[answer.Count - 1] != sequence[answer.Count - 1]) {
			Debug.Log("You fail");
			IsWrong();
			isPlaying = false;
			startWaiting = false;
		}
		else {
			Debug.Log("Correct");
		}
	}

	IEnumerator PlayUp() {
		EnableUp();
		yield return new WaitForSeconds(sequenceDelay);
		DisableAll();
	}

	IEnumerator PlayRight() {
		EnableRight();
		yield return new WaitForSeconds(sequenceDelay);
		DisableAll();
	}

	IEnumerator PlayLeft() {
		EnableLeft();
		yield return new WaitForSeconds(sequenceDelay);
		DisableAll();
	}

	IEnumerator PlayDown() {
		EnableDown();
		yield return new WaitForSeconds(sequenceDelay);
		DisableAll();
	}

	void DisableAll() {
		upMat.DisableKeyword("_EMISSION");
		rightMat.DisableKeyword("_EMISSION");
		leftMat.DisableKeyword("_EMISSION");
		downMat.DisableKeyword("_EMISSION");
	}
	void EnableAll() {
		upMat.EnableKeyword("_EMISSION");
		rightMat.EnableKeyword("_EMISSION");
		leftMat.EnableKeyword("_EMISSION");
		downMat.EnableKeyword("_EMISSION");
	}

	void EnableUp() {
		upMat.EnableKeyword("_EMISSION");
		rightMat.DisableKeyword("_EMISSION");
		leftMat.DisableKeyword("_EMISSION");
		downMat.DisableKeyword("_EMISSION");
	}

	void EnableRight() {
		upMat.DisableKeyword("_EMISSION");
		rightMat.EnableKeyword("_EMISSION");
		leftMat.DisableKeyword("_EMISSION");
		downMat.DisableKeyword("_EMISSION");
	}

	void EnableLeft() {
		upMat.DisableKeyword("_EMISSION");
		rightMat.DisableKeyword("_EMISSION");
		leftMat.EnableKeyword("_EMISSION");
		downMat.DisableKeyword("_EMISSION");
	}

	void EnableDown() {
		upMat.DisableKeyword("_EMISSION");
		rightMat.DisableKeyword("_EMISSION");
		leftMat.DisableKeyword("_EMISSION");
		downMat.EnableKeyword("_EMISSION");
	}
}
