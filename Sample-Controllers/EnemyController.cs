using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class EnemyController : MonoBehaviour {
	public GameObject[] AI = new GameObject[8];

	public GameObject fireArea1, fireArea2;

	public bool deathSoundPlayed = true;

	public bool xpAnimated = false;

	public GameObject xp1, xp2, xp3, xp4;

	private AudioSource audioSource;

	public AudioClip attackSound1, slice, miss, roar;

	public float timeSinceAwake;

	string MCClass = "";

	public int randDir;

	public GameObject rightPos, leftPos, backPos;

	public bool isLettingUp = false;

	public static bool pIslettingUp;

	public bool isRoaring = false;

	public bool shouldAttack = true;

	public bool attacking = false;

	//Represents if the enemy is in a state of being hit, or has just been hit to prevent extra damage or a repeated hit animation.
	bool justHit = false;
	bool hit = true;

	public bool dead = false;

	private Animator twoHandedKnightAnimations;

	//Represents the attack from the animator.
	public int attackType;

	public string[] attackTypes = new string[]{"attack1", "attack2", "attack3"};

	public string[] enemyAttackTypes = new string[]{"sword", "arrow", "grenade"};

	public float minDist;

	public float fightDistance;

	public bool fightMode;

	public float walkSpeed;

	public float attackingMoveSpeed = 6f;

	public float defaultSpeed;

	public float distFromMainCharacter;

	public float distFromBarrier;

	public float armour;

	public float health;

	public bool beingAttacked;

	public GameObject MainCharacter, Rogue, Hunter, BHunter, AI1, AI2, AI3, AI4, AI5, AI6, AI7, AI8;

	public GameObject EnemyBlade, EnemyHitbox;

	public bool ttt;

	float letUpInterval = 8f;
	bool letUp;

	void Awake(){
		audioSource = GetComponent<AudioSource>();
	}

	void OnEnable(){
		ttt = false;
		audioSource = GetComponent<AudioSource>();
		MCClass = "";
		deathSoundPlayed = true;
		xpAnimated = false;
		shouldAttack = true;		
		attacking = false;		
		justHit = false;
		hit = true;

		//TESTING IF LETTING UP CAUSES ISSUES
		letUpInterval = 800000f;
		isRoaring = false;
		timeSinceAwake = 0;
		isLettingUp = false;
		pIslettingUp = false;
		letUp = false;
		health = 30;
		dead = false;


		walkSpeed = defaultSpeed;
		attackType = Random.Range (0, 3);
		twoHandedKnightAnimations = gameObject.GetComponentInChildren<Animator> ();

		if (Application.loadedLevelName == "Infinite") { 
			AI [0] = AI1;
			AI [1] = AI2;
			AI [2] = AI3;
			AI [3] = AI4;
			AI [4] = AI5;
			AI [5] = AI6;
			AI [6] = AI7;
			AI [7] = AI8;
		}
	}

	// Use this for initialization
	void Start () {
		if (Application.loadedLevelName == "Terrain1" || Application.loadedLevelName == "Terrain4" || Application.loadedLevelName == "Terrain6" || Application.loadedLevelName == "Infinite") {
			xp1.gameObject.SetActive (true);
			xp2.gameObject.SetActive (false);
			xp3.gameObject.SetActive (false);
			xp4.gameObject.SetActive (false);
		}


		//if (this.gameObject.name == "overlord") {
		//	twoHandedKnightAnimations.Play("overlordequip");
		//}
	}
	
	// Update is called once per frame
	void Update () {

		if (Application.loadedLevelName == "Infinite") {
			//Enemy burns if they go near the fire
			if (Vector3.Distance (transform.position, fireArea1.transform.position) <= 7 && fireArea1.activeInHierarchy) {

				if (Time.timeScale==1){
					health -= 0.25f;
				}
			}	

			if (Vector3.Distance (transform.position, fireArea2.transform.position) <= 7 && fireArea2.activeInHierarchy) {
					if (Time.timeScale==1){
					health -= 0.25f;
				}
			}
		}

		if (Application.loadedLevelName == "Infinite") {
			//DEALS WITH GETTING HIT BY AI
			for (int i = 0; i < AI.Length; i++) {
				if (Vector3.Distance (transform.position, AI [i].transform.position) <= minDist && AI [i].gameObject.activeInHierarchy) {
					if (Time.timeScale==1){
						health -= 0.15f;
					}

				}
			}
		}

		pIslettingUp = isLettingUp;

		if (health < 0) {
			Debug.Log ("dead");
		}
	//	if (twoHandedKnightAnimations.GetCurrentAnimatorStateInfo (0).IsName ("standing_taunt_battlecry 0")) {

	//	audioSource.clip = roar;
	//		audioSource.Play ();
	//	}

		timeSinceAwake += Time.deltaTime;
		//Debug.Log (letUpInterval);

		if (Rogue.gameObject.activeInHierarchy == true) {
			MCClass = "Rogue";
			MainCharacter = Rogue;
		} else if (BHunter.gameObject.activeInHierarchy == true) {
			MCClass = "Rogue";
			MainCharacter = BHunter;
		} else if (Hunter.gameObject.activeInHierarchy == true) {
			MCClass = "Rogue";
			MainCharacter = Hunter;
		}

		if (MCClass == "BHunter" || MCClass == "Hunter" ) {
			//twoHandedKnightAnimations.ResetTrigger ("Roar");
			//twoHandedKnightAnimations.ResetTrigger ("Return");
			twoHandedKnightAnimations.ResetTrigger ("LetUpRight");
			twoHandedKnightAnimations.ResetTrigger ("LetUpLeft");
			twoHandedKnightAnimations.ResetTrigger ("LetUpBack");

			if (!dead){
				//Always face the target.
				transform.LookAt (new Vector3 (MainCharacter.transform.position.x, transform.position.y, MainCharacter.transform.position.z));
			}

			//If the enemy gets hit within a range of the main character's hitbox, they take damage.
			if (Vector3.Distance (transform.position, EnemyHitbox.transform.position) <= minDist && EnemyHitbox.gameObject.activeInHierarchy == true && !justHit) {



				justHit = true;
				StartCoroutine (resetHit ());
				Debug.Log ("hit");
			}

			if (AttackBtnBehaviour.strongAttackSuccess == true && Vector3.Distance (transform.position, EnemyHitbox.transform.position) <= minDist && !letUp) {

				Handheld.Vibrate();

				Debug.Log ("Strong hit");
				twoHandedKnightAnimations.SetTrigger ("isHit");
				AttackBtnBehaviour.strongAttackSuccess = false;

				justHit = true;
				StartCoroutine (resetHit ());
			}
		
			if (timeSinceAwake > letUpInterval) {
				Debug.Log ("dfsdfsdfsdfsdfsdf");
				letUpInterval += 16;
				int rand = Random.Range (0, 4);
				if (rand > -1) {
					letUp = true;
					StartCoroutine (LetUp ());
				} 
			}

			//Enemy dies if health is depleted.
			if (health <= 0) {
				dead = true;
				twoHandedKnightAnimations.SetTrigger ("exitArmedAnim");
				StartCoroutine ("die");
			}

			if (letUp) {
				Player.MCtakeDamage = false;		
				shouldAttack = false;
				attacking = false;
			}


			if (!letUp) {

				shouldAttack = true;

				if (attacking == true) {
					attacking = false;
				}



				/*//Equip weapon if character is close.
			if (Vector3.Distance (transform.position, MainCharacter.transform.position) >= fightDistance && fightMode == false) {
				fightMode = true;
				twoHandedKnightAnimations.SetTrigger ("equip");

				//If character moves away, put weapon away.
			} else {
				fightMode = false;
				twoHandedKnightAnimations.SetTrigger ("unequip");
			}*/

				//if (!letUp) {
				//Move closer to MC if close enough
				if (Vector3.Distance (transform.position, MainCharacter.transform.position) >= minDist && !dead) {
					MoveTowardsCharacter ();
					Player.MCtakeDamage = false;
				} else if (ttt == false) {
					twoHandedKnightAnimations.SetTrigger ("goIdle");

					//if close enough, attack.
					if (shouldAttack) {
						shouldAttack = false;
						StartCoroutine (switchAttack ());
						shouldAttack = true;
					}
					ttt = true;
				}
				//}
			}

			if (isLettingUp && !dead) {
				if (randDir == 1) {
					twoHandedKnightAnimations.SetTrigger ("LetUpRight");
			
					float targetDirx = rightPos.gameObject.transform.position.x;
					float targetDirz = rightPos.gameObject.transform.position.z;
					Vector3 targetDir = new Vector3 (targetDirx, transform.position.y, targetDirz);
					float step = Time.deltaTime;
					Vector3 newDir = Vector3.RotateTowards (transform.forward, targetDir, step, 0.0F);
					transform.position = Vector3.MoveTowards (transform.position, targetDir, step);

				} else if (randDir == 2) {
					twoHandedKnightAnimations.SetTrigger ("LetUpLeft");

					float targetDirx = leftPos.gameObject.transform.position.x;
					float targetDirz = leftPos.gameObject.transform.position.z;
					Vector3 targetDir = new Vector3 (targetDirx, transform.position.y, targetDirz);
					float step = Time.deltaTime;
					Vector3 newDir = Vector3.RotateTowards (transform.forward, targetDir, step, 0.0F);
					transform.position = Vector3.MoveTowards (transform.position, targetDir, step);

				} else {
					twoHandedKnightAnimations.SetTrigger ("LetUpBack");

					float targetDirx = backPos.gameObject.transform.position.x;
					float targetDirz = backPos.gameObject.transform.position.z;
					Vector3 targetDir = new Vector3 (targetDirx, transform.position.y, targetDirz);
					float step = Time.deltaTime;
					Vector3 newDir = Vector3.RotateTowards (transform.forward, targetDir, step, 0.0F);
					transform.position = Vector3.MoveTowards (transform.position, targetDir, step);
				} 	
			}  else if (isRoaring) {
				
				//if(letUp == true){
				twoHandedKnightAnimations.SetTrigger ("Roar");
				
				//}
			} else if (!isRoaring) {
				twoHandedKnightAnimations.SetTrigger ("Return");
			} 
		} else if (MCClass == "Rogue") {
			//twoHandedKnightAnimations.ResetTrigger ("Roar");
			//twoHandedKnightAnimations.ResetTrigger ("Return");

			twoHandedKnightAnimations.ResetTrigger ("LetUpRight");
			twoHandedKnightAnimations.ResetTrigger ("LetUpLeft");
			twoHandedKnightAnimations.ResetTrigger ("LetUpBack");

			
			//Always face the target.
			transform.LookAt (new Vector3 (MainCharacter.transform.position.x, transform.position.y, MainCharacter.transform.position.z));
			//Debug.Log ("Distance to mc:" + Vector3.Distance (transform.position, EnemyHitbox.transform.position));
			//If the enemy gets hit within a range of the main character's hitbox, they take damage.
			if (Vector3.Distance (transform.position, EnemyHitbox.transform.position) <= minDist && AttackBtnBehaviour.isAttacking == true && !justHit) {
				
				//AttackBtnBehaviour.strongAttackSuccess = false;
				
				justHit = true;
				StartCoroutine (resetHit ());
				Debug.Log ("hit");
			}
						
			if (AttackBtnBehaviour.strongAttackSuccess == true && Vector3.Distance (transform.position, EnemyHitbox.transform.position) <= minDist && !justHit) {
				Debug.Log ("Strong hit");
				twoHandedKnightAnimations.SetTrigger ("isHit");
				AttackBtnBehaviour.strongAttackSuccess = false;
				
				justHit = true;
				StartCoroutine (resetHit ());
			}
			
			if (timeSinceAwake > letUpInterval) {
				letUpInterval += 16;
				int rand = Random.Range (1, 4);
				if (rand > 2) {
					letUp = true;
					StartCoroutine (LetUp ());
				} 
			}

			//Enemy dies if health is depleted.
			if (health <= 0) {
				dead = true;
				twoHandedKnightAnimations.SetTrigger ("exitArmedAnim");
				StartCoroutine ("die");
			}

			if (letUp) {
				Player.MCtakeDamage = false;		
				shouldAttack = false;
				attacking = false;
			}
			if (!letUp) {
				
				shouldAttack = true;
				
				if (attacking == true) {
					attacking = false;
				}
				
				//Enemy dies if health is depleted.
				if (health <= 0) {
					dead = true;
					twoHandedKnightAnimations.SetTrigger ("exitArmedAnim");
					StartCoroutine ("die");
				}
				
				/*//Equip weapon if character is close.
				if (Vector3.Distance (transform.position, MainCharacter.transform.position) >= fightDistance && fightMode == false) {
					fightMode = true;
					twoHandedKnightAnimations.SetTrigger ("equip");
					
					//If character moves away, put weapon away.
				} else {
					fightMode = false;
					twoHandedKnightAnimations.SetTrigger ("unequip");
				}*/
					
					//if (!letUp) {
					//Move closer to MC if close enough
				if (Vector3.Distance (transform.position, MainCharacter.transform.position) >= minDist && !dead) {
					MoveTowardsCharacter ();
					Player.MCtakeDamage = false;
				} else if (ttt == false) {
					twoHandedKnightAnimations.SetTrigger ("goIdle");
					
					//if close enough, attack.
					if (shouldAttack) {
						shouldAttack = false;
						StartCoroutine (switchAttack ());
						shouldAttack = true;
					}
					ttt = true;
				}
				//}
			}
			
			if (isLettingUp && !dead) {
				if (randDir == 1) {
					twoHandedKnightAnimations.SetTrigger ("LetUpRight");
					
					float targetDirx = rightPos.gameObject.transform.position.x;
					float targetDirz = rightPos.gameObject.transform.position.z;
					Vector3 targetDir = new Vector3 (targetDirx, transform.position.y, targetDirz);
					float step = Time.deltaTime;
					Vector3 newDir = Vector3.RotateTowards (transform.forward, targetDir, step, 0.0F);
					transform.position = Vector3.MoveTowards (transform.position, targetDir, step);
					
				} else if (randDir == 2) {
					twoHandedKnightAnimations.SetTrigger ("LetUpLeft");
					
					float targetDirx = leftPos.gameObject.transform.position.x;
					float targetDirz = leftPos.gameObject.transform.position.z;
					Vector3 targetDir = new Vector3 (targetDirx, transform.position.y, targetDirz);
					float step = Time.deltaTime;
					Vector3 newDir = Vector3.RotateTowards (transform.forward, targetDir, step, 0.0F);
					transform.position = Vector3.MoveTowards (transform.position, targetDir, step);
					
				} else {
					twoHandedKnightAnimations.SetTrigger ("LetUpBack");
					
					float targetDirx = backPos.gameObject.transform.position.x;
					float targetDirz = backPos.gameObject.transform.position.z;
					Vector3 targetDir = new Vector3 (targetDirx, transform.position.y, targetDirz);
					float step = Time.deltaTime;
					Vector3 newDir = Vector3.RotateTowards (transform.forward, targetDir, step, 0.0F);
					transform.position = Vector3.MoveTowards (transform.position, targetDir, step);
				} 	
			} else if (isRoaring) {
				
				//if(letUp == true){
				twoHandedKnightAnimations.SetTrigger ("Roar");
				
				//}
			} else if (!isRoaring) {
				twoHandedKnightAnimations.SetTrigger ("Return");
			} 
		}
	}

	void MoveTowardsCharacter(){

		if (!isLettingUp || !letUp) {
			//Moves towards main character.
			float targetDirx = MainCharacter.gameObject.transform.position.x;
			float targetDirz = MainCharacter.gameObject.transform.position.z;
			Vector3 targetDir = new Vector3 (targetDirx, transform.position.y, targetDirz);
			float step = walkSpeed * Time.deltaTime;
			Vector3 newDir = Vector3.RotateTowards (transform.forward, targetDir, step, 0.0F);
			transform.position = Vector3.MoveTowards (transform.position, targetDir, step);

			twoHandedKnightAnimations.SetTrigger ("run");
		}
	}

	//Randomizes attack type;
	IEnumerator switchAttack(){
		walkSpeed = attackingMoveSpeed;
		
		twoHandedKnightAnimations.SetTrigger (attackTypes [attackType]);
		
		int randSound = Random.Range (0, 100);

		if (!(randSound < 50)) {
			audioSource.clip = attackSound1;
			audioSource.Play ();
		}

		yield return new WaitForSeconds(0.7f);

		if (randSound < 50) {
			audioSource.clip = miss;
			audioSource.Play ();
		} 

		if( !twoHandedKnightAnimations.GetCurrentAnimatorStateInfo(0).IsName("standing_react_large_gut")&& !dead/* && !justHit*/)
		{

			Player.MCtakeDamage = true;
			Debug.Log ("MCTAKEDAMEAGE " + Player.MCtakeDamage);

		}
		yield return new WaitForSeconds(1.75f);
		attackType = Random.Range (0, 3);
		ttt = false;
		twoHandedKnightAnimations.SetTrigger ("exitArmedAnim");

		
		walkSpeed = defaultSpeed;
		
	}

	void startAttack(){
	}

	//resets the state of being hit
	IEnumerator resetHit(){
		Debug.Log ("Damage: " + WarriorController.MCDamage);
		if (health > 0) {
			Player.multiplier += 1;
		}
		health -= WarriorController.MCDamage;
		audioSource.clip = slice;
		if (health > 0) {
			audioSource.Play ();
		}
		yield return new WaitForSeconds(0.5f);
		justHit = false;
	}

	void animateMultiplier(){
		if (!xpAnimated){
			if (Player.multiplier == 1) {
				xp1.gameObject.SetActive (true);
				xpAnimated = true;
			} else if (Player.multiplier == 2) {
				xp2.gameObject.SetActive (true);
				xpAnimated = true;
			} else if (Player.multiplier == 3) {
				xp3.gameObject.SetActive (true);
				xpAnimated = true;
			} else if (Player.multiplier == 4) {
				xp4.gameObject.SetActive (true);
				xpAnimated = true;
			}
		}
	}

	IEnumerator die(){
		animateMultiplier ();

		if (deathSoundPlayed == false) {
			audioSource.clip = roar;
			audioSource.Play ();
		}
		twoHandedKnightAnimations.SetTrigger ("die");
		yield return new WaitForSeconds(4f);
		justHit = false;
		Player.killCntr += 1;
		this.gameObject.SetActive(false);
	}

	IEnumerator LetUp(){
	//	if (health < 1) {
		//	twoHandedKnightAnimations.SetTrigger ("die");
	//	}
		isLettingUp = true;

		randDir = Random.Range (1, 4);

		StartCoroutine (RoarSound ());

		yield return new WaitForSeconds(2f);		
		isLettingUp = false;
		isRoaring = true;
		if (timeSinceAwake < 16) {	
			audioSource.clip = roar;
			audioSource.Play ();
		}
		//twoHandedKnightAnimations.SetTrigger ("Roar");
		yield return new WaitForSeconds(2f);
	//	if (health < 1) {
		//	twoHandedKnightAnimations.SetTrigger ("die");
		//}
		//twoHandedKnightAnimations.SetTrigger ("Return");
		isRoaring = false;
		yield return new WaitForSeconds(2f);
		//if (health < 1) {
		//	twoHandedKnightAnimations.SetTrigger ("die");
		//}

		letUp = false;
	}

	IEnumerator RoarSound(){
		if (!dead){
			if (timeSinceAwake > 16) {
				yield return new WaitForSeconds (1f);	
				audioSource.clip = roar;
				audioSource.Play ();
			}
		}
	}

	void OnDisable(){
		Player.points += Player.multiplier * 10;
	}

	//IEnumerator RegularHit(){		
	//	yield return new WaitForSeconds (.25f);
	//	audioSource.clip = slice;
	//	audioSource.Play ();
	//}
}


