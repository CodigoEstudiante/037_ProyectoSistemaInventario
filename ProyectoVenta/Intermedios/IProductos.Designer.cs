namespace ProyectoVenta.Intermedios
{
    partial class IProductos
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
            this.btnagregarproductos = new FontAwesome.Sharp.IconButton();
            this.btnvolver = new FontAwesome.Sharp.IconButton();
            this.btncargar = new FontAwesome.Sharp.IconButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnagregarproductos
            // 
            this.btnagregarproductos.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnagregarproductos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnagregarproductos.FlatAppearance.BorderColor = System.Drawing.Color.Teal;
            this.btnagregarproductos.FlatAppearance.BorderSize = 2;
            this.btnagregarproductos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnagregarproductos.ForeColor = System.Drawing.Color.Black;
            this.btnagregarproductos.IconChar = FontAwesome.Sharp.IconChar.PlusCircle;
            this.btnagregarproductos.IconColor = System.Drawing.Color.Black;
            this.btnagregarproductos.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnagregarproductos.IconSize = 80;
            this.btnagregarproductos.Location = new System.Drawing.Point(23, 49);
            this.btnagregarproductos.Name = "btnagregarproductos";
            this.btnagregarproductos.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.btnagregarproductos.Size = new System.Drawing.Size(87, 92);
            this.btnagregarproductos.TabIndex = 3;
            this.btnagregarproductos.Text = "Nuevo";
            this.btnagregarproductos.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnagregarproductos.UseVisualStyleBackColor = false;
            this.btnagregarproductos.Click += new System.EventHandler(this.btnagregarproductos_Click);
            // 
            // btnvolver
            // 
            this.btnvolver.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnvolver.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnvolver.FlatAppearance.BorderColor = System.Drawing.Color.Teal;
            this.btnvolver.FlatAppearance.BorderSize = 2;
            this.btnvolver.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnvolver.ForeColor = System.Drawing.Color.Black;
            this.btnvolver.IconChar = FontAwesome.Sharp.IconChar.TimesCircle;
            this.btnvolver.IconColor = System.Drawing.Color.Black;
            this.btnvolver.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnvolver.IconSize = 80;
            this.btnvolver.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnvolver.Location = new System.Drawing.Point(247, 49);
            this.btnvolver.Name = "btnvolver";
            this.btnvolver.Size = new System.Drawing.Size(87, 92);
            this.btnvolver.TabIndex = 4;
            this.btnvolver.Text = "Cerrar";
            this.btnvolver.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnvolver.UseVisualStyleBackColor = false;
            this.btnvolver.Click += new System.EventHandler(this.btnvolver_Click);
            // 
            // btncargar
            // 
            this.btncargar.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btncargar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btncargar.FlatAppearance.BorderColor = System.Drawing.Color.Teal;
            this.btncargar.FlatAppearance.BorderSize = 2;
            this.btncargar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncargar.ForeColor = System.Drawing.Color.Black;
            this.btncargar.IconChar = FontAwesome.Sharp.IconChar.Upload;
            this.btncargar.IconColor = System.Drawing.Color.Black;
            this.btncargar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btncargar.IconSize = 80;
            this.btncargar.Location = new System.Drawing.Point(135, 49);
            this.btncargar.Name = "btncargar";
            this.btncargar.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.btncargar.Size = new System.Drawing.Size(87, 92);
            this.btncargar.TabIndex = 7;
            this.btncargar.Text = "Cargar";
            this.btncargar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btncargar.UseVisualStyleBackColor = false;
            this.btncargar.Click += new System.EventHandler(this.btncargar_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(348, 152);
            this.label1.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.White;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Teal;
            this.label7.Location = new System.Drawing.Point(20, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 15);
            this.label7.TabIndex = 113;
            this.label7.Text = "PRODUCTOS";
            // 
            // IProductos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Red;
            this.ClientSize = new System.Drawing.Size(357, 161);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btncargar);
            this.Controls.Add(this.btnvolver);
            this.Controls.Add(this.btnagregarproductos);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "IProductos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "IProductos";
            this.Load += new System.EventHandler(this.IProductos_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FontAwesome.Sharp.IconButton btnagregarproductos;
        private FontAwesome.Sharp.IconButton btnvolver;
        private FontAwesome.Sharp.IconButton btncargar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
    }
}