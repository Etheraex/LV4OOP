namespace LabV4OOP
{
    partial class GameForm
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
            this.cardOne = new System.Windows.Forms.PictureBox();
            this.txtBoxPoints = new System.Windows.Forms.TextBox();
            this.cardTwo = new System.Windows.Forms.PictureBox();
            this.cardThree = new System.Windows.Forms.PictureBox();
            this.cardFour = new System.Windows.Forms.PictureBox();
            this.cardFive = new System.Windows.Forms.PictureBox();
            this.p1 = new System.Windows.Forms.Panel();
            this.p2 = new System.Windows.Forms.Panel();
            this.p4 = new System.Windows.Forms.Panel();
            this.p3 = new System.Windows.Forms.Panel();
            this.p5 = new System.Windows.Forms.Panel();
            this.btnSwap = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtBoxBet = new System.Windows.Forms.TextBox();
            this.bet = new System.Windows.Forms.Button();
            this.btnAll = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblBetMade = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.cardOne)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardTwo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardThree)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardFour)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardFive)).BeginInit();
            this.p1.SuspendLayout();
            this.p2.SuspendLayout();
            this.p4.SuspendLayout();
            this.p3.SuspendLayout();
            this.p5.SuspendLayout();
            this.SuspendLayout();
            // 
            // cardOne
            // 
            this.cardOne.Location = new System.Drawing.Point(1, 1);
            this.cardOne.Margin = new System.Windows.Forms.Padding(0);
            this.cardOne.Name = "cardOne";
            this.cardOne.Size = new System.Drawing.Size(71, 94);
            this.cardOne.TabIndex = 0;
            this.cardOne.TabStop = false;
            // 
            // txtBoxPoints
            // 
            this.txtBoxPoints.Location = new System.Drawing.Point(64, 229);
            this.txtBoxPoints.Name = "txtBoxPoints";
            this.txtBoxPoints.ReadOnly = true;
            this.txtBoxPoints.Size = new System.Drawing.Size(100, 20);
            this.txtBoxPoints.TabIndex = 1;
            // 
            // cardTwo
            // 
            this.cardTwo.Location = new System.Drawing.Point(1, 1);
            this.cardTwo.Name = "cardTwo";
            this.cardTwo.Size = new System.Drawing.Size(71, 94);
            this.cardTwo.TabIndex = 0;
            this.cardTwo.TabStop = false;
            // 
            // cardThree
            // 
            this.cardThree.Location = new System.Drawing.Point(1, 1);
            this.cardThree.Name = "cardThree";
            this.cardThree.Size = new System.Drawing.Size(71, 94);
            this.cardThree.TabIndex = 0;
            this.cardThree.TabStop = false;
            // 
            // cardFour
            // 
            this.cardFour.Location = new System.Drawing.Point(1, 1);
            this.cardFour.Name = "cardFour";
            this.cardFour.Size = new System.Drawing.Size(71, 94);
            this.cardFour.TabIndex = 0;
            this.cardFour.TabStop = false;
            // 
            // cardFive
            // 
            this.cardFive.Location = new System.Drawing.Point(1, 1);
            this.cardFive.Name = "cardFive";
            this.cardFive.Size = new System.Drawing.Size(71, 94);
            this.cardFive.TabIndex = 0;
            this.cardFive.TabStop = false;
            // 
            // p1
            // 
            this.p1.Controls.Add(this.cardOne);
            this.p1.Location = new System.Drawing.Point(16, 60);
            this.p1.Name = "p1";
            this.p1.Size = new System.Drawing.Size(73, 96);
            this.p1.TabIndex = 2;
            // 
            // p2
            // 
            this.p2.Controls.Add(this.cardTwo);
            this.p2.Location = new System.Drawing.Point(95, 60);
            this.p2.Name = "p2";
            this.p2.Size = new System.Drawing.Size(73, 96);
            this.p2.TabIndex = 2;
            // 
            // p4
            // 
            this.p4.Controls.Add(this.cardFour);
            this.p4.Location = new System.Drawing.Point(253, 60);
            this.p4.Name = "p4";
            this.p4.Size = new System.Drawing.Size(73, 96);
            this.p4.TabIndex = 2;
            // 
            // p3
            // 
            this.p3.Controls.Add(this.cardThree);
            this.p3.Location = new System.Drawing.Point(174, 60);
            this.p3.Name = "p3";
            this.p3.Size = new System.Drawing.Size(73, 96);
            this.p3.TabIndex = 2;
            // 
            // p5
            // 
            this.p5.Controls.Add(this.cardFive);
            this.p5.Location = new System.Drawing.Point(332, 60);
            this.p5.Name = "p5";
            this.p5.Size = new System.Drawing.Size(73, 96);
            this.p5.TabIndex = 2;
            // 
            // btnSwap
            // 
            this.btnSwap.Location = new System.Drawing.Point(251, 12);
            this.btnSwap.Name = "btnSwap";
            this.btnSwap.Size = new System.Drawing.Size(75, 23);
            this.btnSwap.TabIndex = 3;
            this.btnSwap.Text = "Zameni";
            this.btnSwap.UseVisualStyleBackColor = true;
            this.btnSwap.Click += new System.EventHandler(this.btnSwap_Click);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(329, 12);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 3;
            this.btnSend.Text = "Predaj";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtBoxBet
            // 
            this.txtBoxBet.Location = new System.Drawing.Point(63, 266);
            this.txtBoxBet.Name = "txtBoxBet";
            this.txtBoxBet.Size = new System.Drawing.Size(100, 20);
            this.txtBoxBet.TabIndex = 4;
            // 
            // bet
            // 
            this.bet.Location = new System.Drawing.Point(169, 265);
            this.bet.Name = "bet";
            this.bet.Size = new System.Drawing.Size(75, 23);
            this.bet.TabIndex = 6;
            this.bet.Text = "Bet";
            this.bet.UseVisualStyleBackColor = true;
            this.bet.Click += new System.EventHandler(this.bet_Click);
            // 
            // btnAll
            // 
            this.btnAll.Location = new System.Drawing.Point(250, 264);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(75, 23);
            this.btnAll.TabIndex = 7;
            this.btnAll.Text = "All In";
            this.btnAll.UseVisualStyleBackColor = true;
            this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 232);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Balans:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 269);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Ulog:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(129, 192);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Trenutni ulog:";
            // 
            // lblBetMade
            // 
            this.lblBetMade.AutoSize = true;
            this.lblBetMade.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBetMade.Location = new System.Drawing.Point(207, 188);
            this.lblBetMade.Name = "lblBetMade";
            this.lblBetMade.Size = new System.Drawing.Size(0, 18);
            this.lblBetMade.TabIndex = 9;
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 299);
            this.Controls.Add(this.lblBetMade);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAll);
            this.Controls.Add(this.bet);
            this.Controls.Add(this.txtBoxBet);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.btnSwap);
            this.Controls.Add(this.p3);
            this.Controls.Add(this.p5);
            this.Controls.Add(this.p4);
            this.Controls.Add(this.p2);
            this.Controls.Add(this.p1);
            this.Controls.Add(this.txtBoxPoints);
            this.Name = "GameForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.cardOne)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardTwo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardThree)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardFour)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardFive)).EndInit();
            this.p1.ResumeLayout(false);
            this.p2.ResumeLayout(false);
            this.p4.ResumeLayout(false);
            this.p3.ResumeLayout(false);
            this.p5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox cardOne;
        private System.Windows.Forms.TextBox txtBoxPoints;
        private System.Windows.Forms.PictureBox cardTwo;
        private System.Windows.Forms.PictureBox cardThree;
        private System.Windows.Forms.PictureBox cardFour;
        private System.Windows.Forms.PictureBox cardFive;
        private System.Windows.Forms.Panel p1;
        private System.Windows.Forms.Panel p2;
        private System.Windows.Forms.Panel p4;
        private System.Windows.Forms.Panel p3;
        private System.Windows.Forms.Panel p5;
        private System.Windows.Forms.Button btnSwap;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtBoxBet;
        private System.Windows.Forms.Button bet;
        private System.Windows.Forms.Button btnAll;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblBetMade;
    }
}

