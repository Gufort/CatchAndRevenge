using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GodMode : MonoBehaviour
{   
    [SerializeField] private PlayerController _player;
    [SerializeField] private GameObject _playerHP;
    [SerializeField] private TMP_Text tMP_Text;
    public void activeGameMode(){
        if(_player._canTakeDamage){
            _player._canTakeDamage = false;
            _playerHP.GetComponent<PlayerHPRenderer>().enabled = false;
            tMP_Text.text = "???";
        }
        else {
            _player._canTakeDamage = true;
            _playerHP.GetComponent<PlayerHPRenderer>().enabled = true;
            tMP_Text.text = PlayerController.curr_hp_to_renderer.ToString();
        }
    }
}
