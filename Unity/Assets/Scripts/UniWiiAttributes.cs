using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

public class UniWiiAttributes : MonoBehaviour {

	[DllImport ("UniWii")]
	private static extern void wiimote_start();
	[DllImport ("UniWii")]
	private static extern void wiimote_stop();
	
	[DllImport ("UniWii")]
	private static extern int wiimote_count();
	[DllImport ("UniWii")]
	private static extern bool wiimote_available( int which );
	[DllImport ("UniWii")]
	private static extern bool wiimote_isIRenabled( int which );
	[DllImport ("UniWii")]	
	private static extern bool wiimote_enableIR( int which );

	[DllImport ("UniWii")]
	private static extern void wiimote_rumble( int which, float duration);
	[DllImport ("UniWii")]
	private static extern double wiimote_getBatteryLevel( int which );
	
	[DllImport ("UniWii")]
	private static extern byte wiimote_getAccX(int which);
	[DllImport ("UniWii")]
	private static extern byte wiimote_getAccY(int which);
	[DllImport ("UniWii")]
	private static extern byte wiimote_getAccZ(int which);

	[DllImport ("UniWii")]
	private static extern float wiimote_getIrX(int which);
	[DllImport ("UniWii")]
	private static extern float wiimote_getIrY(int which);
	[DllImport ("UniWii")]
	private static extern float wiimote_getRoll(int which);
	[DllImport ("UniWii")]
	private static extern float wiimote_getPitch(int which);
	[DllImport ("UniWii")]
	private static extern float wiimote_getYaw(int which);

	[DllImport ("UniWii")]
	private static extern bool wiimote_getButtonA(int which);
	[DllImport ("UniWii")]
	private static extern bool wiimote_getButtonB(int which);
	[DllImport ("UniWii")]
	private static extern bool wiimote_getButtonUp(int which);
	[DllImport ("UniWii")]
	private static extern bool wiimote_getButtonLeft(int which);
	[DllImport ("UniWii")]
	private static extern bool wiimote_getButtonRight(int which);
	[DllImport ("UniWii")]
	private static extern bool wiimote_getButtonDown(int which);
	[DllImport ("UniWii")]
	private static extern bool wiimote_getButton1(int which);
	[DllImport ("UniWii")]
	private static extern bool wiimote_getButton2(int which);
	[DllImport ("UniWii")]
	private static extern bool wiimote_getButtonNunchuckC(int which);
	[DllImport ("UniWii")]
	private static extern bool wiimote_getButtonNunchuckZ(int which);
	[DllImport ("UniWii")]
	private static extern bool wiimote_getButtonPlus(int which);
	[DllImport ("UniWii")]
	private static extern bool wiimote_getButtonMinus(int which);

	[DllImport ("UniWii")]	
	private static extern bool wiimote_setLed( int which );

	private string display;
	private int cursor_x, cursor_y;
	private Texture2D cursor_tex;
	private Vector3 oldVec;

	// Use this for initialization
	void Start () {
		wiimote_start();
	}
	
	// Update is called once per frame
	void Update () {
		int c = wiimote_count();
		if (c>0) {
			display = "";
			for (int i=0; i<=c-1; i++) {
				int x = wiimote_getAccX(i);
				int y = wiimote_getAccY(i);
				int z = wiimote_getAccZ(i);
				float roll = Mathf.Round(wiimote_getRoll(i));
				float p = Mathf.Round(wiimote_getPitch(i));
				float yaw = Mathf.Round(wiimote_getYaw(i));
				float ir_x = wiimote_getIrX(i);
				float ir_y = wiimote_getIrY(i);
				display += "Wiimote " + i + " accX: " + x + " accY: " + y + " accZ: " + z + " roll: " + roll + " pitch: " + p + " yaw: " + yaw + " IR X: " + ir_x + " IR Y: " + ir_y + "\n";
				if (!float.IsNaN(roll) && !float.IsNaN(p) && (i==c-1)) {
					Vector3 vec = new Vector3(p, 0 , -1 * roll);
					vec = Vector3.Lerp(oldVec, vec, Time.deltaTime * 5);
					oldVec = vec;
					GameObject.Find("wiiparent").transform.eulerAngles = vec;
				}
				if ( (i==c-1) && (ir_x != -100) && (ir_y != -100) ) {
					//float temp_x = ((ir_x + (float) 1.0)/ (float)2.0) * (float) Screen.width;
					//float temp_y = (float) Screen.height - (((ir_y + (float) 1.0)/ (float)2.0) * (float) Screen.height);
					float temp_x = ( Screen.width / 2) + ir_x * (float) Screen.width / (float)2.0;
					float temp_y = Screen.height - (ir_y * (float) Screen.height / (float)2.0);
					cursor_x = Mathf.RoundToInt(temp_x);
					cursor_y = Mathf.RoundToInt(temp_y);
				}
			}
		}
		else display = "Press the '1' and '2' buttons on your Wii Remote.";

		wiimote_rumble(0, 1.0f);
		wiimote_setLed(1);
	}

	void OnApplicationQuit () {
		wiimote_stop();
	}

	void OnGUI() {
		GUI.Label( new Rect(10,10, 500, 100), display);
		if ((cursor_x != 0) || (cursor_y != 0)) GUI.Box ( new Rect (cursor_x, cursor_y, 50, 50), cursor_tex); //"Pointing\nHere");
		int c = wiimote_count();
		for (int i=0; i<=c-1; i++) {
			float ir_x = wiimote_getIrX(i);
			float ir_y = wiimote_getIrY(i);
			if ( (ir_x != -100) && (ir_y != -100) ) {
				float temp_x = ((ir_x + (float) 1.0)/ (float)2.0) * (float) Screen.width;
				float temp_y = (float) Screen.height - (((ir_y + (float) 1.0)/ (float)2.0) * (float) Screen.height);
				temp_x = Mathf.RoundToInt(temp_x);
				temp_y = Mathf.RoundToInt(temp_y);
				//if ((cursor_x != 0) || (cursor_y != 0))
				GUI.Box ( new Rect (temp_x, temp_y, 64, 64), "Pointer " + i);
			}
		}
	}
}
