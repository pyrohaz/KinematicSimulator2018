/*
 * Created by SharpDevelop.
 * User: harri
 * Date: 09/10/2017
 * Time: 19:29
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace KinematicSimulator2018
{
	public partial class MainForm : Form
	{
		List<Vec3> nodes = new List<Vec3>();
		List<Vec3> projectednodes = new List<Vec3>();
		int nodeselindex = -1;
		Camera camera = new Camera(new Vec3(0,0,0), new Vec3(-Math.PI/4, 0, Math.PI/4));
		Floor floor = new Floor(-100, 100, 2);
		
		public MainForm()
		{
			InitializeComponent();
			
			typeof(Panel).InvokeMember("DoubleBuffered",
			                           BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
			                           null, panel, new object[] { true });
			
			panel.Paint += new PaintEventHandler(panel_Paint);
			panel.MouseDown += new MouseEventHandler(panel_MouseDown);
			panel.MouseMove += new MouseEventHandler(panel_MouseMove);
			panel.MouseUp += new MouseEventHandler(panel_MouseUp);
			panel.MouseWheel += new MouseEventHandler(panel_MouseWheel);
			this.KeyDown += new KeyEventHandler(MainForm_KeyDown);
			this.KeyUp += new KeyEventHandler(MainForm_KeyUp);
			
			nodes.Add(new Vec3(-1,-1,0));
			nodes.Add(new Vec3(1,-1,0));
			nodes.Add(new Vec3(1,1,1));
			nodes.Add(new Vec3(-1,1,1));
		}
		
		int PointDistance(Point p1, Point p2){
			return (int)Math.Sqrt(Math.Pow(p1.X-p2.X, 2) + Math.Pow(p1.Y-p2.Y, 2));
		}
		
		void panel_MouseDown(object Sender, MouseEventArgs e){
			if(!checkBox_add.Checked){
				camera.CameraHandler(HandlerTypes.MouseDown, e);
				
				if(!camera.GetEnable()){
					nodeselindex = -1;
					for(int n = 0; n<projectednodes.Count; n++){
						Point node = new Point((int)projectednodes[n].GetX(), (int)projectednodes[n].GetY());
						if(PointDistance(e.Location, node)<10){
							nodeselindex = n;
							
							textBox_x.Text = nodes[n].GetX().ToString();
							textBox_y.Text = nodes[n].GetY().ToString();
							textBox_z.Text = nodes[n].GetZ().ToString();
							
							break;
						}
					}
				}
			}
			else{
				//Add a node at mouse point
				double px, py, pz;
				px = (e.Location.X-panel.Width/2)/(camera.GetMaxZoom()-camera.GetPosition().GetZ());
				py = (panel.Height/2-e.Location.Y)/(camera.GetMaxZoom()-camera.GetPosition().GetZ());
				pz = camera.GetPosition().GetZ();
				nodes.Add(new Vec3(px,py,pz));
			}
		}
		
		
		
		void panel_MouseUp(object Sender, MouseEventArgs e){
			camera.CameraHandler(HandlerTypes.MouseUp, e);
			panel.Invalidate();
		}
		
		void panel_MouseMove(object Sender, MouseEventArgs e){
			panel.Invalidate();
			camera.CameraHandler(HandlerTypes.MouseMove, e);
		}
		
		void panel_MouseWheel(object Sender, MouseEventArgs e){
			panel.Invalidate();
			camera.CameraHandler(HandlerTypes.MouseWheel, e);
		}
		
		void MainForm_KeyDown(object Sender, KeyEventArgs e){
			panel.Invalidate();
			camera.CameraHandler(HandlerTypes.KeyDown, e);
		}
		
		void MainForm_KeyUp(object Sender, KeyEventArgs e){
			panel.Invalidate();
			camera.CameraHandler(HandlerTypes.KeyUp, e);
		}
		
		void panel_Paint(object Sender, PaintEventArgs e){
			SolidBrush brush = new SolidBrush(Color.Green);
			Pen pen = new Pen(Color.Blue);
			Graphics g = e.Graphics;
			
			if(Math.Cos(camera.GetAngle().GetX())>0) g.Clear(Color.WhiteSmoke);
			else g.Clear(Color.LightGray);
			
			projectednodes.Clear();
			foreach(Vec3 node in nodes){
				Vec3 projnode = node.Project(camera);
				projnode.SetX(projnode.GetX()+panel.Width/2);
				projnode.SetY(panel.Height-(projnode.GetY()+panel.Height/2));
				
				projectednodes.Add(projnode);
			}
			
			//Render floor
			Pen linepen = new Pen(Color.Gray);
			for(int n = 0; n<floor.GetNodes().Count; n+=4){
				Vec3 n1 = floor.GetNodes()[n].Project(camera);
				Vec3 n2 = floor.GetNodes()[n+1].Project(camera);
				
				n1.SetX(n1.GetX()+panel.Width/2);
				n1.SetY(panel.Height-(n1.GetY()+panel.Height/2));
				n2.SetX(n2.GetX()+panel.Width/2);
				n2.SetY(panel.Height-(n2.GetY()+panel.Height/2));
				
				g.DrawLine(linepen, (float)n1.GetX(), (float)n1.GetY(), (float)n2.GetX(), (float)n2.GetY());
				
				n1 = floor.GetNodes()[n+2].Project(camera);
				n2 = floor.GetNodes()[n+3].Project(camera);
				
				n1.SetX(n1.GetX()+panel.Width/2);
				n1.SetY(panel.Height-(n1.GetY()+panel.Height/2));
				n2.SetX(n2.GetX()+panel.Width/2);
				n2.SetY(panel.Height-(n2.GetY()+panel.Height/2));
				
				g.DrawLine(linepen, (float)n1.GetX(), (float)n1.GetY(), (float)n2.GetX(), (float)n2.GetY());
			}
			
			for(int n = 0; n<projectednodes.Count; n++){
				if(nodeselindex == n) brush.Color = Color.Green;
				else brush.Color = Color.Red;
				
				g.FillEllipse(brush, (float)projectednodes[n].GetX()-3, (float)projectednodes[n].GetY()-3, 6, 6);
			}
			
			if(projectednodes.Count>0){
				for(int n = 0; n<projectednodes.Count+1; n++){
					int idx1 = (n%projectednodes.Count);
					int idx2 = ((n-1)%projectednodes.Count);
					if(idx1>projectednodes.Count) idx1 -= projectednodes.Count;
					if(idx2<0) idx2 += projectednodes.Count;
					
					g.DrawLine(pen, (float)projectednodes[idx1].GetX(), (float)projectednodes[idx1].GetY(), (float)projectednodes[idx2].GetX(), (float)projectednodes[idx2].GetY());
				}
			}
		}
		
		void Button_setnodeClick(object sender, EventArgs e)
		{
			if(nodeselindex!=-1){
				double val;
				if(double.TryParse(textBox_x.Text, out val)){
					nodes[nodeselindex].SetX(val);
				}
				if(double.TryParse(textBox_y.Text, out val)){
					nodes[nodeselindex].SetY(val);
				}
				if(double.TryParse(textBox_z.Text, out val)){
					nodes[nodeselindex].SetZ(val);
				}
				Debug.WriteLine(nodes[nodeselindex]);
				panel.Invalidate();
			}
		}
		
		void Button_deleteClick(object sender, EventArgs e)
		{
			if(nodeselindex!=-1 && nodes.Count>0){
				nodes.RemoveAt(nodeselindex);
				panel.Invalidate();
				nodeselindex = -1;
			}
		}
	}
}