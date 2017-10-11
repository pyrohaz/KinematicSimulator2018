/*
 * Created by SharpDevelop.
 * User: harri
 * Date: 11/10/2017
 * Time: 21:21
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace KinematicSimulator2018
{
	public class Floor
	{
		public Floor(){
			
		}
		
		public Floor(int Start, int End, int Spacing)
		{
			for(int n = Start; n<=End; n+=Spacing){
				nodes.Add(new Vec3(n, Start, 0));
				nodes.Add(new Vec3(n, End, 0));
				nodes.Add(new Vec3(Start, n, 0));
				nodes.Add(new Vec3(End, n, 0));
			}
		}
		
		public List<Vec3> GetNodes(){
			return nodes;
		}
		
		public void Translate(Vec3 Position){
			for(int n = 0; n<nodes.Count; n++){
				nodes[n]=nodes[n].Translate(Position);
			}
		}
		
		List<Vec3> nodes = new List<Vec3>();
	}
}
