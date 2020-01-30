namespace UHFDemo
{
    partial class ReporteLista
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource4 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.DataSetUsuario = new UHFDemo.DataSetUsuario();
            this.LlamarlistaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.LlamarlistaTableAdapter = new UHFDemo.DataSetUsuarioTableAdapters.LlamarlistaTableAdapter();
            this.productoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.productoTableAdapter = new UHFDemo.DataSetUsuarioTableAdapters.productoTableAdapter();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DataSetUsuario)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LlamarlistaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.productoBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            reportDataSource3.Name = "DataSet1";
            reportDataSource3.Value = this.LlamarlistaBindingSource;
            reportDataSource4.Name = "DataSet2";
            reportDataSource4.Value = this.productoBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource4);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "UHFDemo.ReportLista.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(-1, -1);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(804, 408);
            this.reportViewer1.TabIndex = 0;
            // 
            // DataSetUsuario
            // 
            this.DataSetUsuario.DataSetName = "DataSetUsuario";
            this.DataSetUsuario.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // LlamarlistaBindingSource
            // 
            this.LlamarlistaBindingSource.DataMember = "Llamarlista";
            this.LlamarlistaBindingSource.DataSource = this.DataSetUsuario;
            // 
            // LlamarlistaTableAdapter
            // 
            this.LlamarlistaTableAdapter.ClearBeforeFill = true;
            // 
            // productoBindingSource
            // 
            this.productoBindingSource.DataMember = "producto";
            this.productoBindingSource.DataSource = this.DataSetUsuario;
            // 
            // productoTableAdapter
            // 
            this.productoTableAdapter.ClearBeforeFill = true;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.DarkTurquoise;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.ForeColor = System.Drawing.Color.Red;
            this.button1.Location = new System.Drawing.Point(-1, 404);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(804, 48);
            this.button1.TabIndex = 4;
            this.button1.Text = "SALIR";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // ReporteLista
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.reportViewer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ReporteLista";
            this.Text = "ReporteLista";
            this.Load += new System.EventHandler(this.ReporteLista_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataSetUsuario)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LlamarlistaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.productoBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource LlamarlistaBindingSource;
        private DataSetUsuario DataSetUsuario;
        private System.Windows.Forms.BindingSource productoBindingSource;
        private DataSetUsuarioTableAdapters.LlamarlistaTableAdapter LlamarlistaTableAdapter;
        private DataSetUsuarioTableAdapters.productoTableAdapter productoTableAdapter;
        private System.Windows.Forms.Button button1;
    }
}