using System.Text.Json.Serialization;

namespace Demo.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Manager { get; set; }
        // [JsonIgnore]
        public List<Employee>? Employees { get; set; }
    }
}
