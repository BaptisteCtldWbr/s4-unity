using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxLifePoints = 3;
    public int currentLifePoints = 3;
    public bool isInvulnerable = false;
    public float invulnerableTime = 2.25f;
    public float invulnerableFlash = 0.2f;
    public SpriteRenderer sr;

    void Start(){
        currentLifePoints = maxLifePoints;
    }

    public void Hurt(int damage = 1){
        if (isInvulnerable){
            return;
        }
        currentLifePoints = currentLifePoints - damage;
        Debug.Log("Perte de "+damage+"pts de vie - Totale à "+ currentLifePoints);
        if(currentLifePoints <= 0){
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
