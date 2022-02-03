
namespace EmpManagement
{
    partial class JustificaIncon
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBoxTipo = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.checkBoxcorr = new System.Windows.Forms.CheckBox();
            this.labelFec = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonAcept = new System.Windows.Forms.Button();
            this.labelNombre = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelTipo = new System.Windows.Forms.Label();
            this.textBoxComentInc = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelIDEmp = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBoxTipo);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.checkBoxcorr);
            this.groupBox1.Controls.Add(this.labelFec);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.labelNombre);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.labelTipo);
            this.groupBox1.Controls.Add(this.textBoxComentInc);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.labelIDEmp);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(360, 345);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Detalles Justificación";
            // 
            // comboBoxTipo
            // 
            this.comboBoxTipo.FormattingEnabled = true;
            this.comboBoxTipo.Location = new System.Drawing.Point(98, 79);
            this.comboBoxTipo.Name = "comboBoxTipo";
            this.comboBoxTipo.Size = new System.Drawing.Size(168, 23);
            this.comboBoxTipo.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label7.Location = new System.Drawing.Point(15, 79);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 15);
            this.label7.TabIndex = 12;
            this.label7.Text = "Tipo evento:";
            // 
            // checkBoxcorr
            // 
            this.checkBoxcorr.AutoSize = true;
            this.checkBoxcorr.Location = new System.Drawing.Point(276, 51);
            this.checkBoxcorr.Name = "checkBoxcorr";
            this.checkBoxcorr.Size = new System.Drawing.Size(84, 19);
            this.checkBoxcorr.TabIndex = 11;
            this.checkBoxcorr.Text = "Corrección";
            this.checkBoxcorr.UseVisualStyleBackColor = true;
            // 
            // labelFec
            // 
            this.labelFec.AutoSize = true;
            this.labelFec.Location = new System.Drawing.Point(54, 51);
            this.labelFec.Name = "labelFec";
            this.labelFec.Size = new System.Drawing.Size(38, 15);
            this.labelFec.TabIndex = 10;
            this.labelFec.Text = "label7";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(15, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 15);
            this.label5.TabIndex = 9;
            this.label5.Text = "Fecha:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonCancel);
            this.groupBox2.Controls.Add(this.buttonAcept);
            this.groupBox2.Location = new System.Drawing.Point(66, 281);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 56);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Controles";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(100, 22);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancelar";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonAcept
            // 
            this.buttonAcept.Location = new System.Drawing.Point(19, 22);
            this.buttonAcept.Name = "buttonAcept";
            this.buttonAcept.Size = new System.Drawing.Size(75, 23);
            this.buttonAcept.TabIndex = 0;
            this.buttonAcept.Text = "Aceptar";
            this.buttonAcept.UseVisualStyleBackColor = true;
            this.buttonAcept.Click += new System.EventHandler(this.buttonAcept_Click);
            // 
            // labelNombre
            // 
            this.labelNombre.AutoSize = true;
            this.labelNombre.Location = new System.Drawing.Point(131, 30);
            this.labelNombre.Name = "labelNombre";
            this.labelNombre.Size = new System.Drawing.Size(38, 15);
            this.labelNombre.TabIndex = 7;
            this.labelNombre.Text = "label5";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(79, 30);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label4.Size = new System.Drawing.Size(56, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "Nombre:";
            // 
            // labelTipo
            // 
            this.labelTipo.AutoSize = true;
            this.labelTipo.Location = new System.Drawing.Point(179, 51);
            this.labelTipo.Name = "labelTipo";
            this.labelTipo.Size = new System.Drawing.Size(38, 15);
            this.labelTipo.TabIndex = 5;
            this.labelTipo.Text = "label4";
            // 
            // textBoxComentInc
            // 
            this.textBoxComentInc.Location = new System.Drawing.Point(12, 137);
            this.textBoxComentInc.Multiline = true;
            this.textBoxComentInc.Name = "textBoxComentInc";
            this.textBoxComentInc.Size = new System.Drawing.Size(327, 137);
            this.textBoxComentInc.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(12, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(180, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "Comentarios de la Justificación:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(149, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tipo:";
            // 
            // labelIDEmp
            // 
            this.labelIDEmp.AutoSize = true;
            this.labelIDEmp.Location = new System.Drawing.Point(34, 30);
            this.labelIDEmp.Name = "labelIDEmp";
            this.labelIDEmp.Size = new System.Drawing.Size(38, 15);
            this.labelIDEmp.TabIndex = 1;
            this.labelIDEmp.Text = "label2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(15, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label6);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(360, 23);
            this.panel2.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(360, 23);
            this.label6.TabIndex = 1;
            this.label6.Text = "Justificación de Inconsistencias";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // JustificaIncon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 345);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.groupBox1);
            this.Name = "JustificaIncon";
            this.Load += new System.EventHandler(this.JustificaIncon_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonAcept;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxComentInc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.Label labelNombre;
        public System.Windows.Forms.Label labelTipo;
        public System.Windows.Forms.Label labelIDEmp;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.Label labelFec;
        private System.Windows.Forms.CheckBox checkBoxcorr;
        private System.Windows.Forms.ComboBox comboBoxTipo;
        private System.Windows.Forms.Label label7;
    }
}