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
        private List<Form2> openOutputForms = new List<Form2>();
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
        private string ExecuteCobraCode(string cobraCode)
        {
            ScriptEngine engine = Python.CreateEngine();
            ScriptScope scope = engine.CreateScope();

            var output = new MemoryStream();
            var writer = new StreamWriter(output);
            writer.AutoFlush = true;
            engine.Runtime.IO.SetOutput(output, writer);

            try
            {
                string prelude = @"
def say(message):
    print(message)
def txt(value):
    return str(value)
";
                engine.Execute(prelude, scope);

                engine.Execute(cobraCode, scope);
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
            MyRichTextBox.Undo();
        }

        private void redo_Click(object sender, EventArgs e)
        {
            if (MyRichTextBox.CanRedo)
            {
                MyRichTextBox.Redo();
            }
            MyRichTextBox.Redo();
        }
        private void initializeTokens(ref string cobraCode)
        {
            cobraCode = Regex.Replace(cobraCode, @"DECLARE\s+num\s+(\w+)\s*=\s*(.+)", "$1 = int($2)");
            cobraCode = Regex.Replace(cobraCode, @"DECLARE\s+text\s+(\w+)\s*=\s*(.+)", "$1 = str($2)");
            cobraCode = Regex.Replace(cobraCode, @"DECLARE\s+tof\s+(\w+)\s*=\s*(.+)", "$1 = bool($2)");
            cobraCode = Regex.Replace(cobraCode, @"DECLARE\s+alph\s+(\w+)\s*=\s*'(.+)'", "$1 = '$2'");
            cobraCode = Regex.Replace(cobraCode, @"DECLARE\s+numd\s+(\w+)\s*=\s*(.+)", "$1 = float($2)");

            cobraCode = Regex.Replace(cobraCode, @"\bsay\(", "print(");
            cobraCode = Regex.Replace(cobraCode, @"\bstr\(", "txt(");
            cobraCode = Regex.Replace(cobraCode, @"\+(\S)", " + $1");
            cobraCode = Regex.Replace(cobraCode, @"(\S)\+", "$1 + ");

            cobraCode = Regex.Replace(cobraCode, @"\bpag\s*\((.+?)\)\s*:\s*\n", "if ($1):\n");
            cobraCode = Regex.Replace(cobraCode, @"\bkung\s*\((.+?)\)\s*:\s*\n", "elif ($1):\n");
            cobraCode = Regex.Replace(cobraCode, @"\bedi\s*:\s*\n", "else:\n");

            cobraCode = Regex.Replace(cobraCode, @"DECLARE\s+(\w+)\s+tas\s+repeat\((\d+)\)\s*:\s*\n", "for $1 in range($2):\n");

            cobraCode = Regex.Replace(cobraCode, @"lipat\s+(\w+)\s*:\s*\n((?:\s*kaso\s+\S+\s*:\s*.+\n)+)\s*hinto", m =>
            {
                string variable = m.Groups[1].Value;
                string cases = m.Groups[2].Value;

                var caseStatements = new List<string>();
                foreach (Match caseMatch in Regex.Matches(cases, @"\s*kaso\s+(\S+)\s*:\s*(.+)\n"))
                {
                    string caseValue = caseMatch.Groups[1].Value;
                    string caseCode = caseMatch.Groups[2].Value;
                    caseStatements.Add($"{(caseStatements.Count == 0 ? "if" : "elif")} {variable} == {caseValue}:\n    {caseCode}");
                }

                return string.Join("\n", caseStatements);
            });

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

            foreach (Match match in Regex.Matches(lineText, @"\b(DECLARE|num|text|tof|alph|numd|say|txt|pag|kung|edi|repeat)\b"))
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
                case "pag":
                case "kung":
                case "edi":
                    return Color.Pink;
                case "repeat":
                    return Color.Yellow;
                default:
                    return MyRichTextBox.ForeColor;
            }
        }

        private void lexical_Click(object sender, EventArgs e)
        {
            string code = MyRichTextBox.Text;

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
                    MyRichTextBox.SelectionBackColor = Color.Yellow;
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

        private void stop_Click(object sender, EventArgs e)
        {
            foreach (Form2 form in openOutputForms)
            {
                form.Close();
            }
            openOutputForms.Clear();
        }

        private void start_Click(object sender, EventArgs e)
        {
            try
            {
                string cobraCode = MyRichTextBox.Text;
                string tokenized = Tokenize(cobraCode);
                //string interpreted = InterpretCobraCode(cobraCode);
                Form2 outputForm = new Form2();
                outputForm.SetOutput(ExecuteCobraCode(tokenized));
                outputForm.Show();
                openOutputForms.Add(outputForm);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Syntax Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string Tokenize(string cobraCode)
        {
            string txtFunctionDefinition = @"
def txt(value):
    return str(value)
";

            string[] reservedKeywords = { "DECLARE", "alph", "num", "numd", "text", "tof", "say", "txt" };

            string variablePattern = @"DECLARE\s+(num|text|tof|alph|numd)\s+(\w+)\s*=";

            MatchCollection matches = Regex.Matches(cobraCode, variablePattern);
            foreach (Match match in matches)
            {
                string variableName = match.Groups[2].Value;
                if (Array.Exists(reservedKeywords, keyword => keyword.Equals(variableName, StringComparison.OrdinalIgnoreCase)))
                {
                    int lineNumber = GetLineNumber(cobraCode, match.Index);
                    throw new ArgumentException($"Error: '{variableName}' is a reserved keyword and cannot be used as an identifier (Line {lineNumber}).");
                }
            }

            initializeTokens(ref cobraCode);



            return txtFunctionDefinition + cobraCode;
        }
        
        private void InterpretCobraCode(string code)
        {
            var variables = new Dictionary<string, object>();

            variables.Clear(); // Clear variables before interpreting new code
            var lines = code.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i].Trim();

                if (string.IsNullOrWhiteSpace(line))
                    continue;

                if (line.StartsWith("DECLARE", StringComparison.OrdinalIgnoreCase))
                {
                    ProcessDeclaration(line, i + 1);
                }
                else if (line.StartsWith("say(", StringComparison.OrdinalIgnoreCase))
                {
                    ProcessSayFunction(line, i + 1);
                }
                else if (line.StartsWith("pag", StringComparison.OrdinalIgnoreCase))
                {
                    ProcessIfStatement(line, i + 1);
                }
                else if (line.StartsWith("kung", StringComparison.OrdinalIgnoreCase))
                {
                    ProcessIfElseStatement(line, i + 1);
                }
                else if (line.StartsWith("lipat", StringComparison.OrdinalIgnoreCase))
                {
                    ProcessSwitchStatement(line, i + 1);
                }
                else if (line.StartsWith("hinto", StringComparison.OrdinalIgnoreCase))
                {
                    break; // Exit loop on 'hinto'
                }
                else
                {
                    throw new Exception($"Syntax error on line {i + 1}: Unknown statement.");
                }
            }
        }

        private void ProcessDeclaration(string line, int lineNumber)
        {
            var variables = new Dictionary<string, object>();
            // Example: DECLARE text name = "ARJO" + " LADIA"
            var declarePattern = new Regex(@"^\s*DECLARE\s+(text|num|tof|alph|numd)\s+(\w+)\s*=\s*(.+)$", RegexOptions.IgnoreCase);
            var match = declarePattern.Match(line);

            if (!match.Success)
                throw new Exception($"Syntax error on line {lineNumber}: Invalid declaration.");

            string dataType = match.Groups[1].Value.ToLower();
            string identifier = match.Groups[2].Value;
            string value = match.Groups[3].Value.Trim();

            object parsedValue = ParseValue(dataType, value);

            variables[identifier] = parsedValue;
        }

        private object ParseValue(string dataType, string value)
        {
            if (dataType.Equals("text", StringComparison.OrdinalIgnoreCase))
            {
                // Handle text (string) values
                if (value.StartsWith("\"") && value.EndsWith("\""))
                {
                    return value.Substring(1, value.Length - 2); // Remove surrounding quotes
                }
                else
                {
                    throw new Exception($"Invalid text value: {value}");
                }
            }
            else if (dataType.Equals("num", StringComparison.OrdinalIgnoreCase))
            {
                // Handle numeric (int) values
                if (int.TryParse(value, out int intValue))
                {
                    return intValue;
                }
                else
                {
                    throw new Exception($"Invalid num value: {value}");
                }
            }
            else if (dataType.Equals("tof", StringComparison.OrdinalIgnoreCase))
            {
                // Handle boolean (bool) values
                if (bool.TryParse(value, out bool boolValue))
                {
                    return boolValue;
                }
                else
                {
                    throw new Exception($"Invalid tof value: {value}");
                }
            }
            else if (dataType.Equals("alph", StringComparison.OrdinalIgnoreCase))
            {
                // Handle character (char) values
                if (value.Length == 1 && value.StartsWith("'") && value.EndsWith("'"))
                {
                    return value[0];
                }
                else
                {
                    throw new Exception($"Invalid alph value: {value}");
                }
            }
            else if (dataType.Equals("numd", StringComparison.OrdinalIgnoreCase))
            {
                // Handle decimal (double) values
                if (double.TryParse(value, out double doubleValue))
                {
                    return doubleValue;
                }
                else
                {
                    throw new Exception($"Invalid numd value: {value}");
                }
            }
            else
            {
                throw new Exception($"Unknown data type: {dataType}");
            }
        }

        private void ProcessSayFunction(string line, int lineNumber)
        {
            // Example: say("Welcome " + name)
            var sayPattern = new Regex(@"^\s*say\((.+)\)\s*$", RegexOptions.IgnoreCase);
            var match = sayPattern.Match(line);

            if (!match.Success)
                throw new Exception($"Syntax error on line {lineNumber}: Invalid say statement.");

            string expression = match.Groups[1].Value.Trim();

            string evaluatedString = EvaluateExpression(expression);

            ShowOutput(evaluatedString);
        }

        private string EvaluateExpression(string expression)
        {
            var variables = new Dictionary<string, object>();
            var variablePattern = new Regex(@"\b(\w+)\b");
            var evaluatedExpression = new StringBuilder();
            int lastIndex = 0;

            foreach (Match match in variablePattern.Matches(expression))
            {
                string variableName = match.Groups[1].Value;
                int index = match.Index;

                // Append text before the variable
                evaluatedExpression.Append(expression.Substring(lastIndex, index - lastIndex));

                if (variables.ContainsKey(variableName))
                {
                    evaluatedExpression.Append(variables[variableName].ToString());
                }
                else if (variableName == "True" || variableName == "False")
                {
                    evaluatedExpression.Append(variableName);
                }
                else if (variableName.StartsWith("\"") && variableName.EndsWith("\""))
                {
                    evaluatedExpression.Append(variableName);
                }
                else if (variableName.StartsWith("'") && variableName.EndsWith("'") && variableName.Length == 3)
                {
                    evaluatedExpression.Append(variableName);
                }
                else
                {
                    throw new Exception($"Unknown variable: {variableName}");
                }

                lastIndex = index + variableName.Length;
            }

            evaluatedExpression.Append(expression.Substring(lastIndex));

            return evaluatedExpression.ToString().Trim('"');
        }

        private void ProcessIfStatement(string line, int lineNumber)
        {
            var i = 0;
            var ifPattern = new Regex(@"^\s*pag\s*\((.+)\)\s*:\s*$", RegexOptions.IgnoreCase);
            var match = ifPattern.Match(line);

            if (!match.Success)
                throw new Exception($"Syntax error on line {lineNumber}: Invalid pag statement.");

            string condition = match.Groups[1].Value.Trim();

            if (EvaluateCondition(condition))
            {
                i++;
            }
        }

        private void ProcessIfElseStatement(string line, int lineNumber)
        {
            var i = 0;
            var ifElsePattern = new Regex(@"^\s*kung\s*\((.+)\)\s*:\s*$", RegexOptions.IgnoreCase);
            var match = ifElsePattern.Match(line);

            if (!match.Success)
                throw new Exception($"Syntax error on line {lineNumber}: Invalid kung statement.");

            string condition = match.Groups[1].Value.Trim();

            if (EvaluateCondition(condition))
            {
                i++;
            }
            else
            {
                i++;
            }
        }

        private void ProcessSwitchStatement(string line, int lineNumber)
        {
            var variables = new Dictionary<string, object>();
            var i = 0;
            var switchPattern = new Regex(@"^\s*lipat\s+(\w+)\s*:\s*$", RegexOptions.IgnoreCase);
            var match = switchPattern.Match(line);

            if (!match.Success)
                throw new Exception($"Syntax error on line {lineNumber}: Invalid lipat statement.");

            string switchVariable = match.Groups[1].Value.Trim();

            if (variables.ContainsKey(switchVariable))
            {
                // Execute switch case
                i++;
            }
        }

        private bool EvaluateCondition(string condition)
        {
            // Implement condition evaluation logic here
            return true; // Placeholder, replace with actual evaluation
        }

        private void ShowOutput(string output)
        {
            Form2 outputForm = new Form2();
            outputForm.SetOutput(output);
            outputForm.Show();
            openOutputForms.Add(outputForm);
        }

        private void ShowErrorOutput(string error)
        {
            MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ShowSuccessOutput()
        {
            MessageBox.Show("Code executed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private int GetLineNumber(string text, int charIndex)
        {
            int lineNumber = 1;
            for (int i = 0; i < charIndex; i++)
            {
                if (text[i] == '\n')
                {
                    lineNumber++;
                }
            }
            return lineNumber;
        }
    }

}
