using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyProject.Model
{
    public class Titan
    {
        public string IdTitan { get; set; }
        public string? Name { get; set; }
        public string? Picture { get; set; }
        public string? Height { get; set; }
        public string? Abilities { get; set; }
        public string? Current_inheritor { get; set; }
        public string? Former_inheritor { get; set; }
        public string? Allegiance { get; set; }
        public string IdUser { get; set; }

        [JsonIgnore]
        public User? User { get; set; }

        // Constructeur sans paramètre pour EF
        public Titan() { }


        //def ctor
        public Titan(string id, string name, string picture, string height, string abilities, string current_inheritor, string former_inheritor, string allegiance, string idUser)
        {
            IdTitan = id;
            Name = name;
            Picture = picture;
            Height = height;
            Abilities = abilities;
            Current_inheritor = current_inheritor;
            Former_inheritor = former_inheritor;
            Allegiance = allegiance;
            IdUser = idUser;
        }

    }
}   