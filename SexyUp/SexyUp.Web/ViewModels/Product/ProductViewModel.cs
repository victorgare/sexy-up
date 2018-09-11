using System.ComponentModel.DataAnnotations;

namespace SexyUp.Web.ViewModels.Product
{
    public class ProductViewModel
    {
        public string Id { get; set; }

        [Required, Display(Name = "Nome")]
        public string Name { get; set; }

        [Required, Display(Name = "Descrição")]
        public string Description { get; set; }

        [Required, Display(Name = "Preço")]
        public string Price { get; set; }

        [Display(Name = "Peso (kg)")]
        public string Weight { get; set; }

        [Display(Name = "Altura da caixa (cm)")]
        public string BoxHeight { get; set; }

        [Display(Name = "Largura da caixa (cm)")]
        public string BoxWidth { get; set; }

        [Display(Name = "Profundidade da caixa (cm)")]
        public string BoxDepth { get; set; }

        [Display(Name = "Quantidade")]
        public string Unit { get; set; }

        [Display(Name = "Unidade de medida")]
        public string Measure { get; set; }

        [Display(Name = "Marca")]
        public string Brand { get; set; }

        [Display(Name = "Categoria")]
        public string Category { get; set; }

    }
}