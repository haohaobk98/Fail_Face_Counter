using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace FaceRecognition
{
    public partial class frmEnrollment : Form
    {
        Image<Bgr, Byte> currentFrame;
        Capture grabber;
        HaarCassade face;
        HaarCassade eye;
        MCvFont font = new MCvFont(FONT.CV_FONT_HERSHEY_TRIPLEX, 0.5d, 0.5d);
        Image<Gray, byte> result, TrainedFace = null;
        Image<Gray, byte> gray = null;
        List<Image<Gray, byte>> trainingImages = new List<Image<Gray, byte>>();
        List<string> labels = new List<string>();
        List<string> NamePersons = new List<string>();
        int ContTrain, Numlabels, t;
        string name, names = null;

        public frmEnrollment()
        {
            InitializeComponent();
            face = new HaarCassade("haarcassade_frontalface_alt_tree.xml");
            eye = new HaarCassade("haarcassade_eye.xml");
            try
            {
                string Labelsinfo = File.ReadAllText(Application.StartupPath + "/TrainedFace/Trainedlabels.txt");
                string[] labels = Labelsinfo.Split('%');
                Numberlabels = Convert.ToInt16(Labels[0]);
                ContTrain = Labels;
                string LoadFaces;
                for (int tf = 1; tf < NumLabels + 1; tf++)
                {
                    LoadFaces = " face" + tf + ".bmp";
                    trainingImages.Add(new Image<Gray, byte>(Application.StartupPath + "/TrainedFaces" + LoadFaces));
                    labels.Add(Labels[tf]);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("khong co du lieu khuon mat.Vui lang dang ki 1 khuon mat", "thong bao", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        void button2Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Equals(""))
            {
                MessageBox.Show("vui long nhap ten truoc khi luu....");
                return;
            }
            else
            {
                try
                {
                    ContTrain = ContTrain + 1;
                    grey = grabber.QueryFrame().Resize(320, 240, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
                    MCvAvgComp[][] facesDetected = gray.DetectHaarCasscade(
                    face,
                    1.2,
                    10,
                    Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUMING,
                    new Size(20, 20));

                    foreach (MCvAvgComp f in faceDetected[0])
                    {
                        TraniedFace = currentFrame.Copy(f.rect).Convert<Gray, byte>();
                        break;
                    }
                    TrainedFace = result.Resize(100, 100, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
                    TrainingImages.Add(TrainedFace);
                    labels.Add(textBox1.Text);

                    imageBox1.Image = TrainedFace;

                    File.writeAllText(Application.StartupPath + "/TrainedFaces/Trainedlabels.txt", trainingImages.ToArray().Length.ToString() + "%");
                    for (int i = 1; i < trainingImages.ToArray().Length + 1; i++)
                    {
                        rainingImages.ToArray()[i - 1].Save(Application.StartupPath + "TrainedFaces/face" + i + ".bmp");
                        File.AppendAllText(Application.StartupPath + "/TrainedFaces/Trainedlabels.txt", labels.ToArray()[i - 1] + "%");
                    }
                    MessageBox.Show("khuon mat duoc phat hien va them vao", "Training ok", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    MessageBox.Show("vui long chon chuc nang phat hien khuon mat truoc", "Training fail", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        void FrmEnrollmentLoad(object sender, EventArgs e)
        {
            grabber = new Capture();
            grabber.QueryFrame();
            Application.Idle += new EventHandler(FrameGrabber);
        }
        void FrameGrabber(object sender, EventArgs e)
        {

            NamePerSons.Add("");

            currentFrame = grabber.QueryFrame().Resize(320, 240, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);

            gray = currentFrame.Convert<Gray, Byte>();

            MCvAvgComp[][] facesDetected = gray.DetectHaarCasscade(
            face,
            1.2,
            10,
            Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUMING,
            new Size(20, 20));

            foreach (MCvAvgComp in faceDetected[0])
            {
                t = t + 1;
                result = currentFrame.Copy(f.rect).Convert<Gray, byte>().Resize(100, 100, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
                currentFrame.Draw(f.rect, new Bgr(Color, Red), 2);

                if (trainingImages.ToArray[].Length != 0)
                {
                    MCvTermCriteria termCrit = new MCvTermCriteria(ContTrain, 0.001);
                    EigenObjectRecognizer recognizer = new EigenObjectRecognizer(
                    trainingImages.ToArray(),
                    labels.ToArray(),
                    5000,
                    ref termCrit);
                    name = recognizer.REcognize(result);
                    cunrrentFrame.Draw(name, ref font, new Point(f.rect.X - 2, f.rect Y - 2), new Bgr(Color.LightGreen));
                }
                NamePersons[t - 1] = name;
                NamePersons.Add("");

                /*
                gray . ROI = f.rect;
                MCvAvgComp[][] eyeDetected  = gray.DetectHaarCasscade(
                  eye,
                  1.1,
                  10,
                  Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUMING,
                  new Size(20,20));
                  gray.ROI = Rectangle.Empty;
                  foreach(MCvAvgComp ey in eyeDetected[0])
                  {
                    Rectangle eyeRect  = ey.rect;
                    eyeRect.Offset(f.rect.X,f.rext.Y);
                    currentFrame.Draw(eyeRect,new Bgr(Color,Blue),2);
                  } */
            }
            t = 0;
            for (int nnn = 0; nnn < faceDetected[0]; nnn++)
            {
                names = names + NamePersons[nnn] + " , ";
            }
            imageBoxframeGrabber.Image = currentframe;
            NamePersons.Clear();
        }
    }
}









