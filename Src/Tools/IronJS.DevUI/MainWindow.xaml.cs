﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IronJS.DevUI {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        IronJS.Hosting.Context ijsCtx;

        public MainWindow() {
            InitializeComponent();
            Title = IronJS.Version.FullName + " DevUI";

            RunCode.Click += new RoutedEventHandler(RunCode_Click);
            ResetEnv.Click += new RoutedEventHandler(ResetEnv_Click);

            ResetEnv_Click(null, null);

            IronJS.Printer.print = new IronJS.Print(Print);
        }

        void Print(System.Linq.Expressions.Expression expr) {
            Debug.Text += IronJS.Dlr.Utils.debugView(expr);
        }

        void ResetEnv_Click(object sender, RoutedEventArgs e) {
            ijsCtx = IronJS.Hosting.Context.Create();
        }

        void RunCode_Click(object sender, RoutedEventArgs e) {
            Debug.Text = "";
            Result.Text = IronJS.Api.TypeConverter.toString(ijsCtx.Execute(Input.Text));
        }
    }
}
