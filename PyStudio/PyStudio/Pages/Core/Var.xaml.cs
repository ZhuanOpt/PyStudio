using CsvHelper;
using System;
using System.IO;
using System.Windows.Controls;

namespace PythonEnv.pages
{
    /// <summary>
    /// var.xaml 的交互逻辑
    /// </summary>
    public partial class var : Page
    {
        public var()
        {
            InitializeComponent();
        }

        public static implicit operator var(StreamReader v)
        {
            throw new NotImplementedException();
        }

        public static implicit operator var(CsvReader v)
        {
            throw new NotImplementedException();
        }

        internal var GetRecords<T>()
        {
            throw new NotImplementedException();
        }
    }
}
