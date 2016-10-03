namespace CafmConnectObjectsGui
{
    partial class Form
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
            this.label1 = new System.Windows.Forms.Label();
            this.buttonVDI3805Read = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.buttonCafmConnectCreate = new System.Windows.Forms.Button();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(176, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "VDI3805 ==> CAFM-Connect (IFC4)";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // buttonVDI3805Read
            // 
            this.buttonVDI3805Read.Location = new System.Drawing.Point(29, 55);
            this.buttonVDI3805Read.Name = "buttonVDI3805Read";
            this.buttonVDI3805Read.Size = new System.Drawing.Size(150, 23);
            this.buttonVDI3805Read.TabIndex = 1;
            this.buttonVDI3805Read.Text = "VDI 3805 Datei einlesen";
            this.buttonVDI3805Read.UseVisualStyleBackColor = true;
            this.buttonVDI3805Read.Click += new System.EventHandler(this.buttonVDI3805Read_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "VDI3805(*.zip)|*.zip";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(29, 117);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(677, 278);
            this.textBox1.TabIndex = 3;
            // 
            // buttonCafmConnectCreate
            // 
            this.buttonCafmConnectCreate.Location = new System.Drawing.Point(194, 55);
            this.buttonCafmConnectCreate.Name = "buttonCafmConnectCreate";
            this.buttonCafmConnectCreate.Size = new System.Drawing.Size(157, 23);
            this.buttonCafmConnectCreate.TabIndex = 4;
            this.buttonCafmConnectCreate.Text = "CAFM-Connect-Datei erstellen";
            this.buttonCafmConnectCreate.UseVisualStyleBackColor = true;
            this.buttonCafmConnectCreate.Click += new System.EventHandler(this.buttonCafmConnectCreate_Click);
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(40, 91);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(0, 13);
            this.linkLabel2.TabIndex = 8;
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked_1);
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1029, 395);
            this.Controls.Add(this.linkLabel2);
            this.Controls.Add(this.buttonCafmConnectCreate);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.buttonVDI3805Read);
            this.Controls.Add(this.label1);
            this.Name = "Form";
            this.Text = "CafmConnectObjects";
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonVDI3805Read;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button buttonCafmConnectCreate;
        private System.Windows.Forms.LinkLabel linkLabel2;
    }
}

