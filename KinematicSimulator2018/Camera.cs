/*
 * Created by SharpDevelop.
 * User: harri
 * Date: 11/10/2017
 * Time: 21:04
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace KinematicSimulator2018
{
	/// <summary>
	/// Description of Camera.
	/// </summary>
	
	public enum HandlerTypes{
		MouseUp,
		MouseDown,
		MouseMove,
		MouseWheel,
		KeyUp,
		KeyDown
	}
	
	public class Camera
	{
		
		public Camera()
		{
			position = new Vec3(0,0,0);
			angle = new Vec3(0,0,0);
			cameramove = false;
			camerarotate = false;
			cameraenable = false;
		}
		
		public Camera(Vec3 Position, Vec3 Angle){
			position = Position;
			angle = Angle;
		}
		
		public Vec3 GetPosition(){
			return position;
		}
		
		public Vec3 GetAngle(){
			return angle;
		}
		
		public bool GetEnable(){
			return cameramove | cameraenable;	
		}
		
		public void SetPosition(Vec3 Position){
			position = Position;
		}
		
		public void SetAngle(Vec3 Angle){
			angle = Angle;
		}
		
		public void CameraHandler(HandlerTypes type, EventArgs e){
			if(type == HandlerTypes.KeyDown){
				ctrl = ((KeyEventArgs)e).Control;
				alt = ((KeyEventArgs)e).Alt;
				if(((KeyEventArgs)e).KeyCode == Keys.Space){
					position.SetZ(0);
				}
			}
			else if(type == HandlerTypes.KeyUp){
				ctrl = false;
				alt = false;
			}
			else if(type == HandlerTypes.MouseDown){
				cameramove = ctrl;
				camerarotate = alt;
				if(cameraenable) Cursor.Current = Cursors.NoMove2D;
			}
			else if(type == HandlerTypes.MouseUp){
				cameramove = false;
				camerarotate = false;
				Cursor.Current = Cursors.Default;
			}
			else if(type == HandlerTypes.MouseMove){
				double dx, dy;
				
				dx = ((MouseEventArgs)e).Location.X-mouselast.X;
				dy = mouselast.Y-((MouseEventArgs)e).Location.Y;
				
				if(cameramove){
					angle.SetX(angle.GetX()-dy*0.01);
					angle.SetZ(angle.GetZ()+dx*0.01);
				}
				else if(camerarotate){
					angle.SetY(angle.GetY()+dx*0.01);
				}
				
				mouselast = ((MouseEventArgs)e).Location;
			}
			else if(type == HandlerTypes.MouseWheel){
				double scroll = ((MouseEventArgs)e).Delta*SystemInformation.MouseWheelScrollLines*0.02;
				double posnew = position.GetZ()+scroll;
				if(posnew<0) posnew = 0;
				if(posnew>100) posnew = maxzoom;
				position.SetZ(posnew);
			}
			
			//Debug.WriteLine(cameraenable + " " + cameramove + " " + camerarotate);
			//Debug.WriteLine(ctrl + " " + alt);
		}
		
		public double GetMaxZoom(){
			return maxzoom;
		}
		
		Vec3 position, angle;
		Point mouselast;
		bool camerarotate, cameramove, cameraenable;
		bool ctrl, alt;
		double maxzoom = 100;
	}
}

