using UnityEngine;
using System.Collections;

public class CoverFlow : MonoBehaviour {
	
	//all covers
	public Transform[] covers;
	
	private const float angle = 50.0f;
	
	//center cover area x
	private float centerArea = 0.6f;
	//save current x-axis displacement 
	//change this change cover state
	private float currentX = 0.0f;
	
	private float maxLength;
	
	//delta x-axis length per cover;
	private float dxPerCover = .6f;
	//center cover z-axis offset
	private float offsetZ = .5f;
	void Start () {
		maxLength = (covers.Length-1) * dxPerCover;
		
		currentX = covers.Length/2 * dxPerCover;
		//init cover state
		UpdateCoverState();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateCoverState();
	}
	
	void OnGUI()
	{
		currentX = GUI.HorizontalSlider(new Rect(10,10,Screen.width-10,20), currentX, 0, maxLength);
	}
	
	void UpdateCoverState()
	{
		for( int i = 0 ; i < covers.Length ; ++i)
		{
			float posx = dxPerCover * i - currentX;
			float posz = CalcPosZ(posx);
			Vector3 pos = new Vector3(posx, 0 ,posz);
			covers[i].position = pos;
			Vector3 euler = new Vector3(0, CalcRotation(posx), 0);
			covers[i].eulerAngles = euler;
		}
	}
	
	//calculate rotation of the cover 
	//piecewise function
	//return rotation y axis
	float CalcRotation(float posx)
	{
		//left covers
		if(posx < -centerArea)
		{
			return -angle;
		}
		else 
			if(posx > centerArea) {
			return angle;
		}
		else {
			return posx * (angle/centerArea);	
		}
	}
	//piecewise function
	//calculate cover z-axis offset 
	//return z-axis offset
	float CalcPosZ(float posx)
	{
		if(posx < -centerArea)
		{
			return 0;
		}
		else if (posx < 0) {
			return -offsetZ / centerArea * posx - offsetZ;
		}
		else if (posx < centerArea)
		{
			return offsetZ / centerArea * posx - offsetZ;
		}
		else {
			return 0;
		}
	}
}
