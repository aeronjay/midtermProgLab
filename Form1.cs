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
        private int debounceInterval = 300; 

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
            MyRichTextBox.MouseWheel += new MouseEventHandler(MyRichTextBox_MouseWheel);

            MyRichTextBox.Text = "\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n";

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
        private void MyRichTextBox_MouseWheel(object sender, MouseEventArgs e)
        {
            if (ModifierKeys.HasFlag(Keys.Control))
            {
                ((HandledMouseEventArgs)e).Handled = true;
            }
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
            MyRichTextBox.Text = "\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n";
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

        private void ApplySyntaxHighlighting()
        {
            MyRichTextBox.SuspendLayout();

            int originalSelectionStart = MyRichTextBox.SelectionStart;
            int originalSelectionLength = MyRichTextBox.SelectionLength;

            int firstVisibleCharIndex = MyRichTextBox.GetCharIndexFromPosition(new Point(0, 0));
            int firstVisibleLine = MyRichTextBox.GetLineFromCharIndex(firstVisibleCharIndex);
            int lastVisibleCharIndex = MyRichTextBox.GetCharIndexFromPosition(new Point(0, MyRichTextBox.ClientRectangle.Bottom));
            int lastVisibleLine = MyRichTextBox.GetLineFromCharIndex(lastVisibleCharIndex);

            ColorVisibleText(firstVisibleLine, lastVisibleLine);

            MyRichTextBox.Select(originalSelectionStart, originalSelectionLength);

            MyRichTextBox.ResumeLayout();
        }
        private void ColorVisibleText(int startLine, int endLine)
        {
            int originalSelectionStart = MyRichTextBox.SelectionStart;
            int originalSelectionLength = MyRichTextBox.SelectionLength;
            Color originalColor = MyRichTextBox.SelectionColor;

            using (Graphics g = MyRichTextBox.CreateGraphics())
            {
                for (int i = startLine; i <= endLine && i < MyRichTextBox.Lines.Length; i++)
                {
                    int startIndex = MyRichTextBox.GetFirstCharIndexFromLine(i);
                    int endIndex = (i < MyRichTextBox.Lines.Length - 1) ?
                        MyRichTextBox.GetFirstCharIndexFromLine(i + 1) - 1 :
                        MyRichTextBox.TextLength - 1;

                    string lineText = MyRichTextBox.Lines[i];
                    ColorLineText(lineText, startIndex, endIndex);
                }
            }

            MyRichTextBox.Select(originalSelectionStart, originalSelectionLength);
            MyRichTextBox.SelectionColor = Color.White;
        }
        private void ColorLineText(string lineText, int startIndex, int endIndex)
        {
            int originalSelectionStart = MyRichTextBox.SelectionStart;
            int originalSelectionLength = MyRichTextBox.SelectionLength;
            Color originalColor = MyRichTextBox.SelectionColor;

            foreach (Match match in Regex.Matches(lineText, @"\b(DECLARE|num|text|tof|alph|numd|say|txt)\b"))
            {
                MyRichTextBox.Select(startIndex + match.Index, match.Length);
                MyRichTextBox.SelectionColor = GetColorForToken(match.Value);
            }

            MyRichTextBox.Select(originalSelectionStart, originalSelectionLength);
            MyRichTextBox.SelectionColor = originalColor;
        }

        private Color GetColorForToken(string token)
        {
            switch (token)
            {
                case "DECLARE":
                    return Color.Red;
                case "num":
                case "text":
                case "tof":
                case "alph":
                case "numd":
                    return Color.Blue;
                case "say":
                    return Color.Green;
                case "txt":
                    return Color.Yellow;
                default:
                    return MyRichTextBox.ForeColor;
            }
        }

        private void start_Click(object sender, EventArgs e)
        {
            string cobraCode = MyRichTextBox.Text;
            string pythonCode = Tokenize(cobraCode);
            Form2 outputForm = new Form2();
            outputForm.SetOutput(ExecuteCobraCode(pythonCode));
            outputForm.Show();
        }

        private string Tokenize(string cobraCode)
{
    // Define the txt function to replace str
    string txtFunctionDefinition = @"
def txt(value):
    return str(value)
";

    // Translate Cobra code to Python code
    cobraCode = Regex.Replace(cobraCode, @"DECLARE\s+num\s+(\w+)\s*=\s*(.+)", "$1 = int($2)");
    cobraCode = Regex.Replace(cobraCode, @"DECLARE\s+text\s+(\w+)\s*=\s*(.+)", "$1 = str($2)");
    cobraCode = Regex.Replace(cobraCode, @"DECLARE\s+tof\s+(\w+)\s*=\s*(.+)", "$1 = bool($2)");
    cobraCode = Regex.Replace(cobraCode, @"DECLARE\s+alph\s+(\w+)\s*=\s*'(.+)'", "$1 = '$2'");
    cobraCode = Regex.Replace(cobraCode, @"DECLARE\s+numd\s+(\w+)\s*=\s*(.+)", "$1 = float($2)");

    cobraCode = Regex.Replace(cobraCode, @"\bsay\(", "print(");

    // Replace str() with txt()
    cobraCode = Regex.Replace(cobraCode, @"\bstr\(", "txt(");

    cobraCode = Regex.Replace(cobraCode, @"\+(\S)", " + $1");
    cobraCode = Regex.Replace(cobraCode, @"(\S)\+", "$1 + ");

    // Prepend the txt function definition to the translated code
    return txtFunctionDefinition + cobraCode;
}

        private string ExecuteCobraCode(string pythonCode)
        {
            ScriptEngine engine = Python.CreateEngine();
            ScriptScope scope = engine.CreateScope();

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

            output.Seek(0, SeekOrigin.Begin);
            using (var reader = new StreamReader(output))
            {
                return reader.ReadToEnd();
            }
        }

        private void lexical_Click(object sender, EventArgs e)
        {
            string code = MyRichTextBox.Text;

            // Keywords and datatypes
            string[] keywords = { "DECLARE", "num", "text", "tof", "alph", "numd" };
            string[] functions = { "say" };
            string[] operators = { "+", "=" };

            Dictionary<string, int> keywordCounts = new Dictionary<string, int>();
            Dictionary<string, int> functionCounts = new Dictionary<string, int>();
            Dictionary<string, int> operatorCounts = new Dictionary<string, int>();

            foreach (string keyword in keywords)
            {
                keywordCounts[keyword] = Regex.Matches(code, $@"\b{Regex.Escape(keyword)}\b").Count;
            }

            foreach (string function in functions)
            {
                functionCounts[function] = Regex.Matches(code, $@"\b{Regex.Escape(function)}\b").Count;
            }

            foreach (string op in operators)
            {
                operatorCounts[op] = Regex.Matches(code, Regex.Escape(op)).Count;
            }

            StringBuilder output = new StringBuilder();
            output.AppendLine("Keywords:");
            foreach (var kvp in keywordCounts)
            {
                output.AppendLine($"{kvp.Key} : {kvp.Value}");
            }

            output.AppendLine("\nFunctions:");
            foreach (var kvp in functionCounts)
            {
                output.AppendLine($"{kvp.Key} : {kvp.Value}");
            }

            output.AppendLine("\nOperators:");
            foreach (var kvp in operatorCounts)
            {
                output.AppendLine($"{kvp.Key} : {kvp.Value}");
            }

            MessageBox.Show(output.ToString(), "Lexical Analysis", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void newFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyRichTextBox.Clear();
            currentFilePath = string.Empty;
            fileNameLabel.Text = "New File";
        }

        private void saveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            savefile_Click(sender, e);


        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveAs_Click(sender, e);

        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        MyRichTextBox.Text = File.ReadAllText(openFileDialog.FileName);
                        currentFilePath = openFileDialog.FileName;
                        fileNameLabel.Text = Path.GetFileName(currentFilePath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to open file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            undo_Click(sender, e);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(MyRichTextBox.SelectedText))
            {
                MyRichTextBox.SelectedText = string.Empty;
            }
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            redo_Click(sender, e);
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                MyRichTextBox.Paste();
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(MyRichTextBox.SelectedText))
            {
                Clipboard.SetText(MyRichTextBox.SelectedText);
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            string searchesFor = searchText.Text;

            if (string.IsNullOrEmpty(searchesFor))
            {
                MessageBox.Show("Please enter text to search for.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            HighlightSearchResults(searchesFor);
        }

        private void HighlightSearchResults(string searchText)
        {
            // Reset previous highlights
            MyRichTextBox.SelectAll();
            MyRichTextBox.SelectionBackColor = MyRichTextBox.BackColor;

            if (string.IsNullOrEmpty(searchText))
            {
                return;
            }

            int startIndex = 0;
            while (startIndex < MyRichTextBox.TextLength)
            {
                int index = MyRichTextBox.Text.IndexOf(searchText, startIndex, StringComparison.CurrentCultureIgnoreCase);
                if (index != -1)
                {
                    MyRichTextBox.Select(index, searchText.Length);
                    MyRichTextBox.SelectionBackColor = Color.Yellow; // Highlight color
                    startIndex = index + searchText.Length;
                }
                else
                {
                    break;
                }
            }

            MyRichTextBox.Select(MyRichTextBox.TextLength, 0);
            MyRichTextBox.SelectionBackColor = MyRichTextBox.BackColor;
        }
    }

}
