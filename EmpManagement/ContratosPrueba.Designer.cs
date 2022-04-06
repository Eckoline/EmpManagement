
namespace EmpManagement
{
    partial class ContratosPrueba
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContratosPrueba));
            this.btnGenerar = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Doclabel = new System.Windows.Forms.Label();
            this.TipoCon = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.PanAvPriv = new System.Windows.Forms.Panel();
            this.NomTrab = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.NomCor = new System.Windows.Forms.TextBox();
            this.FecFirm = new System.Windows.Forms.DateTimePicker();
            this.RFC = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.Naciona = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.NombreT = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.PanAcuerdConfi = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.PanAvPriv.SuspendLayout();
            this.PanAcuerdConfi.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGenerar
            // 
            this.btnGenerar.Location = new System.Drawing.Point(659, 418);
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.Size = new System.Drawing.Size(133, 23);
            this.btnGenerar.TabIndex = 0;
            this.btnGenerar.Text = "Generar";
            this.btnGenerar.UseVisualStyleBackColor = true;
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 358);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 15);
            this.label5.TabIndex = 16;
            this.label5.Text = "label5";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 327);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 15);
            this.label4.TabIndex = 15;
            this.label4.Text = "label4";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(54, 320);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 23);
            this.textBox3.TabIndex = 19;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(54, 350);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(100, 23);
            this.textBox4.TabIndex = 20;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(54, 379);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(100, 23);
            this.textBox5.TabIndex = 21;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(379, 49);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(57, 53);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 24;
            this.pictureBox1.TabStop = false;
            // 
            // Doclabel
            // 
            this.Doclabel.AutoSize = true;
            this.Doclabel.BackColor = System.Drawing.Color.Transparent;
            this.Doclabel.Font = new System.Drawing.Font("Lucida Bright", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Doclabel.Location = new System.Drawing.Point(227, 112);
            this.Doclabel.Name = "Doclabel";
            this.Doclabel.Size = new System.Drawing.Size(225, 15);
            this.Doclabel.TabIndex = 26;
            this.Doclabel.Text = "Seleccionar tipo de documento:";
            // 
            // TipoCon
            // 
            this.TipoCon.AutoCompleteCustomSource.AddRange(new string[] {
            "Aviso de Privacidad",
            "Acuerdo de confidencialidad",
            "Contrato Individual por periodo de capacitación"});
            this.TipoCon.FormattingEnabled = true;
            this.TipoCon.Items.AddRange(new object[] {
            "Aviso de Privacidad",
            "Acuerdo de Confidencialidad",
            "Contrato Capacitación Inicial"});
            this.TipoCon.Location = new System.Drawing.Point(458, 109);
            this.TipoCon.Name = "TipoCon";
            this.TipoCon.Size = new System.Drawing.Size(121, 23);
            this.TipoCon.TabIndex = 25;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Lucida Bright", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(324, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(168, 27);
            this.label6.TabIndex = 27;
            this.label6.Text = "CONTRATOS";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(-1, 1);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(180, 56);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 28;
            this.pictureBox2.TabStop = false;
            // 
            // PanAvPriv
            // 
            this.PanAvPriv.Controls.Add(this.NomTrab);
            this.PanAvPriv.Controls.Add(this.label1);
            this.PanAvPriv.Controls.Add(this.label2);
            this.PanAvPriv.Controls.Add(this.label3);
            this.PanAvPriv.Controls.Add(this.NomCor);
            this.PanAvPriv.Controls.Add(this.FecFirm);
            this.PanAvPriv.Location = new System.Drawing.Point(218, 157);
            this.PanAvPriv.Name = "PanAvPriv";
            this.PanAvPriv.Size = new System.Drawing.Size(385, 100);
            this.PanAvPriv.TabIndex = 29;
            this.PanAvPriv.Visible = false;
            // 
            // NomTrab
            // 
            this.NomTrab.Location = new System.Drawing.Point(156, 69);
            this.NomTrab.Name = "NomTrab";
            this.NomTrab.Size = new System.Drawing.Size(178, 23);
            this.NomTrab.TabIndex = 28;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 15);
            this.label1.TabIndex = 24;
            this.label1.Text = "Nombre de cordinador:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(60, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 15);
            this.label2.TabIndex = 25;
            this.label2.Text = "Fecha de Firma:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 15);
            this.label3.TabIndex = 26;
            this.label3.Text = "Nombre del Trabajador:";
            // 
            // NomCor
            // 
            this.NomCor.Location = new System.Drawing.Point(156, 3);
            this.NomCor.Name = "NomCor";
            this.NomCor.Size = new System.Drawing.Size(160, 23);
            this.NomCor.TabIndex = 27;
            // 
            // FecFirm
            // 
            this.FecFirm.Location = new System.Drawing.Point(156, 38);
            this.FecFirm.Name = "FecFirm";
            this.FecFirm.Size = new System.Drawing.Size(219, 23);
            this.FecFirm.TabIndex = 29;
            // 
            // RFC
            // 
            this.RFC.Location = new System.Drawing.Point(152, 184);
            this.RFC.Name = "RFC";
            this.RFC.Size = new System.Drawing.Size(125, 23);
            this.RFC.TabIndex = 44;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(115, 192);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(31, 15);
            this.label14.TabIndex = 43;
            this.label14.Text = "RFC:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(189, 46);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(35, 15);
            this.label13.TabIndex = 42;
            this.label13.Text = "años.";
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(152, 126);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(217, 23);
            this.textBox7.TabIndex = 41;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(83, 134);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(61, 15);
            this.label12.TabIndex = 40;
            this.label12.Text = "Domicilio:";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(152, 98);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(125, 23);
            this.textBox2.TabIndex = 39;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(115, 106);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(31, 15);
            this.label11.TabIndex = 38;
            this.label11.Text = "NSS:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(152, 69);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(86, 23);
            this.textBox1.TabIndex = 37;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(75, 77);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 15);
            this.label10.TabIndex = 36;
            this.label10.Text = "Estado Civil:";
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(152, 38);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(31, 23);
            this.textBox6.TabIndex = 35;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(108, 46);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(36, 15);
            this.label9.TabIndex = 34;
            this.label9.Text = "Edad:";
            // 
            // Naciona
            // 
            this.Naciona.Location = new System.Drawing.Point(152, 11);
            this.Naciona.Name = "Naciona";
            this.Naciona.Size = new System.Drawing.Size(145, 23);
            this.Naciona.TabIndex = 33;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(64, 19);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 15);
            this.label8.TabIndex = 32;
            this.label8.Text = "Nacionalidad:";
            // 
            // NombreT
            // 
            this.NombreT.Location = new System.Drawing.Point(152, 155);
            this.NombreT.Name = "NombreT";
            this.NombreT.Size = new System.Drawing.Size(160, 23);
            this.NombreT.TabIndex = 31;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 158);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(131, 15);
            this.label7.TabIndex = 30;
            this.label7.Text = "Nombre del Trabajador:";
            // 
            // PanAcuerdConfi
            // 
            this.PanAcuerdConfi.BackColor = System.Drawing.Color.Transparent;
            this.PanAcuerdConfi.Controls.Add(this.RFC);
            this.PanAcuerdConfi.Controls.Add(this.label14);
            this.PanAcuerdConfi.Controls.Add(this.label13);
            this.PanAcuerdConfi.Controls.Add(this.textBox7);
            this.PanAcuerdConfi.Controls.Add(this.label12);
            this.PanAcuerdConfi.Controls.Add(this.textBox2);
            this.PanAcuerdConfi.Controls.Add(this.label11);
            this.PanAcuerdConfi.Controls.Add(this.textBox1);
            this.PanAcuerdConfi.Controls.Add(this.label10);
            this.PanAcuerdConfi.Controls.Add(this.textBox6);
            this.PanAcuerdConfi.Controls.Add(this.label9);
            this.PanAcuerdConfi.Controls.Add(this.Naciona);
            this.PanAcuerdConfi.Controls.Add(this.label8);
            this.PanAcuerdConfi.Controls.Add(this.NombreT);
            this.PanAcuerdConfi.Controls.Add(this.label7);
            this.PanAcuerdConfi.Location = new System.Drawing.Point(218, 157);
            this.PanAcuerdConfi.Name = "PanAcuerdConfi";
            this.PanAcuerdConfi.Size = new System.Drawing.Size(418, 284);
            this.PanAcuerdConfi.TabIndex = 30;
            // 
            // ContratosPrueba
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 453);
            this.Controls.Add(this.PanAcuerdConfi);
            this.Controls.Add(this.PanAvPriv);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Doclabel);
            this.Controls.Add(this.TipoCon);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnGenerar);
            this.Name = "ContratosPrueba";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.ContratosPrueba_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.PanAvPriv.ResumeLayout(false);
            this.PanAvPriv.PerformLayout();
            this.PanAcuerdConfi.ResumeLayout(false);
            this.PanAcuerdConfi.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGenerar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label Doclabel;
        private System.Windows.Forms.ComboBox TipoCon;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel PanAvPriv;
        private System.Windows.Forms.TextBox NomTrab;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox NomCor;
        private System.Windows.Forms.DateTimePicker FecFirm;
        private System.Windows.Forms.TextBox NombreT;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox Naciona;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox RFC;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel PanAcuerdConfi;
    }
}