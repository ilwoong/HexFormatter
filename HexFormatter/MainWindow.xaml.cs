/**
 * MIT License
 * 
 * Copyright (c) 2020 Ilwoong Jeong (https://github.com/ilwoong)
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace HexFormatter
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private int _wordSize = 4;
        private int _bytesPerLine = 16;

        public int WordSize { 
            get
            {
                return _wordSize;
            }

            set
            {
                _wordSize = value;
                UpdateFormat();
            }
        }

        public int BytesPerLine { 
            get
            {
                return _bytesPerLine;
            }
            set
            {
                _bytesPerLine = value;
                UpdateFormat();
            } 
        }

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;
        }

        private void UpdateFormat()
        {
            var text = TextSrc.Text;
            text = text?.Replace(" ", "");
            text = text?.Replace("\r", "");
            text = text?.Replace("\n", "");
            text = text?.Trim();

            var index = 0;
            var length = text.Length;
            string outText = "";
            while (length > 0)
            {
                outText += text.Substring(index * WordSize * 2, Math.Min(length, WordSize * 2));
                length -= 2 * WordSize;
                index += 1;

                if ((index % BytesPerLine) == 0)
                {
                    outText += "\r\n";
                }
                else
                {
                    outText += " ";
                }
            }

            TextDst.Text = outText;
        }

        private void TextSrc_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateFormat();
        }
        
        private void TextSrc_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.All;
            e.Handled = true;
        }

        private void TextSrc_PreviewDrop(object sender, DragEventArgs e)
        {
            string[] files = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (files?.Length > 0)
            {
                var file = new FileInfo(files[0]);
                Title = file.Name;
                LoadFileAsSrc(file);
            }
        }

        private void LoadFileAsSrc(FileInfo info)
        {

        }
    }
}
