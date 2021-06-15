using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivilApp.Class.Earthquake
{
    class ElasticSiteSpectra
    {
        public Item ChTSLS;
        public Item ChTULS;
        public Item Z;
        public Item D;
        public Item Ru;
        public Item Rs;
        public Item NmaxT;
        public Item Ntdu;
        public Item Ntds;

        public Item CTSLS;
        public Item CTULS;

        public List<Item> calculationSLSList;
        public List<Item> calculationULSList;

        public ElasticSiteSpectra()
        {

        }

        public void Solve()
        {
            CalculateElasticSiteSpectraSLS();
            CalculateElasticSiteSpectraULS();
        }

        public void CalculateElasticSiteSpectraSLS()
        {
            calculationSLSList = new List<Item>();
            double SLS = ChTSLS.number * Z.number * Rs.number * Ntds.number;

            CTSLS = new Item("Elastic Site Spectra C(T)(SLS)", SLS, "", "AS/NZS 1170.5:2004 3.1.1");

            calculationSLSList.Add(ChTSLS);
            calculationSLSList.Add(Z);
            calculationSLSList.Add(Rs);
            calculationSLSList.Add(Ntds);
            calculationSLSList.Add(CTSLS);
        }

        public void CalculateElasticSiteSpectraULS()
        {
            calculationULSList = new List<Item>();
            double ULS = ChTULS.number * Z.number * Ru.number * Ntdu.number;

            CTULS = new Item("Elastic Site Spectra C(T)(ULS)", ULS, "", "AS/NZS 1170.5:2004 3.1.1");

            calculationULSList.Add(ChTULS);
            calculationULSList.Add(Z);
            calculationULSList.Add(Ru);
            calculationULSList.Add(NmaxT);
            calculationULSList.Add(Ntdu);
            calculationULSList.Add(CTULS);
        }

        private static void Print(List<Item> list)
        {
            foreach (Item item in list)
            {
                Console.WriteLine(item.name + ":" + MathHelper.Round3dec(item.number) + item.unit + ":" + item.note);
            }
        }
    }
}
