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


	void Awake(){
	
		ballRB = (GameObject.FindGameObjectWithTag("Ball")).GetComponent<Rigidbody>() ;
		acertarAngulo = 1f;
	}

	void Start () {
		acertarAngulo = 1f;
	}
	

	void Update () {
		//Angulo.z = 0;
		Angulo = (Vector3.down * acertarAngulo  + Vector3.right).normalized;
		ballRB.AddForce (Angulo * 10f * Time.deltaTime);
	}
}
