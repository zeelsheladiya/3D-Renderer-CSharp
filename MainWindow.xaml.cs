using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Configuration;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HelixToolkit.Wpf;
using System.Windows.Media.Media3D;
using Microsoft.Win32;
using System.IO;
using System.Windows.Forms;

namespace Zeel_3D_Renderer
{
    public partial class MainWindow : Window
    {
        public string MODEL_PATH = "../../car.obj";
        public ModelVisual3D device3D = new ModelVisual3D();

        public MainWindow()
        {
            InitializeComponent();
            device3D.Content = null;
            viewPort3d.Children.Add(device3D);
        }

        private Model3D Display3d(string model)
        {
            Model3D device = null;
            try
            {

                viewPort3d.RotateGesture = new MouseGesture(MouseAction.LeftClick);

                ModelImporter import = new ModelImporter();

                device = import.Load(model);
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Exception Error : " + e.StackTrace,"Error",0,MessageBoxImage.Error);
            }
            return device;
        }

        private void Load_obj_click(object sender, RoutedEventArgs e)
        {
            try
            {
                viewPort3d.Children.Remove(device3D);
                Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();
                openFileDlg.DefaultExt = ".obj";
                openFileDlg.Filter = "Text documents (.obj)|*.obj;*.stl";
                Nullable<bool> result = openFileDlg.ShowDialog();
                if (result == true)
                {
                    MODEL_PATH = openFileDlg.FileName;
                    device3D.Content = Display3d(MODEL_PATH);
                    viewPort3d.Children.Add(device3D);
                    System.Windows.MessageBox.Show("Object Loaded", "Information", 0, MessageBoxImage.Information);
                }
            }
            catch(Exception E)
            {
                System.Windows.MessageBox.Show("Object File is Corrupted! Error :- " + E.Message, "Error", 0, MessageBoxImage.Error);
                viewPort3d.Children.Remove(device3D);
            }

        }

    }
}
