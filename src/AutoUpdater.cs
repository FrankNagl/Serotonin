//
// Shader-Based-Image-Processing (SBIP)
// http://code.google.com/p/sbip/
//
// Copyright © Frank Nagl, 2009-2011
// admin@franknagl.de
//
namespace SBIP
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Threading;
    using System.Windows.Forms;

    /// <summary>Declares the event handler for available updates.</summary>
    public delegate void OnCheckUpdateEventHandler(bool isUpdatable);
    
    /// <summary>
    /// Checks a program for an update, simply by reading out
    /// a textfile with two informations (version number, release date).
    /// </summary>
    public class AutoUpdater
    {
        private static string path;
        private static string updateDir;

        private readonly string newFile;
        private readonly string oldFile;        
        private readonly string programExe;

        /// <summary>Handles the OnCheckUpdateEvent at the client.</summary>
        public OnCheckUpdateEventHandler OnCheckUpdateEvent;

        /// <summary>
        /// Overloaded. Checks a program for an update, simply by reading out
        /// a textfile with two informations (version number, release date).
        /// </summary>
        /// <remarks>
        /// <para>The textfile <seealso cref="newFile"/> is located in general
        /// on a server and consists of:</para>
        ///<para>In first line the version number, e.g.
        /// <code>Version;1.0</code></para> 
        ///<para>In second line the release date, e.g.
        /// <code>ReleaseDate;2008-11-13</code></para> 
        /// <para>In all other lines two markers:</para>
        /// <para><see>Copy</see>, for files to update (e.g. Copy;newFile.exe)</para>
        /// <para><code>Delete</code>, for files to delete (e.g. Delete;oldFile.exe)</para>
        /// <para>All <code>copy</code>-files will be updates by copy and paste, 
        /// existing files will be override.</para>
        ///</remarks>
        /// <param name="oldFile">Path to the old textfile with the version number.</param>
        /// <param name="newFile">Path of the new textfile with the update informations 
        /// and new version number.</param>
        /// <param name="programPath">The path to the program</param>
        /// <param name="programExe">The exe file to restart the program</param>
        public AutoUpdater(
            string oldFile, 
            string newFile, 
            string programPath, 
            string programExe)
        {
            this.oldFile = oldFile.Replace('\\', '/');
            this.newFile = newFile.Replace('\\', '/');
            this.programExe = programExe.Replace('\\', '/');
            path = programPath.Replace('\\', '/') + '/';
            updateDir = path + "tempUpdate/";            
        }

        #region public methods        
        /// <summary>Starts the check for an update as thread.</summary>
        public void CheckUpdateAsThread()
        {
            ThreadStart pts = new ThreadStart(CheckUpdate);
            Thread thread = new Thread(pts);
            thread.IsBackground = true;
            thread.Start();
        }

        /// <summary>Starts the check for an update.</summary>
        public void CheckUpdate()
        {
            try
            {
                WebRequest request = WebRequest.Create(newFile);
                WebResponse response = request.GetResponse();
// ReSharper disable PossibleNullReferenceException
// ReSharper disable AssignNullToNotNullAttribute
                StreamReader r = new StreamReader(response.GetResponseStream());
// ReSharper restore AssignNullToNotNullAttribute
// ReSharper restore PossibleNullReferenceException
                string newVersion = r.ReadLine();

// ReSharper disable PossibleNullReferenceException
                newVersion = newVersion.Substring(newVersion.IndexOf(';') + 1);
// ReSharper restore PossibleNullReferenceException
                r.ReadLine(); // throw the 2nd line away
                string constaintVersion = r.ReadLine(); // we need the third line
// ReSharper disable PossibleNullReferenceException
                constaintVersion = constaintVersion.Substring(constaintVersion.IndexOf(';') + 1);
// ReSharper restore PossibleNullReferenceException
                r.Close();

                float oldV = ReadFloatData(oldFile, 1, ';');
                float newV = float.Parse(newVersion,
                                         System.Globalization.CultureInfo.CreateSpecificCulture("en-us"));
// ReSharper disable AssignNullToNotNullAttribute
                float constraintV = float.Parse(constaintVersion,
// ReSharper restore AssignNullToNotNullAttribute
                         System.Globalization.CultureInfo.CreateSpecificCulture("en-us"));
                if (oldV < newV && OnCheckUpdateEvent != null)
                {
                    // is old version updateable ?
                    OnCheckUpdateEvent.Invoke(oldV >= constraintV);
                }
            }
            catch (/*System.Net.WebException*/Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }

        /// <summary>Makes the update.</summary>
        /// <returns>True, if the update was successful; otherwise false.</returns>
        public bool MakeUpdate()
        {
            try
            {
                ProgressBar bar = InitFormAndProgressBar();
                //get url of the new files
                string url = newFile.Remove(newFile.LastIndexOf('/') + 1);
                //get name of update file (e.g. update.csv)
                string updateFile = updateDir + newFile.Substring(newFile.LastIndexOf('/') + 1);
                Directory.CreateDirectory(updateDir);
                WebClient client = new WebClient();
                //download update file (e.g. update.csv)
                client.DownloadFile(newFile, updateFile);
                StreamReader r = new StreamReader(updateFile);
                List<string> copyFiles = new List<string>();
                string s;

                while ((s = r.ReadLine()) != null)
                {
                    if (s.StartsWith("Copy"))
                        copyFiles.Add(s.Substring(s.IndexOf(';') + 1));
                }

                bar.Maximum = 2 * copyFiles.Count;
                //copy all new files into the updateDir
                foreach (string t in copyFiles)
                {
                    client.DownloadFile(url + t, updateDir + t);
                    bar.PerformStep();
                }
                copyFiles.Clear();
                r.Close();
                client.Dispose();
                Application.OpenForms[0].Dispose();

                System.Diagnostics.ProcessStartInfo startInfo =
                new System.Diagnostics.ProcessStartInfo
                    (path + "update.exe", "\"" + updateFile + "\" \"" + programExe +
                    "\" \"" + oldFile + "\"");
                //startInfo.UseShellExecute = false;//hide the java shell
                System.Diagnostics.Process.Start(startInfo);
                //tProcess.WaitForExit();

                return true;
            }
            catch (Exception)
            {
                MessageBox.Show(@"Update failed.", @"Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Application.OpenForms[0].Close();
                return false;
            }

        }
        #endregion public methods

        #region private methods
        
        private static ProgressBar InitFormAndProgressBar()
        {
            Form updateForm = new Form();
            ProgressBar bar = new ProgressBar();
            Label label = new Label();
            updateForm.Controls.Add(bar);
            updateForm.Controls.Add(label);

            //init the form
            updateForm.Text = @"Updating...";
            updateForm.Width = 300;
            updateForm.Height = 200;
            updateForm.BackColor = System.Drawing.Color.Lavender;
            updateForm.StartPosition = FormStartPosition.CenterScreen;

            ////init the icon
            //System.Resources.ResourceManager resource =
            //    new System.Resources.ResourceManager("Nail.Properties.Resources",
            //    System.Reflection.Assembly.GetExecutingAssembly());
            //updateForm.Icon = (System.Drawing.Icon)resource.GetObject("AutoUpdaterIcon");

            //init the label
            label.Text = @"Updating...";
            label.AutoSize = true;
            label.Location = new System.Drawing.Point(100, 30);

            //init the progressBar
            bar.Location = new System.Drawing.Point(100, 70);
            bar.Value = 0;
            bar.Step = 1;

            updateForm.Show();
            updateForm.Refresh();
            return bar;
        }

        /// <summary>Reads out a float data from a line of a file.</summary>
        /// <param name="filename">The filename.</param>
        /// <param name="line">The line.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <returns>The readed float data.</returns>
        private static float ReadFloatData(
            String filename, 
            int line, 
            char delimiter)
        {
            string content = "";
            float row = 0;
            if (File.Exists(filename))
            {
                StreamReader tFile = 
                    new StreamReader(filename, System.Text.Encoding.Default);
                while (!tFile.EndOfStream && row < line)
                {
                    row++;
                    content = tFile.ReadLine();
                }
                tFile.Close();
                if (row < line)
                    content = "";
            }

            float data = 0f;
            if (content != null)
            {
                content = content.Substring(content.IndexOf(delimiter) + 1);
                data = float.Parse(
                    content, 
                    System.Globalization.CultureInfo.
                    CreateSpecificCulture("en-us"));
            }
            
            return data;
        }

        #endregion private methods
    }
}
