namespace VDIBootEditor
{
    partial class EditorForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.vdiPath = new System.Windows.Forms.TextBox();
            this.openVDI = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "VDI file:";
            // 
            // vdiPath
            // 
            this.vdiPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vdiPath.BackColor = System.Drawing.Color.White;
            this.vdiPath.Location = new System.Drawing.Point(66, 13);
            this.vdiPath.Name = "vdiPath";
            this.vdiPath.ReadOnly = true;
            this.vdiPath.Size = new System.Drawing.Size(344, 21);
            this.vdiPath.TabIndex = 3;
            // 
            // openVDI
            // 
            this.openVDI.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.openVDI.Location = new System.Drawing.Point(413, 12);
            this.openVDI.Name = "openVDI";
            this.openVDI.Size = new System.Drawing.Size(75, 23);
            this.openVDI.TabIndex = 5;
            this.openVDI.Text = "Open";
            this.openVDI.UseVisualStyleBackColor = true;
            this.openVDI.Click += new System.EventHandler(this.openVDI_Click);
            // 
            // button3
            // 
            this.button3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.button3.Location = new System.Drawing.Point(243, 40);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(167, 33);
            this.button3.TabIndex = 6;
            this.button3.Text = "Write MBR to VDI";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.button2.Location = new System.Drawing.Point(66, 40);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(171, 33);
            this.button2.TabIndex = 10;
            this.button2.Text = "Read MBR from VDI";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // EditorForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(497, 83);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.openVDI);
            this.Controls.Add(this.vdiPath);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Name = "EditorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VDI MBR editor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox vdiPath;
        private System.Windows.Forms.Button openVDI;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
    }
}

