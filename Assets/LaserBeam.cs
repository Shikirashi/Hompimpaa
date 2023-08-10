using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam{
	Vector3 pos, dir;
	GameObject laserObject;
	LineRenderer laser;
	List<Vector3> laserIndices = new List<Vector3>();
	LayerMask layer = 1 << 3;
	public LaserBeam(Vector3 pos, Vector3 dir, Material mat) {
		this.laser = new LineRenderer();
		this.laserObject = new GameObject();
		this.laserObject.name = "Laser Beam";
		this.laserObject.tag = "Laser";
		this.laserObject.layer = 3;
		this.pos = pos;
		this.dir = dir;

		this.laser = this.laserObject.AddComponent(typeof(LineRenderer)) as LineRenderer;
		this.laser.startWidth = 0.1f;
		this.laser.endWidth = 0.1f;
		this.laser.material = mat;
		this.laser.startColor = Color.red;
		this.laser.endColor = Color.red;

		CastRay(pos, dir, laser);
	}

	void UpdateLaser() {
		int count = 0;
		laser.positionCount = laserIndices.Count;
		foreach (Vector3 idx in laserIndices) {
			laser.SetPosition(count, idx);
			count++;
		}
	}

	void CastRay(Vector3 pos, Vector3 dir, LineRenderer laser) {
		laserIndices.Add(pos);

		Ray ray = new Ray(pos, dir);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 30, 1)) {
			CheckHit(hit, dir, laser);
		}
		else {
			laserIndices.Add(ray.GetPoint(30));
			UpdateLaser();
		}
	}

	private void CheckHit(RaycastHit hitInfo, Vector3 dir, LineRenderer laser) {
		if (hitInfo.collider.gameObject.tag == "Mirror") {
			Vector3 pos = hitInfo.point;
			Vector3 direction = Vector3.Reflect(dir, hitInfo.normal);

			CastRay(pos, direction, laser);
		}
		else {
			if (hitInfo.collider.gameObject.tag == "Battery") {
				Debug.Log("Laser hit battery");
				hitInfo.collider.gameObject.GetComponent<ChargeBattery>().isCharging = true;
			}
			laserIndices.Add(hitInfo.point);
			UpdateLaser();
		}
	}
}
