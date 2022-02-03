
namespace EmpManagement
{
    partial class ReporteQuincenal
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReporteQuincenal));
            this.dataGridViewDetalleDias = new System.Windows.Forms.DataGridView();
            this.dataGridViewDatos = new System.Windows.Forms.DataGridView();
            this.groupBoxOpciones = new System.Windows.Forms.GroupBox();
            this.buttonMarcaciones = new System.Windows.Forms.Button();
            this.buttonAcept = new System.Windows.Forms.Button();
            this.groupBoxFechas = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePickerFin = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerIni = new System.Windows.Forms.DateTimePicker();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.exportarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.excelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBox2 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel9 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonJustificar = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDetalleDias)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDatos)).BeginInit();
            this.groupBoxOpciones.SuspendLayout();
            this.groupBoxFechas.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel8.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel9.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel7.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewDetalleDias
            // 
            this.dataGridViewDetalleDias.AllowUserToAddRows = false;
            this.dataGridViewDetalleDias.AllowUserToDeleteRows = false;
            this.dataGridViewDetalleDias.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridViewDetalleDias.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDetalleDias.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewDetalleDias.Location = new System.Drawing.Point(3, 19);
            this.dataGridViewDetalleDias.Name = "dataGridViewDetalleDias";
            this.dataGridViewDetalleDias.ReadOnly = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridViewDetalleDias.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewDetalleDias.RowTemplate.Height = 25;
            this.dataGridViewDetalleDias.Size = new System.Drawing.Size(933, 200);
            this.dataGridViewDetalleDias.TabIndex = 1;
            // 
            // dataGridViewDatos
            // 
            this.dataGridViewDatos.AllowUserToAddRows = false;
            this.dataGridViewDatos.AllowUserToDeleteRows = false;
            this.dataGridViewDatos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDatos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewDatos.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dataGridViewDatos.Location = new System.Drawing.Point(3, 19);
            this.dataGridViewDatos.Name = "dataGridViewDatos";
            this.dataGridViewDatos.ReadOnly = true;
            this.dataGridViewDatos.RowTemplate.Height = 25;
            this.dataGridViewDatos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewDatos.Size = new System.Drawing.Size(933, 233);
            this.dataGridViewDatos.TabIndex = 0;
            this.dataGridViewDatos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewDatos_CellClick_1);
            this.dataGridViewDatos.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewDatos_CellFormatting);
            // 
            // groupBoxOpciones
            // 
            this.groupBoxOpciones.Controls.Add(this.buttonMarcaciones);
            this.groupBoxOpciones.Controls.Add(this.buttonAcept);
            this.groupBoxOpciones.Location = new System.Drawing.Point(291, 1);
            this.groupBoxOpciones.Name = "groupBoxOpciones";
            this.groupBoxOpciones.Size = new System.Drawing.Size(173, 62);
            this.groupBoxOpciones.TabIndex = 10;
            this.groupBoxOpciones.TabStop = false;
            this.groupBoxOpciones.Text = "Control";
            // 
            // buttonMarcaciones
            // 
            this.buttonMarcaciones.Location = new System.Drawing.Point(76, 23);
            this.buttonMarcaciones.Name = "buttonMarcaciones";
            this.buttonMarcaciones.Size = new System.Drawing.Size(87, 23);
            this.buttonMarcaciones.TabIndex = 2;
            this.buttonMarcaciones.Text = "Marcaciones";
            this.buttonMarcaciones.UseVisualStyleBackColor = true;
            this.buttonMarcaciones.Click += new System.EventHandler(this.buttonMarcaciones_Click);
            // 
            // buttonAcept
            // 
            this.buttonAcept.Location = new System.Drawing.Point(6, 23);
            this.buttonAcept.Name = "buttonAcept";
            this.buttonAcept.Size = new System.Drawing.Size(64, 23);
            this.buttonAcept.TabIndex = 0;
            this.buttonAcept.Text = "Aceptar";
            this.buttonAcept.UseVisualStyleBackColor = true;
            this.buttonAcept.Click += new System.EventHandler(this.buttonAcept_Click);
            // 
            // groupBoxFechas
            // 
            this.groupBoxFechas.Controls.Add(this.label2);
            this.groupBoxFechas.Controls.Add(this.label1);
            this.groupBoxFechas.Controls.Add(this.dateTimePickerFin);
            this.groupBoxFechas.Controls.Add(this.dateTimePickerIni);
            this.groupBoxFechas.Location = new System.Drawing.Point(3, 1);
            this.groupBoxFechas.Name = "groupBoxFechas";
            this.groupBoxFechas.Size = new System.Drawing.Size(282, 62);
            this.groupBoxFechas.TabIndex = 9;
            this.groupBoxFechas.TabStop = false;
            this.groupBoxFechas.Text = "Rango Fechas";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(144, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "A:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "De:";
            // 
            // dateTimePickerFin
            // 
            this.dateTimePickerFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerFin.Location = new System.Drawing.Point(168, 22);
            this.dateTimePickerFin.Name = "dateTimePickerFin";
            this.dateTimePickerFin.Size = new System.Drawing.Size(95, 23);
            this.dateTimePickerFin.TabIndex = 3;
            // 
            // dateTimePickerIni
            // 
            this.dateTimePickerIni.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerIni.Location = new System.Drawing.Point(39, 22);
            this.dateTimePickerIni.Name = "dateTimePickerIni";
            this.dateTimePickerIni.Size = new System.Drawing.Size(95, 23);
            this.dateTimePickerIni.TabIndex = 2;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(180)))), ((int)(((byte)(145)))));
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.toolStripProgressBar1,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.toolStripTextBox1,
            this.toolStripLabel2,
            this.toolStripTextBox2,
            this.toolStripSeparator2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(939, 32);
            this.toolStrip1.TabIndex = 14;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportarToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(86, 29);
            this.toolStripDropDownButton1.Text = "Opciones";
            this.toolStripDropDownButton1.ToolTipText = "Opciones";
            // 
            // exportarToolStripMenuItem
            // 
            this.exportarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.excelToolStripMenuItem});
            this.exportarToolStripMenuItem.Name = "exportarToolStripMenuItem";
            this.exportarToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.exportarToolStripMenuItem.Text = "Exportar";
            // 
            // excelToolStripMenuItem
            // 
            this.excelToolStripMenuItem.Name = "excelToolStripMenuItem";
            this.excelToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            this.excelToolStripMenuItem.Text = "Excel";
            this.excelToolStripMenuItem.Click += new System.EventHandler(this.excelToolStripMenuItem_Click);
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripProgressBar1.Margin = new System.Windows.Forms.Padding(1, 2, 5, 1);
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 29);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 32);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(21, 29);
            this.toolStripLabel1.Text = "ID:";
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(100, 32);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(54, 29);
            this.toolStripLabel2.Text = "Nombre:";
            // 
            // toolStripTextBox2
            // 
            this.toolStripTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.toolStripTextBox2.Name = "toolStripTextBox2";
            this.toolStripTextBox2.Size = new System.Drawing.Size(100, 32);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 32);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(939, 605);
            this.panel1.TabIndex = 15;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel8);
            this.panel4.Controls.Add(this.panel9);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 125);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(939, 480);
            this.panel4.TabIndex = 16;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.groupBox1);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(939, 255);
            this.panel8.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridViewDatos);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(939, 255);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos";
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.groupBox2);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel9.Location = new System.Drawing.Point(0, 255);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(939, 225);
            this.panel9.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridViewDetalleDias);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(939, 222);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Detalle Días";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel7);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(939, 125);
            this.panel2.TabIndex = 15;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.groupBox3);
            this.panel7.Controls.Add(this.groupBoxOpciones);
            this.panel7.Controls.Add(this.groupBoxFechas);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(0, 55);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(939, 70);
            this.panel7.TabIndex = 4;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.buttonJustificar);
            this.groupBox3.Location = new System.Drawing.Point(470, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(195, 60);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Edición Inconsistencias";
            // 
            // buttonJustificar
            // 
            this.buttonJustificar.Location = new System.Drawing.Point(54, 22);
            this.buttonJustificar.Name = "buttonJustificar";
            this.buttonJustificar.Size = new System.Drawing.Size(75, 23);
            this.buttonJustificar.TabIndex = 3;
            this.buttonJustificar.Text = "Editar";
            this.buttonJustificar.UseVisualStyleBackColor = true;
            this.buttonJustificar.Click += new System.EventHandler(this.buttonJustificar_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel6);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(939, 55);
            this.panel3.TabIndex = 3;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.toolStrip1);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 23);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(939, 32);
            this.panel6.TabIndex = 3;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label3);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(939, 23);
            this.panel5.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(939, 23);
            this.label3.TabIndex = 2;
            this.label3.Text = "Reporte de Tiempos Quincenal";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ReporteQuincenal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(939, 605);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ReporteQuincenal";
            this.Text = "Reporte Quincenal";
            this.Load += new System.EventHandler(this.ReporteQuincenal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDetalleDias)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDatos)).EndInit();
            this.groupBoxOpciones.ResumeLayout(false);
            this.groupBoxFechas.ResumeLayout(false);
            this.groupBoxFechas.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridViewDetalleDias;
        private System.Windows.Forms.DataGridView dataGridViewDatos;
        private System.Windows.Forms.GroupBox groupBoxOpciones;
        private System.Windows.Forms.Button buttonMarcaciones;
        private System.Windows.Forms.Button buttonAcept;
        private System.Windows.Forms.GroupBox groupBoxFechas;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePickerFin;
        private System.Windows.Forms.DateTimePicker dateTimePickerIni;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem exportarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem excelToolStripMenuItem;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button buttonJustificar;
    }
}