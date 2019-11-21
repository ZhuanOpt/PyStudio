
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Highlighting;
using Microsoft.Win32;
using PythonEnv.pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace PythonEnv
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();




        }
        string path_app = AppDomain.CurrentDomain.BaseDirectory;

        private void NewCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            NewItem newwin = new NewItem();
            newwin.Show();
        }
        private void OpenCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void OpenCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Python文件(*.py)|*.py|全部文件(*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (openFileDialog.ShowDialog() == true)
                textEditor.Text = File.ReadAllText(openFileDialog.FileName);
        }
        private void SaveCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void SaveCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "文本文件(*.txt)|*.txt|Python文件(*.py)|*.py";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (saveFileDialog.ShowDialog() == true)

                File.WriteAllText(saveFileDialog.FileName, textEditor.Text);

        }

        private void Menu_save_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Menu_setting_Click(object sender, RoutedEventArgs e)
        {
            Opinion opinion = new Opinion();
            opinion.Show();
        }

        private void Show_left_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Menu_import_Click(object sender, RoutedEventArgs e)
        {
            Import import = new Import();
            import.Show();
        }

        private void Python_env_Click(object sender, RoutedEventArgs e)
        {
            Env env = new Env();
            env.Show();
        }

        private void Readme_Click(object sender, RoutedEventArgs e)
        {
            Readme readme = new Readme();
            readme.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            current_fiel_path.Text = path_app;



        }

        private void Py_test_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Ex_test_Click(object sender, RoutedEventArgs e)
        {
            //Process.Start(@"D:\Programs\Python\Python37\Lib\idlelib\idle.pyw");
            //Process.Start("cmd.exe", "/k echo 欢迎使用Pandax Studio命令行工具");

            System.Diagnostics.Process process = new Process(); //创建进程对象 
            process.StartInfo.FileName = "python.exe";
            process.StartInfo.Arguments = @"D:\Programs\Python\Python37\Lib\idlelib\idle.pyw";
            process.StartInfo.UseShellExecute = false; //不使用系统外壳程序启动 
            process.StartInfo.CreateNoWindow = true;   //是否在新窗口中启动该进程的值 
            process.StartInfo.RedirectStandardInput = true; //重定向输入（一定是true） 
            process.StartInfo.RedirectStandardOutput = true; //重定向输出 
            process.StartInfo.RedirectStandardError = true;  //重定向错误流
            process.Start();
            pyOutput.Text = process.StandardOutput.ReadToEnd();
            process.Close();

        }

        private void Menu_exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        //设置已选文本提示
        private void TextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.TextBox textBox = sender as System.Windows.Controls.TextBox;
            int row = textBox.GetLineIndexFromCharacterIndex(textBox.CaretIndex);
            int col = textBox.CaretIndex - textBox.GetCharacterIndexFromLineIndex(row);


            txtStatus_row.Text = $"行：{ row + 1}";
            txtStatus_col.Text = $"列：{ col + 1}"; ;
            txtStatus_select.Text = "已选：" + textBox.SelectionLength;
        }




        private void Btn_toolbar_python(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start("python.exe");
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Check_update_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("当前已是最新版本！", "检查更新", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Check_officesite_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start("http://www.py2cn.com");
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Go_python_site_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start("https://www.python.org/");
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Go_pypi_site_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start("https://pypi.org/");
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Go_pandas_site_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start("http://pandas.pydata.org/");
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Go_sklearn_site_Click(object sender, RoutedEventArgs e)
        {


            try
            {
                Process.Start("http://scikit-learn.org/");
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Help_doc_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start("http://www.py2cn.com/");
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Python_run_Click(object sender, RoutedEventArgs e)
        {
            string path_app = AppDomain.CurrentDomain.BaseDirectory;
            string file_path = path_app + @"temp.py";
            ClearTxt(file_path); //清空文件内容

            //保存文件
            StreamWriter sw = new StreamWriter(file_path, true);
            sw.WriteLine(textEditor.Text);
            sw.Close();





            System.Diagnostics.Process process = new Process(); //创建进程对象 
            process.StartInfo.FileName = "pythonw.exe";
            process.StartInfo.Arguments = file_path;
            process.StartInfo.UseShellExecute = false; //不使用系统外壳程序启动 
            process.StartInfo.CreateNoWindow = true;   //是否在新窗口中启动该进程的值 
            process.StartInfo.RedirectStandardInput = true; //重定向输入（一定是true） 
            process.StartInfo.RedirectStandardOutput = true; //重定向输出 
            process.StartInfo.RedirectStandardError = true;  //重定向错误流
            process.Start();
            pyOutput.Text = process.StandardOutput.ReadToEnd();
            process.Close();
        }

        //清空文件内容
        public void ClearTxt(String txtPath)
        {

            FileStream stream = File.Open(txtPath, FileMode.OpenOrCreate, FileAccess.Write);
            stream.Seek(0, SeekOrigin.Begin);
            stream.SetLength(0);
            stream.Close();
        }

        private void Cmd_run_Click(object sender, RoutedEventArgs e)
        {
            //加载.exe文件
            Process.Start("cmd.exe", "/k echo 欢迎使用Py2cn命令行工具");


        }

        private void ipython_run_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("ipython.exe");
        }

        private void Btn_ext_Click(object sender, RoutedEventArgs e)
        {
            Env env = new Env();
            env.Show();
        }

        private void btn_view_data_Click(object sender, RoutedEventArgs e)
        {
            right_frame.Navigate(new Uri("Pages/Data/Data.xaml", UriKind.Relative));
        }

        private void btn_stat_Click(object sender, RoutedEventArgs e)
        {
            right_frame.Navigate(new Uri("Pages/Stat/Stat.xaml", UriKind.Relative));
        }

        private void btn_Visualize_Click(object sender, RoutedEventArgs e)
        {
            right_frame.Navigate(new Uri("Pages/Plot/Visualize.xaml", UriKind.Relative));
        }

        private void btn_model_Click(object sender, RoutedEventArgs e)
        {
            right_frame.Navigate(new Uri("Pages/Model/Model.xaml", UriKind.Relative));
        }

        private void test_run_Click(object sender, RoutedEventArgs e)
        {
            right_frame.Navigate(new Uri("Pages/prop.xaml", UriKind.Relative));
        }

        private void btn_view_var_Click(object sender, RoutedEventArgs e)
        {
            right_frame.Navigate(new Uri("Pages/Var.xaml", UriKind.Relative));
        }




        string currentFileName;

        void openFileClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.CheckFileExists = true;
            if (dlg.ShowDialog() ?? false)
            {
                currentFileName = dlg.FileName;
                textEditor.Load(currentFileName);
                textEditor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinitionByExtension(System.IO.Path.GetExtension(currentFileName));
            }
        }

        void saveFileClick(object sender, EventArgs e)
        {
            if (currentFileName == null)
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.DefaultExt = ".txt";
                if (dlg.ShowDialog() ?? false)
                {
                    currentFileName = dlg.FileName;
                }
                else
                {
                    return;
                }
            }
            textEditor.Save(currentFileName);
        }



        CompletionWindow completionWindow;



        void textEditor_TextArea_TextEntering(object sender, TextCompositionEventArgs e)
        {
            if (e.Text.Length > 0 && completionWindow != null)
            {
                if (!char.IsLetterOrDigit(e.Text[0]))
                {
                    // Whenever a non-letter is typed while the completion window is open,
                    // insert the currently selected element.
                    completionWindow.CompletionList.RequestInsertion(e);
                }
            }
            // do not set e.Handled=true - we still want to insert the character that was typed
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (currentFileName == null)
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.DefaultExt = ".txt";
                if (dlg.ShowDialog() ?? false)
                {
                    currentFileName = dlg.FileName;
                }
                else
                {
                    return;
                }
            }
            textEditor.Save(currentFileName);
        }

       
      


     

        private void jupyter_run_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process process = new Process(); //创建进程对象 
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = "/k jupyter-notebook";
            process.StartInfo.UseShellExecute = false; //不使用系统外壳程序启动 
            process.StartInfo.RedirectStandardInput = true; //重定向输入（一定是true） 
            process.StartInfo.RedirectStandardOutput = true; //重定向输出 
            process.StartInfo.CreateNoWindow = true; //不创建窗口 
            process.Start();

            process.Close();
        }
    }
}