namespace UHFDemo
{
    partial class r_S_Pcs
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.salida_productoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DataSetUsuario = new UHFDemo.DataSetUsuario();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.salida_productoTableAdapter = new UHFDemo.DataSetUsuarioTableAdapters.salida_productoTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.salida_productoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSetUsuario)).BeginInit();
            this.SuspendLayout();
            // 
            // salida_productoBindingSource
            // 
            this.salida_productoBindingSource.DataMember = "salida_producto";
            this.salida_productoBindingSource.DataSource = this.DataSetUsuario;
            // 
            // DataSetUsuario
            // 
            this.DataSetUsuario.DataSetName = "DataSetUsuario";
            this.DataSetUsuario.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.salida_productoBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "UHFDemo.ReportSalida.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(800, 450);
            this.reportViewer1.TabIndex = 0;
            // 
            // salida_productoTableAdapter
            // 
            this.salida_productoTableAdapter.ClearBeforeFill = true;
            // 
            // r_S_Pcs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.reportViewer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "r_S_Pcs";
            this.Text = "r_S_Pcs";
            this.Load += new System.EventHandler(this.R_S_Pcs_Load);
            ((System.ComponentModel.ISupportInitialize)(this.salida_productoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSetUsuario)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource salida_productoBindingSource;
        private DataSetUsuario DataSetUsuario;
        private DataSetUsuarioTableAdapters.salida_productoTableAdapter salida_productoTableAdapter;
    }
}