using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Windows;
using Path = System.IO.Path;

namespace PythonEnv.pages
{
    //扩展方法必须在非泛型静态类中定义 
    public static class pandaenv
    {
        public static List<T> DtToList<T>(this DataTable dt) where T : class, new()
        {
            //属性列表
            List<System.Reflection.PropertyInfo> ls_pro = new List<System.Reflection.PropertyInfo>();

            Type t = typeof(T);
            //把T里所有public属性字段和dt列名一样的放进ls_pro属性列表存放
            Array.ForEach<System.Reflection.PropertyInfo>(t.GetProperties(), p => { if (dt.Columns.IndexOf(p.Name) != -1) ls_pro.Add(p); });
            //Array.ForEach<System.Reflection.PropertyInfo>(t.GetProperties(), p => { if (dt.Columns.Contains(p.Name)) ls_pro.Add(p); });
            List<T> ls_t = new List<T>();
            foreach (DataRow dr in dt.Rows)
            {
                T ob = new T();
                //遍历出属性列表里字段在dt里的值(非空即为有值)
                ls_pro.ForEach(p => { if (dr[p.Name] != DBNull.Value) p.SetValue(ob, dr[p.Name], null); });
                ls_t.Add(ob);
            }
            return ls_t;
        }
    }

    /// <summary>
    /// Env.xaml 的交互逻辑
    /// </summary>
    public partial class Env : Window
    {
        public Env()
        {
            InitializeComponent();
            List<PipList> PipList = new List<PipList>();
            PipList.Add(new PipList() { Id = 1, Name = "pandas", Version = "1.01", NewVersion = "1.01" });
            PipList.Add(new PipList() { Id = 2, Name = "numpy", Version = "1.01", NewVersion = "1.01" });
            PipList.Add(new PipList() { Id = 3, Name = "sklearn", Version = "1.01", NewVersion = "1.01" });

            pipView.ItemsSource = PipList;
        }

        public class PipList
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public string Version { get; set; }
            public string NewVersion { get; set; }
        }




        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //连接数据库
            string path_db = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"config\panda.db");
            string con_str = @"Data Source=" + path_db + @";Version=3;";
            // MessageBox.Show(path_db);
            SQLiteConnection con = new SQLiteConnection(con_str);
            con.Open();


            string sql = @"Select * from t_pip_info";
            SQLiteCommand command = new SQLiteCommand(sql, con);

            SQLiteDataReader dr = command.ExecuteReader();


            if (dr != null)
            {
                DataTable Dt = new DataTable();

                Dt.Load(dr);
                pipView.ItemsSource = Dt.DefaultView;


                con.Close();


            }
        }

        private void env_ext_install_Click(object sender, RoutedEventArgs e)
        {
            string cmd = "pip install ";
            if (env_source.Text == "清华大学")
            {
                cmd = cmd + "-i https://pypi.tuna.tsinghua.edu.cn/simple" + " " + env_ext_name.Text;


            }
            else if (env_source.Text == "豆瓣")
            {
                cmd = cmd + "-i https://pypi.douban.com/simple" + " " + env_ext_name.Text;

            }
            else if (env_source.Text == "阿里巴巴")
            {
                cmd = cmd + "-i http://mirrors.aliyun.com/pypi/simple" + " " + env_ext_name.Text;

            }
            else if (env_source.Text == "腾讯")
            {
                cmd = cmd + "-i https://mirrors.cloud.tencent.com/pypi/simple" + " " + env_ext_name.Text;

            }
            else
            {
                cmd = cmd + env_ext_name.Text;

            }

            MessageBox.Show(cmd);
        }

        private void env_ext_uninstall_Click(object sender, RoutedEventArgs e)
        {
            string cmd = "pip uninstall " + env_ext_name.Text;
            MessageBox.Show(cmd);
        }

        private void env_ext_update_Click(object sender, RoutedEventArgs e)
        {

        }
    }

}
