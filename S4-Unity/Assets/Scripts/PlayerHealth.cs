using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public PlayerData dataPlayer;
    public bool isInvulnerable = false;
    public float invulnerableTime = 2.25f;
    public float invulnerableFlash = 0.2f;
    public SpriteRenderer sr;

    void Start(){
        dataPlayer.currentLifePoints = dataPlayer.maxLifePoints;
    }

    public void Hurt(int damage = 1){
        if (isInvulnerable){
            return;
        }
        dataPlayer.currentLifePoints = dataPlayer.currentLifePoints - damage;
        Debug.Log("Perte de "+damage+"pts de vie - Totale à "+ dataPlayer.currentLifePoints);
        if(dataPlayer.currentLifePoints <= 0){
            Debug.Log("sale naze");
            Destroy(gameObject);
        } else {
            StartCoroutine(Invulnerable());
        }
    }

    IEnumerator Invulnerable(){
        Color startColor = sr.color;
        isInvulnerable = true;

        WaitForSeconds invulnerableFlashWait = new WaitForSeconds(invulnerableFlash);
        for (float i = 0; i <= invulnerableTime; i += invulnerableFlash){
            if(sr.color.a == 1){
                sr.color = Color.clear;
            } else {
                sr.color = startColor;
            }
            yield return new WaitForSeconds(invulnerableFlash);
        }
        sr.color = startColor;
        isInvulnerable = false;
    }
}
