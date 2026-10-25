using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
//(27 - 18*x + 2*Pow(x, 2))*Pow(2.7182, (-x/3))
namespace FistLab {
  public partial class Form3 : Form {
    private readonly MathSolver _solver = new MathSolver();

    // Храним последний корень, чтобы показать его на графике
    private double? _lastRootX = null;
    private double? _lastRootY = null;

    public Form3() {
      InitializeComponent();
      SetupChart();
    }

    // ============================================================
    // БЛОК 1: НАСТРОЙКА ГРАФИКА (при старте)
    // ============================================================
    private void SetupChart() {
      chart1.Series.Clear();
      chart1.ChartAreas.Clear();
      chart1.ChartAreas.Add("Area1");

      // Линия функции
      Series seriesFunc = new Series("Function") {
        ChartType = SeriesChartType.Line,
        BorderWidth = 3,
        Color = System.Drawing.Color.Blue
      };
      chart1.Series.Add(seriesFunc);

      // Точка корня
      Series seriesRoot = new Series("RootPoint") {
        ChartType = SeriesChartType.Point,
        MarkerSize = 12,
        MarkerStyle = MarkerStyle.Circle,
        Color = System.Drawing.Color.Red
      };
      chart1.Series.Add(seriesRoot);

      // Оси пересекаются в (0, 0)
      chart1.ChartAreas[0].AxisX.Crossing = 0;
      chart1.ChartAreas[0].AxisY.Crossing = 0;
    }

    // ============================================================
    // БЛОК 2: РАССЧИТАТЬ КОРЕНЬ (только считает, не рисует!)
    // ============================================================
    private void calculateToolStripMenuItem_Click(object sender, EventArgs e) {
      try {
        // Валидация
        if (!double.TryParse(txtA.Text, out double a))
          throw new Exception("Параметр 'a' должен быть числом.");
        if (!double.TryParse(txtB.Text, out double b))
          throw new Exception("Параметр 'b' должен быть числом.");
        if (a >= b)
          throw new Exception("Интервал задан неверно: 'a' должно быть меньше 'b'.");

        int precisionDigits = (int)txtE.Value;
        double eps = Math.Pow(10, -precisionDigits);

        string funcStr = txtFunc.Text;
        if (string.IsNullOrWhiteSpace(funcStr))
          throw new Exception("Введите формулу функции.");

        // Считаем
        var result = _solver.FindRoot(funcStr, a, b, eps);

        // Сохраняем для графика
        _lastRootX = result.Root;
        _lastRootY = result.RootValue;

        // Вывод
        string format = "F" + (precisionDigits);
        lblResult.Text =
            "Корень уравнения: x = " + result.Root.ToString(format) + "\n" +
            "f(x) = " + result.RootValue.ToString(format) + "\n" +
            "Итераций: " + result.Iterations;
      } catch (Exception ex) {
        MessageBox.Show(ex.Message, "Ошибка расчёта",
            MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }
    }

    // ============================================================
    // БЛОК 3: ПОСТРОИТЬ ГРАФИК (только рисует, не считает!)
    // ============================================================
    private void построитьГрафикToolStripMenuItem_Click(object sender, EventArgs e) {
      try {
        // Валидация
        if (!double.TryParse(txtA.Text, out double a))
          throw new Exception("Параметр 'a' должен быть числом.");
        if (!double.TryParse(txtB.Text, out double b))
          throw new Exception("Параметр 'b' должен быть числом.");
        if (a >= b)
          throw new Exception("Интервал задан неверно: 'a' должно быть меньше 'b'.");

        string funcStr = txtFunc.Text;
        if (string.IsNullOrWhiteSpace(funcStr))
          throw new Exception("Введите формулу функции.");

        // Рисуем
        PlotGraph(funcStr, a, b);
      } catch (Exception ex) {
        MessageBox.Show(ex.Message, "Ошибка отрисовки",
            MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }
    }

    // ============================================================
    // ОТРИСОВКА ГРАФИКА (вызывается из "Построить график")
    // ============================================================
  private void PlotGraph(string funcStr, double a, double b) {
    chart1.Series["Function"].Points.Clear();
    chart1.Series["RootPoint"].Points.Clear();

    // Расширяем диапазон
    double delta = (b - a) * 0.1; // Можно уменьшить, если нужно
    double minX = a - delta;
    double maxX = b + delta;
    if (Math.Abs(maxX - minX) < 0.1) { minX -= 1; maxX += 1; }

    int pointsCount = 5000; 
    double step = (maxX - minX) / pointsCount;

    // Ограничение по Y. Если функция выше/ниже — считаем это разрывом.
    const double yLimit = 10; // Уменьшил до 10, чтобы график был более "классическим"

    bool isInGap = false; // Флаг, что мы находимся в зоне разрыва

    for (int i = 0; i <= pointsCount; i++) {
        double x = minX + i * step;
        
        try {
            double y = _solver.CalculateFunction(funcStr, x);

            // Проверяем, является ли точка "нормальной"
            // (не NaN, не бесконечность и не выходит за наши рамки yLimit)
            bool isValid = !double.IsNaN(y) && !double.IsInfinity(y) && Math.Abs(y) <= yLimit;

            if (isValid) {
                // Если мы только что вышли из разрыва, добавляем пустую точку,
                // чтобы начать новую линию
                if (isInGap) {
                    chart1.Series["Function"].Points.AddXY(x, double.NaN);
                    isInGap = false;
                }
                
                // Добавляем нормальную точку
                chart1.Series["Function"].Points.AddXY(x, y);
            } 
            else {
                // Если точка невалидна (улетела в бесконечность или за yLimit)
                // и мы еще не в разрыве, добавляем пустую точку, чтобы оборвать линию
                if (!isInGap) {
                    chart1.Series["Function"].Points.AddXY(x, double.NaN);
                    // ДОБАВЛЯЕМ ВТОРУЮ ПУСТУЮ ТОЧКУ ДЛЯ НАДЕЖНОСТИ
                    chart1.Series["Function"].Points.AddXY(x, double.NaN);
                    isInGap = true;
                }
            }
        } 
        catch { 
            // При ошибке вычисления тоже ставим разрыв
            if (!isInGap) {
                chart1.Series["Function"].Points.AddXY(x, double.NaN);
                chart1.Series["Function"].Points.AddXY(x, double.NaN);
                isInGap = true;
            }
        }
    }

    // Рисуем корень, если он есть
    if (_lastRootX.HasValue && _lastRootY.HasValue) {
        if (Math.Abs(_lastRootY.Value) <= yLimit) {
            chart1.Series["RootPoint"].Points.AddXY(_lastRootX.Value, _lastRootY.Value);
        }
    }

    chart1.ChartAreas[0].RecalculateAxesScale();
}

    // ============================================================
    // БЛОК 4: ОЧИСТИТЬ
    // ============================================================
    private void clearToolStripMenuItem_Click(object sender, EventArgs e) {
      txtA.Clear();
      txtB.Clear();
      txtFunc.Clear();
      txtE.Value = 4;
      _lastRootX = null;
      _lastRootY = null;

      lblResult.Text = "Корень уравнения: ";
      chart1.Series["Function"].Points.Clear();
      chart1.Series["RootPoint"].Points.Clear();
    }
  }
}