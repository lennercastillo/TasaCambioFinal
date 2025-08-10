namespace TasaCambio
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.label4 = new System.Windows.Forms.Label();
            this.BotonNuevo = new System.Windows.Forms.Button();
            this.BotonEditar = new System.Windows.Forms.Button();
            this.BotonEliminar = new System.Windows.Forms.Button();
            this.textBox_tasa = new System.Windows.Forms.TextBox();
            this.dateTimePicker_fecha = new System.Windows.Forms.DateTimePicker();
            this.button_guardar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.formAssistant1 = new DevExpress.XtraBars.FormAssistant();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn_fecha = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_tasa = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridControl_tasacambio = new DevExpress.XtraGrid.GridControl();
            this.button_recargar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_tasacambio)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label4.Location = new System.Drawing.Point(306, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(225, 26);
            this.label4.TabIndex = 1;
            this.label4.Text = "Maestro Tasa Cambio";
//            this.label4.Click += new System.EventHandler(this.label1_Click);
            // 
            // BotonNuevo
            // 
            this.BotonNuevo.ForeColor = System.Drawing.Color.Black;
            this.BotonNuevo.Location = new System.Drawing.Point(1, 55);
            this.BotonNuevo.Name = "BotonNuevo";
            this.BotonNuevo.Size = new System.Drawing.Size(86, 31);
            this.BotonNuevo.TabIndex = 2;
            this.BotonNuevo.Text = "NUEVO";
            this.BotonNuevo.UseVisualStyleBackColor = true;
            this.BotonNuevo.Click += new System.EventHandler(this.BotonNuevo_Click);
            // 
            // BotonEditar
            // 
            this.BotonEditar.ForeColor = System.Drawing.Color.Black;
            this.BotonEditar.Location = new System.Drawing.Point(93, 57);
            this.BotonEditar.Name = "BotonEditar";
            this.BotonEditar.Size = new System.Drawing.Size(83, 29);
            this.BotonEditar.TabIndex = 3;
            this.BotonEditar.Text = "EDITAR";
            this.BotonEditar.UseVisualStyleBackColor = true;
            this.BotonEditar.Click += new System.EventHandler(this.BotonEditar_Click);
            // 
            // BotonEliminar
            // 
            this.BotonEliminar.ForeColor = System.Drawing.Color.Black;
            this.BotonEliminar.Location = new System.Drawing.Point(182, 57);
            this.BotonEliminar.Name = "BotonEliminar";
            this.BotonEliminar.Size = new System.Drawing.Size(83, 29);
            this.BotonEliminar.TabIndex = 4;
            this.BotonEliminar.Text = "ELIMINAR";
            this.BotonEliminar.UseVisualStyleBackColor = true;
            this.BotonEliminar.Click += new System.EventHandler(this.BotonEliminar_Click);
            // 
            // textBox_tasa
            // 
            this.textBox_tasa.Location = new System.Drawing.Point(247, 122);
            this.textBox_tasa.Name = "textBox_tasa";
            this.textBox_tasa.Size = new System.Drawing.Size(147, 21);
            this.textBox_tasa.TabIndex = 5;
            // 
            // dateTimePicker_fecha
            // 
            this.dateTimePicker_fecha.CustomFormat = "dd/MM/yyyy";
            this.dateTimePicker_fecha.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker_fecha.Location = new System.Drawing.Point(12, 122);
            this.dateTimePicker_fecha.Name = "dateTimePicker_fecha";
            this.dateTimePicker_fecha.Size = new System.Drawing.Size(200, 21);
            this.dateTimePicker_fecha.TabIndex = 6;
            // 
            // button_guardar
            // 
            this.button_guardar.ForeColor = System.Drawing.Color.Black;
            this.button_guardar.Location = new System.Drawing.Point(682, 112);
            this.button_guardar.Name = "button_guardar";
            this.button_guardar.Size = new System.Drawing.Size(106, 31);
            this.button_guardar.TabIndex = 7;
            this.button_guardar.Text = "GUARDAR";
            this.button_guardar.UseVisualStyleBackColor = true;
            this.button_guardar.Click += new System.EventHandler(this.button_guardar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(90, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Fecha:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(244, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Ingrese la tasa a agregar:";
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn_fecha,
            this.gridColumn_tasa});
            this.gridView1.GridControl = this.gridControl_tasacambio;
            this.gridView1.Name = "gridView1";
            // 
            // gridColumn_fecha
            // 
            this.gridColumn_fecha.Caption = "Fecha";
            this.gridColumn_fecha.FieldName = "fecha";
            this.gridColumn_fecha.Name = "gridColumn_fecha";
            this.gridColumn_fecha.OptionsColumn.ReadOnly = true;
            this.gridColumn_fecha.Visible = true;
            this.gridColumn_fecha.VisibleIndex = 0;
            // 
            // gridColumn_tasa
            // 
            this.gridColumn_tasa.Caption = "tasa";
            this.gridColumn_tasa.FieldName = "tasa";
            this.gridColumn_tasa.Name = "gridColumn_tasa";
            this.gridColumn_tasa.OptionsColumn.ReadOnly = true;
            this.gridColumn_tasa.Visible = true;
            this.gridColumn_tasa.VisibleIndex = 1;
            // 
            // gridControl_tasacambio
            // 
            this.gridControl_tasacambio.Location = new System.Drawing.Point(12, 198);
            this.gridControl_tasacambio.MainView = this.gridView1;
            this.gridControl_tasacambio.Name = "gridControl_tasacambio";
            this.gridControl_tasacambio.Size = new System.Drawing.Size(776, 167);
            this.gridControl_tasacambio.TabIndex = 10;
            this.gridControl_tasacambio.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // button_recargar
            // 
            this.button_recargar.ForeColor = System.Drawing.Color.Black;
            this.button_recargar.Location = new System.Drawing.Point(570, 112);
            this.button_recargar.Name = "button_recargar";
            this.button_recargar.Size = new System.Drawing.Size(106, 31);
            this.button_recargar.TabIndex = 11;
            this.button_recargar.Text = "CANCELAR";
            this.button_recargar.UseVisualStyleBackColor = true;
            this.button_recargar.Click += new System.EventHandler(this.button_recargar_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(817, 445);
            this.Controls.Add(this.button_recargar);
            this.Controls.Add(this.gridControl_tasacambio);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_guardar);
            this.Controls.Add(this.dateTimePicker_fecha);
            this.Controls.Add(this.textBox_tasa);
            this.Controls.Add(this.BotonEliminar);
            this.Controls.Add(this.BotonEditar);
            this.Controls.Add(this.BotonNuevo);
            this.Controls.Add(this.label4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_tasacambio)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button BotonNuevo;
        private System.Windows.Forms.Button BotonEditar;
        private System.Windows.Forms.Button BotonEliminar;
        private System.Windows.Forms.TextBox textBox_tasa;
        private System.Windows.Forms.DateTimePicker dateTimePicker_fecha;
        private System.Windows.Forms.Button button_guardar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraBars.FormAssistant formAssistant1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_fecha;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn_tasa;
        private DevExpress.XtraGrid.GridControl gridControl_tasacambio;
        private System.Windows.Forms.Button button_recargar;
    }
}

