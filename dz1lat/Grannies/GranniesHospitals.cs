using System;
using System.Collections.Generic;
using System.Linq;
namespace dz1lat.Grannies
{
    public struct Disease
    {
        public string Name { get; set; }
        public string Medicine { get; set; }

        public Disease(string name, string medicine)
        {
            Name = name;
            Medicine = medicine;
        }
    }
    public struct Granny
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public List<Disease> Diseases { get; set; }

        public Granny(string name, int age, List<Disease> diseases)
        {
            Name = name;
            Age = age;
            Diseases = diseases != null ? diseases : new List<Disease>();
        }

        public override string ToString()
        {
            if (Diseases.Count == 0)
            {
                return Name + " (Возраст: " + Age + ", Болезни: [Нет болезней (только спросить)])";
            }

            string diseaseList = "";
            for (int i = 0; i < Diseases.Count; i++)
            {
                diseaseList += Diseases[i].Name;
                if (i < Diseases.Count - 1)
                {
                    diseaseList += ", ";
                }
            }

            return Name + " (Возраст: " + Age + ", Болезни: [" + diseaseList + "])";
        }
    }
    public struct Hospital
    {
        public string Name { get; set; }
        public HashSet<string> TreatableDiseases { get; set; }
        public int Capacity { get; set; }
        public List<Granny> Patients { get; set; }

        public Hospital(string name, List<string> treatableDiseases, int capacity)
        {
            Name = name;
            TreatableDiseases = new HashSet<string>(treatableDiseases, StringComparer.OrdinalIgnoreCase);
            Capacity = capacity;
            Patients = new List<Granny>();
        }

        public double FullnessPercentage
        {
            get
            {
                if (Capacity == 0) return 0;
                return Math.Round((double)Patients.Count / Capacity * 100, 1);
            }
        }

        public override string ToString()
        {
            string diseases = string.Join(", ", TreatableDiseases);
            return "Больница: '" + Name + "' | Лечит: [" + diseases + "] | Занято: " + Patients.Count + "/" + Capacity + " (" + FullnessPercentage + "%)";
        }
    }
}
