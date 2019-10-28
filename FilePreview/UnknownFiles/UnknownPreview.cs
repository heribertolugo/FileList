using Common.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace FilePreview.UnknownFiles
{
    public class UnknownPreview : Common.Models.IPreviewFile
    {
        public UnknownPreview()
        {
            this.Viewer = new RichTextBox() { ReadOnly = true };
            this.Viewer.BackColor = Color.Black;
            this.Viewer.ForeColor = Color.Black;
            this.Viewer.BackColor = Color.White;
        }

        public IEnumerable<string> Extensions { get { return new string[] { string.Empty, "*" }; } }

        public Control Viewer { get; private set; }

        public FileType FileType
        {
            get { return FileType.Unknown; }
        }

        public bool LoadFile(string path)
        {
            return this.DisplayApplicationPreview(path, this.Viewer as TextBoxBase);
        }

        public bool LoadFile(FileData path)
        {
            return this.DisplayApplicationPreview(path.Path, this.Viewer as TextBoxBase);
        }

        public void Clear()
        {
            if (this.textThread != null && this.textThreadCancel != null && !this.textThreadCancel.IsCancellationRequested)
                this.textThreadCancel.Cancel();
            (this.Viewer as RichTextBox).Clear();
        }



        private Thread textThread = null;
        private CancellationTokenSource textThreadCancel;
        private bool DisplayApplicationPreview(string path, TextBoxBase textBox)
        {
            if (this.textThread != null && this.textThreadCancel != null)
            {
                this.textThreadCancel.Cancel();
            }
            if (path == null)
            {
                textBox.Text = (string)null;
                return false;
            }
            else
            {
                try
                {
                    textBox.Tag = path;

                    textThread = new Thread((object s) =>
                    {
                        TextBoxBase box = textBox;
                        CancellationTokenSource token = (CancellationTokenSource)s;

                        box.Invoke((MethodInvoker)delegate
                        {
                            box.Clear();
                            box.Focus();
                            box.SelectionStart = box.Text.Length;
                            box.Select();
                        });

                        string p = box.Tag as string;

                        using (StreamReader reader = new StreamReader(p, UnknownPreview.GetFileEncoding(p)))
                        {
                            string intkar = string.Empty;
                            try
                            {
                                while ((intkar = reader.ReadLine()) != null && !token.IsCancellationRequested)
                                {
                                    token.Token.ThrowIfCancellationRequested();
                                    box.Invoke((MethodInvoker)delegate
                                    {
                                        box.AppendText(intkar);
                                    });
                                }
                            }
                            catch (Exception)
                            {
                                //box.Invoke((MethodInvoker)delegate{ box.Clear(); }); 
                            }
                        }
                    });

                    textThread.IsBackground = true;
                    textThreadCancel = new CancellationTokenSource();

                    textThread.Start(textThreadCancel);
                    return true;
                }
                catch (Exception ex)
                {
                    textBox.Text = (string)null;
                }
            }
                return false;
        }

        private static System.Text.Encoding GetFileEncoding(string path)
        {
            return System.Text.Encoding.UTF8;
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    this.Clear();
                    this.Viewer.Dispose();
                }

                this._disposed = true;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~UnknownPreview()
        {
            this.Dispose(false);
        }
    }
}
