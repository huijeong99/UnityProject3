using UnityEngine;
using System.Collections;

public class SpikesCeiling : MonoBehaviour {

	public Animator SpikesCeilingAnimator;
	private bool On = true;

	void OnTriggerEnter(Collider col){

		if(col.gameObject.tag == "Player" && On){
			On = false;
			this.audio.Play();
			this.SpikesCeilingAnimator.SetTrigger("Enable");
			Destroy(this, 10f);

		}


	}
}
