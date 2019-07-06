namespace Tes4
{
    partial class QueryPatient
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QueryPatient));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.listView = new System.Windows.Forms.ListView();
            this.name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TreatmentDay = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DisName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.symptom = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.numQuery = new DevExpress.XtraEditors.TextEdit();
            this.nameQuery = new DevExpress.XtraEditors.TextEdit();
            this.idQuery = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.ID = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.Tên = new DevExpress.XtraLayout.LayoutControlItem();
            this.SĐT = new DevExpress.XtraLayout.LayoutControlItem();
            this.Result = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.erroP2 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQuery.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nameQuery.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.idQuery.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Tên)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SĐT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Result)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.erroP2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnClear);
            this.layoutControl1.Controls.Add(this.btnSearch);
            this.layoutControl1.Controls.Add(this.listView);
            this.layoutControl1.Controls.Add(this.numQuery);
            this.layoutControl1.Controls.Add(this.nameQuery);
            this.layoutControl1.Controls.Add(this.idQuery);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1209, 480);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnClear
            // 
            this.btnClear.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.ImageOptions.Image")));
            this.btnClear.Location = new System.Drawing.Point(963, 150);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(212, 42);
            this.btnClear.StyleController = this.layoutControl1;
            this.btnClear.TabIndex = 9;
            this.btnClear.Text = "Xóa";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.ImageOptions.Image")));
            this.btnSearch.Location = new System.Drawing.Point(24, 150);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(931, 42);
            this.btnSearch.StyleController = this.layoutControl1;
            this.btnSearch.TabIndex = 8;
            this.btnSearch.Text = "Tìm";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // listView
            // 
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.name,
            this.TreatmentDay,
            this.DisName,
            this.symptom});
            this.listView.FullRowSelect = true;
            this.listView.GridLines = true;
            this.listView.Location = new System.Drawing.Point(85, 200);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(1100, 256);
            this.listView.TabIndex = 7;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            // 
            // name
            // 
            this.name.Text = "Tên";
            this.name.Width = 154;
            // 
            // TreatmentDay
            // 
            this.TreatmentDay.Text = "Ngày khám";
            this.TreatmentDay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TreatmentDay.Width = 257;
            // 
            // DisName
            // 
            this.DisName.Text = "Bệnh";
            this.DisName.Width = 210;
            // 
            // symptom
            // 
            this.symptom.Text = "Triệu chứng";
            this.symptom.Width = 217;
            // 
            // numQuery
            // 
            this.numQuery.Location = new System.Drawing.Point(85, 108);
            this.numQuery.Name = "numQuery";
            this.numQuery.Size = new System.Drawing.Size(1100, 34);
            this.numQuery.StyleController = this.layoutControl1;
            this.numQuery.TabIndex = 6;
            // 
            // nameQuery
            // 
            this.nameQuery.Location = new System.Drawing.Point(85, 66);
            this.nameQuery.Name = "nameQuery";
            this.nameQuery.Size = new System.Drawing.Size(1100, 34);
            this.nameQuery.StyleController = this.layoutControl1;
            this.nameQuery.TabIndex = 5;
            // 
            // idQuery
            // 
            this.idQuery.Location = new System.Drawing.Point(85, 24);
            this.idQuery.Name = "idQuery";
            this.idQuery.Size = new System.Drawing.Size(1100, 34);
            this.idQuery.StyleController = this.layoutControl1;
            this.idQuery.TabIndex = 4;
            this.idQuery.TextChanged += new System.EventHandler(this.idQuery_TextChanged);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.ID,
            this.emptySpaceItem1,
            this.Tên,
            this.SĐT,
            this.Result,
            this.layoutControlItem1,
            this.layoutControlItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 4;
            this.layoutControlGroup1.Size = new System.Drawing.Size(1209, 480);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // ID
            // 
            this.ID.Control = this.idQuery;
            this.ID.Location = new System.Drawing.Point(0, 0);
            this.ID.Name = "ID";
            this.ID.Size = new System.Drawing.Size(1169, 42);
            this.ID.TextSize = new System.Drawing.Size(57, 25);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(1159, 126);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(10, 50);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // Tên
            // 
            this.Tên.Control = this.nameQuery;
            this.Tên.Location = new System.Drawing.Point(0, 42);
            this.Tên.Name = "Tên";
            this.Tên.Size = new System.Drawing.Size(1169, 42);
            this.Tên.TextSize = new System.Drawing.Size(57, 25);
            // 
            // SĐT
            // 
            this.SĐT.Control = this.numQuery;
            this.SĐT.Location = new System.Drawing.Point(0, 84);
            this.SĐT.Name = "SĐT";
            this.SĐT.Size = new System.Drawing.Size(1169, 42);
            this.SĐT.TextSize = new System.Drawing.Size(57, 25);
            // 
            // Result
            // 
            this.Result.Control = this.listView;
            this.Result.Location = new System.Drawing.Point(0, 176);
            this.Result.Name = "Result";
            this.Result.Size = new System.Drawing.Size(1169, 264);
            this.Result.TextSize = new System.Drawing.Size(57, 25);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btnSearch;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 126);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(939, 50);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnClear;
            this.layoutControlItem2.Location = new System.Drawing.Point(939, 126);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(220, 50);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // erroP2
            // 
            this.erroP2.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.erroP2.ContainerControl = this;
            // 
            // QueryPatient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1209, 480);
            this.Controls.Add(this.layoutControl1);
            this.Name = "QueryPatient";
            this.Text = "Tìm kiếm bệnh nhân";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numQuery.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nameQuery.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.idQuery.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Tên)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SĐT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Result)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.erroP2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.TextEdit numQuery;
        private DevExpress.XtraEditors.TextEdit nameQuery;
        private DevExpress.XtraEditors.TextEdit idQuery;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem ID;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem Tên;
        private DevExpress.XtraLayout.LayoutControlItem SĐT;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ColumnHeader name;
        private System.Windows.Forms.ColumnHeader TreatmentDay;
        private System.Windows.Forms.ColumnHeader DisName;
        private System.Windows.Forms.ColumnHeader symptom;
        private DevExpress.XtraLayout.LayoutControlItem Result;
        private System.Windows.Forms.ErrorProvider erroP2;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    }
}