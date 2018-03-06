using System;
 using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace FaceRecognition
{
    public partial class frmEnrollment : Form
    {
        Image<Bgr, Byte> currentFrame;
        CaptureType grabber;
        HaarCascade face;
        HaarCascade eye;
        MCvFont font= new MCvFont(FONT.CV_FONT_HERSHEY_TRIPLEX, 0.5d, 0.5d);
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
            face = new HaarCascade("haarcascade_frontalface_alt_tree.xml");
            eye = new HaarCascade("haarcascade_eye.xml");
            try
            {
                string Labelsinfo = File.ReadAllText(Application.StartupPath + "/TrainedFaces/Trainedlabels.txt");
                string[] labels = Labelsinfo.Split('%');
                Numlabels = Convert.ToInt16(labels[0]);
                ContTrain = Numlabels;
                string LoadFaces;
                for (int tf = 1; tf < Numlabels + 1; tf++)
                {
                    LoadFaces = " face" + tf + ".bmp";
                    trainingImages.Add(new Image<Gray, byte>(Application.StartupPath + "/TrainedFaces" + LoadFaces));
                    labels.Add(labels[tf]);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("khong co du lieu khuon mat.Vui lang dang ki 1 khuon mat", "thong bao", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            frmEnrollment frm = new frmEnrollment();
            frm.ShowDialog();
        }

     

        void FrameGrabber(object sender, EventArgs e)
        {
            label3.Text = "0";
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


                label3.Text = faceDetected[0].Length.ToString();
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
                //imageBoxframeGrabber.Image = currentframe;
                //NamePersons.Clear();
            }
            imageBoxFrameGrabber.Image = currentFrame;
            label4.Txet = names;
            names = "";

            NamePersons.Clear();
        }
        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            grabber = new Capture();
            grabber.QueryFrame();
            Application.Idle += new EventHandler(FrameGrabber);
        }
        void AboutToolStripMenuItemClick(object sender, EventArgs e)
        {
            frmAbout f = new frmAbout();
            f.ShowDialog();
        }
        void ExitToolStripMenuItemClick(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}








