using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;
using System.Text.Json;

namespace WebApplication1
{
    public class DbOperation
    {
        public static readonly string NAME_PATH = AppDomain.CurrentDomain.BaseDirectory;
        public static readonly string NAME_DIRECTORY = NAME_PATH + @"\Database\";
        public static readonly string NAME_FILE = NAME_DIRECTORY+"Person.json";

        public static List<Person> ReadData()
        {
            if (!Directory.Exists(NAME_DIRECTORY))
            {
                Directory.CreateDirectory(NAME_DIRECTORY);
            }
            if (File.Exists(NAME_FILE))
            {
                // leggo i dati in formato json e ne prendo il riferimento sotto forma di stringa
                // dalla stringa letta la deserializzo in una lista di Person e la restituisco

                List<Person> listPerson = new List<Person>();
                try
                {
                    using StreamReader reader = new(NAME_FILE);
                    var json = reader.ReadToEnd();
                    listPerson = JsonSerializer.Deserialize<List<Person>>(json);
                    reader.Close();
                } catch (Exception ex) {
                    listPerson = new List<Person>();
                }
                return listPerson;
            }

            return new List<Person>();
        }

        public static void AddData(string fiscalCode, string name, string surname)
        {
            if (!Directory.Exists(NAME_DIRECTORY))
            {
                Directory.CreateDirectory(NAME_DIRECTORY);
            }
            if (!File.Exists(NAME_FILE))
            {
                File.Create(NAME_FILE).Close();
            }

            List<Person> list = ReadData();
            var person = new Person(fiscalCode, name, surname);
            list.Add(person);

            var options = new JsonSerializerOptions { WriteIndented = true };
            var serializedList = JsonSerializer.Serialize(list, options);

            try
            {
                File.WriteAllText(NAME_FILE, serializedList);
            }
            catch (Exception ex)
            {

            }
        }
        public static void DeleteData(string fiscalCode)
        {
            if (Directory.Exists(NAME_DIRECTORY)) {
               if(File.Exists(NAME_FILE))
                {
                   var listPerson = ReadData();
                    if (listPerson.Count > 0)
                    {
                       var personToDelete = listPerson.Find(e => e.FiscalCode == fiscalCode);
                       if(!Object.ReferenceEquals(personToDelete, null))
                        {
                            if (listPerson.Remove(personToDelete))
                            {
                                var options = new JsonSerializerOptions { WriteIndented = true };
                                string? textListPerson = JsonSerializer.Serialize(listPerson, options);
                                File.WriteAllText(NAME_FILE, textListPerson);
                            }

                        } 
                       
                    }
                }
               

            }
        }
    }
}
