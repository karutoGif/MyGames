using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_UI : MonoBehaviour{

	//sistema de dano
    public float life = 1;
	public GameObject indicator;

	//sistema de balas
	public GameObject[] bullets;
	public int bulletCount;

	//sistema de reload
	public Color baseColorBullets;

	public void reloadWeapon() {
		for (int i = 0; i < this.bullets.Length; i++){
			if(this.bullets[i].gameObject.GetComponent<SpriteRenderer>().color == Color.black) {
				this.bullets[i].gameObject.GetComponent<SpriteRenderer>().color = baseColorBullets;
				bulletCount = 0;
			}
		}
	}

	public void attBullets() {
		if (bulletCount < bullets.Length){
			bullets[bulletCount].gameObject.GetComponent<SpriteRenderer>().color = Color.black;			
			bulletCount++;
		}
		
	}

	public void attHelthBar(float damage) { //dano de 0 a 1
		life -= damage;
		if(life > 0)
			indicator.transform.localScale = new Vector2(indicator.transform.localScale.x, indicator.transform.localScale.y - damage);
	}
}
