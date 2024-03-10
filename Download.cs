using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace TestBunifu
{
    internal class Download
    {
        private DatabaseOperations dbOperations;

        public Download()
        {
            dbOperations = new DatabaseOperations();
        }

        public async Task<bool> DownloadFileAsync(string url, IProgress<Tuple<int, string>> progress = null)
        {
            try
            {
                HttpHandler httpHandler = new HttpHandler();
                byte[] fileBytes = await httpHandler.DownloadFileAsync(url, progress);

                if (fileBytes != null)
                {
                    string fileName = Path.GetFileName(url);
                    string downloadsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");

                    string filePath = Path.Combine(downloadsPath, fileName);

                    Console.WriteLine($"Downloading file to: {filePath}");

                    File.WriteAllBytes(filePath, fileBytes);

                    Console.WriteLine($"File downloaded successfully to: {filePath}");

                    
                    UpdateDatabase(fileName, fileBytes.Length, "Downloaded", DateTime.Now, "PDF");

                    return true; // Return true indicating successful download
                }
                else
                {
                    Console.WriteLine("File bytes are null.");
                    return false; // Return false indicating download failure
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error downloading file: {ex.Message}");
                return false; // Return false indicating download failure
            }
        }





        private void UpdateDatabase(string fileName, long sizeInBytes, string status, DateTime lastTryDate, string type)
        {
            try
            {

                //New feature is added here:File Extension
                string fileExtension = Path.GetExtension(fileName).ToLower();
                string fileType;

                // Determine the file type based on the extension
                switch (fileExtension)
                {
                    
                    case ".bat":
                    case ".cmd":
                    case ".db":
                    case ".sqlite":
                    case ".sqlitedb":
                    case ".mdb":
                    case ".json":
                    case ".geojson":
                    case ".jsonl":
                    case ".exe":
                    case ".qdz":
                    case ".dll":
                    case ".msi":
                    case ".java":
                    case ".c":
                    case ".cpp":
                    case ".cs":
                    case ".py":
                    case ".js":
                    case ".html":
                    case ".css":
                    case ".php":
                    case ".rb":
                    case ".swift":
                    case ".go":
                    case ".ts":
                    case ".scala":
                    case ".perl":
                    case ".lua":
                    case ".dart":
                    case ".kotlin":
                    case ".rust":
                    case ".r":
                    case ".sql":
                    case ".pl":
                    case ".vb":
                    case ".asm":
                        fileType = "Software"; 
                        break;
                    case ".jpg":
                    case ".jpeg":
                    case ".png":
                    case ".gif":
                    case ".jpe":
                    case ".jfif":
                    case ".pjpeg":
                    case ".pjp":
                        fileType = "Image";
                        break;
                    case ".mp4":
                    case ".avi":
                    case ".mov":
                    case ".wmv":
                        fileType = "Video";
                        break;
                    case ".epub":
                    case ".pdf":
                    case ".xll":
                    case ".doc":
                    case ".docx":
                    case ".rtx":
                    case ".txt":
                    case ".pptx":
                    case ".ppt":
                    case ".pub":
                        fileType = "Document"; //Just grouping them in order to filter 
                        break;
                    case ".zip":
                    case ".tar":
                    case ".rar":
                    case ".gz":
                    case ".7z":
                        fileType = "Compressed";
                        break;
                    case ".mp3":
                    case ".wav":
                    case ".ogg":
                    case ".flac":
                        fileType = "Music";
                        break;
                    default:
                        fileType = "Other";
                        break;
                }

                using (SQLiteConnection connection = dbOperations.OpenConnection())
                {
                    using (SQLiteCommand command = new SQLiteCommand("INSERT INTO Downloads (Name, Size, Status, LastTryDate, Type) VALUES (@Name, @Size, @Status, @LastTryDate, @Type)", connection))
                    {

                        //using the method conversion method here
                        string readableSize = ConvertBytesToReadableFormat(sizeInBytes);

                        command.Parameters.AddWithValue("@Name", fileName);

                        command.Parameters.AddWithValue("@Size", readableSize);
                        command.Parameters.AddWithValue("@Status", status);
                        command.Parameters.AddWithValue("@LastTryDate", lastTryDate);
                        command.Parameters.AddWithValue("@Type", fileType);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating database: {ex.Message}");
            }
        }

        public List<DownloadInfo> GetAllDownloads()
        {
            List<DownloadInfo> downloads = new List<DownloadInfo>();

            try
            {
                using (SQLiteConnection connection = dbOperations.OpenConnection())
                {
                    using (SQLiteCommand command = new SQLiteCommand("SELECT Name, Size, Status, LastTryDate, Type FROM Downloads", connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DownloadInfo download = new DownloadInfo
                                {
                                    Name = reader["Name"].ToString(),
                                    Size = reader["Size"].ToString(),
                                    Status = reader["Status"].ToString(),
                                    LastTryDate = DateTime.Parse(reader["LastTryDate"].ToString()),
                                    Type = reader["Type"].ToString()
                                };

                                downloads.Add(download);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving downloads: {ex.Message}");
            }

            return downloads;
        }


        /*Since all of our download are recorded as bytes instead of normal converted version
         * we need to cretae an method to convert bytes into readble formats like kb/mb etc
         */
        private string ConvertBytesToReadableFormat(long bytes)
        {
            string[] sizes = { "Bytes", "KB", "MB", "GB", "TB" };
            double formattedSize = bytes;
            int sizeIndex = 0;
            while (formattedSize >= 1024 && sizeIndex < sizes.Length - 1)
            {
                formattedSize /= 1024;
                sizeIndex++;
            }
            return $"{formattedSize:0.##} {sizes[sizeIndex]}";
        }



        /*Creating here new method for the btn Conponents that they filter 
         * out the expected output based on the user requirement/ method will filter 
         * the databse and show the downloaded data  records
         */

        public List<DownloadInfo> GetDownloadsByType(string fileType)
        {
            List<DownloadInfo> downloads = new List<DownloadInfo> ();

            try
            {
                using (SQLiteConnection connection = dbOperations.OpenConnection())
                {
                    using (SQLiteCommand command = new SQLiteCommand("SELECT Name, Size, Status, LastTryDate, Type FROM Downloads WHERE Type = @Type", connection))
                    {
                        command.Parameters.AddWithValue("@Type", fileType);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DownloadInfo download = new DownloadInfo
                                {
                                    Name = reader["Name"].ToString(),
                                    Size = reader["Size"].ToString(),
                                    Status = reader["Status"].ToString(),
                                    LastTryDate = DateTime.Parse(reader["LastTryDate"].ToString()),
                                    Type = reader["Type"].ToString()
                                };

                                downloads.Add(download);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving downloads by type: {ex.Message}");

            }

            return downloads;

        }
    }

    }

    

