
namespace EmpManagement
{
    partial class Directorio
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
            this.groupBoxFiltroDept = new System.Windows.Forms.GroupBox();
            this.labelDept = new System.Windows.Forms.Label();
            this.comboBoxDepts = new System.Windows.Forms.ComboBox();
            this.groupBoxBusqueda = new System.Windows.Forms.GroupBox();
            this.textBoxNombre = new System.Windows.Forms.TextBox();
            this.textBoxID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxOp = new System.Windows.Forms.GroupBox();
            this.buttonBaja = new System.Windows.Forms.Button();
            this.buttonActualizar = new System.Windows.Forms.Button();
            this.buttonNew = new System.Windows.Forms.Button();
            this.dataGridViewDatos = new System.Windows.Forms.DataGridView();
            this.groupBoxDatos = new System.Windows.Forms.GroupBox();
            this.groupBoxFiltroDept.SuspendLayout();
            this.groupBoxBusqueda.SuspendLayout();
            this.groupBoxOp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDatos)).BeginInit();
            this.groupBoxDatos.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxFiltroDept
            // 
            this.groupBoxFiltroDept.Controls.Add(this.labelDept);
            this.groupBoxFiltroDept.Controls.Add(this.comboBoxDepts);
            this.groupBoxFiltroDept.Location = new System.Drawing.Point(12, 12);
            this.groupBoxFiltroDept.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBoxFiltroDept.Name = "groupBoxFiltroDept";
            this.groupBoxFiltroDept.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBoxFiltroDept.Size = new System.Drawing.Size(276, 100);
            this.groupBoxFiltroDept.TabIndex = 0;
            this.groupBoxFiltroDept.TabStop = false;
            this.groupBoxFiltroDept.Text = "Filtro por Departamento";
            // 
            // labelDept
            // 
            this.labelDept.AutoSize = true;
            this.labelDept.Location = new System.Drawing.Point(6, 33);
            this.labelDept.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDept.Name = "labelDept";
            this.labelDept.Size = new System.Drawing.Size(89, 15);
            this.labelDept.TabIndex = 1;
            this.labelDept.Text = "Departamento:";
            // 
            // comboBoxDepts
            // 
            this.comboBoxDepts.FormattingEnabled = true;
            this.comboBoxDepts.Location = new System.Drawing.Point(107, 30);
            this.comboBoxDepts.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.comboBoxDepts.Name = "comboBoxDepts";
            this.comboBoxDepts.Size = new System.Drawing.Size(148, 23);
            this.comboBoxDepts.TabIndex = 0;
            this.comboBoxDepts.SelectedIndexChanged += new System.EventHandler(this.comboBoxDepts_SelectedIndexChanged);
            // 
            // groupBoxBusqueda
            // 
            this.groupBoxBusqueda.Controls.Add(this.textBoxNombre);
            this.groupBoxBusqueda.Controls.Add(this.textBoxID);
            this.groupBoxBusqueda.Controls.Add(this.label2);
            this.groupBoxBusqueda.Controls.Add(this.label1);
            this.groupBoxBusqueda.Location = new System.Drawing.Point(294, 12);
            this.groupBoxBusqueda.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBoxBusqueda.Name = "groupBoxBusqueda";
            this.groupBoxBusqueda.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBoxBusqueda.Size = new System.Drawing.Size(276, 100);
            this.groupBoxBusqueda.TabIndex = 1;
            this.groupBoxBusqueda.TabStop = false;
            this.groupBoxBusqueda.Text = "Busqueda";
            // 
            // textBoxNombre
            // 
            this.textBoxNombre.Location = new System.Drawing.Point(113, 64);
            this.textBoxNombre.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBoxNombre.Name = "textBoxNombre";
            this.textBoxNombre.Size = new System.Drawing.Size(148, 21);
            this.textBoxNombre.TabIndex = 3;
            this.textBoxNombre.TextChanged += new System.EventHandler(this.textBoxNombre_TextChanged);
            // 
            // textBoxID
            // 
            this.textBoxID.Location = new System.Drawing.Point(113, 30);
            this.textBoxID.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBoxID.Name = "textBoxID";
            this.textBoxID.Size = new System.Drawing.Size(148, 21);
            this.textBoxID.TabIndex = 2;
            this.textBoxID.TextChanged += new System.EventHandler(this.textBoxID_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 67);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Nombre:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 33);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID:";
            // 
            // groupBoxOp
            // 
            this.groupBoxOp.Controls.Add(this.buttonBaja);
            this.groupBoxOp.Controls.Add(this.buttonActualizar);
            this.groupBoxOp.Controls.Add(this.buttonNew);
            this.groupBoxOp.Location = new System.Drawing.Point(592, 12);
            this.groupBoxOp.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBoxOp.Name = "groupBoxOp";
            this.groupBoxOp.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBoxOp.Size = new System.Drawing.Size(200, 100);
            this.groupBoxOp.TabIndex = 2;
            this.groupBoxOp.TabStop = false;
            this.groupBoxOp.Text = "Opciones";
            // 
            // buttonBaja
            // 
            this.buttonBaja.Location = new System.Drawing.Point(18, 63);
            this.buttonBaja.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonBaja.Name = "buttonBaja";
            this.buttonBaja.Size = new System.Drawing.Size(75, 23);
            this.buttonBaja.TabIndex = 2;
            this.buttonBaja.Text = "Baja";
            this.buttonBaja.UseVisualStyleBackColor = true;
            // 
            // buttonActualizar
            // 
            this.buttonActualizar.Location = new System.Drawing.Point(111, 29);
            this.buttonActualizar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonActualizar.Name = "buttonActualizar";
            this.buttonActualizar.Size = new System.Drawing.Size(75, 23);
            this.buttonActualizar.TabIndex = 1;
            this.buttonActualizar.Text = "Actualizar";
            this.buttonActualizar.UseVisualStyleBackColor = true;
            this.buttonActualizar.Click += new System.EventHandler(this.buttonActualizar_Click);
            // 
            // buttonNew
            // 
            this.buttonNew.Location = new System.Drawing.Point(18, 29);
            this.buttonNew.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonNew.Name = "buttonNew";
            this.buttonNew.Size = new System.Drawing.Size(75, 23);
            this.buttonNew.TabIndex = 0;
            this.buttonNew.Text = "Nuevo";
            this.buttonNew.UseVisualStyleBackColor = true;
            this.buttonNew.Click += new System.EventHandler(this.buttonNew_Click);
            // 
            // dataGridViewDatos
            // 
            this.dataGridViewDatos.AllowUserToAddRows = false;
            this.dataGridViewDatos.AllowUserToDeleteRows = false;
            this.dataGridViewDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDatos.Location = new System.Drawing.Point(6, 22);
            this.dataGridViewDatos.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dataGridViewDatos.Name = "dataGridViewDatos";
            this.dataGridViewDatos.RowTemplate.Height = 25;
            this.dataGridViewDatos.Size = new System.Drawing.Size(878, 397);
            this.dataGridViewDatos.TabIndex = 3;
            // 
            // groupBoxDatos
            // 
            this.groupBoxDatos.Controls.Add(this.dataGridViewDatos);
            this.groupBoxDatos.Location = new System.Drawing.Point(12, 128);
            this.groupBoxDatos.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBoxDatos.Name = "groupBoxDatos";
            this.groupBoxDatos.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBoxDatos.Size = new System.Drawing.Size(890, 425);
            this.groupBoxDatos.TabIndex = 4;
            this.groupBoxDatos.TabStop = false;
            this.groupBoxDatos.Text = "Datos";
            // 
            // Directorio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(913, 565);
            this.Controls.Add(this.groupBoxDatos);
            this.Controls.Add(this.groupBoxOp);
            this.Controls.Add(this.groupBoxBusqueda);
            this.Controls.Add(this.groupBoxFiltroDept);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Directorio";
            this.Text = "Directorio";
            this.Load += new System.EventHandler(this.Directorio_Load);
            this.groupBoxFiltroDept.ResumeLayout(false);
            this.groupBoxFiltroDept.PerformLayout();
            this.groupBoxBusqueda.ResumeLayout(false);
            this.groupBoxBusqueda.PerformLayout();
            this.groupBoxOp.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDatos)).EndInit();
            this.groupBoxDatos.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxFiltroDept;
        private System.Windows.Forms.Label labelDept;
        private System.Windows.Forms.ComboBox comboBoxDepts;
        private System.Windows.Forms.GroupBox groupBoxBusqueda;
        private System.Windows.Forms.TextBox textBoxNombre;
        private System.Windows.Forms.TextBox textBoxID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBoxOp;
        private System.Windows.Forms.Button buttonBaja;
        private System.Windows.Forms.Button buttonActualizar;
        private System.Windows.Forms.Button buttonNew;
        private System.Windows.Forms.DataGridView dataGridViewDatos;
        private System.Windows.Forms.GroupBox groupBoxDatos;
    }
}