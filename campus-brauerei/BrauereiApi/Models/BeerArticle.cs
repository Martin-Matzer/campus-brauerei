using System.ComponentModel.DataAnnotations;

namespace BrauereiApi.Models;

public class BeerArticle
{
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Range(0, int.MaxValue)]
        public int Lagerbestand { get; set; }

        [Range(0, int.MaxValue)]
        public int Mindestbestand { get; set; }

        [Range(0.1, 5.0)]
        public double Flaschengroesse { get; set; }
    }
