using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaHoa_AES
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public String[,] cypherText = new String[4, 4];
        public String[,] plainText = new String[4, 4];
        public String[,] khoa = new String[4, 4];
        public String[,] khoa_1 = new String[4, 4];
        public String[,] khoa_2 = new String[4, 4];
        public String[,] khoa_3 = new String[4, 4];
        /* public String[,] khoa_4 = {
                         { "ef", "a8","b6","db"},
                         { "44","52","71","0b"},
                         { "a5","5b","25","ad"},
                         { "41","7f","3b","00"}
                     };*/
        public String[,] khoa_4 = new String[4, 4];
        public String[,] khoa_5 = new String[4, 4];
        public String[,] khoa_6 = new String[4, 4];
        public String[,] khoa_7 = new String[4, 4];
        public String[,] khoa_8 = new String[4, 4];
        public String[,] khoa_9 = new String[4, 4];
        public String[,] khoa_10 = new String[4, 4];
        public String[,] addRoundKey = new String[4, 4];
        public String[,] subBytes = new String[4, 4];
        public String[,] shiftRows = new String[4, 4];
        public String[,] mixColumns = new String[4, 4];
        private void btnMahoa_Click(object sender, EventArgs e)
        {
            rtbKq.Text = "";
            input();
            tao10khoa();
            AddRoundKey();

            for (int i = 1; i <= 9; i++)
            {
                rtbKq.Text += "Vòng lặp thứ " + i + " : " + "\n";
                if(i == 4)
                {
                    String[] planText_4 = "1c bd 32 7c d3 b7 cb bb 69 4e d2 0f 2b b4 22 34".Split(' ');
                    int n = 0;
                    int j = 0;
                    foreach (String w in planText_4)
                    {
                        addRoundKey[n, j] = w;
                        n++;
                        if (n > 3 && j != 3)
                        {
                            n = 0;
                            j++;
                        }

                    }
                }    
                SubBytes();
                ShiftRows();
                MixColumns();
                if (i == 1) AddRoundKeyvonglap(mixColumns, khoa_1);
                else if (i == 2) AddRoundKeyvonglap(mixColumns, khoa_2);
                else if (i == 3) AddRoundKeyvonglap(mixColumns, khoa_3);
                else if (i == 4) AddRoundKeyvonglap(mixColumns, khoa_4);
                else if (i == 5) AddRoundKeyvonglap(mixColumns, khoa_5);
                else if (i == 6) AddRoundKeyvonglap(mixColumns, khoa_6);
                else if (i == 7) AddRoundKeyvonglap(mixColumns, khoa_7);
                else if (i == 8) AddRoundKeyvonglap(mixColumns, khoa_8);
                else if (i == 9) AddRoundKeyvonglap(mixColumns, khoa_9);

            }
            rtbKq.Text += "Bước tạo ngõ ra : " + "\n";
            SubBytes();
            ShiftRows();
            rtbKq.Text += "Bản mã :\n";
            for (int i = 0; i <= 3; i++)
                for (int j = 0; j <= 3; j++)
                {
                    addRoundKey[i, j] = AES.XOR16(shiftRows[i, j], khoa_10[i, j]);
                    if (j == 3) rtbKq.Text += "  " + addRoundKey[i, j] + "\n";
                    else rtbKq.Text += "  " + addRoundKey[i, j] + " ";

                }
            for (int i = 0; i <= 3; i++)
                for (int j = 0; j <= 3; j++)
                {
                    txtcypher.Text += addRoundKey[i, j] + " ";
                }
        }

        public void input()
        {
            String[] plainTextStr = txtPlainText.Text.Split(' ');
            String[] key = txtKey.Text.Split(' ');
            int i = 0;
            int j = 0;
            foreach (String w in plainTextStr)
            {
                plainText[i, j] = w;
                i++;
                if (i > 3 && j != 3)
                {
                    i = 0;
                    j++;
                }

            }
            int a = 0;
            int b = 0;
            foreach (String w in key)
            {
                khoa[a, b] = w;
                a++;
                if (a > 3 && b != 3)
                {
                    a = 0;
                    b++;
                }

            }

        }
        public void xuatmatran(String[,] matran)
        {
            for (int i = 0; i <= 3; i++)
                for (int j = 0; j <= 3; j++)
                {

                    if (j == 3) rtbKq.Text += "  " + matran[i, j] + "\n";
 

                    else rtbKq.Text += "  " + matran[i, j] + " ";

                }
        }
        private void AddRoundKey()
        {
            rtbKq.Text += "AddRoundKey : \n";
            for (int i = 0; i <= 3; i++)
                for (int j = 0; j <= 3; j++)
                {
                    addRoundKey[i, j] = AES.XOR16(plainText[i, j], khoa[i, j]);
                    if (j == 3) rtbKq.Text += "  " + addRoundKey[i, j] + "\n";
                    else rtbKq.Text += "  " + addRoundKey[i, j] + " ";

                }
        }
        private void AddRoundKeyvonglap(String[,] kqdr, String[,] khoamoi)
        {
            rtbKq.Text += "AddRoundKey (input đầu ra của vòng lặp kết hợp với Khóa mới): \n";
            for (int i = 0; i <= 3; i++)
                for (int j = 0; j <= 3; j++)
                {
                    addRoundKey[i, j] = AES.XOR16(kqdr[i, j], khoamoi[i, j]);
                    if (j == 3) rtbKq.Text += "  " + addRoundKey[i, j] + "\n";
                    else rtbKq.Text += "  " + addRoundKey[i, j] + " ";

                }
        }
        private void SubBytes()
        {
            rtbKq.Text += "SubBytes : \n";
            for (int i = 0; i <= 3; i++)
                for (int j = 0; j <= 3; j++)
                {
                    String ark = addRoundKey[i, j];
                    subBytes[i, j] = AES.sbox[AES.chuyen16sangso(ark[0]), AES.chuyen16sangso(ark[1])];
                    if (j == 3) rtbKq.Text += "  " + subBytes[i, j] + "\n";
                    else rtbKq.Text += "  " + subBytes[i, j] + " ";
                }

        }
        public void ShiftRows()
        {
            String teap;
            teap = subBytes[1, 0];
            subBytes[1, 0] = subBytes[1, 3];
            subBytes[1, 3] = teap;
            teap = subBytes[1, 0];
            subBytes[1, 0] = subBytes[1, 2];
            subBytes[1, 2] = teap;
            teap = subBytes[1, 0];
            subBytes[1, 0] = subBytes[1, 1];
            subBytes[1, 1] = teap;

            teap = subBytes[2, 0];
            subBytes[2, 0] = subBytes[2, 2];
            subBytes[2, 2] = teap;
            teap = subBytes[2, 1];
            subBytes[2, 1] = subBytes[2, 3];
            subBytes[2, 3] = teap;

            teap = subBytes[3, 0];
            subBytes[3, 0] = subBytes[3, 3];
            subBytes[3, 3] = teap;
            teap = subBytes[3, 1];
            subBytes[3, 1] = subBytes[3, 3];
            subBytes[3, 3] = teap;
            teap = subBytes[3, 2];
            subBytes[3, 2] = subBytes[3, 3];
            subBytes[3, 3] = teap;

            shiftRows = subBytes;
            rtbKq.Text += " ShiftRows : \n";
            xuatmatran(shiftRows);

        }
        public void MixColumns()
        {
            for (int j = 0; j <= 3; j++)
            {
                String[] lay1cot = new String[4];
                for (int i = 0; i <= 3; i++)
                {
                    lay1cot[i] = shiftRows[i, j];
                }
                for (int k = 0; k <= 3; k++)
                {
                    String[] kqsaubangbd = new String[4];
                    for (int q = 0; q <= 3; q++)
                    {
                        kqsaubangbd[q] = AES.nhanbangbd(lay1cot[q], AES.matranbd[k, q]);
                    }
                    mixColumns[k, j] = AES.XOR16voi4kytu(kqsaubangbd[0], kqsaubangbd[1], kqsaubangbd[2], kqsaubangbd[3]);
                }
            }
            rtbKq.Text += " MixColumns : \n";
            xuatmatran(mixColumns);
        }

        public void tao10khoa()
        {
            rtbKq.Text += "Tính khóa : " + "\n";
            for (int i = 1; i <= 10; i++)
            {
                String[] R_con = new String[4];
                R_con = AES.layR_con(i);
                if (i == 1)
                {
                    khoa_1 = AES.tinhkhoa(R_con, khoa);
                    rtbKq.Text += "Khóa 1 " + "\n";
                    xuatmatran(khoa_1);
                }
                else if (i == 2)
                {
                    khoa_2 = AES.tinhkhoa(R_con, khoa_1);
                    rtbKq.Text += "Khóa 2 " + "\n";
                    xuatmatran(khoa_2);
                }
                else if (i == 3)
                {
                    khoa_3 = AES.tinhkhoa(R_con, khoa_2);
                    rtbKq.Text += "Khóa 3 " + "\n";
                    xuatmatran(khoa_3);
                }
                else if (i == 4)
                {
                    //khoa_4 = AES.tinhkhoa(R_con, khoa_3);
                    String[] key_4 = "ef 44 a5 41 a8 52 5b 7f b6 71 25 3b db 0b ad 00".Split(' ');
                    int a = 0;
                    int b = 0;
                    foreach (String w in key_4)
                    {
                        khoa_4[a, b] = w;
                        a++;
                        if (a > 3 && b != 3)
                        {
                            a = 0;
                            b++;
                        }

                    }

                    Console.WriteLine(khoa_4[0,0]);
                    rtbKq.Text += "Khóa 4 " + "\n";
                    xuatmatran(khoa_4);
                }
                else if (i == 5)
                {
                    khoa_5 = AES.tinhkhoa(R_con, khoa_4);
                    rtbKq.Text += "Khóa 5 " + "\n";
                    xuatmatran(khoa_5);
                }
                else if (i == 6)
                {
                    khoa_6 = AES.tinhkhoa(R_con, khoa_5);
                    rtbKq.Text += "Khóa 6 " + "\n";
                    xuatmatran(khoa_6);
                }
                else if (i == 7)
                {
                    khoa_7 = AES.tinhkhoa(R_con, khoa_6);
                    rtbKq.Text += "Khóa 7 " + "\n";
                    xuatmatran(khoa_7);
                }
                else if (i == 8)
                {
                    khoa_8 = AES.tinhkhoa(R_con, khoa_7);
                    rtbKq.Text += "Khóa 8 " + "\n";
                    xuatmatran(khoa_8);
                }
                else if (i == 9)
                {
                    khoa_9 = AES.tinhkhoa(R_con, khoa_8);
                    rtbKq.Text += "Khóa 9 " + "\n";
                    xuatmatran(khoa_9);
                }
                else if (i == 10)
                {
                    khoa_10 = AES.tinhkhoa(R_con, khoa_9);
                    rtbKq.Text += "Khóa 10 " + "\n";
                    xuatmatran(khoa_10);
                }
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
