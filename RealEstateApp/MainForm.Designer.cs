namespace RealEstateApp
{
    partial class MainForm
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
            this.pnlMain = new System.Windows.Forms.Panel();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.pnlFilters = new System.Windows.Forms.Panel();
            this.pnlFilterButtons = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.pnlKeyword = new System.Windows.Forms.Panel();
            this.lblKeyword = new System.Windows.Forms.Label();
            this.txtKeyword = new System.Windows.Forms.TextBox();
            this.pnlFloor = new System.Windows.Forms.Panel();
            this.lblFloor = new System.Windows.Forms.Label();
            this.lblFloorRange = new System.Windows.Forms.Label();
            this.nudMinFloor = new System.Windows.Forms.NumericUpDown();
            this.nudMaxFloor = new System.Windows.Forms.NumericUpDown();
            this.pnlArea = new System.Windows.Forms.Panel();
            this.lblArea = new System.Windows.Forms.Label();
            this.lblAreaRange = new System.Windows.Forms.Label();
            this.nudMinArea = new System.Windows.Forms.NumericUpDown();
            this.nudMaxArea = new System.Windows.Forms.NumericUpDown();
            this.pnlPrice = new System.Windows.Forms.Panel();
            this.lblPrice = new System.Windows.Forms.Label();
            this.lblPriceRange = new System.Windows.Forms.Label();
            this.nudMinPrice = new System.Windows.Forms.NumericUpDown();
            this.nudMaxPrice = new System.Windows.Forms.NumericUpDown();
            this.pnlRooms = new System.Windows.Forms.Panel();
            this.lblRooms = new System.Windows.Forms.Label();
            this.lblRoomsRange = new System.Windows.Forms.Label();
            this.nudMinRooms = new System.Windows.Forms.NumericUpDown();
            this.nudMaxRooms = new System.Windows.Forms.NumericUpDown();
            this.pnlLocation = new System.Windows.Forms.Panel();
            this.lblLocation = new System.Windows.Forms.Label();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.pnlOwnerType = new System.Windows.Forms.Panel();
            this.lblOwnerType = new System.Windows.Forms.Label();
            this.cmbOwnerType = new System.Windows.Forms.ComboBox();
            this.pnlPurpose = new System.Windows.Forms.Panel();
            this.lblPurpose = new System.Windows.Forms.Label();
            this.cmbPurpose = new System.Windows.Forms.ComboBox();
            this.pnlBuildingType = new System.Windows.Forms.Panel();
            this.lblBuildingType = new System.Windows.Forms.Label();
            this.cmbBuildingType = new System.Windows.Forms.ComboBox();
            this.pnlEntityType = new System.Windows.Forms.Panel();
            this.lblEntityType = new System.Windows.Forms.Label();
            this.cmbEntityType = new System.Windows.Forms.ComboBox();
            this.pnlResults = new System.Windows.Forms.Panel();
            this.pnlListings = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlPagination = new System.Windows.Forms.Panel();
            this.lblPageInfo = new System.Windows.Forms.Label();
            this.btnNextPage = new System.Windows.Forms.Button();
            this.btnPrevPage = new System.Windows.Forms.Button();
            this.pnlResultsHeader = new System.Windows.Forms.Panel();
            this.lblResultsInfo = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.btnFavorites = new System.Windows.Forms.Button();
            this.btnListView = new System.Windows.Forms.Button();
            this.btnGridView = new System.Windows.Forms.Button();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.pnlFilters.SuspendLayout();
            this.pnlFilterButtons.SuspendLayout();
            this.pnlKeyword.SuspendLayout();
            this.pnlFloor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinFloor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxFloor)).BeginInit();
            this.pnlArea.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinArea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxArea)).BeginInit();
            this.pnlPrice.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxPrice)).BeginInit();
            this.pnlRooms.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinRooms)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxRooms)).BeginInit();
            this.pnlLocation.SuspendLayout();
            this.pnlOwnerType.SuspendLayout();
            this.pnlPurpose.SuspendLayout();
            this.pnlBuildingType.SuspendLayout();
            this.pnlEntityType.SuspendLayout();
            this.pnlResults.SuspendLayout();
            this.pnlPagination.SuspendLayout();
            this.pnlResultsHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.splitContainer);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(20, 60);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1160, 620);
            this.pnlMain.TabIndex = 0;
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.pnlFilters);
            this.splitContainer.Panel1MinSize = 250;
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.pnlResults);
            this.splitContainer.Panel2MinSize = 500;
            this.splitContainer.Size = new System.Drawing.Size(1160, 620);
            this.splitContainer.SplitterDistance = 309;
            this.splitContainer.TabIndex = 0;
            // 
            // pnlFilters
            // 
            this.pnlFilters.AutoScroll = true;
            this.pnlFilters.Controls.Add(this.pnlFilterButtons);
            this.pnlFilters.Controls.Add(this.pnlKeyword);
            this.pnlFilters.Controls.Add(this.pnlFloor);
            this.pnlFilters.Controls.Add(this.pnlArea);
            this.pnlFilters.Controls.Add(this.pnlPrice);
            this.pnlFilters.Controls.Add(this.pnlRooms);
            this.pnlFilters.Controls.Add(this.pnlLocation);
            this.pnlFilters.Controls.Add(this.pnlOwnerType);
            this.pnlFilters.Controls.Add(this.pnlPurpose);
            this.pnlFilters.Controls.Add(this.pnlBuildingType);
            this.pnlFilters.Controls.Add(this.pnlEntityType);
            this.pnlFilters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFilters.Location = new System.Drawing.Point(0, 0);
            this.pnlFilters.Name = "pnlFilters";
            this.pnlFilters.Padding = new System.Windows.Forms.Padding(5);
            this.pnlFilters.Size = new System.Drawing.Size(309, 620);
            this.pnlFilters.TabIndex = 0;
            // 
            // pnlFilterButtons
            // 
            this.pnlFilterButtons.Controls.Add(this.btnSearch);
            this.pnlFilterButtons.Controls.Add(this.btnReset);
            this.pnlFilterButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilterButtons.Location = new System.Drawing.Point(5, 505);
            this.pnlFilterButtons.Name = "pnlFilterButtons";
            this.pnlFilterButtons.Size = new System.Drawing.Size(299, 50);
            this.pnlFilterButtons.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(170, 10);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(120, 30);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "Axtar";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(0, 10);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(120, 30);
            this.btnReset.TabIndex = 1;
            this.btnReset.Text = "Sıfırla";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // pnlKeyword
            // 
            this.pnlKeyword.Controls.Add(this.lblKeyword);
            this.pnlKeyword.Controls.Add(this.txtKeyword);
            this.pnlKeyword.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlKeyword.Location = new System.Drawing.Point(5, 455);
            this.pnlKeyword.Name = "pnlKeyword";
            this.pnlKeyword.Size = new System.Drawing.Size(299, 50);
            this.pnlKeyword.TabIndex = 1;
            // 
            // lblKeyword
            // 
            this.lblKeyword.AutoSize = true;
            this.lblKeyword.Location = new System.Drawing.Point(3, 0);
            this.lblKeyword.Name = "lblKeyword";
            this.lblKeyword.Size = new System.Drawing.Size(48, 13);
            this.lblKeyword.TabIndex = 0;
            this.lblKeyword.Text = "Açar söz";
            // 
            // txtKeyword
            // 
            this.txtKeyword.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtKeyword.Location = new System.Drawing.Point(0, 30);
            this.txtKeyword.Name = "txtKeyword";
            this.txtKeyword.Size = new System.Drawing.Size(299, 20);
            this.txtKeyword.TabIndex = 1;
            // 
            // pnlFloor
            // 
            this.pnlFloor.Controls.Add(this.lblFloor);
            this.pnlFloor.Controls.Add(this.lblFloorRange);
            this.pnlFloor.Controls.Add(this.nudMinFloor);
            this.pnlFloor.Controls.Add(this.nudMaxFloor);
            this.pnlFloor.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFloor.Location = new System.Drawing.Point(5, 405);
            this.pnlFloor.Name = "pnlFloor";
            this.pnlFloor.Size = new System.Drawing.Size(299, 50);
            this.pnlFloor.TabIndex = 2;
            // 
            // lblFloor
            // 
            this.lblFloor.AutoSize = true;
            this.lblFloor.Location = new System.Drawing.Point(3, 0);
            this.lblFloor.Name = "lblFloor";
            this.lblFloor.Size = new System.Drawing.Size(46, 13);
            this.lblFloor.TabIndex = 0;
            this.lblFloor.Text = "Mərtəbə";
            // 
            // lblFloorRange
            // 
            this.lblFloorRange.AutoSize = true;
            this.lblFloorRange.Location = new System.Drawing.Point(145, 25);
            this.lblFloorRange.Name = "lblFloorRange";
            this.lblFloorRange.Size = new System.Drawing.Size(10, 13);
            this.lblFloorRange.TabIndex = 1;
            this.lblFloorRange.Text = "-";
            // 
            // nudMinFloor
            // 
            this.nudMinFloor.Location = new System.Drawing.Point(3, 25);
            this.nudMinFloor.Name = "nudMinFloor";
            this.nudMinFloor.Size = new System.Drawing.Size(120, 20);
            this.nudMinFloor.TabIndex = 2;
            // 
            // nudMaxFloor
            // 
            this.nudMaxFloor.Location = new System.Drawing.Point(167, 25);
            this.nudMaxFloor.Name = "nudMaxFloor";
            this.nudMaxFloor.Size = new System.Drawing.Size(120, 20);
            this.nudMaxFloor.TabIndex = 3;
            // 
            // pnlArea
            // 
            this.pnlArea.Controls.Add(this.lblArea);
            this.pnlArea.Controls.Add(this.lblAreaRange);
            this.pnlArea.Controls.Add(this.nudMinArea);
            this.pnlArea.Controls.Add(this.nudMaxArea);
            this.pnlArea.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlArea.Location = new System.Drawing.Point(5, 355);
            this.pnlArea.Name = "pnlArea";
            this.pnlArea.Size = new System.Drawing.Size(299, 50);
            this.pnlArea.TabIndex = 3;
            // 
            // lblArea
            // 
            this.lblArea.AutoSize = true;
            this.lblArea.Location = new System.Drawing.Point(3, 0);
            this.lblArea.Name = "lblArea";
            this.lblArea.Size = new System.Drawing.Size(52, 13);
            this.lblArea.TabIndex = 0;
            this.lblArea.Text = "Sahə (m²)";
            // 
            // lblAreaRange
            // 
            this.lblAreaRange.AutoSize = true;
            this.lblAreaRange.Location = new System.Drawing.Point(145, 25);
            this.lblAreaRange.Name = "lblAreaRange";
            this.lblAreaRange.Size = new System.Drawing.Size(10, 13);
            this.lblAreaRange.TabIndex = 1;
            this.lblAreaRange.Text = "-";
            // 
            // nudMinArea
            // 
            this.nudMinArea.Location = new System.Drawing.Point(3, 25);
            this.nudMinArea.Name = "nudMinArea";
            this.nudMinArea.Size = new System.Drawing.Size(120, 20);
            this.nudMinArea.TabIndex = 2;
            // 
            // nudMaxArea
            // 
            this.nudMaxArea.Location = new System.Drawing.Point(167, 25);
            this.nudMaxArea.Name = "nudMaxArea";
            this.nudMaxArea.Size = new System.Drawing.Size(120, 20);
            this.nudMaxArea.TabIndex = 3;
            // 
            // pnlPrice
            // 
            this.pnlPrice.Controls.Add(this.lblPrice);
            this.pnlPrice.Controls.Add(this.lblPriceRange);
            this.pnlPrice.Controls.Add(this.nudMinPrice);
            this.pnlPrice.Controls.Add(this.nudMaxPrice);
            this.pnlPrice.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPrice.Location = new System.Drawing.Point(5, 305);
            this.pnlPrice.Name = "pnlPrice";
            this.pnlPrice.Size = new System.Drawing.Size(299, 50);
            this.pnlPrice.TabIndex = 4;
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Location = new System.Drawing.Point(3, 0);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(70, 13);
            this.lblPrice.TabIndex = 0;
            this.lblPrice.Text = "Qiymət (AZN)";
            // 
            // lblPriceRange
            // 
            this.lblPriceRange.AutoSize = true;
            this.lblPriceRange.Location = new System.Drawing.Point(145, 25);
            this.lblPriceRange.Name = "lblPriceRange";
            this.lblPriceRange.Size = new System.Drawing.Size(10, 13);
            this.lblPriceRange.TabIndex = 1;
            this.lblPriceRange.Text = "-";
            // 
            // nudMinPrice
            // 
            this.nudMinPrice.Location = new System.Drawing.Point(3, 25);
            this.nudMinPrice.Name = "nudMinPrice";
            this.nudMinPrice.Size = new System.Drawing.Size(120, 20);
            this.nudMinPrice.TabIndex = 2;
            // 
            // nudMaxPrice
            // 
            this.nudMaxPrice.Location = new System.Drawing.Point(167, 25);
            this.nudMaxPrice.Name = "nudMaxPrice";
            this.nudMaxPrice.Size = new System.Drawing.Size(120, 20);
            this.nudMaxPrice.TabIndex = 3;
            // 
            // pnlRooms
            // 
            this.pnlRooms.Controls.Add(this.lblRooms);
            this.pnlRooms.Controls.Add(this.lblRoomsRange);
            this.pnlRooms.Controls.Add(this.nudMinRooms);
            this.pnlRooms.Controls.Add(this.nudMaxRooms);
            this.pnlRooms.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRooms.Location = new System.Drawing.Point(5, 255);
            this.pnlRooms.Name = "pnlRooms";
            this.pnlRooms.Size = new System.Drawing.Size(299, 50);
            this.pnlRooms.TabIndex = 5;
            // 
            // lblRooms
            // 
            this.lblRooms.AutoSize = true;
            this.lblRooms.Location = new System.Drawing.Point(3, 0);
            this.lblRooms.Name = "lblRooms";
            this.lblRooms.Size = new System.Drawing.Size(51, 13);
            this.lblRooms.TabIndex = 0;
            this.lblRooms.Text = "Otaq sayı";
            // 
            // lblRoomsRange
            // 
            this.lblRoomsRange.AutoSize = true;
            this.lblRoomsRange.Location = new System.Drawing.Point(145, 25);
            this.lblRoomsRange.Name = "lblRoomsRange";
            this.lblRoomsRange.Size = new System.Drawing.Size(10, 13);
            this.lblRoomsRange.TabIndex = 1;
            this.lblRoomsRange.Text = "-";
            // 
            // nudMinRooms
            // 
            this.nudMinRooms.Location = new System.Drawing.Point(3, 25);
            this.nudMinRooms.Name = "nudMinRooms";
            this.nudMinRooms.Size = new System.Drawing.Size(120, 20);
            this.nudMinRooms.TabIndex = 2;
            // 
            // nudMaxRooms
            // 
            this.nudMaxRooms.Location = new System.Drawing.Point(167, 25);
            this.nudMaxRooms.Name = "nudMaxRooms";
            this.nudMaxRooms.Size = new System.Drawing.Size(120, 20);
            this.nudMaxRooms.TabIndex = 3;
            // 
            // pnlLocation
            // 
            this.pnlLocation.Controls.Add(this.lblLocation);
            this.pnlLocation.Controls.Add(this.txtLocation);
            this.pnlLocation.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLocation.Location = new System.Drawing.Point(5, 205);
            this.pnlLocation.Name = "pnlLocation";
            this.pnlLocation.Size = new System.Drawing.Size(299, 50);
            this.pnlLocation.TabIndex = 6;
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(3, 0);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(23, 13);
            this.lblLocation.TabIndex = 0;
            this.lblLocation.Text = "Yer";
            // 
            // txtLocation
            // 
            this.txtLocation.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtLocation.Location = new System.Drawing.Point(0, 30);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(299, 20);
            this.txtLocation.TabIndex = 1;
            // 
            // pnlOwnerType
            // 
            this.pnlOwnerType.Controls.Add(this.lblOwnerType);
            this.pnlOwnerType.Controls.Add(this.cmbOwnerType);
            this.pnlOwnerType.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlOwnerType.Location = new System.Drawing.Point(5, 155);
            this.pnlOwnerType.Name = "pnlOwnerType";
            this.pnlOwnerType.Size = new System.Drawing.Size(299, 50);
            this.pnlOwnerType.TabIndex = 7;
            // 
            // lblOwnerType
            // 
            this.lblOwnerType.AutoSize = true;
            this.lblOwnerType.Location = new System.Drawing.Point(3, 0);
            this.lblOwnerType.Name = "lblOwnerType";
            this.lblOwnerType.Size = new System.Drawing.Size(63, 13);
            this.lblOwnerType.TabIndex = 0;
            this.lblOwnerType.Text = "Satıcının tipi";
            // 
            // cmbOwnerType
            // 
            this.cmbOwnerType.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cmbOwnerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOwnerType.Location = new System.Drawing.Point(0, 29);
            this.cmbOwnerType.Name = "cmbOwnerType";
            this.cmbOwnerType.Size = new System.Drawing.Size(299, 21);
            this.cmbOwnerType.TabIndex = 1;
            // 
            // pnlPurpose
            // 
            this.pnlPurpose.Controls.Add(this.lblPurpose);
            this.pnlPurpose.Controls.Add(this.cmbPurpose);
            this.pnlPurpose.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPurpose.Location = new System.Drawing.Point(5, 105);
            this.pnlPurpose.Name = "pnlPurpose";
            this.pnlPurpose.Size = new System.Drawing.Size(299, 50);
            this.pnlPurpose.TabIndex = 8;
            this.pnlPurpose.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlPurpose_Paint);
            // 
            // lblPurpose
            // 
            this.lblPurpose.AutoSize = true;
            this.lblPurpose.Location = new System.Drawing.Point(3, 0);
            this.lblPurpose.Name = "lblPurpose";
            this.lblPurpose.Size = new System.Drawing.Size(63, 13);
            this.lblPurpose.TabIndex = 0;
            this.lblPurpose.Text = "Elanın növü";
            // 
            // cmbPurpose
            // 
            this.cmbPurpose.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cmbPurpose.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPurpose.Location = new System.Drawing.Point(0, 29);
            this.cmbPurpose.Name = "cmbPurpose";
            this.cmbPurpose.Size = new System.Drawing.Size(299, 21);
            this.cmbPurpose.TabIndex = 1;
            // 
            // pnlBuildingType
            // 
            this.pnlBuildingType.Controls.Add(this.lblBuildingType);
            this.pnlBuildingType.Controls.Add(this.cmbBuildingType);
            this.pnlBuildingType.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBuildingType.Location = new System.Drawing.Point(5, 55);
            this.pnlBuildingType.Name = "pnlBuildingType";
            this.pnlBuildingType.Size = new System.Drawing.Size(299, 50);
            this.pnlBuildingType.TabIndex = 9;
            // 
            // lblBuildingType
            // 
            this.lblBuildingType.AutoSize = true;
            this.lblBuildingType.Location = new System.Drawing.Point(3, 0);
            this.lblBuildingType.Name = "lblBuildingType";
            this.lblBuildingType.Size = new System.Drawing.Size(69, 13);
            this.lblBuildingType.TabIndex = 0;
            this.lblBuildingType.Text = "Binanın növü";
            // 
            // cmbBuildingType
            // 
            this.cmbBuildingType.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cmbBuildingType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBuildingType.Location = new System.Drawing.Point(0, 29);
            this.cmbBuildingType.Name = "cmbBuildingType";
            this.cmbBuildingType.Size = new System.Drawing.Size(299, 21);
            this.cmbBuildingType.TabIndex = 1;
            // 
            // pnlEntityType
            // 
            this.pnlEntityType.Controls.Add(this.lblEntityType);
            this.pnlEntityType.Controls.Add(this.cmbEntityType);
            this.pnlEntityType.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEntityType.Location = new System.Drawing.Point(5, 5);
            this.pnlEntityType.Name = "pnlEntityType";
            this.pnlEntityType.Size = new System.Drawing.Size(299, 50);
            this.pnlEntityType.TabIndex = 10;
            // 
            // lblEntityType
            // 
            this.lblEntityType.AutoSize = true;
            this.lblEntityType.Location = new System.Drawing.Point(3, 0);
            this.lblEntityType.Name = "lblEntityType";
            this.lblEntityType.Size = new System.Drawing.Size(76, 13);
            this.lblEntityType.TabIndex = 0;
            this.lblEntityType.Text = "Obyektin növü";
            // 
            // cmbEntityType
            // 
            this.cmbEntityType.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cmbEntityType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEntityType.Location = new System.Drawing.Point(0, 29);
            this.cmbEntityType.Name = "cmbEntityType";
            this.cmbEntityType.Size = new System.Drawing.Size(299, 21);
            this.cmbEntityType.TabIndex = 1;
            // 
            // pnlResults
            // 
            this.pnlResults.Controls.Add(this.pnlListings);
            this.pnlResults.Controls.Add(this.pnlPagination);
            this.pnlResults.Controls.Add(this.pnlResultsHeader);
            this.pnlResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlResults.Location = new System.Drawing.Point(0, 0);
            this.pnlResults.Name = "pnlResults";
            this.pnlResults.Size = new System.Drawing.Size(847, 620);
            this.pnlResults.TabIndex = 0;
            // 
            // pnlListings
            // 
            this.pnlListings.AutoScroll = true;
            this.pnlListings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlListings.Location = new System.Drawing.Point(0, 50);
            this.pnlListings.Name = "pnlListings";
            this.pnlListings.Size = new System.Drawing.Size(847, 520);
            this.pnlListings.TabIndex = 0;
            this.pnlListings.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlListings_Paint);
            // 
            // pnlPagination
            // 
            this.pnlPagination.Controls.Add(this.lblPageInfo);
            this.pnlPagination.Controls.Add(this.btnNextPage);
            this.pnlPagination.Controls.Add(this.btnPrevPage);
            this.pnlPagination.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPagination.Location = new System.Drawing.Point(0, 570);
            this.pnlPagination.Name = "pnlPagination";
            this.pnlPagination.Size = new System.Drawing.Size(847, 50);
            this.pnlPagination.TabIndex = 1;
            // 
            // lblPageInfo
            // 
            this.lblPageInfo.AutoSize = true;
            this.lblPageInfo.Location = new System.Drawing.Point(380, 15);
            this.lblPageInfo.Name = "lblPageInfo";
            this.lblPageInfo.Size = new System.Drawing.Size(46, 13);
            this.lblPageInfo.TabIndex = 0;
            this.lblPageInfo.Text = "Səhifə 1";
            // 
            // btnNextPage
            // 
            this.btnNextPage.Location = new System.Drawing.Point(450, 10);
            this.btnNextPage.Name = "btnNextPage";
            this.btnNextPage.Size = new System.Drawing.Size(100, 30);
            this.btnNextPage.TabIndex = 1;
            this.btnNextPage.Text = "Növbəti >";
            this.btnNextPage.UseVisualStyleBackColor = true;
            this.btnNextPage.Click += new System.EventHandler(this.btnNextPage_Click);
            // 
            // btnPrevPage
            // 
            this.btnPrevPage.Location = new System.Drawing.Point(270, 10);
            this.btnPrevPage.Name = "btnPrevPage";
            this.btnPrevPage.Size = new System.Drawing.Size(100, 30);
            this.btnPrevPage.TabIndex = 2;
            this.btnPrevPage.Text = "< Əvvəlki";
            this.btnPrevPage.UseVisualStyleBackColor = true;
            this.btnPrevPage.Click += new System.EventHandler(this.btnPrevPage_Click);
            // 
            // pnlResultsHeader
            // 
            this.pnlResultsHeader.Controls.Add(this.lblResultsInfo);
            this.pnlResultsHeader.Controls.Add(this.progressBar);
            this.pnlResultsHeader.Controls.Add(this.btnFavorites);
            this.pnlResultsHeader.Controls.Add(this.btnListView);
            this.pnlResultsHeader.Controls.Add(this.btnGridView);
            this.pnlResultsHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlResultsHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlResultsHeader.Name = "pnlResultsHeader";
            this.pnlResultsHeader.Size = new System.Drawing.Size(847, 50);
            this.pnlResultsHeader.TabIndex = 2;
            // 
            // lblResultsInfo
            // 
            this.lblResultsInfo.AutoSize = true;
            this.lblResultsInfo.Location = new System.Drawing.Point(10, 15);
            this.lblResultsInfo.Name = "lblResultsInfo";
            this.lblResultsInfo.Size = new System.Drawing.Size(49, 13);
            this.lblResultsInfo.TabIndex = 0;
            this.lblResultsInfo.Text = "Nəticələr";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(165, 15);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(100, 20);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar.TabIndex = 1;
            this.progressBar.Visible = false;
            // 
            // btnFavorites
            // 
            this.btnFavorites.Location = new System.Drawing.Point(520, 10);
            this.btnFavorites.Name = "btnFavorites";
            this.btnFavorites.Size = new System.Drawing.Size(100, 30);
            this.btnFavorites.TabIndex = 2;
            this.btnFavorites.Text = "Seçilmişlər";
            this.btnFavorites.UseVisualStyleBackColor = true;
            this.btnFavorites.Click += new System.EventHandler(this.btnFavorites_Click);
            // 
            // btnListView
            // 
            this.btnListView.Location = new System.Drawing.Point(630, 10);
            this.btnListView.Name = "btnListView";
            this.btnListView.Size = new System.Drawing.Size(90, 30);
            this.btnListView.TabIndex = 3;
            this.btnListView.Text = "Siyahı";
            this.btnListView.UseVisualStyleBackColor = true;
            this.btnListView.Click += new System.EventHandler(this.btnListView_Click);
            // 
            // btnGridView
            // 
            this.btnGridView.Location = new System.Drawing.Point(730, 10);
            this.btnGridView.Name = "btnGridView";
            this.btnGridView.Size = new System.Drawing.Size(90, 30);
            this.btnGridView.TabIndex = 4;
            this.btnGridView.Text = "Şəbəkə";
            this.btnGridView.UseVisualStyleBackColor = true;
            this.btnGridView.Click += new System.EventHandler(this.btnGridView_Click);
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.pnlMain);
            this.Name = "MainForm";
            this.Style = MetroFramework.MetroColorStyle.Green;
            this.Text = "Daşınmaz Əmlak Elanları";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.pnlMain.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.pnlFilters.ResumeLayout(false);
            this.pnlFilterButtons.ResumeLayout(false);
            this.pnlKeyword.ResumeLayout(false);
            this.pnlKeyword.PerformLayout();
            this.pnlFloor.ResumeLayout(false);
            this.pnlFloor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinFloor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxFloor)).EndInit();
            this.pnlArea.ResumeLayout(false);
            this.pnlArea.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinArea)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxArea)).EndInit();
            this.pnlPrice.ResumeLayout(false);
            this.pnlPrice.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxPrice)).EndInit();
            this.pnlRooms.ResumeLayout(false);
            this.pnlRooms.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinRooms)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxRooms)).EndInit();
            this.pnlLocation.ResumeLayout(false);
            this.pnlLocation.PerformLayout();
            this.pnlOwnerType.ResumeLayout(false);
            this.pnlOwnerType.PerformLayout();
            this.pnlPurpose.ResumeLayout(false);
            this.pnlPurpose.PerformLayout();
            this.pnlBuildingType.ResumeLayout(false);
            this.pnlBuildingType.PerformLayout();
            this.pnlEntityType.ResumeLayout(false);
            this.pnlEntityType.PerformLayout();
            this.pnlResults.ResumeLayout(false);
            this.pnlPagination.ResumeLayout(false);
            this.pnlPagination.PerformLayout();
            this.pnlResultsHeader.ResumeLayout(false);
            this.pnlResultsHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Panel pnlFilters;
        private System.Windows.Forms.Panel pnlEntityType;
        private System.Windows.Forms.Label lblEntityType;
        private System.Windows.Forms.ComboBox cmbEntityType;
        private System.Windows.Forms.Panel pnlBuildingType;
        private System.Windows.Forms.Label lblBuildingType;
        private System.Windows.Forms.ComboBox cmbBuildingType;
        private System.Windows.Forms.Panel pnlPurpose;
        private System.Windows.Forms.Label lblPurpose;
        private System.Windows.Forms.ComboBox cmbPurpose;
        private System.Windows.Forms.Panel pnlOwnerType;
        private System.Windows.Forms.Label lblOwnerType;
        private System.Windows.Forms.ComboBox cmbOwnerType;
        private System.Windows.Forms.Panel pnlLocation;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.Panel pnlRooms;
        private System.Windows.Forms.Label lblRooms;
        private System.Windows.Forms.Label lblRoomsRange;
        private System.Windows.Forms.NumericUpDown nudMaxRooms;
        private System.Windows.Forms.NumericUpDown nudMinRooms;
        private System.Windows.Forms.Panel pnlPrice;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label lblPriceRange;
        private System.Windows.Forms.NumericUpDown nudMaxPrice;
        private System.Windows.Forms.NumericUpDown nudMinPrice;
        private System.Windows.Forms.Panel pnlArea;
        private System.Windows.Forms.Label lblArea;
        private System.Windows.Forms.Label lblAreaRange;
        private System.Windows.Forms.NumericUpDown nudMaxArea;
        private System.Windows.Forms.NumericUpDown nudMinArea;
        private System.Windows.Forms.Panel pnlFloor;
        private System.Windows.Forms.Label lblFloor;
        private System.Windows.Forms.Label lblFloorRange;
        private System.Windows.Forms.NumericUpDown nudMaxFloor;
        private System.Windows.Forms.NumericUpDown nudMinFloor;
        private System.Windows.Forms.Panel pnlKeyword;
        private System.Windows.Forms.Label lblKeyword;
        private System.Windows.Forms.TextBox txtKeyword;
        private System.Windows.Forms.Panel pnlFilterButtons;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Panel pnlResults;
        private System.Windows.Forms.Panel pnlResultsHeader;
        private System.Windows.Forms.Label lblResultsInfo;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button btnFavorites;
        private System.Windows.Forms.Button btnListView;
        private System.Windows.Forms.Button btnGridView;
        private System.Windows.Forms.FlowLayoutPanel pnlListings;
        private System.Windows.Forms.Panel pnlPagination;
        private System.Windows.Forms.Label lblPageInfo;
        private System.Windows.Forms.Button btnNextPage;
        private System.Windows.Forms.Button btnPrevPage;
    }
}
