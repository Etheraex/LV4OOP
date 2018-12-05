namespace LabV4OOP
{
    partial class PickTypeForm
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
            this.std52 = new System.Windows.Forms.Button();
            this.std32 = new System.Windows.Forms.Button();
            this.tx52 = new System.Windows.Forms.Button();
            this.tx32 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // std52
            // 
            this.std52.Location = new System.Drawing.Point(26, 24);
            this.std52.Name = "std52";
            this.std52.Size = new System.Drawing.Size(101, 23);
            this.std52.TabIndex = 0;
            this.std52.Text = "Standard 52";
            this.std52.UseVisualStyleBackColor = true;
            this.std52.Click += new System.EventHandler(this.std52_Click);
            // 
            // std32
            // 
            this.std32.Location = new System.Drawing.Point(26, 53);
            this.std32.Name = "std32";
            this.std32.Size = new System.Drawing.Size(101, 23);
            this.std32.TabIndex = 0;
            this.std32.Text = "Standard 32";
            this.std32.UseVisualStyleBackColor = true;
            this.std32.Click += new System.EventHandler(this.std32_Click);
            // 
            // tx52
            // 
            this.tx52.Location = new System.Drawing.Point(26, 82);
            this.tx52.Name = "tx52";
            this.tx52.Size = new System.Drawing.Size(101, 23);
            this.tx52.TabIndex = 0;
            this.tx52.Text = "Texas Holdem 52";
            this.tx52.UseVisualStyleBackColor = true;
            this.tx52.Click += new System.EventHandler(this.tx52_Click);
            // 
            // tx32
            // 
            this.tx32.Location = new System.Drawing.Point(26, 111);
            this.tx32.Name = "tx32";
            this.tx32.Size = new System.Drawing.Size(101, 23);
            this.tx32.TabIndex = 0;
            this.tx32.Text = "Texas Holdem 32";
            this.tx32.UseVisualStyleBackColor = true;
            this.tx32.Click += new System.EventHandler(this.tx32_Click);
            // 
            // PickTypeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(151, 165);
            this.Controls.Add(this.tx32);
            this.Controls.Add(this.tx52);
            this.Controls.Add(this.std32);
            this.Controls.Add(this.std52);
            this.Name = "PickTypeForm";
            this.Text = "PickTypeForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button std52;
        private System.Windows.Forms.Button std32;
        private System.Windows.Forms.Button tx52;
        private System.Windows.Forms.Button tx32;
    }
}