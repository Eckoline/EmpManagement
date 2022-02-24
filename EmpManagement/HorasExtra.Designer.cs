
namespace EmpManagement
{
    partial class HorasExtra
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
            this.labelID = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelNombre = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxHor = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxMinutos = new System.Windows.Forms.TextBox();
            this.buttonActualizar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID:";
            // 
            // labelID
            // 
            this.labelID.AutoSize = true;
            this.labelID.Location = new System.Drawing.Point(41, 25);
            this.labelID.Name = "labelID";
            this.labelID.Size = new System.Drawing.Size(38, 15);
            this.labelID.TabIndex = 1;
            this.labelID.Text = "label2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Nombre:";
            // 
            // labelNombre
            // 
            this.labelNombre.AutoSize = true;
            this.labelNombre.Location = new System.Drawing.Point(74, 44);
            this.labelNombre.Name = "labelNombre";
            this.labelNombre.Size = new System.Drawing.Size(38, 15);
            this.labelNombre.TabIndex = 3;
            this.labelNombre.Text = "label4";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 93);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 15);
            this.label5.TabIndex = 4;
            this.label5.Text = "Horas:";
            // 
            // textBoxHor
            // 
            this.textBoxHor.Location = new System.Drawing.Point(74, 85);
            this.textBoxHor.Name = "textBoxHor";
            this.textBoxHor.Size = new System.Drawing.Size(100, 23);
            this.textBoxHor.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 127);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 15);
            this.label6.TabIndex = 6;
            this.label6.Text = "Minutos:";
            // 
            // textBoxMinutos
            // 
            this.textBoxMinutos.Location = new System.Drawing.Point(74, 124);
            this.textBoxMinutos.Name = "textBoxMinutos";
            this.textBoxMinutos.Size = new System.Drawing.Size(100, 23);
            this.textBoxMinutos.TabIndex = 7;
            // 
            // buttonActualizar
            // 
            this.buttonActualizar.Location = new System.Drawing.Point(62, 171);
            this.buttonActualizar.Name = "buttonActualizar";
            this.buttonActualizar.Size = new System.Drawing.Size(75, 23);
            this.buttonActualizar.TabIndex = 8;
            this.buttonActualizar.Text = "Actualizar";
            this.buttonActualizar.UseVisualStyleBackColor = true;
            this.buttonActualizar.Click += new System.EventHandler(this.buttonActualizar_Click);
            // 
            // HorasExtra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(194, 210);
            this.Controls.Add(this.buttonActualizar);
            this.Controls.Add(this.textBoxMinutos);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxHor);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.labelNombre);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelID);
            this.Controls.Add(this.label1);
            this.Name = "HorasExtra";
            this.Text = "HorasExtra";
            this.Load += new System.EventHandler(this.HorasExtra_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxHor;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxMinutos;
        private System.Windows.Forms.Button buttonActualizar;
        public System.Windows.Forms.Label labelID;
        public System.Windows.Forms.Label labelNombre;
    }
}