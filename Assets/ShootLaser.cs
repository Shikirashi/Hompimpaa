using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLaser : MonoBehaviour
{
	public Material mat;
	LaserBeam beam;

	private void Update() {
		Destroy(GameObject.Find("Laser Beam"));
		beam = new LaserBeam(gameObject.transform.position, -gameObject.transform.up, mat);
	}
}
