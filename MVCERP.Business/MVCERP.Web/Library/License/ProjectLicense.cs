using Microsoft.Win32;
using System;
using System.IO;
using System.Web;
using System.Text;
using System.Security.Cryptography;
using board = HardwareMotherboardID.nsMotherBoardID.MotherBoardID; //Allows for checking of Motherboard ID number. It never changes, so it's a good way of seeing if registry entries were copied to another computer


namespace MVCERP.Web.Library.License
{
    public class iSolutionLifeLicense
    {
         #region "Variables"

        private string AllowUsers = "iSol_Lif=10"; // Private Key is a secure and only Sarvanam management know this key, (=3) it means how many users allow.
        //private string AllowDomain = "EFBCP"          // Domain Name where the original provided application runs.
        private int TrialPeriod = 365;                   // Days in the trial. Thirty is usally the average.
        public static string BuyNowURL = "mailto:sanjayatripathi@gmail.com&Subject=iSolutionLife Registration Code"; // The URL of the purchase page.!
        private string RegSubKeyName = "iSolution\\Life"; //The name for the sub-key to store registry info. It is more secure if you make this hard to guess. You can create multiple levels by using \\ (i.e. PAW\\Blah\\Yada\\Yada)
        private Encryption.Data key;                    //If encryption is set, choose a key to encrypt with.
        private string EnvFilePath;                     // The file which store inside the ALL Users Application Directory.
        private string AllowDomain;

        #endregion

        #region "Options"

        public bool SilentStart = false;    //the registration screen to show unless the trial is over.
        public bool SupportMe = true;       //the small about button. It's not required to give me credit, just appriciated.
        public bool CreateKeys = true;      //create the registry keys for start date during setup. The program will disable access if they aren't there, as opposed to creating them.
        public bool EncryptKeys = true;     //Encrypt the keys so they can't be changed. Most secure when combined with the above option, but you'll have to configure setup to encrypt them! See encryption key in Variables.

        #endregion

        #region "License Status"
        public enum LicStatus : int
        {
            Expire = 1,
            Exception = 2,
            ClockChanged = 3,
            FullVersion = 4,
            Trail = 5
        }
        public enum RegStatus : int
        {
            InvalidCode = 1,
            InvalidDomain = 2,
            Pirated = 3,
            Success = 4
        }
        #endregion


        public bool InTrial = true;
        public bool FullVersion = false;
        public string UsedTrialDays = "";
        public string AUsers;
        public string RN = "";
        public string RC = "";


        //Constructor
        public iSolutionLifeLicense()
        {
            key = new Encryption.Data(AllowUsers);
            EnvFilePath = GetFileName();
            AllowDomain = GetDomainName();
            AUsers = "Users=" + AllowUsers.Split('=')[1];
        }

        public LicStatus CheckLicense(string DomainName)
        {
            bool domainMatch = IsDomainCorrect(DomainName);
            try
            {
                RegistryKey oReg;
                oReg = Registry.LocalMachine.OpenSubKey("Software", true);
                oReg = oReg.CreateSubKey(RegSubKeyName);
                oReg = Registry.LocalMachine.OpenSubKey("Software\\\\" + RegSubKeyName);

                string OldDay = oReg.GetValue("ComputerID", "").ToString();
                string OldMonth = oReg.GetValue("motherboardsettings", "").ToString();
                string OldYear = oReg.GetValue("processor", "").ToString();
                string RegName = oReg.GetValue("RAM", "").ToString();
                string RegCode = oReg.GetValue("Memory", "").ToString();
                string CompID = oReg.GetValue("CDROMSettings", "").ToString();
                string TrialDone = oReg.GetValue("ScannerSettings", "").ToString();
                string FirstTime = oReg.GetValue("LANUSBSettings", "").ToString();
                oReg.Close();

                string FirstData = GetData(EnvFilePath);

                if (FirstTime == FirstData)
                {
                }
                else
                {
                    return LicStatus.Exception;
                }
                //The keys created.
                switch (CreateKeys)
                {
                    case true:
                        if (OldDay == "" && OldMonth == "" && OldYear == "" && FirstTime == "" && FirstData == "")
                        {
                            CreateRegKeys();
                        }
                        break;

                    case false:
                        if (OldDay == "" || OldMonth == "" || OldYear == "")
                        {
                            return LicStatus.Exception;
                        }
                        break;
                }

                if (OldDay == null || OldMonth == null || OldYear == null)
                {
                    return LicStatus.Exception;
                }

                //See if the user is already registered, if so re-process the info and check if the computer is all okay.
                if (RegName == "" || !domainMatch)
                {
                }
                else
                {
                    RN = DecryptU(RegName);
                    RC = DecryptU(RegCode);
                    string UserName = RegName;
                    UserName = UserName.Remove(16, (UserName.Length - 16));
                    if (UserName == DecryptU(RegCode))
                    {
                        string set_CompID = GetData(HttpContext.Current.Server.MapPath("./Settings.set")).Trim();
                        string boardID = EncryptU(getMotherboardId());
                        if (boardID == CompID && boardID == set_CompID)
                        {
                            InTrial = false;
                            FullVersion = true;
                            oReg = Registry.LocalMachine.OpenSubKey("Software", true);
                            oReg = oReg.CreateSubKey(RegSubKeyName);
                            oReg.SetValue("ScannerSettings", "");
                            oReg.Close();

                            return LicStatus.FullVersion;
                        }
                        else
                        {
                            return LicStatus.Exception;
                        }
                    }
                    else
                    {
                        return LicStatus.Exception;
                    }
                }


                //Decrypt the keys
                if (EncryptKeys)
                {
                    OldDay = Decrypt(OldDay);
                    OldMonth = Decrypt(OldMonth);
                    OldYear = Decrypt(OldYear);
                }

                //Define global variables.
                UsedTrialDays = DiffDate(OldDay, OldMonth, OldYear).ToString();

                //Disable the continue button if the trial is over
                if (DiffDate(OldDay, OldMonth, OldYear) > TrialPeriod)
                {
                    InTrial = false;
                    oReg = Registry.LocalMachine.OpenSubKey("Software", true);
                    oReg = oReg.CreateSubKey(RegSubKeyName);
                    oReg.SetValue("ScannerSettings", "1");
                    oReg.Close();

                    return LicStatus.Expire;
                }

                //If the date is earlier than possible, then disable the program.
                if (OldMonth == "")
                {
                }
                else
                {
                    string OldDate = OldMonth + "/" + OldDay + "/" + OldYear;
                    if (DateTime.Compare(DateTime.Now, Convert.ToDateTime(OldDate)) < 0)
                    {
                        InTrial = false;
                        oReg = Registry.LocalMachine.OpenSubKey("Software", true);
                        oReg = oReg.CreateSubKey(RegSubKeyName);
                        oReg.SetValue("ScannerSettings", "1");
                        oReg.Close();

                        return LicStatus.Expire;
                    }
                }

                //If the trial is done then disable the button
                if (TrialDone == "1")
                {
                    InTrial = false;

                    return LicStatus.ClockChanged;
                }

                return LicStatus.Trail;
            }
            catch
            {
                return LicStatus.Exception;
            }

        }

        #region "Methods"

        private Encryption.Hash hash = new Encryption.Hash(Encryption.Hash.Provider.SHA512);
        struct CurrentDate
        {
            public string Day;
            public string Month;
            public string Year;
        }

        private void CreateRegKeys()
        {
            try
            {
                CurrentDate Current;
                Current.Day = DateTime.Now.Day.ToString();
                Current.Month = DateTime.Now.Month.ToString();
                Current.Year = DateTime.Now.Year.ToString();
                if (EncryptKeys == true)
                {
                    Current.Day = Encrypt(Current.Day);
                    Current.Month = Encrypt(Current.Month);
                    Current.Year = Encrypt(Current.Year);
                }
                string CurrentDate = Encrypt(Current.Year + "-" + Current.Month + "-" + Current.Day + "-");
                RegistryKey oReg;
                oReg = Registry.LocalMachine.OpenSubKey("Software", true);
                oReg = oReg.CreateSubKey(RegSubKeyName);
                oReg.SetValue("processor", Current.Year);
                oReg.SetValue("ComputerID", Current.Day);
                oReg.SetValue("motherboardsettings", Current.Month);
                oReg.SetValue("LANUSBSettings", CurrentDate);
                oReg.Close();
                CreateFile(CurrentDate);
            }
            catch
            {
            }
        }

        private void CreateFile(string CurrentDate)
        {
            System.IO.StreamWriter oWrite;
            oWrite = new StreamWriter(EnvFilePath, true);
            oWrite.WriteLine(CurrentDate);
            oWrite.Close();
        }

        private string Encrypt(string text)
        {
            Encryption.Symmetric sym = new Encryption.Symmetric(Encryption.Symmetric.Provider.Rijndael, true);
            Encryption.Data encryptedData;
            encryptedData = sym.Encrypt(new Encryption.Data(text), key);
            return encryptedData.ToHex();
        }

        private string Decrypt(string hexstream)
        {
            try
            {
                Encryption.Symmetric sym = new Encryption.Symmetric(Encryption.Symmetric.Provider.Rijndael, true);
                Encryption.Data encryptedData = new Encryption.Data();
                encryptedData.Hex = hexstream;
                Encryption.Data decryptedData;
                decryptedData = sym.Decrypt(encryptedData, key);
                return decryptedData.ToString();
            }
            catch
            {
                return "";
            }
        }
        public static int DateDiff(DateTime date1, DateTime date2)
        {
            TimeSpan diffResult = date2 - date1;
            return diffResult.Days;
        }
        private int DiffDate(string OrigDay, string OrigMonth, string OrigYear)
        {
            try
            {
                string D1 = OrigMonth + "/" + OrigDay + "/" + OrigYear;

                return DateDiff(Convert.ToDateTime(D1), DateTime.Now);
            }
            catch
            {
                return 0;
            }
        }

        private string DecryptU(string hexstream)
        {
            try
            {
                Encryption.Symmetric sym = new Encryption.Symmetric(Encryption.Symmetric.Provider.Rijndael, true);
                Encryption.Data encryptedData = new Encryption.Data();
                encryptedData.Hex = hexstream;
                Encryption.Data decryptedData;
                decryptedData = sym.Decrypt(encryptedData, key);
                return decryptedData.ToString();
            }
            catch
            {
                return "";
            }
        }

        private string EncryptU(string text)
        {
            Encryption.Symmetric sym = new Encryption.Symmetric(Encryption.Symmetric.Provider.Rijndael, true);
            Encryption.Data encryptedData;
            encryptedData = sym.Encrypt(new Encryption.Data(text), key);
            return encryptedData.ToHex();
        }

        private Environment.SpecialFolder GetSpecialFolderFromList(string FolderName)
        {
            //"CommonApplicationData"

            // lstFolders.SelectedItem returns the name of the 
            // special folder. System.Enum.Parse will turn that
            // into an object corresponding to the enumerated value
            // matching the specific text. CType then converts the
            // object into an Environment.SpecialFolder object.
            // This is all required because Option Strict is on.
            return (Environment.SpecialFolder)System.Enum.Parse(typeof(Environment.SpecialFolder), FolderName);
        }

        private string GetFileName()
        {
            Environment.SpecialFolder sf;

            sf = GetSpecialFolderFromList("CommonApplicationData");
            string fdName = Environment.GetFolderPath(sf) + "\\iSolution\\Life\\";

            try
            {
                if (!Directory.Exists(fdName))
                {
                    Directory.CreateDirectory(fdName);
                }

                fdName = fdName + "LANUSBSettings.set";
            }
            catch
            {
                fdName = "Error Occured.";
            }
            return fdName;
        }

        private string GetDomainName()
        {
            string fdName = EnvFilePath;
            string Key = "";

            try
            {
                System.IO.StreamReader oRead = null;

                try
                {
                    if (!File.Exists(fdName))
                    {
                        Key = "";
                    }
                    else
                    {

                        oRead = File.OpenText(fdName);
                        Key = oRead.ReadLine();
                        Key = oRead.ReadLine();
                        if (Key == null)
                        {
                            Key = "";
                        }
                        else
                        {
                            Key = DecryptU(Key);
                        }

                    }
                }
                catch
                {
                    Key = "";
                }
                finally
                {
                    if (oRead != null) oRead.Close();
                }
            }
            catch
            {
                fdName = "";
            }

            return Key;

        }

        private string GetData(string fileName)
        {
            System.IO.StreamReader oRead = null;
            string Key;
            try
            {
                if (!File.Exists(fileName))
                {
                    Key = "";
                }
                else
                {
                    oRead = File.OpenText(fileName);
                    Key = oRead.ReadLine();
                }
            }
            catch
            {
                Key = "Error";
            }
            finally
            {
                if (oRead != null) oRead.Close();
            }

            return Key;
        }

        private string GetData()
        {
            System.IO.StreamReader oRead = null;
            string fileName = HttpContext.Current.Server.MapPath("Settings.set");
            string Key;
            try
            {
                if (!File.Exists(fileName))
                {
                    Key = "newthanksnot";
                }
                else
                {
                    oRead = File.OpenText(fileName);
                    Key = oRead.ReadToEnd();
                }
            }
            catch
            {
                Key = "Error";
            }
            finally
            {
                if (oRead != null) oRead.Close();
            }

            return Key;
        }

        public bool IsDomainCorrect(string DomainName)
        {
            bool rValue;

            if (DomainName.Equals(AllowDomain.Trim().ToLower())) rValue = true; else rValue = false;

            return rValue;
        }

        public string getMotherboardId()
        {
            if (board.GetMotherBoardID() == null)
            {
                return "951753258456ISOL";
            }
            else
            {
                return board.GetMotherBoardID().Trim().ToString();
            }
        }

        public string GenerateKey(string txtUser, string txtDomain)
        {
            try
            {
               

                string UserName = Encrypt(txtUser.ToUpper());
                string DomainName = Encrypt(txtDomain.ToUpper());

                UserName = UserName.Remove(16, (UserName.Length - 16));
                DomainName = DomainName.Remove(16, (DomainName.Length - 16));
                
                //RegCode = RegCode.Replace("-", "");
                //string DomainCode = RegCode.Substring(16, 16);
                //RegCode = RegCode.Substring(0, 16);

                if (UserName != "" & DomainName != "")
                {
                    return UserName + "-" + DomainName;
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }
        }
        public RegStatus RegisterData(string txtUser, string txtReg, string txtDomain)
        {
            try
            {
                string RegCode = txtReg.ToUpper();
                string UserName = Encrypt(txtUser.ToUpper());
                string DomainName = Encrypt(txtDomain.ToUpper());

                UserName = UserName.Remove(16, (UserName.Length - 16));
                DomainName = DomainName.Remove(16, (DomainName.Length - 16));
                RegCode = RegCode.Replace("-", "");

                string DomainCode = RegCode.Substring(16, 16);
                RegCode = RegCode.Substring(0, 16);

                if (UserName == RegCode & DomainName == DomainCode)
                {
                    return AuthorizeComputer(txtUser.ToUpper().Trim(), RegCode, txtDomain.ToUpper().Trim());
                }
                else
                {
                    return RegStatus.InvalidCode;
                }
            }
            catch
            {
                return RegStatus.InvalidCode;
            }
        }

        private RegStatus AuthorizeComputer(string Username, string RegCode, string DomainName)
        {
            string MotherboardID = Encrypt(getMotherboardId());

            string saveData = GetData().Trim().ToLower();
            if (saveData.Equals("newthanks") || saveData.Equals(MotherboardID.Trim().ToLower().Trim()) || saveData.Equals("newthanksnot"))
            {
                if (DomainName.Trim().ToLower() == GetUrlName().Trim().ToLower())
                {
                    RegistryKey oReg;
                    oReg = Registry.LocalMachine.OpenSubKey("Software", true);
                    oReg = oReg.CreateSubKey(RegSubKeyName);

                    oReg.SetValue("RAM", Encrypt(Username));
                    oReg.SetValue("Memory", Encrypt(RegCode));
                    oReg.SetValue("ScannerSettings", "");
                    oReg.SetValue("CDROMSettings", MotherboardID);
                    oReg.Close();
                    WriteToFile(MotherboardID);
                    SaveDomain(EncryptU(DomainName));
                    return RegStatus.Success;
                }
                else
                {
                    return RegStatus.InvalidDomain;
                }
            }
            else
            {
                return RegStatus.Pirated;
            }

        }

        private void WriteToFile(string saveData)
        {
            System.IO.StreamWriter oWrite = null;
            string fileName = HttpContext.Current.Server.MapPath("Settings.set");
            try
            {
                oWrite = File.CreateText(fileName);
                oWrite.Write(saveData);
                oWrite.Close();
            }
            catch
            {
            }
            finally
            {
                if (oWrite != null) oWrite.Close();
            }
        }

        private void SaveDomain(string DomainName)
        {
            System.IO.StreamWriter oWrite = null;
            string fileName = EnvFilePath;
            try
            {
                oWrite = new StreamWriter(fileName, true);
                oWrite.WriteLine(DomainName);
                oWrite.Close();
            }
            catch
            {
            }
            finally
            {
                if (oWrite != null) oWrite.Close();
            }
        }

        private string GetUrlName()
        {
            string cURL = HttpContext.Current.Request.Url.AbsoluteUri;
            string[] DomainNames = cURL.Split('/');

            if (DomainNames.Length > 3)
            {
                return DomainNames[2];
            }
            else
            {
                return "Not Found";
            }

        }

        #endregion
    }
}