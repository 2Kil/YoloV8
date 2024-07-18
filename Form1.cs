using System.Diagnostics;
using Compunet.YoloV8;
using Compunet.YoloV8.Plotting;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Masuit.Tools.Hardware;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private YoloV8Predictor predictor;
        private SixLabors.ImageSharp.Image? image;
        private SixLabors.ImageSharp.Image? ploted;
        private Compunet.YoloV8.Data.DetectionResult? result;
        private string detectModel = "onnx";
        private string? selectedImagePath;
        private Bitmap screenCapture = new Bitmap(640, 640);//��ȡ��Ļָ������
        public Form1()
        {
            InitializeComponent();
            LoadOnnxFilesIntoComboBox();
        }

        //ͼƬʶ��
        private void Button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";
            openFileDialog.Title = "ѡ��ͼƬ";

            //��ʾͼƬ
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                selectedImagePath = openFileDialog.FileName;
                pictureBox1.Image = System.Drawing.Image.FromFile(selectedImagePath);
                image = SixLabors.ImageSharp.Image.Load(openFileDialog.FileName);
            }
            else
            {
                return;
            }

            //��ʱ
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            //����
            for (int i = 0; i < numericUpDown1.Value; i++)
            {
                result = predictor.Detect(image);
                Console.WriteLine(result);
            }

            stopwatch.Stop();//��ʱ
            ploted = result.PlotImage(image);//���ƽ��
            var outStream = new MemoryStream();
            ploted.SaveAsBmp(outStream);
            pictureBox1.Image = new System.Drawing.Bitmap(outStream);

            label1.Text = "��ʱ" + stopwatch.ElapsedMilliseconds.ToString() + "ms";
            label2.Text = "ƽ����ʱ" + (stopwatch.ElapsedMilliseconds / numericUpDown1.Value).ToString() + "ms";
        }

        private void Onnx_CheckedChanged(object sender, EventArgs e)
        {
            if (onnx.Checked)
            {
                detectModel = "onnx";
                ncnn.Checked = false;
            }
        }

        private void Ncnn_CheckedChanged(object sender, EventArgs e)
        {
            if (ncnn.Checked)
            {
                detectModel = "ncnn";
                onnx.Checked = false;
            }
        }

        //��ͣ��Ļʶ��
        private void Button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        //��ʼ��Ļʶ��
        private void Button3_Click(object? sender, EventArgs? e)
        {
            timer1.Stop();
            timer1.Interval = (int)(numericUpDown2.Value);
            timer1.Start();
        }

        //��Ļʶ��
        private async void Timer1_Tick(object sender, EventArgs e)
        {
            // ��ȡ��Ļָ������
            Graphics graphics = Graphics.FromImage(screenCapture);
            graphics.CopyFromScreen(340, 250, 0, 0, new System.Drawing.Size(640, 640));

            //Bitmap����
            using Image<Rgba32> s1 = BitmapToImageSharpImage(screenCapture);

            //��ʱ��ʼ
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            //����
            //result = predictor.Detect(s1);
            result = await predictor.DetectAsync(s1);

            //ֹͣ��ʱ
            stopwatch.Stop();

            //���ƽ��
            PlotImage(result, screenCapture);

            label1.Text = "��ʱ" + stopwatch.ElapsedMilliseconds.ToString() + "����";
            label2.Text = SystemInfo.CpuLoad.ToString("0.##") + "%";
        }

        //BitmapתΪImageͼ
        public static Image<Rgba32> BitmapToImageSharpImage(Bitmap bitmap)
        {
            using (var memoryStream = new MemoryStream())
            {
                bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                memoryStream.Seek(0, SeekOrigin.Begin);
                return SixLabors.ImageSharp.Image.Load<Rgba32>(memoryStream);
            }
        }

        //���������ģ���ļ�
        private void LoadOnnxFilesIntoComboBox()
        {
            // ��ȡ��ǰ����Ŀ¼�����������ָ������Ŀ¼  
            string currentDirectory = Directory.GetCurrentDirectory();

            // ʹ��Directory.GetFiles������ȡ����.onnx�ļ�  
            string[] onnxFiles = Directory.GetFiles(currentDirectory + "\\ONNX", "*.onnx");

            // ���comboBox1�����е���  
            comboBox1.Items.Clear();

            // �����ļ����飬�����ļ�����ӵ�comboBox1��  
            foreach (string file in onnxFiles)
            {
                // �����ֻ������ļ�������������·��������ʹ��Path.GetFileName  
                comboBox1.Items.Add(Path.GetFileName(file));
            }
        }

        //����ѡ��ģ���ļ�������
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1 && comboBox1.SelectedItem != null)
            {
                timer1.Stop();

                //��ʼ��ģ��
                predictor = YoloV8Predictor.Create("ONNX\\" + comboBox1.SelectedItem.ToString());

                //��⵱ǰͼƬ
                Bitmap bit1 = (Bitmap)pictureBox1.Image;
                if (result == null)
                {
                    result = predictor.Detect(BitmapToImageSharpImage(bit1));
                    PlotImage(result, bit1);
                }

                //�Զ�����ʶ��
                //Button3_Click(null, null);
            }
        }

        //���Ƽ��������ʾ
        private Bitmap PlotImage(Compunet.YoloV8.Data.DetectionResult? results, Bitmap img)
        {
            var tmp = BitmapToImageSharpImage(img);

            //���ƽ��
            if (results != null)
            {
                SixLabors.ImageSharp.Image ploted = results.PlotImage(tmp);
                using MemoryStream outStream = new MemoryStream();
                ploted.SaveAsBmp(outStream);
                var s1 = new Bitmap(outStream);
                pictureBox1.Image = new Bitmap(outStream);
                return s1;
            }
            return img;
        }
    }
}