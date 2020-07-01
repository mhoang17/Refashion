namespace Refashion_UI
{
    partial class Form1
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
            this.sellerListView = new System.Windows.Forms.ListView();
            this.SellerTag = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SellerName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.userListSearchBox = new System.Windows.Forms.ComboBox();
            this.newSellerBtn = new System.Windows.Forms.Button();
            this.newUserHeader = new System.Windows.Forms.Label();
            this.sellerFirstNameLabel = new System.Windows.Forms.Label();
            this.sellerAddressLabel = new System.Windows.Forms.Label();
            this.sellerZipLabel = new System.Windows.Forms.Label();
            this.sellerPhoneLabel = new System.Windows.Forms.Label();
            this.sellerCityLabel = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.sellerFirstNameTextBox = new System.Windows.Forms.TextBox();
            this.sellerAddressTextBox = new System.Windows.Forms.TextBox();
            this.sellerCityTextBox = new System.Windows.Forms.TextBox();
            this.sellerZIPTextBox = new System.Windows.Forms.TextBox();
            this.sellerPhoneTextBox = new System.Windows.Forms.TextBox();
            this.saveNewSellerBtn = new System.Windows.Forms.Button();
            this.sellerLastNameTextBox = new System.Windows.Forms.TextBox();
            this.sellerLastNameLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // sellerListView
            // 
            this.sellerListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.SellerTag,
            this.SellerName});
            this.sellerListView.FullRowSelect = true;
            this.sellerListView.HideSelection = false;
            this.sellerListView.Location = new System.Drawing.Point(64, 83);
            this.sellerListView.Name = "sellerListView";
            this.sellerListView.Size = new System.Drawing.Size(330, 782);
            this.sellerListView.TabIndex = 0;
            this.sellerListView.UseCompatibleStateImageBehavior = false;
            this.sellerListView.View = System.Windows.Forms.View.Details;
            // 
            // SellerTag
            // 
            this.SellerTag.Text = "Tag";
            this.SellerTag.Width = 100;
            // 
            // SellerName
            // 
            this.SellerName.Text = "Name";
            this.SellerName.Width = 180;
            // 
            // userListSearchBox
            // 
            this.userListSearchBox.FormattingEnabled = true;
            this.userListSearchBox.ItemHeight = 20;
            this.userListSearchBox.Location = new System.Drawing.Point(64, 83);
            this.userListSearchBox.Name = "userListSearchBox";
            this.userListSearchBox.Size = new System.Drawing.Size(330, 28);
            this.userListSearchBox.TabIndex = 1;
            // 
            // newSellerBtn
            // 
            this.newSellerBtn.Location = new System.Drawing.Point(64, 871);
            this.newSellerBtn.Name = "newSellerBtn";
            this.newSellerBtn.Size = new System.Drawing.Size(330, 49);
            this.newSellerBtn.TabIndex = 2;
            this.newSellerBtn.Text = "Ny Sælger";
            this.newSellerBtn.UseVisualStyleBackColor = true;
            // 
            // newUserHeader
            // 
            this.newUserHeader.AutoSize = true;
            this.newUserHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.newUserHeader.Location = new System.Drawing.Point(484, 71);
            this.newUserHeader.Name = "newUserHeader";
            this.newUserHeader.Size = new System.Drawing.Size(215, 47);
            this.newUserHeader.TabIndex = 3;
            this.newUserHeader.Text = "Ny Sælger";
            // 
            // sellerFirstNameLabel
            // 
            this.sellerFirstNameLabel.AutoSize = true;
            this.sellerFirstNameLabel.Location = new System.Drawing.Point(549, 174);
            this.sellerFirstNameLabel.Name = "sellerFirstNameLabel";
            this.sellerFirstNameLabel.Size = new System.Drawing.Size(71, 20);
            this.sellerFirstNameLabel.TabIndex = 4;
            this.sellerFirstNameLabel.Text = "Fornavn:";
            // 
            // sellerAddressLabel
            // 
            this.sellerAddressLabel.AutoSize = true;
            this.sellerAddressLabel.Location = new System.Drawing.Point(549, 248);
            this.sellerAddressLabel.Name = "sellerAddressLabel";
            this.sellerAddressLabel.Size = new System.Drawing.Size(72, 20);
            this.sellerAddressLabel.TabIndex = 5;
            this.sellerAddressLabel.Text = "Adresse:";
            // 
            // sellerZipLabel
            // 
            this.sellerZipLabel.AutoSize = true;
            this.sellerZipLabel.Location = new System.Drawing.Point(519, 328);
            this.sellerZipLabel.Name = "sellerZipLabel";
            this.sellerZipLabel.Size = new System.Drawing.Size(103, 20);
            this.sellerZipLabel.TabIndex = 6;
            this.sellerZipLabel.Text = "Postnummer:";
            // 
            // sellerPhoneLabel
            // 
            this.sellerPhoneLabel.AutoSize = true;
            this.sellerPhoneLabel.Location = new System.Drawing.Point(496, 369);
            this.sellerPhoneLabel.Name = "sellerPhoneLabel";
            this.sellerPhoneLabel.Size = new System.Drawing.Size(124, 20);
            this.sellerPhoneLabel.TabIndex = 7;
            this.sellerPhoneLabel.Text = "Telefonnummer:";
            // 
            // sellerCityLabel
            // 
            this.sellerCityLabel.AutoSize = true;
            this.sellerCityLabel.Location = new System.Drawing.Point(588, 283);
            this.sellerCityLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.sellerCityLabel.Name = "sellerCityLabel";
            this.sellerCityLabel.Size = new System.Drawing.Size(31, 20);
            this.sellerCityLabel.TabIndex = 8;
            this.sellerCityLabel.Text = "By:";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // sellerFirstNameTextBox
            // 
            this.sellerFirstNameTextBox.Location = new System.Drawing.Point(657, 171);
            this.sellerFirstNameTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.sellerFirstNameTextBox.Name = "sellerFirstNameTextBox";
            this.sellerFirstNameTextBox.Size = new System.Drawing.Size(506, 26);
            this.sellerFirstNameTextBox.TabIndex = 10;
            // 
            // sellerAddressTextBox
            // 
            this.sellerAddressTextBox.Location = new System.Drawing.Point(657, 243);
            this.sellerAddressTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.sellerAddressTextBox.Name = "sellerAddressTextBox";
            this.sellerAddressTextBox.Size = new System.Drawing.Size(506, 26);
            this.sellerAddressTextBox.TabIndex = 11;
            // 
            // sellerCityTextBox
            // 
            this.sellerCityTextBox.Location = new System.Drawing.Point(657, 283);
            this.sellerCityTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.sellerCityTextBox.Name = "sellerCityTextBox";
            this.sellerCityTextBox.Size = new System.Drawing.Size(506, 26);
            this.sellerCityTextBox.TabIndex = 12;
            // 
            // sellerZIPTextBox
            // 
            this.sellerZIPTextBox.Location = new System.Drawing.Point(657, 325);
            this.sellerZIPTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.sellerZIPTextBox.Name = "sellerZIPTextBox";
            this.sellerZIPTextBox.Size = new System.Drawing.Size(506, 26);
            this.sellerZIPTextBox.TabIndex = 13;
            this.sellerZIPTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.sellerZIPTextBox_KeyPress);
            // 
            // sellerPhoneTextBox
            // 
            this.sellerPhoneTextBox.Location = new System.Drawing.Point(657, 365);
            this.sellerPhoneTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.sellerPhoneTextBox.Name = "sellerPhoneTextBox";
            this.sellerPhoneTextBox.Size = new System.Drawing.Size(506, 26);
            this.sellerPhoneTextBox.TabIndex = 14;
            this.sellerPhoneTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.sellerPhoneTextBox_KeyPress);
            // 
            // saveNewSellerBtn
            // 
            this.saveNewSellerBtn.Location = new System.Drawing.Point(1053, 445);
            this.saveNewSellerBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.saveNewSellerBtn.Name = "saveNewSellerBtn";
            this.saveNewSellerBtn.Size = new System.Drawing.Size(112, 35);
            this.saveNewSellerBtn.TabIndex = 15;
            this.saveNewSellerBtn.Text = "Gem";
            this.saveNewSellerBtn.UseVisualStyleBackColor = true;
            this.saveNewSellerBtn.Click += new System.EventHandler(this.saveNewSellerBtn_Click);
            // 
            // sellerLastNameTextBox
            // 
            this.sellerLastNameTextBox.Location = new System.Drawing.Point(657, 207);
            this.sellerLastNameTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.sellerLastNameTextBox.Name = "sellerLastNameTextBox";
            this.sellerLastNameTextBox.Size = new System.Drawing.Size(506, 26);
            this.sellerLastNameTextBox.TabIndex = 17;
            // 
            // sellerLastNameLabel
            // 
            this.sellerLastNameLabel.AutoSize = true;
            this.sellerLastNameLabel.Location = new System.Drawing.Point(540, 210);
            this.sellerLastNameLabel.Name = "sellerLastNameLabel";
            this.sellerLastNameLabel.Size = new System.Drawing.Size(82, 20);
            this.sellerLastNameLabel.TabIndex = 16;
            this.sellerLastNameLabel.Text = "Efternavn:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1671, 1002);
            this.Controls.Add(this.sellerLastNameTextBox);
            this.Controls.Add(this.sellerLastNameLabel);
            this.Controls.Add(this.saveNewSellerBtn);
            this.Controls.Add(this.sellerPhoneTextBox);
            this.Controls.Add(this.sellerZIPTextBox);
            this.Controls.Add(this.sellerCityTextBox);
            this.Controls.Add(this.sellerAddressTextBox);
            this.Controls.Add(this.sellerFirstNameTextBox);
            this.Controls.Add(this.sellerCityLabel);
            this.Controls.Add(this.sellerPhoneLabel);
            this.Controls.Add(this.sellerZipLabel);
            this.Controls.Add(this.sellerAddressLabel);
            this.Controls.Add(this.sellerFirstNameLabel);
            this.Controls.Add(this.newUserHeader);
            this.Controls.Add(this.newSellerBtn);
            this.Controls.Add(this.userListSearchBox);
            this.Controls.Add(this.sellerListView);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView sellerListView;
        private System.Windows.Forms.ComboBox userListSearchBox;
        private System.Windows.Forms.Button newSellerBtn;
        private System.Windows.Forms.Label newUserHeader;
        private System.Windows.Forms.Label sellerFirstNameLabel;
        private System.Windows.Forms.Label sellerAddressLabel;
        private System.Windows.Forms.Label sellerZipLabel;
        private System.Windows.Forms.Label sellerPhoneLabel;
        private System.Windows.Forms.Label sellerCityLabel;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TextBox sellerFirstNameTextBox;
        private System.Windows.Forms.TextBox sellerAddressTextBox;
        private System.Windows.Forms.TextBox sellerCityTextBox;
        private System.Windows.Forms.TextBox sellerZIPTextBox;
        private System.Windows.Forms.TextBox sellerPhoneTextBox;
        private System.Windows.Forms.Button saveNewSellerBtn;
        private System.Windows.Forms.ColumnHeader SellerTag;
        private System.Windows.Forms.ColumnHeader SellerName;
        private System.Windows.Forms.TextBox sellerLastNameTextBox;
        private System.Windows.Forms.Label sellerLastNameLabel;
    }
}

