using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControle : MonoBehaviour {

	public enum tiposPlayer{

		Player1, Player2
	}

	public tiposPlayer playerEscolhido;

	public bool iaAtivaInvencivel;

	public bool iaAtivaHumanizada;

	public Transform playerPos;

	public Transform bolaPos;

	public float speed;

	public float frameCount;

	public Vector3 posIA;

	public bool delayIAhumana;

	void Awake () {
		//Coleta o Trasnform da Bola
		bolaPos = (GameObject.FindGameObjectWithTag("Ball")).GetComponent<Transform>() ;

		//velocidade
		speed = 60f;

		//Identifica o Player
		SelecionarPlayer ();


		//Countagem de frame no inicio
		frameCount = 4f;

		//Determina uma direção inicial para IA Humanizada
		posIA = bolaPos.position;
	}


	

	void FixedUpdate () {

		//Controle da IA
		IAcontroleInvencivel();

		//Controle Humano
		ControleHumano ();

		//IA Humanizada
		IAcontroleHumanizada ();

		//Não deixa o Player sair da Tela
	    LimiteDaTela ();
	}

	void SelecionarPlayer(){

		//Se Estiver a Direita é o Player 1
		if (transform.position.x < 0f) {
			
			playerEscolhido = tiposPlayer.Player1;

		} 

		//Na direita, Player 2
		else if (transform.position.x > 0f) {
			
			playerEscolhido = tiposPlayer.Player2;

		}
	
	}

	//Ia Invencivel
	void IAcontroleInvencivel(){
		if (iaAtivaInvencivel && !iaAtivaHumanizada) {

			//Sempre vai estar na mesma posição que a Bola
			transform.position =new Vector3(transform.position.x, bolaPos.position.y, transform.position.z);

		}
	
	}

	//Controle do Player
	void ControleHumano(){
		if (!iaAtivaInvencivel && !iaAtivaHumanizada ) {

			//Muda a referencia do comando para cada Player, 1 e 2
			if (playerEscolhido == tiposPlayer.Player1) {
				float movimentoVertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;
				transform.Translate (0f,  movimentoVertical, 0f);

			} else if (playerEscolhido == tiposPlayer.Player2) {
				float movimentoVertical = Input.GetAxis("Vertical2") * speed * Time.deltaTime;
				transform.Translate (0f, movimentoVertical, 0f);

			}
		}
	}

	//Metodo que limita o valor em Y de cada player
	void LimiteDaTela(){
	
		transform.position = new Vector3 (
			transform.position.x, Mathf.Clamp(transform.position.y, -2.9f , 4.9f), transform.position.z);
	}

	//IA como limitações no jogo
	void IAcontroleHumanizada(){

		//Possui um delay pra começar
		if (delayIAhumana) {
			if (!iaAtivaInvencivel && iaAtivaHumanizada) {

				//Muda a posição que ira se mover
				AchandoPosIA ();



				if (frameCount > 0) {


					if (transform.position.y != posIA.y) {

						//Dependendo da posição entre o Player e a Bola, ira se mover para cima ou para baixo.
						if (transform.position.y > posIA.y + 0.5f && transform.position.y > posIA.y) {
							transform.position = new Vector3 (transform.position.x, transform.position.y - 0.2f, transform.position.z); 
						} else if (transform.position.y < posIA.y -0.5f && transform.position.y < posIA.y) {
							transform.position = new Vector3 (transform.position.x, transform.position.y + 0.2f, transform.position.z); 
						} else if (transform.position.y >= posIA.y + 0.5f || transform.position.y <= posIA.y - 0.5f) {
							Debug.Log ("Same POS");
						}
					}
			
				} else {
					Debug.Log ("Same POS");
				
				}
			}
		}
	}

	//Seta a Posição
	void AchandoPosIA(){
		//CountDown das Frames, só depois desse delay que a IA percebe a nova posição da bola
		if (frameCount > 0) {
		
			frameCount--;
		
		} else if (frameCount <= 0) {
			Debug.Log ("Negativo");
			frameCount = Random.Range(0, 16);
			posIA = bolaPos.position;

		}
	
	}

	void OnTriggerEnter(Collider c){

		//Caso colida com a Bola, acaba o delay da IA humanizada
		if (c.gameObject.tag == "Ball") {

			delayIAhumana = true;
			
		}
	}

	}




