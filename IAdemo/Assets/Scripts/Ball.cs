﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	//Mostra último Player a Tocar na bola
	public enum ultimoPlayer
	{
		PlayerOne, PlayerTwo
	
	}

	//Salva ultimo Player a tocar na bola
	public ultimoPlayer Toque;

	//A Soma dos Vetores que direciona a bola
	public Vector3 Angulo;

	//RigidBody da Bola
	public Transform ballRB;

	//Corrigi o Angulo do Vetor que direciona a bola
	public float acertarAngulo;

	//Velocidade da Bola
	public float speed;

	//Opta pela direção Vertical do Vetor
	public Vector3 dirY;

	void Awake(){

		//Coleta o Trasnform da Bola
		ballRB = (GameObject.FindGameObjectWithTag("Ball")).GetComponent<Transform>() ;

		//Certificasse de acerta o Angulo antes mesmo do Script ser chamado
		acertarAngulo = Random.Range(0f, 1f);

		//Da uma velocidade a bola
		speed = 10f;
	}
	

	void FixedUpdate () {

		//Chama o Método responsavel por chamar o método de movimentação da bola.
		CallMov();

		//Eventos maas laterais
	    BatendoNaParede ();
	}

	void OnTriggerEnter(Collider c){

		//Caso colida com Player1
		if(c.gameObject.tag == "Player1"){
			Debug.Log ("Colidindo com Player1");
			Debug.Log ("Posição da Bola:  " + c.transform.position.ToString() + "Posição do Player1:  " + c.transform.position.ToString());
			//Último player a tocar passa a ser o Um (Esquerda)
			Toque = ultimoPlayer.PlayerOne;
			AjustandoAnguloDaBola (c.gameObject);

		} 

		//Caso colida com Player2
		else if(c.gameObject.tag == "Player2"){
			Debug.Log ("Colidindo com Player2");
			Debug.Log ("Posição da Bola:  " + c.transform.position.ToString() + "Posição do Player2:  " + c.transform.position.ToString());
			//Último player a tocar passa a ser o Dois (Direita)
			Toque = ultimoPlayer.PlayerTwo;
			AjustandoAnguloDaBola (c.gameObject);
		}
	}

	void MovimentoBola( Vector3 horizontalDir , Vector3 verticalDir , float veloBall , float difAngulo ){

		//Calcula a soma dos Vetores
		Angulo = ( horizontalDir * difAngulo + verticalDir ).normalized;
		//Adiciona a força na Bola
		ballRB.transform.Translate (Angulo *  veloBall * Time.deltaTime);
	}

	void CallMov(){

		//Se o último player a tocar a bola for o Primeiro, chamas com essas variaveis
		if(Toque == ultimoPlayer.PlayerOne){
			MovimentoBola( dirY, Vector3.right , speed , acertarAngulo );
		}

		//Se o último player a tocar a bola for o Segundo, chamas com essas variaveis
		else if(Toque == ultimoPlayer.PlayerTwo){
			MovimentoBola( dirY, Vector3.left , speed , acertarAngulo );
		}
	
	}

	void AjustandoAnguloDaBola(GameObject c){

		//Ajusta Angulo da bola de acordo com o local da colisão.
		if(transform.position.y == c.transform.position.y){
			acertarAngulo = Random.Range(0, 10);
			speed ++;
		} else if (transform.position.y >= c.transform.position.y + 0.5f && transform.position.y <= c.transform.position.y + 0.8f  || 
			transform.position.y <= c.transform.position.y - 0.5f && transform.position.y >= c.transform.position.y + 0.8f  ){
			acertarAngulo = 0.5f;
			speed = speed + 2f;;
		} else if (transform.position.y >= c.transform.position.y + 0.8f || transform.position.y <= c.transform.position.y - 0.8f){
			acertarAngulo = 1f;
			speed = speed + 5f;
		}

		//Ajusta a direção na vertical
		if(transform.position.y > c.transform.position.y){
			dirY = Vector3.up;
		} else if (transform.position.y < c.transform.position.y){
			dirY = Vector3.down;
		}
	}

	void BatendoNaParede(){

		//Batendo na Vertical
		if(transform.position.y > 5.6f ){
			dirY = Vector3.down;
			acertarAngulo = 1f;
		} else if(transform.position.y < -3.6f ){
			acertarAngulo = 1f;
			dirY = Vector3.up;
		}

		//Batendo na Horizontal
		if(transform.position.x > 10f ){
			speed = 10f;
			transform.position = new Vector3 (0f, 0f, 0f);
		} else if(transform.position.x < -10f ){
			speed = 10f;
			transform.position = new Vector3 (0f, 0f, 0f);
		}
	
	}
}
