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
      System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
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
      ((System.ComponentModel.ISupportInitialize)(this.sortingShow)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.chartVisualizer)).BeginInit();
      this.menuStrip1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResults)).BeginInit();
      this.SuspendLayout();
      // 
      // sortingShow
      // 
      this.sortingShow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.sortingShow.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ValueColumn});
      this.sortingShow.Location = new System.Drawing.Point(533, 245);
      this.sortingShow.Name = "sortingShow";
      this.sortingShow.Size = new System.Drawing.Size(240, 55);
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
      this.textBox1.Location = new System.Drawing.Point(533, 317);
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new System.Drawing.Size(100, 20);
      this.textBox1.TabIndex = 1;
      // 
      // randomGeneric
      // 
      this.randomGeneric.Location = new System.Drawing.Point(656, 306);
      this.randomGeneric.Name = "randomGeneric";
      this.randomGeneric.Size = new System.Drawing.Size(117, 40);
      this.randomGeneric.TabIndex = 2;
      this.randomGeneric.Text = "Сгенерировать данные";
      this.randomGeneric.UseVisualStyleBackColor = true;
      this.randomGeneric.Click += new System.EventHandler(this.randomGeneric_Click);
      // 
      // chartVisualizer
      // 
      chartArea2.Name = "ChartArea1";
      this.chartVisualizer.ChartAreas.Add(chartArea2);
      this.chartVisualizer.Location = new System.Drawing.Point(533, 23);
      this.chartVisualizer.Name = "chartVisualizer";
      this.chartVisualizer.Size = new System.Drawing.Size(240, 205);
      this.chartVisualizer.TabIndex = 3;
      this.chartVisualizer.Text = "chart1";
      // 
      // checkBoxBubble
      // 
      this.checkBoxBubble.AutoSize = true;
      this.checkBoxBubble.Location = new System.Drawing.Point(13, 23);
      this.checkBoxBubble.Name = "checkBoxBubble";
      this.checkBoxBubble.Size = new System.Drawing.Size(95, 17);
      this.checkBoxBubble.TabIndex = 4;
      this.checkBoxBubble.Text = "Пузырьковая";
      this.checkBoxBubble.UseVisualStyleBackColor = true;
      // 
      // checkBoxInsertion
      // 
      this.checkBoxInsertion.AutoSize = true;
      this.checkBoxInsertion.Location = new System.Drawing.Point(13, 46);
      this.checkBoxInsertion.Name = "checkBoxInsertion";
      this.checkBoxInsertion.Size = new System.Drawing.Size(82, 17);
      this.checkBoxInsertion.TabIndex = 4;
      this.checkBoxInsertion.Text = "Вставками";
      this.checkBoxInsertion.UseVisualStyleBackColor = true;
      // 
      // checkBoxShaker
      // 
      this.checkBoxShaker.AutoSize = true;
      this.checkBoxShaker.Location = new System.Drawing.Point(13, 69);
      this.checkBoxShaker.Name = "checkBoxShaker";
      this.checkBoxShaker.Size = new System.Drawing.Size(83, 17);
      this.checkBoxShaker.TabIndex = 4;
      this.checkBoxShaker.Text = "Шейкерная";
      this.checkBoxShaker.UseVisualStyleBackColor = true;
      // 
      // checkBoxQuick
      // 
      this.checkBoxQuick.AutoSize = true;
      this.checkBoxQuick.Location = new System.Drawing.Point(12, 92);
      this.checkBoxQuick.Name = "checkBoxQuick";
      this.checkBoxQuick.Size = new System.Drawing.Size(70, 17);
      this.checkBoxQuick.TabIndex = 4;
      this.checkBoxQuick.Text = "Быстрая";
      this.checkBoxQuick.UseVisualStyleBackColor = true;
      // 
      // checkBoxBogo
      // 
      this.checkBoxBogo.AutoSize = true;
      this.checkBoxBogo.Location = new System.Drawing.Point(12, 115);
      this.checkBoxBogo.Name = "checkBoxBogo";
      this.checkBoxBogo.Size = new System.Drawing.Size(57, 17);
      this.checkBoxBogo.TabIndex = 4;
      this.checkBoxBogo.Text = "BOGO";
      this.checkBoxBogo.UseVisualStyleBackColor = true;
      // 
      // menuStrip1
      // 
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.управлениеToolStripMenuItem});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new System.Drawing.Size(800, 24);
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
      this.dataGridViewResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridViewResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AlgorithmColumn,
            this.TimeColumn});
      this.dataGridViewResults.Location = new System.Drawing.Point(114, 149);
      this.dataGridViewResults.Name = "dataGridViewResults";
      this.dataGridViewResults.ReadOnly = true;
      this.dataGridViewResults.Size = new System.Drawing.Size(240, 150);
      this.dataGridViewResults.TabIndex = 6;
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
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(800, 450);
      this.Controls.Add(this.dataGridViewResults);
      this.Controls.Add(this.checkBoxBogo);
      this.Controls.Add(this.checkBoxQuick);
      this.Controls.Add(this.checkBoxShaker);
      this.Controls.Add(this.checkBoxInsertion);
      this.Controls.Add(this.checkBoxBubble);
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
  }
}

