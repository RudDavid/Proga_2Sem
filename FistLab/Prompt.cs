using System.Drawing;
using System.Windows.Forms;

namespace FistLab {
  public static class Prompt {
    public static string ShowDialog(string text, string caption) {
      Form prompt = new Form() {
        Width = 580,
        Height = 180,
        FormBorderStyle = FormBorderStyle.FixedDialog,
        Text = caption,
        StartPosition = FormStartPosition.CenterParent,
        MaximizeBox = false,
        MinimizeBox = false
      };

      Label textLabel = new Label() {
        Left = 15,
        Top = 15,
        Text = text,
        Width = 530,
        Height = 20
      };

      TextBox inputBox = new TextBox() {
        Left = 15,
        Top = 45,
        Width = 530
      };

      Button confirmation = new Button() {
        Text = "OK",
        Left = 370,
        Width = 80,
        Top = 90,
        DialogResult = DialogResult.OK
      };

      Button cancel = new Button() {
        Text = "Отмена",
        Left = 460,
        Width = 85,
        Top = 90,
        DialogResult = DialogResult.Cancel
      };

      prompt.Controls.Add(textLabel);
      prompt.Controls.Add(inputBox);
      prompt.Controls.Add(confirmation);
      prompt.Controls.Add(cancel);
      prompt.AcceptButton = confirmation;
      prompt.CancelButton = cancel;

      return prompt.ShowDialog() == DialogResult.OK
        ? inputBox.Text.Trim()
        : null;
    }
  }
}