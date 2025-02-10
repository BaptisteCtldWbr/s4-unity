using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxLifePoints = 3;
    public int currentLifePoints = 3;

    void Start(){
        currentLifePoints = maxLifePoints;
    }

    public void Hurt(int damage = 1){
        currentLifePoints = currentLifePoints - damage;
        Debug.Log("Perte de "+damage+"pts de vie - Totale à "+ currentLifePoints);
        if(currentLifePoints <= 0){
            Debug.Log("sale naze");
            Destroy(gameObject);
        }
    }
}
