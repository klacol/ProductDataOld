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
            this.linkLabelVDI3085 = new System.Windows.Forms.LinkLabel();
            this.labelCounter = new System.Windows.Forms.Label();
            this.labelStopwatch = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(216, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Converter VDI3805 to CAFM-Connect (IFC4)";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // buttonVDI3805Read
            // 
            this.buttonVDI3805Read.Location = new System.Drawing.Point(29, 55);
            this.buttonVDI3805Read.Name = "buttonVDI3805Read";
            this.buttonVDI3805Read.Size = new System.Drawing.Size(150, 23);
            this.buttonVDI3805Read.TabIndex = 1;
            this.buttonVDI3805Read.Text = "Read VDI 3805 file";
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
            this.textBox1.Location = new System.Drawing.Point(29, 135);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(396, 260);
            this.textBox1.TabIndex = 3;
            // 
            // buttonCafmConnectCreate
            // 
            this.buttonCafmConnectCreate.Location = new System.Drawing.Point(471, 55);
            this.buttonCafmConnectCreate.Name = "buttonCafmConnectCreate";
            this.buttonCafmConnectCreate.Size = new System.Drawing.Size(157, 23);
            this.buttonCafmConnectCreate.TabIndex = 4;
            this.buttonCafmConnectCreate.Text = "CAFM-Connect-Datei erstellen";
            this.buttonCafmConnectCreate.UseVisualStyleBackColor = true;
            this.buttonCafmConnectCreate.Click += new System.EventHandler(this.buttonCafmConnectCreate_Click);
            // 
            // linkLabelVDI3085
            // 
            this.linkLabelVDI3085.AutoSize = true;
            this.linkLabelVDI3085.Location = new System.Drawing.Point(191, 96);
            this.linkLabelVDI3085.Name = "linkLabelVDI3085";
            this.linkLabelVDI3085.Size = new System.Drawing.Size(91, 13);
            this.linkLabelVDI3085.TabIndex = 8;
            this.linkLabelVDI3085.TabStop = true;
            this.linkLabelVDI3085.Text = "linkLabelVDI3085";
            this.linkLabelVDI3085.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked_1);
            // 
            // labelCounter
            // 
            this.labelCounter.AutoSize = true;
            this.labelCounter.Location = new System.Drawing.Point(857, 60);
            this.labelCounter.Name = "labelCounter";
            this.labelCounter.Size = new System.Drawing.Size(66, 13);
            this.labelCounter.TabIndex = 9;
            this.labelCounter.Text = "labelCounter";
            // 
            // labelStopwatch
            // 
            this.labelStopwatch.AutoSize = true;
            this.labelStopwatch.Location = new System.Drawing.Point(857, 73);
            this.labelStopwatch.Name = "labelStopwatch";
            this.labelStopwatch.Size = new System.Drawing.Size(80, 13);
            this.labelStopwatch.TabIndex = 11;
            this.labelStopwatch.Text = "labelStopwatch";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(771, 60);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(47, 20);
            this.textBox2.TabIndex = 12;
            this.textBox2.Text = "100";
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Filename VDI3805";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Filename CAFM-Connect";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(191, 109);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(62, 13);
            this.linkLabel1.TabIndex = 15;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "linkLabelCc";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(670, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Generate Variants:";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(490, 135);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox3.Size = new System.Drawing.Size(489, 260);
            this.textBox3.TabIndex = 17;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(432, 270);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(52, 23);
            this.button1.TabIndex = 18;
            this.button1.Text = "==>";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1029, 395);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.labelStopwatch);
            this.Controls.Add(this.labelCounter);
            this.Controls.Add(this.linkLabelVDI3085);
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
        private System.Windows.Forms.LinkLabel linkLabelVDI3085;
        private System.Windows.Forms.Label labelCounter;
        private System.Windows.Forms.Label labelStopwatch;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button button1;
    }
}

