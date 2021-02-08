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
using System.IO;
using Path = System.IO.Path;

namespace WpfFileDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// 
    /// https://docs.microsoft.com/en-us/dotnet/standard/io/how-to-read-and-write-to-a-newly-created-data-file
    /// </summary>
    public partial class MainWindow : Window
    {
        private string fileName = "test.txt";
        private string pathAndFileName;
        public MainWindow()
        {
            InitializeComponent();
            // Set a variable to the Documents path.
            string docPath =
              Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            pathAndFileName = Path.Combine(docPath, fileName);

            FilePath.Text = pathAndFileName;
            ReadFile();
        }



        private void NewEmpty_Click(object sender, RoutedEventArgs e)
        {

            // Create a new empty file
            using (StreamWriter outputFile = new StreamWriter(pathAndFileName))
            {
                // do nothing

            }
            ReadFile();
        }
        private void AppendLine_Click(object sender, RoutedEventArgs e)
        {

            // Append text to an existing file named.
            using (StreamWriter outputFile = new StreamWriter(pathAndFileName, true))
            {
                outputFile.WriteLine(TextToAppend.Text);
            }
            ReadFile();
        }

        private void NewWithContent_Click(object sender, RoutedEventArgs e)
        {
            // Create a string array with the lines of text
            string[] lines = { "First line", "Second line", "Third line" };


            // Write the string array to a new file.
            using (StreamWriter outputFile = new StreamWriter(pathAndFileName))
            {
                foreach (string line in lines)
                    outputFile.WriteLine(line);
            }
            ReadFile();
        }
        /// <summary>
        /// Read and display the file.
        /// </summary>
        private void ReadFileAllAtOnce()
        {
            try
            {
                // Open the text file using a stream reader.
                using (var sr = new StreamReader(pathAndFileName))
                {
                    // Read the stream as a string, and write the string to GUI.
                    FileContent.Text = sr.ReadToEnd();
                }
            }
            catch (IOException e)
            {
                FileContent.Text = "The file could not be read:" + e.Message;
            }
        }
        /// <summary>
        /// Read and display the file. 
        /// </summary>
        private void ReadFile()
        {
            try
            {
                // Open the text file using a stream reader.
                using (var sr = new StreamReader(pathAndFileName))
                {
                    // Read the file row by row. Display in GUI.
                    FileContent.Text = "";
                    // Read first line in file. The method ReadLine will return null when the end of file is reached.
                    string row = sr.ReadLine();
                    while (row != null)
                    {
                        FileContent.Text += row + Environment.NewLine;
                        row = sr.ReadLine(); // Will read next line.
                    }
                }
            }
            catch (IOException e)
            {
                FileContent.Text = "The file could not be read:" + e.Message;
            }
        }
    }
}
