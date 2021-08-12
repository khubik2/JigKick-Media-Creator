
namespace Jig_Replicator
{
	partial class Form3
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form3));
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.radioButton2 = new System.Windows.Forms.RadioButton();
			this.button1 = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.button2 = new System.Windows.Forms.Button();
			this.radioButton3 = new System.Windows.Forms.RadioButton();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.button4 = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.checkBox8 = new System.Windows.Forms.CheckBox();
			this.radioButton4 = new System.Windows.Forms.RadioButton();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(80, 41);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(0, 13);
			this.label2.TabIndex = 6;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.Location = new System.Drawing.Point(96, 25);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(250, 29);
			this.label1.TabIndex = 5;
			this.label1.Text = "JigKick Media Creator";
			// 
			// label3
			// 
			this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label3.Location = new System.Drawing.Point(12, 77);
			this.label3.MinimumSize = new System.Drawing.Size(0, 2);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(402, 2);
			this.label3.TabIndex = 7;
			// 
			// radioButton2
			// 
			this.radioButton2.AutoSize = true;
			this.radioButton2.Location = new System.Drawing.Point(12, 128);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.Size = new System.Drawing.Size(146, 43);
			this.radioButton2.TabIndex = 10;
			this.radioButton2.TabStop = true;
			this.radioButton2.Text = "Create a JigKick clone \r\nwith provided MSID\r\n(for TA-088v3 and newer)";
			this.radioButton2.UseVisualStyleBackColor = true;
			this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
			// 
			// button1
			// 
			this.button1.Enabled = false;
			this.button1.Location = new System.Drawing.Point(350, 139);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(62, 20);
			this.button1.TabIndex = 14;
			this.button1.Text = "Browse...";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// textBox1
			// 
			this.textBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.textBox1.Enabled = false;
			this.textBox1.ForeColor = System.Drawing.Color.BlanchedAlmond;
			this.textBox1.Location = new System.Drawing.Point(164, 140);
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new System.Drawing.Size(182, 20);
			this.textBox1.TabIndex = 15;
			this.textBox1.Text = "msid.bin file...";
			this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.button2.Location = new System.Drawing.Point(12, 254);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(403, 51);
			this.button2.TabIndex = 18;
			this.button2.Text = "Start Media Creation!";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// radioButton3
			// 
			this.radioButton3.AutoSize = true;
			this.radioButton3.Location = new System.Drawing.Point(12, 83);
			this.radioButton3.Name = "radioButton3";
			this.radioButton3.Size = new System.Drawing.Size(255, 30);
			this.radioButton3.TabIndex = 20;
			this.radioButton3.TabStop = true;
			this.radioButton3.Text = "Create a JigKick clone (for TA-088v3 and newer)\r\nwith pre-set MSID for microSD ad" +
    "apters";
			this.radioButton3.UseVisualStyleBackColor = true;
			this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new System.Drawing.Point(13, 205);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(210, 17);
			this.checkBox1.TabIndex = 21;
			this.checkBox1.Text = "Manual device select (for cardreaders):";
			this.checkBox1.UseVisualStyleBackColor = true;
			this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
			// 
			// comboBox1
			// 
			this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox1.Enabled = false;
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new System.Drawing.Point(13, 227);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(401, 21);
			this.comboBox1.TabIndex = 22;
			this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.ForeColor = System.Drawing.Color.DarkCyan;
			this.label5.Location = new System.Drawing.Point(8, 308);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(190, 13);
			this.label5.TabIndex = 23;
			this.label5.Text = "Waiting for PSP Memory Stick mount...";
			this.label5.TextChanged += new System.EventHandler(this.label5_TextChanged);
			this.label5.Click += new System.EventHandler(this.label5_Click);
			// 
			// checkBox2
			// 
			this.checkBox2.AutoSize = true;
			this.checkBox2.Location = new System.Drawing.Point(326, 205);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.checkBox2.Size = new System.Drawing.Size(89, 17);
			this.checkBox2.TabIndex = 25;
			this.checkBox2.Text = "Quick Format";
			this.checkBox2.UseVisualStyleBackColor = true;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::Jig_Replicator.Properties.Resources.Colex600;
			this.pictureBox1.Location = new System.Drawing.Point(2, -1);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(78, 78);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 1;
			this.pictureBox1.TabStop = false;
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(273, 94);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(142, 23);
			this.button4.TabIndex = 28;
			this.button4.Text = "Select your adapter here...";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(27, 112);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(91, 13);
			this.label6.TabIndex = 29;
			this.label6.Text = "Selected adapter:";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(115, 112);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(31, 13);
			this.label7.TabIndex = 30;
			this.label7.Text = "none";
			// 
			// checkBox8
			// 
			this.checkBox8.AutoSize = true;
			this.checkBox8.Location = new System.Drawing.Point(13, 327);
			this.checkBox8.Name = "checkBox8";
			this.checkBox8.Size = new System.Drawing.Size(407, 17);
			this.checkBox8.TabIndex = 0;
			this.checkBox8.Text = "Enable FAT32 large capacity cheat (manual format with downloaded fat32format)\r\n";
			this.checkBox8.UseVisualStyleBackColor = true;
			this.checkBox8.CheckedChanged += new System.EventHandler(this.checkBox8_CheckedChanged);
			// 
			// radioButton4
			// 
			this.radioButton4.AutoSize = true;
			this.radioButton4.Location = new System.Drawing.Point(12, 177);
			this.radioButton4.Name = "radioButton4";
			this.radioButton4.Size = new System.Drawing.Size(391, 17);
			this.radioButton4.TabIndex = 31;
			this.radioButton4.TabStop = true;
			this.radioButton4.Text = "Install Despertar del Cementerio 9.00 (5.00 + M33-7, for TA-088v3 and newer)";
			this.radioButton4.UseVisualStyleBackColor = true;
			// 
			// Form3
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(426, 328);
			this.Controls.Add(this.radioButton4);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.checkBox8);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.radioButton2);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.checkBox2);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.checkBox1);
			this.Controls.Add(this.radioButton3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.pictureBox1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(442, 390);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(442, 367);
			this.Name = "Form3";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Media Creator";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form3_FormClosing);
			this.Load += new System.EventHandler(this.Form3_Load);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.RadioButton radioButton2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.RadioButton radioButton3;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.CheckBox checkBox2;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.CheckBox checkBox8;
		private System.Windows.Forms.RadioButton radioButton4;
	}
}