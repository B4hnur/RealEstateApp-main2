namespace RealEstateApp
{
    partial class DetailsForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlMain = new System.Windows.Forms.Panel();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.pnlImages = new System.Windows.Forms.Panel();
            this.lblImageCounter = new System.Windows.Forms.Label();
            this.btnNextImage = new System.Windows.Forms.Button();
            this.btnPrevImage = new System.Windows.Forms.Button();
            this.pictureBoxMain = new System.Windows.Forms.PictureBox();
            this.pnlThumbnails = new System.Windows.Forms.Panel();
            this.pnlInfo = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pnlActions = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnFavorite = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabDetails = new System.Windows.Forms.TabPage();
            this.txtDetails = new System.Windows.Forms.TextBox();
            this.tabDescription = new System.Windows.Forms.TabPage();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.tabContact = new System.Windows.Forms.TabPage();
            this.txtContact = new System.Windows.Forms.TextBox();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblLocation = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();

            // Main panel
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(20, 60);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(960, 620);

            // Split container
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.splitContainer.Panel1MinSize = 300;
            this.splitContainer.Panel2MinSize = 200;
            this.splitContainer.Size = new System.Drawing.Size(960, 620);
            this.splitContainer.SplitterDistance = 350;

            // Panel for images (top panel)
            this.pnlImages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlImages.Location = new System.Drawing.Point(0, 0);
            this.pnlImages.Name = "pnlImages";
            this.pnlImages.Size = new System.Drawing.Size(960, 350);

            // Main image
            this.pictureBoxMain.Location = new System.Drawing.Point(50, 10);
            this.pictureBoxMain.Name = "pictureBoxMain";
            this.pictureBoxMain.Size = new System.Drawing.Size(860, 240);
            this.pictureBoxMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // Image counter label
            this.lblImageCounter.AutoSize = true;
            this.lblImageCounter.Location = new System.Drawing.Point(450, 260);
            this.lblImageCounter.Name = "lblImageCounter";
            this.lblImageCounter.Size = new System.Drawing.Size(60, 20);
            this.lblImageCounter.Text = "0 / 0";

            // Previous image button
            this.btnPrevImage.Location = new System.Drawing.Point(10, 100);
            this.btnPrevImage.Name = "btnPrevImage";
            this.btnPrevImage.Size = new System.Drawing.Size(30, 50);
            this.btnPrevImage.Text = "<";
            this.btnPrevImage.UseVisualStyleBackColor = true;
            this.btnPrevImage.Click += new System.EventHandler(this.btnPrevImage_Click);

            // Next image button
            this.btnNextImage.Location = new System.Drawing.Point(920, 100);
            this.btnNextImage.Name = "btnNextImage";
            this.btnNextImage.Size = new System.Drawing.Size(30, 50);
            this.btnNextImage.Text = ">";
            this.btnNextImage.UseVisualStyleBackColor = true;
            this.btnNextImage.Click += new System.EventHandler(this.btnNextImage_Click);

            // Thumbnails panel
            this.pnlThumbnails.Location = new System.Drawing.Point(10, 280);
            this.pnlThumbnails.Name = "pnlThumbnails";
            this.pnlThumbnails.Size = new System.Drawing.Size(940, 90);
            this.pnlThumbnails.AutoScroll = true;

            // Add controls to images panel
            this.pnlImages.Controls.Add(this.lblImageCounter);
            this.pnlImages.Controls.Add(this.btnNextImage);
            this.pnlImages.Controls.Add(this.btnPrevImage);
            this.pnlImages.Controls.Add(this.pictureBoxMain);
            this.pnlImages.Controls.Add(this.pnlThumbnails);

            // Info panel (bottom panel)
            this.pnlInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInfo.Location = new System.Drawing.Point(0, 0);
            this.pnlInfo.Name = "pnlInfo";
            this.pnlInfo.Size = new System.Drawing.Size(960, 266);

            // Status label
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(10, 234);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(150, 20);
            this.lblStatus.Text = "Yüklənir...";
            this.lblStatus.Visible = false;

            // Actions panel
            this.pnlActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlActions.Location = new System.Drawing.Point(0, 226);
            this.pnlActions.Name = "pnlActions";
            this.pnlActions.Size = new System.Drawing.Size(960, 40);

            // Close button
            this.btnClose.Location = new System.Drawing.Point(850, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 30);
            this.btnClose.Text = "Bağla";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            // Favorite button
            this.btnFavorite.Location = new System.Drawing.Point(660, 5);
            this.btnFavorite.Name = "btnFavorite";
            this.btnFavorite.Size = new System.Drawing.Size(180, 30);
            this.btnFavorite.Text = "Seçilmişlərə əlavə et";
            this.btnFavorite.UseVisualStyleBackColor = true;
            this.btnFavorite.Click += new System.EventHandler(this.btnFavorite_Click);

            // Add buttons to actions panel
            this.pnlActions.Controls.Add(this.btnClose);
            this.pnlActions.Controls.Add(this.btnFavorite);

            // Header panel
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(960, 70);

            // Title label
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(10, 5);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(200, 25);
            this.lblTitle.Text = "Elan başlığı";

            // Price label
            this.lblPrice.AutoSize = true;
            this.lblPrice.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblPrice.ForeColor = System.Drawing.Color.Green;
            this.lblPrice.Location = new System.Drawing.Point(10, 35);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(80, 25);
            this.lblPrice.Text = "Qiymət";

            // Location label
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(500, 35);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(80, 20);
            this.lblLocation.Text = "Yer";

            // Add labels to header panel
            this.pnlHeader.Controls.Add(this.lblLocation);
            this.pnlHeader.Controls.Add(this.lblPrice);
            this.pnlHeader.Controls.Add(this.lblTitle);

            // Tab control
            this.tabControl.Location = new System.Drawing.Point(0, 70);
            this.tabControl.Name = "tabControl";
            this.tabControl.Size = new System.Drawing.Size(960, 156);
            this.tabControl.TabIndex = 0;

            // Details tab
            this.tabDetails.Location = new System.Drawing.Point(4, 22);
            this.tabDetails.Name = "tabDetails";
            this.tabDetails.Padding = new System.Windows.Forms.Padding(3);
            this.tabDetails.Size = new System.Drawing.Size(952, 130);
            this.tabDetails.TabIndex = 0;
            this.tabDetails.Text = "Ətraflı məlumat";
            this.tabDetails.UseVisualStyleBackColor = true;

            // Details text box
            this.txtDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDetails.Multiline = true;
            this.txtDetails.ReadOnly = true;
            this.txtDetails.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDetails.Name = "txtDetails";
            this.txtDetails.Size = new System.Drawing.Size(946, 124);

            // Add details textbox to details tab
            this.tabDetails.Controls.Add(this.txtDetails);

            // Description tab
            this.tabDescription.Location = new System.Drawing.Point(4, 22);
            this.tabDescription.Name = "tabDescription";
            this.tabDescription.Padding = new System.Windows.Forms.Padding(3);
            this.tabDescription.Size = new System.Drawing.Size(952, 130);
            this.tabDescription.TabIndex = 1;
            this.tabDescription.Text = "Təsvir";
            this.tabDescription.UseVisualStyleBackColor = true;

            // Description text box
            this.txtDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDescription.Multiline = true;
            this.txtDescription.ReadOnly = true;
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(946, 124);

            // Add description textbox to description tab
            this.tabDescription.Controls.Add(this.txtDescription);

            // Contact tab
            this.tabContact.Location = new System.Drawing.Point(4, 22);
            this.tabContact.Name = "tabContact";
            this.tabContact.Padding = new System.Windows.Forms.Padding(3);
            this.tabContact.Size = new System.Drawing.Size(952, 130);
            this.tabContact.TabIndex = 2;
            this.tabContact.Text = "Əlaqə";
            this.tabContact.UseVisualStyleBackColor = true;

            // Contact text box
            this.txtContact.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtContact.Multiline = true;
            this.txtContact.ReadOnly = true;
            this.txtContact.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtContact.Name = "txtContact";
            this.txtContact.Size = new System.Drawing.Size(946, 124);

            // Add contact textbox to contact tab
            this.tabContact.Controls.Add(this.txtContact);

            // Add tabs to tab control
            this.tabControl.Controls.Add(this.tabDetails);
            this.tabControl.Controls.Add(this.tabDescription);
            this.tabControl.Controls.Add(this.tabContact);

            // Add controls to info panel
            this.pnlInfo.Controls.Add(this.lblStatus);
            this.pnlInfo.Controls.Add(this.pnlActions);
            this.pnlInfo.Controls.Add(this.tabControl);
            this.pnlInfo.Controls.Add(this.pnlHeader);

            // Add panels to split container
            this.splitContainer.Panel1.Controls.Add(this.pnlImages);
            this.splitContainer.Panel2.Controls.Add(this.pnlInfo);

            // Add split container to main panel
            this.pnlMain.Controls.Add(this.splitContainer);

            // Add main panel to form
            this.Controls.Add(this.pnlMain);

            // Set up form properties
            this.Name = "DetailsForm";
            this.Text = "Elan detalları";
            this.Style = MetroFramework.MetroColorStyle.Green; // Ensure MetroFramework is referenced correctly
            this.Size = new System.Drawing.Size(1000, 700);
            this.Load += new System.EventHandler(this.DetailsForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DetailsForm_FormClosing);
        }



        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Panel pnlImages;
        private System.Windows.Forms.Label lblImageCounter;
        private System.Windows.Forms.Button btnNextImage;
        private System.Windows.Forms.Button btnPrevImage;
        private System.Windows.Forms.PictureBox pictureBoxMain;
        private System.Windows.Forms.Panel pnlThumbnails;
        private System.Windows.Forms.Panel pnlInfo;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel pnlActions;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnFavorite;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabDetails;
        private System.Windows.Forms.TextBox txtDetails;
        private System.Windows.Forms.TabPage tabDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TabPage tabContact;
        private System.Windows.Forms.TextBox txtContact;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label lblTitle;
    }
}


