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
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
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
            radioButtonCheckEdge = new RadioButton();
            radioButtonShowDegree = new RadioButton();
            radioButtonShowAdjacent = new RadioButton();
            comboBoxGraphType = new ComboBox();
            contextMenuStrip1 = new ContextMenuStrip(components);
            radioButtonDFS = new RadioButton();
            ((System.ComponentModel.ISupportInitialize)pictureGraph).BeginInit();
            TabView.SuspendLayout();
            TabAdjacencyMatrix.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)matrixGridView).BeginInit();
            TabLogs.SuspendLayout();
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
            richLogs.Dock = DockStyle.Fill;
            richLogs.Location = new Point(3, 3);
            richLogs.Name = "richLogs";
            richLogs.ReadOnly = true;
            richLogs.Size = new Size(534, 534);
            richLogs.TabIndex = 0;
            richLogs.Text = "";
            // 
            // checkBoxGrid
            // 
            checkBoxGrid.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            checkBoxGrid.AutoSize = true;
            checkBoxGrid.Location = new Point(661, 655);
            checkBoxGrid.Name = "checkBoxGrid";
            checkBoxGrid.Size = new Size(59, 24);
            checkBoxGrid.TabIndex = 2;
            checkBoxGrid.Text = "Grid";
            checkBoxGrid.UseVisualStyleBackColor = true;
            checkBoxGrid.CheckedChanged += CheckBoxGrid_CheckedChanged;
            // 
            // buttonClearGraph
            // 
            buttonClearGraph.Location = new Point(235, 12);
            buttonClearGraph.MaximumSize = new Size(94, 29);
            buttonClearGraph.MinimumSize = new Size(94, 29);
            buttonClearGraph.Name = "buttonClearGraph";
            buttonClearGraph.Size = new Size(94, 29);
            buttonClearGraph.TabIndex = 5;
            buttonClearGraph.Text = "Clear";
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
            buttonRun.Location = new Point(135, 12);
            buttonRun.Name = "buttonRun";
            buttonRun.Size = new Size(94, 27);
            buttonRun.TabIndex = 7;
            buttonRun.Text = "Run";
            buttonRun.UseVisualStyleBackColor = true;
            buttonRun.Click += ButtonRun_Click;
            // 
            // radioButtonCheckEdge
            // 
            radioButtonCheckEdge.AutoSize = true;
            radioButtonCheckEdge.Location = new Point(12, 51);
            radioButtonCheckEdge.Name = "radioButtonCheckEdge";
            radioButtonCheckEdge.Size = new Size(107, 24);
            radioButtonCheckEdge.TabIndex = 8;
            radioButtonCheckEdge.Text = "Check Edge";
            radioButtonCheckEdge.UseVisualStyleBackColor = true;
            // 
            // radioButtonShowDegree
            // 
            radioButtonShowDegree.AutoSize = true;
            radioButtonShowDegree.Location = new Point(125, 51);
            radioButtonShowDegree.Name = "radioButtonShowDegree";
            radioButtonShowDegree.Size = new Size(119, 24);
            radioButtonShowDegree.TabIndex = 9;
            radioButtonShowDegree.Text = "Show Degree";
            radioButtonShowDegree.UseVisualStyleBackColor = true;
            // 
            // radioButtonShowAdjacent
            // 
            radioButtonShowAdjacent.AutoSize = true;
            radioButtonShowAdjacent.Location = new Point(250, 51);
            radioButtonShowAdjacent.Name = "radioButtonShowAdjacent";
            radioButtonShowAdjacent.Size = new Size(129, 24);
            radioButtonShowAdjacent.TabIndex = 10;
            radioButtonShowAdjacent.Text = "Show Adjacent";
            radioButtonShowAdjacent.UseVisualStyleBackColor = true;
            // 
            // comboBoxGraphType
            // 
            comboBoxGraphType.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            comboBoxGraphType.FormattingEnabled = true;
            comboBoxGraphType.Items.AddRange(new object[] { "Not Oriented", "Oriented" });
            comboBoxGraphType.Location = new Point(729, 653);
            comboBoxGraphType.Name = "comboBoxGraphType";
            comboBoxGraphType.Size = new Size(123, 28);
            comboBoxGraphType.TabIndex = 11;
            comboBoxGraphType.SelectedIndexChanged += ComboBoxGraphType_SelectedIndexChanged;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // radioButtonDFS
            // 
            radioButtonDFS.AutoSize = true;
            radioButtonDFS.Location = new Point(385, 51);
            radioButtonDFS.Name = "radioButtonDFS";
            radioButtonDFS.Size = new Size(56, 24);
            radioButtonDFS.TabIndex = 12;
            radioButtonDFS.Text = "DFS";
            radioButtonDFS.UseVisualStyleBackColor = true;
            // 
            // GraphView
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1212, 703);
            Controls.Add(radioButtonDFS);
            Controls.Add(comboBoxGraphType);
            Controls.Add(radioButtonShowAdjacent);
            Controls.Add(radioButtonShowDegree);
            Controls.Add(radioButtonCheckEdge);
            Controls.Add(buttonRun);
            Controls.Add(textBox);
            Controls.Add(buttonClearGraph);
            Controls.Add(checkBoxGrid);
            Controls.Add(TabView);
            Controls.Add(pictureGraph);
            MinimumSize = new Size(1230, 750);
            Name = "GraphView";
            Text = "Kitiny";
            ((System.ComponentModel.ISupportInitialize)pictureGraph).EndInit();
            TabView.ResumeLayout(false);
            TabAdjacencyMatrix.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)matrixGridView).EndInit();
            TabLogs.ResumeLayout(false);
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
        private RadioButton radioButtonCheckEdge;
        private RadioButton radioButtonShowDegree;
        private RadioButton radioButtonShowAdjacent;
        private ComboBox comboBoxGraphType;
        private ContextMenuStrip contextMenuStrip1;
        private RadioButton radioButtonDFS;
    }
}
