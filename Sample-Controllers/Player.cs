using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public static int respawns = 3;
	public GameObject deathMenu, infiniteGame;
	public GameObject fire1, fire2, fire3, fire4, bodyFire;
	public GameObject scoreboard, pointsDisplay, kills;
	public static int killCntr = 0;
	public static bool dead = false;
	public static int multiplier = 1, points;

	public bool hasPlayedDieSound = false;

	public bool swingSoundPlayed = false;

	private AudioSource audioSource;
	
	public AudioClip slice, miss, grunt, dieSound;

	public static bool MCtakeDamage = false;

	public static bool MCtakeDamageFromBehemoth = false;

	public GameObject health1, health2, health3, health4, health5;

	public bool justHit, hit = false;

	public static int health = 30000;

	Rigidbody rb;

	private Animator mainCharacterAnimations;

	//Represents an invisible object that covers the sword used for collision detection with enemies.
	public GameObject /*swordCollider,*/ hitbox, behemoth/*EnemyHitbox*/;

	void Awake(){
		audioSource = GetComponent<AudioSource>();
	}

	void Start () {
		doNotPurchase.quit = false;

		killCntr = 0;
		dead = false;
		multiplier = 1;
		hasPlayedDieSound = false;
		swingSoundPlayed = false;
		justHit = false;
		hit = false;
		MCtakeDamage = false;
		MCtakeDamageFromBehemoth = false;
		health = 120;
		mainCharacterAnimations = gameObject.GetComponentInChildren<Animator> ();
		rb = this.GetComponent<Rigidbody>(); 
		health += WarriorController.MCArmor;
	}
	
	// Update is called once per frame
	void Update () {
		//Restore health
		if (health >= 10){
			health1.gameObject.SetActive(true);
			health2.gameObject.SetActive(true);
			health3.gameObject.SetActive(true);
			health4.gameObject.SetActive(true);
			health5.gameObject.SetActive(true);
		}

		// Die if beast runs into you
		if (Application.loadedLevelName == "Terrain6") {
			if (Vector3.Distance (transform.position, behemoth.transform.position) <= 4) {
				if (BehemothBossController.charging && !BehemothBossController.dead) {
					health -= health;
				}
			}

			if (Vector3.Distance (transform.position, fire1.transform.position) <= 3 || Vector3.Distance (transform.position, fire2.transform.position) <= 3 || Vector3.Distance (transform.position, fire3.transform.position) <= 3 || Vector3.Distance (transform.position, fire4.transform.position) <= 3) {
				StartCoroutine (resetHit ());
				bodyFire.gameObject.SetActive(true);
			}

		} else if (Application.loadedLevelName == "Terrain1" && Time.timeSinceLevelLoad >= 240) {
			DPLoadScreen.Instance.LoadLevel("levelOneEnd", "LoadScreen");
		}

		//Set to false by default.
		mainCharacterAnimations.ResetTrigger("exitStrongAttack");

		//Exit the strong attack.
		if (AttackBtnBehaviour.exitArmedAnim == true) {
			mainCharacterAnimations.SetTrigger ("exitStrongAttack");
			AttackBtnBehaviour.exitArmedAnim = false;
		}

		//Die.
		if (health <= 0) {
			/*if (deathMenu.activeInHierarchy){
				Time.timeScale = 0.00001f;
			} else {
				Time.timeScale = 1f;
			}*/
			if(Application.loadedLevelName == "Infinite" && !doNotPurchase.quit && respawns > 0){
				deathMenu.gameObject.SetActive(true);
				infiniteGame.gameObject.SetActive(false);

			} else{
				mainCharacterAnimations.SetTrigger ("die");
				dead = true;
				if (!hasPlayedDieSound){
					audioSource.clip = dieSound;
					audioSource.Play ();
					hasPlayedDieSound = true;
				}


				StartCoroutine(LoadPreviousScene());
			}
		}

		//If the enemy gets hits the player, they take damage.
		if (/*Vector3.Distance (transform.position, EnemyHitbox.transform.position) <= 4 &&*/ MCtakeDamage == true && !justHit) {
			Debug.Log ("SHOULD "+ MCtakeDamage);
			justHit = true;
			hit=false;
			MCtakeDamage = false;
			Debug.Log ("should take damamge");
			StartCoroutine(resetHit());

		}

		//If a behemoth gets hits the player, they take lots of damage.
		if (/*Vector3.Distance (transform.position, EnemyHitbox.transform.position) <= 4 &&*/ MCtakeDamageFromBehemoth == true && !justHit) {

			if (BehemothController.attackType == 0){
				justHit = true;
				hit=false;
				MCtakeDamageFromBehemoth = false;
				StartCoroutine(resetHitFromBehemoth());
				StartCoroutine(resetHitFromBehemoth());

			} else if (BehemothController.attackType == 1){
				justHit = true;
				hit=false;
				MCtakeDamageFromBehemoth = false;
				StartCoroutine(resetHitFromBehemothAttackTwo());
			}

		}

		//Stops unwanted crouches and flying.
		rb.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezePositionY;

		//If the main character is attacking, the attack animation is triggered. The same happens for the ther buttons.
		if (AttackBtnBehaviour.isAttacking == true && !dead) {

			mainCharacterAnimations.Play("Attack");

			//The sword's collider only becomes active when the player attacks to prevent dealing damage when not attacking.
			hitbox.gameObject.SetActive (true);

			//After a short time, the collider becomes inactive so ony successful swings deal damage.
			StartCoroutine(DisableSwordCollider());
		} 

		if (AttackBtnBehaviour.isStrongAttacking == true && !dead) {
			
			mainCharacterAnimations.Play ("standing_melee_attack_360_high");			

			
			//After a short time, the collider becomes inactive so ony successful swings deal damage.
			StartCoroutine(DisableSwordCollider());
		} 

		if (AttackBtnBehaviour.strongAttackSuccess == true) {

			//The sword's collider only becomes active when the player attacks to prevent dealing damage when not attacking.
			hitbox.gameObject.SetActive (true);
		}
	}

	//Disables the hitbox and sword collider
	IEnumerator DisableSwordCollider() {		

		if (swingSoundPlayed == false) {
			StartCoroutine(playSwingSound());
			swingSoundPlayed = true;
		}

		yield return new WaitForSeconds(0.5f);
		//swordCollider.gameObject.SetActive (false);
		hitbox.gameObject.SetActive (false);
		AttackBtnBehaviour.isAttacking = false;
	}

	//resets the state of being hit
	IEnumerator resetHit(){
		//if (advancedEnemyController.gateTarget == false) {
		Debug.Log ("helllo");
			multiplier = 1;
			int rand = Random.Range (0, 100);
			if (rand < 50) {
				audioSource.clip = slice;
				audioSource.Play ();
			} else {
				audioSource.clip = grunt;
				audioSource.Play ();
			}
			yield return new WaitForSeconds (0f);

			health -= 2;
			Debug.Log ("health" + health);
			if (health < 10) {
				
				if (health5.gameObject.activeInHierarchy == true) {
					health5.gameObject.SetActive (false);
				} else if (health4.gameObject.activeInHierarchy == true) {
					health4.gameObject.SetActive (false);
				} else if (health3.gameObject.activeInHierarchy == true) {
					health3.gameObject.SetActive (false);
				} else if (health2.gameObject.activeInHierarchy == true) {
					health2.gameObject.SetActive (false);
				} else if (health1.gameObject.activeInHierarchy == true) {
					health1.gameObject.SetActive (false);
			}
		}
			justHit = false;
		//}
	}

	IEnumerator resetHitFromBehemoth(){

		audioSource.clip = grunt;
		audioSource.Play ();
		
		yield return new WaitForSeconds(0f);
		
		health -= 2;

		//Added march 9
		if (health < 10) {
			if (health5.gameObject.activeInHierarchy == true) {
				health5.gameObject.SetActive (false);
			} else if (health4.gameObject.activeInHierarchy == true) {
				health4.gameObject.SetActive (false);
			} else if (health3.gameObject.activeInHierarchy == true) {
				health3.gameObject.SetActive (false);
			} else if (health2.gameObject.activeInHierarchy == true) {
				health2.gameObject.SetActive (false);
			} else if (health1.gameObject.activeInHierarchy == true) {
				health1.gameObject.SetActive (false);
			}
		}
		justHit = false;
	}

	IEnumerator resetHitFromBehemothAttackTwo(){
		
		audioSource.clip = grunt;
		audioSource.Play ();
		
		yield return new WaitForSeconds(0f);
		
		health -= 2;

		//Added march 9
		if (health < 10) {
			if (health5.gameObject.activeInHierarchy == true) {
				health5.gameObject.SetActive (false);
			} else if (health4.gameObject.activeInHierarchy == true) {
				health4.gameObject.SetActive (false);
			} else if (health3.gameObject.activeInHierarchy == true) {
				health3.gameObject.SetActive (false);
			} else if (health2.gameObject.activeInHierarchy == true) {
				health2.gameObject.SetActive (false);
			} else if (health1.gameObject.activeInHierarchy == true) {
				health1.gameObject.SetActive (false);
			}
		}

		yield return new WaitForSeconds(0.5f);

		health -= 2;

		//Added march 9
		if (health < 10) {
			if (health5.gameObject.activeInHierarchy == true) {
				health5.gameObject.SetActive (false);
			} else if (health4.gameObject.activeInHierarchy == true) {
				health4.gameObject.SetActive (false);
			} else if (health3.gameObject.activeInHierarchy == true) {
				health3.gameObject.SetActive (false);
			} else if (health2.gameObject.activeInHierarchy == true) {
				health2.gameObject.SetActive (false);
			} else if (health1.gameObject.activeInHierarchy == true) {
				health1.gameObject.SetActive (false);
			}
		}

		justHit = false;
	}

	IEnumerator playSwingSound(){
		audioSource.clip = miss;
		audioSource.Play ();
		yield return new WaitForSeconds(.9f);
		swingSoundPlayed =false;
	}

	IEnumerator LoadPreviousScene(){
		/*scoreboard.gameObject.SetActive (true);
		yield return new WaitForSeconds (4f);
		pointsDisplay.gameObject.SetActive (true);
		yield return new WaitForSeconds (2f);
		kills.gameObject.SetActive (true);*/
		yield return new WaitForSeconds (2f);

		if (health <= 0) {
			DPLoadScreen.Instance.LoadLevel ("Blacksmith's Forge", "LoadScreen");
		}
	}


}

