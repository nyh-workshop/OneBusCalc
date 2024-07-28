using System.ComponentModel;
using WinForms = System.Windows.Forms;

namespace OneBusCalc
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void calcPRG()
        {
            // Calculate PRG!
            int R4100 = Convert.ToInt32(numericUpDown1.Value);
            int R4105 = Convert.ToInt32(numericUpDown2.Value);
            int R4106 = Convert.ToInt32(numericUpDown3.Value);
            int R4107 = Convert.ToInt32(numericUpDown4.Value);
            int R4108 = Convert.ToInt32(numericUpDown5.Value);
            int R4109 = Convert.ToInt32(numericUpDown6.Value);
            int R410A = Convert.ToInt32(numericUpDown7.Value);
            int R410B = Convert.ToInt32(numericUpDown8.Value);

            int PhysicalAddrPRG_Bank0 = 0;
            int PhysicalAddrPRG_Bank1 = 0;
            int PhysicalAddrPRG_Bank2 = 0;
            int PhysicalAddrPRG_Bank3 = 0;

            int R00 = 0;
            int R01 = 0;

            if (R410B < 0x06)
            {
                switch (R410B & 0x07)
                {
                    case 0x00:
                        R00 = 0xC0;
                        R01 = 0x3F;
                        break;
                    case 0x01:
                        R00 = 0xE0;
                        R01 = 0x1F;
                        break;
                    case 0x02:
                        R00 = 0xF0;
                        R01 = 0x0F;
                        break;
                    case 0x03:
                        R00 = 0xF8;
                        R01 = 0x07;
                        break;
                    case 0x04:
                        R00 = 0xFC;
                        R01 = 0x03;
                        break;
                    case 0x05:
                        R00 = 0xFE;
                        R01 = 0x01;
                        break;
                    default:
                        R00 = 0xC0;
                        R01 = 0x3F;
                        break;
                }
                if ((R4105 & 0x40) == 0)
                {
                    PhysicalAddrPRG_Bank0 = ((R4100 & 0xF0) << 17) + (((R410A & R00) | (R4107 & R01)) << 13);
                    PhysicalAddrPRG_Bank1 = ((R4100 & 0xF0) << 17) + (((R410A & R00) | (R4108 & R01)) << 13);
                    PhysicalAddrPRG_Bank2 = ((R4100 & 0xF0) << 17) + (((R410A & R00) | (0xFE & R01)) << 13);
                    PhysicalAddrPRG_Bank3 = ((R4100 & 0xF0) << 17) + (((R410A & R00) | (0xFF & R01)) << 13);
                }
                else
                {
                    PhysicalAddrPRG_Bank0 = ((R4100 & 0xF0) << 17) + (((R410A & R00) | (0xFE & R01)) << 13);
                    PhysicalAddrPRG_Bank1 = ((R4100 & 0xF0) << 17) + (((R410A & R00) | (R4108 & R01)) << 13);
                    PhysicalAddrPRG_Bank2 = ((R4100 & 0xF0) << 17) + (((R410A & R00) | (R4109 & R01)) << 13);
                    PhysicalAddrPRG_Bank3 = ((R4100 & 0xF0) << 17) + (((R410A & R00) | (0xFF & R01)) << 13);
                }
            }
            else
            {
                if (R410B == 0x06)
                {
                    PhysicalAddrPRG_Bank0 = ((R4100 & 0xF0) << 17) + (R410A << 13);
                }
                else if (R410B == 0x07)
                {
                    if ((R4105 & 0x40) == 0)
                    {
                        PhysicalAddrPRG_Bank0 = ((R4100 & 0xF0) << 17) + (R4107 << 13);
                        PhysicalAddrPRG_Bank1 = ((R4100 & 0xF0) << 17) + (R4108 << 13);
                        PhysicalAddrPRG_Bank2 = ((R4100 & 0xF0) << 17) + (0xFE << 13);
                        PhysicalAddrPRG_Bank3 = ((R4100 & 0xF0) << 17) + (0xFF << 13);
                    }
                    else
                    {
                        PhysicalAddrPRG_Bank0 = ((R4100 & 0xF0) << 17) + (0xFE << 13);
                        PhysicalAddrPRG_Bank1 = ((R4100 & 0xF0) << 17) + (R4108 << 13);
                        PhysicalAddrPRG_Bank2 = ((R4100 & 0xF0) << 17) + (R4109 << 13);
                        PhysicalAddrPRG_Bank3 = ((R4100 & 0xF0) << 17) + (0xFF << 13);
                    }
                }
                else
                {
                    PhysicalAddrPRG_Bank0 = 0;
                    PhysicalAddrPRG_Bank1 = 0;
                    PhysicalAddrPRG_Bank2 = 0;
                    PhysicalAddrPRG_Bank3 = 0;
                }
            }
            listBox1.Items[0] = "Bank0 = " + PhysicalAddrPRG_Bank0.ToString("X8");
            listBox1.Items[1] = "Bank1 = " + PhysicalAddrPRG_Bank1.ToString("X8");
            listBox1.Items[2] = "Bank2 = " + PhysicalAddrPRG_Bank2.ToString("X8");
            listBox1.Items[3] = "Bank3 = " + PhysicalAddrPRG_Bank3.ToString("X8");

            MyGlobalVariable.PRG_swap = R4105 & 0x40;
            MyGlobalVariable.HorzVertSelection = R4106 & 0x01;
            textBox21.Text = checkHorzVertSelection();
            textBox22.Text = checkPRGswap();
        }

        public void calcCHR()
        {
            // Video Address Normal Mode:
            int R4100 = Convert.ToInt32(numericUpDown1.Value);
            int R2012 = Convert.ToInt32(numericUpDown9.Value);
            int R2013 = Convert.ToInt32(numericUpDown10.Value);
            int R2014 = Convert.ToInt32(numericUpDown11.Value);
            int R2015 = Convert.ToInt32(numericUpDown12.Value);
            int R2016 = Convert.ToInt32(numericUpDown13.Value);
            int R2017 = Convert.ToInt32(numericUpDown14.Value);
            int R2018 = Convert.ToInt32(numericUpDown15.Value);
            int R201A = Convert.ToInt32(numericUpDown16.Value);

            int R00 = 0x00;
            int R01 = 0x00;

            int PhysicalAddrCHR_Bank0 = 0;
            int PhysicalAddrCHR_Bank1 = 0;
            int PhysicalAddrCHR_Bank2 = 0;
            int PhysicalAddrCHR_Bank3 = 0;
            int PhysicalAddrCHR_Bank4 = 0;
            int PhysicalAddrCHR_Bank5 = 0;

            if (R201A == 0)
            {
                PhysicalAddrCHR_Bank0 = ((R4100 & 0x0F) << 21) + ((R2018 & 0x70) << 14) + ((R2016 & 0xFE) << 10);
                PhysicalAddrCHR_Bank1 = ((R4100 & 0x0F) << 21) + ((R2018 & 0x70) << 14) + ((R2017 & 0xFE) << 10);
                PhysicalAddrCHR_Bank2 = ((R4100 & 0x0F) << 21) + ((R2018 & 0x70) << 14) + ((R2012) << 10);
                PhysicalAddrCHR_Bank3 = ((R4100 & 0x0F) << 21) + ((R2018 & 0x70) << 14) + ((R2013) << 10);
                PhysicalAddrCHR_Bank4 = ((R4100 & 0x0F) << 21) + ((R2018 & 0x70) << 14) + ((R2014) << 10);
                PhysicalAddrCHR_Bank5 = ((R4100 & 0x0F) << 21) + ((R2018 & 0x70) << 14) + ((R2015) << 10);
            }
            else
            {
                switch (R201A & 0x07)
                {
                    case 0x01:
                        R00 = 0x80;
                        R01 = 0x7F;
                        break;
                    case 0x02:
                        R00 = 0xC0;
                        R01 = 0x3F;
                        break;
                    case 0x04:
                        R00 = 0xE0;
                        R01 = 0x1F;
                        break;
                    case 0x05:
                        R00 = 0xF0;
                        R01 = 0x0F;
                        break;
                    case 0x06:
                        R00 = 0xF8;
                        R01 = 0x07;
                        break;
                    default:
                        // Back to case 0x01!
                        R00 = 0x80;
                        R01 = 0x7F;
                        break;
                }

                PhysicalAddrCHR_Bank0 = ((R4100 & 0x0F) << 21) + ((R2018 & 0x70) << 14) + (((R201A & R00) | ((R2016 & 0xFE) & R01)) << 10);
                PhysicalAddrCHR_Bank1 = ((R4100 & 0x0F) << 21) + ((R2018 & 0x70) << 14) + (((R201A & R00) | ((R2017 & 0xFE) & R01)) << 10);
                PhysicalAddrCHR_Bank2 = ((R4100 & 0x0F) << 21) + ((R2018 & 0x70) << 14) + (((R201A & R00) | ((R2012) & R01)) << 10);
                PhysicalAddrCHR_Bank3 = ((R4100 & 0x0F) << 21) + ((R2018 & 0x70) << 14) + (((R201A & R00) | ((R2013) & R01)) << 10);
                PhysicalAddrCHR_Bank4 = ((R4100 & 0x0F) << 21) + ((R2018 & 0x70) << 14) + (((R201A & R00) | ((R2014) & R01)) << 10);
                PhysicalAddrCHR_Bank5 = ((R4100 & 0x0F) << 21) + ((R2018 & 0x70) << 14) + (((R201A & R00) | ((R2015) & R01)) << 10);
            }

            listBox2.Items[0] = "Bank0 = " + PhysicalAddrCHR_Bank0.ToString("X8");
            listBox2.Items[1] = "Bank1 = " + PhysicalAddrCHR_Bank1.ToString("X8");
            listBox2.Items[2] = "Bank2 = " + PhysicalAddrCHR_Bank2.ToString("X8");
            listBox2.Items[3] = "Bank3 = " + PhysicalAddrCHR_Bank3.ToString("X8");
            listBox2.Items[4] = "Bank4 = " + PhysicalAddrCHR_Bank4.ToString("X8");
            listBox2.Items[5] = "Bank5 = " + PhysicalAddrCHR_Bank5.ToString("X8");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // PRG input boxes:
            numericUpDown1.ValueChanged += new EventHandler(numericUpDown1_ValueChanged);
            numericUpDown2.ValueChanged += new EventHandler(numericUpDown2_ValueChanged);
            numericUpDown3.ValueChanged += new EventHandler(numericUpDown3_ValueChanged);
            numericUpDown4.ValueChanged += new EventHandler(numericUpDown4_ValueChanged);
            numericUpDown5.ValueChanged += new EventHandler(numericUpDown5_ValueChanged);
            numericUpDown6.ValueChanged += new EventHandler(numericUpDown6_ValueChanged);
            numericUpDown7.ValueChanged += new EventHandler(numericUpDown7_ValueChanged);
            numericUpDown8.ValueChanged += new EventHandler(numericUpDown8_ValueChanged);

            // CHR input boxes:
            numericUpDown9.ValueChanged += new EventHandler(numericUpDown9_ValueChanged);
            numericUpDown10.ValueChanged += new EventHandler(numericUpDown10_ValueChanged);
            numericUpDown11.ValueChanged += new EventHandler(numericUpDown11_ValueChanged);
            numericUpDown12.ValueChanged += new EventHandler(numericUpDown12_ValueChanged);
            numericUpDown13.ValueChanged += new EventHandler(numericUpDown13_ValueChanged);
            numericUpDown14.ValueChanged += new EventHandler(numericUpDown14_ValueChanged);
            numericUpDown15.ValueChanged += new EventHandler(numericUpDown15_ValueChanged);
            numericUpDown16.ValueChanged += new EventHandler(numericUpDown16_ValueChanged);


            listBox1.Items.Add("Bank0 = " + "00000000");
            listBox1.Items.Add("Bank1 = " + "00000000");
            listBox1.Items.Add("Bank2 = " + "00000000");
            listBox1.Items.Add("Bank3 = " + "00000000");

            listBox2.Items.Add("Bank0 = " + "00000000");
            listBox2.Items.Add("Bank1 = " + "00000000");
            listBox2.Items.Add("Bank2 = " + "00000000");
            listBox2.Items.Add("Bank3 = " + "00000000");
            listBox2.Items.Add("Bank4 = " + "00000000");
            listBox2.Items.Add("Bank5 = " + "00000000");

            textBox21.Text = checkHorzVertSelection();
            textBox22.Text = checkPRGswap();

            richTextBox1.Clear();
        }

        private string checkHorzVertSelection()
        {
            if (MyGlobalVariable.HorzVertSelection == 0)
            {
                return "$4106.0 = 0 [Vert. Arr. \"Horz Mirr.\")]";
            }
            else
            {
                return "$4106.0 = 1 [Horz. Arr. \"Vert Mirr.\")]";
            }
        }

        private string checkPRGswap()
        {
            if (MyGlobalVariable.PRG_swap == 0)
            {
                return "$4105.6 = 0 PRG No Swap";
            }
            else
            {
                return "$4105.6 = 1 PRG Swap";
            }
        }

        private void numericUpDown1_ValueChanged(Object? sender, EventArgs e)
        {
            calcPRG();
        }

        private void numericUpDown2_ValueChanged(Object? sender, EventArgs e)
        {
            calcPRG();
        }
        private void numericUpDown3_ValueChanged(Object? sender, EventArgs e)
        {
            calcPRG();
        }
        private void numericUpDown4_ValueChanged(Object? sender, EventArgs e)
        {
            calcPRG();
        }
        private void numericUpDown5_ValueChanged(Object? sender, EventArgs e)
        {
            calcPRG();
        }
        private void numericUpDown6_ValueChanged(Object? sender, EventArgs e)
        {
            calcPRG();
        }
        private void numericUpDown7_ValueChanged(Object? sender, EventArgs e)
        {
            calcPRG();
        }
        private void numericUpDown8_ValueChanged(Object? sender, EventArgs e)
        {
            calcPRG();
        }

        private void numericUpDown9_ValueChanged(Object? sender, EventArgs e)
        {
            calcCHR();
        }

        private void numericUpDown10_ValueChanged(Object? sender, EventArgs e)
        {
            calcCHR();
        }
        private void numericUpDown11_ValueChanged(Object? sender, EventArgs e)
        {
            calcCHR();
        }
        private void numericUpDown12_ValueChanged(Object? sender, EventArgs e)
        {
            calcCHR();
        }
        private void numericUpDown13_ValueChanged(Object? sender, EventArgs e)
        {
            calcCHR();
        }
        private void numericUpDown14_ValueChanged(Object? sender, EventArgs e)
        {
            calcCHR();
        }
        private void numericUpDown15_ValueChanged(Object? sender, EventArgs e)
        {
            calcCHR();
        }
        private void numericUpDown16_ValueChanged(Object? sender, EventArgs e)
        {
            calcCHR();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            richTextBox1.Text = "[0x" + Convert.ToInt32(numericUpDown9.Value).ToString("X2");
            richTextBox1.Text += ", ";
            richTextBox1.Text += "0x" + Convert.ToInt32(numericUpDown10.Value).ToString("X2");
            richTextBox1.Text += ", ";
            richTextBox1.Text += "0x" + Convert.ToInt32(numericUpDown11.Value).ToString("X2");
            richTextBox1.Text += ", ";
            richTextBox1.Text += "0x" + Convert.ToInt32(numericUpDown12.Value).ToString("X2");
            richTextBox1.Text += ", ";
            richTextBox1.Text += "0x" + Convert.ToInt32(numericUpDown13.Value).ToString("X2");
            richTextBox1.Text += ", ";
            richTextBox1.Text += "0x" + Convert.ToInt32(numericUpDown14.Value).ToString("X2");
            richTextBox1.Text += ", ";
            richTextBox1.Text += "0x" + Convert.ToInt32(numericUpDown15.Value).ToString("X2");
            richTextBox1.Text += ", ";
            richTextBox1.Text += "0x" + Convert.ToInt32(numericUpDown16.Value).ToString("X2");
            richTextBox1.Text += ", ";

            richTextBox1.Text += "0x" + Convert.ToInt32(numericUpDown1.Value).ToString("X2");
            richTextBox1.Text += ", ";
            richTextBox1.Text += "0x" + Convert.ToInt32(numericUpDown2.Value).ToString("X2");
            richTextBox1.Text += ", ";
            richTextBox1.Text += "0x" + Convert.ToInt32(numericUpDown3.Value).ToString("X2");
            richTextBox1.Text += ", ";
            richTextBox1.Text += "0x" + Convert.ToInt32(numericUpDown4.Value).ToString("X2");
            richTextBox1.Text += ", ";
            richTextBox1.Text += "0x" + Convert.ToInt32(numericUpDown5.Value).ToString("X2");
            richTextBox1.Text += ", ";
            richTextBox1.Text += "0x" + Convert.ToInt32(numericUpDown6.Value).ToString("X2");
            richTextBox1.Text += ", ";
            richTextBox1.Text += "0x" + Convert.ToInt32(numericUpDown7.Value).ToString("X2");
            richTextBox1.Text += ", ";
            richTextBox1.Text += "0x" + Convert.ToInt32(numericUpDown8.Value).ToString("X2");
            richTextBox1.Text += "]";
        }
    }

    public class MyGlobalVariable
    {
        public static int PRG_swap = 0;
        public static int HorzVertSelection = 0;
    }

    class PhysicalAddr
    {
        private int bank0 = 0;
        private int bank1 = 0;
        private int bank2 = 0;
        private int bank3 = 0;

        public PhysicalAddr() { }
        public PhysicalAddr(int B0, int B1, int B2, int B3)
        {
            bank0 = B0;
            bank1 = B1;
            bank2 = B2;
            bank3 = B3;
        }
        public int Bank0
        {
            get {  return bank0; }
            set { bank0 = value; }
        }
        public int Bank1
        {
            get { return bank1; }
            set { bank1 = value; }
        }
        public int Bank2
        {
            get { return bank2; }
            set { bank2 = value; }
        }
        public int Bank3
        {
            get { return bank3; }
            set { bank3 = value; }
        }
    }
}
