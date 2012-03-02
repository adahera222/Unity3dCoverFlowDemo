using UnityEngine;
using System.Collections;

public class CoverFlow : MonoBehaviour {
	
	//all covers
	public Transform[] covers;
	
	private const float angle = 50.0f;
	
	//center cover area x
	private float centerArea = 1.0f;
	//save current x-axis displacement 
	//change this change cover state
	private float currentX = 0.0f;
	
	private float maxLength;
	
	//delta x-axis length per cover;
	private float dxPerCover = 1.0f;
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
			float offsetx = dxPerCover * i - currentX;
			float posx = CalcPosX(offsetx);
			float posz = CalcPosZ(offsetx);
			Vector3 pos = new Vector3(posx, 0 ,posz);
			covers[i].position = pos;
			Vector3 euler = new Vector3(0, CalcRotation(offsetx), 0);
			covers[i].eulerAngles = euler;
		}
	}
	
	//calculate rotation of the cover 
	//piecewise function
	//return rotation y axis
	float CalcRotation(float offsetx)
	{
		//left covers
		if(offsetx < -centerArea)
		{
			return -angle;
		}
		else 
			if(offsetx > centerArea) {
			return angle;
		}
		else {
			return offsetx * (angle/centerArea);	
		}
	}
	//piecewise function
	//calculate cover z-axis offset 
	//return z-axis offset
	float CalcPosZ(float offsetx)
	{
		if(offsetx < -centerArea)
		{
			return 0;
		}
		else if (offsetx < 0) {
			return -offsetZ / centerArea * offsetx - offsetZ;
		}
		else if (offsetx < centerArea)
		{
			return offsetZ / centerArea * offsetx - offsetZ;
		}
		else {
			return 0;
		}
	}
	//calculate real position on x-axis
	float CalcPosX(float offsetx)
	{
		if(offsetx >=1.0f) {
			return  Mathf.Sqrt(2*offsetx - 1);
		}else if( offsetx <= -1.0f){
			return - Mathf.Sqrt(-2 * offsetx - 1);
		}else 
			return offsetx;
	}
	
	//inverse function of calcpox 
	//this calculate offsetx by giving real position on x-axis
	float CalcPosXInverse(float realPosx)
	{
		if(realPosx < -1.0f) {
			return - (realPosx*realPosx +1)/2;
		} else if( realPosx < 1.0f) {
			return realPosx;
		}else {
			return (realPosx * realPosx + 1) / 2;
		}
 	}
	
	void StartAnimation(float targetoffsetx)
	{
		float offset = CalcPosXInverse(targetoffsetx);
		iTween.ValueTo(gameObject, iTween.Hash("from",currentX, "to", currentX + offset, "time",0.5, "easetype",iTween.EaseType.easeOutCubic,"onupdate","UpdateAnimation"));
	}
	
	void UpdateAnimation(float pos)
	{
		currentX = pos;
	}
}
