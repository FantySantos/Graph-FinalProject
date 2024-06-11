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
            pictureGraph = new PictureBox();
            TabView = new TabControl();
            TabAdjacencyMatrix = new TabPage();
            matrixGridView = new DataGridView();
            TabLogs = new TabPage();
            richLogs = new RichTextBox();
            checkBoxGrid = new CheckBox();
            radioButtonDirected = new RadioButton();
            radioButtonUndirected = new RadioButton();
            buttonClearGraph = new Button();
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
            pictureGraph.SizeChanged += pictureGraph_SizeChanged;
            pictureGraph.MouseDown += PictureGraph_MouseDown;
            pictureGraph.MouseMove += pictureGraph_MouseMove;
            pictureGraph.MouseUp += pictureGraph_MouseUp;
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
            matrixGridView.BorderStyle = BorderStyle.None;
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
            checkBoxGrid.Location = new Point(663, 658);
            checkBoxGrid.Name = "checkBoxGrid";
            checkBoxGrid.Size = new Size(59, 24);
            checkBoxGrid.TabIndex = 2;
            checkBoxGrid.Text = "Grid";
            checkBoxGrid.UseVisualStyleBackColor = true;
            checkBoxGrid.CheckedChanged += CheckBoxGrid_CheckedChanged;
            // 
            // radioButtonDirected
            // 
            radioButtonDirected.AutoSize = true;
            radioButtonDirected.Location = new Point(147, 51);
            radioButtonDirected.Name = "radioButtonDirected";
            radioButtonDirected.Size = new Size(97, 24);
            radioButtonDirected.TabIndex = 3;
            radioButtonDirected.Text = "Orientado";
            radioButtonDirected.UseVisualStyleBackColor = true;
            // 
            // radioButtonUndirected
            // 
            radioButtonUndirected.AutoSize = true;
            radioButtonUndirected.Checked = true;
            radioButtonUndirected.Location = new Point(12, 51);
            radioButtonUndirected.Name = "radioButtonUndirected";
            radioButtonUndirected.Size = new Size(129, 24);
            radioButtonUndirected.TabIndex = 4;
            radioButtonUndirected.TabStop = true;
            radioButtonUndirected.Text = "Não Orientado";
            radioButtonUndirected.UseVisualStyleBackColor = true;
            radioButtonUndirected.CheckedChanged += radioButton_CheckedChanged;
            // 
            // buttonClearGraph
            // 
            buttonClearGraph.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonClearGraph.Location = new Point(558, 46);
            buttonClearGraph.MaximumSize = new Size(94, 29);
            buttonClearGraph.MinimumSize = new Size(94, 29);
            buttonClearGraph.Name = "buttonClearGraph";
            buttonClearGraph.Size = new Size(94, 29);
            buttonClearGraph.TabIndex = 5;
            buttonClearGraph.Text = "Apagar";
            buttonClearGraph.UseVisualStyleBackColor = true;
            buttonClearGraph.Click += buttonClearGraph_Click;
            // 
            // GraphView
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1212, 703);
            Controls.Add(buttonClearGraph);
            Controls.Add(radioButtonUndirected);
            Controls.Add(radioButtonDirected);
            Controls.Add(checkBoxGrid);
            Controls.Add(TabView);
            Controls.Add(pictureGraph);
            MinimumSize = new Size(1230, 750);
            Name = "GraphView";
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
        private RadioButton radioButtonDirected;
        private RadioButton radioButtonUndirected;
        private Button buttonClearGraph;
    }
}
