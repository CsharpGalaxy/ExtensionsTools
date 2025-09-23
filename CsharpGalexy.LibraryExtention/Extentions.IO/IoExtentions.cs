using ExtentionLibrary.Strings;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace CsharpGalexy.LibraryExtention.IO
{
    /// <summary>
    /// Provides extension methods for file and folder operations, including JSON loading, text appending, copying, deletion, and string counting.
    /// </summary>
    public static class IoExtentions
    {
        /// <summary>
        /// Loads and deserializes a JSON file into an object of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type to deserialize the JSON into.</typeparam>
        /// <param name="jsonPath">The full path to the JSON file.</param>
        /// <returns>The deserialized object if successful; otherwise the default value of <typeparamref name="T"/>.</returns>
        public static T LoadJson<T>(string jsonPath)
        {
            try
            {
                using StreamReader streamReader = new StreamReader(jsonPath);
                return streamReader.ReadToEnd().ParseTo<T>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return default;
            }
        }

        /// <summary>
        /// Appends a line of text to a file. If the file does not exist, it will be created.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="text">The text to append.</param>
        public static void AppendText(string path, string text)
        {
            if (System.IO.File.Exists(path))
            {
                using (StreamWriter writer = System.IO.File.AppendText(path))
                {
                    writer.WriteLine(text);
                    writer.Close();
                }
                return;
            }
            System.IO.File.WriteAllText(path, text);
            using (StreamWriter writer = System.IO.File.AppendText(path))
            {
                writer.Close();
            }
        }

        /// <summary>
        /// Copies all files from a source folder to a target folder. Creates the target folder if it does not exist.
        /// </summary>
        /// <param name="sourcePath">The source folder path.</param>
        /// <param name="targetPath">The target folder path.</param>
        /// <returns><c>true</c> if the folder exists and files were copied; otherwise <c>false</c>.</returns>
        public static bool CopyFolder(string sourcePath, string targetPath)
        {
            string fileName = "";
            string destFile = "";
            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
            }
            if (Directory.Exists(sourcePath))
            {
                foreach (string s in Directory.GetFiles(sourcePath))
                {
                    fileName = Path.GetFileName(s);
                    destFile = Path.Combine(targetPath, fileName);
                    System.IO.File.Copy(s, destFile, true);
                }
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Deletes a folder at the specified path.
        /// </summary>
        /// <param name="dir">The folder path.</param>
        /// <param name="recursivo">If <c>true</c>, deletes the folder recursively including all subfolders and files.</param>
        /// <returns><c>true</c> if the folder existed and was deleted; otherwise <c>false</c>.</returns>
        public static bool DeleteFolder(string dir, bool recursivo)
        {
            if (Directory.Exists(dir))
            {
                Directory.Delete(dir, recursivo);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Counts the number of occurrences of a specific string in a file.
        /// </summary>
        /// <param name="file">The file path to search in.</param>
        /// <param name="str">The string to count.</param>
        /// <returns>The number of occurrences of the string in the file.</returns>
        public static int CountStr(string file, string str)
        {
            string text;
            MatchCollection tokensCollection;
            int i1;
            using (TextReader reader = (TextReader)System.IO.File.OpenText(file))
            {
                text = reader.ReadToEnd();
                reader.Close();
                tokensCollection = Regex.Matches(text, str);
                i1 = tokensCollection.Count;
            }
            return i1;
        }
    }
}
