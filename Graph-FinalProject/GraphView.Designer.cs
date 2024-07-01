namespace Graph_FinalProject
{
    partial class GraphView
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GraphView));
            pictureGraph = new PictureBox();
            TabView = new TabControl();
            TabAdjacencyMatrix = new TabPage();
            matrixGridView = new DataGridView();
            TabLogs = new TabPage();
            richLogs = new RichTextBox();
            checkBoxGrid = new CheckBox();
            buttonClearGraph = new Button();
            textBox = new TextBox();
            buttonRun = new Button();
            comboBoxGraphType = new ComboBox();
            comboBoxAlgorithms = new ComboBox();
            trackBar = new TrackBar();
            ((System.ComponentModel.ISupportInitialize)pictureGraph).BeginInit();
            TabView.SuspendLayout();
            TabAdjacencyMatrix.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)matrixGridView).BeginInit();
            TabLogs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar).BeginInit();
            SuspendLayout();
            // 
            // pictureGraph
            // 
            pictureGraph.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureGraph.BackColor = Color.White;
            pictureGraph.BorderStyle = BorderStyle.FixedSingle;
            pictureGraph.Location = new Point(12, 81);
            pictureGraph.MaximumSize = new Size(1320, 920);
            pictureGraph.MinimumSize = new Size(640, 600);
            pictureGraph.Name = "pictureGraph";
            pictureGraph.Size = new Size(640, 600);
            pictureGraph.TabIndex = 0;
            pictureGraph.TabStop = false;
            pictureGraph.SizeChanged += PictureGraph_SizeChanged;
            pictureGraph.MouseDown += PictureGraph_MouseDown;
            pictureGraph.MouseMove += PictureGraph_MouseMove;
            pictureGraph.MouseUp += PictureGraph_MouseUp;
            // 
            // TabView
            // 
            TabView.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            TabView.Controls.Add(TabAdjacencyMatrix);
            TabView.Controls.Add(TabLogs);
            TabView.Location = new Point(658, 81);
            TabView.Name = "TabView";
            TabView.SelectedIndex = 0;
            TabView.Size = new Size(548, 573);
            TabView.TabIndex = 1;
            // 
            // TabAdjacencyMatrix
            // 
            TabAdjacencyMatrix.BackColor = Color.White;
            TabAdjacencyMatrix.Controls.Add(matrixGridView);
            TabAdjacencyMatrix.Location = new Point(4, 29);
            TabAdjacencyMatrix.Name = "TabAdjacencyMatrix";
            TabAdjacencyMatrix.Padding = new Padding(3);
            TabAdjacencyMatrix.Size = new Size(540, 540);
            TabAdjacencyMatrix.TabIndex = 0;
            TabAdjacencyMatrix.Text = "Adjacency Matrix";
            // 
            // matrixGridView
            // 
            matrixGridView.AllowUserToAddRows = false;
            matrixGridView.AllowUserToDeleteRows = false;
            matrixGridView.AllowUserToResizeColumns = false;
            matrixGridView.AllowUserToResizeRows = false;
            matrixGridView.BackgroundColor = Color.White;
            matrixGridView.CellBorderStyle = DataGridViewCellBorderStyle.Sunken;
            matrixGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = Color.White;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = Color.White;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            matrixGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            matrixGridView.ColumnHeadersHeight = 29;
            matrixGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            matrixGridView.DefaultCellStyle = dataGridViewCellStyle2;
            matrixGridView.Dock = DockStyle.Fill;
            matrixGridView.EditMode = DataGridViewEditMode.EditProgrammatically;
            matrixGridView.EnableHeadersVisualStyles = false;
            matrixGridView.GridColor = Color.White;
            matrixGridView.Location = new Point(3, 3);
            matrixGridView.MultiSelect = false;
            matrixGridView.Name = "matrixGridView";
            matrixGridView.ReadOnly = true;
            matrixGridView.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = Color.White;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            matrixGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            matrixGridView.RowHeadersWidth = 51;
            matrixGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            matrixGridView.RowTemplate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            matrixGridView.RowTemplate.Height = 40;
            matrixGridView.RowTemplate.ReadOnly = true;
            matrixGridView.RowTemplate.Resizable = DataGridViewTriState.False;
            matrixGridView.SelectionMode = DataGridViewSelectionMode.CellSelect;
            matrixGridView.ShowCellToolTips = false;
            matrixGridView.ShowEditingIcon = false;
            matrixGridView.Size = new Size(534, 534);
            matrixGridView.TabIndex = 0;
            matrixGridView.CellMouseDown += MatrixGridView_CellMouseDown;
            matrixGridView.CellMouseEnter += MatrixGridView_CellMouseEnter;
            matrixGridView.CellMouseLeave += MatrixGridView_CellMouseLeave;
            // 
            // TabLogs
            // 
            TabLogs.Controls.Add(richLogs);
            TabLogs.Location = new Point(4, 29);
            TabLogs.Name = "TabLogs";
            TabLogs.Padding = new Padding(3);
            TabLogs.Size = new Size(540, 540);
            TabLogs.TabIndex = 1;
            TabLogs.Text = "Logs";
            TabLogs.UseVisualStyleBackColor = true;
            // 
            // richLogs
            // 
            richLogs.BackColor = Color.White;
            richLogs.BorderStyle = BorderStyle.FixedSingle;
            richLogs.Location = new Point(3, 3);
            richLogs.Name = "richLogs";
            richLogs.ReadOnly = true;
            richLogs.Size = new Size(534, 534);
            richLogs.TabIndex = 0;
            richLogs.Text = "";
            // 
            // checkBoxGrid
            // 
            checkBoxGrid.AutoSize = true;
            checkBoxGrid.Location = new Point(135, 51);
            checkBoxGrid.Name = "checkBoxGrid";
            checkBoxGrid.Size = new Size(59, 24);
            checkBoxGrid.TabIndex = 2;
            checkBoxGrid.Text = "Grid";
            checkBoxGrid.TextAlign = ContentAlignment.MiddleCenter;
            checkBoxGrid.UseVisualStyleBackColor = true;
            checkBoxGrid.CheckedChanged += CheckBoxGrid_CheckedChanged;
            // 
            // buttonClearGraph
            // 
            buttonClearGraph.Image = Properties.Resources.clean2;
            buttonClearGraph.ImageAlign = ContentAlignment.MiddleRight;
            buttonClearGraph.Location = new Point(489, 12);
            buttonClearGraph.MaximumSize = new Size(94, 29);
            buttonClearGraph.MinimumSize = new Size(94, 29);
            buttonClearGraph.Name = "buttonClearGraph";
            buttonClearGraph.Size = new Size(94, 29);
            buttonClearGraph.TabIndex = 5;
            buttonClearGraph.Text = "Clear";
            buttonClearGraph.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonClearGraph.UseVisualStyleBackColor = true;
            buttonClearGraph.Click += ButtonClearGraph_Click;
            // 
            // textBox
            // 
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox.Location = new Point(12, 12);
            textBox.Name = "textBox";
            textBox.Size = new Size(117, 27);
            textBox.TabIndex = 6;
            textBox.TextAlign = HorizontalAlignment.Center;
            textBox.KeyDown += TextBox_KeyDown;
            // 
            // buttonRun
            // 
            buttonRun.Image = Properties.Resources.play2;
            buttonRun.ImageAlign = ContentAlignment.MiddleRight;
            buttonRun.Location = new Point(389, 10);
            buttonRun.Name = "buttonRun";
            buttonRun.Size = new Size(94, 29);
            buttonRun.TabIndex = 7;
            buttonRun.Text = "Run";
            buttonRun.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonRun.UseVisualStyleBackColor = true;
            buttonRun.Click += ButtonRun_Click;
            // 
            // comboBoxGraphType
            // 
            comboBoxGraphType.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxGraphType.FormattingEnabled = true;
            comboBoxGraphType.Items.AddRange(new object[] { "Not Oriented", "Oriented" });
            comboBoxGraphType.Location = new Point(12, 47);
            comboBoxGraphType.Name = "comboBoxGraphType";
            comboBoxGraphType.Size = new Size(117, 28);
            comboBoxGraphType.TabIndex = 11;
            comboBoxGraphType.SelectedIndexChanged += ComboBoxGraphType_SelectedIndexChanged;
            // 
            // comboBoxAlgorithms
            // 
            comboBoxAlgorithms.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxAlgorithms.FormattingEnabled = true;
            comboBoxAlgorithms.Items.AddRange(new object[] { "Check Edge", "Show Degree", "Show Adjacent", "Check Cyclic", "Connection Check", "Strongly Connected Components", "Topological Sort", "Eulerian Cycle", "Find Shortest Path" });
            comboBoxAlgorithms.Location = new Point(135, 11);
            comboBoxAlgorithms.Name = "comboBoxAlgorithms";
            comboBoxAlgorithms.Size = new Size(248, 28);
            comboBoxAlgorithms.TabIndex = 18;
            comboBoxAlgorithms.SelectedIndexChanged += ComboBoxAlgorithms_SelectedIndexChanged;
            // 
            // trackBar
            // 
            trackBar.LargeChange = 200;
            trackBar.Location = new Point(589, 12);
            trackBar.Maximum = 1000;
            trackBar.Minimum = 100;
            trackBar.Name = "trackBar";
            trackBar.RightToLeft = RightToLeft.Yes;
            trackBar.Size = new Size(162, 56);
            trackBar.SmallChange = 100;
            trackBar.TabIndex = 19;
            trackBar.TickFrequency = 100;
            trackBar.TickStyle = TickStyle.None;
            trackBar.Value = 100;
            // 
            // GraphView
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1230, 703);
            Controls.Add(pictureGraph);
            Controls.Add(trackBar);
            Controls.Add(comboBoxAlgorithms);
            Controls.Add(comboBoxGraphType);
            Controls.Add(buttonRun);
            Controls.Add(textBox);
            Controls.Add(buttonClearGraph);
            Controls.Add(checkBoxGrid);
            Controls.Add(TabView);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(1230, 750);
            Name = "GraphView";
            Text = "Kintiny";
            ((System.ComponentModel.ISupportInitialize)pictureGraph).EndInit();
            TabView.ResumeLayout(false);
            TabAdjacencyMatrix.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)matrixGridView).EndInit();
            TabLogs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)trackBar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureGraph;
        private TabControl TabView;
        private TabPage TabAdjacencyMatrix;
        private TabPage TabLogs;
        private DataGridView matrixGridView;
        private CheckBox checkBoxGrid;
        private RichTextBox richLogs;
        private Button buttonClearGraph;
        private TextBox textBox;
        private Button buttonRun;
        private ComboBox comboBoxGraphType;
        private TrackBar trackBar;
        private ComboBox comboBoxAlgorithms;
    }
}
