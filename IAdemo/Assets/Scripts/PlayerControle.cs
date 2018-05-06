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
		IAcontroleInvencivel();

		//Controle Humano
		ControleHumano ();

		//Não deixa o Player sair da Tela
	    LimiteDaTela ();
	}

	void SelecionarPlayer(){
		
		if (transform.position.x < 0f) {
			
			playerEscolhido = tiposPlayer.Player1;

		} else if (transform.position.x > 0f) {
			
			playerEscolhido = tiposPlayer.Player2;

		}
	
	}

	void IAcontroleInvencivel(){
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

	void ControleHumano(){
		if (!iaAtiva) {

			if (playerEscolhido == tiposPlayer.Player1) {
				float movimentoVertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;
				transform.Translate (0f,  movimentoVertical, 0f);

			} else if (playerEscolhido == tiposPlayer.Player2) {
				float movimentoVertical = Input.GetAxis("Vertical2") * speed * Time.deltaTime;
				transform.Translate (0f, movimentoVertical, 0f);

			}
		}
	}

	void LimiteDaTela(){
	
		transform.position = new Vector3 (
			transform.position.x, Mathf.Clamp(transform.position.y, -2.9f , 4.9f), transform.position.z);
	}
}
