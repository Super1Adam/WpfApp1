﻿using Microsoft.Win32;
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
using Microsoft.Win32;
using System.IO;
using System.Collections.ObjectModel;
using WpfApp1.ViewModels;
using WpfApp1.Models;
using System.Globalization;

namespace WpfApp1.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            InitializeComponent();


            this.DataContext = new MainViewModels();



            //mediaElement.Source = new Uri(@"E:\gird\WpfApp1\WpfApp1\Contorls\fengji.mp4", UriKind.Absolute);


            //// 播放视频
            //mediaElement.MediaEnded += MediaElement_MediaEnded;
            //mediaElement.Play();

        }
        //private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        //{
        //    // 视频播放完毕后重新播放
        //    mediaElement.Position = TimeSpan.Zero;  // 将视频进度设置为开头
        //    mediaElement.Play();                    // 重新播放视频
        //}

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            
        }

        private void CartesianChart_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void CartesianChart_Loaded_1(object sender, RoutedEventArgs e)
        {

        }


        //private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        //{
        //    OpenFileDialog openFileDialog = new OpenFileDialog();
        //    if (openFileDialog.ShowDialog() == true)
        //        txtEditor.Text = File.ReadAllText(openFileDialog.FileName);

        //}

    }
}