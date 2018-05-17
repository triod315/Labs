namespace Lab15
{
    partial class About_form
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
            this.productDeveloperLabel = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.productVersionLabel = new System.Windows.Forms.Label();
            this.productNameLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // productDeveloperLabel
            // 
            this.productDeveloperLabel.AutoSize = true;
            this.productDeveloperLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.productDeveloperLabel.Location = new System.Drawing.Point(12, 91);
            this.productDeveloperLabel.Name = "productDeveloperLabel";
            this.productDeveloperLabel.Size = new System.Drawing.Size(89, 20);
            this.productDeveloperLabel.TabIndex = 0;
            this.productDeveloperLabel.Text = "Developer: ";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.linkLabel1.Location = new System.Drawing.Point(12, 129);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(96, 20);
            this.linkLabel1.TabIndex = 2;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "source code";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // productVersionLabel
            // 
            this.productVersionLabel.AutoSize = true;
            this.productVersionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.productVersionLabel.Location = new System.Drawing.Point(12, 54);
            this.productVersionLabel.Name = "productVersionLabel";
            this.productVersionLabel.Size = new System.Drawing.Size(126, 20);
            this.productVersionLabel.TabIndex = 3;
            this.productVersionLabel.Text = "Product version: ";
            // 
            // productNameLabel
            // 
            this.productNameLabel.AutoSize = true;
            this.productNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.productNameLabel.Location = new System.Drawing.Point(12, 20);
            this.productNameLabel.Name = "productNameLabel";
            this.productNameLabel.Size = new System.Drawing.Size(116, 20);
            this.productNameLabel.TabIndex = 4;
            this.productNameLabel.Text = "Product name: ";
            // 
            // About_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(267, 171);
            this.Controls.Add(this.productNameLabel);
            this.Controls.Add(this.productVersionLabel);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.productDeveloperLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "About_form";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About";
            this.Load += new System.EventHandler(this.About_form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label productDeveloperLabel;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label productVersionLabel;
        private System.Windows.Forms.Label productNameLabel;
    }
}