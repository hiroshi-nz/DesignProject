using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Word;
using System.Windows.Forms;
using CivilApp.Class.PortalFrameCalculation;

namespace CivilApp.Class
{
    class WordHelper
    {
        public static void ExportToWord()
        {
            try
            {
                Microsoft.Office.Interop.Word.Application application = new Microsoft.Office.Interop.Word.Application();
                object missing = System.Reflection.Missing.Value;
                Document document = application.Documents.Add(ref missing, ref missing, ref missing, ref missing);



                List<string> equationList = new List<string>();

                //equationList = WindEquations();
                //InsertEquations(document, equationList);

                //equationList = SteelEquations();
                //InsertEquations(document, equationList);


                //https://quicklatex.com/
                //https://en.wikibooks.org/wiki/LaTeX/Mathematics


                //NZS3404 9.3.2
                equationList.Add(@"V^*_f");
                equationList.Add(@"V^*_f \leq \phi V_f");

                equationList.Add(@"V_f");
                equationList.Add(@"f_{uf}");
                equationList.Add(@"k_r");
                equationList.Add(@"n_n");
                equationList.Add(@"A_c");
                equationList.Add(@"n_x");
                equationList.Add(@"A_o");
                equationList.Add(@"V_f = 0.62 f_{uf} k_r ( n_n A_c + n_x A_o)");

                //9.3.2.3 Bolt subject to combined shear and tension
                equationList.Add(@"N^*_{tf}");
                equationList.Add(@"N_{tf}");
                equationList.Add(@"N^*_{tf} \leq \phi N_{tf}");
                equationList.Add(@"\left( \frac {V^*_f}{\phi V_f}\right)^2 + \left( \frac {N^*_{tf}}{\phi N_{tf}}\right)^2 \leq 1.0");

                //9.3.2.4 ply in bearing
                equationList.Add(@"V_b");
                equationList.Add(@"d_f");
                equationList.Add(@"t_p");
                equationList.Add(@"f_{up}");
                equationList.Add(@"V_b = 3.2 d_f t_p f_{up}");

                equationList.Add(@"a_e");
                equationList.Add(@"V_b = a_e t_p f_{up}");
               // InsertEquations(document, equationList);

                //7 Member subject to axial tension
                //7.2.1 Nominal section capacity
                equationList.Add(@"N_t");
                equationList.Add(@"A_g");
                equationList.Add(@"f_y");
                equationList.Add(@"k_{te}");
                equationList.Add(@"A_n");
                equationList.Add(@"f_u");
                equationList.Add(@"N_t = A_g f_y");
                equationList.Add(@"N_t = 0.85 k_{te} A_n f_u");
                // not implemented in Word?
                //equationList.Add(@"N_t = \text{lesser of} \begin{cases} A_g f_y \\ 0.85 k_{te} A_n f_u \end{cases}");


                equationList.Add(@"V_{sit,\beta} = V_R M_d(M_{z,cat} M_s M_t)");

                equationList.Add(@"V_{sit,\beta}");
                equationList.Add(@"V_{R}");
                equationList.Add(@"M_d");
                equationList.Add(@"M_{z,cat}");
                equationList.Add(@"M_s");
                equationList.Add(@"M_t");

                equationList.Add(@"V_{500}");
                equationList.Add(@"V_{25}");

                equationList.Add(@"\tan^{-1}");

                equationList.Add(@"\rho_{air}");
                equationList.Add(@"V_{des,\theta}");
                equationList.Add(@"C_{fig}");
                equationList.Add(@"C_{dyn}");
                equationList.Add(@"p = (0.5\rho_{air})[V_{des,\theta}]^2 C_{fig} C_{dyn}");
                equationList.Add(@"f = (0.5\rho_{air})[V_{des,\theta}]^2 C_{fig} C_{dyn}");
                equationList.Add(@"C_{fig,i}");
                equationList.Add(@"C_{fig,e}");
                equationList.Add(@"C_{fig}");
                equationList.Add(@"C_{fig,i}");
                equationList.Add(@"C_{fig,e}");
                equationList.Add(@"C_{fig}");
                equationList.Add(@"C_{p,e}");
                equationList.Add(@"C_{p,i}");
                equationList.Add(@"C_f");
                equationList.Add(@"C_{p,n}");
                equationList.Add(@"K_a");
                equationList.Add(@"K_c");
                equationList.Add(@"K_{c,e}");
                equationList.Add(@"K_{c,i}");
                equationList.Add(@"K_l");
                equationList.Add(@"K_p");

                equationList.Add(@"C_{fig,i} = C_{p,i} K_{c,i}");
                equationList.Add(@"C_{fig,e} = C_{p,e} K_a K_{c,e} K_l K_p");
                equationList.Add(@"C_{fig} = C_f K_a K_c");


                equationList.Add(@"\phi V_f");
                equationList.Add(@"\phi N_{tf}");
                equationList.Add(@"\phi M_{sx}");
                equationList.Add(@"\phi M_{bx}");
                equationList.Add(@"\phi M_{sy}");
                equationList.Add(@"\phi V_{v}");
                equationList.Add(@"\phi M_{bx}=\alpha_m \times \alpha_s \times \phi M_{sx}");
                equationList.Add(@"\phi M_{bx}=\alpha_m \alpha_s \phi M_{sx}");
                equationList.Add(@"M^*_x \leq \phi M_{bx}");




                //Bolt Connection

                equationList.Add(@"\phi V_f = 0.62 f_{uf}k_r n_n A_c");//
                equationList.Add(@"");
                equationList.Add(@"");
                equationList.Add(@"");
                equationList.Add(@"");
                equationList.Add(@"");
                equationList.Add(@"");
                equationList.Add(@"");
                equationList.Add(@"");
                equationList.Add(@"");

                InsertEquations(document, equationList);

                equationList.Add(@"");
                equationList.Add(@"");


                equationList.Add(@"");
                equationList.Add(@"");
                equationList.Add(@"");
                equationList.Add(@"");
                equationList.Add(@"");
                equationList.Add(@"");

                equationList.Add(@"");
                equationList.Add(@"");
                equationList.Add(@"");
                equationList.Add(@"");
                equationList.Add(@"");
                equationList.Add(@"");
                equationList.Add(@"");
                equationList.Add(@"");
                equationList.Add(@"");
                equationList.Add(@"");




                object filename = @"C:\Users\Kite\Desktop\CivilApp.docx";
                document.SaveAs2(ref filename);
                document.Close(ref missing, ref missing, ref missing);
                document = null;
                application.Quit(ref missing, ref missing, ref missing);
                application = null;
                //MessageBox.Show("Document created successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void ExportToWordItems()
        {
            try
            {
                Microsoft.Office.Interop.Word.Application application = new Microsoft.Office.Interop.Word.Application();
                object missing = System.Reflection.Missing.Value;
                Document document = application.Documents.Add(ref missing, ref missing, ref missing, ref missing);

               
                List<Item> equationList = new List<Item>();

                //https://quicklatex.com/
                //https://en.wikibooks.org/wiki/LaTeX/Mathematics




                /*
 //NZS3404 9.3.2
 equationList.Add(@"V^*_f");
 equationList.Add(@"V^*_f \leq \phi V_f");

 equationList.Add(@"V_f");
 equationList.Add(@"f_{uf}");
 equationList.Add(@"k_r");
 equationList.Add(@"n_n");
 equationList.Add(@"A_c");
 equationList.Add(@"n_x");
 equationList.Add(@"A_o");
 equationList.Add();

 //9.3.2.3 Bolt subject to combined shear and tension
 equationList.Add(@"N^*_{tf}");
 equationList.Add(@"N_{tf}");
 equationList.Add(@"N^*_{tf} \leq \phi N_{tf}");
 equationList.Add(@"\left( \frac {V^*_f}{\phi V_f}\right)^2 + \left( \frac {N^*_{tf}}{\phi N_{tf}}\right)^2 \leq 1.0");

 //9.3.2.4 ply in bearing
 equationList.Add(@"V_b");
 equationList.Add(@"d_f");
 equationList.Add(@"t_p");
 equationList.Add(@"f_{up}");
 equationList.Add(@"V_b = 3.2 d_f t_p f_{up}");

 equationList.Add(@"a_e");
 equationList.Add(@"V_b = a_e t_p f_{up}");
 // InsertEquations(document, equationList);

 //7 Member subject to axial tension
 //7.2.1 Nominal section capacity
 equationList.Add(@"N_t");
 equationList.Add(@"A_g");
 equationList.Add(@"f_y");
 equationList.Add(@"k_{te}");
 equationList.Add(@"A_n");
 equationList.Add(@"f_u");
 equationList.Add(@"N_t = A_g f_y");
 equationList.Add(@"N_t = 0.85 k_{te} A_n f_u");
 // not implemented in Word?
 //equationList.Add(@"N_t = \text{lesser of} \begin{cases} A_g f_y \\ 0.85 k_{te} A_n f_u \end{cases}");
 */

                //Bolt Connection

                Item item = new Item(@"N_t = A_g f_y", 0, "", "Plate Yield Failure(Elongation) : lesser of two");
                equationList.Add(item);

                item = new Item(@"N_t = 0.85 k_{te} A_n f_u", 0, "", "Tensile Fracture Failure : lesser of two");
                equationList.Add(item);

                item = new Item(@"V_b = 3.2 d_f t_p f_{ up }", 0, "", "Plate in Bearing(Yielding) phi = 0.9");
                equationList.Add(item);

                //Bolt Moment
                item = new Item(@"a = \frac{n^' A_s}{p}", 0, "", "p = crs, n' = number of bolts in horizontal line");
                equationList.Add(item);

                item = new Item(@"b_{eff} = 8 \times t", 0, "", "t = the thickness of plate");
                equationList.Add(item);

                item = new Item(@"\frac{c}{\bar{y}} = \sqrt{\frac{b_eff}{a}}", 0, "", "");
                equationList.Add(item);

                item = new Item(@"", 0, "", "");
                equationList.Add(item);

                item = new Item(@"", 0, "", "");
                equationList.Add(item);

                item = new Item(@"", 0, "", "");
                equationList.Add(item);

                item = new Item(@"", 0, "", "");
                equationList.Add(item);

                item = new Item(@"", 0, "", "");
                equationList.Add(item);

                item = new Item(@"", 0, "", "");
                equationList.Add(item);


                item = new Item(@"\phi V_f = 0.62 f_{uf}k_r n_n A_c", 0, "", "Bolts in Shear");
                equationList.Add(item);
                item = new Item(@"V_f = 0.62 f_{uf} k_r ( n_n A_c + n_x A_o)", 0, "", "Bolts in Shear");
                equationList.Add(item);
                item = new Item(@"\phi V_f = 0.62 f_{uf}k_r n_n A_c", 0, "", "111");
                equationList.Add(item);


                InsertEquationsAndTexts(document, equationList);

                object filename = @"C:\Users\Kite\Desktop\CivilApp.docx";
                document.SaveAs2(ref filename);
                document.Close(ref missing, ref missing, ref missing);
                document = null;
                application.Quit(ref missing, ref missing, ref missing);
                application = null;
                //MessageBox.Show("Document created successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void InsertEquations(Document document, List<string> equationList)
        {

            OMaths oMath = document.OMaths;

            foreach (string equation in equationList)
            {
                Paragraph p = document.Content.Paragraphs.Add();
                p.Range.Text = equation;
                oMath.Add(p.Range);
                p.Range.InsertParagraphAfter();



            }
            oMath.BuildUp();         
        }

        public static void InsertEquationsAndTexts(Document document, List<Item> equationList)
        {

            OMaths oMath = document.OMaths;

            foreach (Item item in equationList)
            {
                Paragraph textP = document.Content.Paragraphs.Add();
                textP.Range.Text = item.note;
                textP.Range.InsertParagraphAfter();

                Paragraph p = document.Content.Paragraphs.Add();
                p.Range.Text = item.name;
                oMath.Add(p.Range);
                p.Range.InsertParagraphAfter();
            }
            oMath.BuildUp();
        }

        //https://stackoverflow.com/questions/44456263/microsoft-office-interop-word-omath
        public static void InsertEquation(Document document, string equation)
        {

            OMaths oMath = document.OMaths;
            Paragraph p = document.Content.Paragraphs.Add();
            p.Range.Text = equation;
            oMath.Add(p.Range);
            oMath.BuildUp();

            p.Range.InsertParagraphAfter();
        }

        public static List<string> SteelEquations()
        {
            List<string> equationList = new List<string>();

            equationList.Add(@"\phi V_f");
            equationList.Add(@"\phi N_{tf}");
            equationList.Add(@"\phi M_{sx}");
            equationList.Add(@"\phi M_{bx}");
            equationList.Add(@"\phi M_{sy}");
            equationList.Add(@"\phi V_{v}");
            equationList.Add(@"\phi M_{bx}=\alpha_m \times \alpha_s \times \phi M_{sx}");
            equationList.Add(@"\phi M_{bx}=\alpha_m \alpha_s \phi M_{sx}");
            equationList.Add(@"M^*_x \leq \phi M_{bx}");

            return equationList;
        }
        public static List<string> WindEquations()
        {
            List<string> equationList = new List<string>();
            equationList.Add(@"V_{sit,\beta} = V_R M_d(M_{z,cat} M_s M_t)");

            equationList.Add(@"V_{sit,\beta}");
            equationList.Add(@"V_{R}");
            equationList.Add(@"M_d");
            equationList.Add(@"M_{z,cat}");
            equationList.Add(@"M_s");
            equationList.Add(@"M_t");

            equationList.Add(@"V_{500}");
            equationList.Add(@"V_{25}");

            equationList.Add(@"\tan^{-1}");

            equationList.Add(@"\rho_{air}");
            equationList.Add(@"V_{des,\theta}");
            equationList.Add(@"C_{fig}");
            equationList.Add(@"C_{dyn}");
            equationList.Add(@"p = (0.5\rho_{air})[V_{des,\theta}]^2 C_{fig} C_{dyn}");
            equationList.Add(@"f = (0.5\rho_{air})[V_{des,\theta}]^2 C_{fig} C_{dyn}");
            equationList.Add(@"C_{fig,i}");
            equationList.Add(@"C_{fig,e}");
            equationList.Add(@"C_{fig}");
            equationList.Add(@"C_{fig,i}");
            equationList.Add(@"C_{fig,e}");
            equationList.Add(@"C_{fig}");
            equationList.Add(@"C_{p,e}");
            equationList.Add(@"C_{p,i}");
            equationList.Add(@"C_f");
            equationList.Add(@"C_{p,n}");
            equationList.Add(@"K_a");
            equationList.Add(@"K_c");
            equationList.Add(@"K_{c,e}");
            equationList.Add(@"K_{c,i}");
            equationList.Add(@"K_l");
            equationList.Add(@"K_p");

            equationList.Add(@"C_{fig,i} = C_{p,i} K_{c,i}");
            equationList.Add(@"C_{fig,e} = C_{p,e} K_a K_{c,e} K_l K_p");
            equationList.Add(@"C_{fig} = C_f K_a K_c");

            return equationList;
        }
    }
}
