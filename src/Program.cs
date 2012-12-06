// Shader-Based-Image-Processing (SBIP)
// http://code.google.com/p/sbip/
//
// Copyright © Frank Nagl, 2009-2011
// admin@franknagl.de
//
namespace SBIP
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Windows.Forms;
    using Microsoft.Win32;
    using SBIP.Helper;

    /// <summary>Start entry point of the SBIP Application.</summary>
    public static class Program
    {        
        /// <summary>The file name, where the options are saved.</summary>
        public static string OptionsFile { get; private set; }
        /// <summary>The program exe, which starts Picturez.</summary>
        public static string ProgramExe { get; private set; }
        /// <summary>The program directory, where Picturez is located.</summary>
        public static string ProgramPath { get; private set; }
        /// <summary>Parameter value, that determines if an update will be done.
        /// </summary>
        public const string UpdateArgument = "sbip.makeupdate";

        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ProgramExe = Application.ExecutablePath;
            ProgramPath =
                Directory.GetParent(Application.ExecutablePath).ToString();
            OptionsFile = Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData) +
                "\\SBIP\\options.dat";

            if (args.Length == 1 && 
                args[0] == UpdateArgument && 
                VistaSecurity.IsAdmin())
            {
                AutoUpdater updater = new AutoUpdater(
                                OptionsFile,
                                "http://franknagl.de/updates/SBIP/update.csv",
                                ProgramPath,
                                ProgramExe);
                updater.MakeUpdate();
                return;
            }

            // check (and repair) context menu entries
            if (CheckContextMenuEntries())
            {
                return;
            }

            //Creates only by very first start of SBIP
            if (!File.Exists(OptionsFile))
            {
                CreateOptionsFile(OptionsFile);
            }            
         
            ProcessorForm form;
            // start with parameter
            if (args.Length == 2 && args[1] == "sbip.frank")
            {
                form = new ProcessorForm(args[0]);
            }
            // normal start
            else
            {
                form = new ProcessorForm();
            }

            Application.Run(form);
            //if (form.IsDisposed)
            //{
            //    return;
            //}

            //form.Show();
            //while (form.Created)
            //{
            //    //form.Processor.Render();
            //    //form.UpdateEachFrame(false);
            //    Application.DoEvents();
            //}

            #region TODO Frank Nagl: Delete this region.
            //Bitmap image = new Bitmap("sample1.jpg");

            //// 1. Offline rendering
            //// create Processor, used as rendering framework
            //Processor processor = new Processor();
            //// starts Processor
            //processor.Begin(image);
            //// create RotateChannels filter
            //RotateChannels filter = new RotateChannels();
            //processor.Filter = filter;
            //// optional: configure filter
            //filter.Order = RGBOrder.GBR;
            //// apply the filter
            //Bitmap resultImage = processor.RenderToBitmap();
            ////Texture2D resultTexture = processor.RenderToTexture( );
            //resultImage.Save("RotateChannels.jpg", ImageFormat.Jpeg);
            //processor.End();

            //// 2. Online rendering
            //// create any windows control for rendering in
            //Form myForm = new Form();
            //// create Processor, used as rendering framework
            //Processor processor2 = new Processor();
            //// starts Processor
            //processor2.Begin(image, myForm);
            //// create ExtractChannel filter
            //RotateChannels filter2 = new RotateChannels();
            //processor2.Filter = filter2;
            //// optional: configure filter
            //filter2.Order = RGBOrder.GBR;

            //// apply the filter
            //myForm.Show();
            //while (myForm.Created)
            //{
            //    processor2.Render();
            //    Application.DoEvents();
            //}
            //processor2.End();

            #endregion TODO Frank Nagl: Delete this region.
        }

        /// <summary>
        /// Determines whether the windows explorer context menu entries for 
        /// Picturez are correct in the win registry.
        /// </summary>
        /// <returns>True, if contextmenu are correct.</returns>
        private static bool AreCorrectContextMenuEntries()
        {
            RegistryKey regmenu;
            try
            {
                // check OPEN WITH SBIP
                regmenu = Registry.ClassesRoot.OpenSubKey(
                    "*\\shell\\Open with SBIP\\Command", false);
                //If format does not already exist, return true
                if (regmenu == null)
                {
                    return false;
                }

                regmenu.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private static bool CheckContextMenuEntries()
        {
            if (VistaSecurity.IsAdmin())
            {
                Process context = new Process();
                context.StartInfo.FileName = ProgramPath + "\\ContextMenu.exe";
                context.Start();
                return false;
            }

            if (AreCorrectContextMenuEntries())
            {
                return false;
            }

            if (MessageBox.Show(
                @"Context menu entries are wrong. Do you want to correct them (recommended)?",
                @"Wrong context menu entries",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                VistaSecurity.RestartElevatedForUpdate();
                return true;
            }  

            return false;
        }

        private static void CreateOptionsFile(string file)
        {
            string dir = file.Substring(0, file.LastIndexOf('\\'));
            Directory.CreateDirectory(dir);
            FileStream f = File.Create(file);
            f.Close();
            IOFile.WriteLine(file, 1, "Version;1.2", true);
            IOFile.WriteLine(file, 2, "ReleaseDate;2011-07-04", true);
        }

        ///// <summary>
        ///// Source from nail.dll: Schreibt den übergebenen Text in eine 
        ///// definierte Zeile.
        ///// </summary>
        ///// <param name="filename">Pfad zur Datei</param>
        ///// <param name="line">Zeilennummer</param>
        ///// <param name="lines">Text für die übergebene Zeile</param>
        ///// <param name="replace">Text in dieser Zeile überschreiben (t) 
        ///// oder einfügen (f)</param>
        //private static void WriteLine(
        //    String filename, int line, string lines, bool replace)
        //{
        //    string tContent = "";
        //    string[] tDelimiterstring = { "\r\n" };

        //    if (File.Exists(filename))
        //    {
        //        StreamReader tFile = new StreamReader(
        //            filename, 
        //            System.Text.Encoding.Default);
        //        tContent = tFile.ReadToEnd();
        //        tFile.Close();
        //    }

        //    string[] tCols = tContent.Split(
        //        tDelimiterstring, 
        //        StringSplitOptions.None);

        //    if (tCols.Length >= line)
        //    {
        //        if (!replace)
        //            tCols[line - 1] = lines + "\r\n" + tCols[line - 1];
        //        else
        //            tCols[line - 1] = lines;

        //        tContent = "";
        //        for (int x = 0; x < tCols.Length - 1; x++)
        //        {
        //            tContent += tCols[x] + "\r\n";
        //        }
        //        tContent += tCols[tCols.Length - 1];

        //    }
        //    else
        //    {
        //        for (int x = 0; x < line - tCols.Length; x++)
        //            tContent += "\r\n";

        //        tContent += lines;
        //    }


        //    StreamWriter tSaveFile = new StreamWriter(filename);
        //    tSaveFile.Write(tContent);
        //    tSaveFile.Close();
        //}
    }
}
