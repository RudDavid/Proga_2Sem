using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml.XPath;

namespace FistLab {
  public partial class Form1 : Form {

    // Флаг направления сортировки (true = по убыванию)
    private bool _sortDescending = false;

    public Form1() {
      InitializeComponent();
    }

    // ============================================================
    // ВСПОМОГАТЕЛЬНЫЕ МЕТОДЫ
    // ============================================================

    /// <summary>
    /// Определяет, нужно ли менять местами два элемента с учётом направления сортировки.
    /// </summary>
    private bool NeedSwap(int a, int b) {
      return _sortDescending ? a < b : a > b;
    }

    /// <summary>
    /// Заполняет DataGridView числами и синхронизирует textBox1.
    /// </summary>
    private void FillGridWithNumbers(List<int> numbers) {
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

    private void randomGeneric_Click(object sender, System.EventArgs e) {
      if (!int.TryParse(textBox1.Text, out int value) || value <= 0 || value > 100) {
        MessageBox.Show("Пожалуйста, введите корректное число элементов до 100 штук.",
          "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
      }

      sortingShow.Rows.Clear();

      Random random = new Random();

      for (int indexI = 0; indexI < value; ++indexI) {
        int randomNum = random.Next(1, 101);
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

    private void InitializeChartForSelectedAlgorithms(int[] baseDataArray) {
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

    private void UpdateChartPoint(string seriesName, int pointIndex, int newValue) {
      if (this.InvokeRequired) {
        this.Invoke((MethodInvoker)delegate { UpdateChartPoint(seriesName, pointIndex, newValue); });
        return;
      }

      if (chartVisualizer.Series.FindByName(seriesName) != null) {
        chartVisualizer.Series[seriesName].Points[pointIndex].SetValueY(newValue);
        chartVisualizer.Refresh();
      }
    }

    // ============================================================
    // АЛГОРИТМЫ СОРТИРОВКИ С ВИЗУАЛИЗАЦИЕЙ
    // ============================================================

    public void VisualBubbleSort(int[] dataArray, string seriesName) {
      int arrayLength = dataArray.Length;

      for (int currentPass = 0; currentPass < arrayLength - 1; currentPass++) {
        for (int elementIndex = 0; elementIndex < arrayLength - currentPass - 1; elementIndex++) {
          if (NeedSwap(dataArray[elementIndex], dataArray[elementIndex + 1])) {
            int temporaryValue = dataArray[elementIndex];
            dataArray[elementIndex] = dataArray[elementIndex + 1];
            dataArray[elementIndex + 1] = temporaryValue;

            UpdateChartPoint(seriesName, elementIndex, dataArray[elementIndex]);
            UpdateChartPoint(seriesName, elementIndex + 1, dataArray[elementIndex + 1]);
            Thread.Sleep(50);
          }
        }
      }
    }

    public void VisualInsertionSort(int[] dataArray, string seriesName, int sleepTimeout) {
      int arrayLength = dataArray.Length;

      for (int currentStep = 1; currentStep < arrayLength; currentStep++) {
        int keyElement = dataArray[currentStep];
        int previousIndex = currentStep - 1;

        while (previousIndex >= 0 && NeedSwap(dataArray[previousIndex], keyElement)) {
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

    public void VisualShakerSort(int[] dataArray, string seriesName, int sleepTimeout) {
      int leftBoundary = 0;
      int rightBoundary = dataArray.Length - 1;

      while (leftBoundary <= rightBoundary) {
        for (int scanIndex = leftBoundary; scanIndex < rightBoundary; scanIndex++) {
          if (NeedSwap(dataArray[scanIndex], dataArray[scanIndex + 1])) {
            int temporaryValue = dataArray[scanIndex];
            dataArray[scanIndex] = dataArray[scanIndex + 1];
            dataArray[scanIndex + 1] = temporaryValue;

            UpdateChartPoint(seriesName, scanIndex, dataArray[scanIndex]);
            UpdateChartPoint(seriesName, scanIndex + 1, dataArray[scanIndex + 1]);
            Thread.Sleep(sleepTimeout);
          }
        }
        rightBoundary--;

        for (int scanIndex = rightBoundary; scanIndex > leftBoundary; scanIndex--) {
          if (NeedSwap(dataArray[scanIndex - 1], dataArray[scanIndex])) {
            int temporaryValue = dataArray[scanIndex];
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

    public void VisualQuickSort(int[] dataArray, int startingIndex, int endingIndex, string seriesName, int sleepTimeout) {
      if (startingIndex < endingIndex) {
        int pivotIndex = PartitionArrayVisual(dataArray, startingIndex, endingIndex, seriesName, sleepTimeout);

        VisualQuickSort(dataArray, startingIndex, pivotIndex - 1, seriesName, sleepTimeout);
        VisualQuickSort(dataArray, pivotIndex + 1, endingIndex, seriesName, sleepTimeout);
      }
    }

    private int PartitionArrayVisual(int[] dataArray, int startingIndex, int endingIndex, string seriesName, int sleepTimeout) {
      int pivotValue = dataArray[endingIndex];
      int smallerElementIndex = startingIndex - 1;

      for (int currentIndex = startingIndex; currentIndex < endingIndex; currentIndex++) {
        // Для сортировки по убыванию элементы "больше" опорного идут влево
        bool shouldMoveLeft = _sortDescending
          ? dataArray[currentIndex] > pivotValue
          : dataArray[currentIndex] < pivotValue;

        if (shouldMoveLeft) {
          smallerElementIndex++;
          int temporaryValue = dataArray[smallerElementIndex];
          dataArray[smallerElementIndex] = dataArray[currentIndex];
          dataArray[currentIndex] = temporaryValue;

          UpdateChartPoint(seriesName, smallerElementIndex, dataArray[smallerElementIndex]);
          UpdateChartPoint(seriesName, currentIndex, dataArray[currentIndex]);
          Thread.Sleep(sleepTimeout);
        }
      }

      int finalPivotPlaceIndex = smallerElementIndex + 1;
      int temporaryPivotValue = dataArray[finalPivotPlaceIndex];
      dataArray[finalPivotPlaceIndex] = dataArray[endingIndex];
      dataArray[endingIndex] = temporaryPivotValue;

      UpdateChartPoint(seriesName, finalPivotPlaceIndex, dataArray[finalPivotPlaceIndex]);
      UpdateChartPoint(seriesName, endingIndex, dataArray[endingIndex]);
      Thread.Sleep(sleepTimeout);

      return finalPivotPlaceIndex;
    }

    public void VisualBogoSort(int[] dataArray, string seriesName, int sleepTimeout) {
      Random randomGenerator = new Random();

      Stopwatch bogoTimer = Stopwatch.StartNew();
      long timeLimitMilliseconds = 5000;

      while (!CheckIfArrayIsSorted(dataArray)) {
        if (bogoTimer.ElapsedMilliseconds >= timeLimitMilliseconds) {
          bogoTimer.Stop();

          this.Invoke((MethodInvoker)delegate {
            MessageBox.Show(
              $"Время выполнения BOGO Sort превысило лимит в {timeLimitMilliseconds / 1000} секунд! Алгоритм был принудительно остановлен.",
              "Превышен лимит времени", MessageBoxButtons.OK, MessageBoxIcon.Warning);
          });

          return;
        }

        for (int currentIndex = dataArray.Length - 1; currentIndex > 0; currentIndex--) {
          int randomIndex = randomGenerator.Next(currentIndex + 1);

          int temporaryValue = dataArray[currentIndex];
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
    private bool CheckIfArrayIsSorted(int[] dataArray) {
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

    private int[] GetArrayFromGrid() {
      if (sortingShow.Rows.Count == 0) {
        return new int[0];
      }

      int rowsCount = sortingShow.AllowUserToAddRows
        ? sortingShow.Rows.Count - 1
        : sortingShow.Rows.Count;

      if (rowsCount <= 0) {
        return new int[0];
      }

      List<int> validNumbersList = new List<int>();

      for (int index = 0; index < rowsCount; index++) {
        object cellValue = sortingShow.Rows[index].Cells[0].Value;

        if (cellValue == null || string.IsNullOrWhiteSpace(cellValue.ToString())) {
          MessageBox.Show(
            $"Строка №{index + 1} пуста или содержит некорректные данные. Пожалуйста, заполните её целым числом или удалите строку.",
            "Ошибка валидации данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return null;
        }

        if (!int.TryParse(cellValue.ToString().Trim(), out int validNumber)) {
          MessageBox.Show(
            $"В строке №{index + 1} обнаружено недопустимое значение '{cellValue}'. Допускаются только целые числа!",
            "Ошибка валидации данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
          List<int> numbers = new List<int>();

          using (var workbook = new XLWorkbook(ofd.FileName)) {
            var worksheet = workbook.Worksheets.First();
            var usedRange = worksheet.RangeUsed();

            // --- ПРОВЕРКА 1: файл/лист пустой ---
            if (usedRange == null) {
              MessageBox.Show("Файл пуст — ни одной заполненной ячейки.",
                "Пустой файл", MessageBoxButtons.OK, MessageBoxIcon.Warning);
              return;
            }

            // Проходим по всем строкам и всем ячейкам в каждой строке
            foreach (var row in usedRange.RowsUsed()) {
              foreach (var cell in row.CellsUsed()) {
                // --- ПРОВЕРКА 2: не число ---
                if (cell.TryGetValue<double>(out double dbl)) {
                  numbers.Add((int)dbl);
                } else {
                  string text = cell.GetString().Trim();

                  // Пустая ячейка — молча пропускаем
                  if (string.IsNullOrWhiteSpace(text)) continue;

                  // Не число — предупреждаем и останавливаемся
                  if (!int.TryParse(text, out int parsed)) {
                    MessageBox.Show(
                      $"Ячейка {cell.Address} содержит недопустимое значение: '{text}'.\n" +
                      "Допускаются только целые числа.",
                      "Ошибка данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                  }
                  numbers.Add(parsed);
                }
              }
            }
          }

          // --- ПРОВЕРКА 3: массив пустой ---
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

          List<int> numbers;
          try {
            numbers = ParseCsvAllFields(csv);
          } catch (FormatException fex) {
            MessageBox.Show(fex.Message, "Ошибка данных",
              MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
          }

          // --- ПРОВЕРКА: пустой массив ---
          if (numbers.Count == 0) {
            MessageBox.Show("В таблице не найдено ни одного числа.",
              "Пустой массив", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
          }

          if (numbers.Count == 0) {
            MessageBox.Show("Не найдено числовых данных в первом столбце.\n\n" +
              "Проверьте, что таблица опубликована: Файл → Поделиться → Доступ по ссылке.",
              "Нет данных", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
      if (url.Contains("export?format=csv") || url.EndsWith(".csv"))
        return url;

      var match = Regex.Match(url, @"/spreadsheets/d/([a-zA-Z0-9-_]+)");
      if (!match.Success)
        throw new ArgumentException("Не удалось распознать ID таблицы в ссылке. Убедитесь, что ссылка ведёт на Google Sheets.");

      string id = match.Groups[1].Value;

      string gid = "0";
      var gidMatch = Regex.Match(url, @"[#&?]gid=(\d+)");
      if (gidMatch.Success) gid = gidMatch.Groups[1].Value;

      return $"https://docs.google.com/spreadsheets/d/{id}/export?format=csv&gid={gid}";
    }

    /// Парсит CSV и вытаскивает числовые значения из первого столбца.
    private List<int> ParseCsvAllFields(string csv) {
      List<int> result = new List<int>();
      int lineNumber = 0;

      using (var reader = new StringReader(csv)) {
        string line;
        while ((line = reader.ReadLine()) != null) {
          lineNumber++;
          if (string.IsNullOrWhiteSpace(line)) continue;

          // Разбор строки CSV с учётом кавычек
          var fields = new List<string>();
          bool inQuotes = false;
          var current = new System.Text.StringBuilder();

          for (int i = 0; i < line.Length; i++) {
            char c = line[i];

            if (c == '"') {
              // Двойная кавычка внутри кавычек — экранирование
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

          // Обработка каждой ячейки в строке
          foreach (var field in fields) {
            string trimmed = field.Trim();

            // Пустая ячейка — пропускаем
            if (string.IsNullOrWhiteSpace(trimmed)) continue;

            if (int.TryParse(trimmed, out int num)) {
              result.Add(num);
            } else if (double.TryParse(trimmed,
                         System.Globalization.NumberStyles.Any,
                         System.Globalization.CultureInfo.InvariantCulture,
                         out double dbl)) {
              // Десятичные — округляем
              result.Add((int)dbl);
            } else {
              throw new FormatException(
                $"Строка {lineNumber}: недопустимое значение '{trimmed}'.\n" +
                "Допускаются только числа.");
            }
          }
        }
      }

      return result;
    }

    // ============================================================
    // ГЛАВНЫЙ ЗАПУСК СОРТИРОВОК
    // ============================================================

    private async void рассчитатьToolStripMenuItem_Click(object sender, EventArgs e) {
      int[] originalArray = GetArrayFromGrid();
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
        MessageBox.Show("Пожалуйста, выберите хотя бы один алгоритм сортировки с помощью чекбоксов!",
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

      // Устанавливаем направление сортировки из чекбокса
      _sortDescending = checkBoxReverse.Checked;

      sortingShow.Enabled = false;
      textBox1.Enabled = false;
      randomGeneric.Enabled = false;

      InitializeChartForSelectedAlgorithms(originalArray);

      int animationSpeedTimeout = 50;

      List<Task> sortingTasks = new List<Task>();
      Dictionary<string, double> executionTimesReport = new Dictionary<string, double>();

      // --- ПАРАЛЛЕЛЬНЫЙ ЗАПУСК СОРТИРОВОК С ЗАМЕРАМИ ВРЕМЕНИ ---

      if (checkBoxBubble.Checked) {
        int[] bubbleSortArray = (int[])originalArray.Clone();
        sortingTasks.Add(Task.Run(() => {
          TimeSpan startProcessorTime = Process.GetCurrentProcess().TotalProcessorTime;
          VisualBubbleSort(bubbleSortArray, "Пузырьковая");
          TimeSpan endProcessorTime = Process.GetCurrentProcess().TotalProcessorTime;
          executionTimesReport["Пузырьковая"] = (endProcessorTime - startProcessorTime).TotalMilliseconds;
        }));
      }

      if (checkBoxInsertion.Checked) {
        int[] insertionSortArray = (int[])originalArray.Clone();
        sortingTasks.Add(Task.Run(() => {
          TimeSpan startProcessorTime = Process.GetCurrentProcess().TotalProcessorTime;
          VisualInsertionSort(insertionSortArray, "Вставками", animationSpeedTimeout);
          TimeSpan endProcessorTime = Process.GetCurrentProcess().TotalProcessorTime;
          executionTimesReport["Вставками"] = (endProcessorTime - startProcessorTime).TotalMilliseconds;
        }));
      }

      if (checkBoxShaker.Checked) {
        int[] shakerSortArray = (int[])originalArray.Clone();
        sortingTasks.Add(Task.Run(() => {
          TimeSpan startProcessorTime = Process.GetCurrentProcess().TotalProcessorTime;
          VisualShakerSort(shakerSortArray, "Шейкерная", animationSpeedTimeout);
          TimeSpan endProcessorTime = Process.GetCurrentProcess().TotalProcessorTime;
          executionTimesReport["Шейкерная"] = (endProcessorTime - startProcessorTime).TotalMilliseconds;
        }));
      }

      if (checkBoxQuick.Checked) {
        int[] quickSortArray = (int[])originalArray.Clone();
        sortingTasks.Add(Task.Run(() => {
          TimeSpan startProcessorTime = Process.GetCurrentProcess().TotalProcessorTime;
          VisualQuickSort(quickSortArray, 0, quickSortArray.Length - 1, "Быстрая", animationSpeedTimeout);
          TimeSpan endProcessorTime = Process.GetCurrentProcess().TotalProcessorTime;
          executionTimesReport["Быстрая"] = (endProcessorTime - startProcessorTime).TotalMilliseconds;
        }));
      }

      if (checkBoxBogo.Checked) {
        int[] bogoSortArray = (int[])originalArray.Clone();
        sortingTasks.Add(Task.Run(() => {
          TimeSpan startProcessorTime = Process.GetCurrentProcess().TotalProcessorTime;
          VisualBogoSort(bogoSortArray, "BOGO Sort", animationSpeedTimeout);
          TimeSpan endProcessorTime = Process.GetCurrentProcess().TotalProcessorTime;
          executionTimesReport["BOGO Sort"] = (endProcessorTime - startProcessorTime).TotalMilliseconds;
        }));
      }

      await Task.WhenAll(sortingTasks);

      sortingShow.Enabled = true;
      textBox1.Enabled = true;
      randomGeneric.Enabled = true;

      dataGridViewResults.Rows.Clear();

      foreach (var record in executionTimesReport) {
        int rowIndex = dataGridViewResults.Rows.Add();
        dataGridViewResults.Rows[rowIndex].Cells[0].Value = record.Key;
        dataGridViewResults.Rows[rowIndex].Cells[1].Value = record.Value.ToString("F2");
      }

      dataGridViewResults.Sort(dataGridViewResults.Columns[1],
        System.ComponentModel.ListSortDirection.Ascending);

      if (dataGridViewResults.Rows.Count > 0) {
        dataGridViewResults.Rows[0].DefaultCellStyle.BackColor = Color.LightGreen;
      }
    }

    private void dataGridViewResults_CellContentClick(object sender, DataGridViewCellEventArgs e) {

    }
  }
}