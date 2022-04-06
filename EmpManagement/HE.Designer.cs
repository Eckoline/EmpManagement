
namespace EmpManagement
{
    partial class HE
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
            this.label2 = new System.Windows.Forms.Label();
            this.labelid = new System.Windows.Forms.Label();
            this.labelnom = new System.Windows.Forms.Label();
            this.labelfecin = new System.Windows.Forms.Label();
            this.labelfecfin = new System.Windows.Forms.Label();
            this.dataGridViewDatos = new System.Windows.Forms.DataGridView();
            this.dataGridViewDatosMod = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonAcep = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDatos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDatosMod)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Nombre:";
            // 
            // labelid
            // 
            this.labelid.AutoSize = true;
            this.labelid.Location = new System.Drawing.Point(77, 18);
            this.labelid.Name = "labelid";
            this.labelid.Size = new System.Drawing.Size(38, 15);
            this.labelid.TabIndex = 2;
            this.labelid.Text = "label3";
            // 
            // labelnom
            // 
            this.labelnom.AutoSize = true;
            this.labelnom.Location = new System.Drawing.Point(77, 38);
            this.labelnom.Name = "labelnom";
            this.labelnom.Size = new System.Drawing.Size(38, 15);
            this.labelnom.TabIndex = 3;
            this.labelnom.Text = "label4";
            // 
            // labelfecin
            // 
            this.labelfecin.AutoSize = true;
            this.labelfecin.Location = new System.Drawing.Point(10, 18);
            this.labelfecin.Name = "labelfecin";
            this.labelfecin.Size = new System.Drawing.Size(38, 15);
            this.labelfecin.TabIndex = 4;
            this.labelfecin.Text = "label3";
            // 
            // labelfecfin
            // 
            this.labelfecfin.AutoSize = true;
            this.labelfecfin.Location = new System.Drawing.Point(10, 38);
            this.labelfecfin.Name = "labelfecfin";
            this.labelfecfin.Size = new System.Drawing.Size(38, 15);
            this.labelfecfin.TabIndex = 5;
            this.labelfecfin.Text = "label4";
            // 
            // dataGridViewDatos
            // 
            this.dataGridViewDatos.AllowUserToAddRows = false;
            this.dataGridViewDatos.AllowUserToDeleteRows = false;
            this.dataGridViewDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDatos.Location = new System.Drawing.Point(5, 22);
            this.dataGridViewDatos.Name = "dataGridViewDatos";
            this.dataGridViewDatos.ReadOnly = true;
            this.dataGridViewDatos.RowTemplate.Height = 25;
            this.dataGridViewDatos.Size = new System.Drawing.Size(214, 227);
            this.dataGridViewDatos.TabIndex = 6;
            // 
            // dataGridViewDatosMod
            // 
            this.dataGridViewDatosMod.AllowUserToAddRows = false;
            this.dataGridViewDatosMod.AllowUserToDeleteRows = false;
            this.dataGridViewDatosMod.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDatosMod.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewDatosMod.Location = new System.Drawing.Point(221, 22);
            this.dataGridViewDatosMod.Name = "dataGridViewDatosMod";
            this.dataGridViewDatosMod.RowTemplate.Height = 25;
            this.dataGridViewDatosMod.Size = new System.Drawing.Size(214, 227);
            this.dataGridViewDatosMod.TabIndex = 7;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridViewDatosMod);
            this.groupBox1.Controls.Add(this.dataGridViewDatos);
            this.groupBox1.Location = new System.Drawing.Point(12, 92);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(441, 263);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.labelid);
            this.groupBox2.Controls.Add(this.labelnom);
            this.groupBox2.Location = new System.Drawing.Point(12, 30);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(304, 59);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Empleado";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.buttonCancel);
            this.groupBox3.Controls.Add(this.buttonAcep);
            this.groupBox3.Location = new System.Drawing.Point(140, 361);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(186, 58);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Opciones";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(88, 22);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancelar";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonAcep
            // 
            this.buttonAcep.Location = new System.Drawing.Point(7, 22);
            this.buttonAcep.Name = "buttonAcep";
            this.buttonAcep.Size = new System.Drawing.Size(75, 23);
            this.buttonAcep.TabIndex = 0;
            this.buttonAcep.Text = "Aceptar";
            this.buttonAcep.UseVisualStyleBackColor = true;
            this.buttonAcep.Click += new System.EventHandler(this.buttonAcep_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.labelfecin);
            this.groupBox4.Controls.Add(this.labelfecfin);
            this.groupBox4.Location = new System.Drawing.Point(322, 30);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(131, 59);
            this.groupBox4.TabIndex = 11;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Fechas";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(465, 23);
            this.label6.TabIndex = 12;
            this.label6.Text = "Edición de Horas Extras";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // HE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 428);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "HE";
            this.Text = "Edición Horas Extra";
            this.Load += new System.EventHandler(this.HE_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDatos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDatosMod)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label labelid;
        private System.Windows.Forms.DataGridView dataGridViewDatos;
        private System.Windows.Forms.DataGridView dataGridViewDatosMod;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonAcep;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.Label labelfecin;
        public System.Windows.Forms.Label labelfecfin;
        public System.Windows.Forms.Label labelnom;
    }
}