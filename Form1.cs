using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

namespace midtermProgLab
{
    public partial class Form1 : Form
    {
        private string currentFilePath;
        private Label fileNameLabel;

        private Timer debounceTimer;
        private int debounceInterval = 300; // Adjust interval as needed

        public Form1()
        {
            InitializeComponent();
            InitializeCustomComponents();

            debounceTimer = new Timer();
            debounceTimer.Interval = debounceInterval;
            debounceTimer.Tick += DebounceTimer_Tick;

            MyRichTextBox.Resize += new EventHandler(MyRichTextBox_Resize);
            MyRichTextBox.VScroll += new EventHandler(MyRichTextBox_VScroll);
            MyRichTextBox.Paint += new PaintEventHandler(MyRichTextBox_Paint);
            MyPictureBox.Paint += new PaintEventHandler(pictureBox1_Paint);
            MyRichTextBox.TextChanged += new EventHandler(MyRichTextBox_TextChanged);

            MyRichTextBox.Text = "\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n";

            ApplySyntaxHighlighting();
        }

        private void InitializeCustomComponents()
        {
            fileNameLabel = new Label
            {
                AutoSize = true,
                Location = new Point(54, 8),
                Text = "New File",
                ForeColor = Color.White
            };
            this.panel3.Controls.Add(fileNameLabel);
        }

        private void MyRichTextBox_TextChanged(object sender, EventArgs e)
        {
            debounceTimer.Stop();
            debounceTimer.Start();
        }
        private void DebounceTimer_Tick(object sender, EventArgs e)
        {
            debounceTimer.Stop();
            ApplySyntaxHighlighting();
        }


        private void MyRichTextBox_Resize(object sender, EventArgs e)
        {
            MyPictureBox.Invalidate();
        }

        private void MyRichTextBox_VScroll(object sender, EventArgs e)
        {
            MyPictureBox.Invalidate();
        }

        private void MyRichTextBox_Paint(object sender, PaintEventArgs e)
        {
            MyPictureBox.Invalidate();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            DrawRichTextBoxLineNumbers(e.Graphics);
        }

        private void DrawRichTextBoxLineNumbers(Graphics g)
        {
            using (SolidBrush brush = new SolidBrush(MyRichTextBox.BackColor))
            {
                g.FillRectangle(brush, MyPictureBox.ClientRectangle);
            }

            float fontHeight = MyRichTextBox.Font.GetHeight();
            int firstIndex = MyRichTextBox.GetCharIndexFromPosition(new Point(0, (int)g.VisibleClipBounds.Y));
            int firstLine = MyRichTextBox.GetLineFromCharIndex(firstIndex);
            Point firstPos = MyRichTextBox.GetPositionFromCharIndex(firstIndex);

            int lastIndex = MyRichTextBox.GetCharIndexFromPosition(new Point(0, (int)g.VisibleClipBounds.Y + MyPictureBox.Height));
            int lastLine = MyRichTextBox.GetLineFromCharIndex(lastIndex);

            const float verticalPadding = 3.0f;

            using (SolidBrush brush = new SolidBrush(Color.Gray))
            {
                for (int i = firstLine; i <= lastLine; i++)
                {
                    Point pos = MyRichTextBox.GetPositionFromCharIndex(MyRichTextBox.GetFirstCharIndexFromLine(i));
                    float y = pos.Y + verticalPadding;
                    g.DrawString((i + 1).ToString(), MyRichTextBox.Font, brush, MyPictureBox.Width - g.MeasureString((i + 1).ToString(), MyRichTextBox.Font).Width, y);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MyRichTextBox.Text = "\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n";
        }

        private void AdjustFontSize(float adjustment)
        {
            float currentSize = MyRichTextBox.Font.Size;
            float newSize = currentSize + adjustment;
            if (newSize < 1) newSize = 1;
            MyRichTextBox.Font = new Font(MyRichTextBox.Font.FontFamily, newSize);
            MyPictureBox.Invalidate();
        }

        private void zoomInCode_Click(object sender, EventArgs e)
        {
            AdjustFontSize(1);
        }

        private void zoomOutCode_Click(object sender, EventArgs e)
        {
            AdjustFontSize(-1);
        }

        private void darkMode_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuToggleSwitch.CheckedChangedEventArgs e)
        {
            if (e.Checked)
            {
                // Dark mode
            }
            else
            {
                // Light mode
            }
            MyPictureBox.Invalidate();
        }

        private void undo_Click(object sender, EventArgs e)
        {
            if (MyRichTextBox.CanUndo)
            {
                MyRichTextBox.Undo();
            }
        }

        private void redo_Click(object sender, EventArgs e)
        {
            if (MyRichTextBox.CanRedo)
            {
                MyRichTextBox.Redo();
            }
        }

        private void savefile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentFilePath))
            {
                saveAs_Click(sender, e);
            }
            else
            {
                SaveFile(currentFilePath);
            }
        }

        private void saveAs_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    currentFilePath = saveFileDialog.FileName;
                    SaveFile(currentFilePath);
                }
            }
        }

        private void SaveFile(string path)
        {
            try
            {
                File.WriteAllText(path, MyRichTextBox.Text);
                MessageBox.Show("File saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                fileNameLabel.Text = Path.GetFileName(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ColorWords(RichTextBox richTextBox, string word, Color color)
        {
            string pattern = $@"\b{Regex.Escape(word)}\b";

            int originalSelectionStart = richTextBox.SelectionStart;
            int originalSelectionLength = richTextBox.SelectionLength;
            Color originalColor = richTextBox.SelectionColor;

            foreach (Match match in Regex.Matches(richTextBox.Text, pattern))
            {
                richTextBox.Select(match.Index, match.Length);
                richTextBox.SelectionColor = color;
            }

            richTextBox.Select(originalSelectionStart, originalSelectionLength);
            richTextBox.SelectionColor = originalColor;
        }

        private void ApplySyntaxHighlighting()
        {
            MyRichTextBox.SuspendLayout();

            int originalSelectionStart = MyRichTextBox.SelectionStart;
            int originalSelectionLength = MyRichTextBox.SelectionLength;

            ColorWords(MyRichTextBox, @"DECLARE\s+num\b", Color.Red);
            ColorWords(MyRichTextBox, @"DECLARE\s+text\b", Color.Red);
            ColorWords(MyRichTextBox, @"DECLARE\s+tof\b", Color.Red);
            ColorWords(MyRichTextBox, @"DECLARE\s+alph\b", Color.Red);
            ColorWords(MyRichTextBox, @"DECLARE\s+numd\b", Color.Red);
            ColorWords(MyRichTextBox, @"\bnum\b", Color.Blue);
            ColorWords(MyRichTextBox, @"\btext\b", Color.Blue);
            ColorWords(MyRichTextBox, @"\btof\b", Color.Blue);
            ColorWords(MyRichTextBox, @"\balph\b", Color.Blue);
            ColorWords(MyRichTextBox, @"\bnumd\b", Color.Blue);
            ColorWords(MyRichTextBox, @"\bsay\b", Color.Green);

            MyRichTextBox.Select(originalSelectionStart, originalSelectionLength);
            MyRichTextBox.SelectionColor = MyRichTextBox.ForeColor;

            MyRichTextBox.ResumeLayout();
        }

        private void start_Click(object sender, EventArgs e)
        {
            string cobraCode = MyRichTextBox.Text;
            string pythonCode = TranslateToPython(cobraCode);
            Form2 outputForm = new Form2();
            outputForm.SetOutput(ExecutePythonCode(pythonCode));
            outputForm.Show();
        }

        private string TranslateToPython(string cobraCode)
        {
            // Basic translation for variable declarations
            cobraCode = Regex.Replace(cobraCode, @"DECLARE\s+num\s+(\w+)\s*=\s*(.+)", "$1 = int($2)");
            cobraCode = Regex.Replace(cobraCode, @"DECLARE\s+text\s+(\w+)\s*=\s*(.+)", "$1 = str($2)");
            cobraCode = Regex.Replace(cobraCode, @"DECLARE\s+tof\s+(\w+)\s*=\s*(.+)", "$1 = bool($2)");
            cobraCode = Regex.Replace(cobraCode, @"DECLARE\s+alph\s+(\w+)\s*=\s*'(.+)'", "$1 = '$2'");
            cobraCode = Regex.Replace(cobraCode, @"DECLARE\s+numd\s+(\w+)\s*=\s*(.+)", "$1 = float($2)");

            // Translation for print statements
            cobraCode = Regex.Replace(cobraCode, @"\bsay\(", "print(");

            // Handling concatenation for Python (ensuring spaces around + for readability)
            cobraCode = Regex.Replace(cobraCode, @"\+(\S)", " + $1");
            cobraCode = Regex.Replace(cobraCode, @"(\S)\+", "$1 + ");

            return cobraCode;
        }

        private string ExecutePythonCode(string pythonCode)
        {
            ScriptEngine engine = Python.CreateEngine();
            ScriptScope scope = engine.CreateScope();

            // Redirect stdout
            var output = new MemoryStream();
            var writer = new StreamWriter(output);
            writer.AutoFlush = true;
            engine.Runtime.IO.SetOutput(output, writer);

            try
            {
                engine.Execute(pythonCode, scope);
            }
            catch (Exception ex)
            {
                var eo = engine.GetService<Microsoft.Scripting.Hosting.ExceptionOperations>();
                string error = eo.FormatException(ex);
                return $"Error: {ex.Message}\n{error}";
            }

            // Read the output
            output.Seek(0, SeekOrigin.Begin);
            using (var reader = new StreamReader(output))
            {
                return reader.ReadToEnd();
            }
        }
    }

}
