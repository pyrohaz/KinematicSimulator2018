/*
 * Created by SharpDevelop.
 * User: harri
 * Date: 09/10/2017
 * Time: 19:29
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace KinematicSimulator2018
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.panel = new System.Windows.Forms.Panel();
			this.button_delete = new System.Windows.Forms.Button();
			this.checkBox_add = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textBox_x = new System.Windows.Forms.TextBox();
			this.textBox_y = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textBox_z = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.button_setnode = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// panel
			// 
			this.panel.BackColor = System.Drawing.Color.White;
			this.panel.Location = new System.Drawing.Point(12, 12);
			this.panel.Name = "panel";
			this.panel.Size = new System.Drawing.Size(640, 480);
			this.panel.TabIndex = 1;
			// 
			// button_delete
			// 
			this.button_delete.Location = new System.Drawing.Point(659, 13);
			this.button_delete.Name = "button_delete";
			this.button_delete.Size = new System.Drawing.Size(75, 23);
			this.button_delete.TabIndex = 2;
			this.button_delete.Text = "Delete";
			this.button_delete.UseVisualStyleBackColor = true;
			this.button_delete.Click += new System.EventHandler(this.Button_deleteClick);
			// 
			// checkBox_add
			// 
			this.checkBox_add.Location = new System.Drawing.Point(659, 43);
			this.checkBox_add.Name = "checkBox_add";
			this.checkBox_add.Size = new System.Drawing.Size(104, 24);
			this.checkBox_add.TabIndex = 3;
			this.checkBox_add.Text = "Add Node";
			this.checkBox_add.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(659, 108);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 23);
			this.label1.TabIndex = 4;
			this.label1.Text = "Node";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(659, 131);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(18, 23);
			this.label2.TabIndex = 5;
			this.label2.Text = "X:";
			// 
			// textBox_x
			// 
			this.textBox_x.Location = new System.Drawing.Point(686, 128);
			this.textBox_x.Name = "textBox_x";
			this.textBox_x.Size = new System.Drawing.Size(73, 20);
			this.textBox_x.TabIndex = 6;
			// 
			// textBox_y
			// 
			this.textBox_y.Location = new System.Drawing.Point(686, 154);
			this.textBox_y.Name = "textBox_y";
			this.textBox_y.Size = new System.Drawing.Size(73, 20);
			this.textBox_y.TabIndex = 8;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(659, 157);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(18, 23);
			this.label3.TabIndex = 7;
			this.label3.Text = "Y:";
			// 
			// textBox_z
			// 
			this.textBox_z.Location = new System.Drawing.Point(686, 180);
			this.textBox_z.Name = "textBox_z";
			this.textBox_z.Size = new System.Drawing.Size(73, 20);
			this.textBox_z.TabIndex = 10;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(659, 183);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(18, 23);
			this.label4.TabIndex = 9;
			this.label4.Text = "Z:";
			// 
			// button_setnode
			// 
			this.button_setnode.Location = new System.Drawing.Point(658, 209);
			this.button_setnode.Name = "button_setnode";
			this.button_setnode.Size = new System.Drawing.Size(75, 23);
			this.button_setnode.TabIndex = 11;
			this.button_setnode.Text = "Set Node";
			this.button_setnode.UseVisualStyleBackColor = true;
			this.button_setnode.Click += new System.EventHandler(this.Button_setnodeClick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(769, 505);
			this.Controls.Add(this.button_setnode);
			this.Controls.Add(this.textBox_z);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textBox_y);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textBox_x);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.checkBox_add);
			this.Controls.Add(this.button_delete);
			this.Controls.Add(this.panel);
			this.KeyPreview = true;
			this.Name = "MainForm";
			this.Text = "KinematicSimulator2018";
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.Button button_setnode;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBox_z;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBox_y;
		private System.Windows.Forms.TextBox textBox_x;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox checkBox_add;
		private System.Windows.Forms.Button button_delete;
		private System.Windows.Forms.Panel panel;
	}
}
