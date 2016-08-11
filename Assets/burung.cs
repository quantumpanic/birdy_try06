using UnityEngine;
using System.Collections;

public class burung : MonoBehaviour {

	public float kecepatan = 20;
	public Vector3 pergerakan = Vector3.zero;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		CharacterController penggerak = GetComponent<CharacterController>();

		float sumbuX = Input.GetAxis("Horizontal");
		float sumbuY = Input.GetAxis("Vertical");

		pergerakan = new Vector3(sumbuX,sumbuY);
		pergerakan = transform.TransformDirection(pergerakan);
		pergerakan *= kecepatan;

		penggerak.Move(Vector3.right * 0.01f);
		penggerak.Move(pergerakan * Time.deltaTime);

		transform.position = new Vector3(
			Mathf.Clamp(transform.position.x, -5,-5),
			Mathf.Clamp(transform.position.y, -5,5)
		);

		if (!gameOver)
		{
			waktuLewat += Time.deltaTime;
			teksWaktu.text = "Waktu: " + waktuLewat.ToString("F2");
		}

		else
		{
			if (Input.GetKey(KeyCode.Return))
				Application.LoadLevel("birdGame");
		}
	
	}

	public bool gameOver = false;
	public GameObject teksKalah;

	void OnControllerColliderHit (ControllerColliderHit hit)
	{
		if (!gameOver)
		{
			gameOver = true;
			teksKalah.SetActive(true);
			GetComponentInChildren<Animator>().Play("birdDie");
			GetComponent<AudioSource>().time = 0.1f;
			GetComponent<AudioSource>().Play();
		}
	}

	public float waktuLewat = 0;
	public UnityEngine.UI.Text teksWaktu;
}
