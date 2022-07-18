using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class iaAgent : MonoBehaviour {
  //TODO: FALTA HACER LA PARTDE DE EJERCICIO   
	public Transform Player;
  public transfomr Player2;
	Animator anim;
	private NavMeshAgent naveMesh;
	private float DistanciaDePlayer, DistanciaDeAIPoint, DistanciaDePlayer2;
	public float DistanciaDePercepcion = 20, DistanciaDeBaile = 8, VelocidadDePaseo = 3;
	private bool ViendoPlayer;
	public Transform[] DestinosAleatorios;
	private int AIPointActual; 
	private bool PersiguiendoAlgo, bailando;
	private float cronometroDePersecucion, cronometroAtaque;

	void Start (){
		AIPointActual = 0;
		naveMesh = GetComponent<NavMeshAgent> ();
		anim = GetComponent<Animator>();
	}

	void Update (){
		anim.SetBool("isWalking",true);
		DistanciaDePlayer = Vector3.Distance(Player.transform.position,transform.position);
    DistanciaDePlayer2 = Vector3.Distance(Player2.transform.position,transform.position);
		DistanciaDeAIPoint =  Vector3.Distance(DestinosAleatorios[AIPointActual].transform.position,transform.position);
		//============================== RAYCAST ===================================//
		RaycastHit hit;
		Vector3 deOnde = transform.position;
		Vector3 paraOnde = Player.transform.position;
		Vector3 direction = paraOnde - deOnde;
		if(Physics.Raycast (transform.position,direction,out hit,1000) && DistanciaDePlayer < DistanciaDePercepcion){
			if(hit.collider.gameObject.CompareTag("Player")){
				ViendoPlayer = true;
				anim.SetBool("isIdle",true);
			}else{
				ViendoPlayer = false;
			}
		}
		//================ Deciciones ================//
		if(DistanciaDePlayer > DistanciaDePercepcion){
			anim.SetBool("isWalking",true);
			Pasear();
		}
		//if (DistanciaDePlayer <= DistanciaDePercepcion && DistanciaDePlayer > DistanciaDeSeguir) {
		if (DistanciaDePlayer <= DistanciaDePercepcion && DistanciaDePlayer>DistanciaDeBaile) {
			if(ViendoPlayer == true){
				anim.SetBool("isWalking",false);
				anim.SetBool("isIdle",true);
				Mirar ();
			}
			else{
				anim.SetBool("isWalking",true);
				anim.SetBool("isIdle",false);
				Pasear();
			}
		}
		if(DistanciaDePlayer<=DistanciaDeBaile){
			if(ViendoPlayer==true){
				anim.SetBool("isDance", true);
				Bailar();
			}else{
				anim.SetBool("isDance",false);
				anim.SetBool("isWalking",true);
				Pasear();
			}
		}
		//COMANDOS DE PASEAR
		// if (DistanciaDeAIPoint <= 1) {
		// 	AIPointAtual = Random.Range (0, DestinosAleatorios.Length);
		// 	Pasear();
		// }
    if(DistanciaDeAIPoint < 2){	
			AIPointActual++;
			if(AIPointActual >= waypoints.Length)
			{
				AIPointActual = 0;
		  }
      Pasear();	
		}
	}

	void Pasear (){
		if(bailando==false){
			naveMesh.acceleration = 5;
			naveMesh.speed = VelocidadDePaseo;
			naveMesh.destination = DestinosAleatorios[AIPointActual].position;
		}else{
			bailando=false;
		}
	}

	void Mirar(){
		naveMesh.speed = 0;
		transform.LookAt(Player);
	}

	void Bailar(){
		naveMesh.speed = 0;
		bailando = true;
	}
}