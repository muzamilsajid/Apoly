namespace Monopoly
{
    partial class frmBoard
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
            this.txtTileName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTileValue = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTileRent = new System.Windows.Forms.TextBox();
            this.listBoxTileNames = new System.Windows.Forms.ListBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // txtTileName
            // 
            this.txtTileName.Location = new System.Drawing.Point(83, 19);
            this.txtTileName.Name = "txtTileName";
            this.txtTileName.Size = new System.Drawing.Size(135, 20);
            this.txtTileName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Tile Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Tile Value:";
            // 
            // txtTileValue
            // 
            this.txtTileValue.Location = new System.Drawing.Point(83, 45);
            this.txtTileValue.Name = "txtTileValue";
            this.txtTileValue.Size = new System.Drawing.Size(135, 20);
            this.txtTileValue.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Tile Rent:";
            // 
            // txtTileRent
            // 
            this.txtTileRent.Location = new System.Drawing.Point(83, 71);
            this.txtTileRent.Name = "txtTileRent";
            this.txtTileRent.Size = new System.Drawing.Size(135, 20);
            this.txtTileRent.TabIndex = 4;
            // 
            // listBoxTileNames
            // 
            this.listBoxTileNames.FormattingEnabled = true;
            this.listBoxTileNames.Location = new System.Drawing.Point(83, 106);
            this.listBoxTileNames.Name = "listBoxTileNames";
            this.listBoxTileNames.Size = new System.Drawing.Size(135, 212);
            this.listBoxTileNames.TabIndex = 6;
            this.listBoxTileNames.SelectedIndexChanged += new System.EventHandler(this.listBoxTileNames_SelectedIndexChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(224, 69);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 7;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(265, 106);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(135, 212);
            this.listBox1.TabIndex = 8;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // frmBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(703, 364);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.listBoxTileNames);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtTileRent);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtTileValue);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTileName);
            this.Name = "frmBoard";
            this.Text = "frmBoard";
            this.Load += new System.EventHandler(this.frmBoard_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtTileName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTileValue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTileRent;
        private System.Windows.Forms.ListBox listBoxTileNames;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ListBox listBox1;
    }
}