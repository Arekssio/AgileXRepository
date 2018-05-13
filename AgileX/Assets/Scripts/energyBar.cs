using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class energyBar : MonoBehaviour {

	public float hp, maxHp = 100f;
	public Image energy;
	// Use this for initialization
	void Start () {
		hp = maxHp;
	}

	public void TakeEnergy(float cantidad) {
		hp = Mathf.Clamp (hp - cantidad, 0f, maxHp);
		energy.transform.localScale = new Vector3(hp / maxHp, 0.83f, 1);
	}

}
