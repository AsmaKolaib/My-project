using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace s
{
    public partial class Form1 : Form
    {

        Form2 F = new Form2();

        public Form1()
        {

            InitializeComponent();
            //إنشاء 3 ازرار
            Button btn_Encrypt = new Button();
            btn_Encrypt.Text = "Encrypt".ToString();
            btn_Encrypt.Name = "Encrypt".ToString();
            btn_Encrypt.Visible = true;
            btn_Encrypt.Location = new System.Drawing.Point(755, 625);
            this.Controls.Add(btn_Encrypt);
            btn_Encrypt.Click += new System.EventHandler(this.btn_Encrypt_Click);

            Button btn_Dncrypt = new Button();
            btn_Dncrypt.Text = "Dncrypt".ToString();
            btn_Dncrypt.Name = "Dncrypt".ToString();
            btn_Dncrypt.Visible = true;
            btn_Dncrypt.Location = new System.Drawing.Point(855, 625);
            this.Controls.Add(btn_Dncrypt);
            btn_Dncrypt.Click += new System.EventHandler(this.btn_dncrypt_Click);


            Button END = new Button();
            END.Text = "END".ToString();
            END.Name = "END".ToString();
            END.Visible = true;
            END.Location = new System.Drawing.Point(955, 625);
            this.Controls.Add(END);
            END.Click += new System.EventHandler(this.END_Click);
            
     
        }
        

        //دالة لرسم مستطيلات الجزء الخاص بتوليد المفاتيح الفرعية 
        public void draw1(int tx, int ty)
        {
            //DrawLine يرسم الخط 
            CreateGraphics().DrawLine(Pens.Black, tx, ty + 50, tx + 100, ty + 50);//
            CreateGraphics().DrawLine(Pens.Black, tx + 100, ty + 50, tx + 100, ty + 20);
            CreateGraphics().DrawLine(Pens.Black, tx + 100, ty + 20, tx, ty + 20);
            CreateGraphics().DrawLine(Pens.Black, tx, ty + 50, tx, ty + 20);
        }

        //دالة لرسم مستطيلات الجزء الخاص بتوليد الشفر

        public void draw2(int tx, int ty)
        {
            CreateGraphics().DrawLine(Pens.Black, tx, ty, tx + 120, ty);
            CreateGraphics().DrawLine(Pens.Black, tx + 120, ty, tx + 120, ty + 25);
            CreateGraphics().DrawLine(Pens.Black, tx + 120, ty + 25, tx, ty + 25);
            CreateGraphics().DrawLine(Pens.Black, tx, ty, tx, ty + 25);
        }
        int X;
        public int[] _k = new int[10];
        int[] subsout = new int[4];
        int[] text = new int[8];
        string tt, s1, s2,_s, skey;
        int flag, xx;
        DialogResult rusult;
        //دالة تحويل قيم الصفوف والاعمدة لمصفوفتي التعويض من الثنائي الى العشري
        public void index(int b1, int b2)
        {
            if (b1 == 0)
            {
                X = b2;
            }
            else if (b2 == 0)
            {
                X = 2;
            }
            else
            {
                X = 3;
            }
        }

        //دالة فحص القيم المدخله للمفتاح العام 
        //اذا كان 0 او 1 تضيفة الى المصفوفه الخاصه بالمفتاح
        //واذا كان فارغ 
        public void ch()
        {
            _s = F.text2.Text.ToString();

              for (int i = 0; i <= 9; i++)
            {
                if (_s.Substring(i, 1) == "0")
                {
                    _k[i] = 0;
                }
                else if (_s.Substring(i, 1) == "1")
                {
                    _k[i] = 1;
                }
                else
                {

                    int t = Convert.ToInt32(MessageBox.Show("Enter the correct key !", "Error key", MessageBoxButtons.RetryCancel));
                    if (t == 4)
                    {
                        F.ShowDialog();
                        _s = F.text2.Text.ToString();
                        i = 0;//لحتى ترجع الدوره من اول حرف لمايدخل مره اخرى 
                    }
                    else
                    {
                        System.Environment.Exit(1);
                    }
                }

            }
        
        }
        
        //دالة تقرأ النص المطلوب تشفيره وتقرأ المفتاح العام ثم نجزىء النص حرف حرف واستداعاء الدالة التي تقوم بالتشفير كل حرف ع حده 
        public void run(int f)
        {

            ch();//دالة التحقق من المفتاح المدخل 
            flag = f;//يتم اسناد القيمة 0 او 1 التي يتم تمرريها الى الدالة لتحديد العملية تشفير او فك تشفير 
           //0 تدل على التشفير 
            //1 تدل على فك التشفير 

            if (f == 0)//اذا كان تشفير 
            {
                //اسناد قيم النص المراد تشفيره ايضا المفتاح العام الى المتغيرات 
                s1 = F.text1.Text.ToString();

                skey = F.text2.Text.ToString();
                          }
            else
            {
                //في هذيه الحاله بيكون فك تشفير 
                //رح يسند قيمة s2  التي تخزن النص المشفر 
                //للمتغير s1 الذي تستخدمه الدالة للتشفير او فك التشفير 
                s1 = s2;//
            }

            s2 = "";//اجعل قيمته فاارغه علشان فيما بعد تسند له قيمه سوء النص المشفر او فك التشفير 
            // يتم استخدامه في الرساله الاخير التي تنعرض بعد تشفير كل حرف تحتوي ع النص كامل s2

            if (s1 == "")
            {
                return;
            }

            
            if (f == 0)
            {
                tt = "Encryption";
            }
            else
            {
                tt = "Decryption";
            }



            int tempVar = s1.Length;//متغير يحفظ قيمه طول السلسه  "النص المدخل"
         //الدورة الخاصه بتجزئيه النص المدخل الى حرف حرف 
            for (int kk = 1; kk <= tempVar; kk++)
            {
                clr();//يتم استعادة دالة تنظيف lables في كل مره 
                xx = Microsoft.VisualBasic.Strings.Asc(s1.Substring(kk - 1, 1));//اسناد الاسكي تبع الحرف الى المتغير xx
                //Substring هذيه الداله تعمل على البدء من اول حرف من الكلمه بمقدار خطوة واحده


                char t1 = Microsoft.VisualBasic.Strings.Chr(xx);//Chr هذيه الدالة تعمل على اعطاء القيمة التي تقابل الاسكي 
                //اي تخزن الحرف  مثلا دخلت a 

              
                t_1.Text = " : " + t1.ToString();//عرض الحرف يلي تكون امامه تمثيله في البينري شاهدي التنفيذ جزء توليد الشفره
              

                //هذيه الدوره تعمل على التمثيل البياني للحرف واسنادها الى المصفوفه text
                for (int ii = 1; ii <= 8; ii++)
                {
                    X = xx - 2 * Convert.ToInt32(xx / 2);
                    text[8 - ii] = X;
                    xx = Convert.ToInt32(xx / 2);
                }
            //الدالة التي تعمل على تشفير كل   حرف 
                run1(f);
                int a = 0;
                //دورة لتحويل التمثيل البينري لعرض الحرف الذي يقابل هذا 
                for (int i = 7; i >= 0; i--)
                {

                    a = Convert.ToInt32(a + text[i] * Math.Pow(2, 7 - i));
                }

                char t2 = Microsoft.VisualBasic.Strings.Chr(a);//تخزين الحرف المقابل لقيمة الاسكي المخزنة في a


                t_2.Text = " : " + t2.ToString();//عرض الحرف الذي يتم تشفير او فك تشفيره شاهدي شاشة التنفيذ الاسفل 
               

                s2 = s2 + t2;//علشان تعرض النص كامل
                //اضافه الحرف الجديد مع الاول لقبمة s2

                rusult = MessageBox.Show(t1 +" " + "-->" + " " + t2, tt, MessageBoxButtons.OK);//الرسالة التي تعرض كل حرف والحرف الذي تشفر له 
                //if (rusult == DialogResult.OK)
                //{
                //    return;
                //}
            }
            rusult = MessageBox.Show(s1 + " " + "-->" + " " +s2, tt, MessageBoxButtons.OKCancel);//الرسالة التي تعرض النص كامل سوء المشفر او فك التشفير 
            s1 = s2;//شاهدي التنفيذ بعد التشفير عند الضغط على زر فك التشفير لايتطلب ادخال النص   الذي تريدي فك تشفيره يتم اسناده الى 
            //s1 ليتم فك اتشفير 
        }

        //دالة تقوم بتحويل ناتج كل من مصفوفتي التعويض  من العشري  الى الثنائي 

        public void getsubsout(int s0, int s1)
        {
            if (s1 == 0 || s1 == 1)
            {
                subsout[0] = 0;
                subsout[1] = s1 % 2;
            }
            else
            {
                subsout[0] = 1;
                subsout[1] = s1 % 2;
            }
            if (s0 == 0 || s0 == 1)
            {
                subsout[2] = 0;
                subsout[3] = s0 % 2;
            }
            else
            {
                subsout[2] = 1;
                subsout[3] = s0 % 2;
            }
        }
        /// دالة تقوم بتوليد المفاتيح الفرعية وتوليد الشفرة مع رسم الواجهه التي توضح النتائج 
        public void run1(int flag)
        {

            int[] key = new int[10];//مصفوفه تخزين المفتاح العام 
            int[] p10 = new int[10];
            int[] ls11 = new int[5];
            int[] ls12 = new int[5];
            int[] ls21 = new int[5];
            int[] ls22 = new int[5];
            int[] p8 = new int[8];
            int[] key1 = new int[8];
            int[] key2 = new int[8];
            int i, j;

            //هذيه الدوره تسند قيم المفتاح الى مصفوفه اخرى المستخدمه في الخطوات التالية 
            for (i = 0; i <= 9; i++)
            {
                key[i] = _k[i];
            }


        

            //}
            //ترتيب البتات على ضوء الP10
            // 3 5 2 7 4 10 1 9 8 6
            // 1 2 3 4 5  6 7 8 9 10
            p10[0] = key[2]; //1 3
            p10[1] = key[4]; //2 5
            p10[2] = key[1]; //3 2 
            p10[3] = key[6]; //4 7
            p10[4] = key[3]; //5 4
            p10[5] = key[9]; //6 10
            p10[6] = key[0]; //7 1
            p10[7] = key[8]; // 8 9
            p10[8] = key[7]; // 9 8
            p10[9] = key[5]; //10 6


            //تقسيم البتات الى اللست الاولى والثانية 
            //إضافة الى الازاحة الدورانية
            ls11[4] = p10[0];

            for (i = 0; i <= 3; i++)
            { ls11[i] = p10[i + 1]; }

            ls12[4] = p10[5];

            for (i = 0; i <= 3; i++)
            { ls12[i] = p10[i + 6]; }



            //ترتيب البتات على ضوء الP8
            p8[0] = ls12[0]; //1 1
            p8[1] = ls11[2]; //2 3
            p8[2] = ls12[1]; //3 2 
            p8[3] = ls11[3]; //4 4
            p8[4] = ls12[2]; //5 3
            p8[5] = ls11[4]; //6 5
            p8[6] = ls12[4]; //7 5
            p8[7] = ls12[3]; //8 4 
       

            for (i = 0; i <= 7; i++)
            { key1[i] = p8[i]; }//المفتاح  الاول 
            ////////////////////////////////////////////
            //ازاحة دورانية بمقدار2بت 
            ls21[0] = ls11[2];//1 3
            ls21[1] = ls11[3];//2 4
            ls21[2] = ls11[4];//3 5
            ls21[3] = ls11[0];//4 1
            ls21[4] = ls11[1];//5 3
            ////////////////////////////////////////////
            ls22[0] = ls12[2];//1 3
            ls22[1] = ls12[3];//2 4
            ls22[2] = ls12[4];//3 5
            ls22[3] = ls12[0];//4 1
            ls22[4] = ls12[1];//5 3
            ///////////////////////////////////////////

            //ترتيب البتات ع ضوء P8
            p8[0] = ls22[0]; //1 1
            p8[1] = ls21[2]; //2 3
            p8[2] = ls22[1]; //3 2 
            p8[3] = ls21[3]; //4 4
            p8[4] = ls22[2]; //5 3
            p8[5] = ls21[4]; //6 5
            p8[6] = ls22[4]; //7 5
            p8[7] = ls22[3]; //8 4

            for (i = 0; i <= 7; i++)
            { key2[i] = p8[i]; }//المفتاح العام الثاني 
            ///////////////////////////////////////////////////

            for (i = 0; i <= 9; i++) { tKey.Text += "  " + key[i].ToString(); } //عرض المفتاح العام 
            ///////////////////////////////////////////////////////////////////////
            //اذا كان في قيمه للمفتاح العام خلي الالوان اسود غير كذ  خليها احمر 
            if (skey == "0000000000") { }
            else
            {
                this.tP8.ForeColor = System.Drawing.Color.Red;
                this.tLs1_2_2.ForeColor = System.Drawing.Color.Red;
                this.tLs1_2.ForeColor = System.Drawing.Color.Red;
                this.t1Ls1_1_2.ForeColor = System.Drawing.Color.Red;
                this.t1Ls1_1.ForeColor = System.Drawing.Color.Red;
                this.t2P10.ForeColor = System.Drawing.Color.Red;
                this.t1P10.ForeColor = System.Drawing.Color.Red;
                this.tEP_1.ForeColor = System.Drawing.Color.Red;
                this.t3sub.ForeColor = System.Drawing.Color.Red;
                this.t4sub.ForeColor = System.Drawing.Color.Red;
                this.tt1p4.ForeColor = System.Drawing.Color.Red;
                this.txor4.ForeColor = System.Drawing.Color.Red;
                this.ttext.ForeColor = System.Drawing.Color.Red;
                this.tP8_1.ForeColor = System.Drawing.Color.Red;
                this.tkey2.ForeColor = System.Drawing.Color.Red;
                this.tkey1.ForeColor = System.Drawing.Color.Red;
                this.tIP8.ForeColor = System.Drawing.Color.Red;
                this.tEP.ForeColor = System.Drawing.Color.Red;
                this.tXor3.ForeColor = System.Drawing.Color.Red;
                this.t1IP8.ForeColor = System.Drawing.Color.Red;
                this.t1Xor.ForeColor = System.Drawing.Color.Red;
                this.t0sub.ForeColor = System.Drawing.Color.Red;
                this.t1sub.ForeColor = System.Drawing.Color.Red;
                this.t2Xor.ForeColor = System.Drawing.Color.Red;
                this.t1p4.ForeColor = System.Drawing.Color.Red;
                this.t1xor2.ForeColor = System.Drawing.Color.Red;
                this.txor2.ForeColor = System.Drawing.Color.Red;
                this.t4xor2.ForeColor = System.Drawing.Color.Red;
                this.ttIP8_4.ForeColor = System.Drawing.Color.Red;
                this.t1IP8_4.ForeColor = System.Drawing.Color.Red;
                this.t1Xor3.ForeColor = System.Drawing.Color.Red;

            }

            //دورات عرض 
            for (i = 5; i <= 9; i++) { t1P10.Text += "  " + p10[i].ToString(); }

            
            

            for (i = 0; i <= 4; i++) { t2P10.Text += "  " + p10[i].ToString(); }


            for (i = 5; i <= 9; i++) { t1_P10.Text += "  " + p10[i].ToString(); }


            for (i = 0; i <= 4; i++) { t2_P10.Text += "  " + p10[i].ToString(); }

            for (i = 0; i <= 4; i++) { t1Ls1_1.Text += "  " + ls12[i].ToString(); }

            for (i = 0; i <= 4; i++) { t1Ls1_1_2.Text += "  " + ls11[i].ToString(); }

            for (i = 0; i <= 4; i++) { t2Ls1_1.Text += "  " + ls12[i].ToString(); }

            for (i = 0; i <= 4; i++) { t2Ls1_1_2.Text += "  " + ls11[i].ToString(); }

            for (i = 0; i <= 4; i++) { t3Ls1_1_2.Text += "  " + ls11[i].ToString(); }

            for (i = 0; i <= 4; i++) { t4Ls1_1_2.Text += "  " + ls12[i].ToString(); }


            ////////////////////////////////////////

            for (i = 0; i <= 4; i++) { tLs1_2.Text += "  " + ls22[i].ToString(); }


            for (i = 0; i <= 4; i++) { t1Ls1_2.Text += "  " + ls22[i].ToString(); }
           
            
            for (i = 0; i <= 4; i++) { tLs1_2_2.Text += "  " + ls21[i].ToString(); }


            for (i = 0; i <= 4; i++) { t1Ls1_2_2.Text += "  " + ls21[i].ToString(); }


            ///////////////////////////////////////
           
            for (i = 0; i <= 7; i++) { tP8_1.Text += "  " + key2[i].ToString(); }


            ///////////////////////////////

            for (i = 0; i <= 7; i++) { tP8.Text += "  " + key1[i].ToString(); }
           
            for (i = 0; i <= 7; i++) { tkey2.Text += " " + key2[i].ToString(); }

            for (i = 0; i <= 7; i++) { tkey1.Text += " " + key1[i].ToString(); }

            //اذا كان فك تشفير يتم تبديل بين المفتاح الاول يرجع الثاني والثاني يرجع الاول 
            if (flag == 1)
            {
                int t;
                for (i = 0; i <= 7; i++)
                {
                    t = key1[i];
                    key1[i] = key2[i];
                    key2[i] = t;
                }
            }

            ///////////////////////text////////////////
            
            
     
           

            for (i = 0; i <= 7; i++) { tplaintext.Text += "  " + text[i].ToString(); }//عرض الحرف بالبينري قبل التشفير او فك التشفير 


            int[] text1 = new int[8];
            int[] ep = new int[8];
            int[] xor1 = new int[8];
            int[] xor2 = new int[4];
            int[] xor3 = new int[8];
            int[] xor4 = new int[4];
            int[,] subs0 = new int[4, 4];
            int[,] subs1 = new int[4, 4];
            int s0, s1;
            int[] p4 = new int[4];//4


            //اسناد قيم المصفوفتين s0 , s1 
            //كمافي الفيديو
            subs0[0, 0] = 0;
            subs0[0, 1] = 3;
            subs0[0, 2] = 2;
            subs0[0, 3] = 1;

            subs0[1, 0] = 3;
            subs0[1, 1] = 2;
            subs0[1, 2] = 1;
            subs0[1, 3] = 0;

            subs0[2, 0] = 0;
            subs0[2, 1] = 2;
            subs0[2, 2] = 1;
            subs0[2, 3] = 3;

            subs0[3, 0] = 3;//////////////////////////////////////////////
            subs0[3, 1] = 1;
            subs0[3, 2] = 3;
            subs0[3, 3] = 0;
            ///////////////////////////////////////////////////////////////////////////////////////////


            subs1[0, 0] = 0;
            subs1[0, 1] = 1;
            subs1[0, 2] = 2;
            subs1[0, 3] = 3;

            subs1[1, 0] = 2;
            subs1[1, 1] = 0;
            subs1[1, 2] = 3;
            subs1[1, 3] = 1;

            subs1[2, 0] = 3;
            subs1[2, 1] = 0;
            subs1[2, 2] = 1;
            subs1[2, 3] = 0;///////////////////////////

            subs1[3, 0] = 2;
            subs1[3, 1] = 1;
            subs1[3, 2] = 3;
            subs1[3, 3] = 0;


            ////////////////ip//////////////
            //ترتيب البتات ع ضوء IP
            text1[0] = text[1];//1 2
            text1[1] = text[5];//2 6
            text1[2] = text[2];//3 3 
            text1[3] = text[0];//4 1
            text1[4] = text[3];//5 4
            text1[5] = text[7];//6 8
            text1[6] = text[4];//7 5
            text1[7] = text[6];//8 7


            
           //دورات عرض 
            for (i = 4; i <= 7; i++) { tIP8.Text += "  " + text1[i].ToString(); }


            for (i = 4; i <= 7; i++) { tIP8_1.Text += "  " + text1[i].ToString(); }


            for (i = 4; i <= 7; i++) { tIP8_2.Text += "  " + text1[i].ToString(); }

           
            
            for (i = 0; i <= 3; i++) { t1IP8.Text += "  " + text1[i].ToString(); }


            for (i = 0; i <= 3; i++) { t1IP8_1.Text += "  " + text1[i].ToString(); }




            ///////////////EP////////////////
            ////ترتيب البتات ع ضوء IP
            ep[0] = text1[7];//1 8
            ep[1] = text1[4];//2 5
            ep[2] = text1[5];//3 6  
            ep[3] = text1[6];//4 7
            ep[4] = text1[5];//5 6
            ep[5] = text1[6];//6 7
            ep[6] = text1[7];//7 8
            ep[7] = text1[4];//8 5

          
            for (i = 0; i <= 7; i++) { tEP.Text += "  " + ep[i].ToString(); }//عرض البتات الناتجة من الترتيب 

            //عملية xor 1
            for (i = 0; i <= 7; i++)
            {
                xor1[i] = key1[i] ^ ep[i];
            }



           
            
            //دورات عرض 
            for (i = 0; i <= 3; i++) { t1Xor.Text += "  " + xor1[i].ToString(); }

          
            
            for (i = 0; i <= 3; i++) { t2Xor.Text += "  " + xor1[i].ToString(); }




            //هنا مثل مايشرح عند الدقيقة26

            //substitution-0
            index(xor1[4], xor1[7]);//من االدالة نحصل على قيمة الاكس التي تمثل عدد الصف 
            i = X;

            index(xor1[5], xor1[6]);//من االدالة نحصل على قيمة الاكس التي تمثل عدد العمود 
            j = X;
           
            //بعد ماحددنا الصف والعمود نحدد القيمة ونسندها للمتغير 
            s0 = subs0[i, j];


            //نفس الخطوات التي فوق 
            //substitution-1
            index(xor1[0], xor1[3]);
            i = X;
           
            index(xor1[1], xor1[2]);
            j = X;
           
            s1 = subs1[i, j];
            //    ts.Text = s1.ToString();


            getsubsout(s0, s1);//الدالة تعرض الناتج بالبينري مثلا 2 تعرضه 10

           
            
            for (i = 0; i <= 1; i++) { t0sub.Text += "  " + subsout[i].ToString(); }

           
           
            for (i = 2; i <= 3; i++) { t1sub.Text += "  " + subsout[i].ToString(); }



            for (i = 0; i <= 3; i++) { t2sub.Text += "  " + subsout[i].ToString(); }

            ///////////////////////////////////////////////

            //ترتيبالبتات على ضوء P4
            p4[0] = subsout[1]; // 1 2
            p4[1] = subsout[3]; // 2 4
            p4[2] = subsout[2]; // 3 3
            p4[3] = subsout[0]; // 4 1




           
            
            for (i = 0; i <= 3; i++) { t1p4.Text += "  " + p4[i].ToString(); }

            //XOr - 2
            for (i = 0; i <= 3; i++)
            {
                xor2[i] = p4[i] ^ text1[i];

            }

          
            for (i = 0; i <= 3; i++) { txor2.Text += "  " + xor2[i].ToString(); }

           
          
            for (i = 4; i <= 7; i++) { ttIP8_4.Text += "  " + text1[i].ToString(); }


            for (i = 4; i <= 7; i++) { tt1IP8_4.Text += "  " + text1[i].ToString(); }

           
          
            for (i = 0; i <= 3; i++) { t1xor2.Text += "  " + xor2[i].ToString(); }

           
           
            for (i = 4; i <= 7; i++) { t1IP8_4.Text += "  " + text1[i].ToString(); }



            for (i = 0; i <= 3; i++) { t2xor2.Text += "  " + xor2[i].ToString(); }


            for (i = 0; i <= 3; i++) { t3xor2.Text += "  " + xor2[i].ToString(); }

           
            
            for (i = 0; i <= 3; i++) { t4xor2.Text += "  " + xor2[i].ToString(); }




            ////ep

            //ترتيب البتات ع ضوء xor 2
            ep[0] = xor2[3];//1 4
            ep[1] = xor2[0];//2 1
            ep[2] = xor2[1];//3 2  
            ep[3] = xor2[2];//4 3
            ep[4] = xor2[1];//5 2
            ep[5] = xor2[2];//6 3
            ep[6] = xor2[3];//7 4
            ep[7] = xor2[0];//8 1

            //باقي الخطوات كما الدوره الاولى توليد الشفرة
           
            for (i = 0; i <= 7; i++) { tEP_1.Text += "  " + ep[i].ToString(); }
            //XOR 3
            for (i = 0; i <= 7; i++)
            {
                xor3[i] = key2[i] ^ ep[i];

            }
           
           
            for (i = 0; i <= 3; i++) { tXor3.Text += "  " + xor3[i].ToString(); }

           
           
            for (i = 0; i <= 3; i++) { t1Xor3.Text += "  " + xor3[i].ToString(); }


            //substitution-3
            index(xor3[4], xor3[7]);
            i = X;
            index(xor3[5], xor3[6]);

            j = X;
            s0 = subs0[i, j];

            //substitution-4
            index(xor3[0], xor3[3]);
            i = X;

            index(xor3[1], xor3[2]);
            j = X;
            s1 = subs1[i, j];
            getsubsout(s0, s1);


            for (i = 0; i <= 1; i++) { t3sub.Text += "  " + subsout[i].ToString(); }


            for (i = 2; i <= 3; i++) { t4sub.Text += "  " + subsout[i].ToString(); }


            for (i = 0; i <= 3; i++) { t6sub.Text += "  " + subsout[i].ToString(); }
            /////////////////////////////////////////////////



            p4[0] = subsout[1]; // 1 2
            p4[1] = subsout[3]; // 2 4
            p4[2] = subsout[2]; // 3 3
            p4[3] = subsout[0]; // 4 1




            for (i = 0; i <= 3; i++) { tt1p4.Text += "  " + p4[i].ToString(); }

            //for (i = 0; i <= 3; i++) { tx.Text += " " + text1[i].ToString(); }


            int b;
            for (i = 0; i <= 3; i++)
            {
                b = i + 4;
                xor4[i] = p4[i] ^ text1[b];
            }


            for (i = 0; i <= 3; i++) { txor4.Text += "  " + xor4[i].ToString(); }

            text[0] = xor4[3];//1 4
            text[1] = xor4[0];//2 1
            text[2] = xor4[2];//3 3  
            text[3] = xor2[0];//4 1
            text[4] = xor2[2];//5 3
            text[5] = xor4[1];//6 2
            text[6] = xor2[3];//7 4
            text[7] = xor2[1];//8 2




            for (i = 0; i <= 7; i++) { ttext.Text += "  " + text[i].ToString(); }

            ////////////////////////////////////////////////////



        }

        private void clr() {
          
            foreach (Label _Label in this.Controls.OfType<Label>())
            {

                if (_Label.Text != "+" && _Label.Text != "EP" && _Label.Text != "P10" && _Label.Text != "key (10 bits)" 
                    && _Label.Text != "LS1" && _Label.Text != "LS2" && _Label.Text != "P8" && _Label.Text != "Key2 : " 
                    && _Label.Text != "Key1 : " && _Label.Text != "plaintext(8 bit)" && _Label.Text != "Init.permut.(IP8)" 
                    && _Label.Text != "Substitution-0" && _Label.Text != "Substitution-1" && _Label.Text != "Permutation(P4)" 
                    && _Label.Text != "Chiptertext(8 bits)" &&  _Label.Text != "Inv.Premut(IP~)" && _Label.Text != "Swap" )
                {

                    _Label.Text = string.Empty;
                }
            }
        
        
        
        
        
        
        }
   
        private void Form1_Load(object sender, EventArgs e)
        {

            skey = "0000000000";
            s1 = "";
            run1(0);
            
            //////////////////////////////////////////////////
            draw1(1015, 53);//|
            draw1(1083, 140);//
            draw1(950, 140);//
            draw1(1015, 230);//|
            draw1(1083, 318);//
            draw1(950, 318);//
            draw1(1015, 415);//|

            CreateGraphics().DrawLine(Pens.Black, 1068, 33, 1068, 55);
            CreateGraphics().DrawLine(Pens.Black, 1070, 51, 1068, 55);//x+2,y-4
            CreateGraphics().DrawLine(Pens.Black, 1068, 55, 1066, 51);//x-2,y-4

            CreateGraphics().DrawLine(Pens.Black, 1063, 295, 1063, 300);
            CreateGraphics().DrawLine(Pens.Black, 1063, 300, 700, 300);

            CreateGraphics().DrawLine(Pens.Black, 1063, 485, 1063, 510);
            CreateGraphics().DrawLine(Pens.Black, 1063, 510, 700, 510);


           

            CreateGraphics().DrawLine(Pens.Black, 1103, 126, 1103, 141);
            CreateGraphics().DrawLine(Pens.Black, 1105, 137, 1103, 141);
            CreateGraphics().DrawLine(Pens.Black, 1103, 141, 1101, 137);


            CreateGraphics().DrawLine(Pens.Black, 1031, 126, 1031, 141);
            CreateGraphics().DrawLine(Pens.Black, 1033, 137, 1031, 141);
            CreateGraphics().DrawLine(Pens.Black, 1031, 141, 1029, 137);

            CreateGraphics().DrawLine(Pens.Black, 1103, 210, 1103, 235);
            CreateGraphics().DrawLine(Pens.Black, 1105, 231, 1103, 235);
            CreateGraphics().DrawLine(Pens.Black, 1103, 235, 1101, 231);

            CreateGraphics().DrawLine(Pens.Black, 1031, 210, 1031, 235);
            CreateGraphics().DrawLine(Pens.Black, 1033, 231, 1031, 235);
            CreateGraphics().DrawLine(Pens.Black, 1031, 235, 1029, 231);

            CreateGraphics().DrawLine(Pens.Black, 1130, 210, 1130, 323);
            CreateGraphics().DrawLine(Pens.Black, 1132, 319, 1130, 323);
            CreateGraphics().DrawLine(Pens.Black, 1130, 323, 1128, 319);


            CreateGraphics().DrawLine(Pens.Black, 998, 210, 998, 323);
            CreateGraphics().DrawLine(Pens.Black, 1000, 319, 998, 323);
            CreateGraphics().DrawLine(Pens.Black, 998, 323, 996, 319);


            CreateGraphics().DrawLine(Pens.Black, 1103, 385, 1103, 410);
            CreateGraphics().DrawLine(Pens.Black, 1105, 406, 1103, 410);
            CreateGraphics().DrawLine(Pens.Black, 1103, 410, 1101, 406);

            CreateGraphics().DrawLine(Pens.Black, 1031, 385, 1031, 410);
            CreateGraphics().DrawLine(Pens.Black, 1033, 406, 1031, 410);
            CreateGraphics().DrawLine(Pens.Black, 1031, 410, 1029, 406);

            /////////////////////////////
         

            //////////////////////////////////////////////////
            CreateGraphics().DrawLine(Pens.Black, 236, 28, 236, 48);
            CreateGraphics().DrawLine(Pens.Black, 238, 46, 236, 48);//x+2,y-4
            CreateGraphics().DrawLine(Pens.Black, 236, 48, 234, 46);//x-2,y-4

            draw2(176, 66);//|
            CreateGraphics().DrawLine(Pens.Black, 200, 99, 200, 111);
            CreateGraphics().DrawLine(Pens.Black, 99, 111, 200, 111);
            CreateGraphics().DrawLine(Pens.Black, 99, 111, 99, 330);//

            CreateGraphics().DrawLine(Pens.Black, 99, 330, 227, 330);//--
            CreateGraphics().DrawLine(Pens.Black, 223, 332, 227, 330);
            CreateGraphics().DrawLine(Pens.Black, 227, 330, 223, 328);


            CreateGraphics().DrawLine(Pens.Black, 280, 99, 280, 111);
            CreateGraphics().DrawLine(Pens.Black, 280, 111, 480, 111);//ــــــــــــــ
            CreateGraphics().DrawLine(Pens.Black, 480, 111, 480, 340);//|
            CreateGraphics().DrawLine(Pens.Black, 280, 340, 480, 340);//ــــــــــــــ

            CreateGraphics().DrawLine(Pens.Black, 280, 340, 280, 344);//--
            CreateGraphics().DrawLine(Pens.Black, 282, 342, 280, 344);
            CreateGraphics().DrawLine(Pens.Black, 280, 344, 278, 342);

            CreateGraphics().DrawLine(Pens.Black, 322, 111, 322, 116);//---
            CreateGraphics().DrawLine(Pens.Black, 324, 114, 322, 116);
            CreateGraphics().DrawLine(Pens.Black, 322, 116, 320, 114);

            CreateGraphics().DrawLine(Pens.Black, 322, 172, 322, 178);//-----
            CreateGraphics().DrawLine(Pens.Black, 324, 176, 322, 178);
            CreateGraphics().DrawLine(Pens.Black, 322, 178, 320, 176);//

            draw2(263, 132);//|

            Pen pen = new Pen(Color.Black, 1);
            CreateGraphics().DrawEllipse(pen, 315, 180,14 , 14);

            CreateGraphics().DrawLine(Pens.Black, 322, 194, 322, 196);
            CreateGraphics().DrawLine(Pens.Black, 233, 197, 410, 197);
            CreateGraphics().DrawLine(Pens.Black, 233, 197, 233, 201);//--
            CreateGraphics().DrawLine(Pens.Black, 235, 199, 233, 201);
            CreateGraphics().DrawLine(Pens.Black, 233, 201, 231, 199);

            CreateGraphics().DrawLine(Pens.Black, 410, 197, 410, 201);//--
            CreateGraphics().DrawLine(Pens.Black, 412, 199, 410, 201);
            CreateGraphics().DrawLine(Pens.Black, 410, 201, 408, 199);

            draw2(176, 215);//_
            draw2(345, 215);//_
            CreateGraphics().DrawLine(Pens.Black, 233, 258, 410, 258);
            CreateGraphics().DrawLine(Pens.Black, 233, 258, 233, 255);
            CreateGraphics().DrawLine(Pens.Black, 410, 258, 410, 255);

            CreateGraphics().DrawLine(Pens.Black, 322, 258, 322, 260);
            CreateGraphics().DrawLine(Pens.Black, 233, 260, 322, 260);

            CreateGraphics().DrawLine(Pens.Black, 233, 260, 233, 264);//--
            CreateGraphics().DrawLine(Pens.Black, 235, 262, 233, 264);
            CreateGraphics().DrawLine(Pens.Black, 233, 264, 231, 262);

            draw2(176, 278);//_

            CreateGraphics().DrawLine(Pens.Black, 233, 315, 233, 321);//--
            CreateGraphics().DrawLine(Pens.Black, 235, 319, 233, 321);
            CreateGraphics().DrawLine(Pens.Black, 233, 321, 231, 319); 

            Pen pen1 = new Pen(Color.Black, 1);
            CreateGraphics().DrawEllipse(pen1, 226, 323, 14, 14);
            CreateGraphics().DrawLine(Pens.Black, 233, 338, 233, 340);
            CreateGraphics().DrawLine(Pens.Black, 200, 340, 233, 340);

            CreateGraphics().DrawLine(Pens.Black, 200, 340, 200, 344);//--
            CreateGraphics().DrawLine(Pens.Black, 202, 342, 200, 344);
            CreateGraphics().DrawLine(Pens.Black, 200, 344, 198, 342);
            draw2(176, 358);//

            CreateGraphics().DrawLine(Pens.Black, 99, 397, 200, 397);
            CreateGraphics().DrawLine(Pens.Black, 200, 391, 200, 397);//|
            CreateGraphics().DrawLine(Pens.Black, 99, 397, 99, 601);//

            CreateGraphics().DrawLine(Pens.Black, 99, 601, 227, 601);//--
            CreateGraphics().DrawLine(Pens.Black, 223, 603, 227, 601);
            CreateGraphics().DrawLine(Pens.Black, 227, 601, 223, 598);

            CreateGraphics().DrawLine(Pens.Black, 280, 396, 280, 399);
            CreateGraphics().DrawLine(Pens.Black, 280, 399, 480, 399);
            CreateGraphics().DrawLine(Pens.Black, 481, 399, 481, 610);//|
            CreateGraphics().DrawLine(Pens.Black, 274, 610, 481, 610);//ــــــــــــــ

            CreateGraphics().DrawLine(Pens.Black, 274, 610, 274, 614);//--
            CreateGraphics().DrawLine(Pens.Black, 276, 612, 274, 614);
            CreateGraphics().DrawLine(Pens.Black, 274, 614, 272, 612);

            CreateGraphics().DrawLine(Pens.Black, 322, 399, 322, 403);//--
            CreateGraphics().DrawLine(Pens.Black, 324, 401, 322, 403);
            CreateGraphics().DrawLine(Pens.Black, 322, 403, 320, 401);

            draw2(263, 415);//|

            CreateGraphics().DrawLine(Pens.Black, 322, 453, 322, 457);//--
            CreateGraphics().DrawLine(Pens.Black, 324, 455, 322, 457);
            CreateGraphics().DrawLine(Pens.Black, 322, 457, 320, 455);



            Pen pen2 = new Pen(Color.Black, 1);
            CreateGraphics().DrawEllipse(pen2, 315, 458, 14, 14);
            CreateGraphics().DrawLine(Pens.Black, 322, 471, 322, 474);
            CreateGraphics().DrawLine(Pens.Black, 233, 474, 410, 474);
            CreateGraphics().DrawLine(Pens.Black, 233, 474, 233, 478);//--
            CreateGraphics().DrawLine(Pens.Black, 235, 476, 233, 478);
            CreateGraphics().DrawLine(Pens.Black, 233, 478, 231, 476);

            CreateGraphics().DrawLine(Pens.Black, 410, 474, 410, 478);//--
            CreateGraphics().DrawLine(Pens.Black, 412, 476, 410, 478);
            CreateGraphics().DrawLine(Pens.Black, 410, 478, 408, 476);

            draw2(176, 490);//_
            draw2(343, 490);//_
            CreateGraphics().DrawLine(Pens.Black, 233, 531, 410, 531);
            CreateGraphics().DrawLine(Pens.Black, 233, 531, 233, 528);

            CreateGraphics().DrawLine(Pens.Black, 410, 531, 410, 528);
            CreateGraphics().DrawLine(Pens.Black, 322, 531, 322, 533);
            CreateGraphics().DrawLine(Pens.Black, 233, 533, 322, 533);

            CreateGraphics().DrawLine(Pens.Black, 233, 533, 233, 537);//--
            CreateGraphics().DrawLine(Pens.Black, 235, 535, 233, 537);
            CreateGraphics().DrawLine(Pens.Black, 233, 537, 231, 535);

            draw2(176, 550);//_
            CreateGraphics().DrawLine(Pens.Black, 233, 587, 233, 592);//--
            CreateGraphics().DrawLine(Pens.Black, 235, 590, 233, 592);
            CreateGraphics().DrawLine(Pens.Black, 233, 592, 231, 590);

            Pen pen3 = new Pen(Color.Black, 1);
            CreateGraphics().DrawEllipse(pen3, 226, 594, 14, 14);
            CreateGraphics().DrawLine(Pens.Black, 233, 608, 233, 610);
            CreateGraphics().DrawLine(Pens.Black, 200, 610, 233, 610);

            CreateGraphics().DrawLine(Pens.Black, 200, 610, 200,614);//--
            CreateGraphics().DrawLine(Pens.Black, 202, 612, 200, 614);
            CreateGraphics().DrawLine(Pens.Black, 200, 614, 198, 612);
            draw2(176, 626);//

            CreateGraphics().DrawLine(Pens.Black, 232, 652, 232, 660);//--
            CreateGraphics().DrawLine(Pens.Black, 234, 658, 232, 660);
            CreateGraphics().DrawLine(Pens.Black, 232, 660, 230, 658);

            CreateGraphics().DrawLine(Pens.Black, 329, 187, 700, 187);//--
            CreateGraphics().DrawLine(Pens.Black, 331, 189, 329, 187);
            CreateGraphics().DrawLine(Pens.Black, 329, 187, 331, 185);

            CreateGraphics().DrawLine(Pens.Black, 700, 187, 700, 300);

            CreateGraphics().DrawLine(Pens.Black, 329, 465, 700, 465);//--
            CreateGraphics().DrawLine(Pens.Black, 331, 467, 329, 465);
            CreateGraphics().DrawLine(Pens.Black, 329, 465, 331, 463);


            CreateGraphics().DrawLine(Pens.Black, 700, 465, 700, 510);

            CreateGraphics().DrawLine(Pens.Black, 193, 358, 276, 383);
            CreateGraphics().DrawLine(Pens.Black, 283, 358, 191, 383);

        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            label1.Text = "X = " + e.X + " ; Y = " + e.Y;

        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            label1.Text = "X = " + e.X + " ; Y = " + e.Y;

        }

        private void END_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      
            private void btn_Encrypt_Click(object sender, EventArgs e)
        {
            //if (F.ShowDialog() == DialogResult.OK)
            //{
            //    run(0);
            //}
            F.ShowDialog();
            run(0);
        }

            private void btn_dncrypt_Click(object sender, EventArgs e)
            {
                run(1);
            }

            private void pictureBox1_Click(object sender, EventArgs e)
            {
                listBox1.Visible = true;
                listBox1.Items.Add("النص الاصلي المطلوب تشفيره أو فك شفرته");
                
                //label2.Text = "النص الاصلي المطلوب تشفيره أو فك شفرته";
            }

            private void pictureBox2_Click(object sender, EventArgs e)
            {
                listBox1.Visible = true;
                listBox1.Items.Add(  "  : تبديل محتويات مواقع النص الاصلي لتصبح بالترتيب التالي "+ "\n" +" 2 6 3 1 4 8 5 7 ");
                
              
               // label2.Text = " 2 6 3 1 4 8 5 7" + " : تبديل محتويات مواقع النص الاصلي لتصبح بالترتيب التالي ";
            }

            

            private void pictureBox2_Click_1(object sender, EventArgs e)
            {
                pictureBox1.Visible = true;
                pictureBox1.Image = Properties.Resources._1_01;
            }

            private void pictureBox3_Click_1(object sender, EventArgs e)
            {
                pictureBox1.Visible = true;
                pictureBox1.Image = Properties.Resources._2_01;

            }

            private void pictureBox4_Click(object sender, EventArgs e)
            {
                pictureBox1.Visible = true;
                pictureBox1.Image = Properties.Resources._3_01;
            }

            private void pictureBox5_Click(object sender, EventArgs e)
            {
                pictureBox1.Visible = true;
                pictureBox1.Image = Properties.Resources._4_01;
            }

           

            private void pictureBox6_Click(object sender, EventArgs e)
            {
                pictureBox1.Visible = true;
                pictureBox1.Image = Properties.Resources._5_01;
                
            }

            private void pictureBox8_Click(object sender, EventArgs e)
            {
                pictureBox1.Visible = true;
                pictureBox1.Image = Properties.Resources._6_01;
            }

            private void pictureBox7_Click(object sender, EventArgs e)
            {
                pictureBox1.Visible = true;
                pictureBox1.Image = Properties.Resources._7_1_01;
            }

            private void pictureBox9_Click(object sender, EventArgs e)
            {
                pictureBox1.Visible = true;
                pictureBox1.Image = Properties.Resources._8_01;
            }

            private void pictureBox10_Click(object sender, EventArgs e)
            {
                pictureBox1.Visible = true;
                pictureBox1.Image = Properties.Resources._10_01;
            }

            private void pictureBox12_Click(object sender, EventArgs e)
            {
                pictureBox1.Visible = true;
                pictureBox1.Image = Properties.Resources._10_01;
            }

            private void pictureBox11_Click(object sender, EventArgs e)
            {
                pictureBox1.Visible = true;
                pictureBox1.Image = Properties.Resources._9_01;
            }

            private void pictureBox13_Click(object sender, EventArgs e)
            {
                pictureBox1.Visible = true;
                pictureBox1.Image = Properties.Resources._9_01;
            }

            private void pictureBox15_Click(object sender, EventArgs e)
            {
                pictureBox1.Visible = true;
                pictureBox1.Image = Properties.Resources._11_01_01;
            }

            private void pictureBox14_Click(object sender, EventArgs e)
            {
                pictureBox1.Visible = true;
                pictureBox1.Image = Properties.Resources._11_01_01;
            }
         

        
         




    }
}
