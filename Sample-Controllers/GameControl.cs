using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
/*LOGICALLY BUGHT AND UNLOCKED ITEMS DO EACH OTHERS JOB AND ARE SWITCHED*/
public class GameControl : MonoBehaviour {

	public bool achievement1unlocked, achievement2unlocked,achievement3unlocked,achievement4unlocked, achievement5unlocked = false;

	//The PlayerData class stored in a json
	public static string GameStateJson;

	private PlayerData NewGameState, GameState;

	public static bool needToSaveLevelProgression = false;

	bool weaponSaved;
	GameObject man, woman;

	public string gender;
	public static string weaponName;

	public bool poisonUnlocked = false;
	public bool armorUnlocked = false;

	public int currentStageAchieved = 1;
	public int hunterPoints = 0;
	public int hunterLevel = 0;
	public int hunterDamage = 10;
	public int hunterHealth = 10;
	public int hunterArmor = 10;
	public string hunterName = "";
	public string hunterClass = "";			
	public bool kaiigranUnlocked = false;
	public int kaiigranPoints = 0;
	public int kaiigranLevel = 0;
	public int kaiigranDamage = 1;
	public int kaiigranHealth = 10;
	public int kaiigranArmor = 5;
	public string kaiigranName = "";
	public string kaiigranClass = "";			
	public bool BountyHUnlocked = false;
	public int BountyHPoints = 0;
	public int BountyHLevel = 0;
	public int BountyHDamage = 15;
	public int BountyHHealth = 10;
	public int BountyHArmor = 20;
	public string BountyHName = "";
	public string BountyHClass = "";			
	public int MagePoints = 0;
	public int MageLevel = 0;
	public int MageDamage = 10;
	public int MageHealth = 10;
	public int MageArmor = 5;
	public string MageName = "";			
	public bool woodShieldUnlocked = false;
	public bool ironShieldUnlocked = false;
	public bool carbonShieldUnlocked = false;			
	public bool simleSwordUnlocked = false;
	public bool doubleAxeUnlocked = false;
	public bool doubleAxeGlowUnlocked = false;
	public bool elvenSwordUnlocked = false;
	public bool angelicSwordUnlocked = false;			
	public bool woodShieldBought = false;
	public bool ironShieldBought = false;
	public bool carbonShieldBought = false;			
	public bool simleSwordBought = false;
	public bool doubleAxeBought = false;
	public bool doubleAxeGlowBought = false;
	public bool elvenSwordBought = false;
	public  bool angelicSwordBought = false;			
	public bool powerfulStrikesUnlocked = false;
	public bool juggernautUnlocked = false;
	public bool LowBlowUnlocked = false;
	public bool athleticUnlocked = false;
	public bool brutUnlocked = false;
	public bool beastUnlocked = false;
	public bool battleMasterUnlocked = false;
	public bool lethalBlowUnlocked = false;
	public bool ArmsmanUnlocked = false;			
	public bool powerfulStrikesBought = false;
	public bool juggernautBought = false;
	public bool LowBlowBought = false;
	public bool athleticBought = false;
	public bool bruteBought = false;
	public bool beastBought = false;
	public bool battleMasterBought = false;
	public bool lethalBlowBought = false;
	public bool ArmsmanBought = false;			
	public bool halberd1Unlocked = false;
	public bool halberd2Unlocked = false;
	public bool halberd3Unlocked = false;
	public bool skullHammerUnlocked = false;
	public bool darkSwordUnlocked = false;
	public bool hammerUnlocked = false;
	public bool maceUnlocked = false;
	public bool darkAxeUnlocked = false;
	public bool theNightmareUnlocked = false;			
	public bool halberd1Bought = false;
	public bool halberd2Bought = false;
	public bool halberd3Bought = false;
	public bool skullHammerBought = false;
	public bool darkSwordBought = false;
	public bool hammerBought = false;
	public bool maceBought = false;
	public bool darkAxeBought = false;
	public bool theNightmareBought = false;			
	public bool crossBowUnlocked = false;
	public bool elvenBowUnlocked = false;
	public bool bow1Unlocked = false;
	public bool bow2Unlocked = false;
	public bool carbonArrowPreview = false;
	public bool ironArrowUnlocked = false;
	public bool poisonArrowUnlocked = false;
	public bool poisonCarbonArrowUnlocked = false;
	public bool woodArrowUnlocked = false;			
	public bool crossBowBought = false;
	public bool elvenBowBought = false;
	public bool bow1Bought = false;
	public bool bow2Bought = false;
	public bool carbonArrowBought = false;
	public bool ironArrowBought = false;
	public bool poisonArrowBought = false;
	public bool poisonCarbonArrowBought = false;
	public bool woodArrowBought = false;			
	public bool blueMagicUnlocked = false;
	public bool electricMagicUnlocked = false;
	public bool fireMagicUnlocked = false;
	public bool fireMagic2Unlocked = false;
	public bool lightMagicUnlocked = false;
	public bool fireMistMagicUnlocked = false;			
	public bool blueMagicBought = false;
	public bool electricMagicBought = false;
	public bool fireMagicBought = false;
	public bool fireMagic2Bought = false;
	public bool lightMagicBought = false;
	public bool fireMistMagicBought = false;			
	public bool skilledHandsUnlocked = false;
	public bool rogueUnlocked = false;
	public bool crippleUnlocked = false;
	public bool hawkEyeUnlocked = false;
	public bool eagleEyeUnlocked = false;
	public bool powerfulShotUnlocked = false;
	public bool instantKillUnlocked = false;			
	public bool skilledHandsBought = false;
	public bool rogueBought = false;
	public bool crippleBought = false;
	public bool hawkEyeBought = false;
	public bool eagleEyeBought = false;
	public bool powerfulShotBought = false;
	public bool instantKillBought = false;			
	public bool flameUnlocked = false;
	public bool noviceUnlocked = false;
	public bool frostUnlocked = false;
	public bool exploitUnlocked = false;
	public bool fireUnlocked = false;
	public bool snowUnlocked = false;
	public bool incinerateUnlocked = false;
	public bool spellmasterUnlocked = false;
	public bool freezeUnlocked = false;
	public bool flameBought = false;
	public bool noviceBought = false;
	public bool frostBought = false;
	public bool exploitBought = false;
	public bool fireBought = false;
	public bool snowBought = false;
	public bool incinerateBought = false;
	public bool spellmasterBought = false;
	public bool freezeBought = false;

	public static bool parmorUnlocked = false;
	public static bool ppoisonUnlocked = false;

	public static int pcurrentStageAchieved = 1;
	public static int phunterPoints = 0;
	public static int phunterLevel = 0;
	public static int phunterDamage = 10;
	public static int phunterHealth = 10;
	public static int phunterArmor = 10;
	public static string phunterName = "";
	public static string phunterClass = "";			
	public static bool pkaiigranUnlocked = false;
	public static int pkaiigranPoints = 0;
	public static int pkaiigranLevel = 0;
	public static int pkaiigranDamage = 1;
	public static int pkaiigranHealth = 10;
	public static int pkaiigranArmor = 5;
	public static string pkaiigranName = "";
	public static string pkaiigranClass = "";			
	public static bool pBountyHUnlocked = false;
	public static int pBountyHPoints = 0;
	public static int pBountyHLevel = 0;
	public static int pBountyHDamage = 15;
	public static int pBountyHHealth = 10;
	public static int pBountyHArmor = 20;
	public static string pBountyHName = "";
	public static string pBountyHClass = "";			
	public static int pMagePoints = 0;
	public static int pMageLevel = 0;
	public static int pMageDamage = 10;
	public static int pMageHealth = 10;
	public static int pMageArmor = 5;
	public static string pMageName = "";			
	public static bool pwoodShieldUnlocked = false;
	public static bool pironShieldUnlocked = false;
	public static bool pcarbonShieldUnlocked = false;			
	public static bool psimleSwordUnlocked = false;
	public static bool pdoubleAxeUnlocked = false;
	public static bool pdoubleAxeGlowUnlocked = false;
	public static bool pelvenSwordUnlocked = false;
	public static bool pangelicSwordUnlocked = false;			
	public static bool pwoodShieldBought = false;
	public static bool pironShieldBought = false;
	public static bool pcarbonShieldBought = false;			
	public static bool psimleSwordBought = false;
	public static bool pdoubleAxeBought = false;
	public static bool pdoubleAxeGlowBought = false;
	public static bool pelvenSwordBought = false;
	public  static bool pangelicSwordBought = false;			
	public static bool ppowerfulStrikesUnlocked = false;
	public static bool pjuggernautUnlocked = false;
	public static bool pLowBlowUnlocked = false;
	public static bool pathleticUnlocked = false;
	public static bool pbrutUnlocked = false;
	public static bool pbeastUnlocked = false;
	public static bool pbattleMasterUnlocked = false;
	public static bool plethalBlowUnlocked = false;
	public static bool pArmsmanUnlocked = false;			
	public static bool ppowerfulStrikesBought = false;
	public static bool pjuggernautBought = false;
	public static bool pLowBlowBought = false;
	public static bool pathleticBought = false;
	public static bool pbruteBought = false;
	public static bool pbeastBought = false;
	public static bool pbattleMasterBought = false;
	public static bool plethalBlowBought = false;
	public static bool pArmsmanBought = false;			
	public static bool phalberd1Unlocked = false;
	public static bool phalberd2Unlocked = false;
	public static bool phalberd3Unlocked = false;
	public static bool pskullHammerUnlocked = false;
	public static bool pdarkSwordUnlocked = false;
	public static bool phammerUnlocked = false;
	public static bool pmaceUnlocked = false;
	public static bool pdarkAxeUnlocked = false;
	public static bool ptheNightmareUnlocked = false;			
	public static bool phalberd1Bought = false;
	public static bool phalberd2Bought = false;
	public static bool phalberd3Bought = false;
	public static bool pskullHammerBought = false;
	public static bool pdarkSwordBought = false;
	public static bool phammerBought = false;
	public static bool pmaceBought = false;
	public static bool pdarkAxeBought = false;
	public static bool ptheNightmareBought = false;			
	public static bool pcrossBowUnlocked = false;
	public static bool pelvenBowUnlocked = false;
	public static bool pbow1Unlocked = false;
	public static bool pbow2Unlocked = false;
	public static bool pcarbonArrowPreview = false;
	public static bool pironArrowUnlocked = false;
	public static bool ppoisonArrowUnlocked = false;
	public static bool ppoisonCarbonArrowUnlocked = false;
	public static bool pwoodArrowUnlocked = false;			
	public static bool pcrossBowBought = false;
	public static bool pelvenBowBought = false;
	public static bool pbow1Bought = false;
	public static bool pbow2Bought = false;
	public static bool pcarbonArrowBought = false;
	public static bool pironArrowBought = false;
	public static bool ppoisonArrowBought = false;
	public static bool ppoisonCarbonArrowBought = false;
	public static bool pwoodArrowBought = false;			
	public static bool pblueMagicUnlocked = false;
	public static bool pelectricMagicUnlocked = false;
	public static bool pfireMagicUnlocked = false;
	public static bool pfireMagic2Unlocked = false;
	public static bool plightMagicUnlocked = false;
	public static bool pfireMistMagicUnlocked = false;			
	public static bool pblueMagicBought = false;
	public static bool pelectricMagicBought = false;
	public static bool pfireMagicBought = false;
	public static bool pfireMagic2Bought = false;
	public static bool plightMagicBought = false;
	public static bool pfireMistMagicBought = false;			
	public static bool pskilledHandsUnlocked = false;
	public static bool progueUnlocked = false;
	public static bool pcrippleUnlocked = false;
	public static bool phawkEyeUnlocked = false;
	public static bool peagleEyeUnlocked = false;
	public static bool ppowerfulShotUnlocked = false;
	public static bool pinstantKillUnlocked = false;			
	public static bool pskilledHandsBought = false;
	public static bool progueBought = false;
	public static bool pcrippleBought = false;
	public static bool phawkEyeBought = false;
	public static bool peagleEyeBought = false;
	public static bool ppowerfulShotBought = false;
	public static bool pinstantKillBought = false;			
	public static bool pflameUnlocked = false;
	public static bool pnoviceUnlocked = false;
	public static bool pfrostUnlocked = false;
	public static bool pexploitUnlocked = false;
	public static bool pfireUnlocked = false;
	public static bool psnowUnlocked = false;
	public static bool pincinerateUnlocked = false;
	public static bool pspellmasterUnlocked = false;
	public static bool pfreezeUnlocked = false;
	public static bool pflameBought = false;
	public static bool pnoviceBought = false;
	public static bool pfrostBought = false;
	public static bool pexploitBought = false;
	public static bool pfireBought = false;
	public static bool psnowBought = false;
	public static bool pincinerateBought = false;
	public static bool pspellmasterBought = false;
	public static bool pfreezeBought = false;

	void Awake(){
		DontDestroyOnLoad (gameObject);

		Load ();
	}

	// Use this for initialization
	void Start () {



		//TEMP
		//PlayerData GameState = new PlayerData();
		//GameStateJson = JsonUtility.ToJson(GameState);
		//GooglePlayerUtilities.setGameStateForSaving = false;

		weaponSaved = false;

		UpdateGlobalVariables ();
	}


	// Update is called once per frame
	void Update () {


		//Update the json game state for google saves
		if (GooglePlayerUtilities.setGameStateForSaving) {
			GameState = new PlayerData();
			save ();
		}

		//If a new json game state is loaded, set the current game state to it (load it)
		if (SavedGamesExample.jsonLoaded) {

			NewGameState = new PlayerData();
			NewGameState = JsonUtility.FromJson<PlayerData>(SavedGamesExample.GameStateJsonLoaded);

			Load ();
		}

		if (Application.loadedLevelName == "Terrain6" && BehemothBossController.shouldSave){
			save ();
			BehemothBossController.shouldSave = false;
		} 


		if (Time.timeSinceLevelLoad <= 3) {
			//if (Application.loadedLevelName == "levelOneEnd");{
			currentStageAchieved = pcurrentStageAchieved;
			save ();
			//}


			//Man and woman charcters are searched for.
			man = GameObject.Find ("BHunter");
			woman = GameObject.Find ("Hunter");

			//Data is loaded when the menu starts up.
			if (Application.loadedLevelName == "Blacksmith's Forge") {
				if(Time.timeSinceLevelLoad < 3){
					Load ();
				}

				if (!showPhunterPoints.addedPoints){
					showPhunterPoints.addedPoints = true;
					phunterPoints += Player.points;
					Player.points = 0;

				}
				hunterPoints = phunterPoints;
				save();
				phunterPoints = hunterPoints;

			}

			weaponName = continueToMission.chosenWeapon;

			//The non chosen gender is deactivated. Chosen gender gets the gamecontrol object parented to it.
			if (Application.loadedLevelName == "Terrain1" || Application.loadedLevelName == "Terrain4" || Application.loadedLevelName == "Terrain6"|| Application.loadedLevelName == "Infinite") {
				if (gender == "male") {
					//woman.gameObject.SetActive (false);
					transform.parent = man.transform;

				} else {
					//man.gameObject.SetActive (false);
					transform.parent = woman.transform;

				}

				//Data is saved during vicory cutscenes.
			} else if (needToSaveLevelProgression) {
				save ();
				needToSaveLevelProgression = false;
				Debug.Log (currentStageAchieved + "              " + pcurrentStageAchieved);
			}
			//Covering my ass, leaving 20 seconds tcurrentstageachdddddddddo get saving done.
		} else if (Application.loadedLevelName == "Terrain1" || Application.loadedLevelName == "Terrain4" && Time.timeSinceLevelLoad >= 220) {
			hunterPoints += Player.points;
			Player.points = 0;
			save();
		} 

		if (Application.loadedLevelName == "Blacksmith's Forge"){
			weaponName = continueToMission.chosenWeapon;

			if (!weaponSaved){
				if (bossController.acquiredWeapon1 == "axe" || bossController.acquiredWeapon2 == "axe" || bossController.acquiredWeapon3 == "axe"){
					pdoubleAxeBought = true;
					doubleAxeBought = pdoubleAxeBought;
					save ();
					weaponSaved = true;
				}
				if (bossController.acquiredWeapon1 == "axeGlow" || bossController.acquiredWeapon2 == "axeGlow" || bossController.acquiredWeapon3 == "axeGlow"){
					pdoubleAxeGlowBought = true;
					doubleAxeGlowBought = pdoubleAxeBought;
					save ();
					weaponSaved = true;
				}
				if (bossController.acquiredWeapon1 == "mace" || bossController.acquiredWeapon2 == "mace" || bossController.acquiredWeapon3 == "mace"){
					pmaceBought = true;
					maceBought = pmaceBought;
					save ();
					weaponSaved = true;
				}
				if (bossController.acquiredWeapon1 == "h1" || bossController.acquiredWeapon2 == "h1" || bossController.acquiredWeapon3 == "h1"){
					phalberd1Bought = true;
					halberd1Bought = phalberd1Bought;
					save ();
					weaponSaved = true;
				}
				if (bossController.acquiredWeapon1 == "h2" || bossController.acquiredWeapon2 == "h2" || bossController.acquiredWeapon3 == "h2"){
					phalberd2Bought = true;
					halberd2Bought = phalberd2Bought;
					save ();
					weaponSaved = true;
				}
				if (bossController.acquiredWeapon1 == "h3" || bossController.acquiredWeapon2 == "h3" || bossController.acquiredWeapon3 == "h3"){
					phalberd3Bought = true;
					halberd3Bought = phalberd3Bought;
					save ();
					weaponSaved = true;
				}
				if (bossController.acquiredWeapon1 == "darkAxe" || bossController.acquiredWeapon2 == "darkAxe" || bossController.acquiredWeapon3 == "darkAxe"){
					pdarkAxeBought = true;
					darkAxeBought = pdarkAxeBought;
					save ();
					weaponSaved = true;
				}
				if (bossController.acquiredWeapon1 == "darkSword" || bossController.acquiredWeapon2 == "darkSword" || bossController.acquiredWeapon3 == "darkSword"){
					pdarkSwordBought = true;
					darkSwordBought = pdarkSwordBought;
					save ();
					weaponSaved = true;
				}
				if (bossController.acquiredWeapon1 == "skullHammer" || bossController.acquiredWeapon2 == "skullHammer" || bossController.acquiredWeapon3 == "skullHammer"){
					pskullHammerBought = true;
					skullHammerBought = pskullHammerBought;
					save ();
					weaponSaved = true;
				}
				if (bossController.acquiredWeapon1 == "elvenSword" || bossController.acquiredWeapon2 == "skullHammer" || bossController.acquiredWeapon3 == "skullHammer"){
					pelvenSwordBought = true;
					elvenSwordBought = pelvenSwordBought;
					save ();
					weaponSaved = true;
				}
			}


			if (itemAcquisitionManager.needToUpdatePersistentData){
				Debug.Log ("saved");
				poisonUnlocked = ppoisonUnlocked;
				armorUnlocked = parmorUnlocked;

				hunterPoints = phunterPoints;
				doubleAxeUnlocked = pdoubleAxeUnlocked;
				doubleAxeGlowUnlocked = pdoubleAxeGlowUnlocked;
				elvenSwordUnlocked = pelvenSwordUnlocked;
				halberd1Unlocked = phalberd1Unlocked;
				maceUnlocked = pmaceUnlocked;
				halberd2Unlocked = phalberd2Unlocked;
				halberd3Unlocked = phalberd3Unlocked;
				darkSwordUnlocked = pdarkSwordUnlocked;
				darkAxeUnlocked = pdarkAxeUnlocked;
				skullHammerUnlocked = pskullHammerUnlocked;
				save ();
			}
		}
		if (selectChar.gender == "male") {
			gender = "male";
		} else if (selectChar.gender == "woman") {
			gender = "female";
		}


	}

	public void UpdateGlobalVariables(){
		//pcurrentStageAchieved = currentStageAchieved;
		GooglePlayerUtilities.achievement1unlocked = achievement1unlocked;
		GooglePlayerUtilities.achievement2unlocked = achievement2unlocked;
		GooglePlayerUtilities.achievement3unlocked = achievement3unlocked;
		GooglePlayerUtilities.achievement4unlocked = achievement4unlocked;
		GooglePlayerUtilities.achievement5unlocked = achievement5unlocked;
		parmorUnlocked = armorUnlocked;
		ppoisonUnlocked = poisonUnlocked;

		phunterPoints = hunterPoints;
		phunterLevel = hunterLevel;
		phunterDamage = hunterDamage;
		phunterHealth = hunterHealth;
		phunterArmor = hunterArmor;
		phunterName = hunterName;
		phunterClass = hunterClass;		
		pkaiigranUnlocked = kaiigranUnlocked;
		pkaiigranPoints = kaiigranPoints;
		pkaiigranLevel = kaiigranLevel;
		pkaiigranDamage = kaiigranDamage;
		pkaiigranHealth = kaiigranHealth;
		pkaiigranArmor = kaiigranArmor;
		pkaiigranName = kaiigranName;
		pkaiigranClass = kaiigranClass;		
		pBountyHUnlocked = BountyHUnlocked;
		pBountyHPoints = BountyHPoints;
		pBountyHLevel = BountyHLevel;
		pBountyHDamage = BountyHDamage;
		pBountyHHealth = BountyHHealth;
		pBountyHArmor = BountyHArmor;
		pBountyHName = BountyHName;
		pBountyHClass = BountyHClass;		
		pMagePoints = MagePoints;
		pMageLevel = MageLevel;
		pMageDamage = MageDamage;
		pMageHealth = MageHealth;
		pMageArmor = MageArmor;
		pMageName = MageName;		
		pwoodShieldUnlocked = woodShieldUnlocked;
		pironShieldUnlocked = ironShieldUnlocked;
		pcarbonShieldUnlocked = carbonShieldUnlocked;		
		psimleSwordUnlocked = carbonShieldUnlocked;
		pdoubleAxeUnlocked = doubleAxeUnlocked;
		pdoubleAxeGlowUnlocked = doubleAxeGlowUnlocked;
		pelvenSwordUnlocked = elvenSwordUnlocked;
		pangelicSwordUnlocked = angelicSwordUnlocked;		
		pwoodShieldBought = woodShieldBought;
		pironShieldBought = ironShieldBought;
		pcarbonShieldBought = carbonShieldBought;		
		psimleSwordBought = simleSwordBought;
		pdoubleAxeBought = doubleAxeBought;
		pdoubleAxeGlowBought = doubleAxeGlowBought;
		pelvenSwordBought = elvenSwordBought;
		pangelicSwordBought = angelicSwordBought;		
		ppowerfulStrikesUnlocked = powerfulStrikesUnlocked;
		pjuggernautUnlocked = juggernautUnlocked;
		pLowBlowUnlocked = LowBlowUnlocked;
		pathleticUnlocked = athleticUnlocked;
		pbrutUnlocked = brutUnlocked;
		pbeastUnlocked = beastUnlocked;
		pbattleMasterUnlocked = battleMasterUnlocked;
		plethalBlowUnlocked = lethalBlowUnlocked;
		pArmsmanUnlocked = ArmsmanUnlocked;		
		ppowerfulStrikesBought = powerfulStrikesBought;
		pjuggernautBought = juggernautBought;
		pLowBlowBought = LowBlowBought;
		pathleticBought = athleticBought;
		pbruteBought = bruteBought;
		pbeastBought = beastBought;
		pbattleMasterBought = battleMasterBought;
		plethalBlowBought = lethalBlowBought;
		pArmsmanBought = ArmsmanBought;		
		phalberd1Unlocked = halberd1Unlocked;
		phalberd2Unlocked = halberd2Unlocked;
		phalberd3Unlocked = halberd3Unlocked;
		pskullHammerUnlocked = skullHammerUnlocked;
		pdarkSwordUnlocked = darkSwordUnlocked;
		phammerUnlocked = hammerUnlocked;
		pmaceUnlocked = maceUnlocked;
		pdarkAxeUnlocked = darkAxeUnlocked;
		ptheNightmareUnlocked = theNightmareUnlocked;		
		phalberd1Bought = halberd1Bought;
		phalberd2Bought = halberd2Bought;
		phalberd3Bought = halberd3Bought;
		pskullHammerBought = skullHammerBought;
		pdarkSwordBought = darkSwordBought;
		phammerBought = hammerBought;
		pmaceBought = maceBought;
		pdarkAxeBought = darkAxeBought;
		ptheNightmareBought = theNightmareBought;		
		pcrossBowUnlocked = crossBowUnlocked;
		pelvenBowUnlocked = elvenBowUnlocked;
		pbow1Unlocked = bow1Unlocked;
		pbow2Unlocked = bow2Unlocked;
		pcarbonArrowPreview = carbonArrowPreview;
		pironArrowUnlocked = ironArrowUnlocked;
		ppoisonArrowUnlocked = poisonArrowUnlocked;
		ppoisonCarbonArrowUnlocked = poisonCarbonArrowUnlocked;
		pwoodArrowUnlocked = woodArrowUnlocked;		
		pcrossBowBought = crossBowBought;
		pelvenBowBought = elvenBowBought;
		pbow1Bought = bow1Bought;
		pbow2Bought = bow2Bought;
		pcarbonArrowBought = carbonArrowBought;
		pironArrowBought = ironArrowBought;
		ppoisonArrowBought = poisonArrowBought;
		ppoisonCarbonArrowBought = poisonCarbonArrowBought;
		pwoodArrowBought = woodArrowBought;		
		pblueMagicUnlocked = blueMagicUnlocked;
		pelectricMagicUnlocked = electricMagicUnlocked;
		pfireMagicUnlocked = fireMagicUnlocked;
		pfireMagic2Unlocked = fireMagic2Unlocked;
		plightMagicUnlocked = lightMagicUnlocked;
		pfireMistMagicUnlocked = fireMistMagicUnlocked;		
		pblueMagicBought = blueMagicBought;
		pelectricMagicBought = electricMagicBought;
		pfireMagicBought = fireMagicBought;
		pfireMagic2Bought = fireMagic2Bought;
		plightMagicBought = lightMagicBought;
		pfireMistMagicBought = fireMistMagicBought;		
		pskilledHandsUnlocked = skilledHandsUnlocked;
		progueUnlocked = rogueUnlocked;
		pcrippleUnlocked = crippleUnlocked;
		phawkEyeUnlocked = hawkEyeUnlocked;
		peagleEyeUnlocked = eagleEyeUnlocked;
		ppowerfulShotUnlocked = powerfulShotUnlocked;
		pinstantKillUnlocked = instantKillUnlocked;		
		pskilledHandsBought = skilledHandsBought;
		progueBought = rogueBought;
		pcrippleBought = crippleBought;
		phawkEyeBought = hawkEyeBought;
		peagleEyeBought = eagleEyeBought;
		ppowerfulShotBought = powerfulShotBought;
		pinstantKillBought = instantKillBought;		
		pflameUnlocked = flameUnlocked;
		pnoviceUnlocked = noviceUnlocked;
		pfrostUnlocked = frostUnlocked;
		pexploitUnlocked = exploitUnlocked;
		pfireUnlocked = fireUnlocked;
		psnowUnlocked = snowUnlocked;
		pincinerateUnlocked = incinerateUnlocked;
		pspellmasterUnlocked = spellmasterUnlocked;
		pfreezeUnlocked = freezeUnlocked;
		pflameBought = flameBought;
		pnoviceBought = noviceBought;
		pfrostBought = frostBought;
		pexploitBought = exploitBought;
		pfireBought = fireBought;
		psnowBought = snowBought;
		pincinerateBought = incinerateBought;
		pspellmasterBought = spellmasterBought;
		pfreezeBought = freezeBought;
	}

	public void save(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Open (Application.persistentDataPath + "/playerInfo.dat", FileMode.OpenOrCreate);

		PlayerData data = new PlayerData ();

		achievement1unlocked = GooglePlayerUtilities.achievement1unlocked;
		achievement2unlocked = GooglePlayerUtilities.achievement2unlocked;
		achievement3unlocked = GooglePlayerUtilities.achievement3unlocked;
		achievement4unlocked = GooglePlayerUtilities.achievement4unlocked;
		achievement5unlocked = GooglePlayerUtilities.achievement5unlocked;

		data.poisonUnlocked = poisonUnlocked;
		data.armorUnlocked = armorUnlocked;
		currentStageAchieved = pcurrentStageAchieved;
		data.currentStageAchieved = currentStageAchieved;
		data.hunterPoints = hunterPoints;
		data.hunterLevel = hunterLevel;
		data.hunterDamage = hunterDamage;
		data.hunterHealth = hunterHealth;
		data.hunterArmor = hunterArmor;
		data.hunterName = hunterName;
		data.hunterClass = hunterClass;		
		data.kaiigranUnlocked = kaiigranUnlocked;
		data.kaiigranPoints = kaiigranPoints;
		data.kaiigranLevel = kaiigranLevel;
		data.kaiigranDamage = kaiigranDamage;
		data.kaiigranHealth = kaiigranHealth;
		data.kaiigranArmor = kaiigranArmor;
		data.kaiigranName = kaiigranName;
		data.kaiigranClass = kaiigranClass;		
		data.BountyHUnlocked = BountyHUnlocked;
		data.BountyHPoints = BountyHPoints;
		data.BountyHLevel = BountyHLevel;
		data.BountyHDamage = BountyHDamage;
		data.BountyHHealth = BountyHHealth;
		data.BountyHArmor = BountyHArmor;
		data.BountyHName = BountyHName;
		data.BountyHClass = BountyHClass;		
		data.MagePoints = MagePoints;
		data.MageLevel = MageLevel;
		data.MageDamage = MageDamage;
		data.MageHealth = MageHealth;
		data.MageArmor = MageArmor;
		data.MageName = MageName;		
		data.woodShieldUnlocked = woodShieldUnlocked;
		data.ironShieldUnlocked = ironShieldUnlocked;
		data.carbonShieldUnlocked = carbonShieldUnlocked;		
		data.simleSwordUnlocked = carbonShieldUnlocked;
		data.doubleAxeUnlocked = doubleAxeUnlocked;
		data.doubleAxeGlowUnlocked = doubleAxeGlowUnlocked;
		data.elvenSwordUnlocked = elvenSwordUnlocked;
		data.angelicSwordUnlocked = angelicSwordUnlocked;		
		data.woodShieldBought = woodShieldBought;
		data.ironShieldBought = ironShieldBought;
		data.carbonShieldBought = carbonShieldBought;		
		data.simleSwordBought = simleSwordBought;
		data.doubleAxeBought = doubleAxeBought;
		data.doubleAxeGlowBought = doubleAxeGlowBought;
		data.elvenSwordBought = elvenSwordBought;
		data.angelicSwordBought = angelicSwordBought;		
		data.powerfulStrikesUnlocked = powerfulStrikesUnlocked;
		data.juggernautUnlocked = juggernautUnlocked;
		data.LowBlowUnlocked = LowBlowUnlocked;
		data.athleticUnlocked = athleticUnlocked;
		data.brutUnlocked = brutUnlocked;
		data.beastUnlocked = beastUnlocked;
		data.battleMasterUnlocked = battleMasterUnlocked;
		data.lethalBlowUnlocked = lethalBlowUnlocked;
		data.ArmsmanUnlocked = ArmsmanUnlocked;		
		data.powerfulStrikesBought = powerfulStrikesBought;
		data.juggernautBought = juggernautBought;
		data. LowBlowBought = LowBlowBought;
		data.athleticBought = athleticBought;
		data.bruteBought = bruteBought;
		data.beastBought = beastBought;
		data.battleMasterBought = battleMasterBought;
		data.lethalBlowBought = lethalBlowBought;
		data.ArmsmanBought = ArmsmanBought;		
		data.halberd1Unlocked = halberd1Unlocked;
		data.halberd2Unlocked = halberd2Unlocked;
		data.halberd3Unlocked = halberd3Unlocked;
		data.skullHammerUnlocked = skullHammerUnlocked;
		data.darkSwordUnlocked = darkSwordUnlocked;
		data.hammerUnlocked = hammerUnlocked;
		data.maceUnlocked = maceUnlocked;
		data.darkAxeUnlocked = darkAxeUnlocked;
		data.theNightmareUnlocked = theNightmareUnlocked;		
		data.halberd1Bought = halberd1Bought;
		data.halberd2Bought = halberd2Bought;
		data.halberd3Bought = halberd3Bought;
		data.skullHammerBought = skullHammerBought;
		data.darkSwordBought = darkSwordBought;
		data.hammerBought = hammerBought;
		data.maceBought = maceBought;
		data.darkAxeBought = darkAxeBought;
		data.theNightmareBought = theNightmareBought;		
		data.crossBowUnlocked = crossBowUnlocked;
		data.elvenBowUnlocked = elvenBowUnlocked;
		data.bow1Unlocked = bow1Unlocked;
		data.bow2Unlocked = bow2Unlocked;
		data.carbonArrowPreview = carbonArrowPreview;
		data.ironArrowUnlocked = ironArrowUnlocked;
		data.poisonArrowUnlocked = poisonArrowUnlocked;
		data.poisonCarbonArrowUnlocked = poisonCarbonArrowUnlocked;
		data. woodArrowUnlocked = woodArrowUnlocked;		
		data.crossBowBought = crossBowBought;
		data.elvenBowBought = elvenBowBought;
		data.bow1Bought = bow1Bought;
		data.bow2Bought = bow2Bought;
		data.carbonArrowBought = carbonArrowBought;
		data.ironArrowBought = ironArrowBought;
		data.poisonArrowBought = poisonArrowBought;
		data.poisonCarbonArrowBought = poisonCarbonArrowBought;
		data.woodArrowBought = woodArrowBought;		
		data.blueMagicUnlocked = blueMagicUnlocked;
		data.electricMagicUnlocked = electricMagicUnlocked;
		data.fireMagicUnlocked = fireMagicUnlocked;
		data.fireMagic2Unlocked = fireMagic2Unlocked;
		data.lightMagicUnlocked = lightMagicUnlocked;
		data.fireMistMagicUnlocked = fireMistMagicUnlocked;		
		data.blueMagicBought = blueMagicBought;
		data.electricMagicBought = electricMagicBought;
		data.fireMagicBought = fireMagicBought;
		data.fireMagic2Bought = fireMagic2Bought;
		data.lightMagicBought = lightMagicBought;
		data.fireMistMagicBought = fireMistMagicBought;		
		data.skilledHandsUnlocked = skilledHandsUnlocked;
		data.rogueUnlocked = rogueUnlocked;
		data.crippleUnlocked = crippleUnlocked;
		data.hawkEyeUnlocked = hawkEyeUnlocked;
		data.eagleEyeUnlocked = eagleEyeUnlocked;
		data.powerfulShotUnlocked = powerfulShotUnlocked;
		data.instantKillUnlocked = instantKillUnlocked;		
		data.skilledHandsBought = skilledHandsBought;
		data.rogueBought = rogueBought;
		data.crippleBought = crippleBought;
		data.hawkEyeBought = hawkEyeBought;
		data.eagleEyeBought = eagleEyeBought;
		data.powerfulShotBought = powerfulShotBought;
		data.instantKillBought = instantKillBought;		
		data.flameUnlocked = flameUnlocked;
		data.noviceUnlocked = noviceUnlocked;
		data.frostUnlocked = frostUnlocked;
		data.exploitUnlocked = exploitUnlocked;
		data.fireUnlocked = fireUnlocked;
		data.snowUnlocked = snowUnlocked;
		data.incinerateUnlocked = incinerateUnlocked;
		data.spellmasterUnlocked = spellmasterUnlocked;
		data.freezeUnlocked = freezeUnlocked;

		//data is saved now.
		itemAcquisitionManager.needToUpdatePersistentData = false;

		bf.Serialize (file, data);
		file.Close();

		//Update the game state with data for saving to google
		if (GooglePlayerUtilities.setGameStateForSaving) {
			GameState = data;
			GameStateJson = JsonUtility.ToJson(GameState);
			GooglePlayerUtilities.setGameStateForSaving = false;
		}
	}

	public void Load(){

		if(File.Exists(Application.persistentDataPath + "/playerInfo.dat")) {

			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);

			PlayerData data = (PlayerData)bf.Deserialize(file);

			//If we just loaded from google play, data should become the new game state
			if (SavedGamesExample.jsonLoaded) {

				data = NewGameState;
				SavedGamesExample.jsonLoaded = false;
			}

			file.Close();

			poisonUnlocked = data.poisonUnlocked;
			armorUnlocked = data.armorUnlocked;

			currentStageAchieved = data.currentStageAchieved;

			if (pcurrentStageAchieved < currentStageAchieved){
				pcurrentStageAchieved = currentStageAchieved;
			}
			GooglePlayerUtilities.achievement1unlocked = data.achievement1unlocked;
			GooglePlayerUtilities.achievement2unlocked = data.achievement2unlocked;
			GooglePlayerUtilities.achievement3unlocked = data.achievement3unlocked;
			GooglePlayerUtilities.achievement4unlocked = data.achievement4unlocked;
			GooglePlayerUtilities.achievement5unlocked = data.achievement5unlocked;
			hunterPoints = data.hunterPoints;
			hunterLevel = data.hunterLevel;
			hunterDamage = data.hunterDamage;
			hunterHealth = data.hunterHealth;
			hunterArmor = data.hunterArmor;
			hunterName= data.hunterName;
			hunterClass= data.hunterClass;		
			kaiigranUnlocked= data.kaiigranUnlocked;
			kaiigranPoints= data.kaiigranPoints;
			kaiigranLevel= data.kaiigranLevel;
			kaiigranDamage= data.kaiigranDamage;
			kaiigranHealth= data.kaiigranHealth;
			kaiigranArmor= data.kaiigranArmor;
			kaiigranName= data.kaiigranName;
			kaiigranClass= data.kaiigranClass;		
			BountyHUnlocked= data.BountyHUnlocked;
			BountyHPoints= data.BountyHPoints;
			BountyHLevel= data.BountyHLevel;
			BountyHDamage= data.BountyHDamage;
			BountyHHealth= data.BountyHHealth;
			BountyHArmor= data.BountyHArmor;
			BountyHName= data.BountyHName;
			BountyHClass= data.BountyHClass;		
			MagePoints= data.MagePoints;
			MageLevel= data.MageLevel;
			MageDamage= data.MageDamage;
			MageHealth= data.MageHealth;
			MageArmor= data.MageArmor;
			MageName= data.MageName;		
			woodShieldUnlocked= data.woodShieldUnlocked;
			ironShieldUnlocked= data.ironShieldUnlocked;
			carbonShieldUnlocked= data.carbonShieldUnlocked;		
			simleSwordUnlocked= data.simleSwordUnlocked;
			doubleAxeUnlocked= data.doubleAxeUnlocked;
			doubleAxeGlowUnlocked= data.doubleAxeGlowUnlocked;
			elvenSwordUnlocked= data.elvenSwordUnlocked;
			angelicSwordUnlocked= data.angelicSwordUnlocked;		
			woodShieldBought= data.woodShieldBought;
			ironShieldBought= data.ironShieldBought;
			carbonShieldBought= data.carbonShieldBought;	
			simleSwordBought= data.simleSwordBought;
			doubleAxeBought= data.doubleAxeBought;
			doubleAxeGlowBought= data.doubleAxeGlowBought;
			elvenSwordBought= data.elvenSwordBought;
			angelicSwordBought= data.angelicSwordBought;		
			powerfulStrikesUnlocked= data.powerfulStrikesUnlocked;
			juggernautUnlocked= data.juggernautUnlocked;
			LowBlowUnlocked= data.LowBlowUnlocked;
			athleticUnlocked= data.athleticUnlocked;
			brutUnlocked= data.brutUnlocked;
			beastUnlocked= data.beastUnlocked;
			battleMasterUnlocked= data.battleMasterUnlocked;
			lethalBlowUnlocked= data.lethalBlowUnlocked;
			ArmsmanUnlocked= data.ArmsmanUnlocked;		
			powerfulStrikesBought= data.powerfulStrikesBought;
			juggernautBought= data.juggernautBought;
			LowBlowBought= data. LowBlowBought;
			athleticBought= data.athleticBought;
			bruteBought= data.bruteBought;
			beastBought= data.beastBought;
			battleMasterBought= data.battleMasterBought;
			lethalBlowBought= data.lethalBlowBought;
			ArmsmanBought= data.ArmsmanBought;	
			halberd1Unlocked= data.halberd1Unlocked;
			halberd2Unlocked= data.halberd2Unlocked;
			halberd3Unlocked= data.halberd3Unlocked;
			skullHammerUnlocked= data.skullHammerUnlocked;
			darkSwordUnlocked= data.darkSwordUnlocked;
			hammerUnlocked= data.hammerUnlocked;
			maceUnlocked= data.maceUnlocked;
			darkAxeUnlocked= data.darkAxeUnlocked;
			theNightmareUnlocked= data.theNightmareUnlocked;		
			halberd1Bought= data.halberd1Bought;
			halberd2Bought= data.halberd2Bought;
			halberd3Bought= data.halberd3Bought;
			skullHammerBought= data.skullHammerBought;
			darkSwordBought= data.darkSwordBought;
			hammerBought= data.hammerBought;
			maceBought= data.maceBought;
			darkAxeBought= data.darkAxeBought;
			theNightmareBought= data.theNightmareBought;		
			crossBowUnlocked= data.crossBowUnlocked;
			elvenBowUnlocked= data.elvenBowUnlocked;
			bow1Unlocked= data.bow1Unlocked;
			bow2Unlocked= data.bow2Unlocked;
			carbonArrowPreview= data.carbonArrowPreview;
			ironArrowUnlocked= data.ironArrowUnlocked;
			poisonArrowUnlocked= data.poisonArrowUnlocked;
			poisonCarbonArrowUnlocked= data.poisonCarbonArrowUnlocked;
			woodArrowUnlocked= data. woodArrowUnlocked;	
			crossBowBought= data.crossBowBought;
			elvenBowBought= data.elvenBowBought;
			bow1Bought= data.bow1Bought;
			bow2Bought= data.bow2Bought;
			carbonArrowBought= data.carbonArrowBought;
			ironArrowBought= data.ironArrowBought;
			poisonArrowBought= data.poisonArrowBought;
			poisonCarbonArrowBought= data.poisonCarbonArrowBought;
			woodArrowBought= data.woodArrowBought;		
			blueMagicUnlocked= data.blueMagicUnlocked;
			electricMagicUnlocked= data.electricMagicUnlocked;
			fireMagicUnlocked= data.fireMagicUnlocked;
			fireMagic2Unlocked= data.fireMagic2Unlocked;
			lightMagicUnlocked= data.lightMagicUnlocked;
			fireMistMagicUnlocked= data.fireMistMagicUnlocked;		
			blueMagicBought= data.blueMagicBought;
			electricMagicBought= data.electricMagicBought;
			fireMagicBought= data.fireMagicBought;
			fireMagic2Bought= data.fireMagic2Bought;
			lightMagicBought= data.lightMagicBought;
			fireMistMagicBought= data.fireMistMagicBought;		
			skilledHandsUnlocked= data.skilledHandsUnlocked;
			rogueUnlocked= data.rogueUnlocked;
			crippleUnlocked= data.crippleUnlocked;
			hawkEyeUnlocked= data.hawkEyeUnlocked;
			eagleEyeUnlocked= data.eagleEyeUnlocked;
			powerfulShotUnlocked= data.powerfulShotUnlocked;
			instantKillUnlocked= data.instantKillUnlocked;		
			skilledHandsBought= data.skilledHandsBought;
			rogueBought= data.rogueBought;
			crippleBought= data.crippleBought;
			hawkEyeBought= data.hawkEyeBought;
			eagleEyeBought= data.eagleEyeBought;
			powerfulShotBought= data.powerfulShotBought;
			instantKillBought= data.instantKillBought;		
			flameUnlocked= data.flameUnlocked;
			noviceUnlocked= data.noviceUnlocked;
			frostUnlocked= data.frostUnlocked;
			exploitUnlocked= data.exploitUnlocked;
			fireUnlocked= data.fireUnlocked;
			snowUnlocked= data.snowUnlocked;
			incinerateUnlocked= data.incinerateUnlocked;
			spellmasterUnlocked= data.spellmasterUnlocked;
			freezeUnlocked= data.freezeUnlocked;
			flameBought= data.flameBought;
			noviceBought= data.noviceBought;
			frostBought= data.frostBought;
			exploitBought= data.exploitBought;
			fireBought= data.fireBought;
			snowBought= data.snowBought;
			incinerateBought= data.incinerateBought;
			spellmasterBought= data.spellmasterBought;
			freezeBought= data.freezeBought;


		}

		UpdateGlobalVariables ();
	}

	[Serializable]
	class PlayerData{
		public bool achievement1unlocked, achievement2unlocked,achievement3unlocked,achievement4unlocked, achievement5unlocked = false;
		public bool poisonUnlocked = false;
		public bool armorUnlocked = false;

		public int currentStageAchieved = 1;
		public int hunterPoints = 0;
		public int hunterLevel = 0;
		public int hunterDamage = 10;
		public int hunterHealth = 10;
		public int hunterArmor = 10;
		public string hunterName = "";
		public string hunterClass = "";			
		public bool kaiigranUnlocked = false;
		public int kaiigranPoints = 0;
		public int kaiigranLevel = 0;
		public int kaiigranDamage = 1;
		public int kaiigranHealth = 10;
		public int kaiigranArmor = 5;
		public string kaiigranName = "";
		public string kaiigranClass = "";			
		public bool BountyHUnlocked = false;
		public int BountyHPoints = 0;
		public int BountyHLevel = 0;
		public int BountyHDamage = 15;
		public int BountyHHealth = 10;
		public int BountyHArmor = 20;
		public string BountyHName = "";
		public string BountyHClass = "";			
		public int MagePoints = 0;
		public int MageLevel = 0;
		public int MageDamage = 10;
		public int MageHealth = 10;
		public int MageArmor = 5;
		public string MageName = "";			
		public bool woodShieldUnlocked = false;
		public bool ironShieldUnlocked = false;
		public bool carbonShieldUnlocked = false;			
		public bool simleSwordUnlocked = false;
		public bool doubleAxeUnlocked = false;
		public bool doubleAxeGlowUnlocked = false;
		public bool elvenSwordUnlocked = false;
		public bool angelicSwordUnlocked = false;			
		public bool woodShieldBought = false;
		public bool ironShieldBought = false;
		public bool carbonShieldBought = false;			
		public bool simleSwordBought = false;
		public bool doubleAxeBought = false;
		public bool doubleAxeGlowBought = false;
		public bool elvenSwordBought = false;
		public  bool angelicSwordBought = false;			
		public bool powerfulStrikesUnlocked = false;
		public bool juggernautUnlocked = false;
		public bool LowBlowUnlocked = false;
		public bool athleticUnlocked = false;
		public bool brutUnlocked = false;
		public bool beastUnlocked = false;
		public bool battleMasterUnlocked = false;
		public bool lethalBlowUnlocked = false;
		public bool ArmsmanUnlocked = false;			
		public bool powerfulStrikesBought = false;
		public bool juggernautBought = false;
		public bool LowBlowBought = false;
		public bool athleticBought = false;
		public bool bruteBought = false;
		public bool beastBought = false;
		public bool battleMasterBought = false;
		public bool lethalBlowBought = false;
		public bool ArmsmanBought = false;			
		public bool halberd1Unlocked = false;
		public bool halberd2Unlocked = false;
		public bool halberd3Unlocked = false;
		public bool skullHammerUnlocked = false;
		public bool darkSwordUnlocked = false;
		public bool hammerUnlocked = false;
		public bool maceUnlocked = false;
		public bool darkAxeUnlocked = false;
		public bool theNightmareUnlocked = false;			
		public bool halberd1Bought = false;
		public bool halberd2Bought = false;
		public bool halberd3Bought = false;
		public bool skullHammerBought = false;
		public bool darkSwordBought = false;
		public bool hammerBought = false;
		public bool maceBought = false;
		public bool darkAxeBought = false;
		public bool theNightmareBought = false;			
		public bool crossBowUnlocked = false;
		public bool elvenBowUnlocked = false;
		public bool bow1Unlocked = false;
		public bool bow2Unlocked = false;
		public bool carbonArrowPreview = false;
		public bool ironArrowUnlocked = false;
		public bool poisonArrowUnlocked = false;
		public bool poisonCarbonArrowUnlocked = false;
		public bool woodArrowUnlocked = false;			
		public bool crossBowBought = false;
		public bool elvenBowBought = false;
		public bool bow1Bought = false;
		public bool bow2Bought = false;
		public bool carbonArrowBought = false;
		public bool ironArrowBought = false;
		public bool poisonArrowBought = false;
		public bool poisonCarbonArrowBought = false;
		public bool woodArrowBought = false;			
		public bool blueMagicUnlocked = false;
		public bool electricMagicUnlocked = false;
		public bool fireMagicUnlocked = false;
		public bool fireMagic2Unlocked = false;
		public bool lightMagicUnlocked = false;
		public bool fireMistMagicUnlocked = false;			
		public bool blueMagicBought = false;
		public bool electricMagicBought = false;
		public bool fireMagicBought = false;
		public bool fireMagic2Bought = false;
		public bool lightMagicBought = false;
		public bool fireMistMagicBought = false;			
		public bool skilledHandsUnlocked = false;
		public bool rogueUnlocked = false;
		public bool crippleUnlocked = false;
		public bool hawkEyeUnlocked = false;
		public bool eagleEyeUnlocked = false;
		public bool powerfulShotUnlocked = false;
		public bool instantKillUnlocked = false;			
		public bool skilledHandsBought = false;
		public bool rogueBought = false;
		public bool crippleBought = false;
		public bool hawkEyeBought = false;
		public bool eagleEyeBought = false;
		public bool powerfulShotBought = false;
		public bool instantKillBought = false;			
		public bool flameUnlocked = false;
		public bool noviceUnlocked = false;
		public bool frostUnlocked = false;
		public bool exploitUnlocked = false;
		public bool fireUnlocked = false;
		public bool snowUnlocked = false;
		public bool incinerateUnlocked = false;
		public bool spellmasterUnlocked = false;
		public bool freezeUnlocked = false;
		public bool flameBought = false;
		public bool noviceBought = false;
		public bool frostBought = false;
		public bool exploitBought = false;
		public bool fireBought = false;
		public bool snowBought = false;
		public bool incinerateBought = false;
		public bool spellmasterBought = false;
		public bool freezeBought = false;
	}
}