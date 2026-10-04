namespace FistLab {
  partial class Form1 {
    /// <summary>
    /// Обязательная переменная конструктора.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Освободить все используемые ресурсы.
    /// </summary>
    /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Код, автоматически созданный конструктором форм Windows

    /// <summary>
    /// Требуемый метод для поддержки конструктора — не изменяйте 
    /// содержимое этого метода с помощью редактора кода.
    /// </summary>
    private void InitializeComponent() {
      System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
      this.sortingShow = new System.Windows.Forms.DataGridView();
      this.ValueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.randomGeneric = new System.Windows.Forms.Button();
      this.chartVisualizer = new System.Windows.Forms.DataVisualization.Charting.Chart();
      this.checkBoxBubble = new System.Windows.Forms.CheckBox();
      this.checkBoxInsertion = new System.Windows.Forms.CheckBox();
      this.checkBoxShaker = new System.Windows.Forms.CheckBox();
      this.checkBoxQuick = new System.Windows.Forms.CheckBox();
      this.checkBoxBogo = new System.Windows.Forms.CheckBox();
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.управлениеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.рассчитатьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.очиститьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.dataGridViewResults = new System.Windows.Forms.DataGridView();
      this.AlgorithmColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.TimeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.checkBoxReverse = new System.Windows.Forms.CheckBox();
      this.label1 = new System.Windows.Forms.Label();
      this.импортToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.importFromExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.importFromGoogleSheetsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      ((System.ComponentModel.ISupportInitialize)(this.sortingShow)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.chartVisualizer)).BeginInit();
      this.menuStrip1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResults)).BeginInit();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // sortingShow
      // 
      this.sortingShow.BackgroundColor = System.Drawing.Color.White;
      this.sortingShow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.sortingShow.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ValueColumn});
      this.sortingShow.Location = new System.Drawing.Point(686, 309);
      this.sortingShow.Name = "sortingShow";
      this.sortingShow.RowHeadersVisible = false;
      this.sortingShow.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.sortingShow.Size = new System.Drawing.Size(103, 161);
      this.sortingShow.TabIndex = 0;
      this.sortingShow.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.sortingShow_CellContentClick);
      // 
      // ValueColumn
      // 
      this.ValueColumn.HeaderText = "Значение";
      this.ValueColumn.Name = "ValueColumn";
      // 
      // textBox1
      // 
      this.textBox1.Location = new System.Drawing.Point(522, 319);
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new System.Drawing.Size(100, 20);
      this.textBox1.TabIndex = 1;
      // 
      // randomGeneric
      // 
      this.randomGeneric.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
      this.randomGeneric.Cursor = System.Windows.Forms.Cursors.Hand;
      this.randomGeneric.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.randomGeneric.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.randomGeneric.ForeColor = System.Drawing.Color.White;
      this.randomGeneric.Location = new System.Drawing.Point(442, 389);
      this.randomGeneric.Name = "randomGeneric";
      this.randomGeneric.Size = new System.Drawing.Size(160, 36);
      this.randomGeneric.TabIndex = 2;
      this.randomGeneric.Text = "Сгенерировать данные";
      this.randomGeneric.UseVisualStyleBackColor = false;
      this.randomGeneric.Click += new System.EventHandler(this.randomGeneric_Click);
      // 
      // chartVisualizer
      // 
      this.chartVisualizer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      chartArea1.Name = "ChartArea1";
      this.chartVisualizer.ChartAreas.Add(chartArea1);
      this.chartVisualizer.Location = new System.Drawing.Point(380, 40);
      this.chartVisualizer.Name = "chartVisualizer";
      this.chartVisualizer.Size = new System.Drawing.Size(440, 260);
      this.chartVisualizer.TabIndex = 3;
      this.chartVisualizer.Text = "chart1";
      // 
      // checkBoxBubble
      // 
      this.checkBoxBubble.AutoSize = true;
      this.checkBoxBubble.Location = new System.Drawing.Point(6, 19);
      this.checkBoxBubble.Name = "checkBoxBubble";
      this.checkBoxBubble.Size = new System.Drawing.Size(95, 17);
      this.checkBoxBubble.TabIndex = 4;
      this.checkBoxBubble.Text = "Пузырьковая";
      this.checkBoxBubble.UseVisualStyleBackColor = true;
      // 
      // checkBoxInsertion
      // 
      this.checkBoxInsertion.AutoSize = true;
      this.checkBoxInsertion.Location = new System.Drawing.Point(6, 41);
      this.checkBoxInsertion.Name = "checkBoxInsertion";
      this.checkBoxInsertion.Size = new System.Drawing.Size(82, 17);
      this.checkBoxInsertion.TabIndex = 4;
      this.checkBoxInsertion.Text = "Вставками";
      this.checkBoxInsertion.UseVisualStyleBackColor = true;
      // 
      // checkBoxShaker
      // 
      this.checkBoxShaker.AutoSize = true;
      this.checkBoxShaker.Location = new System.Drawing.Point(5, 64);
      this.checkBoxShaker.Name = "checkBoxShaker";
      this.checkBoxShaker.Size = new System.Drawing.Size(83, 17);
      this.checkBoxShaker.TabIndex = 4;
      this.checkBoxShaker.Text = "Шейкерная";
      this.checkBoxShaker.UseVisualStyleBackColor = true;
      // 
      // checkBoxQuick
      // 
      this.checkBoxQuick.AutoSize = true;
      this.checkBoxQuick.Location = new System.Drawing.Point(5, 87);
      this.checkBoxQuick.Name = "checkBoxQuick";
      this.checkBoxQuick.Size = new System.Drawing.Size(70, 17);
      this.checkBoxQuick.TabIndex = 4;
      this.checkBoxQuick.Text = "Быстрая";
      this.checkBoxQuick.UseVisualStyleBackColor = true;
      // 
      // checkBoxBogo
      // 
      this.checkBoxBogo.AutoSize = true;
      this.checkBoxBogo.Location = new System.Drawing.Point(6, 110);
      this.checkBoxBogo.Name = "checkBoxBogo";
      this.checkBoxBogo.Size = new System.Drawing.Size(57, 17);
      this.checkBoxBogo.TabIndex = 4;
      this.checkBoxBogo.Text = "BOGO";
      this.checkBoxBogo.UseVisualStyleBackColor = true;
      // 
      // menuStrip1
      // 
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.управлениеToolStripMenuItem,
            this.импортToolStripMenuItem});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new System.Drawing.Size(828, 24);
      this.menuStrip1.TabIndex = 5;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // управлениеToolStripMenuItem
      // 
      this.управлениеToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.рассчитатьToolStripMenuItem,
            this.очиститьToolStripMenuItem});
      this.управлениеToolStripMenuItem.Name = "управлениеToolStripMenuItem";
      this.управлениеToolStripMenuItem.Size = new System.Drawing.Size(85, 20);
      this.управлениеToolStripMenuItem.Text = "Управление";
      // 
      // рассчитатьToolStripMenuItem
      // 
      this.рассчитатьToolStripMenuItem.Name = "рассчитатьToolStripMenuItem";
      this.рассчитатьToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
      this.рассчитатьToolStripMenuItem.Text = "Рассчитать";
      this.рассчитатьToolStripMenuItem.Click += new System.EventHandler(this.рассчитатьToolStripMenuItem_Click);
      // 
      // очиститьToolStripMenuItem
      // 
      this.очиститьToolStripMenuItem.Name = "очиститьToolStripMenuItem";
      this.очиститьToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
      this.очиститьToolStripMenuItem.Text = "Очистить";
      this.очиститьToolStripMenuItem.Click += new System.EventHandler(this.очиститьToolStripMenuItem_Click);
      // 
      // dataGridViewResults
      // 
      this.dataGridViewResults.AllowUserToAddRows = false;
      this.dataGridViewResults.AllowUserToDeleteRows = false;
      this.dataGridViewResults.BackgroundColor = System.Drawing.Color.White;
      this.dataGridViewResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridViewResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AlgorithmColumn,
            this.TimeColumn});
      this.dataGridViewResults.Location = new System.Drawing.Point(18, 226);
      this.dataGridViewResults.MultiSelect = false;
      this.dataGridViewResults.Name = "dataGridViewResults";
      this.dataGridViewResults.ReadOnly = true;
      this.dataGridViewResults.RowHeadersVisible = false;
      this.dataGridViewResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.dataGridViewResults.Size = new System.Drawing.Size(204, 222);
      this.dataGridViewResults.TabIndex = 6;
      this.dataGridViewResults.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewResults_CellContentClick);
      // 
      // AlgorithmColumn
      // 
      this.AlgorithmColumn.HeaderText = "Алгоритм";
      this.AlgorithmColumn.Name = "AlgorithmColumn";
      this.AlgorithmColumn.ReadOnly = true;
      // 
      // TimeColumn
      // 
      this.TimeColumn.HeaderText = "Время (мс)";
      this.TimeColumn.Name = "TimeColumn";
      this.TimeColumn.ReadOnly = true;
      // 
      // checkBoxReverse
      // 
      this.checkBoxReverse.AutoSize = true;
      this.checkBoxReverse.Location = new System.Drawing.Point(380, 345);
      this.checkBoxReverse.Name = "checkBoxReverse";
      this.checkBoxReverse.Size = new System.Drawing.Size(104, 17);
      this.checkBoxReverse.TabIndex = 7;
      this.checkBoxReverse.Text = "Инвертировать";
      this.checkBoxReverse.UseVisualStyleBackColor = true;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(377, 319);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(139, 13);
      this.label1.TabIndex = 8;
      this.label1.Text = "Введите число элементов";
      // 
      // импортToolStripMenuItem
      // 
      this.импортToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importFromExcelToolStripMenuItem,
            this.importFromGoogleSheetsToolStripMenuItem});
      this.импортToolStripMenuItem.Name = "импортToolStripMenuItem";
      this.импортToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
      this.импортToolStripMenuItem.Text = "Импорт";
      // 
      // importFromExcelToolStripMenuItem
      // 
      this.importFromExcelToolStripMenuItem.Name = "importFromExcelToolStripMenuItem";
      this.importFromExcelToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
      this.importFromExcelToolStripMenuItem.Text = "Exel файл";
      this.importFromExcelToolStripMenuItem.Click += new System.EventHandler(this.importFromExcelToolStripMenuItem_Click);
      // 
      // importFromGoogleSheetsToolStripMenuItem
      // 
      this.importFromGoogleSheetsToolStripMenuItem.Name = "importFromGoogleSheetsToolStripMenuItem";
      this.importFromGoogleSheetsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
      this.importFromGoogleSheetsToolStripMenuItem.Text = "Google Sheets";
      this.importFromGoogleSheetsToolStripMenuItem.Click += new System.EventHandler(this.importFromGoogleSheetsToolStripMenuItem_Click);
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.checkBoxBubble);
      this.groupBox1.Controls.Add(this.checkBoxInsertion);
      this.groupBox1.Controls.Add(this.checkBoxShaker);
      this.groupBox1.Controls.Add(this.checkBoxQuick);
      this.groupBox1.Controls.Add(this.checkBoxBogo);
      this.groupBox1.Location = new System.Drawing.Point(12, 40);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(200, 136);
      this.groupBox1.TabIndex = 9;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Алгоритмы";
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(828, 479);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.checkBoxReverse);
      this.Controls.Add(this.dataGridViewResults);
      this.Controls.Add(this.chartVisualizer);
      this.Controls.Add(this.randomGeneric);
      this.Controls.Add(this.textBox1);
      this.Controls.Add(this.sortingShow);
      this.Controls.Add(this.menuStrip1);
      this.MainMenuStrip = this.menuStrip1;
      this.Name = "Form1";
      this.Text = "Form1";
      ((System.ComponentModel.ISupportInitialize)(this.sortingShow)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.chartVisualizer)).EndInit();
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResults)).EndInit();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.DataGridView sortingShow;
    private System.Windows.Forms.DataGridViewTextBoxColumn ValueColumn;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.Button randomGeneric;
    private System.Windows.Forms.DataVisualization.Charting.Chart chartVisualizer;
    private System.Windows.Forms.CheckBox checkBoxBubble;
    private System.Windows.Forms.CheckBox checkBoxInsertion;
    private System.Windows.Forms.CheckBox checkBoxShaker;
    private System.Windows.Forms.CheckBox checkBoxQuick;
    private System.Windows.Forms.CheckBox checkBoxBogo;
    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem управлениеToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem рассчитатьToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem очиститьToolStripMenuItem;
    private System.Windows.Forms.DataGridView dataGridViewResults;
    private System.Windows.Forms.DataGridViewTextBoxColumn AlgorithmColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn TimeColumn;
    private System.Windows.Forms.CheckBox checkBoxReverse;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ToolStripMenuItem импортToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem importFromExcelToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem importFromGoogleSheetsToolStripMenuItem;
    private System.Windows.Forms.GroupBox groupBox1;
  }
}

