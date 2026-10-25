namespace FistLab {
  partial class Form3 {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.управлениеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.построитьГрафикToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.рассчитатьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.очиститьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.lblResult = new System.Windows.Forms.Label();
      this.txtA = new System.Windows.Forms.TextBox();
      this.txtB = new System.Windows.Forms.TextBox();
      this.txtFunc = new System.Windows.Forms.TextBox();
      this.txtE = new System.Windows.Forms.NumericUpDown();
      this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
      this.label5 = new System.Windows.Forms.Label();
      this.menuStrip1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.txtE)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
      this.SuspendLayout();
      // 
      // menuStrip1
      // 
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.управлениеToolStripMenuItem});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new System.Drawing.Size(800, 24);
      this.menuStrip1.TabIndex = 0;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // управлениеToolStripMenuItem
      // 
      this.управлениеToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.построитьГрафикToolStripMenuItem,
            this.рассчитатьToolStripMenuItem,
            this.очиститьToolStripMenuItem});
      this.управлениеToolStripMenuItem.Name = "управлениеToolStripMenuItem";
      this.управлениеToolStripMenuItem.Size = new System.Drawing.Size(85, 20);
      this.управлениеToolStripMenuItem.Text = "Управление";
      // 
      // построитьГрафикToolStripMenuItem
      // 
      this.построитьГрафикToolStripMenuItem.Name = "построитьГрафикToolStripMenuItem";
      this.построитьГрафикToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
      this.построитьГрафикToolStripMenuItem.Text = "Построить график";
      this.построитьГрафикToolStripMenuItem.Click += new System.EventHandler(this.построитьГрафикToolStripMenuItem_Click);
      // 
      // рассчитатьToolStripMenuItem
      // 
      this.рассчитатьToolStripMenuItem.Name = "рассчитатьToolStripMenuItem";
      this.рассчитатьToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
      this.рассчитатьToolStripMenuItem.Text = "Рассчитать";
      this.рассчитатьToolStripMenuItem.Click += new System.EventHandler(this.calculateToolStripMenuItem_Click);
      // 
      // очиститьToolStripMenuItem
      // 
      this.очиститьToolStripMenuItem.Name = "очиститьToolStripMenuItem";
      this.очиститьToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
      this.очиститьToolStripMenuItem.Text = "Очистить";
      this.очиститьToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.label1.Location = new System.Drawing.Point(69, 71);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(110, 25);
      this.label1.TabIndex = 1;
      this.label1.Text = "Точка A - ";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.label2.Location = new System.Drawing.Point(69, 139);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(110, 25);
      this.label2.TabIndex = 1;
      this.label2.Text = "Точка B - ";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.label3.Location = new System.Drawing.Point(56, 205);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(123, 25);
      this.label3.TabIndex = 1;
      this.label3.Text = "Точность - ";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.label4.Location = new System.Drawing.Point(117, 305);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(62, 25);
      this.label4.TabIndex = 1;
      this.label4.Text = "f(x) - ";
      // 
      // lblResult
      // 
      this.lblResult.AutoSize = true;
      this.lblResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.lblResult.Location = new System.Drawing.Point(70, 351);
      this.lblResult.Name = "lblResult";
      this.lblResult.Size = new System.Drawing.Size(25, 20);
      this.lblResult.TabIndex = 1;
      this.lblResult.Text = " - ";
      // 
      // txtA
      // 
      this.txtA.Location = new System.Drawing.Point(185, 77);
      this.txtA.Name = "txtA";
      this.txtA.Size = new System.Drawing.Size(175, 20);
      this.txtA.TabIndex = 2;
      // 
      // txtB
      // 
      this.txtB.Location = new System.Drawing.Point(185, 145);
      this.txtB.Name = "txtB";
      this.txtB.Size = new System.Drawing.Size(175, 20);
      this.txtB.TabIndex = 2;
      // 
      // txtFunc
      // 
      this.txtFunc.Location = new System.Drawing.Point(185, 311);
      this.txtFunc.Name = "txtFunc";
      this.txtFunc.Size = new System.Drawing.Size(175, 20);
      this.txtFunc.TabIndex = 2;
      // 
      // txtE
      // 
      this.txtE.Location = new System.Drawing.Point(185, 212);
      this.txtE.Name = "txtE";
      this.txtE.Size = new System.Drawing.Size(175, 20);
      this.txtE.TabIndex = 3;
      // 
      // chart1
      // 
      this.chart1.Location = new System.Drawing.Point(376, 71);
      this.chart1.Name = "chart1";
      series2.Name = "Series1";
      this.chart1.Series.Add(series2);
      this.chart1.Size = new System.Drawing.Size(401, 300);
      this.chart1.TabIndex = 4;
      this.chart1.Text = "chart1";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.ForeColor = System.Drawing.SystemColors.AppWorkspace;
      this.label5.Location = new System.Drawing.Point(59, 234);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(181, 13);
      this.label5.TabIndex = 5;
      this.label5.Text = "количество знаков после запятой";
      // 
      // Form3
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(800, 450);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.chart1);
      this.Controls.Add(this.txtE);
      this.Controls.Add(this.txtFunc);
      this.Controls.Add(this.txtB);
      this.Controls.Add(this.txtA);
      this.Controls.Add(this.lblResult);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.menuStrip1);
      this.MainMenuStrip = this.menuStrip1;
      this.Name = "Form3";
      this.Text = "Form3";
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.txtE)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label lblResult;
    private System.Windows.Forms.TextBox txtA;
    private System.Windows.Forms.TextBox txtB;
    private System.Windows.Forms.TextBox txtFunc;
    private System.Windows.Forms.NumericUpDown txtE;
    private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
    private System.Windows.Forms.ToolStripMenuItem управлениеToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem построитьГрафикToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem рассчитатьToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem очиститьToolStripMenuItem;
    private System.Windows.Forms.Label label5;
  }
}