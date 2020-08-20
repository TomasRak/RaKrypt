using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
/* TODO
 * Chci omezovat délku znaků pro vigenerovu šifru??? (zatím omezeno na keyLength(9999))
 * Napsat návod, jaký klíč sedí do jaké šifry - nejspíš nebude nutné, stačí message boxy
 * Otestovat relokace ukládání souborů
 * 
 * MessageBoxům dát focus, když se zobrazí, aby nebyly schovány pod ostatními okny!!
 * 
 */
namespace RaKrypt_DMP
{
    public partial class Form1 : Form
    {
        private bool provedeno = false; //Status provedení druhého zavolání Form1_DragDrop při zaškrtuní testovacího módu
        private string[] files = new string[1]; //Pole cest kryptovaných souborů
        private string outPath = Properties.Settings.Default.customPath; //Uživatelem definovaná cesta
        public Form1()
        {

            if (File.Exists("RaKrypt_LOG"))
                File.Delete("RaKrypt_LOG"); 

            File.Create("RaKrypt_LOG.txt").Dispose(); //Vytvoření logovacího souboru

            InitializeComponent();
            //Definování event handlerů
            this.DragEnter += new DragEventHandler(Form1_DragEnter);
            this.DragDrop += new DragEventHandler(Form1_DragDrop);

            //Vrácení nastavení do stavu při zavření
            if (Properties.Settings.Default.encrypt)
                encryptCheckBox.Checked = true;
            else
                decryptCheckBox.Checked = true;
            cyphersComboBox.SelectedIndex = Properties.Settings.Default.encTypeIndex;
            keyTextBox.Text = Properties.Settings.Default.key;
            key2TextBox.Text = Properties.Settings.Default.key2;
            outPath = Properties.Settings.Default.customPath;
            encExtensionTextBox.Text = Properties.Settings.Default.customEncExt;
            decExtensionTextBox.Text = Properties.Settings.Default.customDecExt;
            customPathCheckBox.Checked = Properties.Settings.Default.customPathChB;

            statusLbl.Text = "Ready";
            LogText("Program started.");
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Vepsání nastavení do config souboru pro příští spuštění
            if (encryptCheckBox.Checked)
                Properties.Settings.Default.encrypt = true;
            else
                Properties.Settings.Default.encrypt = false;
            Properties.Settings.Default.encTypeIndex = cyphersComboBox.SelectedIndex;
            Properties.Settings.Default.key = keyTextBox.Text;
            Properties.Settings.Default.key2 = key2TextBox.Text;
            Properties.Settings.Default.customPath = outPath;
            Properties.Settings.Default.customEncExt = encExtensionTextBox.Text;
            Properties.Settings.Default.customDecExt = decExtensionTextBox.Text;
            Properties.Settings.Default.customPathChB = customPathCheckBox.Checked;

            Properties.Settings.Default.Save();

            LogText("Program closed.");
        }

        void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) 
                e.Effect = DragDropEffects.Copy;
        }
        void Form1_DragDrop(object sender, DragEventArgs e)
        {
            this.Focus();
            if (!provedeno)
                files = (string[])e.Data.GetData(DataFormats.FileDrop);

            statusLbl.Text = "Processing";
            string encFileName = "";

            foreach (string file in files) //Iterace šifrování pro každý dropnutý soubor
            {
                //StreamReader fileSR = new StreamReader(file);
                if (Path.GetExtension(file) == ".txt") //&& fileSR.CurrentEncoding == Encoding.UTF8) //Podmínka propouštějící pouze .txt soubory
                {
                    if (cyphersComboBox.SelectedIndex != -1) //Podmínka zjišťující, jestli je vybrána nějaká šifra
                    {
                        bool encDone = false;
                        int affineB = 0; //parametr b Afinní šifry
                        int intKlic = 0; //použito i jako parametr a pro Afinní šifru
                        string strKlic = keyTextBox.Text;
                        strKlic = RemoveDiacritism(strKlic);
                        int keyLength = 9999; //(Vigener)
                        string[] lineArray = File.ReadAllLines(file);
                        int lineArrayChars = 0;

                        Console.WriteLine("----\n" + file); //Výpis cesty k txt souboru
                        Console.WriteLine("Původní text:"); //Výpis původního textu

                        for (int i = 0; i < lineArray.Length; i++)
                        {
                            lineArray[i] = RemoveDiacritism(lineArray[i]);
                            lineArray[i] = Regex.Replace(lineArray[i], "[^a-zA-Z ]", "");
                            Console.WriteLine(lineArray[i]);
                            lineArrayChars += lineArray[i].Length;
                        }

                        //Sjednocení všech řádků do jednoho, aby bylo možné provést Vigeneruv autokláv
                        StringBuilder allLinesSB = new StringBuilder();
                        foreach (string line in lineArray)
                        {
                            allLinesSB.Append(line + " ");
                        }
                        string allLines = allLinesSB.ToString();
                        allLines = RemoveDiacritism(allLines);

                        string[] encLineArray = new string[0];

                        switch (cyphersComboBox.SelectedIndex) //Exekuce vybrané šifry
                        {
                            case 0: //Caesar - Posunutí znaků o intKlic pozic v základní abecedě
                                if (Int32.TryParse(keyTextBox.Text, out intKlic) && intKlic < 26 && intKlic > 0)
                                {
                                    encLineArray = Caesar(lineArray, intKlic);
                                    encDone = true;
                                    LogEnc("Caesar", file, intKlic.ToString());
                                }
                                else
                                    MessageBox.Show("Klíč je zadán nesprávně.\nUjistěte se, že: je ve formátu čísla v rozmezí 1-25");
                                break;

                            case 1: //Vigenèr - Posunutí znaku[i < strKlic.Length] o strKlic[i] pozic 
                                if (keyTextBox.Text.Length < keyLength && !keyTextBox.Text.Any(char.IsDigit))
                                {
                                    encLineArray = Vigener(lineArray, strKlic);
                                    encDone = true;
                                    LogEnc("Vigenèr", file, strKlic);
                                }
                                else
                                    MessageBox.Show(String.Format("Klíč je zadán nesprávně.\nUjistěte se, že: neobsahuje číslo a je kratší než {0} znaků.", keyLength));
                                break;

                            case 2: //Vigenèr autokláv - To samé co Vigenèrova šifra, ale ke klíči je připojen i výchozí text
                                if (keyTextBox.Text.Length < keyLength && !keyTextBox.Text.Any(char.IsDigit) && !keyTextBox.Text.Any(char.IsWhiteSpace))
                                {
                                    if (encryptCheckBox.Checked)
                                    {
                                        strKlic = Regex.Replace((strKlic + allLines), "[^a-zA-Z]", ""); //Vygenerování autoklíče - připsání původního textu k zadanému klíči

                                        //Zjednodušeno, všechno je sjednoceno do jednoho řádku bez mezer, aby bylo pro program možné šifrovat a dešifrovat metodou autoklávu
                                        //Vytvoření pole, do kterého je zapsán jeden prvek - aby bylo možné použít metodu Vigener, která je uzpůsobená pro práci s poli
                                        string[] line = new string[1];
                                        line[0] = Regex.Replace(allLines, "[^a-zA-Z]", "");

                                        encLineArray = Vigener(line, strKlic);
                                    }
                                    else
                                    {
                                        encLineArray = VigenerAutoKeyDecipher(lineArray, strKlic);
                                        LogEnc("Vigenèr autokey", file, strKlic);
                                    }
                                    encDone = true;
                                }
                                else
                                    MessageBox.Show(String.Format("Klíč je zadán nesprávně.\nUjistěte se, že: neobsahuje čísla, mezery a je kratší než {0} znaků.", keyLength));
                                break;

                            case 3: //Vernam - To samé, co Vigenèrova šifra, ale klíč musí být stejně dlouhý jako výchozí text
                                if (keyTextBox.Text.Length == lineArrayChars && !keyTextBox.Text.Any(char.IsDigit) && !keyTextBox.Text.Any(char.IsWhiteSpace))
                                {
                                    encLineArray = Vigener(lineArray, strKlic);
                                    encDone = true;
                                    LogEnc("Vernam", file, strKlic);
                                }
                                else
                                    MessageBox.Show(String.Format("Klíč je zadán nesprávně.\nUjistěte se, že: neobsahuje čísla, mezery a je stejně dlouhý jako vstupní text({0}.)", lineArrayChars));
                                break;

                            case 4: //Reverse - nejjednodušší transpoziční šifra - opačné zapsání znaků
                                encLineArray = Reverse(lineArray);
                                encDone = true;
                                LogEnc("Reverse", file, "");
                                break;

                            case 5: //Affine - zašifrování dané rovnicí
                                if (Int32.TryParse(keyTextBox.Text, out intKlic) && Int32.TryParse(key2TextBox.Text, out affineB) && !((intKlic % 2) == 0) && intKlic < 100 && intKlic > 0 && affineB < 100 && affineB > 0)
                                {
                                    encLineArray = Afinne(lineArray, intKlic, affineB);
                                    encDone = true;
                                    LogEnc("Affine", file, String.Format("a:{0} b:{1}", intKlic, affineB));
                                }
                                else
                                    MessageBox.Show(String.Format("Klíč/klíče jsou zadány nesprávně.\nUjistěte se, že: jsou ve formátu čísla v rozmezí 1-99 a parametr \"a\" není dělitelný dvěma!"));
                                break;
                        }

                        if (encDone) //Vytvoření zašifrovaného souboru pod podmínkou, že byla provedena nějaká šifrace.
                        {
                            if (customPathCheckBox.Checked)
                            {
                                if (outPath.Length > 3)
                                {
                                    string newPath = outPath + "\\" + Path.GetFileName(file);
                                    if (encryptCheckBox.Checked)
                                        encFileName = (newPath.Insert((newPath.Length - 4), encExtensionTextBox.Text));
                                    else
                                        encFileName = (newPath.Insert((newPath.Length - 4), decExtensionTextBox.Text));
                                }
                                else
                                {
                                    MessageBox.Show("Nebyla zadána platná cílová cesta!\nSoubor bude uložen do cesty, odkud byl vložen původní soubor.");
                                    if (encryptCheckBox.Checked)
                                        encFileName = file.Insert((file.Length - 4), encExtensionTextBox.Text);
                                    else
                                        encFileName = file.Insert((file.Length - 4), decExtensionTextBox.Text);
                                }
                            }
                            else
                            {
                                if (encryptCheckBox.Checked)
                                    encFileName = file.Insert((file.Length - 4), encExtensionTextBox.Text);
                                else
                                    encFileName = file.Insert((file.Length - 4), decExtensionTextBox.Text);
                            }


                            if (File.Exists(encFileName))
                                File.Delete(encFileName);
                            File.WriteAllLines(encFileName, encLineArray); //Vytvoření a zapsání zašifrovaného textu do souboru

                            Console.WriteLine("Zašifrovaný text (klíč:{0}):", intKlic); //Výpis zašifrovaného textu
                            foreach (string s in encLineArray)
                                Console.WriteLine(s);
                        }
                        else
                            Console.WriteLine("Nebyla provedena žádná šifrace");
                    }
                    else
                        MessageBox.Show("Není vybrán žádný typ šifrace!");
                }
                else
                    MessageBox.Show("Nebyl vložen .txt soubor!\nTento program podporuje pouze .txt soubory ve formátu UTF!");

                //Testovací mód - spustí celou metodu Form1_DragDrop ještě jednou s tím, že zašiforvaný text rovnou dešifruje - pro komfort při testování
                if (testModeCheckBox.Checked && !provedeno && encryptCheckBox.Checked)
                {
                    provedeno = true;
                    decryptCheckBox.Checked = true;

                    files[0] = encFileName;

                    object temp = new object();
                    Form1_DragDrop(temp, e);
                    encryptCheckBox.Checked = true;
                }
                else
                    provedeno = false;

                //fileSR.Close();
            }

            statusLbl.Text = "Done - Ready";
        }

        //=======================================================ŠIFRY=======================================================
        private string[] Afinne(string[] txt, int a, int b) // c = a*t + b mod(m) (m=26(délka abecedy, se kterou pracuji))
        {
            string[] encTxt = new string[txt.Length];

            for (int i = 0; i < txt.Length; i++)
            {
                char[] line = txt[i].ToUpper().ToCharArray();

                int aRev = 0;
                int obrHodA = 0;
                string encLine = "";

                if (encryptCheckBox.Checked) //šifrace ====
                {
                    for (int j = 0; j < line.Length; j++)
                    {
                        if (line[j] != ' ')
                        {
                                encLine = encLine + (char)((((a * (line[j] - 'A')) + b) % 26) + 'A');
                        }
                        else
                        {
                            encLine += line[j];
                        }
                    }
                }
                else //dešifrace ====
                {
                    for (int j = 0; j < 26; j++) //Zjišťuji obrácenou hodnotu a, pokud se rovná 1, tak zapíšu jeho pozici(1-26).
                    {
                        obrHodA = (a * j) % 26;
                        if (obrHodA == 1)
                        {
                            aRev = j;
                        }
                    }
                    for (int j = 0; j < line.Length; j++)
                    {
                        if (line[j] != ' ')
                        {
                                encLine = encLine + (char)(((aRev * ((line[j] + 'A' - b)) % 26)) + 'A');
                        }
                        else
                        {
                            encLine += line[j];
                        }
                    }
                }
                encTxt[i] = encLine.ToString();
            }
            return encTxt;
        }
        private string [] Reverse (string [] txt)
        {
            char[] charArray;

            for (int i = 0; i < txt.Length; i++)
            {
                charArray = txt[i].ToCharArray();
                Array.Reverse(charArray);
                String s = new String(charArray);
                txt[i] = s;
            }
            return txt;
        }
        private string[] VigenerAutoKeyDecipher (string [] txt, string strKlic)
        {
            string strKlicD = strKlic;

            string[] line = new string[1];
            string vystupVigenera = "";
            bool konecCyklu = false;

            for (int i = 0; i < txt.Length; i++)
            {
                line[0] = txt[i];

                for (int j = 1; !konecCyklu; j++)
                {
                    vystupVigenera = Vigener(line, strKlic)[0];

                    if (j <= txt[i].Length / strKlicD.Length)
                    {
                        vystupVigenera = vystupVigenera.Substring(0, strKlicD.Length * j);
                        strKlic = strKlicD + vystupVigenera.Substring(0, strKlicD.Length * j);
                    }
                    else
                    {
                        vystupVigenera = vystupVigenera.Substring(0);
                        strKlic = strKlicD + vystupVigenera.Substring(0);

                        konecCyklu = true;
                    }
                }
                konecCyklu = false;
                txt[i] = vystupVigenera;  
            }
            return txt;
        }
        private string[] Vigener (string[] txt, string strKlic) //Vigenerova šifra
        {
            int klic = 0;
            int klicIndex = 0;
            string[] txtEncFin = new string[txt.Length];
            for (int i = 0; i < txt.Length; i++) //i = index řádku
            {
                StringBuilder txtEnc = new StringBuilder(txt[i]); //řádek
                for (int j = 0; j < txt[i].Length; j++) //j = index charu v řádku
                {
                    if (klicIndex < strKlic.Length)
                    {
                        klic = char.ToUpper(strKlic[klicIndex]) - 64;
                        klicIndex++;
                    }
                    else
                    {
                        klicIndex = 0;
                        klic = char.ToUpper(strKlic[klicIndex]) - 64;
                        klicIndex++;
                    }

                    if (klic == 0)
                        txtEnc[j] = ' ';
                    else
                        if (encryptCheckBox.Checked)
                            txtEnc[j] = CaesarCharEnc(txtEnc[j], klic);
                        else
                            txtEnc[j] = CaesarCharEnc(txtEnc[j], 26 - klic);
                }
                txtEncFin[i] = txtEnc.ToString();
            }
            return txtEncFin;
        }
        private string[] Caesar(string[] txt, int klic)  //Caesarova šifra
        {
            if (!encryptCheckBox.Checked)
                klic = 26 - klic;

            for (int i = 0; i < txt.Length; i++)
                txt[i] = CaesarEnc(txt[i], klic);

            return txt;
        }
        private string CaesarEnc(string forTxt, int klic)  //Caesarova enkrypce
        {  
            StringBuilder encTxt = new StringBuilder(forTxt);
            for (int i = 0; i < forTxt.Length; i++)
            {
                encTxt[i] = CaesarCharEnc(forTxt[i], klic);
            }
            return encTxt.ToString();
        }
        private char CaesarCharEnc(char ch, int k) //Caesarova enkrypce (po jednotlivých charech)
        {
            char d = char.IsUpper(ch) ? 'A' : 'a';
            if (char.IsWhiteSpace(ch))
                ch = ' '; //Tento char bude vrácen, je-li vstupní char whitespace
            else
                ch = (char)((((ch + k) - d) % 26) + d); //Zapsání posunutí znaků o k míst
            return ch;
        }
        public string RemoveDiacritism(string text)
        {
            string stringFormD = text.Normalize(System.Text.NormalizationForm.FormD);
            StringBuilder retVal = new StringBuilder();
            for (int index = 0; index < stringFormD.Length; index++)
            {
                if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(stringFormD[index]) != System.Globalization.UnicodeCategory.NonSpacingMark)
                    retVal.Append(stringFormD[index]);
            }
            return retVal.ToString().Normalize(System.Text.NormalizationForm.FormC);
        }
        public void LogText(string text) //Logovací metoda pro text
        {
            DateTime time = DateTime.Now;
            string[] lineArray = File.ReadAllLines("RaKrypt_LOG.txt");
            List<string> lineList = lineArray.ToList();
            lineList.Add((lineList.Count) + ".|" + time + "|=" + text);

            File.WriteAllLines("RaKrypt_LOG.txt", lineList);
        }
        public void LogEnc(string cipherType, string file, string key) //Logovací metoda pro enkrypce
        {
            DateTime time = DateTime.Now;
            string[] lineArray = File.ReadAllLines("RaKrypt_LOG.txt");
            List<string> lineList = lineArray.ToList();
            if (encryptCheckBox.Checked)
            {
                lineList.Add((lineList.Count) + ".|" + time + "|=" + String.Format("Encrypted {0} with {1} cipher (key {2})", file, cipherType, key));
            }
            else
                lineList.Add((lineList.Count) + ".|" + time + "|=" + String.Format("Decrypted {0} with {1} cipher (key {2})", file, cipherType, key));

            File.WriteAllLines("RaKrypt_LOG.txt", lineList);
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }
        private void keyTextBox_TextChanged(object sender, EventArgs e)
        {
            keyLengthLabel.Text = keyTextBox.Text.Length.ToString();
        }
        private void cyphersComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cyphersComboBox.SelectedIndex) //Změna prostředí podle vybrané šifry
            {
                case 0: //Caesar - Posunutí znaků o intKlic pozic v základní abecedě
                    keyLbl.Text = "Key:";
                    Hide2ndKey();
                    keyLengthLabel.Hide();
                    break;
                case 1: //Vigenèr - Posunutí znaku[i < strKlic.Length] o strKlic[i] pozic 
                    keyLbl.Text = "Key:";
                    Hide2ndKey();
                    keyLengthLabel.Hide();
                    break;
                case 2: //Vigenèr autokláv - To samé co Vigenèrova šifra, ale ke klíči je připojen i výchozí text
                    keyLbl.Text = "Key:";
                    Hide2ndKey();
                    keyLengthLabel.Hide();
                    break;
                case 3: //Vernam - To samé, co Vigenèrova šifra, ale klíč musí být stejně dlouhý jako výchozí text
                    keyLbl.Text = "Key:";
                    Hide2ndKey();
                    keyLengthLabel.Show();
                    break;
                case 4: //Reverse - nejjednodušší transpoziční šifra - opačné zapsání znaků
                    keyLbl.Text = "Key:";
                    Hide2ndKey();
                    keyLengthLabel.Hide();
                    break;
                case 5: //Affine - zašifrování dané rovnicí
                    keyLbl.Text = "a:";
                    key2Lbl.Show();
                    key2TextBox.Show();
                    keyLengthLabel.Hide();
                    break;
            }
        }
        private void Hide2ndKey()
        {
            key2Lbl.Hide();
            key2TextBox.Hide();
        }

        private void zmenCestuBtn_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();

            if (folderBrowserDialog1.SelectedPath.Length <= 1)
                MessageBox.Show("Nevybrali jste validní cestu, opakujte prosím akci.");

            outPath = folderBrowserDialog1.SelectedPath.ToString();

            if (customPathCheckBox.Checked)
                cestaLbl.Text = outPath.ToString();
        }
        private void customPathCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (customPathCheckBox.Checked)
                cestaLbl.Text = outPath.ToString();
            else
                cestaLbl.Text = "Tam, odkud byly soubory vloženy.";
        }
    }
}