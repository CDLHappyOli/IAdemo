using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControle : MonoBehaviour {

	public enum tiposPlayer{

		Player1, Player2
	}

	public tiposPlayer playerEscolhido;

	public bool iaAtiva;

	public Transform playerPos;

	public Transform bolaPos;

	public float speed;

	void Awake () {
		//Coleta o Trasnform da Bola
		bolaPos = (GameObject.FindGameObjectWithTag("Ball")).GetComponent<Transform>() ;

		//velocidade
		speed = 60f;

		//Identifica o Player
		SelecionarPlayer ();


	}
	

	void FixedUpdate () {

		//Controle da IA
		IAcontrole ();
	}

	void SelecionarPlayer(){
		
		if (transform.position.x < 0f) {
			
			playerEscolhido = tiposPlayer.Player1;

		} else if (transform.position.x > 0f) {
			
			playerEscolhido = tiposPlayer.Player2;

		}
	
	}

	void IAcontrole(){
		if (iaAtiva) {
			/*if (transform.position.y < bolaPos.position.y) {
				transform.Translate (Vector3.up * speed * Time.deltaTime);
			} else 	if (transform.position.y > bolaPos.position.y) {
				transform.Translate (Vector3.down * speed * Time.deltaTime);
			} */

			transform.position =new Vector3(transform.position.x, bolaPos.position.y, transform.position.z);
			//transform.Translate (new Vector3(transform.position.x, bolaPos.position.y, transform.position.z) * speed * Time.deltaTime);
		
		}
	
	}
}
