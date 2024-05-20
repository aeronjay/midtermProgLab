using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace midtermProgLab
{
    public partial class Form1 : Form
    {
        private string currentFilePath;
        private Label fileNameLabel; // Add this line to define the label

        public Form1()
        {
            InitializeComponent();
            InitializeCustomComponents();
            MyRichTextBox.Resize += new EventHandler(MyRichTextBox_Resize);
            MyRichTextBox.VScroll += new EventHandler(MyRichTextBox_VScroll);
            MyRichTextBox.Paint += new PaintEventHandler(MyRichTextBox_Paint);
            MyPictureBox.Paint += new PaintEventHandler(pictureBox1_Paint);
            MyRichTextBox.TextChanged += new EventHandler(MyRichTextBox_TextChanged);
            MyRichTextBox.Text = "\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n";

            ColorWords(MyRichTextBox, "int", Color.Red);
            ColorWords(MyRichTextBox, "string", Color.Blue);
            ColorWords(MyRichTextBox, "if", Color.Green);
        }

        private void InitializeCustomComponents()
        {
            // Initialize and configure the fileNameLabel
            fileNameLabel = new Label
            {
                AutoSize = true,
                Location = new Point(54, 8), // Adjust location as needed
                Text = "New File",
                ForeColor = Color.White
            };
            this.panel3.Controls.Add(fileNameLabel);
        }

        private void MyRichTextBox_TextChanged(object sender, EventArgs e)
        {
            MyPictureBox.Invalidate();

            ColorWords(MyRichTextBox, "int", Color.Red);
            ColorWords(MyRichTextBox, "string", Color.Blue);
            ColorWords(MyRichTextBox, "if", Color.Green);
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

            const float verticalPadding = 3.0f; // Add a constant for vertical padding

            using (SolidBrush brush = new SolidBrush(Color.Gray)) // Changed color to Gray
            {
                for (int i = firstLine; i <= lastLine; i++)
                {
                    Point pos = MyRichTextBox.GetPositionFromCharIndex(MyRichTextBox.GetFirstCharIndexFromLine(i));
                    float y = pos.Y + verticalPadding; // Add padding to the Y-coordinate
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
            if (newSize < 1) newSize = 1; // Prevent font size from becoming too small
            MyRichTextBox.Font = new Font(MyRichTextBox.Font.FontFamily, newSize);
            MyPictureBox.Invalidate(); // Redraw line numbers to match new font size
        }

        private void zoomInCode_Click(object sender, EventArgs e)
        {
            AdjustFontSize(1); // Increase font size by 1
        }

        private void zoomOutCode_Click(object sender, EventArgs e)
        {
            AdjustFontSize(-1); // Decrease font size by 1
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
                fileNameLabel.Text = Path.GetFileName(path); // Update the label with the file name
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

            richTextBox.SuspendLayout();

            foreach (Match match in Regex.Matches(richTextBox.Text, pattern))
            {
                richTextBox.Select(match.Index, match.Length);
                richTextBox.SelectionColor = color;
            }

            richTextBox.Select(originalSelectionStart, originalSelectionLength);
            richTextBox.SelectionColor = originalColor;

            richTextBox.ResumeLayout();
        }

        private void start_Click(object sender, EventArgs e)
        {

        }
    }
}
