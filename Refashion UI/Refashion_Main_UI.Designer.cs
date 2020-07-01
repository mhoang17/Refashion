namespace Refashion_UI
{
    partial class Refashion_Main_UI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Refashion_Main_UI));
            this.sellerListView = new System.Windows.Forms.ListView();
            this.SellerTag = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SellerName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.userListSearchBox = new System.Windows.Forms.ComboBox();
            this.newSellerBtn = new System.Windows.Forms.Button();
            this.sellerNameLabel = new System.Windows.Forms.Label();
            this.sellerAddressLabel = new System.Windows.Forms.Label();
            this.sellerZipLabel = new System.Windows.Forms.Label();
            this.sellerPhoneLabel = new System.Windows.Forms.Label();
            this.sellerCityLabel = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.sellerNameTextBox = new System.Windows.Forms.TextBox();
            this.sellerAddressTextBox = new System.Windows.Forms.TextBox();
            this.sellerCityTextBox = new System.Windows.Forms.TextBox();
            this.sellerZIPTextBox = new System.Windows.Forms.TextBox();
            this.sellerPhoneTextBox = new System.Windows.Forms.TextBox();
            this.saveNewSellerBtn = new System.Windows.Forms.Button();
            this.newSellerGroupBox = new System.Windows.Forms.GroupBox();
            this.sellerEmailLabel = new System.Windows.Forms.Label();
            this.sellerEmailTextBox = new System.Windows.Forms.TextBox();
            this.tabPage = new System.Windows.Forms.TabControl();
            this.sellerTab = new System.Windows.Forms.TabPage();
            this.sellerSeparator = new System.Windows.Forms.Label();
            this.sellerInformationGroupBox = new System.Windows.Forms.GroupBox();
            this.sellerInformation = new System.Windows.Forms.GroupBox();
            this.sellerTagInfoLabel = new System.Windows.Forms.Label();
            this.cancelSellerInfoBtn = new System.Windows.Forms.Button();
            this.saveSellerInfoBtn = new System.Windows.Forms.Button();
            this.sellerEmailInfoBox = new System.Windows.Forms.TextBox();
            this.sellerPhoneInfoBox = new System.Windows.Forms.TextBox();
            this.sellerZIPCityInfoBox = new System.Windows.Forms.TextBox();
            this.sellerAddressInfoBox = new System.Windows.Forms.TextBox();
            this.sellerNameInfoBox = new System.Windows.Forms.TextBox();
            this.sellerJoinDateInfoBox = new System.Windows.Forms.Label();
            this.editSellerInfoBtn = new System.Windows.Forms.Button();
            this.sellerTabPage = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabIcons = new System.Windows.Forms.ImageList(this.components);
            this.newSellerGroupBox.SuspendLayout();
            this.tabPage.SuspendLayout();
            this.sellerTab.SuspendLayout();
            this.sellerInformationGroupBox.SuspendLayout();
            this.sellerInformation.SuspendLayout();
            this.SuspendLayout();
            // 
            // sellerListView
            // 
            this.sellerListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.SellerTag,
            this.SellerName});
            this.sellerListView.FullRowSelect = true;
            this.sellerListView.HideSelection = false;
            this.sellerListView.Location = new System.Drawing.Point(53, 155);
            this.sellerListView.Name = "sellerListView";
            this.sellerListView.Size = new System.Drawing.Size(330, 700);
            this.sellerListView.TabIndex = 0;
            this.sellerListView.UseCompatibleStateImageBehavior = false;
            this.sellerListView.View = System.Windows.Forms.View.Details;
            this.sellerListView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.sellerListView_ItemSelectionChanged);
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
            this.userListSearchBox.Location = new System.Drawing.Point(53, 155);
            this.userListSearchBox.Name = "userListSearchBox";
            this.userListSearchBox.Size = new System.Drawing.Size(330, 28);
            this.userListSearchBox.TabIndex = 1;
            // 
            // newSellerBtn
            // 
            this.newSellerBtn.BackColor = System.Drawing.Color.LightGreen;
            this.newSellerBtn.Location = new System.Drawing.Point(53, 852);
            this.newSellerBtn.Name = "newSellerBtn";
            this.newSellerBtn.Size = new System.Drawing.Size(330, 49);
            this.newSellerBtn.TabIndex = 2;
            this.newSellerBtn.Text = "Ny Sælger";
            this.newSellerBtn.UseVisualStyleBackColor = false;
            this.newSellerBtn.Click += new System.EventHandler(this.newSellerBtn_Click);
            // 
            // sellerNameLabel
            // 
            this.sellerNameLabel.AutoSize = true;
            this.sellerNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sellerNameLabel.Location = new System.Drawing.Point(268, 116);
            this.sellerNameLabel.Name = "sellerNameLabel";
            this.sellerNameLabel.Size = new System.Drawing.Size(64, 25);
            this.sellerNameLabel.TabIndex = 4;
            this.sellerNameLabel.Text = "Navn:";
            // 
            // sellerAddressLabel
            // 
            this.sellerAddressLabel.AutoSize = true;
            this.sellerAddressLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sellerAddressLabel.Location = new System.Drawing.Point(242, 198);
            this.sellerAddressLabel.Name = "sellerAddressLabel";
            this.sellerAddressLabel.Size = new System.Drawing.Size(91, 25);
            this.sellerAddressLabel.TabIndex = 5;
            this.sellerAddressLabel.Text = "Adresse:";
            // 
            // sellerZipLabel
            // 
            this.sellerZipLabel.AutoSize = true;
            this.sellerZipLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sellerZipLabel.Location = new System.Drawing.Point(211, 239);
            this.sellerZipLabel.Name = "sellerZipLabel";
            this.sellerZipLabel.Size = new System.Drawing.Size(128, 25);
            this.sellerZipLabel.TabIndex = 6;
            this.sellerZipLabel.Text = "Postnummer:";
            // 
            // sellerPhoneLabel
            // 
            this.sellerPhoneLabel.AutoSize = true;
            this.sellerPhoneLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sellerPhoneLabel.Location = new System.Drawing.Point(188, 282);
            this.sellerPhoneLabel.Name = "sellerPhoneLabel";
            this.sellerPhoneLabel.Size = new System.Drawing.Size(155, 25);
            this.sellerPhoneLabel.TabIndex = 7;
            this.sellerPhoneLabel.Text = "Telefonnummer:";
            // 
            // sellerCityLabel
            // 
            this.sellerCityLabel.AutoSize = true;
            this.sellerCityLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sellerCityLabel.Location = new System.Drawing.Point(543, 239);
            this.sellerCityLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.sellerCityLabel.Name = "sellerCityLabel";
            this.sellerCityLabel.Size = new System.Drawing.Size(41, 25);
            this.sellerCityLabel.TabIndex = 8;
            this.sellerCityLabel.Text = "By:";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // sellerNameTextBox
            // 
            this.sellerNameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sellerNameTextBox.Location = new System.Drawing.Point(370, 116);
            this.sellerNameTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.sellerNameTextBox.Name = "sellerNameTextBox";
            this.sellerNameTextBox.Size = new System.Drawing.Size(506, 30);
            this.sellerNameTextBox.TabIndex = 10;
            this.sellerNameTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.sellerNameTextBox_KeyDown);
            // 
            // sellerAddressTextBox
            // 
            this.sellerAddressTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sellerAddressTextBox.Location = new System.Drawing.Point(370, 196);
            this.sellerAddressTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.sellerAddressTextBox.Name = "sellerAddressTextBox";
            this.sellerAddressTextBox.Size = new System.Drawing.Size(506, 30);
            this.sellerAddressTextBox.TabIndex = 13;
            this.sellerAddressTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.sellerAddressTextBox_KeyDown);
            // 
            // sellerCityTextBox
            // 
            this.sellerCityTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sellerCityTextBox.Location = new System.Drawing.Point(604, 236);
            this.sellerCityTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.sellerCityTextBox.Name = "sellerCityTextBox";
            this.sellerCityTextBox.Size = new System.Drawing.Size(272, 30);
            this.sellerCityTextBox.TabIndex = 15;
            this.sellerCityTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.sellerCityTextBox_KeyDown);
            // 
            // sellerZIPTextBox
            // 
            this.sellerZIPTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sellerZIPTextBox.Location = new System.Drawing.Point(369, 236);
            this.sellerZIPTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.sellerZIPTextBox.Name = "sellerZIPTextBox";
            this.sellerZIPTextBox.Size = new System.Drawing.Size(104, 30);
            this.sellerZIPTextBox.TabIndex = 14;
            this.sellerZIPTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.sellerZIPTextBox_KeyDown);
            this.sellerZIPTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.sellerZIPTextBox_KeyPress);
            // 
            // sellerPhoneTextBox
            // 
            this.sellerPhoneTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sellerPhoneTextBox.Location = new System.Drawing.Point(369, 281);
            this.sellerPhoneTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.sellerPhoneTextBox.Name = "sellerPhoneTextBox";
            this.sellerPhoneTextBox.Size = new System.Drawing.Size(506, 30);
            this.sellerPhoneTextBox.TabIndex = 16;
            this.sellerPhoneTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.sellerPhoneTextBox_KeyDown);
            this.sellerPhoneTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.sellerPhoneTextBox_KeyPress);
            // 
            // saveNewSellerBtn
            // 
            this.saveNewSellerBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveNewSellerBtn.Location = new System.Drawing.Point(763, 335);
            this.saveNewSellerBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.saveNewSellerBtn.Name = "saveNewSellerBtn";
            this.saveNewSellerBtn.Size = new System.Drawing.Size(112, 35);
            this.saveNewSellerBtn.TabIndex = 17;
            this.saveNewSellerBtn.Text = "Gem";
            this.saveNewSellerBtn.UseVisualStyleBackColor = true;
            this.saveNewSellerBtn.Click += new System.EventHandler(this.saveNewSellerBtn_Click);
            // 
            // newSellerGroupBox
            // 
            this.newSellerGroupBox.Controls.Add(this.sellerEmailLabel);
            this.newSellerGroupBox.Controls.Add(this.sellerEmailTextBox);
            this.newSellerGroupBox.Controls.Add(this.sellerNameTextBox);
            this.newSellerGroupBox.Controls.Add(this.sellerNameLabel);
            this.newSellerGroupBox.Controls.Add(this.sellerAddressLabel);
            this.newSellerGroupBox.Controls.Add(this.sellerZipLabel);
            this.newSellerGroupBox.Controls.Add(this.saveNewSellerBtn);
            this.newSellerGroupBox.Controls.Add(this.sellerPhoneLabel);
            this.newSellerGroupBox.Controls.Add(this.sellerPhoneTextBox);
            this.newSellerGroupBox.Controls.Add(this.sellerCityLabel);
            this.newSellerGroupBox.Controls.Add(this.sellerZIPTextBox);
            this.newSellerGroupBox.Controls.Add(this.sellerAddressTextBox);
            this.newSellerGroupBox.Controls.Add(this.sellerCityTextBox);
            this.newSellerGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newSellerGroupBox.Location = new System.Drawing.Point(446, 155);
            this.newSellerGroupBox.Name = "newSellerGroupBox";
            this.newSellerGroupBox.Size = new System.Drawing.Size(1098, 746);
            this.newSellerGroupBox.TabIndex = 18;
            this.newSellerGroupBox.TabStop = false;
            this.newSellerGroupBox.Text = "Ny Sælger";
            this.newSellerGroupBox.Visible = false;
            // 
            // sellerEmailLabel
            // 
            this.sellerEmailLabel.AutoSize = true;
            this.sellerEmailLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sellerEmailLabel.Location = new System.Drawing.Point(259, 159);
            this.sellerEmailLabel.Name = "sellerEmailLabel";
            this.sellerEmailLabel.Size = new System.Drawing.Size(73, 25);
            this.sellerEmailLabel.TabIndex = 17;
            this.sellerEmailLabel.Text = "E-mail:";
            // 
            // sellerEmailTextBox
            // 
            this.sellerEmailTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sellerEmailTextBox.Location = new System.Drawing.Point(370, 156);
            this.sellerEmailTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.sellerEmailTextBox.Name = "sellerEmailTextBox";
            this.sellerEmailTextBox.Size = new System.Drawing.Size(506, 30);
            this.sellerEmailTextBox.TabIndex = 12;
            this.sellerEmailTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.sellerEmailTextBox_KeyDown);
            // 
            // tabPage
            // 
            this.tabPage.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabPage.AllowDrop = true;
            this.tabPage.Controls.Add(this.sellerTab);
            this.tabPage.Controls.Add(this.tabPage2);
            this.tabPage.Controls.Add(this.tabPage3);
            this.tabPage.ImageList = this.tabIcons;
            this.tabPage.ItemSize = new System.Drawing.Size(120, 100);
            this.tabPage.Location = new System.Drawing.Point(0, 95);
            this.tabPage.Multiline = true;
            this.tabPage.Name = "tabPage";
            this.tabPage.SelectedIndex = 0;
            this.tabPage.Size = new System.Drawing.Size(1670, 952);
            this.tabPage.TabIndex = 19;
            // 
            // sellerTab
            // 
            this.sellerTab.Controls.Add(this.sellerSeparator);
            this.sellerTab.Controls.Add(this.sellerInformationGroupBox);
            this.sellerTab.Controls.Add(this.newSellerGroupBox);
            this.sellerTab.Controls.Add(this.sellerTabPage);
            this.sellerTab.Controls.Add(this.newSellerBtn);
            this.sellerTab.Controls.Add(this.userListSearchBox);
            this.sellerTab.Controls.Add(this.sellerListView);
            this.sellerTab.Cursor = System.Windows.Forms.Cursors.Default;
            this.sellerTab.ImageIndex = 0;
            this.sellerTab.Location = new System.Drawing.Point(104, 4);
            this.sellerTab.Name = "sellerTab";
            this.sellerTab.Padding = new System.Windows.Forms.Padding(3);
            this.sellerTab.Size = new System.Drawing.Size(1562, 944);
            this.sellerTab.TabIndex = 0;
            this.sellerTab.UseVisualStyleBackColor = true;
            // 
            // sellerSeparator
            // 
            this.sellerSeparator.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.sellerSeparator.Location = new System.Drawing.Point(53, 128);
            this.sellerSeparator.Name = "sellerSeparator";
            this.sellerSeparator.Size = new System.Drawing.Size(1487, 10);
            this.sellerSeparator.TabIndex = 1;
            // 
            // sellerInformationGroupBox
            // 
            this.sellerInformationGroupBox.Controls.Add(this.sellerInformation);
            this.sellerInformationGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sellerInformationGroupBox.Location = new System.Drawing.Point(443, 155);
            this.sellerInformationGroupBox.Name = "sellerInformationGroupBox";
            this.sellerInformationGroupBox.Size = new System.Drawing.Size(1095, 746);
            this.sellerInformationGroupBox.TabIndex = 17;
            this.sellerInformationGroupBox.TabStop = false;
            this.sellerInformationGroupBox.Text = "Sælger Informationer";
            this.sellerInformationGroupBox.Visible = false;
            // 
            // sellerInformation
            // 
            this.sellerInformation.BackColor = System.Drawing.Color.Honeydew;
            this.sellerInformation.Controls.Add(this.sellerTagInfoLabel);
            this.sellerInformation.Controls.Add(this.cancelSellerInfoBtn);
            this.sellerInformation.Controls.Add(this.saveSellerInfoBtn);
            this.sellerInformation.Controls.Add(this.sellerEmailInfoBox);
            this.sellerInformation.Controls.Add(this.sellerPhoneInfoBox);
            this.sellerInformation.Controls.Add(this.sellerZIPCityInfoBox);
            this.sellerInformation.Controls.Add(this.sellerAddressInfoBox);
            this.sellerInformation.Controls.Add(this.sellerNameInfoBox);
            this.sellerInformation.Controls.Add(this.sellerJoinDateInfoBox);
            this.sellerInformation.Controls.Add(this.editSellerInfoBtn);
            this.sellerInformation.Location = new System.Drawing.Point(27, 53);
            this.sellerInformation.Name = "sellerInformation";
            this.sellerInformation.Size = new System.Drawing.Size(1039, 288);
            this.sellerInformation.TabIndex = 0;
            this.sellerInformation.TabStop = false;
            // 
            // sellerTagInfoLabel
            // 
            this.sellerTagInfoLabel.AutoSize = true;
            this.sellerTagInfoLabel.Location = new System.Drawing.Point(33, 46);
            this.sellerTagInfoLabel.Name = "sellerTagInfoLabel";
            this.sellerTagInfoLabel.Size = new System.Drawing.Size(130, 46);
            this.sellerTagInfoLabel.TabIndex = 14;
            this.sellerTagInfoLabel.Text = "0000#";
            // 
            // cancelSellerInfoBtn
            // 
            this.cancelSellerInfoBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelSellerInfoBtn.Location = new System.Drawing.Point(693, 214);
            this.cancelSellerInfoBtn.Name = "cancelSellerInfoBtn";
            this.cancelSellerInfoBtn.Size = new System.Drawing.Size(125, 34);
            this.cancelSellerInfoBtn.TabIndex = 13;
            this.cancelSellerInfoBtn.Text = "Annuller";
            this.cancelSellerInfoBtn.UseVisualStyleBackColor = true;
            this.cancelSellerInfoBtn.Visible = false;
            this.cancelSellerInfoBtn.Click += new System.EventHandler(this.cancelSellerInfoBtn_Click);
            // 
            // saveSellerInfoBtn
            // 
            this.saveSellerInfoBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveSellerInfoBtn.Location = new System.Drawing.Point(524, 214);
            this.saveSellerInfoBtn.Name = "saveSellerInfoBtn";
            this.saveSellerInfoBtn.Size = new System.Drawing.Size(125, 34);
            this.saveSellerInfoBtn.TabIndex = 12;
            this.saveSellerInfoBtn.Text = "Gem";
            this.saveSellerInfoBtn.UseVisualStyleBackColor = true;
            this.saveSellerInfoBtn.Visible = false;
            this.saveSellerInfoBtn.Click += new System.EventHandler(this.saveSellerInfoBtn_Click);
            // 
            // sellerEmailInfoBox
            // 
            this.sellerEmailInfoBox.BackColor = System.Drawing.Color.Honeydew;
            this.sellerEmailInfoBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.sellerEmailInfoBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.sellerEmailInfoBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sellerEmailInfoBox.Location = new System.Drawing.Point(41, 229);
            this.sellerEmailInfoBox.Name = "sellerEmailInfoBox";
            this.sellerEmailInfoBox.ReadOnly = true;
            this.sellerEmailInfoBox.Size = new System.Drawing.Size(436, 28);
            this.sellerEmailInfoBox.TabIndex = 11;
            this.sellerEmailInfoBox.Text = "Default";
            this.sellerEmailInfoBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.sellerEmailInfoBox_KeyDown);
            // 
            // sellerPhoneInfoBox
            // 
            this.sellerPhoneInfoBox.BackColor = System.Drawing.Color.Honeydew;
            this.sellerPhoneInfoBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.sellerPhoneInfoBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.sellerPhoneInfoBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sellerPhoneInfoBox.Location = new System.Drawing.Point(41, 183);
            this.sellerPhoneInfoBox.Name = "sellerPhoneInfoBox";
            this.sellerPhoneInfoBox.ReadOnly = true;
            this.sellerPhoneInfoBox.Size = new System.Drawing.Size(436, 28);
            this.sellerPhoneInfoBox.TabIndex = 10;
            this.sellerPhoneInfoBox.Text = "Default";
            this.sellerPhoneInfoBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.sellerPhoneInfoBox_KeyDown);
            // 
            // sellerZIPCityInfoBox
            // 
            this.sellerZIPCityInfoBox.BackColor = System.Drawing.Color.Honeydew;
            this.sellerZIPCityInfoBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.sellerZIPCityInfoBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.sellerZIPCityInfoBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sellerZIPCityInfoBox.Location = new System.Drawing.Point(41, 143);
            this.sellerZIPCityInfoBox.Name = "sellerZIPCityInfoBox";
            this.sellerZIPCityInfoBox.ReadOnly = true;
            this.sellerZIPCityInfoBox.Size = new System.Drawing.Size(436, 28);
            this.sellerZIPCityInfoBox.TabIndex = 9;
            this.sellerZIPCityInfoBox.Text = "Default";
            this.sellerZIPCityInfoBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.sellerZIPCityInfoBox_KeyDown);
            // 
            // sellerAddressInfoBox
            // 
            this.sellerAddressInfoBox.BackColor = System.Drawing.Color.Honeydew;
            this.sellerAddressInfoBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.sellerAddressInfoBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.sellerAddressInfoBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sellerAddressInfoBox.Location = new System.Drawing.Point(41, 102);
            this.sellerAddressInfoBox.Name = "sellerAddressInfoBox";
            this.sellerAddressInfoBox.ReadOnly = true;
            this.sellerAddressInfoBox.Size = new System.Drawing.Size(436, 28);
            this.sellerAddressInfoBox.TabIndex = 8;
            this.sellerAddressInfoBox.Text = "Default";
            this.sellerAddressInfoBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.sellerAddressInfoBox_KeyDown);
            // 
            // sellerNameInfoBox
            // 
            this.sellerNameInfoBox.BackColor = System.Drawing.Color.Honeydew;
            this.sellerNameInfoBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.sellerNameInfoBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.sellerNameInfoBox.Location = new System.Drawing.Point(169, 46);
            this.sellerNameInfoBox.Name = "sellerNameInfoBox";
            this.sellerNameInfoBox.ReadOnly = true;
            this.sellerNameInfoBox.Size = new System.Drawing.Size(603, 46);
            this.sellerNameInfoBox.TabIndex = 7;
            this.sellerNameInfoBox.Text = "Test";
            this.sellerNameInfoBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.sellerNameInfoBox_KeyDown);
            // 
            // sellerJoinDateInfoBox
            // 
            this.sellerJoinDateInfoBox.AutoSize = true;
            this.sellerJoinDateInfoBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sellerJoinDateInfoBox.Location = new System.Drawing.Point(519, 128);
            this.sellerJoinDateInfoBox.Name = "sellerJoinDateInfoBox";
            this.sellerJoinDateInfoBox.Size = new System.Drawing.Size(257, 29);
            this.sellerJoinDateInfoBox.TabIndex = 6;
            this.sellerJoinDateInfoBox.Text = "Oprettelse: 12/12-2012";
            // 
            // editSellerInfoBtn
            // 
            this.editSellerInfoBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editSellerInfoBtn.Location = new System.Drawing.Point(524, 214);
            this.editSellerInfoBtn.Name = "editSellerInfoBtn";
            this.editSellerInfoBtn.Size = new System.Drawing.Size(125, 34);
            this.editSellerInfoBtn.TabIndex = 5;
            this.editSellerInfoBtn.Text = "Rediger Info";
            this.editSellerInfoBtn.UseVisualStyleBackColor = true;
            this.editSellerInfoBtn.Click += new System.EventHandler(this.editSellerInfoBtn_Click);
            // 
            // sellerTabPage
            // 
            this.sellerTabPage.AutoSize = true;
            this.sellerTabPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sellerTabPage.Location = new System.Drawing.Point(532, 37);
            this.sellerTabPage.Name = "sellerTabPage";
            this.sellerTabPage.Size = new System.Drawing.Size(463, 69);
            this.sellerTabPage.TabIndex = 19;
            this.sellerTabPage.Text = "Sælger Oversigt";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(104, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1562, 944);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(104, 4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1562, 944);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabIcons
            // 
            this.tabIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("tabIcons.ImageStream")));
            this.tabIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.tabIcons.Images.SetKeyName(0, "SellerIcon.png");
            // 
            // Refashion_Main_UI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1684, 1075);
            this.Controls.Add(this.tabPage);
            this.Name = "Refashion_Main_UI";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Refashion_Main_UI_Load);
            this.newSellerGroupBox.ResumeLayout(false);
            this.newSellerGroupBox.PerformLayout();
            this.tabPage.ResumeLayout(false);
            this.sellerTab.ResumeLayout(false);
            this.sellerTab.PerformLayout();
            this.sellerInformationGroupBox.ResumeLayout(false);
            this.sellerInformation.ResumeLayout(false);
            this.sellerInformation.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView sellerListView;
        private System.Windows.Forms.ComboBox userListSearchBox;
        private System.Windows.Forms.Button newSellerBtn;
        private System.Windows.Forms.Label sellerNameLabel;
        private System.Windows.Forms.Label sellerAddressLabel;
        private System.Windows.Forms.Label sellerZipLabel;
        private System.Windows.Forms.Label sellerPhoneLabel;
        private System.Windows.Forms.Label sellerCityLabel;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TextBox sellerNameTextBox;
        private System.Windows.Forms.TextBox sellerAddressTextBox;
        private System.Windows.Forms.TextBox sellerCityTextBox;
        private System.Windows.Forms.TextBox sellerZIPTextBox;
        private System.Windows.Forms.TextBox sellerPhoneTextBox;
        private System.Windows.Forms.Button saveNewSellerBtn;
        private System.Windows.Forms.ColumnHeader SellerTag;
        private System.Windows.Forms.ColumnHeader SellerName;
        private System.Windows.Forms.GroupBox newSellerGroupBox;
        private System.Windows.Forms.TabControl tabPage;
        private System.Windows.Forms.TabPage sellerTab;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ImageList tabIcons;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label sellerTabPage;
        private System.Windows.Forms.GroupBox sellerInformationGroupBox;
        private System.Windows.Forms.GroupBox sellerInformation;
        private System.Windows.Forms.Label sellerEmailLabel;
        private System.Windows.Forms.TextBox sellerEmailTextBox;
        private System.Windows.Forms.Label sellerJoinDateInfoBox;
        private System.Windows.Forms.Button editSellerInfoBtn;
        private System.Windows.Forms.Label sellerSeparator;
        private System.Windows.Forms.TextBox sellerEmailInfoBox;
        private System.Windows.Forms.TextBox sellerPhoneInfoBox;
        private System.Windows.Forms.TextBox sellerZIPCityInfoBox;
        private System.Windows.Forms.TextBox sellerAddressInfoBox;
        private System.Windows.Forms.TextBox sellerNameInfoBox;
        private System.Windows.Forms.Button cancelSellerInfoBtn;
        private System.Windows.Forms.Button saveSellerInfoBtn;
        private System.Windows.Forms.Label sellerTagInfoLabel;
    }
}

