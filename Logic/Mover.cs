using Common.Extensions;
using Common.Helpers;
using FileList.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace FileList.Logic
{
    public class Mover
    {
        private IEnumerable<string> _source;
        private string _destination;
        private ProgressInfoControl _moveProgress;
        private bool _preserveDirectories;
        private bool _isCopy;
        private bool _overwrite;
        private int _maxErrors;
        private readonly string OperationText;
        private List<Exception> _exceptions;

        public Mover(string[] source, string destination, bool retainDirectoryStructure, 
            bool copyOnly, bool overwrite, ProgressInfoControl moveProgress, int maxErrors)
        {
            this._source = source;
            this._destination = destination;
            this._moveProgress = moveProgress;
            this._preserveDirectories = retainDirectoryStructure;
            this._isCopy = copyOnly;
            this._overwrite = overwrite;
            this._maxErrors = maxErrors;
            this._exceptions = new List<Exception>();
            this.OperationText = copyOnly ? "copied" : "moved";
        }

        public void Start()
        {
            if (this._preserveDirectories)
                this.MovePreserved();
            else
                this.MoveTo();
        }

        public IEnumerable<Exception> Exceptions
        {
            get
            {
                return this._exceptions;
            }
            private set
            {
            }
        }

        private void MoveTo()
        {
            foreach (string item in this._source)
            {
                try
                {
                    if (!File.Exists(item))
                        throw new Exception(string.Format("The file requested does not exist: {0}", item));
                    string destination = Path.Combine(this._destination, FileHelper.GetFileName(item));
                    File.Copy(item, destination, this._overwrite);
                    this._moveProgress.InvokeIfRequired(p => p.PushMessage(string.Format("File copied: {0}", item)));
                    if (!this.ValidateCopy(item, destination))
                        throw new Exception(string.Format("File was not {2} correctly. Source file was not removed.\nFile: {0}\nDestination: {1}", item, destination, this.OperationText));
                    if (!this._isCopy)
                    {
                        File.Delete(item);
                        if (File.Exists(item))
                            throw new Exception(string.Format("Could not delete file: {0}", item));
                        string directoryName = FileHelper.GetDirectoryName(item);
                        if (Directory.GetFiles(directoryName).Length < 1)
                            Directory.Delete(directoryName);
                    }
                    this._moveProgress.InvokeIfRequired(p => p.AddItem(item, string.Format("{0} successfully {2} to {1}", FileHelper.GetFileName(item), destination, this.OperationText)));
                }
                catch (Exception ex)
                {
                    this._exceptions.Add(ex);
                    this._moveProgress.InvokeIfRequired(p => p.AddItem(item, "An error has been recorded"));
                    if (this._exceptions.Count > this._maxErrors)
                    {
                        this._moveProgress.InvokeIfRequired(p => p.PushMessage("Operation cancelled due to excessive errors"));
                        break;
                    }
                }
                this._moveProgress.InvokeIfRequired(p => p.PushMessage(string.Empty));
            }
        }

        private void MovePreserved()
        {
            foreach (string item in this._source)
            {
                try
                {
                    if (!File.Exists(item))
                        throw new Exception(string.Format("The file requested does not exist: {0}", item));
                    string destination = this.CreateDestination(item, this._destination, this._moveProgress);
                    File.Copy(item, destination, this._overwrite);
                    if (!this.ValidateCopy(item, destination))
                        throw new Exception(string.Format("File was not {2} correctly. Source file was not removed.\nFile: {0}\nDestination: {1}", item, destination, this.OperationText));
                    if (!this._isCopy)
                    {
                        File.Delete(item);
                        if (File.Exists(item))
                            throw new Exception(string.Format("Could not delete file: {0}", item));
                        string directoryName = FileHelper.GetDirectoryName(item);
                        if (Directory.GetFiles(directoryName).Length < 1)
                            Directory.Delete(directoryName);
                    }
                    this._moveProgress.InvokeIfRequired(p => p.AddItem(item, string.Format("{0} successfully {2} to {1}", FileHelper.GetFileName(item), destination, this.OperationText)));
                }
                catch (Exception ex)
                {
                    this._exceptions.Add(ex);
                    this._moveProgress.InvokeIfRequired(p => p.AddItem(item, "An error has been recorded"));
                    if (this._exceptions.Count > this._maxErrors)
                    {
                        this._moveProgress.InvokeIfRequired(p => p.PushMessage("Operation cancelled due to excessive errors"));
                        break;
                    }
                }
                this._moveProgress.InvokeIfRequired(p => p.PushMessage(string.Empty));
            }
        }

        private bool ValidateCopy(string source, string destination)
        {
            if (!File.Exists(source) || !File.Exists(destination))
                return false;
            return File.ReadAllBytes(source).SequenceEqual(File.ReadAllBytes(destination));
        }

        private string CreateDestination(string source, string destination, ProgressInfoControl moveProgress)
        {
            string path = Path.Combine(destination, source.Replace(":", ""));
            string directoryName = FileHelper.GetDirectoryName(path);

            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
                foreach (string directory in source.Split(new char[1] { Path.DirectorySeparatorChar }))
                    moveProgress.AddItem(null, null);
                moveProgress.PushMessage(string.Format("Destination created: {0}", directoryName));
            }

            return path;
        }
    }
}
