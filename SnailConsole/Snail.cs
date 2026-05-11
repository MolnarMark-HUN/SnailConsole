using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnailConsole
{
    public class Snail
    {
        public int id {  get; set; }
        public string snailName { get;set;  }
        public string species { get;set;  }
        public double shellDiameter { get;set;  }
        public string habitat { get;set;  }
        public Snail(string snailName,string species,int shellDiameter,string habitat)
        {
           this.snailName = snailName;
            this.species = species;
            this.shellDiameter = shellDiameter;
            this.habitat = habitat;
        }
        public override string ToString()
        {
            return $"{id} |{snailName} | {species} | {habitat} | {shellDiameter}cm";
        }
    }

}
