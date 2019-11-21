using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;

namespace PythonEnv.pages
{
    /// <summary>
    /// Import.xaml 的交互逻辑
    /// </summary>
    public partial class Import : Window
    {
        public Import()
        {
            InitializeComponent();
        }

        private void Btn_import_file_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV文件(*.csv)|*.csv|全部文件(*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (openFileDialog.ShowDialog() == true)
            {
                tbox_import.Text = openFileDialog.FileName;

                tbox_import_view.Text = File.ReadAllText(openFileDialog.FileName);
            }

            Python_code();


        }
        private void Python_code()
        {
            string import_file_path = tbox_import.Text;
            //变量初始化
            string str_header = "";
            string str_nrows = "";
            string str_sep = "";
            string str_na_values = "";
            string str_encoding = "";
            string str_skiprows = "";
            string str_skipfooter = "";


            //赋值逻辑
            if (tbox_header.IsChecked == true)
            {
                str_header = ",header=0";
            }
            else
            {
                str_header = ",header=None";
            }

            if (cbox_sep.Text != null)
            {
                if (cbox_sep.Text == "逗号")
                {
                    str_sep = ",sep=\'" + "," + "\'";
                }
                else if (cbox_sep.Text == "分号")
                {
                    str_sep = ",sep=\'" + ";" + "\'";
                }
                else if (cbox_sep.Text == "空格")
                {
                    str_sep = ",sep=\'" + " " + "\'";
                }
                else if (cbox_sep.Text == "制表符")
                {
                    str_sep = ",sep=\'" + "\t" + "\'";
                }
            }

            if (tbox_skiprows.Text != null)
            {
                str_skiprows = ",skiprows=" + tbox_skiprows.Text;
            }

            if (tbox_skipfooter.Text != null)
            {
                str_skipfooter = ",skipfooter=" + tbox_skipfooter.Text;
            }


            if (tbox_nrows.Text == "全部" && tbox_nrows.Text != null)
            { str_nrows = ""; }
            else
            { str_nrows = "nrows=" + tbox_nrows.Text; }


            if (cbox_na_values.Text == "默认" || cbox_na_values.SelectedItem == null)
            { str_na_values = ""; }
            else if (cbox_na_values.Text == "NA")
            { str_na_values = ",na_values=\'" + cbox_na_values.Text + "\'"; }
            else if (cbox_na_values.Text == "null")
            { str_na_values = ",na_values=\'" + cbox_na_values.Text + "\'"; }
            else if (cbox_na_values.Text == "0")
            { str_na_values = ",na_values=\'" + cbox_na_values.Text + "\'"; }
            else if (cbox_na_values.Text == "空")
            { str_na_values = ",na_values=\'" + cbox_na_values.Text + "\'"; }


            if (cbox_encoding.SelectedItem.ToString() == "默认" || cbox_encoding.Text == "UTF-8" || cbox_encoding.Text == null)
            { str_encoding = ",encoding='utf8'"; }
            else if (cbox_encoding.SelectedItem.ToString() == "GB2312")
            { str_encoding = ",encoding=\'" + "gb2312" + "\'"; }
            else if (cbox_encoding.SelectedItem.ToString() == "GBK")
            { str_encoding = ",encoding=\'" + "gbk" + "\'"; }


            string read_csv = "import pandas as pd\ndf=pd.read_csv(\'" + import_file_path.Replace("\\", "/") + "\'" + str_header + str_sep + str_skiprows + str_skipfooter + str_nrows + str_na_values + str_encoding + ")";
            import_tbox_code.Text = read_csv;


        }

        private void import_btn_summit_Click(object sender, RoutedEventArgs e)
        {




            foreach (Window mainwindow in Application.Current.Windows)
            {
                if (mainwindow.GetType() == typeof(MainWindow))
                {
                    (mainwindow as MainWindow).textEditor.Text += import_tbox_code.Text;
                }
            }
        }
    }
}

