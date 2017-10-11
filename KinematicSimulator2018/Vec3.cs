/*
 * Created by SharpDevelop.
 * User: harri
 * Date: 09/10/2017
 * Time: 19:32
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Diagnostics;

namespace KinematicSimulator2018
{
	/// <summary>
	/// Description of Vec3.
	/// </summary>
	public class Vec3
	{
		public Vec3(){
			x = y = z = 0.0;
		}
		
		public Vec3(double X, double Y, double Z){
			x = X;
			y = Y;
			z = Z;
		}
		
		public override string ToString()
		{
			return string.Format("[Vec3 X={0}, Y={1}, Z={2}]", x, y, z);
		}

		
		public double GetX(){ return x; }
		public double GetY(){ return y; }
		public double GetZ(){ return z; }
		
		public void SetX(double X){ x = X; }
		public void SetY(double Y){ y = Y; }
		public void SetZ(double Z){ z = Z; }
		
		public static Vec3 operator *(Vec3 n1, double m){
			return new Vec3(n1.x*m, n1.y*m, n1.z*m);
		}
		
		public static Vec3 operator /(Vec3 n1, double m){
			return new Vec3(n1.x/m, n1.y/m, n1.z/m);
		}
		
		public static Vec3 operator +(Vec3 n1, double m){
			return new Vec3(n1.x+m, n1.y+m, n1.z+m);
		}
		
		public static Vec3 operator-(Vec3 n1, Vec3 n2){
			return new Vec3(n1.x-n2.x, n1.y-n2.y, n1.z-n2.z);
		}
		
		public Vec3 RotX(double angle){
			return new Vec3(x, y*Math.Cos(angle)-z*Math.Sin(angle), y*Math.Sin(angle)+Math.Cos(angle));
		}
		
		public Vec3 RotY(double angle){
			return new Vec3(x*Math.Cos(angle)+z*Math.Sin(angle), y, -x*Math.Sin(angle)+z*Math.Cos(angle));
		}
		
		public Vec3 RotZ(double angle){
			return new Vec3(x*Math.Cos(angle) - y*Math.Sin(angle), x*Math.Sin(angle)+y*Math.Cos(angle), z);
		}
		
		public Vec3 Translate(Vec3 pos){
			return new Vec3(x-pos.x, y-pos.y, z-pos.z);
		}
		
		public Vec3 Project(Camera camera){
			Vec3 projvec = new Vec3(x,y,z).RotZ(camera.GetAngle().z).RotY(camera.GetAngle().y).RotX(camera.GetAngle().x);
			projvec.x *= camera.GetMaxZoom()-camera.GetPosition().GetZ();
			projvec.y *= camera.GetMaxZoom()-camera.GetPosition().GetZ();
			return projvec;
		}
		
		public Vec3 ProjectZoom(Camera camera, double zoom){
			Vec3 projvec = new Vec3(x,y,z).RotZ(camera.GetAngle().z).RotY(camera.GetAngle().y).RotX(camera.GetAngle().x);
			projvec.x *= zoom;
			projvec.y *= zoom;
			return projvec;
		}
		
		double x, y, z;
	}
}
