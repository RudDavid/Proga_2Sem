using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace FistLab {
  public partial class Form1 : Form {
    public Form1() {
      InitializeComponent();
    }

    private void randomGeneric_Click(object sender, System.EventArgs e) {

      if (!int.TryParse(textBox1.Text, out int value) || value <= 0 || value > 100) {
        MessageBox.Show("Пожалуйста, введите корректное число элементов до 100 штук.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

    private void InitializeChartForSelectedAlgorithms(int[] baseDataArray) {
      // Очищаем все прошлые ряды данных на графике
      chartVisualizer.Series.Clear();

      // Создаем словарь, где свяжем чекбокс, название ряда и его цвет
      var algorithmSettings = new[]
      {
        new { CheckBox = checkBoxBubble, Name = "Пузырьковая", Color = Color.Coral },
        new { CheckBox = checkBoxInsertion, Name = "Вставками", Color = Color.DeepSkyBlue },
        new { CheckBox = checkBoxShaker, Name = "Шейкерная", Color = Color.LimeGreen },
        new { CheckBox = checkBoxQuick, Name = "Быстрая", Color = Color.Orchid },
        new { CheckBox = checkBoxBogo, Name = "BOGO Sort", Color = Color.Crimson }
    };

      foreach (var setting in algorithmSettings) {
        // Если пользователь выбрал этот алгоритм чекбоксом
        if (setting.CheckBox.Checked) {
          // Создаем новый ряд столбиков
          Series newSeries = new Series(setting.Name);
          newSeries.ChartType = SeriesChartType.Column; // Тип: вертикальные столбики
          newSeries.Color = setting.Color;

          // Заполняем этот ряд копией исходного неотсортированного массива
          for (int index = 0; index < baseDataArray.Length; index++) {
            newSeries.Points.AddY(baseDataArray[index]);
          }

          // Добавляем ряд на сам график
          chartVisualizer.Series.Add(newSeries);
        }
      }
    }

    private void UpdateChartPoint(string seriesName, int pointIndex, int newValue) {
      // Безопасно перенаправляем обновление в поток интерфейса WinForms
      if (this.InvokeRequired) {
        this.Invoke((MethodInvoker)delegate { UpdateChartPoint(seriesName, pointIndex, newValue); });
        return;
      }

      // Меняем высоту столбика на графике
      if (chartVisualizer.Series.FindByName(seriesName) != null) {
        chartVisualizer.Series[seriesName].Points[pointIndex].SetValueY(newValue);
        chartVisualizer.Refresh(); // Принудительно перерисовываем окно графика
      }
    }









    public void VisualBubbleSort(int[] dataArray, string seriesName) {
      int arrayLength = dataArray.Length;

      for (int currentPass = 0; currentPass < arrayLength - 1; currentPass++) {
        for (int elementIndex = 0; elementIndex < arrayLength - currentPass - 1; elementIndex++) {
          if (dataArray[elementIndex] > dataArray[elementIndex + 1]) {
            int temporaryValue = dataArray[elementIndex];
            dataArray[elementIndex] = dataArray[elementIndex + 1];
            dataArray[elementIndex + 1] = temporaryValue;

            // 1. Обновляем измененные элементы на графике!
            UpdateChartPoint(seriesName, elementIndex, dataArray[elementIndex]);
            UpdateChartPoint(seriesName, elementIndex + 1, dataArray[elementIndex + 1]);

            // 2. Делаем паузу (скорость анимации), например, 50 миллисекунд
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

        while (previousIndex >= 0 && dataArray[previousIndex] > keyElement) {
          dataArray[previousIndex + 1] = dataArray[previousIndex];

          // Визуализируем сдвиг элемента
          UpdateChartPoint(seriesName, previousIndex + 1, dataArray[previousIndex + 1]);
          Thread.Sleep(sleepTimeout);

          previousIndex = previousIndex - 1;
        }
        dataArray[previousIndex + 1] = keyElement;

        // Визуализируем вставку ключевого элемента на его финальное место
        UpdateChartPoint(seriesName, previousIndex + 1, dataArray[previousIndex + 1]);
        Thread.Sleep(sleepTimeout);
      }
    }


    public void VisualShakerSort(int[] dataArray, string seriesName, int sleepTimeout) {
      int leftBoundary = 0;
      int rightBoundary = dataArray.Length - 1;

      while (leftBoundary <= rightBoundary) {
        for (int scanIndex = leftBoundary; scanIndex < rightBoundary; scanIndex++) {
          if (dataArray[scanIndex] > dataArray[scanIndex + 1]) {
            int temporaryValue = dataArray[scanIndex];
            dataArray[scanIndex] = dataArray[scanIndex + 1];
            dataArray[scanIndex + 1] = temporaryValue;

            // Блок визуализации (движение вправо)
            UpdateChartPoint(seriesName, scanIndex, dataArray[scanIndex]);
            UpdateChartPoint(seriesName, scanIndex + 1, dataArray[scanIndex + 1]);
            Thread.Sleep(sleepTimeout);
          }
        }
        rightBoundary--;

        for (int scanIndex = rightBoundary; scanIndex > leftBoundary; scanIndex--) {
          if (dataArray[scanIndex - 1] > dataArray[scanIndex]) {
            int temporaryValue = dataArray[scanIndex];
            dataArray[scanIndex] = dataArray[scanIndex - 1];
            dataArray[scanIndex - 1] = temporaryValue;

            // Блок визуализации (движение влево)
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
        if (dataArray[currentIndex] < pivotValue) {
          smallerElementIndex++;
          int temporaryValue = dataArray[smallerElementIndex];
          dataArray[smallerElementIndex] = dataArray[currentIndex];
          dataArray[currentIndex] = temporaryValue;

          // Визуализация обмена элементов в цикле
          UpdateChartPoint(seriesName, smallerElementIndex, dataArray[smallerElementIndex]);
          UpdateChartPoint(seriesName, currentIndex, dataArray[currentIndex]);
          Thread.Sleep(sleepTimeout);
        }
      }

      int finalPivotPlaceIndex = smallerElementIndex + 1;
      int temporaryPivotValue = dataArray[finalPivotPlaceIndex];
      dataArray[finalPivotPlaceIndex] = dataArray[endingIndex];
      dataArray[endingIndex] = temporaryPivotValue;

      // Визуализация финальной перестановки опорного элемента
      UpdateChartPoint(seriesName, finalPivotPlaceIndex, dataArray[finalPivotPlaceIndex]);
      UpdateChartPoint(seriesName, endingIndex, dataArray[endingIndex]);
      Thread.Sleep(sleepTimeout);

      return finalPivotPlaceIndex;
    }


    public void VisualBogoSort(int[] dataArray, string seriesName, int sleepTimeout) {
      Random randomGenerator = new Random();

      // Создаем и запускаем секундомер для контроля времени выполнения
      Stopwatch bogoTimer = Stopwatch.StartNew();
      // Задаем лимит времени выполнения в миллисекундах (5000 мс = 5 секунд)
      long timeLimitMilliseconds = 5000;

      while (!CheckIfArrayIsSorted(dataArray)) {
        // Проверяем триггер времени: если секундомер превысил лимит, прерываем сортировку
        if (bogoTimer.ElapsedMilliseconds >= timeLimitMilliseconds) {
          // Останавливаем секундомер
          bogoTimer.Stop();

          // Выводим пользователю предупреждение, что алгоритм сдался
          this.Invoke((MethodInvoker)delegate {
            MessageBox.Show($"Время выполнения BOGO Sort превысило лимит в {timeLimitMilliseconds / 1000} секунд! Алгоритм был принудительно остановлен.",
                            "Превышен лимит времени", MessageBoxButtons.OK, MessageBoxIcon.Warning);
          });

          return; // Выходим из метода, не дожидаясь сортировки
        }

        // Стандартное случайное перемешивание элементов массива
        for (int currentIndex = dataArray.Length - 1; currentIndex > 0; currentIndex--) {
          int randomIndex = randomGenerator.Next(currentIndex + 1);

          int temporaryValue = dataArray[currentIndex];
          dataArray[currentIndex] = dataArray[randomIndex];
          dataArray[randomIndex] = temporaryValue;
        }

        // Синхронно обновляем все столбцы на графике для BOGO
        for (int pointIndex = 0; pointIndex < dataArray.Length; pointIndex++) {
          UpdateChartPoint(seriesName, pointIndex, dataArray[pointIndex]);
        }

        Thread.Sleep(sleepTimeout);
      }

      // Если массив чудом успел отсортироваться до таймаута, останавливаем таймер
      bogoTimer.Stop();
    }



    // Вспомогательный метод валидации порядка элементов
    private bool CheckIfArrayIsSorted(int[] dataArray) {
      for (int currentIndex = 0; currentIndex < dataArray.Length - 1; currentIndex++) {
        if (dataArray[currentIndex] > dataArray[currentIndex + 1]) {
          return false;
        }
      }
      return true;
    }

    private int[] GetArrayFromGrid() {
      // Подсчитываем строки, исключая нижнюю пустую строку для ввода
      int rowsCount = sortingShow.AllowUserToAddRows ? sortingShow.Rows.Count - 1 : sortingShow.Rows.Count;

      if (rowsCount <= 0) return new int[0];

      int[] extractedArray = new int[rowsCount];

      for (int index = 0; index < rowsCount; index++) {
        object cellValue = sortingShow.Rows[index].Cells[0].Value;

        // Если ячейка пустая
        if (cellValue == null || string.IsNullOrWhiteSpace(cellValue.ToString())) {
          MessageBox.Show($"Обнаружена пустая ячейка в строке {index + 1}! Заполните её или удалите.",
                          "Ошибка входных данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return null; // Возвращаем null как маркер ошибки
        }

        // Пытаемся преобразовать текст в число. Если там буквы — tryParse вернет false
        if (!int.TryParse(cellValue.ToString(), out int validNumber)) {
          MessageBox.Show($"В строке {index + 1} обнаружено недопустимое значение '{cellValue}'. Допускаются только целые числа!",
                          "Ошибка валидации данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return null; // Возвращаем null, блокируя запуск алгоритма
        }

        extractedArray[index] = validNumber;
      }
      return extractedArray;
    }


    private void очиститьToolStripMenuItem_Click(object sender, EventArgs e) {
      // 1. Очищаем все строки в DataGridView
      sortingShow.Rows.Clear();

      // 2. Полностью очищаем все ряды и столбцы на графике Chart
      chartVisualizer.Series.Clear();

      // 3. Сбрасываем текст в поле ввода количества элементов
      textBox1.Clear();

      // 4. Гарантируем, что таблица снова разрешает ручной ввод (возвращаем свойство в исходное)
      sortingShow.AllowUserToAddRows = true;

      MessageBox.Show("Все данные и графики успешно очищены!", "Сброс", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private async void рассчитатьToolStripMenuItem_Click(object sender, EventArgs e) {
      // 1. Получаем массив чисел из вашей таблицы DataGridView
      int[] originalArray = GetArrayFromGrid();
      if (originalArray.Length == 0) {
        MessageBox.Show("Таблица пуста! Сначала сгенерируйте или введите данные вручную.",
                        "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
      }

      // 2. Считаем, сколько алгоритмов выбрал пользователь через CheckBox
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

      // 3. Отказоустойчивость: Защита от зависания ПК при случайном запуске BOGO на больших данных
      if (checkBoxBogo.Checked && originalArray.Length > 10) {
        DialogResult dialogResult = MessageBox.Show(
            $"Вы выбрали BOGO Sort для {originalArray.Length} элементов. Этот алгоритм работает случайным образом и может вычисляться годами, зависнув на 100% процессора. Вы уверены, что хотите продолжить?",
            "Опасность зависания",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Error);

        if (dialogResult == DialogResult.No) {
          return; // Отменяем расчет, если пользователь передумал
        }
      }

      // 4. Замораживаем элементы управления формы, чтобы пользователь ничего не сломал во время расчетов
      sortingShow.Enabled = false;
      textBox1.Enabled = false;
      randomGeneric.Enabled = false;

      // 5. Инициализируем новые цветные столбики на графике на основе исходного массива
      InitializeChartForSelectedAlgorithms(originalArray);

      // Переменная, которая задает скорость анимации перестроения графиков (в миллисекундах)
      int animationSpeedTimeout = 50;

      // Создаем список задач для одновременного параллельного выполнения
      List<Task> sortingTasks = new List<Task>();

      // Словарь, куда мы сохраним замеры чистого процессорного времени по каждому алгоритму
      Dictionary<string, double> executionTimesReport = new Dictionary<string, double>();

      // --- ПАРАЛЛЕЛЬНЫЙ ЗАПУСК СОРТИРОВОК С ЗАМЕРАМИ ВРЕМЕНИ ---

      if (checkBoxBubble.Checked) {
        // Создаем независимую изолированную копию массива для конкретного потока
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

      // 6. Ожидаем завершения ВСЕХ запущенных потоков одновременно без зависания самой формы
      await Task.WhenAll(sortingTasks);

      // 7. Разблокируем интерфейс обратно для пользователя
      sortingShow.Enabled = true;
      textBox1.Enabled = true;
      randomGeneric.Enabled = true;

      // 8.Очищаем старые результаты перед выводом новых
      dataGridViewResults.Rows.Clear();

      // 9. Переносим данные из словаря замеров прямо в ячейки нашей таблицы результатов
      foreach (var record in executionTimesReport) {
        // Добавляем новую строку в таблицу результатов
        int rowIndex = dataGridViewResults.Rows.Add();

        // В первую ячейку записываем название алгоритма
        dataGridViewResults.Rows[rowIndex].Cells[0].Value = record.Key;

        // Во вторую ячейку записываем время с округлением до двух знаков
        dataGridViewResults.Rows[rowIndex].Cells[1].Value = record.Value.ToString("F2");
      }

      // 10. Автоматически сортируем таблицу результатов по возрастанию времени (колонка с индексом 1)
      dataGridViewResults.Sort(dataGridViewResults.Columns[1], System.ComponentModel.ListSortDirection.Ascending);

      // 11. Бонус для защиты: красим самую первую строчку (лучший результат) в нежно-зеленый цвет
      if (dataGridViewResults.Rows.Count > 0) {
        dataGridViewResults.Rows[0].DefaultCellStyle.BackColor = Color.LightGreen;
      }
    }
  }
}
