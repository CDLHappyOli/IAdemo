using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	public enum ultimoPlayer
	{
		PlayerOne, PlayerTwo
	
	}

	public ultimoPlayer Toque;

	public Vector3 Angulo;

	public Rigidbody ballRB;

	public float acertarAngulo;

	public float speed;


	void Awake(){
	
		ballRB = (GameObject.FindGameObjectWithTag("Ball")).GetComponent<Rigidbody>() ;
		acertarAngulo = 1f;
	}

	void Start () {
		acertarAngulo = 1f;
		speed = 20f;
	}
	

	void Update () {
		
		Angulo = (Vector3.down * acertarAngulo  + Vector3.right).normalized;
		ballRB.AddForce (Angulo *  speed * Time.deltaTime);
	}

	void OnTriggerEnter(Collider c){

		if(c.tag == "Player1"){

			Toque = ultimoPlayer.PlayerOne;

		} else if(c.tag == "Player2"){

			Toque = ultimoPlayer.PlayerTwo;

		}
	}
}
