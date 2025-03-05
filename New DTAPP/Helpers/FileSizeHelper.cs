using New_DTAPP.Models;

namespace New_DTAPP.Helpers
{
    public class FileSizeHelper
    {
        public List<Tuple<string, int>> GetFileExtensions(IQueryable<TransferModel> transfers, int column)
        {

            List<Tuple<string, int>> columns = new List<Tuple<string, int>>();

            foreach (TransferModel transfer in transfers)
            {

                if (transfer.Files.Count >= 1)
                {
                    foreach (FileModel file in transfer.Files)
                    {
                        if (file.FileExt.StartsWith("."))
                        {
                            file.FileExt=file.FileExt.Substring(1);
                        }
                        if (!(columns.FindIndex(x => x.Item1.Equals(file.FileExt.ToUpper())) > -1))
                        {
                            columns.Add(Tuple.Create(file.FileExt.ToUpper(), column));
                            column++;
                        }

                    }
                }
            }
            return columns;
        }
        public int FileCountByType(ICollection<FileModel> files, string type)
        {

            int total = 0;
            int parseResult = 0;
            if (files.Count == 0)
            {
                return total;
            }
            foreach (FileModel file in files)
            {
                if (file.FileExt.StartsWith("."))
                {
                    file.FileExt = file.FileExt.Substring(1);
                }
                if (file.FileExt.ToUpper() == type.ToUpper())
                {
                    if (int.TryParse(file.FileComment, out parseResult))
                    {
                        total += parseResult;
                    }
                    else
                    {
                        total++;
                    }
                }
            }
            return total;
        }

        public int TotalFileCount(ICollection<FileModel> files)
        {
            int total = 0;
            int parseResult = 0;
            if (files.Count == 0)
            {
                return total;
            }
            foreach (FileModel file in files)
            {
                if (int.TryParse(file.FileComment, out parseResult))
                {
                    total += parseResult;
                }
                else
                {
                    total++;
                }
            }
            return total;
        }

        public string TotalFileSize(ICollection<FileModel> files)
        {
            decimal totalFileSize = 0;
            if (files.Count == 0)
            {
                return "";
            }
            foreach (FileModel file in files)
            {
                totalFileSize += returnBytes(file.FileSize);
            }
            return returnFileSize(totalFileSize);
        }

        public decimal TotalFileSizeInBytes(ICollection<FileModel> files)
        {
            decimal totalFileSize = 0;
            if (files.Count == 0)
            {
                return totalFileSize;
            }
            foreach (FileModel file in files)
            {
                totalFileSize += returnBytes(file.FileSize);
            }
            return totalFileSize;
        }

        public string returnFileSize(decimal bytes)
        {
            if (bytes < 1024)
            {
                return decimal.Round(bytes, 2).ToString() + " bytes";
            }
            else if (bytes >= 1024 && bytes < 1048576)
            {
                return decimal.Round(bytes / 1024, 2).ToString() + " KB";
            }
            else if (bytes >= 1048576 && bytes < 1073741824)
            {
                return decimal.Round(bytes / 1048576, 2).ToString() + " MB";
            }
            else if (bytes >= 1073741824)
            {
                return decimal.Round(bytes / 1073741824, 2).ToString() + " GB";
            }
            return "";
        }

        public decimal returnBytes(string size)
        {
            if (size.IndexOf(" bytes") != -1)
            {
                return decimal.Round(decimal.Parse(size.Substring(0, size.IndexOf(" bytes"))), 2);
            }
            else if (size.IndexOf(" KB") != -1)
            {

                return decimal.Round((decimal.Parse(size.Substring(0, size.IndexOf(" KB"))) * 1024), 2);
            }
            else if (size.IndexOf(" MB") !=-1)
            {
                size.Substring(0, size.IndexOf(" MB"));
                return decimal.Round((decimal.Parse(size.Substring(0, size.IndexOf(" MB"))) * 1024 * 1024), 2);
            }
            else if (size.IndexOf(" GB") != -1)
            {
                size.Substring(0, size.IndexOf(" GB"));
                return decimal.Round((decimal.Parse(size.Substring(0, size.IndexOf(" GB"))) * 1024 * 1024 * 1024), 2);
            }
            return 0;
        }
    }
}
