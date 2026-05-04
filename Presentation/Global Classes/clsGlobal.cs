using Buisness;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Presentation.Global_Classes
{
  public static class clsGlobal
    {
        public static clsUser CurrentUser;
        public static bool RememberUsernameAndPassword(
         string UserName , string password   )
        {
            try {
                string currentDirectory = System.IO.Directory.GetCurrentDirectory();
                string filePath = currentDirectory + "\\data.txt";
                if (UserName=="" && File.Exists(filePath)) { 
                    File.Delete(filePath);
                    return true;
                }
                string DataToSave = UserName + "#//#" + password;
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine(DataToSave);
                    return true;
                }
              
            }
            catch (Exception ex){ 
                MessageBox.Show(ex.Message);
                return false;
            }
            

        }


        public static bool GetStoredCredential(
            ref string UserName, ref string Password)
        {
            try
            {
                string currentDirectory = System.IO.Directory.GetCurrentDirectory();
                string FilePath = currentDirectory + "\\data.txt";
                if (File.Exists(FilePath))
                {
                    using (StreamReader reader = new StreamReader(FilePath))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            Console.WriteLine(line);
                            string[] result = line.Split(new string[] { "#//#" }, StringSplitOptions.None);
                            UserName = result[0];
                            Password = result[1];

                        }
                        return true;
                    }

                }
                else { return false; }


            }
            catch (Exception ex){
                MessageBox.Show(ex.Message);
                return false; }


    }
    }

}
