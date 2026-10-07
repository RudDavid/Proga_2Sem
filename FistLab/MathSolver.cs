using System;
using System.Globalization;
using System.Text.RegularExpressions;
using NCalc;

namespace FistLab {
  public class MathSolver {
    /// <summary>
    /// Вычисляет значение функции f(x) для заданного x через NCalc
    /// </summary>
    public double CalculateFunction(string expression, double x) {
      try {
        // NCalc использует квадратные скобки для параметров: [x]
        // Заменяем одиночный 'x' (не входящий в состав слов типа "exp") на "[x]"
        string normalized = Regex.Replace(
            expression,
            @"(?<![a-zA-Z_])x(?![a-zA-Z_])",
            "[x]");

        // Создаем выражение (InvariantCulture — точка как разделитель)
        var expr = new Expression(normalized, CultureInfo.InvariantCulture);
        expr.Parameters["x"] = x;

        // Проверка на синтаксические ошибки
        if (expr.HasErrors()) {
          throw new Exception(expr.Error.Message);
        }

        object result = expr.Evaluate();

        // Приводим результат к double (NCalc может вернуть int, long и т.д.)
        return Convert.ToDouble(result, CultureInfo.InvariantCulture);
      } catch (Exception ex) {
        throw new Exception($"Ошибка в формуле: {ex.Message}");
      }
    }

    /// <summary>
    /// Метод половинного деления (дихотомия)
    /// </summary>
    public (double Root, double RootValue, int Iterations) FindRoot(
        string funcStr, double a, double b, double eps) {
      // Проверка условия смены знака
      double fa = CalculateFunction(funcStr, a);
      double fb = CalculateFunction(funcStr, b);

      if (fa * fb > 0) {
        throw new Exception(
            $"На концах интервала функция имеет одинаковые знаки.\n" +
            $"f({a}) = {fa:F5}\n" +
            $"f({b}) = {fb:F5}\n" +
            $"Метод дихотомии не гарантирует нахождение корня.");
      }

      double c = 0;
      int iterations = 0;
      const int maxIter = 10000;

      while ((b - a) / 2 > eps && iterations < maxIter) {
        c = (a + b) / 2;
        double fc = CalculateFunction(funcStr, c);

        if (Math.Abs(fc) < eps) break;

        if (fa * fc < 0) {
          b = c;
          fb = fc;
        } else {
          a = c;
          fa = fc;
        }
        iterations++;
      }

      double root = (a + b) / 2;
      double rootValue = CalculateFunction(funcStr, root);

      return (root, rootValue, iterations);
    }
  }
}