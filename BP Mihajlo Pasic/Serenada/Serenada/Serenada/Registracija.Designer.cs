﻿namespace Serenada
{
    partial class Registracija
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
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(71, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(942, 135);
            this.label1.TabIndex = 0;
            this.label1.Text = "REGISTRACIJA";
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(641, 407);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(68, 20);
            this.checkBox2.TabIndex = 7;
            this.checkBox2.Text = "prikaži";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(641, 368);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(68, 20);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "prikaži";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // textBox5
            // 
            this.textBox5.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBox5.ForeColor = System.Drawing.SystemColors.GrayText;
            this.textBox5.Location = new System.Drawing.Point(389, 405);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(246, 22);
            this.textBox5.TabIndex = 6;
            this.textBox5.Text = "Potvrdite lozinku";
            this.textBox5.Enter += new System.EventHandler(this.textBox5_Enter);
            this.textBox5.Leave += new System.EventHandler(this.textBox5_Leave);
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBox4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.textBox4.Location = new System.Drawing.Point(389, 366);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(246, 22);
            this.textBox4.TabIndex = 4;
            this.textBox4.Text = "Lozinka";
            this.textBox4.Enter += new System.EventHandler(this.textBox4_Enter);
            this.textBox4.Leave += new System.EventHandler(this.textBox4_Leave);
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBox2.ForeColor = System.Drawing.SystemColors.GrayText;
            this.textBox2.Location = new System.Drawing.Point(389, 287);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(246, 22);
            this.textBox2.TabIndex = 2;
            this.textBox2.Text = "Prezime";
            this.textBox2.Enter += new System.EventHandler(this.textBox2_Enter);
            this.textBox2.Leave += new System.EventHandler(this.textBox2_Leave);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBox1.ForeColor = System.Drawing.SystemColors.GrayText;
            this.textBox1.Location = new System.Drawing.Point(389, 248);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(246, 22);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "Ime";
            this.textBox1.Enter += new System.EventHandler(this.textBox1_Enter);
            this.textBox1.Leave += new System.EventHandler(this.textBox1_Leave);
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBox3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.textBox3.Location = new System.Drawing.Point(389, 326);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(246, 22);
            this.textBox3.TabIndex = 3;
            this.textBox3.Text = "Email";
            this.textBox3.Enter += new System.EventHandler(this.textBox3_Enter);
            this.textBox3.Leave += new System.EventHandler(this.textBox3_Leave);
            // 
            // textBox6
            // 
            this.textBox6.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBox6.ForeColor = System.Drawing.SystemColors.GrayText;
            this.textBox6.Location = new System.Drawing.Point(389, 446);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(246, 22);
            this.textBox6.TabIndex = 8;
            this.textBox6.Text = "Telefon";
            this.textBox6.Enter += new System.EventHandler(this.textBox6_Enter);
            this.textBox6.Leave += new System.EventHandler(this.textBox6_Leave);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(455, 486);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(113, 49);
            this.button2.TabIndex = 9;
            this.button2.Text = "Registruj me";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 612);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 49);
            this.button1.TabIndex = 10;
            this.button1.Text = "Nazad";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Registracija
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1062, 673);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.label1);
            this.Name = "Registracija";
            this.Text = "Registracija";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
    }
}