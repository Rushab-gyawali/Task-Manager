using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Configuration;


namespace Encryption
{

    #region " Hash"

    /// 
    /// Hash functions are fundamental to modern cryptography. These functions map binary 
    /// strings of an arbitrary length to small binary strings of a fixed length, known as 
    /// hash values. A cryptographic hash function has the property that it is computationally
    /// infeasible to find two distinct inputs that hash to the same value. Hash functions 
    /// are commonly used with digital signatures and for data integrity.
    /// 
    public class Hash
    {

        /// 
        /// Type of hash; some are security oriented, others are fast and simple
        /// 
        public enum Provider
        {
            /// 
            /// Cyclic Redundancy Check provider, 32-bit
            /// 
            CRC32,
            /// 
            /// Secure Hashing Algorithm provider, SHA-1 variant, 160-bit
            /// 
            SHA1,
            /// 
            /// Secure Hashing Algorithm provider, SHA-2 variant, 256-bit
            /// 
            SHA256,
            /// 
            /// Secure Hashing Algorithm provider, SHA-2 variant, 384-bit
            /// 
            SHA384,
            /// 
            /// Secure Hashing Algorithm provider, SHA-2 variant, 512-bit
            /// 
            SHA512,
            /// 
            /// Message Digest algorithm 5, 128-bit
            /// 
            MD5
        }

        private HashAlgorithm _Hash;
        private Data _HashValue = new Data();

        private Hash()
        {
        }

        /// 
        /// Instantiate a new hash of the specified type
        /// 
        public Hash(Provider p)
        {
            switch (p)
            {
                case Provider.CRC32:
                    _Hash = new CRC32();
                    break;
                case Provider.MD5:
                    _Hash = new MD5CryptoServiceProvider();
                    break;
                case Provider.SHA1:
                    _Hash = new SHA1Managed();
                    break;
                case Provider.SHA256:
                    _Hash = new SHA256Managed();
                    break;
                case Provider.SHA384:
                    _Hash = new SHA384Managed();
                    break;
                case Provider.SHA512:
                    _Hash = new SHA512Managed();
                    break;
            }
        }

        /// 
        /// Returns the previously calculated hash
        /// 
        public Data Value
        {
            get { return _HashValue; }
        }

        /// 
        /// Calculates hash on a stream of arbitrary length
        /// 
        public Data Calculate(ref System.IO.Stream s)
        {
            _HashValue.Bytes = _Hash.ComputeHash(s);
            return _HashValue;
        }

        /// 
        /// Calculates hash for fixed length 
        /// 
        public Data Calculate(Data d)
        {
            return CalculatePrivate(d.Bytes);
        }

        /// 
        /// Calculates hash for a string with a prefixed salt value. 
        /// A "salt" is random data prefixed to every hashed value to prevent 
        /// common dictionary attacks.
        /// 
        public Data Calculate(Data d, Data salt)
        {
            byte[] nb = new byte[d.Bytes.Length + salt.Bytes.Length];
            salt.Bytes.CopyTo(nb, 0);
            d.Bytes.CopyTo(nb, salt.Bytes.Length);
            return CalculatePrivate(nb);
        }

        /// 
        /// Calculates hash for an array of bytes
        /// 
        private Data CalculatePrivate(byte[] b)
        {
            _HashValue.Bytes = _Hash.ComputeHash(b);
            return _HashValue;
        }

        #region " CRC32 HashAlgorithm"

        private class CRC32 : HashAlgorithm
        {

            private int result = -1;

            protected override void HashCore(byte[] array, int ibStart, int cbSize)
            {
                int lookup;
                for (int i = ibStart; i <= cbSize - 1; i++)
                {
                    lookup = (result & 255) ^ array[i];
                    result = ((result & -256) / 256) & 16777215;
                    result = result ^ crcLookup[lookup];
                }
            }

            protected override byte[] HashFinal()
            {
                byte[] b = BitConverter.GetBytes(~this.result);
                Array.Reverse(b);
                return b;
            }

            public override void Initialize()
            {
                result = -1;
            }

            private int[] crcLookup = {0, 1996959894, -301047508, -1727442502, 124634137, 1886057615, -379345611, -1637575261, 249268274, 2044508324, 
-522852066, -1747789432, 162941995, 2125561021, -407360249, -1866523247, 498536548, 1789927666, -205950648, -2067906082, 
450548861, 1843258603, -187386543, -2083289657, 325883990, 1684777152, -43845254, -1973040660, 335633487, 1661365465, 
-99664541, -1928851979, 997073096, 1281953886, -715111964, -1570279054, 1006888145, 1258607687, -770865667, -1526024853, 
901097722, 1119000684, -608450090, -1396901568, 853044451, 1172266101, -589951537, -1412350631, 651767980, 1373503546, 
-925412992, -1076862698, 565507253, 1454621731, -809855591, -1195530993, 671266974, 1594198024, -972236366, -1324619484, 
795835527, 1483230225, -1050600021, -1234817731, 1994146192, 31158534, -1731059524, -271249366, 1907459465, 112637215, 
-1614814043, -390540237, 2013776290, 251722036, -1777751922, -519137256, 2137656763, 141376813, -1855689577, -429695999, 
1802195444, 476864866, -2056965928, -228458418, 1812370925, 453092731, -2113342271, -183516073, 1706088902, 314042704, 
-1950435094, -54949764, 1658658271, 366619977, -1932296973, -69972891, 1303535960, 984961486, -1547960204, -725929758, 
1256170817, 1037604311, -1529756563, -740887301, 1131014506, 879679996, -1385723834, -631195440, 1141124467, 855842277, 
-1442165665, -586318647, 1342533948, 654459306, -1106571248, -921952122, 1466479909, 544179635, -1184443383, -832445281, 
1591671054, 702138776, -1328506846, -942167884, 1504918807, 783551873, -1212326853, -1061524307, -306674912, -1698712650, 
62317068, 1957810842, -355121351, -1647151185, 81470997, 1943803523, -480048366, -1805370492, 225274430, 2053790376, 
-468791541, -1828061283, 167816743, 2097651377, -267414716, -2029476910, 503444072, 1762050814, -144550051, -2140837941, 
426522225, 1852507879, -19653770, -1982649376, 282753626, 1742555852, -105259153, -1900089351, 397917763, 1622183637, 
-690576408, -1580100738, 953729732, 1340076626, -776247311, -1497606297, 1068828381, 1219638859, -670225446, -1358292148, 
906185462, 1090812512, -547295293, -1469587627, 829329135, 1181335161, -882789492, -1134132454, 628085408, 1382605366, 
-871598187, -1156888829, 570562233, 1426400815, -977650754, -1296233688, 733239954, 1555261956, -1026031705, -1244606671, 
752459403, 1541320221, -1687895376, -328994266, 1969922972, 40735498, -1677130071, -351390145, 1913087877, 83908371, 
-1782625662, -491226604, 2075208622, 213261112, -1831694693, -438977011, 2094854071, 198958881, -2032938284, -237706686, 
1759359992, 534414190, -2118248755, -155638181, 1873836001, 414664567, -2012718362, -15766928, 1711684554, 285281116, 
-1889165569, -127750551, 1634467795, 376229701, -1609899400, -686959890, 1308918612, 956543938, -1486412191, -799009033, 
1231636301, 1047427035, -1362007478, -640263460, 1088359270, 936918000, -1447252397, -558129467, 1202900863, 817233897, 
-1111625188, -893730166, 1404277552, 615818150, -1160759803, -841546093, 1423857449, 601450431, -1285129682, -1000256840, 
1567103746, 711928724, -1274298825, -1022587231, 1510334235, 755167117};

            public override byte[] Hash
            {
                get
                {
                    byte[] b = BitConverter.GetBytes(~this.result);
                    Array.Reverse(b);
                    return b;
                }
            }
        }

        #endregion

    }
    #endregion

    #region '"  Symmetric"'

    ///  <summary>
    ///  Symmetric encryption uses a single key to encrypt and decrypt. 
    ///  Both parties (encryptor and decryptor) must share the same secret key.
    ///  </summary>
    public class Symmetric
    {

        private const string _DefaultIntializationVector = "%1Az=-@qT";
        private const int _BufferSize = 2048;

        public enum Provider
        {
            ///  <summary>
            ///  The cryptData Encryption Standard provider supports a 64 bit key only
            ///  </summary>
            DES,
            ///  <summary>
            ///  The Rivest Cipher 2 provider supports keys ranging from 40 to 128 bits, default is 128 bits
            ///  </summary>
            RC2,
            ///  <summary>
            ///  The Rijndael (also known as AES) provider supports keys of 128, 192, or 256 bits with a default of 256 bits
            ///  </summary>
            Rijndael,
            ///  <summary>
            ///  The TripleDES provider (also known as 3DES) supports keys of 128 or 192 bits with a default of 192 bits
            ///  </summary>
            TripleDES,
        }


        // TRANSNOTUSED: Private Member Variable _cryptData
        // private cryptData _cryptData; 
        private Data _key;
        private Data _iv;
        private SymmetricAlgorithm _crypto;
        // TRANSNOTUSED: Private Member Variable _EncryptedBytes
        // private byte[] _EncryptedBytes; 
        // TRANSNOTUSED: Private Member Variable _UseDefaultInitializationVector
        // private bool _UseDefaultInitializationVector; 

        private Symmetric()
        {
        }

        ///  <summary>
        ///  Instantiates a new symmetric encryption object using the specified provider.
        ///  </summary>
        public Symmetric(Provider provider, [System.Runtime.InteropServices.Optional] bool useDefaultInitializationVector/* TRANSERROR: default parameter value: true */  )
        {
            switch (provider)
            {
                case Provider.DES:
                    _crypto = new DESCryptoServiceProvider();
                    break;
                case Provider.RC2:
                    _crypto = new RC2CryptoServiceProvider();
                    break;
                case Provider.Rijndael:
                    _crypto = new RijndaelManaged();
                    break;
                case Provider.TripleDES:
                    _crypto = new TripleDESCryptoServiceProvider();
                    break;
            }


            // -- make sure key and IV are always set, no matter what
            this.Key = RandomKey();
            if (useDefaultInitializationVector)
            {
                this.IntializationVector = new Data(_DefaultIntializationVector);
            }
            else
            {
                this.IntializationVector = RandomInitializationVector();
            }
        }

        ///  <summary>
        ///  Key size in bytes. We use the default key size for any given provider; if you 
        ///  want to force a specific key size, set this property
        ///  </summary>
        public int KeySizeBytes
        {
            get
            {
                return _crypto.KeySize / 8;
            }
            set
            {
                _crypto.KeySize = value * 8;
                _key.MaxBytes = value;
            }
        }

        ///  <summary>
        ///  Key size in bits. We use the default key size for any given provider; if you 
        ///  want to force a specific key size, set this property
        ///  </summary>
        public int KeySizeBits
        {
            get
            {
                return _crypto.KeySize;
            }
            set
            {
                _crypto.KeySize = value;
                _key.MaxBits = value;
            }
        }

        ///  <summary>
        ///  The key used to encrypt/decrypt cryptData
        ///  </summary>
        public Data Key
        {
            get
            {
                return _key;
            }
            set
            {
                _key = value;
                _key.MaxBytes = _crypto.LegalKeySizes[0].MaxSize / 8;
                _key.MinBytes = _crypto.LegalKeySizes[0].MinSize / 8;
                _key.StepBytes = _crypto.LegalKeySizes[0].SkipSize / 8;
            }
        }

        ///  <summary>
        ///  Using the default Cipher Block Chaining (CBC) mode, all cryptData blocks are processed using
        ///  the value derived from the previous block; the first cryptData block has no previous cryptData block
        ///  to use, so it needs an InitializationVector to feed the first block
        ///  </summary>
        public Data IntializationVector
        {
            get
            {
                return _iv;
            }
            set
            {
                _iv = value;
                _iv.MaxBytes = _crypto.BlockSize / 8;
                _iv.MinBytes = _crypto.BlockSize / 8;
            }
        }

        ///  <summary>
        ///  generates a random Initialization Vector, if one was not provided
        ///  </summary>
        public Data RandomInitializationVector()
        {
            _crypto.GenerateIV();
            Data d = new Data(_crypto.IV);
            return d;
        }


        ///  <summary>
        ///  generates a random Key, if one was not provided
        ///  </summary>
        public Data RandomKey()
        {
            _crypto.GenerateKey();
            Data d = new Data(_crypto.Key);
            return d;
        }


        ///  <summary>
        ///  Ensures that _crypto object has valid Key and IV
        ///  prior to any attempt to encrypt/decrypt anything
        ///  </summary>
        private void ValidateKeyAndIv(bool isEncrypting)
        {
            if (_key.IsEmpty)
            {
                if (isEncrypting)
                {
                    _key = RandomKey();
                }
                else
                {
                    throw new CryptographicException("No key was provided for the decryption operation!");
                }
            }
            if (_iv.IsEmpty)
            {
                if (isEncrypting)
                {
                    _iv = RandomInitializationVector();
                }
                else
                {
                    throw new CryptographicException("No initialization vector was provided for the decryption operation!");
                }
            }
            _crypto.Key = _key.Bytes;
            _crypto.IV = _iv.Bytes;
        }


        ///  <summary>
        ///  Encrypts the specified cryptData using provided key
        ///  </summary>
        public Data Encrypt(Data d, Data key)
        {
            this.Key = key;
            return Encrypt(d);
        }


        ///  <summary>
        ///  Encrypts the specified cryptData using preset key and preset initialization vector
        ///  </summary>
        public Data Encrypt(Data d)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();

            ValidateKeyAndIv(true);

            CryptoStream cs = new CryptoStream(ms, _crypto.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(d.Bytes, 0, d.Bytes.Length);
            cs.Close();
            ms.Close();

            return new Data(ms.ToArray());
        }


        ///  <summary>
        ///  Encrypts the stream to memory using provided key and provided initialization vector
        ///  </summary>
        public Data Encrypt(System.IO.Stream s, Data key, Data iv)
        {
            this.IntializationVector = iv;
            this.Key = key;
            return Encrypt(s);
        }


        ///  <summary>
        ///  Encrypts the stream to memory using specified key
        ///  </summary>
        public Data Encrypt(System.IO.Stream s, Data key)
        {
            this.Key = key;
            return Encrypt(s);
        }


        ///  <summary>
        ///  Encrypts the specified stream to memory using preset key and preset initialization vector
        ///  </summary>
        public Data Encrypt(System.IO.Stream s)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            byte[] b = new byte[_BufferSize + 1];
            int i = 0;

            ValidateKeyAndIv(true);

            CryptoStream cs = new CryptoStream(ms, _crypto.CreateEncryptor(), CryptoStreamMode.Write);
            i = s.Read(b, 0, _BufferSize);
            while (i > 0)
            {
                cs.Write(b, 0, i);
                i = s.Read(b, 0, _BufferSize);
            }

            cs.Close();
            ms.Close();

            return new Data(ms.ToArray());
        }


        ///  <summary>
        ///  Decrypts the specified cryptData using provided key and preset initialization vector
        ///  </summary>
        public Data Decrypt(Data encryptedcryptData, Data key)
        {
            this.Key = key;
            return Decrypt(encryptedcryptData);
        }


        ///  <summary>
        ///  Decrypts the specified stream using provided key and preset initialization vector
        ///  </summary>
        public Data Decrypt(System.IO.Stream encryptedStream, Data key)
        {
            this.Key = key;
            return Decrypt(encryptedStream);
        }


        ///  <summary>
        ///  Decrypts the specified stream using preset key and preset initialization vector
        ///  </summary>
        public Data Decrypt(System.IO.Stream encryptedStream)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            byte[] b = new byte[_BufferSize + 1];

            ValidateKeyAndIv(false);
            CryptoStream cs = new CryptoStream(encryptedStream, _crypto.CreateDecryptor(), CryptoStreamMode.Read);

            int i = 0;
            i = cs.Read(b, 0, _BufferSize);

            while (i > 0)
            {
                ms.Write(b, 0, i);
                i = cs.Read(b, 0, _BufferSize);
            }
            cs.Close();
            ms.Close();

            return new Data(ms.ToArray());
        }


        ///  <summary>
        ///  Decrypts the specified cryptData using preset key and preset initialization vector
        ///  </summary>
        public Data Decrypt(Data encryptedcryptData)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream(encryptedcryptData.Bytes, 0, encryptedcryptData.Bytes.Length);
            byte[] b = new byte[encryptedcryptData.Bytes.Length - 1 + 1];

            ValidateKeyAndIv(false);
            CryptoStream cs = new CryptoStream(ms, _crypto.CreateDecryptor(), CryptoStreamMode.Read);

            try
            {
                cs.Read(b, 0, encryptedcryptData.Bytes.Length - 1);
            }
            catch (CryptographicException ex)
            {
                throw new CryptographicException("Unable to decrypt cryptData. The provided key may be invalid.", ex);
            }
            finally
            {
                cs.Close();
            }
            return new Data(b);
        }


    }


    #endregion

    #region " Asymmetric"

    /// 
    /// Asymmetric encryption uses a pair of keys to encrypt and decrypt.
    /// There is a "public" key which is used to encrypt. Decrypting, on the other hand, 
    /// requires both the "public" key and an additional "private" key. The advantage is 
    /// that people can send you encrypted messages without being able to decrypt them.
    /// 
    /// 
    /// The only provider supported is the 
    /// 
    public class Asymmetric
    {

        private RSACryptoServiceProvider _rsa;
        private string _KeyContainerName = "Encryption.AsymmetricEncryption.DefaultContainerName";
        private bool _UseMachineKeystore = true;
        private int _KeySize = 1024;

        private const string _ElementParent = "RSAKeyValue";
        private const string _ElementModulus = "Modulus";
        private const string _ElementExponent = "Exponent";
        private const string _ElementPrimeP = "P";
        private const string _ElementPrimeQ = "Q";
        private const string _ElementPrimeExponentP = "DP";
        private const string _ElementPrimeExponentQ = "DQ";
        private const string _ElementCoefficient = "InverseQ";
        private const string _ElementPrivateExponent = "D";

        //-- http://forum.java.sun.com/thread.jsp?forum=9&thread=552022&tstart=0&trange=15 
        private const string _KeyModulus = "PublicKey.Modulus";
        private const string _KeyExponent = "PublicKey.Exponent";
        private const string _KeyPrimeP = "PrivateKey.P";
        private const string _KeyPrimeQ = "PrivateKey.Q";
        private const string _KeyPrimeExponentP = "PrivateKey.DP";
        private const string _KeyPrimeExponentQ = "PrivateKey.DQ";
        private const string _KeyCoefficient = "PrivateKey.InverseQ";
        private const string _KeyPrivateExponent = "PrivateKey.D";

        #region " PublicKey Class"

        /// 
        /// Represents a public encryption key. Intended to be shared, it 
        /// contains only the Modulus and Exponent.
        /// 
        public class PublicKey
        {
            public string Modulus;
            public string Exponent;

            public PublicKey()
            {
            }

            public PublicKey(string KeyXml)
            {
                LoadFromXml(KeyXml);
            }

            /// 
            /// Load public key from App.config or Web.config file
            /// 
            public void LoadFromConfig()
            {
                this.Modulus = Utils.GetConfigString(_KeyModulus);
                this.Exponent = Utils.GetConfigString(_KeyExponent);
            }

            /// 
            /// Returns *.config file XML section representing this public key
            /// 
            public string ToConfigSection()
            {
                StringBuilder sb = new StringBuilder();
                {
                    sb.Append(Utils.WriteConfigKey(_KeyModulus, this.Modulus));
                    sb.Append(Utils.WriteConfigKey(_KeyExponent, this.Exponent));
                }
                return sb.ToString();
            }

            /// 
            /// Writes the *.config file representation of this public key to a file
            /// 
            public void ExportToConfigFile(string filePath)
            {
                StreamWriter sw = new StreamWriter(filePath, false);
                sw.Write(this.ToConfigSection());
                sw.Close();
            }

            /// 
            /// Loads the public key from its XML string
            /// 
            public void LoadFromXml(string keyXml)
            {
                this.Modulus = Utils.GetXmlElement(keyXml, "Modulus");
                this.Exponent = Utils.GetXmlElement(keyXml, "Exponent");
            }

            /// 
            /// Converts this public key to an RSAParameters object
            /// 
            public RSAParameters ToParameters()
            {
                RSAParameters r = new RSAParameters();
                r.Modulus = Convert.FromBase64String(this.Modulus);
                r.Exponent = Convert.FromBase64String(this.Exponent);
                return r;
            }

            /// 
            /// Converts this public key to its XML string representation
            /// 
            public string ToXml()
            {
                StringBuilder sb = new StringBuilder();
                {
                    sb.Append(Utils.WriteXmlNode(_ElementParent));
                    sb.Append(Utils.WriteXmlElement(_ElementModulus, this.Modulus));
                    sb.Append(Utils.WriteXmlElement(_ElementExponent, this.Exponent));
                    sb.Append(Utils.WriteXmlNode(_ElementParent, true));
                }
                return sb.ToString();
            }

            /// 
            /// Writes the Xml representation of this public key to a file
            /// 
            public void ExportToXmlFile(string filePath)
            {
                StreamWriter sw = new StreamWriter(filePath, false);
                sw.Write(this.ToXml());
                sw.Close();
            }

        }
        #endregion

        #region " PrivateKey Class"

        /// 
        /// Represents a private encryption key. Not intended to be shared, as it 
        /// contains all the elements that make up the key.
        /// 
        public class PrivateKey
        {
            public string Modulus;
            public string Exponent;
            public string PrimeP;
            public string PrimeQ;
            public string PrimeExponentP;
            public string PrimeExponentQ;
            public string Coefficient;
            public string PrivateExponent;

            public PrivateKey()
            {
            }

            public PrivateKey(string keyXml)
            {
                LoadFromXml(keyXml);
            }

            /// 
            /// Load private key from App.config or Web.config file
            /// 
            public void LoadFromConfig()
            {
                this.Modulus = Utils.GetConfigString(_KeyModulus);
                this.Exponent = Utils.GetConfigString(_KeyExponent);
                this.PrimeP = Utils.GetConfigString(_KeyPrimeP);
                this.PrimeQ = Utils.GetConfigString(_KeyPrimeQ);
                this.PrimeExponentP = Utils.GetConfigString(_KeyPrimeExponentP);
                this.PrimeExponentQ = Utils.GetConfigString(_KeyPrimeExponentQ);
                this.Coefficient = Utils.GetConfigString(_KeyCoefficient);
                this.PrivateExponent = Utils.GetConfigString(_KeyPrivateExponent);
            }

            /// 
            /// Converts this private key to an RSAParameters object
            /// 
            public RSAParameters ToParameters()
            {
                RSAParameters r = new RSAParameters();
                r.Modulus = Convert.FromBase64String(this.Modulus);
                r.Exponent = Convert.FromBase64String(this.Exponent);
                r.P = Convert.FromBase64String(this.PrimeP);
                r.Q = Convert.FromBase64String(this.PrimeQ);
                r.DP = Convert.FromBase64String(this.PrimeExponentP);
                r.DQ = Convert.FromBase64String(this.PrimeExponentQ);
                r.InverseQ = Convert.FromBase64String(this.Coefficient);
                r.D = Convert.FromBase64String(this.PrivateExponent);
                return r;
            }

            /// 
            /// Returns *.config file XML section representing this private key
            /// 
            public string ToConfigSection()
            {
                StringBuilder sb = new StringBuilder();
                {
                    sb.Append(Utils.WriteConfigKey(_KeyModulus, this.Modulus));
                    sb.Append(Utils.WriteConfigKey(_KeyExponent, this.Exponent));
                    sb.Append(Utils.WriteConfigKey(_KeyPrimeP, this.PrimeP));
                    sb.Append(Utils.WriteConfigKey(_KeyPrimeQ, this.PrimeQ));
                    sb.Append(Utils.WriteConfigKey(_KeyPrimeExponentP, this.PrimeExponentP));
                    sb.Append(Utils.WriteConfigKey(_KeyPrimeExponentQ, this.PrimeExponentQ));
                    sb.Append(Utils.WriteConfigKey(_KeyCoefficient, this.Coefficient));
                    sb.Append(Utils.WriteConfigKey(_KeyPrivateExponent, this.PrivateExponent));
                }
                return sb.ToString();
            }

            /// 
            /// Writes the *.config file representation of this private key to a file
            /// 
            public void ExportToConfigFile(string strFilePath)
            {
                StreamWriter sw = new StreamWriter(strFilePath, false);
                sw.Write(this.ToConfigSection());
                sw.Close();
            }

            /// 
            /// Loads the private key from its XML string
            /// 
            public void LoadFromXml(string keyXml)
            {
                this.Modulus = Utils.GetXmlElement(keyXml, "Modulus");
                this.Exponent = Utils.GetXmlElement(keyXml, "Exponent");
                this.PrimeP = Utils.GetXmlElement(keyXml, "P");
                this.PrimeQ = Utils.GetXmlElement(keyXml, "Q");
                this.PrimeExponentP = Utils.GetXmlElement(keyXml, "DP");
                this.PrimeExponentQ = Utils.GetXmlElement(keyXml, "DQ");
                this.Coefficient = Utils.GetXmlElement(keyXml, "InverseQ");
                this.PrivateExponent = Utils.GetXmlElement(keyXml, "D");
            }

            /// 
            /// Converts this private key to its XML string representation
            /// 
            public string ToXml()
            {
                StringBuilder sb = new StringBuilder();
                {
                    sb.Append(Utils.WriteXmlNode(_ElementParent));
                    sb.Append(Utils.WriteXmlElement(_ElementModulus, this.Modulus));
                    sb.Append(Utils.WriteXmlElement(_ElementExponent, this.Exponent));
                    sb.Append(Utils.WriteXmlElement(_ElementPrimeP, this.PrimeP));
                    sb.Append(Utils.WriteXmlElement(_ElementPrimeQ, this.PrimeQ));
                    sb.Append(Utils.WriteXmlElement(_ElementPrimeExponentP, this.PrimeExponentP));
                    sb.Append(Utils.WriteXmlElement(_ElementPrimeExponentQ, this.PrimeExponentQ));
                    sb.Append(Utils.WriteXmlElement(_ElementCoefficient, this.Coefficient));
                    sb.Append(Utils.WriteXmlElement(_ElementPrivateExponent, this.PrivateExponent));
                    sb.Append(Utils.WriteXmlNode(_ElementParent, true));
                }
                return sb.ToString();
            }

            /// 
            /// Writes the Xml representation of this private key to a file
            /// 
            public void ExportToXmlFile(string filePath)
            {
                StreamWriter sw = new StreamWriter(filePath, false);
                sw.Write(this.ToXml());
                sw.Close();
            }
        }

        #endregion

        /// 
        /// Instantiates a new asymmetric encryption session using the default key size; 
        /// this is usally 1024 bits
        /// 
        public Asymmetric()
        {
            _rsa = GetRSAProvider();
        }

        /// 
        /// Instantiates a new asymmetric encryption session using a specific key size
        /// 
        public Asymmetric(int keySize)
        {
            _KeySize = keySize;
            _rsa = GetRSAProvider();
        }

        /// 
        /// Sets the name of the key container used to store this key on disk; this is an 
        /// unavoidable side effect of the underlying Microsoft CryptoAPI. 
        /// 
        /// 
        /// http://support.microsoft.com/default.aspx?scid=http://support.microsoft.com:80/support/kb/articles/q322/3/71.asp&NoWebContent=1
        /// 
        public string KeyContainerName
        {
            get { return _KeyContainerName; }
            set { _KeyContainerName = value; }
        }

        /// 
        /// Returns the current key size, in bits
        /// 
        public int KeySizeBits
        {
            get { return _rsa.KeySize; }
        }

        /// 
        /// Returns the maximum supported key size, in bits
        /// 
        public int KeySizeMaxBits
        {
            get { return _rsa.LegalKeySizes[0].MaxSize; }
        }

        /// 
        /// Returns the minimum supported key size, in bits
        /// 
        public int KeySizeMinBits
        {
            get { return _rsa.LegalKeySizes[0].MinSize; }
        }

        /// 
        /// Returns valid key step sizes, in bits
        /// 
        public int KeySizeStepBits
        {
            get { return _rsa.LegalKeySizes[0].SkipSize; }
        }

        /// 
        /// Returns the default public key as stored in the *.config file
        /// 
        public PublicKey DefaultPublicKey
        {
            get
            {
                PublicKey pubkey = new PublicKey();
                pubkey.LoadFromConfig();
                return pubkey;
            }
        }

        /// 
        /// Returns the default private key as stored in the *.config file
        /// 
        public PrivateKey DefaultPrivateKey
        {
            get
            {
                PrivateKey privkey = new PrivateKey();
                privkey.LoadFromConfig();
                return privkey;
            }
        }

        /// 
        /// Generates a new public/private key pair as objects
        /// 
        public void GenerateNewKeyset(ref PublicKey publicKey, ref PrivateKey privateKey)
        {
            string PublicKeyXML = null;
            string PrivateKeyXML = null;
            GenerateNewKeyset(ref PublicKeyXML, ref PrivateKeyXML);
            publicKey = new PublicKey(PublicKeyXML);
            privateKey = new PrivateKey(PrivateKeyXML);
        }

        /// 
        /// Generates a new public/private key pair as XML strings
        /// 
        public void GenerateNewKeyset(ref string publicKeyXML, ref string privateKeyXML)
        {
            RSA rsa = RSACryptoServiceProvider.Create();
            publicKeyXML = rsa.ToXmlString(false);
            privateKeyXML = rsa.ToXmlString(true);
        }

        /// 
        /// Encrypts data using the default public key
        /// 
        public Data Encrypt(Data d)
        {
            PublicKey PublicKey = DefaultPublicKey;
            return Encrypt(d, PublicKey);
        }

        /// 
        /// Encrypts data using the provided public key
        /// 
        public Data Encrypt(Data d, PublicKey publicKey)
        {
            _rsa.ImportParameters(publicKey.ToParameters());
            return EncryptPrivate(d);
        }

        /// 
        /// Encrypts data using the provided public key as XML
        /// 
        public Data Encrypt(Data d, string publicKeyXML)
        {
            LoadKeyXml(publicKeyXML, false);
            return EncryptPrivate(d);
        }

        private Data EncryptPrivate(Data d)
        {
            try
            {
                return new Data(_rsa.Encrypt(d.Bytes, false));
            }
            catch (CryptographicException ex)
            {
                if (ex.Message.ToLower().IndexOf("bad length") > -1)
                {
                    throw new CryptographicException("Your data is too large; RSA encryption is designed to encrypt relatively small amounts of data. The exact byte limit depends on the key size. To encrypt more data, use symmetric encryption and then encrypt that symmetric key with asymmetric RSA encryption.", ex);
                }
                else
                {
                    throw;
                }
            }
        }

        /// 
        /// Decrypts data using the default private key
        /// 
        public Data Decrypt(Data encryptedData)
        {
            PrivateKey PrivateKey = new PrivateKey();
            PrivateKey.LoadFromConfig();
            return Decrypt(encryptedData, PrivateKey);
        }

        /// 
        /// Decrypts data using the provided private key
        /// 
        public Data Decrypt(Data encryptedData, PrivateKey PrivateKey)
        {
            _rsa.ImportParameters(PrivateKey.ToParameters());
            return DecryptPrivate(encryptedData);
        }

        /// 
        /// Decrypts data using the provided private key as XML
        /// 
        public Data Decrypt(Data encryptedData, string PrivateKeyXML)
        {
            LoadKeyXml(PrivateKeyXML, true);
            return DecryptPrivate(encryptedData);
        }

        private void LoadKeyXml(string keyXml, bool isPrivate)
        {
            try
            {
                _rsa.FromXmlString(keyXml);
            }
            catch (System.Security.XmlSyntaxException ex)
            {
                string s;
                if (isPrivate)
                {
                    s = "private";
                }
                else
                {
                    s = "public";
                }
                throw new System.Security.XmlSyntaxException(string.Format("The provided {0} encryption key XML does not appear to be valid.", s), ex);
            }
        }

        private Data DecryptPrivate(Data encryptedData)
        {
            return new Data(_rsa.Decrypt(encryptedData.Bytes, false));
        }

        /// 
        /// gets the default RSA provider using the specified key size; 
        /// note that Microsoft's CryptoAPI has an underlying file system dependency that is unavoidable
        /// 
        /// 
        /// http://support.microsoft.com/default.aspx?scid=http://support.microsoft.com:80/support/kb/articles/q322/3/71.asp&NoWebContent=1
        /// 
        private RSACryptoServiceProvider GetRSAProvider()
        {
            RSACryptoServiceProvider rsa = null;
            CspParameters csp = null;
            try
            {
                csp = new CspParameters();
                csp.KeyContainerName = _KeyContainerName;
                rsa = new RSACryptoServiceProvider(_KeySize, csp);
                rsa.PersistKeyInCsp = false;
                RSACryptoServiceProvider.UseMachineKeyStore = true;
                return rsa;
            }
            catch (System.Security.Cryptography.CryptographicException ex)
            {
                if (ex.Message.ToLower().IndexOf("csp for this implementation could not be acquired") > -1)
                {
                    throw new Exception("Unable to obtain Cryptographic Service Provider. " + "Either the permissions are incorrect on the " + "'C:\\Documents and Settings\\All Users\\Application Data\\Microsoft\\Crypto\\RSA\\MachineKeys' " + "folder, or the current security context '" + System.Security.Principal.WindowsIdentity.GetCurrent().Name + "'" + " does not have access to this folder.", ex);
                }
                else
                {
                    throw;
                }
            }
            finally
            {
                if ((rsa != null))
                {
                    rsa = null;
                }
                if ((csp != null))
                {
                    csp = null;
                }
            }
        }

    }

    #endregion

    #region " Data"

    /// 
    /// represents Hex, Byte, Base64, or String data to encrypt/decrypt;
    /// use the .Text property to set/get a string representation 
    /// use the .Hex property to set/get a string-based Hexadecimal representation 
    /// use the .Base64 to set/get a string-based Base64 representation 
    /// 
    public class Data
    {
        private byte[] _b;
        private int _MaxBytes = 0;
        private int _MinBytes = 0;
        private int _StepBytes = 0;

        /// 
        /// Determines the default text encoding across ALL Data instances
        /// 
        public static System.Text.Encoding DefaultEncoding = System.Text.Encoding.GetEncoding("Windows-1252");

        /// 
        /// Determines the default text encoding for this Data instance
        /// 
        public System.Text.Encoding Encoding = DefaultEncoding;

        /// 
        /// Creates new, empty encryption data
        /// 
        public Data()
        {
        }

        /// 
        /// Creates new encryption data with the specified byte array
        /// 
        public Data(byte[] b)
        {
            _b = b;
        }

        /// 
        /// Creates new encryption data with the specified string; 
        /// will be converted to byte array using default encoding
        /// 
        public Data(string s)
        {
            this.Text = s;
        }

        /// 
        /// Creates new encryption data using the specified string and the 
        /// specified encoding to convert the string to a byte array.
        /// 
        public Data(string s, System.Text.Encoding encoding)
        {
            this.Encoding = encoding;
            this.Text = s;
        }

        /// 
        /// returns true if no data is present
        /// 
        public bool IsEmpty
        {
            get
            {
                if (_b == null)
                {
                    return true;
                }

                if (_b.Length == 0)
                {
                    return true;
                }
                return false;
            }
        }

        /// 
        /// allowed step interval, in bytes, for this data; if 0, no limit
        /// 
        public int StepBytes
        {
            get { return _StepBytes; }
            set { _StepBytes = value; }
        }

        /// 
        /// allowed step interval, in bits, for this data; if 0, no limit
        /// 
        public int StepBits
        {
            get { return _StepBytes * 8; }
            set { _StepBytes = value / 8; }
        }

        /// 
        /// minimum number of bytes allowed for this data; if 0, no limit
        /// 
        public int MinBytes
        {
            get { return _MinBytes; }
            set { _MinBytes = value; }
        }

        /// 
        /// minimum number of bits allowed for this data; if 0, no limit
        /// 
        public int MinBits
        {
            get { return _MinBytes * 8; }
            set { _MinBytes = value / 8; }
        }

        /// 
        /// maximum number of bytes allowed for this data; if 0, no limit
        /// 
        public int MaxBytes
        {
            get { return _MaxBytes; }
            set { _MaxBytes = value; }
        }

        /// 
        /// maximum number of bits allowed for this data; if 0, no limit
        /// 
        public int MaxBits
        {
            get { return _MaxBytes * 8; }
            set { _MaxBytes = value / 8; }
        }

        /// 
        /// Returns the byte representation of the data; 
        /// This will be padded to MinBytes and trimmed to MaxBytes as necessary!
        /// 
        public byte[] Bytes
        {
            get
            {
                if (_MaxBytes > 0)
                {
                    if (_b.Length > _MaxBytes)
                    {
                        byte[] b = new byte[_MaxBytes];
                        Array.Copy(_b, b, b.Length);
                        _b = b;
                    }
                }

                if (_MinBytes > 0)
                {
                    if (_b.Length < _MinBytes)
                    {
                        byte[] b = new byte[_MinBytes];
                        Array.Copy(_b, b, _b.Length);
                        _b = b;
                    }
                }
                return _b;
            }
            set { _b = value; }
        }

        /// 
        /// Sets or returns text representation of bytes using the default text encoding
        /// 
        public string Text
        {
            get
            {
                if (_b == null)
                {
                    return "";
                }
                else
                {
                    //-- need to handle nulls here; oddly, C# will happily convert
                    //-- nulls into the string whereas VB stops converting at the
                    //-- first null!
                    int i = Array.IndexOf(_b, (byte)0);
                    if (i >= 0)
                    {
                        return this.Encoding.GetString(_b, 0, i);
                    }
                    else
                    {
                        return this.Encoding.GetString(_b);
                    }
                }
            }
            set { _b = this.Encoding.GetBytes(value); }
        }

        /// 
        /// Sets or returns Hex string representation of this data
        /// 
        public string Hex
        {
            get { return Utils.ToHex(_b); }
            set { _b = Utils.FromHex(value); }
        }

        /// 
        /// Sets or returns Base64 string representation of this data
        /// 
        public string Base64
        {
            get { return Utils.ToBase64(_b); }
            set { _b = Utils.FromBase64(value); }
        }

        /// 
        /// Returns text representation of bytes using the default text encoding
        /// 
        public new string ToString()
        {
            return this.Text;
        }

        /// 
        /// returns Base64 string representation of this data
        /// 
        public string ToBase64()
        {
            return this.Base64;
        }

        /// 
        /// returns Hex string representation of this data
        /// 
        public string ToHex()
        {
            return this.Hex;
        }

    }

    #endregion

    #region " Utils "

    /// 
    /// Friend class for shared utility methods used by multiple Encryption classes
    /// 
    internal class Utils
    {
        /// 
        /// converts an array of bytes to a string Hex representation
        /// 
        static internal string ToHex(byte[] ba)
        {
            if (ba == null || ba.Length == 0)
            {
                return "";
            }
            const string HexFormat = "{0:X2}";
            StringBuilder sb = new StringBuilder();
            foreach (byte b in ba)
            {
                sb.Append(string.Format(HexFormat, b));
            }
            return sb.ToString();
        }

        /// 
        /// converts from a string Hex representation to an array of bytes
        /// 
        static internal byte[] FromHex(string hexEncoded)
        {
            if (hexEncoded == null || hexEncoded.Length == 0)
            {
                return null;
            }
            try
            {
                int l = Convert.ToInt32(hexEncoded.Length / 2);
                byte[] b = new byte[l];
                for (int i = 0; i <= l - 1; i++)
                {
                    b[i] = Convert.ToByte(hexEncoded.Substring(i * 2, 2), 16);
                }
                return b;
            }
            catch (Exception ex)
            {
                throw new System.FormatException("The provided string does not appear to be Hex encoded:" + Environment.NewLine + hexEncoded + Environment.NewLine, ex);
            }
        }

        /// 
        /// converts from a string Base64 representation to an array of bytes
        /// 
        static internal byte[] FromBase64(string base64Encoded)
        {
            if (base64Encoded == null || base64Encoded.Length == 0)
            {
                return null;
            }
            try
            {
                return Convert.FromBase64String(base64Encoded);
            }
            catch (System.FormatException ex)
            {
                throw new System.FormatException("The provided string does not appear to be Base64 encoded:" + Environment.NewLine + base64Encoded + Environment.NewLine, ex);
            }
        }

        /// 
        /// converts from an array of bytes to a string Base64 representation
        /// 
        static internal string ToBase64(byte[] b)
        {
            if (b == null || b.Length == 0)
            {
                return "";
            }
            return Convert.ToBase64String(b);
        }

        /// 
        /// retrieve an element from an XML string
        /// 
        static internal string GetXmlElement(string xml, string element)
        {
            Match m;
            m = Regex.Match(xml, "<" + element + ">(?[^>]*)");
            if (m == null)
            {
                throw new Exception("Could not find <" + element + ">");
            }
            return m.Groups["Element"].ToString();
        }

        /// 
        /// Returns the specified string value from the application .config file
        /// 
        static internal string GetConfigString(string key)
        {
            return GetConfigString(key, true);
        }

        /// 
        /// Returns the specified string value from the application .config file
        /// 
        static internal string GetConfigString(string key, bool isRequired)
        {

            string s = (string)ConfigurationManager.AppSettings.Get(key);
            if (s == null)
            {
                if (isRequired)
                {
                    throw new ConfigurationErrorsException("key <" + key + "> is missing from .config file");
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return s;
            }
        }

        static internal string WriteConfigKey(string key, string value)
        {
            string s = "" + Environment.NewLine;
            return string.Format(s, key, value);
        }

        static internal string WriteXmlElement(string element, string value)
        {
            string s = "<{0}>{1}" + Environment.NewLine;
            return string.Format(s, element, value);
        }

        static internal string WriteXmlNode(string element)
        {
            return WriteXmlNode(element, false);
        }

        static internal string WriteXmlNode(string element, bool isClosing)
        {
            string s;
            if (isClosing)
            {
                s = "" + Environment.NewLine;
            }
            else
            {
                s = "<{0}>" + Environment.NewLine;
            }
            return string.Format(s, element);
        }

    }
    #endregion

}