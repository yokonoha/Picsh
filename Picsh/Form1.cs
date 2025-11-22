using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Picsh
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog openFile1 = new OpenFileDialog();
            //すぐ開けるようにv1から仕様変更
            openFile1.Filter = "画像データ|*.jpg;*.png;*.gif;*.bmp;*.tif";
            if (openFile1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
            pictureBox1.Load(openFile1.FileName);
            this.Text = "Picsh:"+openFile1.FileName;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] fs = System.Environment.GetCommandLineArgs();
            //複数エントリ
            if (fs.Length > 1 && File.Exists(fs[1]))
            {
                    pictureBox1.ImageLocation = fs[1] ;

            }
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                //無視
                return;
            }
                
            if (e.Data.GetData(DataFormats.FileDrop) is string[] fs)
            {
                pictureBox1.ImageLocation = fs[0];
            }
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {

                e.Effect = DragDropEffects.All;
            }
            else
            {

                MessageBox.Show("ファイルのみドラッグアンドドロップが可能です。");
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button== MouseButtons.Right)
            {
                if (this.TopMost == true)
                {
                    this.TopMost = false;
                    MessageBox.Show("-右クリックアクション-\n常に手前表示 を解除しました。");
                }
                else
                {
                    this.TopMost = true;
                    MessageBox.Show("-右クリックアクション-\n常に手前表示 に設定しました。");
                }
            }

            if(e.Button == MouseButtons.Middle)
            {
                MessageBox.Show("-ホイールクリックアクション-\n情報表示を行います。");
                if (pictureBox1.Image == null)
                {
                    MessageBox.Show("画像が読み込まれていません。");
                }
                else
                {
                    MessageBox.Show("実際の画像サイズ:" + pictureBox1.Image.Size.ToString().Replace("Width=","幅(横サイズ)は").Replace("Height=","高さ(縦サイズ)は").Replace("{","").Replace(",", "px(ピクセル)、").Replace("}","px(ピクセル)です。") + "\n現在の表示枠サイズ:" + pictureBox1.Size.ToString().Replace("Width=", "幅(横サイズ)は").Replace("Height=", "高さ(縦サイズ)は").Replace("{", "").Replace(",", "px(ピクセル)、").Replace("}", "px(ピクセル)です。"));
                    MessageBox.Show("画像ファイルサイズ:" + new FileInfo(pictureBox1.ImageLocation).Length.ToString("N0") + " バイトです。\n" + pictureBox1.ImageLocation+"\nに保存されている画像を表示しています。");
                }
                MessageBox.Show("Picshv2\n最軽量・必要最低限の画像表示ソフト\n 2018年7月に公開した初版にコマンドメニューを付け加え、更に内部処理も見直しました。\n 2025 横茶横葉 製作.");
            }
        }
    }
}
