using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class FileManager : Form
    {
        private string filePath = @"C:/Users/Vika/Desktop/программирование";
        private bool isFile = false;
        private string currentlySelectedItemName = "";
        public FileManager()
        {
            InitializeComponent();
        }
        public void loadFilesAndDirectories()
        {
            DirectoryInfo fileList;
            string tempFilePath = "";
            FileAttributes fileAttr;
            try
            {
                if (isFile)
                {
                    tempFilePath = filePath + "/" + currentlySelectedItemName;
                    FileInfo fileDeteils = new FileInfo(tempFilePath);
                    fileAttr = File.GetAttributes(tempFilePath);

                }
                else
                {
                    fileAttr = File.GetAttributes(filePath);

                }
                if ((fileAttr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    fileList = new DirectoryInfo(filePath);
                    FileInfo[] files = fileList.GetFiles();
                    DirectoryInfo[] dirs = fileList.GetDirectories();

                    listView1.Items.Clear();

                    for (int i = 0; i < files.Length; i++)
                    {
                        listView1.Items.Add(files[i].Name);
                    }
                    for (int i = 0; i < dirs.Length; i++)
                    {
                        listView1.Items.Add(dirs[i].Name);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Другие прерывания");
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            filePathTextBox.Text = filePath;
            loadFilesAndDirectories();
        }
        public void loadButtonAction()
        {
            removeBackSlash();
            filePath = filePathTextBox.Text;
            loadFilesAndDirectories();
            isFile = false;
        }


        public void removeBackSlash()
        {
            string path = filePathTextBox.Text;
            if (path.LastIndexOf("/") == path.Length - 1)
            {
                filePathTextBox.Text = path.Substring(0, path.Length - 1);
            }

        }
        public void goBack()
        {
            try
            {
                removeBackSlash();
                string path = filePathTextBox.Text;
                path = path.Substring(0, path.LastIndexOf("/"));
                this.isFile = false;
                filePathTextBox.Text = path;
                removeBackSlash();
            }
            catch (Exception e)
            {

            }
        }

        private void goButton_Click(object sender, EventArgs e)
        {
            loadButtonAction();
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            currentlySelectedItemName = e.Item.Text;
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            loadButtonAction();
        }

        private void createFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filePath = filePathTextBox.Text;
            string path = System.IO.Path.Combine(filePath, "Created folders1");
            System.IO.Directory.CreateDirectory(path);
            MessageBox.Show("Succesfully Created");
            loadFilesAndDirectories();
        }

        private void createFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                filePath = filePathTextBox.Text;
                string file = System.IO.Path.Combine(filePath, "MyFile.txt");
                System.IO.File.Create(file);
                loadFilesAndDirectories();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Другие прерывания");
            }
        }

        private void deleteFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Directory.Delete(filePathTextBox.Text);
                loadFilesAndDirectories();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Другие прерывания");
            }
        }

        private void deleteFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                File.Delete(filePathTextBox.Text);
                loadFilesAndDirectories();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Другие прерывания");
            }
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            mouseclick();
        }
        private void mouseclick()
        {
            string tempFilePath = "";
            tempFilePath = filePath + "/" + currentlySelectedItemName;
            FileInfo fileDeteils = new FileInfo(tempFilePath);
            filePathTextBox.Text = filePath + "/" + fileDeteils.Name;
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            goBack();
            loadButtonAction();
        }
    }
}