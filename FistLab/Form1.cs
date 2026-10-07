using ClosedXML.Excel;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace FistLab {
  public partial class Form1 : Form {

    // ============================================================
    // ПОЛЯ КЛАССА
    // ============================================================

    // Флаг направления сортировки (true = по убыванию)
    private bool _sortDescending = false;

    // Источник отмены для всех параллельных сортировок
    private CancellationTokenSource _cts;

    // Флаг «идёт сортировка прямо сейчас»
    private volatile bool _isSorting = false;

    public Form1() {
      InitializeComponent();

      // Останавливаем все фоновые задачи при закрытии формы
      this.FormClosing += Form1_FormClosing;

      // Заглушка на графике до запуска
      chartVisualizer.Titles.Clear();
      chartVisualizer.Titles.Add("Нажмите «Сгенерировать», затем «Рассчитать»");
      chartVisualizer.Titles[0].Font = new Font("Segoe UI", 11, FontStyle.Italic);
      chartVisualizer.Titles[0].ForeColor = Color.Gray;
    }

    private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
      if (_isSorting) {
        _cts?.Cancel();
      }
    }

    // ============================================================
    // ВСПОМОГАТЕЛЬНЫЕ МЕТОДЫ
    // ============================================================

    /// <summary>
    /// Определяет, нужно ли менять местами два элемента с учётом направления сортировки.
    /// </summary>
    private bool NeedSwap(double a, double b) {
      return _sortDescending ? a < b : a > b;
    }

    /// <summary>
    /// Парсит строку в double, принимая и точку, и запятую как разделитель.
    /// </summary>
    private bool TryParseDouble(string text, out double result) {
      text = text.Trim().Replace(',', '.');
      return double.TryParse(text,
        NumberStyles.Any,
        CultureInfo.InvariantCulture,
        out result);
    }

    /// <summary>
    /// Заполняет DataGridView числами и синхронизирует textBox1.
    /// </summary>
    private void FillGridWithNumbers(List<double> numbers) {
      sortingShow.Rows.Clear();
      chartVisualizer.Series.Clear();
      sortingShow.AllowUserToAddRows = true;

      foreach (var num in numbers) {
        int rowIndex = sortingShow.Rows.Add();
        sortingShow.Rows[rowIndex].Cells[0].Value = num;
      }

      textBox1.Text = numbers.Count.ToString();

      if (sortingShow.Rows.Count >= 100) {
        sortingShow.AllowUserToAddRows = false;
      }
    }

    // ============================================================
    // ГЕНЕРАЦИЯ СЛУЧАЙНЫХ ЧИСЕЛ
    // ============================================================

    private void randomGeneric_Click(object sender, EventArgs e) {
      if (!int.TryParse(textBox1.Text, out int value) || value <= 0 || value > 100) {
        MessageBox.Show("Пожалуйста, введите корректное число элементов до 100 штук.",
          "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
      }

      sortingShow.Rows.Clear();
      Random random = new Random();

      for (int indexI = 0; indexI < value; ++indexI) {
        // От 1.00 до 100.00 с двумя знаками после запятой
        double randomNum = Math.Round(random.NextDouble() * 99.0 + 1.0, 2);
        int rowIndex = sortingShow.Rows.Add();
        sortingShow.Rows[rowIndex].Cells[0].Value = randomNum;
      }
    }

    private void sortingShow_CellContentClick(object sender, DataGridViewCellEventArgs e) {
      if (sortingShow.Rows.Count >= 100) {
        sortingShow.AllowUserToAddRows = false;
      }
    }

    // ============================================================
    // ИНИЦИАЛИЗАЦИЯ ГРАФИКА
    // ============================================================

    private void InitializeChartForSelectedAlgorithms(double[] baseDataArray) {
      chartVisualizer.Titles.Clear();
      chartVisualizer.Series.Clear();

      var algorithmSettings = new[]
      {
        new { CheckBox = checkBoxBubble,    Name = "Пузырьковая", Color = Color.Coral },
        new { CheckBox = checkBoxInsertion, Name = "Вставками",   Color = Color.DeepSkyBlue },
        new { CheckBox = checkBoxShaker,    Name = "Шейкерная",   Color = Color.LimeGreen },
        new { CheckBox = checkBoxQuick,     Name = "Быстрая",     Color = Color.Orchid },
        new { CheckBox = checkBoxBogo,      Name = "BOGO Sort",   Color = Color.Crimson }
      };

      foreach (var setting in algorithmSettings) {
        if (setting.CheckBox.Checked) {
          Series newSeries = new Series(setting.Name);
          newSeries.ChartType = SeriesChartType.Column;
          newSeries.Color = setting.Color;

          for (int index = 0; index < baseDataArray.Length; index++) {
            newSeries.Points.AddY(baseDataArray[index]);
          }

          chartVisualizer.Series.Add(newSeries);
        }
      }
    }

    private void UpdateChartPoint(string seriesName, int pointIndex, double newValue) {
      // Если форма уже уничтожается/уничтожена — молча выходим
      if (this.IsDisposed || this.Disposing) return;

      try {
        if (this.InvokeRequired) {
          this.Invoke((MethodInvoker)delegate { UpdateChartPoint(seriesName, pointIndex, newValue); });
          return;
        }

        if (chartVisualizer.IsDisposed) return;

        var series = chartVisualizer.Series.FindByName(seriesName);
        if (series != null && pointIndex >= 0 && pointIndex < series.Points.Count) {
          series.Points[pointIndex].SetValueY(newValue);
          chartVisualizer.Refresh();
        }
      } catch (ObjectDisposedException) {
        // Форма закрылась во время обновления — игнорируем
      } catch (InvalidOperationException) {
        // Контрол уже удалён — игнорируем
      }
    }

    // ============================================================
    // АЛГОРИТМЫ СОРТИРОВКИ С ВИЗУАЛИЗАЦИЕЙ
    // ============================================================

    public void VisualBubbleSort(double[] dataArray, string seriesName,
                                 CancellationToken ct, Action onIteration) {
      int arrayLength = dataArray.Length;

      for (int currentPass = 0; currentPass < arrayLength - 1; currentPass++) {
        for (int elementIndex = 0; elementIndex < arrayLength - currentPass - 1; elementIndex++) {
          ct.ThrowIfCancellationRequested();
          onIteration();

          if (NeedSwap(dataArray[elementIndex], dataArray[elementIndex + 1])) {
            double temporaryValue = dataArray[elementIndex];
            dataArray[elementIndex] = dataArray[elementIndex + 1];
            dataArray[elementIndex + 1] = temporaryValue;

            UpdateChartPoint(seriesName, elementIndex, dataArray[elementIndex]);
            UpdateChartPoint(seriesName, elementIndex + 1, dataArray[elementIndex + 1]);
            Thread.Sleep(50);
          }
        }
      }
    }

    public void VisualInsertionSort(double[] dataArray, string seriesName,
                                    int sleepTimeout, CancellationToken ct, Action onIteration) {
      int arrayLength = dataArray.Length;

      for (int currentStep = 1; currentStep < arrayLength; currentStep++) {
        ct.ThrowIfCancellationRequested();

        double keyElement = dataArray[currentStep];
        int previousIndex = currentStep - 1;

        while (previousIndex >= 0) {
          ct.ThrowIfCancellationRequested();
          onIteration();

          if (!NeedSwap(dataArray[previousIndex], keyElement)) break;

          dataArray[previousIndex + 1] = dataArray[previousIndex];
          UpdateChartPoint(seriesName, previousIndex + 1, dataArray[previousIndex + 1]);
          Thread.Sleep(sleepTimeout);
          previousIndex = previousIndex - 1;
        }

        dataArray[previousIndex + 1] = keyElement;
        UpdateChartPoint(seriesName, previousIndex + 1, dataArray[previousIndex + 1]);
        Thread.Sleep(sleepTimeout);
      }
    }

    public void VisualShakerSort(double[] dataArray, string seriesName,
                                 int sleepTimeout, CancellationToken ct, Action onIteration) {
      int leftBoundary = 0;
      int rightBoundary = dataArray.Length - 1;

      while (leftBoundary <= rightBoundary) {
        ct.ThrowIfCancellationRequested();

        for (int scanIndex = leftBoundary; scanIndex < rightBoundary; scanIndex++) {
          ct.ThrowIfCancellationRequested();
          onIteration();

          if (NeedSwap(dataArray[scanIndex], dataArray[scanIndex + 1])) {
            double temporaryValue = dataArray[scanIndex];
            dataArray[scanIndex] = dataArray[scanIndex + 1];
            dataArray[scanIndex + 1] = temporaryValue;

            UpdateChartPoint(seriesName, scanIndex, dataArray[scanIndex]);
            UpdateChartPoint(seriesName, scanIndex + 1, dataArray[scanIndex + 1]);
            Thread.Sleep(sleepTimeout);
          }
        }
        rightBoundary--;

        for (int scanIndex = rightBoundary; scanIndex > leftBoundary; scanIndex--) {
          ct.ThrowIfCancellationRequested();
          onIteration();

          if (NeedSwap(dataArray[scanIndex - 1], dataArray[scanIndex])) {
            double temporaryValue = dataArray[scanIndex];
            dataArray[scanIndex] = dataArray[scanIndex - 1];
            dataArray[scanIndex - 1] = temporaryValue;

            UpdateChartPoint(seriesName, scanIndex, dataArray[scanIndex]);
            UpdateChartPoint(seriesName, scanIndex - 1, dataArray[scanIndex - 1]);
            Thread.Sleep(sleepTimeout);
          }
        }
        leftBoundary++;
      }
    }

    public void VisualQuickSort(double[] dataArray, int startingIndex, int endingIndex,
                                string seriesName, int sleepTimeout,
                                CancellationToken ct, Action onIteration) {
      ct.ThrowIfCancellationRequested();

      if (startingIndex < endingIndex) {
        int pivotIndex = PartitionArrayVisual(dataArray, startingIndex, endingIndex,
                                              seriesName, sleepTimeout, ct, onIteration);

        VisualQuickSort(dataArray, startingIndex, pivotIndex - 1, seriesName, sleepTimeout, ct, onIteration);
        VisualQuickSort(dataArray, pivotIndex + 1, endingIndex, seriesName, sleepTimeout, ct, onIteration);
      }
    }

    private int PartitionArrayVisual(double[] dataArray, int startingIndex, int endingIndex,
                                     string seriesName, int sleepTimeout,
                                     CancellationToken ct, Action onIteration) {
      double pivotValue = dataArray[endingIndex];
      int smallerElementIndex = startingIndex - 1;

      for (int currentIndex = startingIndex; currentIndex < endingIndex; currentIndex++) {
        ct.ThrowIfCancellationRequested();
        onIteration();

        bool shouldMoveLeft = _sortDescending
          ? dataArray[currentIndex] > pivotValue
          : dataArray[currentIndex] < pivotValue;

        if (shouldMoveLeft) {
          smallerElementIndex++;
          double temporaryValue = dataArray[smallerElementIndex];
          dataArray[smallerElementIndex] = dataArray[currentIndex];
          dataArray[currentIndex] = temporaryValue;

          UpdateChartPoint(seriesName, smallerElementIndex, dataArray[smallerElementIndex]);
          UpdateChartPoint(seriesName, currentIndex, dataArray[currentIndex]);
          Thread.Sleep(sleepTimeout);
        }
      }

      int finalPivotPlaceIndex = smallerElementIndex + 1;
      double temporaryPivotValue = dataArray[finalPivotPlaceIndex];
      dataArray[finalPivotPlaceIndex] = dataArray[endingIndex];
      dataArray[endingIndex] = temporaryPivotValue;

      UpdateChartPoint(seriesName, finalPivotPlaceIndex, dataArray[finalPivotPlaceIndex]);
      UpdateChartPoint(seriesName, endingIndex, dataArray[endingIndex]);
      Thread.Sleep(sleepTimeout);

      return finalPivotPlaceIndex;
    }

    public void VisualBogoSort(double[] dataArray, string seriesName,
                               int sleepTimeout, CancellationToken ct, Action onIteration) {
      Random randomGenerator = new Random();

      Stopwatch bogoTimer = Stopwatch.StartNew();
      long timeLimitMilliseconds = 5000;

      while (!CheckIfArrayIsSorted(dataArray)) {
        ct.ThrowIfCancellationRequested();
        onIteration();

        if (bogoTimer.ElapsedMilliseconds >= timeLimitMilliseconds) {
          bogoTimer.Stop();

          this.Invoke((MethodInvoker)delegate {
            MessageBox.Show(
              $"Время выполнения BOGO Sort превысило лимит в {timeLimitMilliseconds / 1000} секунд! " +
              "Алгоритм был принудительно остановлен.",
              "Превышен лимит времени", MessageBoxButtons.OK, MessageBoxIcon.Warning);
          });

          return;
        }

        for (int currentIndex = dataArray.Length - 1; currentIndex > 0; currentIndex--) {
          int randomIndex = randomGenerator.Next(currentIndex + 1);

          double temporaryValue = dataArray[currentIndex];
          dataArray[currentIndex] = dataArray[randomIndex];
          dataArray[randomIndex] = temporaryValue;
        }

        for (int pointIndex = 0; pointIndex < dataArray.Length; pointIndex++) {
          UpdateChartPoint(seriesName, pointIndex, dataArray[pointIndex]);
        }

        Thread.Sleep(sleepTimeout);
      }

      bogoTimer.Stop();
    }

    /// <summary>
    /// Проверяет корректность порядка элементов с учётом направления сортировки.
    /// </summary>
    private bool CheckIfArrayIsSorted(double[] dataArray) {
      for (int currentIndex = 0; currentIndex < dataArray.Length - 1; currentIndex++) {
        if (NeedSwap(dataArray[currentIndex], dataArray[currentIndex + 1])) {
          return false;
        }
      }
      return true;
    }

    // ============================================================
    // ПОЛУЧЕНИЕ МАССИВА ИЗ ТАБЛИЦЫ
    // ============================================================

    private double[] GetArrayFromGrid() {
      if (sortingShow.Rows.Count == 0) {
        return new double[0];
      }

      int rowsCount = sortingShow.AllowUserToAddRows
        ? sortingShow.Rows.Count - 1
        : sortingShow.Rows.Count;

      if (rowsCount <= 0) {
        return new double[0];
      }

      List<double> validNumbersList = new List<double>();

      for (int index = 0; index < rowsCount; index++) {
        object cellValue = sortingShow.Rows[index].Cells[0].Value;

        if (cellValue == null || string.IsNullOrWhiteSpace(cellValue.ToString())) {
          MessageBox.Show(
            $"Строка №{index + 1} пуста. Введите число (например, 5 или 3,14) или удалите строку.",
            "Ошибка данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return null;
        }

        if (!TryParseDouble(cellValue.ToString(), out double validNumber)) {
          MessageBox.Show(
            $"В строке №{index + 1} значение '{cellValue}' не является числом.\n" +
            "Допускаются целые и дробные числа (5, 3.14, -2.5).",
            "Ошибка данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return null;
        }

        validNumbersList.Add(validNumber);
      }

      return validNumbersList.ToArray();
    }

    // ============================================================
    // МЕНЮ: ОЧИСТКА
    // ============================================================

    private void очиститьToolStripMenuItem_Click(object sender, EventArgs e) {
      sortingShow.Rows.Clear();
      chartVisualizer.Series.Clear();
      textBox1.Clear();
      sortingShow.AllowUserToAddRows = true;

      // Возвращаем заглушку на график
      chartVisualizer.Titles.Clear();
      chartVisualizer.Titles.Add("Нажмите «Сгенерировать», затем «Рассчитать»");
      chartVisualizer.Titles[0].Font = new Font("Segoe UI", 11, FontStyle.Italic);
      chartVisualizer.Titles[0].ForeColor = Color.Gray;

      MessageBox.Show("Все данные и графики успешно очищены!", "Сброс",
        MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    // ============================================================
    // ИМПОРТ ИЗ EXCEL
    // ============================================================

    private void importFromExcelToolStripMenuItem_Click(object sender, EventArgs e) {
      using (OpenFileDialog ofd = new OpenFileDialog()) {
        ofd.Filter = "Excel файлы (*.xlsx;*.xlsm)|*.xlsx;*.xlsm|Все файлы (*.*)|*.*";
        ofd.Title = "Выберите Excel файл";

        if (ofd.ShowDialog() != DialogResult.OK) return;

        try {
          List<double> numbers = new List<double>();

          using (var workbook = new XLWorkbook(ofd.FileName)) {
            var worksheet = workbook.Worksheets.First();
            var usedRange = worksheet.RangeUsed();

            if (usedRange == null) {
              MessageBox.Show("Файл пуст — ни одной заполненной ячейки.",
                "Пустой файл", MessageBoxButtons.OK, MessageBoxIcon.Warning);
              return;
            }

            foreach (var row in usedRange.RowsUsed()) {
              foreach (var cell in row.CellsUsed()) {
                if (cell.TryGetValue<double>(out double dbl)) {
                  numbers.Add(dbl);
                } else {
                  string text = cell.GetString().Trim();

                  if (string.IsNullOrWhiteSpace(text)) continue;

                  if (!TryParseDouble(text, out double parsed)) {
                    MessageBox.Show(
                      $"Ячейка {cell.Address} содержит недопустимое значение: '{text}'.\n" +
                      "Допускаются только числа (целые или дробные).",
                      "Ошибка данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                  }
                  numbers.Add(parsed);
                }
              }
            }
          }

          if (numbers.Count == 0) {
            MessageBox.Show("В файле не найдено ни одного числа.",
              "Пустой массив", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
          }

          if (numbers.Count > 100) {
            MessageBox.Show($"Найдено {numbers.Count} чисел. Будут загружены первые 100.",
              "Ограничение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            numbers = numbers.Take(100).ToList();
          }

          FillGridWithNumbers(numbers);

          MessageBox.Show($"Успешно загружено {numbers.Count} чисел из Excel.",
            "Импорт завершён", MessageBoxButtons.OK, MessageBoxIcon.Information);

        } catch (Exception ex) {
          MessageBox.Show($"Ошибка чтения Excel файла: {ex.Message}",
            "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      }
    }

    // ============================================================
    // ИМПОРТ ИЗ GOOGLE SHEETS
    // ============================================================

    private async void importFromGoogleSheetsToolStripMenuItem_Click(object sender, EventArgs e) {
      string url = Prompt.ShowDialog(
        "Вставьте ссылку на Google Таблицу (или прямую CSV-ссылку):",
        "Импорт из Google Sheets");

      if (string.IsNullOrWhiteSpace(url)) return;

      string csvUrl;
      try {
        csvUrl = ConvertToCsvExportUrl(url);
      } catch (Exception ex) {
        MessageBox.Show(ex.Message, "Ошибка в ссылке",
          MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      try {
        this.Cursor = Cursors.WaitCursor;

        using (HttpClient client = new HttpClient()) {
          client.Timeout = TimeSpan.FromSeconds(20);
          string csv = await client.GetStringAsync(csvUrl);

          List<double> numbers;
          try {
            numbers = ParseCsvAllFields(csv);
          } catch (FormatException fex) {
            MessageBox.Show(fex.Message, "Ошибка данных",
              MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
          }

          if (numbers.Count == 0) {
            MessageBox.Show("В таблице не найдено ни одного числа.\n\n" +
              "Проверьте, что таблица опубликована: Файл → Поделиться → Доступ по ссылке.",
              "Пустой массив", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
          }

          if (numbers.Count > 100) {
            MessageBox.Show($"Найдено {numbers.Count} чисел. Будут загружены первые 100.",
              "Ограничение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            numbers = numbers.Take(100).ToList();
          }

          FillGridWithNumbers(numbers);

          MessageBox.Show($"Успешно загружено {numbers.Count} чисел из Google Sheets.",
            "Импорт завершён", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
      } catch (Exception ex) {
        MessageBox.Show(
          $"Ошибка загрузки: {ex.Message}\n\nПроверьте, что таблица доступна по ссылке.",
          "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
      } finally {
        this.Cursor = Cursors.Default;
      }
    }

    /// <summary>
    /// Конвертирует обычную ссылку Google Sheets в ссылку CSV-экспорта.
    /// </summary>
    private string ConvertToCsvExportUrl(string url) {
      if (url.Contains("export?format=csv") ||
          url.Contains("output=csv") ||
          url.EndsWith(".csv"))
        return url;

      var match = Regex.Match(url, @"/spreadsheets/d/([a-zA-Z0-9-_]+)");
      if (!match.Success)
        throw new ArgumentException(
          "Не удалось распознать ID таблицы в ссылке. Убедитесь, что ссылка ведёт на Google Sheets.");

      string id = match.Groups[1].Value;

      string gid = "0";
      var gidMatch = Regex.Match(url, @"[#&?]gid=(\d+)");
      if (gidMatch.Success) gid = gidMatch.Groups[1].Value;

      return $"https://docs.google.com/spreadsheets/d/{id}/export?format=csv&gid={gid}";
    }

    /// <summary>
    /// Парсит CSV целиком (все строки × все столбцы) и возвращает все числа.
    /// Бросает FormatException, если встретит нечисловое значение.
    /// </summary>
    private List<double> ParseCsvAllFields(string csv) {
      List<double> result = new List<double>();
      int lineNumber = 0;

      using (var reader = new StringReader(csv)) {
        string line;
        while ((line = reader.ReadLine()) != null) {
          lineNumber++;
          if (string.IsNullOrWhiteSpace(line)) continue;

          var fields = new List<string>();
          bool inQuotes = false;
          var current = new System.Text.StringBuilder();

          for (int i = 0; i < line.Length; i++) {
            char c = line[i];

            if (c == '"') {
              if (inQuotes && i + 1 < line.Length && line[i + 1] == '"') {
                current.Append('"');
                i++;
              } else {
                inQuotes = !inQuotes;
              }
            } else if (c == ',' && !inQuotes) {
              fields.Add(current.ToString());
              current.Clear();
            } else {
              current.Append(c);
            }
          }
          fields.Add(current.ToString());

          foreach (var field in fields) {
            string trimmed = field.Trim();

            if (string.IsNullOrWhiteSpace(trimmed)) continue;

            if (TryParseDouble(trimmed, out double num)) {
              result.Add(num);
            } else {
              throw new FormatException(
                $"Строка {lineNumber}: '{trimmed}' не является числом.\n" +
                "Допускаются целые и дробные числа.");
            }
          }
        }
      }

      return result;
    }

    // ============================================================
    // КНОПКА ПРИНУДИТЕЛЬНОЙ ОСТАНОВКИ
    // ============================================================

    private void stopButton_Click(object sender, EventArgs e) {
      if (_cts != null && !_cts.IsCancellationRequested) {
        _cts.Cancel();
        stopButton.Enabled = false;
      }
    }

    // ============================================================
    // ГЛАВНЫЙ ЗАПУСК СОРТИРОВОК
    // ============================================================

    private async void рассчитатьToolStripMenuItem_Click(object sender, EventArgs e) {
      // Нельзя запускать, пока предыдущая сортировка идёт
      if (_isSorting) {
        MessageBox.Show("Сортировка уже выполняется. Нажмите «Стоп» или дождитесь окончания.",
          "Идёт сортировка", MessageBoxButtons.OK, MessageBoxIcon.Information);
        return;
      }

      double[] originalArray = GetArrayFromGrid();
      if (originalArray == null) return;

      if (originalArray.Length == 0) {
        MessageBox.Show("Таблица пуста! Сначала сгенерируйте или введите данные вручную.",
          "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
      }

      int selectedAlgorithmsCount = 0;
      if (checkBoxBubble.Checked) ++selectedAlgorithmsCount;
      if (checkBoxInsertion.Checked) ++selectedAlgorithmsCount;
      if (checkBoxShaker.Checked) ++selectedAlgorithmsCount;
      if (checkBoxQuick.Checked) ++selectedAlgorithmsCount;
      if (checkBoxBogo.Checked) ++selectedAlgorithmsCount;

      if (selectedAlgorithmsCount == 0) {
        MessageBox.Show("Пожалуйста, выберите хотя бы один алгоритм сортировки!",
          "Алгоритмы не выбраны", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
      }

      if (checkBoxBogo.Checked && originalArray.Length > 10) {
        DialogResult dialogResult = MessageBox.Show(
          $"Вы выбрали BOGO Sort для {originalArray.Length} элементов. " +
          "Этот алгоритм работает случайным образом и может вычисляться годами. " +
          "Вы уверены, что хотите продолжить?",
          "Опасность зависания",
          MessageBoxButtons.YesNo,
          MessageBoxIcon.Error);

        if (dialogResult == DialogResult.No) return;
      }

      // Направление сортировки
      _sortDescending = checkBoxReverse.Checked;

      // Готовим токен отмены
      _cts?.Dispose();
      _cts = new CancellationTokenSource();
      var token = _cts.Token;

      // Блокируем интерфейс, включаем «Стоп»
      _isSorting = true;
      sortingShow.Enabled = false;
      textBox1.Enabled = false;
      randomGeneric.Enabled = false;
      stopButton.Enabled = true;

      InitializeChartForSelectedAlgorithms(originalArray);

      int animationSpeedTimeout = 50;

      List<Task> sortingTasks = new List<Task>();
      var executionTimesReport = new ConcurrentDictionary<string, double>();
      var iterationReport = new ConcurrentDictionary<string, long>();

      // --- ЗАПУСК ВСЕХ ВЫБРАННЫХ СОРТИРОВОК ---

      if (checkBoxBubble.Checked) {
        double[] arr = (double[])originalArray.Clone();
        long iterations = 0;
        sortingTasks.Add(Task.Run(() => {
          var start = Process.GetCurrentProcess().TotalProcessorTime;
          try {
            VisualBubbleSort(arr, "Пузырьковая", token, () => iterations++);
          } catch (OperationCanceledException) { }
          var end = Process.GetCurrentProcess().TotalProcessorTime;
          executionTimesReport["Пузырьковая"] = (end - start).TotalMilliseconds;
          iterationReport["Пузырьковая"] = iterations;
        }, token));
      }

      if (checkBoxInsertion.Checked) {
        double[] arr = (double[])originalArray.Clone();
        long iterations = 0;
        sortingTasks.Add(Task.Run(() => {
          var start = Process.GetCurrentProcess().TotalProcessorTime;
          try {
            VisualInsertionSort(arr, "Вставками", animationSpeedTimeout, token, () => iterations++);
          } catch (OperationCanceledException) { }
          var end = Process.GetCurrentProcess().TotalProcessorTime;
          executionTimesReport["Вставками"] = (end - start).TotalMilliseconds;
          iterationReport["Вставками"] = iterations;
        }, token));
      }

      if (checkBoxShaker.Checked) {
        double[] arr = (double[])originalArray.Clone();
        long iterations = 0;
        sortingTasks.Add(Task.Run(() => {
          var start = Process.GetCurrentProcess().TotalProcessorTime;
          try {
            VisualShakerSort(arr, "Шейкерная", animationSpeedTimeout, token, () => iterations++);
          } catch (OperationCanceledException) { }
          var end = Process.GetCurrentProcess().TotalProcessorTime;
          executionTimesReport["Шейкерная"] = (end - start).TotalMilliseconds;
          iterationReport["Шейкерная"] = iterations;
        }, token));
      }

      if (checkBoxQuick.Checked) {
        double[] arr = (double[])originalArray.Clone();
        long iterations = 0;
        sortingTasks.Add(Task.Run(() => {
          var start = Process.GetCurrentProcess().TotalProcessorTime;
          try {
            VisualQuickSort(arr, 0, arr.Length - 1, "Быстрая", animationSpeedTimeout, token, () => iterations++);
          } catch (OperationCanceledException) { }
          var end = Process.GetCurrentProcess().TotalProcessorTime;
          executionTimesReport["Быстрая"] = (end - start).TotalMilliseconds;
          iterationReport["Быстрая"] = iterations;
        }, token));
      }

      if (checkBoxBogo.Checked) {
        double[] arr = (double[])originalArray.Clone();
        long iterations = 0;
        sortingTasks.Add(Task.Run(() => {
          var start = Process.GetCurrentProcess().TotalProcessorTime;
          try {
            VisualBogoSort(arr, "BOGO Sort", animationSpeedTimeout, token, () => iterations++);
          } catch (OperationCanceledException) { }
          var end = Process.GetCurrentProcess().TotalProcessorTime;
          executionTimesReport["BOGO Sort"] = (end - start).TotalMilliseconds;
          iterationReport["BOGO Sort"] = iterations;
        }, token));
      }

      // Ждём завершения всех задач (или отмены)
      try {
        await Task.WhenAll(sortingTasks);
      } catch (OperationCanceledException) {
        // Отменили — это нормально
      }

      // Возвращаем интерфейс
      _isSorting = false;
      sortingShow.Enabled = true;
      textBox1.Enabled = true;
      randomGeneric.Enabled = true;
      stopButton.Enabled = false;

      // --- ЗАПОЛНЕНИЕ ТАБЛИЦЫ РЕЗУЛЬТАТОВ ---
      dataGridViewResults.Rows.Clear();

      foreach (var record in executionTimesReport) {
        int rowIndex = dataGridViewResults.Rows.Add();

        dataGridViewResults.Rows[rowIndex].Cells[0].Value = record.Key;
        dataGridViewResults.Rows[rowIndex].Cells[1].Value = record.Value.ToString("F2");

        if (iterationReport.TryGetValue(record.Key, out long iters)) {
          dataGridViewResults.Rows[rowIndex].Cells[2].Value = iters.ToString("N0");
        } else {
          dataGridViewResults.Rows[rowIndex].Cells[2].Value = "—";
        }
      }

      // Сортируем по времени
      if (dataGridViewResults.Rows.Count > 0) {
        dataGridViewResults.Sort(dataGridViewResults.Columns[1],
          System.ComponentModel.ListSortDirection.Ascending);

        // Красим лучший результат
        dataGridViewResults.Rows[0].DefaultCellStyle.BackColor = Color.LightGreen;
      }
    }

    private void dataGridViewResults_CellContentClick(object sender, DataGridViewCellEventArgs e) {
      // Пусто
    }

    private void sortingShow_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e) {
      // Если строк (без последней «новой») стало больше 100 — обрезаем и запрещаем добавлять
      int realRowsCount = sortingShow.AllowUserToAddRows
        ? sortingShow.Rows.Count - 1
        : sortingShow.Rows.Count;

      if (realRowsCount >= 100) {
        sortingShow.AllowUserToAddRows = false;

        // Если пользователь умудрился добавить больше — удалим лишние
        if (realRowsCount > 100) {
          while (sortingShow.Rows.Count > 100) {
            sortingShow.Rows.RemoveAt(sortingShow.Rows.Count - 1);
          }
          MessageBox.Show("Достигнут максимум — 100 элементов.",
            "Ограничение", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
      }
    }
  }
}