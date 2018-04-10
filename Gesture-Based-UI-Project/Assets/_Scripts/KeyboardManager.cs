/***
 * Asset original author: Yunhan Li 
 * Any issue please contact yunhn.lee@gmail.com
 * 
 * Adapted for use in the gesture based UI project by Mindaugas Vainauskas and Keith Williams 
 * GMIT 2018
 ***/

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Assets._Scripts;

namespace VRKeyboard.Utils {
    public class KeyboardManager : MonoBehaviour {
        #region Public Variables
        [Header("User defined")]
        [Tooltip("If the character is uppercase at the initialization")]
        public bool isUppercase = false;
        public int maxInputLength;
        
        [Header("UI Elements")]
        public Text inputText;

        [Header("Essentials")]
        public Transform characters;
        #endregion

        #region Private Variables
        private string Input {
            get { return inputText.text;  }
            set { inputText.text = value;  }
        }

        List<PlayerEntity> playerList = new List<PlayerEntity>();

        private Dictionary<GameObject, Text> keysDictionary = new Dictionary<GameObject, Text>();

        private bool capslockFlag;
        #endregion

        #region Monobehaviour Callbacks
        private void Awake() {
            
            for (int i = 0; i < characters.childCount; i++) {
                GameObject key = characters.GetChild(i).gameObject;
                Text _text = key.GetComponentInChildren<Text>();
                keysDictionary.Add(key, _text);

                key.GetComponent<Button>().onClick.AddListener(() => {
                    GenerateInput(_text.text);
                });
            }

            capslockFlag = isUppercase;
            CapsLock();
        }
        #endregion

        #region Public Methods
        public void Backspace() {
            if (Input.Length > 0) {
                Input = Input.Remove(Input.Length - 1);
            } else {
                return;
            }
        }

        public void Clear() {
            Input = "";
        }

        public void CapsLock() {
            if (capslockFlag) {
                foreach (var pair in keysDictionary) {
                    pair.Value.text = ToUpperCase(pair.Value.text);
                }
            } else {
                foreach (var pair in keysDictionary) {
                    pair.Value.text = ToLowerCase(pair.Value.text);
                }
            }
            capslockFlag = !capslockFlag;
        }

        int counter = 0;
        //Submitting the inputed player name
        public void Submit()
        {           
            PlayerEntity player = new PlayerEntity();
            player.setPlayerName(Input);
            playerList.Add(player);           

            //Getting player names and scores to carry through to next scene.
            PlayerPrefs.SetString(("Player" + counter + "Name"), Input);
            //update counter
            counter++;
            //reset input
            Input = "";
            if (playerList.Count == 2)
            {
                counter = 0;
                SceneManager.LoadScene("DemoScene");
            }
        }
        #endregion

        #region Private Methods
        public void GenerateInput(string s) {
            if (Input.Length > maxInputLength) { return; }
            Input += s;
        }

        private string ToLowerCase(string s) {
            return s.ToLower();
        }

        private string ToUpperCase(string s) {
            return s.ToUpper();
        }
        #endregion
    }
}