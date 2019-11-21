using Microsoft.Win32;
using System;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Windows;
using Path = System.IO.Path;
using WinForm = System.Windows.Forms;

namespace PythonEnv.pages
{
    /// <summary>
    /// Settings.xaml 的交互逻辑
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();
        }
        string path_root = AppDomain.CurrentDomain.BaseDirectory;
        //保存通用设置
        private void General_submit_Click(object sender, RoutedEventArgs e)
        {
            string path_app = set_box_pandax_path.Text;
            string path_brower = set_box_browser_path.Text;
            string path_editer = set_box_edit_path.Text;
            string path_download = set_box_download_path.Text;
            string path_log = set_box_log_path.Text;
            string path_err_log = set_box_errlog_path.Text;

            //连接数据库
            string path_db = System.IO.Path.Combine(path_root, @"config\pandax.db");
            string con_str = @"Data Source=" + path_db + @";Version=3;";
            SQLiteConnection con = new SQLiteConnection(con_str);
            con.Open();

            //更新数据的sql
            string sql_path_app = "UPDATE \"t_set_general\" SET \"var_value\" = '" + path_app + "' WHERE \"var_key\" = 'path_app';";
            string sql_path_brower = "UPDATE \"t_set_general\" SET \"var_value\" = '" + path_brower + "' WHERE \"var_key\" = 'path_brower';";
            string sql_path_editer = "UPDATE \"t_set_general\" SET \"var_value\" = '" + path_editer + "' WHERE \"var_key\" = 'path_editer';";
            string sql_path_download = "UPDATE \"t_set_general\" SET \"var_value\" = '" + path_download + "' WHERE \"var_key\" = 'path_download';";
            string sql_path_log = "UPDATE \"t_set_general\" SET \"var_value\" = '" + path_log + "' WHERE \"var_key\" = 'path_log';";
            string sql_path_err_log = "UPDATE \"t_set_general\" SET \"var_value\" = '" + path_err_log + "' WHERE \"var_key\" = 'path_err_log';";

            SQLiteCommand cmd1 = new SQLiteCommand(sql_path_app, con);
            SQLiteCommand cmd2 = new SQLiteCommand(sql_path_brower, con);
            SQLiteCommand cmd3 = new SQLiteCommand(sql_path_editer, con);
            SQLiteCommand cmd4 = new SQLiteCommand(sql_path_download, con);
            SQLiteCommand cmd5 = new SQLiteCommand(sql_path_log, con);
            SQLiteCommand cmd6 = new SQLiteCommand(sql_path_err_log, con);
            cmd1.ExecuteNonQuery();
            cmd2.ExecuteNonQuery();
            cmd3.ExecuteNonQuery();
            cmd4.ExecuteNonQuery();
            cmd5.ExecuteNonQuery();
            cmd6.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("保存成功");






        }
        //取消
        private void General_cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Server_cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Database_cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Php_cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //打开设置页面时，加载数据库中的设置
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //连接数据库
            string path_db = Path.Combine(path_root, @"config\pandax.db");
            string con_str = @"Data Source=" + path_db + @";Version=3;";
            SQLiteConnection con = new SQLiteConnection(con_str);
            con.Open();
            //通用设置
            string sql_path_app = @"SELECT var_value FROM t_set_general WHERE var_key='path_app';";
            string sql_path_brower = @"SELECT var_value FROM t_set_general WHERE var_key='path_brower';";
            string sql_path_editer = @"SELECT var_value FROM t_set_general WHERE var_key='path_editer';";
            string sql_path_download = @"SELECT var_value FROM t_set_general WHERE var_key='path_download';";
            string sql_path_log = @"SELECT var_value FROM t_set_general WHERE var_key='path_log';";
            string sql_path_err_log = @"SELECT var_value FROM t_set_general WHERE var_key='path_err_log';";

            SQLiteCommand cmd1 = new SQLiteCommand(sql_path_app, con);
            SQLiteCommand cmd2 = new SQLiteCommand(sql_path_brower, con);
            SQLiteCommand cmd3 = new SQLiteCommand(sql_path_editer, con);
            SQLiteCommand cmd4 = new SQLiteCommand(sql_path_download, con);
            SQLiteCommand cmd5 = new SQLiteCommand(sql_path_log, con);
            SQLiteCommand cmd6 = new SQLiteCommand(sql_path_err_log, con);
            SQLiteDataReader dr1 = cmd1.ExecuteReader();
            SQLiteDataReader dr2 = cmd2.ExecuteReader();
            SQLiteDataReader dr3 = cmd3.ExecuteReader();
            SQLiteDataReader dr4 = cmd4.ExecuteReader();
            SQLiteDataReader dr5 = cmd5.ExecuteReader();
            SQLiteDataReader dr6 = cmd6.ExecuteReader();
            dr1.Read();
            dr2.Read();
            dr3.Read();
            dr4.Read();
            dr5.Read();
            dr6.Read();
            set_box_pandax_path.Text = dr1.GetValue(0).ToString();
            set_box_browser_path.Text = dr2.GetValue(0).ToString();
            set_box_edit_path.Text = dr3.GetValue(0).ToString();
            set_box_download_path.Text = dr4.GetValue(0).ToString();
            set_box_log_path.Text = dr5.GetValue(0).ToString();
            set_box_errlog_path.Text = dr6.GetValue(0).ToString();

            //服务器设置
            string server_name = @"SELECT var_value FROM t_set_server WHERE var_key='server_name';";
            string server_path = @"SELECT var_value FROM t_set_server WHERE var_key='server_path';";
            string server_port = @"SELECT var_value FROM t_set_server WHERE var_key='server_port';";
            string server_indexpage = @"SELECT var_value FROM t_set_server WHERE var_key='server_indexpage';";
            string server_domin = @"SELECT var_value FROM t_set_server WHERE var_key='server_domin';";
            string server_log = @"SELECT var_value FROM t_set_server WHERE var_key='server_log';";
            string server_path_install = @"SELECT var_value FROM t_set_server WHERE var_key='server_path_install';";
            string server_err_log = @"SELECT var_value FROM t_set_server WHERE var_key='server_err_log';";
            string server_path_config = @"SELECT var_value FROM t_set_server WHERE var_key='server_path_config';";
            string server_service = @"SELECT var_value FROM t_set_server WHERE var_key='server_service';";

            SQLiteCommand cmd101 = new SQLiteCommand(server_name, con);
            SQLiteCommand cmd102 = new SQLiteCommand(server_path, con);
            SQLiteCommand cmd103 = new SQLiteCommand(server_port, con);
            SQLiteCommand cmd104 = new SQLiteCommand(server_indexpage, con);
            SQLiteCommand cmd105 = new SQLiteCommand(server_domin, con);
            SQLiteCommand cmd106 = new SQLiteCommand(server_log, con);
            SQLiteCommand cmd107 = new SQLiteCommand(server_path_install, con);
            SQLiteCommand cmd108 = new SQLiteCommand(server_err_log, con);
            SQLiteCommand cmd109 = new SQLiteCommand(server_path_config, con);
            SQLiteCommand cmd110 = new SQLiteCommand(server_service, con);
            SQLiteDataReader dr101 = cmd101.ExecuteReader();
            SQLiteDataReader dr102 = cmd102.ExecuteReader();
            SQLiteDataReader dr103 = cmd103.ExecuteReader();
            SQLiteDataReader dr104 = cmd104.ExecuteReader();
            SQLiteDataReader dr105 = cmd105.ExecuteReader();
            SQLiteDataReader dr106 = cmd106.ExecuteReader();
            SQLiteDataReader dr107 = cmd107.ExecuteReader();
            SQLiteDataReader dr108 = cmd108.ExecuteReader();
            SQLiteDataReader dr109 = cmd109.ExecuteReader();
            SQLiteDataReader dr110 = cmd110.ExecuteReader();
            dr101.Read();
            dr102.Read();
            dr103.Read();
            dr104.Read();
            dr105.Read();
            dr106.Read();
            dr107.Read();
            dr108.Read();
            dr109.Read();
            dr110.Read();

            box_server_name.Text = dr101.GetValue(0).ToString();
            box_server_www.Text = dr102.GetValue(0).ToString();
            box_server_port.Text = dr103.GetValue(0).ToString();
            box_server_index.Text = dr104.GetValue(0).ToString();
            box_server_domin.Text = dr105.GetValue(0).ToString();
            box_server_log.Text = dr106.GetValue(0).ToString();
            box_server_install_dir.Text = dr107.GetValue(0).ToString();
            box_server_err_log.Text = dr108.GetValue(0).ToString();
            box_server_config.Text = dr109.GetValue(0).ToString();

            if (dr110.GetValue(0).ToString() == "true")
            {
                server_install_service.IsChecked = true;
            }
            else
            {
                server_install_service.IsChecked = false;
            }

            //mysql
            string database_name = @"SELECT var_value FROM t_set_database WHERE var_key='database_name';";
            string database_path_install = @"SELECT var_value FROM t_set_database WHERE var_key='database_path_install';";
            string database_port = @"SELECT var_value FROM t_set_database WHERE var_key='database_port';";
            string database_old_password = @"SELECT var_value FROM t_set_database WHERE var_key='database_old_password';";
            string database_new_password = @"SELECT var_value FROM t_set_database WHERE var_key='database_new_password';";
            string database_bak_name = @"SELECT var_value FROM t_set_database WHERE var_key='database_bak_name';";
            string database_bak_path = @"SELECT var_value FROM t_set_database WHERE var_key='database_bak_path';";
            string database_new_name = @"SELECT var_value FROM t_set_database WHERE var_key='database_new_name';";
            string database_log = @"SELECT var_value FROM t_set_database WHERE var_key='database_log';";
            string database_err_log = @"SELECT var_value FROM t_set_database WHERE var_key='database_err_log';";
            string database_path_config = @"SELECT var_value FROM t_set_database WHERE var_key='database_path_config';";
            string database_tool = @"SELECT var_value FROM t_set_database WHERE var_key='database_tool';";
            string database_service = @"SELECT var_value FROM t_set_database WHERE var_key='database_service';";

            SQLiteCommand cmd201 = new SQLiteCommand(database_name, con);
            SQLiteCommand cmd202 = new SQLiteCommand(database_path_install, con);
            SQLiteCommand cmd203 = new SQLiteCommand(database_port, con);
            SQLiteCommand cmd204 = new SQLiteCommand(database_old_password, con);
            SQLiteCommand cmd205 = new SQLiteCommand(database_new_password, con);
            SQLiteCommand cmd206 = new SQLiteCommand(database_bak_name, con);
            SQLiteCommand cmd207 = new SQLiteCommand(database_bak_path, con);
            SQLiteCommand cmd208 = new SQLiteCommand(database_new_name, con);
            SQLiteCommand cmd209 = new SQLiteCommand(database_log, con);
            SQLiteCommand cmd210 = new SQLiteCommand(database_err_log, con);
            SQLiteCommand cmd211 = new SQLiteCommand(database_path_config, con);
            SQLiteCommand cmd212 = new SQLiteCommand(database_tool, con);
            SQLiteCommand cmd213 = new SQLiteCommand(database_service, con);
            SQLiteDataReader dr201 = cmd201.ExecuteReader();
            SQLiteDataReader dr202 = cmd202.ExecuteReader();
            SQLiteDataReader dr203 = cmd203.ExecuteReader();
            SQLiteDataReader dr204 = cmd204.ExecuteReader();
            SQLiteDataReader dr205 = cmd205.ExecuteReader();
            SQLiteDataReader dr206 = cmd206.ExecuteReader();
            SQLiteDataReader dr207 = cmd207.ExecuteReader();
            SQLiteDataReader dr208 = cmd208.ExecuteReader();
            SQLiteDataReader dr209 = cmd209.ExecuteReader();
            SQLiteDataReader dr210 = cmd210.ExecuteReader();
            SQLiteDataReader dr211 = cmd211.ExecuteReader();
            SQLiteDataReader dr212 = cmd212.ExecuteReader();
            SQLiteDataReader dr213 = cmd213.ExecuteReader();
            dr201.Read();
            dr202.Read();
            dr203.Read();
            dr204.Read();
            dr205.Read();
            dr206.Read();
            dr207.Read();
            dr208.Read();
            dr209.Read();
            dr210.Read();
            dr211.Read();
            dr212.Read();
            dr213.Read();
            box_database_name.Text = dr201.GetValue(0).ToString();
            box_database_path.Text = dr202.GetValue(0).ToString();
            box_database_port.Text = dr203.GetValue(0).ToString();
            box_db_old_password.Text = dr204.GetValue(0).ToString();
            box_db_new_password.Text = dr205.GetValue(0).ToString();
            box_db_bak_name.Text = dr206.GetValue(0).ToString();
            box_db_bak_file_path.Text = dr207.GetValue(0).ToString();
            box_db_target_name.Text = dr208.GetValue(0).ToString();
            box_database_log.Text = dr209.GetValue(0).ToString();
            box_database_err_log.Text = dr210.GetValue(0).ToString();
            box_database_config_path.Text = dr211.GetValue(0).ToString();
            box_database_tool.Text = dr212.GetValue(0).ToString();
            if (dr213.GetValue(0).ToString() == "true")
            {
                database_service_install.IsChecked = true;
            }
            else
            {
                database_service_install.IsChecked = false;
            }

            //php
            string php_name = @"SELECT var_value FROM t_set_php WHERE var_key='php_name';";
            string php_path = @"SELECT var_value FROM t_set_php WHERE var_key='php_path';";
            string php_tool = @"SELECT var_value FROM t_set_php WHERE var_key='php_tool';";
            string php_path_ext = @"SELECT var_value FROM t_set_php WHERE var_key='php_path_ext';";
            string php_info = @"SELECT var_value FROM t_set_php WHERE var_key='php_info';";
            string php_log = @"SELECT var_value FROM t_set_php WHERE var_key='php_log';";
            string php_err_log = @"SELECT var_value FROM t_set_php WHERE var_key='php_err_log';";
            string php_path_config = @"SELECT var_value FROM t_set_php WHERE var_key='php_path_config';";


            SQLiteCommand cmd301 = new SQLiteCommand(php_name, con);
            SQLiteCommand cmd302 = new SQLiteCommand(php_path, con);
            SQLiteCommand cmd303 = new SQLiteCommand(php_tool, con);
            SQLiteCommand cmd304 = new SQLiteCommand(php_path_ext, con);
            SQLiteCommand cmd305 = new SQLiteCommand(php_info, con);
            SQLiteCommand cmd306 = new SQLiteCommand(php_log, con);
            SQLiteCommand cmd307 = new SQLiteCommand(php_err_log, con);
            SQLiteCommand cmd308 = new SQLiteCommand(php_path_config, con);

            SQLiteDataReader dr301 = cmd301.ExecuteReader();
            SQLiteDataReader dr302 = cmd302.ExecuteReader();
            SQLiteDataReader dr303 = cmd303.ExecuteReader();
            SQLiteDataReader dr304 = cmd304.ExecuteReader();
            SQLiteDataReader dr305 = cmd305.ExecuteReader();
            SQLiteDataReader dr306 = cmd306.ExecuteReader();
            SQLiteDataReader dr307 = cmd307.ExecuteReader();
            SQLiteDataReader dr308 = cmd308.ExecuteReader();

            dr301.Read();
            dr302.Read();
            dr303.Read();
            dr304.Read();
            dr305.Read();
            dr306.Read();
            dr307.Read();
            dr308.Read();

            box_php_name.Text = dr301.GetValue(0).ToString();
            box_php_path.Text = dr302.GetValue(0).ToString();
            box_php_tool.Text = dr303.GetValue(0).ToString();
            box_php_ext_path.Text = dr304.GetValue(0).ToString();
            box_php_info.Text = dr305.GetValue(0).ToString();
            box_php_log.Text = dr306.GetValue(0).ToString();
            box_php_err_log.Text = dr307.GetValue(0).ToString();
            box_php_config_path.Text = dr308.GetValue(0).ToString();

            //关闭数据库连接

            con.Close();
        }

        private void Server_submit_Click(object sender, RoutedEventArgs e)
        {
            string server_name = box_server_name.Text;
            string server_path = box_server_www.Text;
            string server_port = box_server_port.Text;
            string server_indexpage = box_server_index.Text;
            string server_domin = box_server_domin.Text;
            string server_log = box_server_log.Text;
            string server_path_install = box_server_install_dir.Text;
            string server_err_log = box_server_err_log.Text;
            string server_path_config = box_server_config.Text;

            string server_service;
            if (server_install_service.IsChecked == true)
            {
                server_service = "true";
            }
            else
            {
                server_service = "false";
            }


            //连接数据库
            string path_db = Path.Combine(path_root, @"config\pandax.db");
            string con_str = @"Data Source=" + path_db + @";Version=3;";
            SQLiteConnection con = new SQLiteConnection(con_str);
            con.Open();

            //更新数据的sql
            string sql_server_name = "UPDATE \"t_set_server\" SET \"var_value\" = '" + server_name + "' WHERE \"var_key\" = 'server_name';";
            string sql_server_path = "UPDATE \"t_set_server\" SET \"var_value\" = '" + server_path + "' WHERE \"var_key\" = 'server_path';";
            string sql_server_port = "UPDATE \"t_set_server\" SET \"var_value\" = '" + server_port + "' WHERE \"var_key\" = 'server_port';";
            string sql_server_indexpage = "UPDATE \"t_set_server\" SET \"var_value\" = '" + server_indexpage + "' WHERE \"var_key\" = 'server_indexpage';";
            string sql_server_domin = "UPDATE \"t_set_server\" SET \"var_value\" = '" + server_domin + "' WHERE \"var_key\" = 'server_domin';";
            string sql_server_log = "UPDATE \"t_set_server\" SET \"var_value\" = '" + server_log + "' WHERE \"var_key\" = 'server_log';";
            string sql_server_path_install = "UPDATE \"t_set_server\" SET \"var_value\" = '" + server_path_install + "' WHERE \"var_key\" = 'server_path_install';";
            string sql_server_err_log = "UPDATE \"t_set_server\" SET \"var_value\" = '" + server_err_log + "' WHERE \"var_key\" = 'server_err_log';";
            string sql_server_path_config = "UPDATE \"t_set_server\" SET \"var_value\" = '" + server_path_config + "' WHERE \"var_key\" = 'server_path_config';";
            string sql_server_service = "UPDATE \"t_set_server\" SET \"var_value\" = '" + server_service + "' WHERE \"var_key\" = 'server_service';";

            SQLiteCommand cmd1 = new SQLiteCommand(sql_server_name, con);
            SQLiteCommand cmd2 = new SQLiteCommand(sql_server_path, con);
            SQLiteCommand cmd3 = new SQLiteCommand(sql_server_port, con);
            SQLiteCommand cmd4 = new SQLiteCommand(sql_server_indexpage, con);
            SQLiteCommand cmd5 = new SQLiteCommand(sql_server_domin, con);
            SQLiteCommand cmd6 = new SQLiteCommand(sql_server_log, con);
            SQLiteCommand cmd7 = new SQLiteCommand(sql_server_path_install, con);
            SQLiteCommand cmd8 = new SQLiteCommand(sql_server_err_log, con);
            SQLiteCommand cmd9 = new SQLiteCommand(sql_server_path_config, con);
            SQLiteCommand cmd10 = new SQLiteCommand(sql_server_service, con);
            cmd1.ExecuteNonQuery();
            cmd2.ExecuteNonQuery();
            cmd3.ExecuteNonQuery();
            cmd4.ExecuteNonQuery();
            cmd5.ExecuteNonQuery();
            cmd6.ExecuteNonQuery();
            cmd7.ExecuteNonQuery();
            cmd8.ExecuteNonQuery();
            cmd9.ExecuteNonQuery();
            cmd10.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("保存成功");
        }

        private void Database_submit_Click(object sender, RoutedEventArgs e)
        {
            string database_name = box_database_name.Text;
            string database_path_install = box_database_path.Text;
            string database_port = box_database_port.Text;
            string database_old_password = box_db_old_password.Text;
            string database_new_password = box_db_new_password.Text;
            string database_bak_name = box_db_bak_name.Text;
            string database_bak_path = box_db_bak_file_path.Text;
            string database_new_name = box_db_target_name.Text;
            string database_log = box_database_log.Text;
            string database_err_log = box_database_err_log.Text;
            string database_path_config = box_database_config_path.Text;
            string database_tool = box_database_tool.Text;
            string database_service;

            if (database_service_install.IsChecked == true)
            {
                database_service = "true";
            }
            else
            {
                database_service = "false";
            }


            //连接数据库
            string path_db = Path.Combine(path_root, @"config\pandax.db");
            string con_str = @"Data Source=" + path_db + @";Version=3;";
            SQLiteConnection con = new SQLiteConnection(con_str);
            con.Open();

            //更新数据的sql
            string sql_database_name = "UPDATE \"t_set_database\" SET \"var_value\" = '" + database_name + "' WHERE \"var_key\" = 'database_name';";
            string sql_database_path_install = "UPDATE \"t_set_database\" SET \"var_value\" = '" + database_path_install + "' WHERE \"var_key\" = 'database_path_install';";
            string sql_database_port = "UPDATE \"t_set_database\" SET \"var_value\" = '" + database_port + "' WHERE \"var_key\" = 'database_port';";
            string sql_database_old_password = "UPDATE \"t_set_database\" SET \"var_value\" = '" + database_old_password + "' WHERE \"var_key\" = 'database_old_password';";
            string sql_database_new_password = "UPDATE \"t_set_database\" SET \"var_value\" = '" + database_new_password + "' WHERE \"var_key\" = 'database_new_password';";
            string sql_database_bak_name = "UPDATE \"t_set_database\" SET \"var_value\" = '" + database_bak_name + "' WHERE \"var_key\" = 'database_bak_name';";
            string sql_database_bak_path = "UPDATE \"t_set_database\" SET \"var_value\" = '" + database_bak_path + "' WHERE \"var_key\" = 'database_bak_path';";

            string sql_database_new_name = "UPDATE \"t_set_database\" SET \"var_value\" = '" + database_new_name + "' WHERE \"var_key\" = 'database_new_name';";
            string sql_database_log = "UPDATE \"t_set_database\" SET \"var_value\" = '" + database_log + "' WHERE \"var_key\" = 'database_log';";
            string sql_database_err_log = "UPDATE \"t_set_database\" SET \"var_value\" = '" + database_err_log + "' WHERE \"var_key\" = 'database_err_log';";
            string sql_database_path_config = "UPDATE \"t_set_database\" SET \"var_value\" = '" + database_path_config + "' WHERE \"var_key\" = 'database_path_config';";
            string sql_database_tool = "UPDATE \"t_set_database\" SET \"var_value\" = '" + database_tool + "' WHERE \"var_key\" = 'database_tool';";
            string sql_database_service = "UPDATE \"t_set_database\" SET \"var_value\" = '" + database_service + "' WHERE \"var_key\" = 'database_service';";

            SQLiteCommand cmd1 = new SQLiteCommand(sql_database_name, con);
            SQLiteCommand cmd2 = new SQLiteCommand(sql_database_path_install, con);
            SQLiteCommand cmd3 = new SQLiteCommand(sql_database_port, con);
            SQLiteCommand cmd4 = new SQLiteCommand(sql_database_old_password, con);
            SQLiteCommand cmd5 = new SQLiteCommand(sql_database_new_password, con);
            SQLiteCommand cmd6 = new SQLiteCommand(sql_database_bak_name, con);
            SQLiteCommand cmd7 = new SQLiteCommand(sql_database_bak_path, con);
            SQLiteCommand cmd8 = new SQLiteCommand(sql_database_new_name, con);
            SQLiteCommand cmd9 = new SQLiteCommand(sql_database_log, con);
            SQLiteCommand cmd10 = new SQLiteCommand(sql_database_err_log, con);
            SQLiteCommand cmd11 = new SQLiteCommand(sql_database_path_config, con);
            SQLiteCommand cmd12 = new SQLiteCommand(sql_database_tool, con);
            SQLiteCommand cmd13 = new SQLiteCommand(sql_database_service, con);
            cmd1.ExecuteNonQuery();
            cmd2.ExecuteNonQuery();
            cmd3.ExecuteNonQuery();
            cmd4.ExecuteNonQuery();
            cmd5.ExecuteNonQuery();
            cmd6.ExecuteNonQuery();
            cmd7.ExecuteNonQuery();
            cmd8.ExecuteNonQuery();
            cmd9.ExecuteNonQuery();
            cmd10.ExecuteNonQuery();
            cmd11.ExecuteNonQuery();
            cmd12.ExecuteNonQuery();
            cmd13.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("保存成功");
        }

        private void Php_submit_Click(object sender, RoutedEventArgs e)
        {
            string php_name = box_php_name.Text;
            string php_path = box_php_path.Text;
            string php_tool = box_php_tool.Text;
            string php_path_ext = box_php_ext_path.Text;
            string php_info = box_php_info.Text;
            string php_log = box_php_log.Text;
            string php_err_log = box_php_err_log.Text;
            string php_path_config = box_php_config_path.Text;



            //连接数据库
            string path_db = Path.Combine(path_root, @"config\pandax.db");
            string con_str = @"Data Source=" + path_db + @";Version=3;";
            SQLiteConnection con = new SQLiteConnection(con_str);
            con.Open();

            //更新数据的sql
            string sql_php_name = "UPDATE \"t_set_php\" SET \"var_value\" = '" + php_name + "' WHERE \"var_key\" = 'php_name';";
            string sql_php_path = "UPDATE \"t_set_php\" SET \"var_value\" = '" + php_path + "' WHERE \"var_key\" = 'php_path';";
            string sql_php_tool = "UPDATE \"t_set_php\" SET \"var_value\" = '" + php_tool + "' WHERE \"var_key\" = 'php_tool';";
            string sql_php_path_ext = "UPDATE \"t_set_php\" SET \"var_value\" = '" + php_path_ext + "' WHERE \"var_key\" = 'php_path_ext';";
            string sql_php_info = "UPDATE \"t_set_php\" SET \"var_value\" = '" + php_info + "' WHERE \"var_key\" = 'php_info';";
            string sql_php_log = "UPDATE \"t_set_php\" SET \"var_value\" = '" + php_log + "' WHERE \"var_key\" = 'php_log';";
            string sql_php_err_log = "UPDATE \"t_set_php\" SET \"var_value\" = '" + php_err_log + "' WHERE \"var_key\" = 'php_err_log';";
            string sql_php_path_config = "UPDATE \"t_set_php\" SET \"var_value\" = '" + php_path_config + "' WHERE \"var_key\" = 'php_path_config';";


            SQLiteCommand cmd1 = new SQLiteCommand(sql_php_name, con);
            SQLiteCommand cmd2 = new SQLiteCommand(sql_php_path, con);
            SQLiteCommand cmd3 = new SQLiteCommand(sql_php_tool, con);
            SQLiteCommand cmd4 = new SQLiteCommand(sql_php_path_ext, con);
            SQLiteCommand cmd5 = new SQLiteCommand(sql_php_info, con);
            SQLiteCommand cmd6 = new SQLiteCommand(sql_php_log, con);
            SQLiteCommand cmd7 = new SQLiteCommand(sql_php_err_log, con);
            SQLiteCommand cmd8 = new SQLiteCommand(sql_php_path_config, con);

            cmd1.ExecuteNonQuery();
            cmd2.ExecuteNonQuery();
            cmd3.ExecuteNonQuery();
            cmd4.ExecuteNonQuery();
            cmd5.ExecuteNonQuery();
            cmd6.ExecuteNonQuery();
            cmd7.ExecuteNonQuery();
            cmd8.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("保存成功");
        }

        private void Set_btn_pandax_open_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                Process.Start(set_box_pandax_path.Text);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Set_btn_edit_path_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;//该值确定是否可以选择多个文件
            dialog.Title = "请选择文本编辑器程序";
            dialog.Filter = "程序文件(*.exe)|*.exe";



            if (dialog.ShowDialog() == true)
            {
                set_box_edit_path.Text = dialog.FileName;
            }
        }

        private void Set_btn_log_path_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start("notepad.exe", set_box_log_path.Text);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Set_btn_errlog_path_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start("notepad.exe", set_box_errlog_path.Text);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Server_install_dir_open_Click(object sender, RoutedEventArgs e)
        {


            //如果目录不存在，则报错
            MessageBox.Show(System.IO.Directory.Exists(box_server_install_dir.Text).ToString());
            if (!System.IO.Directory.Exists(box_server_install_dir.Text))
            {
                throw new System.IO.FileNotFoundException("指定的目录: " + box_server_install_dir.Text + " 不存在!");
            }
            else
            {
                Process.Start(box_server_install_dir.Text);
            }

        }

        private void Server_www_open_Click(object sender, RoutedEventArgs e)
        {



            //如果目录不存在，则报错
            if (!System.IO.Directory.Exists(box_server_www.Text))
            {
                throw new System.IO.FileNotFoundException("指定的目录: " + box_server_www.Text + " 不存在!");
            }
            else
            {
                Process.Start(box_server_www.Text);
            }

        }

        private void Server_log_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start("notepad.exe", box_server_log.Text);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Server_err_log_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start("notepad.exe", box_server_err_log.Text);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Server_config_btn_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                Process.Start("notepad.exe", box_server_config.Text);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Php_path_open_Click(object sender, RoutedEventArgs e)
        {

            //如果目录不存在，则报错
            if (!System.IO.Directory.Exists(box_php_path.Text))
            {
                throw new System.IO.FileNotFoundException("指定的目录: " + box_php_path.Text + " 不存在!");
            }
            else
            {
                Process.Start(box_php_path.Text);
            }

        }

        private void Php_tool_btn_Click(object sender, RoutedEventArgs e)
        {



            //如果目录不存在，则报错
            if (!System.IO.Directory.Exists(box_php_tool.Text))
            {
                throw new System.IO.FileNotFoundException("指定的目录: " + box_php_tool.Text + " 不存在!");
            }
            else
            {
                Process.Start(box_php_tool.Text);
            }

        }

        private void Php_ext_path_btn_Click(object sender, RoutedEventArgs e)
        {


            //如果目录不存在，则报错
            if (!System.IO.Directory.Exists(box_php_ext_path.Text))
            {
                throw new System.IO.FileNotFoundException("指定的目录: " + box_php_ext_path.Text + " 不存在!");
            }
            else
            {
                Process.Start(box_php_ext_path.Text);
            }

        }

        private void Php_info_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(box_php_info.Text);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Php_log_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(box_php_log.Text);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Php_err_log_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(box_php_err_log.Text);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Php_config_path_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(box_php_config_path.Text);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static bool PortInUse(int port)
        {
            bool inUse = false;

            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] ipEndPoints = ipProperties.GetActiveTcpListeners();

            foreach (IPEndPoint endPoint in ipEndPoints)
            {
                if (endPoint.Port == port)
                {
                    inUse = true;
                    break;
                }
            }
            return inUse;
        }
        private void Set_btn_browser_path_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;//该值确定是否可以选择多个文件
            dialog.Title = "请选择浏览器程序";
            dialog.Filter = "程序文件(*.exe)|*.exe";



            if (dialog.ShowDialog() == true)
            {
                set_box_browser_path.Text = dialog.FileName;
            }

        }

        private void Server_port_check_Click(object sender, RoutedEventArgs e)
        {
            if (IsNumeric(box_server_port.Text))
            {
                if (PortInUse(int.Parse(box_server_port.Text)))
                {
                    MessageBox.Show(box_server_port.Text + "端口正在使用中");
                }
                else
                {
                    MessageBox.Show(box_server_port.Text + "端口尚未使用");
                }
            }
            else
            {
                MessageBox.Show("输入有误");
            }
        }

        static bool IsNumeric(string str)
        {
            if (str == null || str.Length == 0)
                return false;
            foreach (char c in str)
            {
                if (!Char.IsNumber(c))
                {
                    return false;
                }
            }
            return true;
        }

        private void Database_port_check_Click(object sender, RoutedEventArgs e)
        {
            if (IsNumeric(box_database_port.Text))
            {
                if (PortInUse(int.Parse(box_database_port.Text)))
                {
                    MessageBox.Show(box_database_port.Text + "端口正在使用中");
                }
                else
                {
                    MessageBox.Show(box_database_port.Text + "端口尚未使用");
                }
            }
            else
            {
                MessageBox.Show("输入有误");
            }
        }

        private void Set_btn_download_path_Click(object sender, RoutedEventArgs e)
        {
            WinForm.FolderBrowserDialog dialog = new WinForm.FolderBrowserDialog();
            dialog.Description = "请选择下载文件夹";
            if (dialog.ShowDialog() == WinForm.DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    MessageBox.Show(this, "文件夹路径不能为空", "提示");
                    return;
                }
                else
                {
                    set_box_download_path.Text = dialog.SelectedPath;
                }
            }
        }

        private void Set_box_download_path_open_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DirectoryInfo download_path = new DirectoryInfo(set_box_download_path.Text);

                if (!download_path.Exists)
                {
                    MessageBox.Show(this, "文件夹路径无效", "提示");
                    return;
                }
                else
                {
                    Process.Start(set_box_download_path.Text);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Server_install_dir_btn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.Description = "请选择 浏览器 所在文件夹";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    System.Windows.MessageBox.Show(this, "文件夹路径不能为空", "提示");
                    return;
                }
                else
                {
                    box_server_install_dir.Text = dialog.SelectedPath;
                }
            }

        }

        private void Server_www_btn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.Description = "请选择 浏览器 所在文件夹";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    System.Windows.MessageBox.Show(this, "文件夹路径不能为空", "提示");
                    return;
                }
                else
                {
                    box_server_www.Text = dialog.SelectedPath;
                }
            }
        }

        private void Php_path_btn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.Description = "请选择 浏览器 所在文件夹";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    System.Windows.MessageBox.Show(this, "文件夹路径不能为空", "提示");
                    return;
                }
                else
                {
                    box_php_path.Text = dialog.SelectedPath;
                }
            }
        }

        private void Database_path_open_Click(object sender, RoutedEventArgs e)
        {

            //如果目录不存在，则报错
            if (!System.IO.Directory.Exists(box_database_path.Text))
            {
                throw new System.IO.FileNotFoundException("指定的目录: " + box_database_path.Text + " 不存在!");
            }
            else
            {
                Process.Start(box_database_path.Text);
            }

        }
    }
}
