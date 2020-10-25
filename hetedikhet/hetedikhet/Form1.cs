using hetedikhet.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace hetedikhet
{
   
    public partial class Form1 : Form
    {
        List<PErson> Population = new List<PErson>();
        List<BirthProbability> BirthProbabilities = new List<BirthProbability>();
        List<DeathProbability> DeathProbabilities = new List<DeathProbability>();
        Random rng = new Random(1234);

        public Form1()
        {
            InitializeComponent();
            Population = GetPopulation(@"C:\Temp\nép.csv");
            BirthProbabilities = GetBirthProbabilities(@"C:\Temp\születés.csv");
            DeathProbabilities = GetDeathProbabilities(@"C:\Temp\halál.csv");

            
            
        }


        private void SimStep(int year, PErson person)
        {

            if (!person.IsAlive) return;


            byte age = (byte)(year - person.BirthYear);


            double pDeath = (from x in DeathProbabilities
                             where x.Gender == person.Gender && x.age == age
                             select x.P).FirstOrDefault();

            if (rng.NextDouble() <= pDeath)
                person.IsAlive = false;

            
            if (person.IsAlive && person.Gender == Gender.Female)
            {

                double pBirth = (from x in BirthProbabilities
                                 where x.age == age
                                 select x.P).FirstOrDefault();

                if (rng.NextDouble() <= pBirth)
                {
                    PErson újszülött = new PErson();
                    újszülött.BirthYear = year;
                    újszülött.NbrOfChildren = 0;
                    újszülött.Gender = (Gender)(rng.Next(1, 3));
                    Population.Add(újszülött);
                }
            }

        }

        public List<PErson> GetPopulation(string csvpath)
        {
            List<PErson> population = new List<PErson>();

            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    population.Add(new PErson()
                    {
                        BirthYear = int.Parse(line[0]),
                        Gender = (Gender)Enum.Parse(typeof(Gender), line[1]),
                        NbrOfChildren = int.Parse(line[2])
                    });
                }
            }

            return population;
        }
        public List<BirthProbability> GetBirthProbabilities(string csvpath)
        {
            List<BirthProbability> population = new List<BirthProbability>();

            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    population.Add(new BirthProbability()
                    {
                        age = int.Parse(line[0]),
                        NbrOfChildren = int.Parse(line[1]),
                        P = double.Parse(line[2])
                    });
                }
            }

            return population;
        }
        public List<DeathProbability> GetDeathProbabilities(string csvpath)
        {
            List<DeathProbability> population = new List<DeathProbability>();

            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    population.Add(new DeathProbability()
                    {
                        age = int.Parse(line[0]),
                        Gender = (Gender)Enum.Parse(typeof(Gender), line[1]),
                        P = double.Parse(line[2])
                    });
                }
            }

            return population;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            for (int year = 2005; year <= 2024; year++)
            {

                for (int i = 0; i < Population.Count; i++)
                {
                    SimStep(year, Population[i]);
                }

                int nbrOfMales = (from x in Population
                                  where x.Gender == Gender.Male && x.IsAlive
                                  select x).Count();
                int nbrOfFemales = (from x in Population
                                    where x.Gender == Gender.Female && x.IsAlive
                                    select x).Count();
                Console.WriteLine(
                    string.Format("Év:{0} Fiúk:{1} Lányok:{2}", year, nbrOfMales, nbrOfFemales));
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            //openfiledialog
        }
    }
}
