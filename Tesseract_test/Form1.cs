using Tesseract;
using BitMiracle.Docotic.Pdf;
//using System.Drawing;
//using System;

namespace Tesseract_test
{
    public partial class Form1 : Form
    {
        public String filename = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();

            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png; *.pdf)|*.jpg; *.jpeg; *.gif; *.bmp; *.png; *.pdf";
            if (open.ShowDialog() == DialogResult.OK)
            {

                if (!open.FileName.Contains("pdf"))
                {
                    pictureBox1.Image = new Bitmap(open.FileName);
                }

                textBox1.Text = open.FileName;
                filename = Path.GetFileName(open.FileName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
      
            if (!textBox1.Text.Contains("pdf"))
            {
                var ocrengine = new TesseractEngine(@".\tessdata", "rus", EngineMode.Default);
                var img = Pix.LoadFromFile(textBox1.Text);
                var res = ocrengine.Process(img);
                richTextBox2.Text = res.GetText();
            }
            else
            {
                var pdf = new PdfDocument(textBox1.Text);
                PdfDrawOptions options = PdfDrawOptions.Create();
                options.BackgroundColor = new PdfRgbColor(255, 255, 255);
                options.HorizontalResolution = 500;
                options.VerticalResolution = 500;

                for (int i = 0; i < pdf.PageCount; ++i)
                {
                    string fullPath = textBox1.Text.Replace(filename, ""); //"D:\\Programming\\DotNet\\Projects\\Tesseract_test\\Tesseract_test\\Tesseract_test\\bin\\Debug\\net7.0-windows\\";
                    var ocrengine1 = new TesseractEngine(@".\tessdata", "rus", EngineMode.Default);
                    String path = fullPath + $"page_{i}.png";
                    pdf.Pages[i].Save(path, options);
                    var img1 = Pix.LoadFromFile(path);
                    var res1 = ocrengine1.Process(img1);
                    richTextBox2.Text = richTextBox2.Text + res1.GetText();
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}