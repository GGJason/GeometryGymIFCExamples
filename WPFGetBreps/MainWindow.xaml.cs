using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GeometryGym.Ifc;
using Microsoft.Win32;
using System.IO;
using System.Diagnostics;
namespace WPFGetBreps
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void openFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "ifc|*.ifc";
            ofd.ShowDialog();
            
            DatabaseIfc db = new DatabaseIfc(ofd.FileName);
            IfcProject project = db.Project;
            IfcSpatialElement rootElement = project.RootElement;
            List<IfcBuildingElement> elements = project.Extract<IfcBuildingElement>();
            List<IfcFacetedBrep> breps = new List<IfcFacetedBrep>();
            foreach (IfcBuildingElement element in elements)
            {
                //outputLabel.Content += element.Name+ "\n";
                IfcProductRepresentation representation = element.Representation;
                if (representation != null)
                {
                    foreach (IfcRepresentation rep in representation.Representations)
                    {
                        IfcShapeRepresentation sr = rep as IfcShapeRepresentation;
                        if (sr != null)
                        {
                            foreach (IfcRepresentationItem item in sr.Items)
                            {
                                Debug.WriteLine(item.GetType());
                                IfcFacetedBrep fb = item as IfcFacetedBrep;
                                if (fb != null)
                                {
                                    breps.Add(fb);
                                    outputLabel.Content += fb + "\n";
                                    /// Each IfcFacetedBrep has one IfcClosedShell called "Outer".
                                    /// IfcClosedShell has several IfcFaces called CfsFaces
                                    //foreach (var face in fb.Outer.CfsFaces)
                                    //{
                                    //    /// IfcFace has several IfcFaceOuterBounds called bounds
                                    //    foreach(var bound in face.Bounds)
                                    //    {
                                    //        /// Each IfcFaceOuterBound inherited from IfcFacetedBrep has IfcPolyLoop
                                    //        var loop = bound.Bound as IfcPolyloop;
                                    //        if(loop != null)
                                    //        {
                                    //            foreach(var point in loop.Polygon)
                                    //            {
                                    //               //point.Coordinates;
                                    //                outputLabel.Content += point.Coordinates + "\n";
                                    //            }
                                    //        }
                                    //    }
                                    //}
                                    //outputLabel.Content += fb.ToString() + "\n";
                                }
                            }

                        }
                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.FileName = "output.txt";

            sfd.ShowDialog();
            StreamWriter sw = new StreamWriter(sfd.FileName);
            sw.Write(outputLabel.Content);
            sw.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            outputLabel.Content = null;
        }
    }
}
