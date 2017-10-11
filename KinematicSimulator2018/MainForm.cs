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
		List<Vec3> nodes2 = new List<Vec3>();
		Camera camera = new Camera(new Vec3(0,0,1), new Vec3(-Math.PI/4, 0, Math.PI/4));
		Floor floor = new Floor(-100, 100, 5);
		
		Timer gfxtimer = new Timer();
		
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
			
			gfxtimer.Tick += delegate { panel.Invalidate(); };
			gfxtimer.Interval = 10;
			gfxtimer.Start();
			
			nodes.Add(new Vec3(-1,-1,0));
			nodes.Add(new Vec3(1,-1,0));
			nodes.Add(new Vec3(1,1,0));
			nodes.Add(new Vec3(-1,1,0));
		}
		
		void panel_MouseDown(object Sender, MouseEventArgs e){
			camera.CameraHandler(HandlerTypes.MouseDown, e);
		}
		
		void panel_MouseUp(object Sender, MouseEventArgs e){
			camera.CameraHandler(HandlerTypes.MouseUp, e);
		}
		
		void panel_MouseMove(object Sender, MouseEventArgs e){
			camera.CameraHandler(HandlerTypes.MouseMove, e);
		}
		
		void panel_MouseWheel(object Sender, MouseEventArgs e){
			camera.CameraHandler(HandlerTypes.MouseWheel, e);
		}
		
		void MainForm_KeyDown(object Sender, KeyEventArgs e){
			camera.CameraHandler(HandlerTypes.KeyDown, e);
		}
		
		void MainForm_KeyUp(object Sender, KeyEventArgs e){
			camera.CameraHandler(HandlerTypes.KeyUp, e);
		}
		
		double floorpos = 0;
		void panel_Paint(object Sender, PaintEventArgs e){
			SolidBrush brush = new SolidBrush(Color.Green);
			Pen pen = new Pen(Color.Blue);
			Graphics g = e.Graphics;
			
			if(Math.Cos(camera.GetAngle().GetX())>0) g.Clear(Color.WhiteSmoke); 
			else g.Clear(Color.LightGray);
			
			nodes2.Clear();
			foreach(Vec3 node in nodes){
				Vec3 projnode = node.Project(camera);
				//projnode *= camera.GetPosition().GetZ();
				projnode.SetX(projnode.GetX()+panel.Width/2);
				projnode.SetY(panel.Height-(projnode.GetY()+panel.Height/2));
				
				nodes2.Add(projnode);
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
			
			foreach(Vec3 node in nodes2){
				g.FillEllipse(brush, (float)node.GetX(), (float)node.GetY(), 5, 5);
			}
			
			for(int n = 0; n<nodes2.Count+1; n++){
				int idx1 = (n%nodes2.Count);
				int idx2 = ((n-1)%nodes2.Count);
				if(idx1>nodes2.Count) idx1 -= nodes2.Count;
				if(idx2<0) idx2 += nodes2.Count;
				
				g.DrawLine(pen, (float)nodes2[idx1].GetX(), (float)nodes2[idx1].GetY(), (float)nodes2[idx2].GetX(), (float)nodes2[idx2].GetY());
			}
		}
	}
}

