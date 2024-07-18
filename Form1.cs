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
        private Bitmap screenCapture = new Bitmap(640, 640);//截取屏幕指定区域
        public Form1()
        {
            InitializeComponent();
            LoadOnnxFilesIntoComboBox();
        }

        //图片识别
        private void Button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";
            openFileDialog.Title = "选择图片";

            //显示图片
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

            //计时
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            //推理
            for (int i = 0; i < numericUpDown1.Value; i++)
            {
                result = predictor.Detect(image);
                Console.WriteLine(result);
            }

            stopwatch.Stop();//计时
            ploted = result.PlotImage(image);//绘制结果
            var outStream = new MemoryStream();
            ploted.SaveAsBmp(outStream);
            pictureBox1.Image = new System.Drawing.Bitmap(outStream);

            label1.Text = "耗时" + stopwatch.ElapsedMilliseconds.ToString() + "ms";
            label2.Text = "平均耗时" + (stopwatch.ElapsedMilliseconds / numericUpDown1.Value).ToString() + "ms";
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

        //暂停屏幕识别
        private void Button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        //开始屏幕识别
        private void Button3_Click(object? sender, EventArgs? e)
        {
            timer1.Stop();
            timer1.Interval = (int)(numericUpDown2.Value);
            timer1.Start();
        }

        //屏幕识别
        private async void Timer1_Tick(object sender, EventArgs e)
        {
            // 截取屏幕指定区域
            Graphics graphics = Graphics.FromImage(screenCapture);
            graphics.CopyFromScreen(340, 250, 0, 0, new System.Drawing.Size(640, 640));

            //Bitmap解码
            using Image<Rgba32> s1 = BitmapToImageSharpImage(screenCapture);

            //计时开始
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            //推理
            //result = predictor.Detect(s1);
            result = await predictor.DetectAsync(s1);

            //停止计时
            stopwatch.Stop();

            //绘制结果
            PlotImage(result, screenCapture);

            label1.Text = "耗时" + stopwatch.ElapsedMilliseconds.ToString() + "毫秒";
            label2.Text = SystemInfo.CpuLoad.ToString("0.##") + "%";
        }

        //Bitmap转为Image图
        public static Image<Rgba32> BitmapToImageSharpImage(Bitmap bitmap)
        {
            using (var memoryStream = new MemoryStream())
            {
                bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                memoryStream.Seek(0, SeekOrigin.Begin);
                return SixLabors.ImageSharp.Image.Load<Rgba32>(memoryStream);
            }
        }

        //下拉框遍历模型文件
        private void LoadOnnxFilesIntoComboBox()
        {
            // 获取当前工作目录，或者你可以指定其他目录  
            string currentDirectory = Directory.GetCurrentDirectory();

            // 使用Directory.GetFiles方法获取所有.onnx文件  
            string[] onnxFiles = Directory.GetFiles(currentDirectory + "\\ONNX", "*.onnx");

            // 清除comboBox1中现有的项  
            comboBox1.Items.Clear();

            // 遍历文件数组，并将文件名添加到comboBox1中  
            foreach (string file in onnxFiles)
            {
                // 如果你只想添加文件名而不是完整路径，可以使用Path.GetFileName  
                comboBox1.Items.Add(Path.GetFileName(file));
            }
        }

        //下拉选择模型文件并加载
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1 && comboBox1.SelectedItem != null)
            {
                timer1.Stop();

                //初始化模型
                predictor = YoloV8Predictor.Create("ONNX\\" + comboBox1.SelectedItem.ToString());

                //检测当前图片
                Bitmap bit1 = (Bitmap)pictureBox1.Image;
                if (result == null)
                {
                    result = predictor.Detect(BitmapToImageSharpImage(bit1));
                    PlotImage(result, bit1);
                }

                //自动开启识别
                //Button3_Click(null, null);
            }
        }

        //绘制检测结果并显示
        private Bitmap PlotImage(Compunet.YoloV8.Data.DetectionResult? results, Bitmap img)
        {
            var tmp = BitmapToImageSharpImage(img);

            //绘制结果
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