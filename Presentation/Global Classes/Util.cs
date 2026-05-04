using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Global_Classes
{
  public  class Util
    {
        public static string GenerateGUID()
        {
            Guid newGuid = Guid.NewGuid();
            return newGuid.ToString();
        }
public static bool CreateFolderIfDoesNotExist(string FolderPath)
        {
            if (!Directory.Exists(FolderPath))
            {
                try { 
                    Directory.CreateDirectory(FolderPath);
                    return true;
                }
                catch (Exception ex){ return false; }

            }
            return true;
        }
  
    public static string ReplaceFileNameWithGUID
            (string SourceFile) { 
        string FileName=SourceFile;
     FileInfo fi=new FileInfo(FileName);
            string extn= fi.Extension;
            return GenerateGUID() + extn;
        }
        public static bool CopyImageToProjectImagesFolder
            (ref string SourceFile)
        {
            string DestinationFolder = @"D:\c#project linces\DVLD-People-Images\";
            if (!CreateFolderIfDoesNotExist(DestinationFolder))
            {
                return false;
            }
            string destinationFile = DestinationFolder + ReplaceFileNameWithGUID(SourceFile);
            try
            {
                File.Copy(SourceFile, destinationFile, true);

            }
            catch (IOException iox) {
                return false;
                
            }
            SourceFile= destinationFile;
            return true;
        }
    }
}
