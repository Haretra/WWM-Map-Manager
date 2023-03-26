﻿	using UnityEngine;

public class Base : MonoBehaviour {
	public BaseType baseType;
	internal bool enemy = false;
	internal MeshRenderer main;

	public void Initiate(Vector3 position, BaseType baseType) {
		transform.position = new Vector3(position.x, position.y, position.z);
		this.baseType = baseType;

		transform.Find("Main").GetComponent<MeshRenderer>();
		UnitManager.Instance.GetBaseTexture(baseType);
	}

	private Vector3 offset;
	private void OnMouseDown() {
		// Calculate the offset between the object's position and the mouse position
		Vector3 mousePosition = Input.mousePosition;
		mousePosition.z = Camera.main.nearClipPlane;
		offset = transform.position - Camera.main.ScreenToWorldPoint(mousePosition);
	}

	private void OnMouseDrag() {
		if (GameObject.FindWithTag("GameController").GetComponent<ApplicationController>().admin) {
			Vector3 mousePosition = Input.mousePosition;
			mousePosition.z = Camera.main.nearClipPlane;
			transform.position = Camera.main.ScreenToWorldPoint(mousePosition) + offset;
		}
	}
}