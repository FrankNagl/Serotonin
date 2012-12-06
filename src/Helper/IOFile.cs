// Shader-Based-Image-Processing (SBIP)
// http://code.google.com/p/sbip/
//
// Copyright © Frank Nagl, 2009-2011
// admin@franknagl.de
//
namespace SBIP.Helper
{
    using System;
    using System.IO;
    using System.Globalization;
    using System.Threading;

    /// <summary>
    /// Static class for reading out, writing in and append (text-)files.
    /// </summary>
    public static class IOFile
    {
        /// <summary>Determines whether a file is in use.</summary>
        ///<param name="filename">The path of the file.</param>
        /// <returns>
        /// Returns <c>true</c> if file is in use; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsFileInUse(string filename)
        {
            {
                var perm = new System.Security.Permissions.FileIOPermission(
                     System.Security.Permissions.FileIOPermissionAccess.Write |
                     System.Security.Permissions.FileIOPermissionAccess.Read,
                     filename);
                try
                {
                    perm.Demand();
                    return false;
                }
                catch
                {
                    return true;
                }
            }
        }

        ///<summary>
        /// Reads out the complete content of the given file.
        ///</summary>
        ///<param name="filename">The path of the file</param>
        public static string ReadFile(String filename)
        {
            string content = "";

            if (File.Exists(filename))
            {
                StreamReader file = new StreamReader(filename, System.Text.Encoding.Default);
                content = file.ReadToEnd();
                file.Close();
            }
            return content;
        }

        ///<summary>
        /// Writes the <paramref name="lines"/> into the <paramref name="filename">
        /// file.</paramref>.
        ///</summary>
        ///<param name="filename">Path of the file.</param>
        ///<param name="lines">The lines to write.</param>
        public static void WriteFile(String filename, String lines)
        {
            StreamWriter tFile = new StreamWriter(filename);
            tFile.Write(lines);
            tFile.Close();
        }


        /// <summary>
        /// Appends the <paramref name="lines"/> at the end of 
        /// the <paramref name="filename"> file.</paramref>.
        /// </summary>
        /// <param name="filename">Path of the file.</param>
        /// <param name="lines">The lines to append.</param>
        public static void Append(string filename, string lines)
        {
            StreamWriter tFile = new StreamWriter(filename, true);
            tFile.Write(lines);
            tFile.Close();
        }

        /// <summary>
        /// Reads out the text of <paramref name="line"/> in the 
        /// <paramref name="filename">file</paramref>.
        /// </summary>
        /// <param name="filename">Path of the file.</param>
        /// <param name="line">The number of the line to read out.</param>
        /// <returns></returns>
        public static string ReadLine(String filename, int line)
        {
            string tContent = "";
            float tRow = 0;
            if (File.Exists(filename))
            {
                StreamReader tFile = new StreamReader(filename, System.Text.Encoding.Default);
                while (!tFile.EndOfStream && tRow < line)
                {
                    tRow++;
                    tContent = tFile.ReadLine();
                }
                tFile.Close();
                if (tRow < line)
                    tContent = "";
            }
            return tContent;
        }

        /// <summary>
        /// Writes the specified text into a file at a specified line number.
        /// </summary>
        /// <param name="filename">Path of the file.</param>
        /// <param name="line">The line number.</param>
        /// <param name="lines">The text to write.</param>
        /// <param name="replace">True, if the old text in the line has to 
        /// be replaced; otherwise, false.</param>
        public static void WriteLine(String filename, int line, string lines, bool replace)
        {
            string content = "";
            string[] delimiterstring = { "\r\n" };

            if (File.Exists(filename))
            {
                StreamReader file = new StreamReader(filename, System.Text.Encoding.Default);
                content = file.ReadToEnd();
                file.Close();
            }

            string[] cols = content.Split(delimiterstring, StringSplitOptions.None);

            if (cols.Length >= line)
            {
                if (!replace)
                    cols[line - 1] = lines + "\r\n" + cols[line - 1];
                else
                    cols[line - 1] = lines;

                content = "";
                for (int x = 0; x < cols.Length - 1; x++)
                {
                    content += cols[x] + "\r\n";
                }
                content += cols[cols.Length - 1];

            }
            else
            {
                for (int x = 0; x < line - cols.Length; x++)
                    content += "\r\n";

                content += lines;
            }


            StreamWriter saveFile = new StreamWriter(filename);
            saveFile.Write(content);
            saveFile.Close();
        }


        /// <summary>
        /// Reads out a line as byte data.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="line">The line number.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <returns></returns>
        public static byte ReadByteData(String filename, int line, char delimiter)
        {
            string content = ReadLine(filename, line);
            content = content.Substring(content.IndexOf(delimiter) + 1);
            byte data = byte.Parse(content);
            return data;
        }

        /// <summary>
        /// Reads out the float data of a line in a text file.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="line">The line number.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <returns>The read float value.</returns>
        public static float ReadFloatData(String filename, int line, char delimiter)
        {
            string content = ReadLine(filename, line);
            content = content.Substring(content.IndexOf(delimiter) + 1);
            float data = //float.Parse(content);
            float.Parse(content, System.Globalization.CultureInfo.CreateSpecificCulture("en-us"));
            return data;
        }

        /// <summary>
        /// Reads out the float data with a german comma of a line in a text file.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="line">The line number.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <returns>The read float value.</returns>
        public static float ReadGermanFloatData(String filename, int line, char delimiter)
        {
            string content = ReadLine(filename, line);
            content = content.Substring(content.IndexOf(delimiter) + 1);
            float data = //float.Parse(content);
            float.Parse(content, System.Globalization.CultureInfo.CreateSpecificCulture("de-de"));
            return data;
        }

        /// <summary>
        /// Reads out several float datas of a line in a text file into a float array.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="line">The line number.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <returns>The read float array.</returns>
        public static float[] ReadFloatDataArray(String filename, int line, char delimiter)
        {
            string content = ReadLine(filename, line);
            string[] s = content.Split(new[] { delimiter }, StringSplitOptions.RemoveEmptyEntries);
            float[] data = new float[s.Length];
            for (int i = 0; i < s.Length; i++)
                data[i] = float.Parse(s[i], System.Globalization.CultureInfo.CreateSpecificCulture("en-us"));

            return data;
        }

        /// <summary>
        /// Reads out the string data.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="line">The line.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <returns></returns>
        public static string ReadStringData(String filename, int line, char delimiter)
        {
            string data = ReadLine(filename, line);
            data = data.Substring(data.IndexOf(delimiter) + 1);
            return data;
        }

        /// <summary>
        /// Creates directory, if it does not exists. Adapts path name with concluding slash.
        /// </summary>
        /// <param name="path">Path of the directory.</param>
        /// <returns>The path inclusive concluding slash.</returns>
        public static string CDir(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            if (!path.EndsWith(@"\") && !path.EndsWith(@"/"))
                path = path + @"/";

            return path;
        }

        /// <summary>
        /// Checks, if a directory is empty.
        /// </summary>
        /// <param name="path">Path of the directory.</param>
        /// <returns>
        /// 	<c>true</c> if directory is empty, otherwise <c>false</c>.
        /// </returns>
        public static bool IsDirEmpty(string path)
        {
            return Directory.GetDirectories(path).Length == 0 &&
                   Directory.GetFiles(path).Length == 0;
        }
    }
}
