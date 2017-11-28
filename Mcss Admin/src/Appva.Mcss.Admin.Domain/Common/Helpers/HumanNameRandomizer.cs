// <copyright file="HumanNameRandomizer.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain
{
    #region Imports.

    using System.Collections.Generic;
    using Appva.Core.Utilities;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal static class HumanNameRandomizer
    {
        /// <summary>
        /// A collection of the most common male and female given names.
        /// <externalLink>
        ///     <linkText>Statistics Sweden</linkText>
        ///     <linkUri>
        ///         http://www.scb.se/be0001/
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        private static readonly IReadOnlyDictionary<Gender, IReadOnlyList<string>> GivenNames = new Dictionary<Gender, IReadOnlyList<string>>
        {
            { 
                Gender.Male, new List<string> 
                {
                    "Lars", "Anders", "Mikael", "Johan", "Karl", "Per", "Erik", "Jan", "Peter", 
                    "Thomas", "Daniel", "Fredrik", "Hans", "Bengt", "Andreas", "Stefan", "Mats", 
                    "Bo", "Marcus", "Magnus", "Mattias", "Nils ", "Jonas", "Sven", "Niklas", 
                    "Martin", "Leif", "Oskar", "Björn", "Alexander", "Patrik", "Ulf", "Christer", 
                    "Viktor", "Henrik", "Joakim", "David", "Kjell", "Simon", "Emil", "Filip", 
                    "Christoffer", "Tommy", "Rolf", "Robert", "Stig", "Gustav", "Anton", "Göran", 
                    "Lennart", "Christian", "Håkan", "Rickard", "Mohamed", "John", "Robin", "William", 
                    "Tobias", "Jonathan", "Sebastian", "Jakob", "Lucas", "Kent", "Adam", "Linus", 
                    "Roger", "Claes", "Axel", "Elias", "Gunnar", "Jesper", "Kurt", "Jörgen", "Åke", 
                    "Kenneth", "Jimmy", "Rasmus", "Isak", "Olof", "Oliver", "Hugo ", "Albin", 
                    "Johnny", "Max", "Joel", "Dennis", "Arne", "Ludvig", "Pontus", "Torbjörn", 
                    "Bertil", "Olle ", "Dan", "Bernt", "Kevin", "Sten", "Tony", "Roland", "Samuel", 
                    "Jens", "Jan-Erik", "Lars-Erik", "Per-Olof", "Jan-Olof", "Karl-Erik", "Lars-Göran", 
                    "Sven-Erik", "Carl-Johan", "Per-Erik", "Lars-Olof"

                }
            },
            { 
                Gender.Female, new List<string> 
                {
                    "Anna", "Eva", "Maria", "Karin", "Kristina", "Lena", "Kerstin", "Sara", "Emma", 
                    "Ingrid", "Marie", "Malin", "Birgitta", "Jenny", "Inger", "Annika", "Monica", 
                    "Linda", "Susanne", "Ulla", "Elin ", "Hanna", "Johanna", "Carina", "Sofia", 
                    "Elisabeth", "Katarina", "Emelie", "Julia", "Ida ", "Linnéa", "Helena", "Åsa", 
                    "Margareta", "Anette", "Marianne", "Anita", "Camilla", "Gunilla", "Sandra", 
                    "Barbro", "Ann", "Anneli", "Siv", "Therese", "Amanda ", "Cecilia", "Josefin", 
                    "Jessica", "Helen", "Lisa", "Caroline", "Frida", "Ulrika", "Elsa", "Gun", "Maja", 
                    "Matilda", "Berit", "Madeleine", "Rebecka", "Agneta", "Britt", "Sofie", "Alice", 
                    "Pia", "Yvonne", "Ebba", "Klara", "Lina", "Mona", "Ann-Marie", "Louise", 
                    "Isabelle", "Sonja", "Ann-Christin", "Rut", "Astrid", "Birgit", "Erika", "Inga", 
                    "Moa", "Nathalie", "Gunnel", "Alexandra", "Viktoria", "Britt-Marie", "Ellen", 
                    "Wilma", "Ella ", "Felicia", "Emilia", "Pernilla", "Lisbeth", "Gerd", "Irene", 
                    "Alva", "Charlotte", "Agnes", "Ingela", "Ann-Marie", "Ann-Christin", "Britt-Marie", 
                    "Ann-Charlotte", "Anna-Karin", "Maj-Britt", "Ann-Sofie", "Marie-Louise", 
                    "Anna-Lena", "Rose-Marie"
                }
            }
        };

        /// <summary>
        /// A collection of the most common family names.
        /// <externalLink>
        ///     <linkText>Statistics Sweden</linkText>
        ///     <linkUri>
        ///         http://www.scb.se/be0001/
        ///     </linkUri>
        /// </externalLink>
        /// </summary>
        private static readonly IReadOnlyList<string> FamilyNames = new List<string>
        {
            "Andersson",   "Johansson",  "Karlsson",   "Nilsson",    "Eriksson",  "Larsson",     "Olsson", 
            "Persson",     "Svensson",   "Gustafsson", "Pettersson", "Jonsson",   "Jansson",     "Jonasson",
            "Hansson",     "Bengtsson",  "Jönsson",    "Lindberg",   "Jakobsson", "Magnusson",   "Holmqvist",
            "Olofsson",    "Lindström",  "Lindqvist",  "Lindgren",   "Axelsson",  "Berg",        "Bergström", 
            "Lundberg",    "Lundgren",   "Lundqvist",  "Lind",       "Mattsson",  "Berglund",    "Lindholm",
            "Fredriksson", "Sandberg",   "Henriksson", "Forsberg",   "Sjöberg",   "Wallin",      "Sundström",
            "Engström",    "Danielsson", "Håkansson",  "Eklund",     "Lundin",    "Gunnarsson",  "Holm", 
            "Björk",       "Bergman",    "Samuelsson", "Fransson",   "Wikström",  "Isaksson",    "Bergqvist", 
            "Arvidsson",   "Nyström",    "Holmberg",   "Löfgren",    "Söderberg", "Nyberg",      "Claesson", 
            "Blomqvist",   "Mårtensson", "Nordström",  "Lundström",  "Eliasson",  "Pålsson",     "Åström",
            "Björklund",   "Viklund",    "Berggren",   "Sandström",  "Lund",      "Mohamed",     "Nordin", 
            "Ali",         "Ström",      "Åberg",      "Hermansson", "Ekström",   "Holmgren",    "Sundberg", 
            "Hedlund",     "Dahlberg",   "Hellström",  "Sjögren",    "Falk",      "Abrahamsson", "Martinsson", 
            "Öberg",       "Blom",       "Andreasson", "Ek",         "Månsson",   "Strömberg",   "Åkesson", 
            "Hansen",      "Norberg"
        };

        /// <summary>
        /// Creates a new gender based <see cref="HumanName"/>.
        /// </summary>
        /// <param name="gender">The gender</param>
        /// <returns>A new <see cref="HumanName"/> instance</returns>
        public static HumanName Random(Gender gender)
        {
            var indexGiven  = RandomNumber.CreateNew(0, GivenNames[gender].Count);
            var indexFamily = RandomNumber.CreateNew(0, FamilyNames.Count);
            return HumanName.New(GivenNames[gender][indexGiven], FamilyNames[indexFamily]);
        }
    }
}